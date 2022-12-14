using RaceRetreat.Blazor.Hubs;
using RaceRetreat.Blazor.Runners;
using RaceRetreat.Contracts;
using System.Diagnostics;

namespace RaceRetreat.Blazor.Workers;

public class GameWorker : BackgroundService
{
    private readonly GameRunner _gameRunner;
    private readonly GameHub _gameHub;

    public GameWorker(GameRunner gameRunner,
        GameHub gameHub)
    {
        _gameRunner = gameRunner;
        _gameHub = gameHub;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var sw = new Stopwatch();
            // The gamerunner will tick and make sure the correct turns and speed is applied.
            var lastMapState = await _gameRunner.Tick();

            if(lastMapState == null) 
                continue;

            // Send the new gamestate to the SignalR hub.
            await _gameHub.SendGameState(new GameState
            {
                MapName = lastMapState.MapName,
                Rounds = lastMapState.Rounds,
                CurrentRound = lastMapState.CurrentRound
            });

            // Calculate remaining time to sleep.
            var delay = lastMapState.TimePerRound - (int)sw.ElapsedMilliseconds;
            await Task.Delay(delay < 0 ? 0 : delay);
        }
    }
}