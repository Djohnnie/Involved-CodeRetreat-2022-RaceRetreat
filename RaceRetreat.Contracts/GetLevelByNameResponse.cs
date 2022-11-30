using RaceRetreat.Domain;

namespace RaceRetreat.Contracts;

public class GetLevelByNameResponse
{
    public string MapName { get; set; }
    public RaceMap Map { get; set; }
}