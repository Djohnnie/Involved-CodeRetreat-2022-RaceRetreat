using RaceRetreat.Blazor.Extensions;
using RaceRetreat.Blazor.Helpers;
using RaceRetreat.Blazor.Hubs;
using RaceRetreat.Blazor.Runners;
using RaceRetreat.Blazor.Workers;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSignalR();
builder.Services.AddServerSideBlazor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(typeof(ApiHelper<>));
builder.Services.AddSingleton<LevelsHelper>();
builder.Services.AddSingleton<ConfigurationHelper>();
builder.Services.AddSingleton<AzureTableHelper>();
builder.Services.AddSingleton<PlaysHelper>();
builder.Services.AddSingleton<GameHelper>();
builder.Services.AddSingleton<LevelBuilderHelper>();
builder.Services.AddSingleton<GraphicsCacheHelper>();
builder.Services.AddSingleton<GameRunner>();
//builder.Services.AddTransient<MapRunner>();
builder.Services.AddSingleton<GameHub>();

builder.Services.AddHostedService<GameWorker>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.MapHub<GameHub>("/_signalr/game");
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");


app.MapOpenApiGet("GetMapByName", " /_api/maps/{mapName}", async (ApiHelper<LevelsHelper> helper, string mapName) =>
{
    return await helper.Execute(m => m.GetMapByName(mapName));
});


app.MapOpenApiGet("GetImageByMapName", "/_api/images/{mapName}", async (ApiHelper<LevelBuilderHelper> helper, string mapName) =>
{
    var sw = Stopwatch.StartNew();
    var result = await helper.ExecuteFile(m => m.BuildMap(mapName), "image/jpg");
    Debug.WriteLine($"GetImageByMapName: {sw.ElapsedMilliseconds} ms");
    return result;
});

app.MapOpenApiGet("PlayMap", " /_api/maps/{mapName}/start", async (ApiHelper<GameHelper> helper, string mapName) =>
{
    return await helper.Execute(m => m.PlayMap(mapName));
});


app.Run();