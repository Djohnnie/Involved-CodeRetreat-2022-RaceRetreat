using Microsoft.AspNetCore.SignalR;
using RaceRetreat.Blazor.Helpers;
using RaceRetreat.Blazor.Runners;
using RaceRetreat.Contracts;
using RaceRetreat.Domain;

namespace RaceRetreat.Blazor.Hubs;

public class GameHub : Hub
{
    private readonly AzureTableHelper _azureTableHelper;
    private readonly GameRunner _gameRunner;
    private readonly Dictionary<string, string> _connectionIdPlayerDict;

    public GameHub(AzureTableHelper azureTableHelper, GameRunner gameRunner)
    {
        _azureTableHelper = azureTableHelper;
        _gameRunner = gameRunner;
        _connectionIdPlayerDict = new Dictionary<string, string>();
    }

    public async Task Login(string playerName)
    {
        await _azureTableHelper.AddPlayer(playerName);
        if(!_connectionIdPlayerDict.ContainsKey(Context.ConnectionId))
            _connectionIdPlayerDict.Add(Context.ConnectionId, playerName);
        else
            _connectionIdPlayerDict[Context.ConnectionId] = playerName;
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

    public override async Task OnDisconnectedAsync(Exception ex)
    {
        if(_connectionIdPlayerDict.ContainsKey(Context.ConnectionId))
            _connectionIdPlayerDict.Remove(Context.ConnectionId);
    }

    private string TryGetPlayerName()
    {
        if (_connectionIdPlayerDict.ContainsKey(Context.ConnectionId))
            return _connectionIdPlayerDict[Context.ConnectionId];

        throw new HubException("NotLoggedIn");
    }
}