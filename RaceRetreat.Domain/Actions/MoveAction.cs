namespace RaceRetreat.Domain.Actions;

public class MoveAction : IRaceAction
{
    public Direction Direction { get; set; }
    public string PlayerName { get; set; }


    public void ExecuteAction(RaceMap map, Configuration configuration)
    {
        var location = map.LocatePlayer(PlayerName);
        var player = location.Players.FirstOrDefault(x => x.PlayerName == PlayerName);

        if (player == null || player.Attacked)
            return;

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
            SetupNewLocation(location, newLocation, player, configuration);
    }

    private void SetupNewLocation(RaceTile oldLocation, RaceTile newLocation, Player player, Configuration configuration)
    {
        if (newLocation.IsDrivable && !newLocation.HasRock)
        {
            player.Points += configuration.PointsPerSuccessfulMove;

            oldLocation.Players.Remove(player);
            newLocation.Players.Add(player);

            //todo appsettings
            if (newLocation.HasOil)
                player.OilTicksRemaining = 4;
        }
    }
}