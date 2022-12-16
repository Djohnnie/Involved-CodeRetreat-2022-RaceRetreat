namespace RaceRetreat.Domain;

public record ActionLog
{
    public DateTimeOffset TimeStamp { get; init; }
    public string Message { get; init; }
}