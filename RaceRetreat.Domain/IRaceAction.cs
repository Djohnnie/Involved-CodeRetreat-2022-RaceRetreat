namespace RaceRetreat.Domain;

public interface IRaceAction
{
    string PlayerName { get; set; }
    void ExecuteAction(RaceMap map);
}

public enum Direction
{
    North = 0,
    East = 1,
    South = 2,
    West = 3
}