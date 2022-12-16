using RaceRetreat.Contracts;
using RaceRetreat.Domain;

namespace RaceRetreat.Blazor.Helpers;

public class PlaysHelper
{
    private readonly AzureTableHelper _azureTableHelper;

    public PlaysHelper(AzureTableHelper azureTableHelper)
    {
        _azureTableHelper = azureTableHelper;
    }

    public async Task<GetPlayersResponse> GetPlayers()
    {
        var players = new GetPlayersResponse();

        var index = 1;

        await foreach (var player in _azureTableHelper.GetPlayers())
        {
            players.Add(new Player
            {
                Index = index,
                PlayerName = player.RowKey
            });

            index++;
        }

        return players;
    }

    public async Task<GetPlayersResponse> GetPlayersByLevel(string levelName)
    {
        var players = new GetPlayersResponse();

        await foreach (var player in _azureTableHelper.GetPlayers())
        {
            players.Add(new Player
            {
                PlayerName = player.RowKey
            });
        }

        return players;
    }

    public async Task<GetLastPlaysByLevelResponse> GetLastPlaysByLevel(string levelName)
    {
        //var plays = await _dbContext.Plays.Where(
        //    x => x.LevelName == levelName).OrderByDescending(x => x.SubmittedOn).ToListAsync();

        //var players = await GetPlayersByLevel(levelName);

        var playsByLevel = new GetLastPlaysByLevelResponse();

        //foreach (var player in players)
        //{
        //    var playsByPlayer = plays.Where(x => x.PlayerName == player.PlayerName).ToList();
        //    if (playsByPlayer.Count > 0)
        //    {
        //        var lastPlay = playsByPlayer.First();

        //        playsByLevel.Add(new PlayByPlayer
        //        {
        //            Index = player.Index,
        //            PlayerName = player.PlayerName,
        //            SubmittedSolution = new Solution
        //            {
        //                Description = lastPlay.Description,
        //                Steps = JsonSerializer.Deserialize<List<SolutionStep>>(lastPlay.SubmittedSolution)
        //            }
        //        });
        //    }
        //}

        return playsByLevel;
    }

    //public async Task CreatePlay(CreatePlayRequest request, string levelName)
    //{
    //    var play = new Play
    //    {
    //        Id = Guid.NewGuid(),
    //        PlayerName = request.PlayerName,
    //        LevelName = levelName,
    //        Description = request.SubmittedSolution.Description,
    //        SubmittedSolution = JsonSerializer.Serialize(request.SubmittedSolution.Steps),
    //        SubmittedOn = DateTime.UtcNow
    //    };

    //    await _dbContext.Plays.AddAsync(play);
    //    await _dbContext.SaveChangesAsync();
    //}
}