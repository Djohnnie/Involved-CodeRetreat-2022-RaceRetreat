namespace RaceRetreat.Domain.Actions;

public class MineRockAction : IRaceAction
{
    public Direction Direction { get; set; }
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
            logger($"{PlayerName} is attacked and cannot mine a rock!");

            return;
        }

        RaceTile? newLocation = null;

        if (Direction == Direction.North)
        {
            newLocation = map[location.X, location.Y - 1];
        }
        else if (Direction == Direction.South)
        {
            newLocation = map[location.X, location.Y + 1];
        }
        else if (Direction == Direction.East)
        {
            newLocation = map[location.X + 1, location.Y];
        }
        else if (Direction == Direction.West)
        {
            newLocation = map[location.X - 1, location.Y];
        }

        if (newLocation != null)
        {
            SetupNewLCheckAndMineLocation(newLocation, logger);
        }
        else
        {
            logger($"{PlayerName} cannot mine a rock that is not there!");
        }
    }

    private void SetupNewLCheckAndMineLocation(RaceTile newLocation, Action<string> logger)
    {
        if (newLocation.IsDrivable && newLocation.HasRock)
        {
            newLocation.Overlay = OverlayKind.O_00;

            logger($"{PlayerName} successfully mined a rock!");

            return;
        }

        if (!newLocation.IsDrivable)
        {
            logger($"{PlayerName} cannot mine a rock that is not on a road!");
        }

        if (!newLocation.HasRock)
        {
            logger($"{PlayerName} cannot mine a rock that is not there!");
        }
    }
}