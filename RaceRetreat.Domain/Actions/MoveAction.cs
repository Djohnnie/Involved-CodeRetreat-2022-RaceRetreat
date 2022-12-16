namespace RaceRetreat.Domain.Actions;

public class MoveAction : IRaceAction
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
            logger($"{PlayerName} is attacked and cannot move!");

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
            SetupNewLocation(location, newLocation, player, configuration, logger);
        }
        else
        {
            logger($"{PlayerName} cannot move to the {Direction} because there is nothing there!");
        }
    }

    private void SetupNewLocation(RaceTile oldLocation, RaceTile newLocation, Player player, Configuration configuration, Action<string> logger)
    {
        if (newLocation.IsDrivable && !newLocation.HasRock)
        {
            player.Points += configuration.PointsPerSuccessfulMove;

            oldLocation.Players.Remove(player);
            newLocation.Players.Add(player);

            if (newLocation.HasOil)
            {
                player.OilTicksRemaining = configuration.OilDamage;

                logger($"{PlayerName} drove into an oil spill!");
            }
        }
        else
        {
            if (newLocation.HasRock)
            {
                logger($"{PlayerName} drove into a rock!");
            }

            if (!newLocation.IsDrivable)
            {
                logger($"{PlayerName} cannot move to the {Direction}, because there is no road there!");
            }
        }

        if (newLocation.IsEnd)
        {
            player.Points -= configuration.DefaultPoints;

            logger($"{PlayerName} has reached the finish line!");
        }
    }
}