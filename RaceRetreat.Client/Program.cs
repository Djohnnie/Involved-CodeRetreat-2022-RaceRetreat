using Microsoft.AspNetCore.SignalR.Client;
using RaceRetreat.Contracts;
using RaceRetreat.Domain;

Console.WriteLine("RaceRetreat Client");
Console.WriteLine("------------------");
Console.WriteLine();

const string HOST = "https://raceretreat.azurewebsites.net/_signalr/game";
var random = new Random();
var closing = false;

var _connection = new HubConnectionBuilder()
            .WithUrl(HOST)
            .Build();

_connection.Closed += async (error) =>
{
    if (!closing)
    {
        await Task.Delay(random.Next(0, 5) * 1000);
        await _connection.StartAsync();
    }
};

_connection.On<GameState>("ReceiveGameState", async gameState =>
{

    Console.WriteLine($"{gameState.PlayerName} | {gameState.MapName} | {gameState.CurrentRound}/{gameState.Rounds}");

    await _connection.SendAsync("ExecuteMoveAction", Direction.East);
});

await _connection.StartAsync();
await _connection.SendAsync("Login", "djohnnie");

Console.ReadKey();