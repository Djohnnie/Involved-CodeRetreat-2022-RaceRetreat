namespace RaceRetreat.Blazor.Extensions;

public static class EnumerableExtensions
{
    public static IEnumerable<T> TakeLast<T>(this IEnumerable<T> enumerable, int numberOfItems)
    {
        return enumerable.Skip(Math.Max(0, enumerable.Count() - numberOfItems));
    }
}