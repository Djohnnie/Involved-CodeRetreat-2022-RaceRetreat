namespace RaceRetreat.Domain.Actions;

public class PlaceRockAction : IRaceAction
{
    public int X { get; set; }
    public int Y { get; set; }
    public string PlayerName { get; set; }

    public void ExecuteAction(RaceMap map, Configuration configuration, Action<string> logger)
    {
        var location = map.LocatePlayer(PlayerName);
        var player = location.Players.FirstOrDefault(x => x.PlayerName == PlayerName);

        if (player == null)
        {
            return;
        }

        if (player.Attacked)
        {
            logger($"{PlayerName} is attacked and cannot place a rock!");

            return;
        }

        if (player.RocksRemaining <= 0)
        {
            logger($"{PlayerName} has no rocks available to create a rock!");

            return;
        }

        if (map[X, Y].IsDrivable)
        {
            map[X, Y].Overlay = OverlayKind.O_21;

            logger($"{PlayerName} has successfully created a rock!");
        }
        else
        {
            logger($"{PlayerName} cannot create a rock on a non-drivable tile!");
        }

        player.RocksRemaining--;
    }
}