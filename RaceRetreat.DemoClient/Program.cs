using Microsoft.AspNetCore.SignalR.Client;
using RaceRetreat.DemoClient.Domain;
using RaceRetreat.DemoClient.Extensions;


Console.WriteLine("RaceRetreat Client");
Console.WriteLine("------------------");
Console.WriteLine();

const string USERNAME = "Baggeraar";


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
        await _connection.Login(USERNAME);
    }
};



_connection.OnReceiveGameState(async gameState =>
{
    Console.WriteLine($"{gameState.MapName} | {gameState.CurrentRound}/{gameState.Rounds}");

    Direction? direction = gameState.CurrentRound switch
    {
        >= 0 and <= 7 => Direction.East,
        >= 8 and <= 11 => Direction.South,
        >= 12 and <= 18 => Direction.East,
        _ => null
    };

    if (direction != null)
    {
        await _connection.ExecuteMoveAction(direction.Value);
    }

    await _connection.ExecuteAttackPlayerAction("Joske");

    await _connection.ExecutePlaceOilAction(4, 6);

    await _connection.ExecutePlaceRockAction(5, 7);

    await _connection.ExecuteMineRockAction(Direction.North);
});

await _connection.StartAsync();
await _connection.Login(USERNAME);

Console.ReadKey();