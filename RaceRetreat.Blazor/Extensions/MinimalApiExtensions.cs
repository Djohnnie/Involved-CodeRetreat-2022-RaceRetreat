using System.Diagnostics.CodeAnalysis;

namespace RaceRetreat.Blazor.Extensions;

public static class MinimalApiExtensions
{
    public static IEndpointConventionBuilder MapOpenApiGet(
        this IEndpointRouteBuilder endpoints, string openApiName,
        [StringSyntax("Route")] string pattern, Delegate handler)
    {
        return endpoints.MapGet(pattern, handler)
            .WithOpenApi(operation => new(operation)
            {
                OperationId = openApiName
            });
    }
}