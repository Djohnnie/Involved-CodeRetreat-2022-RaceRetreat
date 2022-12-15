﻿namespace RaceRetreat.Domain.Actions;

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

        if (map[X, Y].IsDrivable && !map[X, Y].HasRock)
            map[X, Y].Overlay = OverlayKind.O_11;
    }
}