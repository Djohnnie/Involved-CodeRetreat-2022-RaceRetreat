using Microsoft.AspNetCore.SignalR;
using RaceRetreat.Blazor.Helpers;
using RaceRetreat.Blazor.Runners;
using RaceRetreat.Contracts;
using RaceRetreat.Domain;
using RaceRetreat.Domain.Actions;

namespace RaceRetreat.Blazor.Hubs;

public class GameHub : Hub
{
    private readonly AzureTableHelper _azureTableHelper;
    private readonly GameRunner _gameRunner;

    private readonly Dictionary<string, string> _connectedPlayerCache = new();

    public GameHub(AzureTableHelper azureTableHelper, GameRunner gameRunner)
    {
        _azureTableHelper = azureTableHelper;
        _gameRunner = gameRunner;
    }

    public async Task Login(string playerName)
    {
        if (!_connectedPlayerCache.ContainsKey(Context.ConnectionId))
        {
            _connectedPlayerCache.Add(Context.ConnectionId, playerName);
        }
        else
        {
            _connectedPlayerCache[Context.ConnectionId] = playerName;
        }
        
        await _azureTableHelper.AddPlayer(playerName);
    }

    public async Task ExecuteMoveAction(Direction direction)
    {
        await _gameRunner.AddAction(new MoveAction
        {
            Direction = direction,
            PlayerName = TryGetPlayerName()
        });
    }

    public async Task ExecuteAttackPlayerAction(string playerNameToAttack)
    {
        await _gameRunner.AddAction(new AttackPlayerAction
        {
            PlayerNameToAttack = playerNameToAttack,
            PlayerName = TryGetPlayerName()
        });
    }

    public async Task ExecuteMineRockAction(Direction direction)
    {
        await _gameRunner.AddAction(new MineRockAction
        {
            Direction = direction,
            PlayerName = TryGetPlayerName()
        });
    }

    public async Task ExecutePlaceOilAction(int x, int y)
    {
        await _gameRunner.AddAction(new PlaceOilAction
        {
            X = x,
            Y = y,
            PlayerName = TryGetPlayerName()
        });
    }

    public async Task ExecutePlaceRockAction(int x, int y)
    {
        await _gameRunner.AddAction(new PlaceRockAction
        {
            X = x,
            Y = y,
            PlayerName = TryGetPlayerName()
        });
    }

    public async Task SendGameState(GameState gameState)
    {
        if (Clients != null)
        {
            await Clients.All.SendAsync("ReceiveGameState", gameState);
        }
    }

    public override Task OnDisconnectedAsync(Exception ex)
    {
        if (_connectedPlayerCache.ContainsKey(Context.ConnectionId))
        {
            _connectedPlayerCache.Remove(Context.ConnectionId);
        }

        return Task.CompletedTask;
    }

    private string TryGetPlayerName()
    {
        if (_connectedPlayerCache.ContainsKey(Context.ConnectionId))
        {
            return _connectedPlayerCache[Context.ConnectionId];
        }

        throw new HubException("NotLoggedIn");
    }
}