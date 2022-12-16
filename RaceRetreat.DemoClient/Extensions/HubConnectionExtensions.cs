using Microsoft.AspNetCore.SignalR.Client;
using RaceRetreat.DemoClient.Domain;

namespace RaceRetreat.DemoClient.Extensions;

internal static class HubConnectionExtensions
{
    public static IDisposable OnReceiveGameState(this HubConnection connection, Func<GameState, Task> action)
    {
        return connection.On<GameState>("ReceiveGameState", async state =>
        {
            await action(state);
        });
    }

    public static async Task Login(this HubConnection connection, string playerName)
    {
        await connection.SendAsync(nameof(Login), playerName);
    }

    public static async Task ExecuteMoveAction(this HubConnection connection, Direction direction)
    {
        await connection.SendAsync(nameof(ExecuteMoveAction), direction);
    }

    public static async Task ExecuteAttackPlayerAction(this HubConnection connection, string playerNameToAttack)
    {
        await connection.SendAsync(nameof(ExecuteAttackPlayerAction), playerNameToAttack);
    }

    public static async Task ExecuteMineRockAction(this HubConnection connection, Direction direction)
    {
        await connection.SendAsync(nameof(ExecuteMineRockAction), direction);
    }

    public static async Task ExecutePlaceOilAction(this HubConnection connection, int x, int y)
    {
        await connection.SendAsync(nameof(ExecutePlaceOilAction), x, y);
    }

    public static async Task ExecutePlaceRockAction(this HubConnection connection, int x, int y)
    {
        await connection.SendAsync(nameof(ExecutePlaceRockAction), x, y);
    }
}