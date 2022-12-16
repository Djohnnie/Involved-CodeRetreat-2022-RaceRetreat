using RaceRetreat.Domain;

namespace RaceRetreat.Blazor.Helpers;

public class ActionLogHelper
{
    public List<ActionLog> ActionLogs { get; set; } = new();

    public void Clear()
    {
        ActionLogs.Clear();
    }

    public void Log(string message)
    {
        ActionLogs.Add(new ActionLog
        {
            TimeStamp = DateTimeOffset.UtcNow,
            Message = message
        });
    }

    public List<ActionLog> GetTopLogs()
    {
        return ActionLogs.OrderByDescending(x => x.TimeStamp).Take(50).ToList();
    }
}