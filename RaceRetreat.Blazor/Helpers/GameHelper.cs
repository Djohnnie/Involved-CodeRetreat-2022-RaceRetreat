using RaceRetreat.Blazor.Runners;

namespace RaceRetreat.Blazor.Helpers;

public class GameHelper
{
    private readonly GameRunner _gameRunner;

    public GameHelper(GameRunner gameRunner)
	{
        _gameRunner = gameRunner;
    }

    public async Task PlayMap(string mapName)
    {
        await _gameRunner.SetActiveMap(mapName);
    }
}