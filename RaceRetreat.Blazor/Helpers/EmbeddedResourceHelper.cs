using System.Reflection;
using System.Text;

namespace RaceRetreat.Blazor.Helpers;

public static class EmbeddedResourceHelper
{
    public static string GetMapByResourceName(string resourceName)
    {
        try
        {
            using var stream = typeof(EmbeddedResourceHelper).GetTypeInfo()
                .Assembly.GetManifestResourceStream(resourceName);
            using var reader = new StreamReader(stream, Encoding.UTF8);
            return reader.ReadToEnd();
        }

        catch
        {
            return string.Empty;
        }
    }

    public static Stream GetByResourceName(string resourceName)
    {
        return typeof(EmbeddedResourceHelper).GetTypeInfo()
            .Assembly.GetManifestResourceStream(resourceName);
    }
}