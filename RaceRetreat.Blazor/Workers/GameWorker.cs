using RaceRetreat.Blazor.Helpers;
using RaceRetreat.Blazor.Hubs;
using RaceRetreat.Blazor.Runners;
using RaceRetreat.Contracts;
using System.Diagnostics;

namespace RaceRetreat.Blazor.Workers;

public class GameWorker : BackgroundService
{
    private readonly GameRunner _gameRunner;
    private readonly GameHub _gameHub;
    private readonly ActionLogHelper _actionLogHelper;

    public GameWorker(GameRunner gameRunner, GameHub gameHub, ActionLogHelper actionLogHelper)
    {
        _gameRunner = gameRunner;
        _gameHub = gameHub;
        _actionLogHelper = actionLogHelper;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var sw = new Stopwatch();

            // The gamerunner will tick and make sure the correct turns and speed is applied.
            var lastMapState = await _gameRunner.Tick();

            if (lastMapState == null)
            {
                continue;
            }

            // Send the new gamestate to the SignalR hub.
            await _gameHub.SendGameState(new GameState
            {
                MapName = lastMapState.MapName,
                Rounds = lastMapState.Rounds,
                CurrentRound = lastMapState.CurrentRound,
                Map = lastMapState.Map,
                ActionLog = _actionLogHelper.GetTopLogs()
            });

            // Calculate remaining time to sleep.
            var delay = lastMapState.TimePerRound * 1000 - (int)sw.ElapsedMilliseconds;
            await Task.Delay(delay < 0 ? 0 : delay);
        }
    }
}