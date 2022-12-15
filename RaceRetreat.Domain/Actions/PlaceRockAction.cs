namespace RaceRetreat.Domain.Actions;

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

        if (player == null || player.Attacked || player.RocksRemaining == 0)
            return;

        if (map[X, Y].IsDrivable)
            map[X, Y].Overlay = OverlayKind.O_21;

        player.RocksRemaining--;
    }
}