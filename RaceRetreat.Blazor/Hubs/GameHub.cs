using Microsoft.AspNetCore.SignalR;
using RaceRetreat.Blazor.Helpers;
using RaceRetreat.Contracts;

namespace RaceRetreat.Blazor.Hubs;

public class GameHub : Hub
{
    private readonly AzureTableHelper _azureTableHelper;

    public GameHub(AzureTableHelper azureTableHelper)
    {
        _azureTableHelper = azureTableHelper;
    }

    public async Task Login(string playerName)
    {
        await _azureTableHelper.AddPlayer(playerName);
    }

    public async Task SendGameState(GameState gameState)
    {
        if (Clients != null)
        {
            await Clients.All.SendAsync("ReceiveGameState", gameState);
        }
    }
}