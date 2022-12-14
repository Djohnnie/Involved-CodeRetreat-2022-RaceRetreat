using RaceRetreat.Blazor.Runners;

namespace RaceRetreat.Blazor.Workers;

public class GameWorker : BackgroundService
{
    private readonly GameRunner _gameRunner;

    public GameWorker(GameRunner gameRunner)
    {
        _gameRunner = gameRunner;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // The gamerunner will tick and make sure the correct turns and speed is applied.
            await _gameRunner.Tick();
        }
    }
}