namespace RaceRetreat.Domain;

//Todo a file for each class =)
public interface IRaceAction
{
    string PlayerName { get; set; }
    void ExecuteAction(RaceMap map);
}

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

        if(newLocation != null)
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

public class PlaceRockAction : IRaceAction
{
    public int X { get; set; }
    public int Y { get; set; }
    public string PlayerName { get; set; }

    //Todo make random?
    public void ExecuteAction(RaceMap map)
    {
        var location = map.LocatePlayer(PlayerName);
        var player = location.Players.FirstOrDefault(x => x.PlayerName == PlayerName);

        if (player == null || player.Attacked)
            return;

        if (map[X, Y].IsDrivable)
            map[X, Y].Overlay = OverlayKind.O_21;
    }
}

public class PlaceOilAction : IRaceAction
{
    public int X { get; set; }
    public int Y { get; set; }
    public string PlayerName { get; set; }

    public void ExecuteAction(RaceMap map)
    {
        var location = map.LocatePlayer(PlayerName);
        var player = location.Players.FirstOrDefault(x => x.PlayerName == PlayerName);

        if (player == null || player.Attacked)
            return;

        if (map[X, Y].IsDrivable && !map[X,Y].HasRock)
            map[X, Y].Overlay = OverlayKind.O_11;
    }
}

public class MineRockAction : IRaceAction
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
            SetupNewLCheckAndMineLocation(newLocation);
    }

    private void SetupNewLCheckAndMineLocation(RaceTile newLocation)
    {
        if (newLocation.IsDrivable && newLocation.HasRock)
        {
            newLocation.Overlay = OverlayKind.O_00;
        }
    }
}

public class AttackPlayerAction : IRaceAction
{
    public string PlayerNameToAttack { get; set; }
    public string PlayerName { get; set; }

    public void ExecuteAction(RaceMap map)
    {
        var location = map.LocatePlayer(PlayerName);
        var locationPlayerToAttack = map.LocatePlayer(PlayerNameToAttack);
        var playerToAttack = location.Players.FirstOrDefault(x => x.PlayerName == PlayerNameToAttack);

        if (location.X == locationPlayerToAttack.X && location.Y == locationPlayerToAttack.Y)
            playerToAttack.Attacked = true;

        if (location.X == locationPlayerToAttack.X &&  Math.Abs(location.Y - locationPlayerToAttack.Y) <= 2)
            playerToAttack.Attacked = true;

        if (Math.Abs(location.X - locationPlayerToAttack.X) <= 2 && location.Y == locationPlayerToAttack.Y)
            playerToAttack.Attacked = true;
    }
}


public enum Direction
{
    North = 0,
    East = 1,
    South = 2,
    West = 3
}