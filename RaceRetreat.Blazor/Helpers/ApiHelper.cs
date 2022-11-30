using System.Diagnostics;
using System.Linq.Expressions;

namespace RaceRetreat.Blazor.Helpers;

public class ApiHelper<TManager>
{
    private readonly TManager _manager;
    private readonly ILogger<ApiHelper<TManager>> _logger;

    public ApiHelper(TManager manager, ILogger<ApiHelper<TManager>> logger)
    {
        _manager = manager;
        _logger = logger;
    }

    public async Task<IResult> Execute<TResult>(Expression<Func<TManager, Task<TResult>>> managerCall)
    {
        return await Try(async () =>
        {
            var logicCall = managerCall.Compile();

            TResult result = await logicCall(_manager);
            return result != null ? Results.Ok(result) : Results.NotFound();
        });
    }

    public async Task<IResult> ExecuteFile(Expression<Func<TManager, Task<byte[]>>> managerCall, string contentType)
    {
        return await Try(async () =>
        {
            var logicCall = managerCall.Compile();

            var result = await logicCall(_manager);
            return result != null ? Results.File(result, contentType) : Results.NotFound();
        });
    }

    public async Task<IResult> Execute(Func<TManager, Task> managerCall)
    {
        return await Try(async () =>
        {
            await managerCall(_manager);
            return Results.Ok();
        });
    }

    public async Task<IResult> Post<TResult>(Func<TManager, Task<TResult>> managerCall)
    {
        return await Try(async () =>
        {
            var result = await managerCall(_manager);
            return result != null ? Results.Created("", result) : Results.NotFound();
        });
    }

    private async Task<IResult> Try(Func<Task<IResult>> action)
    {
        try
        {
            var stopwatch = Stopwatch.StartNew();

            var result = await action();

            _logger.LogTrace($"REQUEST: {stopwatch.ElapsedMilliseconds}ms");

            return result;
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
}