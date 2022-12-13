using Microsoft.AspNetCore.SignalR.Client;
using RaceRetreat.Contracts;

Console.WriteLine("RaceRetreat Client");
Console.WriteLine("------------------");
Console.WriteLine();

const string HOST = "https://localhost:7292/_signalr/game";
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

_connection.On<GameState>("ReceiveGameState", gameState =>
{
    Console.WriteLine($"{gameState.PlayerName} | {gameState.MapName} | {gameState.Round}");
});

await _connection.StartAsync();
await _connection.SendAsync("Login", "djohnnie");

Console.ReadKey();