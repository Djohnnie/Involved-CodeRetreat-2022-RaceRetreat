namespace RaceRetreat.Domain.Actions;

public class PlaceOilAction : IRaceAction
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
            logger($"{PlayerName} is attacked and cannot place an oil spill!");

            return;
        }

        if (player.OilRemaining <= 0)
        {
            logger($"{PlayerName} has no oil available to create an oil spill!");

            return;
        }

        if (map[X, Y].IsDrivable && !map[X, Y].HasRock)
        {
            map[X, Y].Overlay = OverlayKind.O_11;

            logger($"{PlayerName} has successfully created an oil spill!");
        }

        if (!map[X, Y].IsDrivable)
        {
            logger($"{PlayerName} cannot create an oil spill on a non-drivable tile!");
        }

        if (!map[X, Y].HasRock)
        {
            logger($"{PlayerName} cannot create an oil spill on top of a rock!");
        }

        player.OilRemaining--;
    }
}