namespace RaceRetreat.Domain;

public record Configuration
{
    public int PointsPerSuccessfulMove { get; init; }
    public int DefaultPoints { get; init; }
}