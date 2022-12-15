namespace RaceRetreat.Domain.Actions;

public class AttackPlayerAction : IRaceAction
{
    public string PlayerNameToAttack { get; set; }
    public string PlayerName { get; set; }

    public void ExecuteAction(RaceMap map, Configuration configuration)
    {
        var location = map.LocatePlayer(PlayerName);
        var locationPlayerToAttack = map.LocatePlayer(PlayerNameToAttack);
        var playerToAttack = location.Players.FirstOrDefault(x => x.PlayerName == PlayerNameToAttack);

        if (location.X == locationPlayerToAttack.X && location.Y == locationPlayerToAttack.Y)
            playerToAttack.Attacked = true;

        if (location.X == locationPlayerToAttack.X && Math.Abs(location.Y - locationPlayerToAttack.Y) <= 2)
            playerToAttack.Attacked = true;

        if (Math.Abs(location.X - locationPlayerToAttack.X) <= 2 && location.Y == locationPlayerToAttack.Y)
            playerToAttack.Attacked = true;
    }
}