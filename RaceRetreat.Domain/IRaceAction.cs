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
        throw new NotImplementedException();
    }
}

public class PlaceRockAction : IRaceAction
{
    public int X { get; set; }
    public int Y { get; set; }
    public string PlayerName { get; set; }

    public void ExecuteAction(RaceMap map)
    {
        throw new NotImplementedException();
    }
}

public class PlaceOilAction : IRaceAction
{
    public int X { get; set; }
    public int Y { get; set; }
    public string PlayerName { get; set; }

    public void ExecuteAction(RaceMap map)
    {
        throw new NotImplementedException();
    }
}

public class MineRockAction : IRaceAction
{
    public Direction Direction { get; set; }
    public string PlayerName { get; set; }

    public void ExecuteAction(RaceMap map)
    {
        throw new NotImplementedException();
    }
}

public class AttackPlayerAction : IRaceAction
{
    public string PlayerNameToAttack { get; set; }
    public string PlayerName { get; set; }

    public void ExecuteAction(RaceMap map)
    {
        throw new NotImplementedException();
    }
}


public enum Direction
{
    North = 0,
    South = 1,
    East = 2,
    West = 3
}