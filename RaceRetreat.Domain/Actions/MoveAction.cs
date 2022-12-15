namespace RaceRetreat.Domain.Actions;

public class MoveAction : IRaceAction
{
    public Direction Direction { get; set; }
    public string PlayerName { get; set; }


    public void ExecuteAction(RaceMap map)
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
            SetupNewLocation(location, newLocation, player);
    }

    private void SetupNewLocation(RaceTile oldLocation, RaceTile newLocation, Player player)
    {
        if (newLocation.IsDrivable && !newLocation.HasRock)
        {
            oldLocation.Players.Remove(player);
            newLocation.Players.Add(player);

            //todo appsettings
            if (newLocation.HasOil)
                player.OilTicksRemaining = 4;
        }
    }
}