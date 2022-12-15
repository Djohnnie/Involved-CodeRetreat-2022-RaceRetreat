using RaceRetreat.Domain;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.Collections.Concurrent;

namespace RaceRetreat.Blazor.Helpers;

public class GraphicsCacheHelper
{
    private readonly ConcurrentDictionary<TileKind, Image> _tileCache = new();
    private readonly ConcurrentDictionary<OverlayKind, Image> _overlayCache = new();
    private readonly ConcurrentDictionary<UIKind, Image> _uiCache = new();
    private readonly ConcurrentDictionary<string, Image> _levelCache = new();

    public Image GetImageByTileKind(TileKind tileKind, int tileSize)
    {
        if (_tileCache.ContainsKey(tileKind))
        {
            return _tileCache[tileKind];
        }

        var resourceName = GetResourceNameByTileKind(tileKind);
        var tileImageStream = EmbeddedResourceHelper.GetByResourceName(resourceName);
        var tileImage = Image.Load(tileImageStream);
        tileImage.Mutate(x => x.Resize(tileSize, tileSize));
        _tileCache.TryAdd(tileKind, tileImage);

        return tileImage;
    }

    public Image GetImageByOverlayKind(OverlayKind overlayKind, int tileSize)
    {
        if (_overlayCache.ContainsKey(overlayKind))
        {
            return _overlayCache[overlayKind];
        }

        var resourceName = GetResourceNameByOverlayKind(overlayKind);
        var overlayImageStream = EmbeddedResourceHelper.GetByResourceName(resourceName);
        var overlayImage = Image.Load(overlayImageStream);
        overlayImage.Mutate(x => x.Resize(tileSize, tileSize));
        _overlayCache.TryAdd(overlayKind, overlayImage);

        return overlayImage;
    }

    public Image GetImageByUIKind(UIKind uiKind, int tileSize)
    {
        if (_uiCache.ContainsKey(uiKind))
        {
            return _uiCache[uiKind];
        }

        var resourceName = GetResourceNameByUIKind(uiKind);
        var uiImageStream = EmbeddedResourceHelper.GetByResourceName(resourceName);
        var uiImage = Image.Load(uiImageStream);
        uiImage.Mutate(x => x.Resize(tileSize, tileSize));
        _uiCache.TryAdd(uiKind, uiImage);

        return uiImage;
    }

    public Image GetImageByUIKind(UIKind uiKind)
    {
        if (_uiCache.ContainsKey(uiKind))
        {
            return _uiCache[uiKind];
        }

        var resourceName = GetResourceNameByUIKind(uiKind);
        var uiImageStream = EmbeddedResourceHelper.GetByResourceName(resourceName);
        var uiImage = Image.Load(uiImageStream);
        _uiCache.TryAdd(uiKind, uiImage);

        return uiImage;
    }

    public Image GetLevelByName(string levelName, Func<Image> levelFactory)
    {
        if (_levelCache.ContainsKey(levelName))
        {
            return _levelCache[levelName];
        }

        var levelImage = levelFactory();
        _levelCache.TryAdd(levelName, levelImage);
        return levelImage;
    }

    private string GetResourceNameByTileKind(TileKind tileKind)
    {
        return $"RaceRetreat.Blazor.Graphics.{tileKind.ToString().Replace("_", "-").ToLowerInvariant()}.png";
    }

    private string GetResourceNameByOverlayKind(OverlayKind overlayKind)
    {
        return $"RaceRetreat.Blazor.Graphics.{overlayKind.ToString().Replace("_", "-").ToLowerInvariant()}.png";
    }

    private string GetResourceNameByUIKind(UIKind uiKind)
    {
        return $"RaceRetreat.Blazor.Graphics.{uiKind.ToString().Replace("_", "-").ToLowerInvariant()}.png";
    }
}