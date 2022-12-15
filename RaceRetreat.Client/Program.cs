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

    Direction? direction = gameState.CurrentRound switch
    {
        >= 0 and <= 8 => Direction.East,
        >= 9 and <= 12 => Direction.South,
        >= 13 and <= 19 => Direction.East,
        _ => null
    };

    if (direction != null)
    {
        await _connection.SendAsync("ExecuteMoveAction", direction);
    }
});

await _connection.StartAsync();
await _connection.SendAsync("Login", "djohnnie");

Console.ReadKey();