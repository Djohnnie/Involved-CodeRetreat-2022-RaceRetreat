using RaceRetreat.Domain;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp;
using System.Diagnostics;
using System.Text.Json;
using SixLabors.ImageSharp.Drawing.Processing;
using RaceRetreat.Contracts;
using SixLabors.Fonts;
using SixLabors.ImageSharp.Drawing;

namespace RaceRetreat.Blazor.Helpers;

public class LevelBuilderHelper
{
    private const int TILE_SIZE = 128;

    private readonly Random _randomGenerator = new();
    private readonly PlaysHelper _playsHelper;
    private readonly GraphicsCacheHelper _graphicsCacheHelper;

    public LevelBuilderHelper(
        PlaysHelper playsHelper,
        GraphicsCacheHelper graphicsCacheHelper)
    {
        _playsHelper = playsHelper;
        _graphicsCacheHelper = graphicsCacheHelper;
    }

    public async Task<byte[]> BuildMap(string mapName)
    {
        var sw = Stopwatch.StartNew();

        var mapResourceName = BuildMapResourceName(mapName);
        var mapJson = EmbeddedResourceHelper.GetMapByResourceName(mapResourceName);
        var map = JsonSerializer.Deserialize<RaceMap>(mapJson);
        var plays = await _playsHelper.GetLastPlaysByLevel(mapName);

        var (width, height) = (map.Width * TILE_SIZE, map.Height * TILE_SIZE);

        var mapImage = _graphicsCacheHelper.GetLevelByName(mapName, () =>
        {
            var mapImage = new Image<Rgba32>(width, height);

            mapImage.Mutate(imageContext =>
            {
                var backgroundColor = Rgba32.ParseHex("#ffffff");
                imageContext.BackgroundColor(backgroundColor);

                foreach (var tile in map)
                {
                    if (tile.IsUsed)
                    {
                        var backgroundBitmap = _graphicsCacheHelper.GetImageByTileKind(TileKind.R1_00, TILE_SIZE);
                        var tileBitmap = _graphicsCacheHelper.GetImageByTileKind(tile.Kind, TILE_SIZE);
                        var overlayBitmap = _graphicsCacheHelper.GetImageByOverlayKind(tile.Overlay, TILE_SIZE);
                        var tileLocation = CalculateBounds(tile.X, tile.Y);

                        imageContext.DrawImage(backgroundBitmap, tileLocation, new GraphicsOptions
                        {
                            AlphaCompositionMode = PixelAlphaCompositionMode.SrcOver,
                            Antialias = false,
                            AntialiasSubpixelDepth = 16,
                            BlendPercentage = 1,
                            ColorBlendingMode = PixelColorBlendingMode.Normal
                        });

                        imageContext.DrawImage(tileBitmap, tileLocation, new GraphicsOptions
                        {
                            AlphaCompositionMode = PixelAlphaCompositionMode.SrcOver,
                            Antialias = false,
                            AntialiasSubpixelDepth = 16,
                            BlendPercentage = 1,
                            ColorBlendingMode = PixelColorBlendingMode.Normal
                        });

                        imageContext.DrawImage(overlayBitmap, tileLocation, new GraphicsOptions
                        {
                            AlphaCompositionMode = PixelAlphaCompositionMode.SrcOver,
                            Antialias = false,
                            AntialiasSubpixelDepth = 16,
                            BlendPercentage = 1,
                            ColorBlendingMode = PixelColorBlendingMode.Normal
                        });
                    }
                }
            });

            return mapImage;
        });

        mapImage = mapImage.Clone(imageContext =>
        {
            if (mapName == "map-0")
            {
                GeneratePathsForMap0(imageContext);
            }
            else
            {
                GenerateUI(imageContext, map);
                GeneratePathsForLevel(imageContext, map, plays);
            }
        });

        await using var mapImageStream = new MemoryStream();
        await mapImage.SaveAsJpegAsync(mapImageStream, new JpegEncoder { Quality = 100 });
        var buffer = mapImageStream.GetBuffer();

        sw.Stop();
        Debug.WriteLine($"{sw.ElapsedMilliseconds}ms");

        return buffer;
    }

    private void GeneratePathsForMap0(IImageProcessingContext imageContext)
    {
        var points = new List<Point>
            {
                new(0, 2), new(1, 2), new(2, 2),
                new(3, 2), new(4, 2), new(5, 2),
                new(6, 2), new(7, 2), new(8, 2)
            };

        for (int i = 0; i < 5; i++)
        {
            var actualPoints = new List<PointF>();

            foreach (var point in points)
            {
                var tileLocation = CalculateBounds(point.X, point.Y);
                var offsetX = _randomGenerator.Next(TILE_SIZE / 6, TILE_SIZE - TILE_SIZE / 6);
                var offsetY = _randomGenerator.Next(TILE_SIZE / 6, TILE_SIZE - TILE_SIZE / 6);
                actualPoints.Add(new PointF(tileLocation.X + offsetX, tileLocation.Y + offsetY));
            }

            var color = GenerateColorByIndex(i + 1);
            imageContext.DrawLines(Color.White, 6f, actualPoints.ToArray());
            imageContext.DrawLines(color, 2f, actualPoints.ToArray());
        }
    }

    private void GenerateUI(IImageProcessingContext imageContext, RaceMap map)
    {
        for (int x = 0; x < map.Width; x++)
        {
            var uiBitmap = _graphicsCacheHelper.GetImageByUIKind(UIKind.UI_02, TILE_SIZE);

            if (x == 0)
            {
                uiBitmap = _graphicsCacheHelper.GetImageByUIKind(UIKind.UI_01, TILE_SIZE);
            }

            if (x == map.Width - 1)
            {
                uiBitmap = _graphicsCacheHelper.GetImageByUIKind(UIKind.UI_03, TILE_SIZE);
            }

            var uiLocation = CalculateBounds(x, 0);

            imageContext.DrawImage(uiBitmap, uiLocation, new GraphicsOptions
            {
                AlphaCompositionMode = PixelAlphaCompositionMode.SrcOver,
                Antialias = false,
                AntialiasSubpixelDepth = 16,
                BlendPercentage = 1,
                ColorBlendingMode = PixelColorBlendingMode.Normal
            });
        }

        var font = SystemFonts.Get("Consolas").CreateFont(40, FontStyle.Bold);
        var textLeft = "Total rounds:";
        var textRight = "Time per round: 0.1 sec.";

        var offsetX = 50;

        var textLeftBounds = TextMeasurer.MeasureBounds(textLeft, new TextOptions(font));
        var textRightBounds = TextMeasurer.MeasureBounds(textRight, new TextOptions(font));

        imageContext.DrawText(textLeft, font, Color.Black, new PointF(offsetX + 2, TILE_SIZE / 2f - textLeftBounds.Height / 2f + 2));
        imageContext.DrawText(textLeft, font, Color.White, new PointF(offsetX, TILE_SIZE / 2f - textLeftBounds.Height / 2f));

        imageContext.DrawText(textRight, font, Color.Black, new PointF(TILE_SIZE * map.Width - offsetX + 2 - textRightBounds.Width, TILE_SIZE / 2f - textRightBounds.Height / 2f + 2));
        imageContext.DrawText(textRight, font, Color.White, new PointF(TILE_SIZE * map.Width - offsetX - textRightBounds.Width, TILE_SIZE / 2f - textRightBounds.Height / 2f));

        var uiRoundShadowBitmap = _graphicsCacheHelper.GetImageByUIKind(UIKind.UI_11);
        var uiRoundBitmap = _graphicsCacheHelper.GetImageByUIKind(UIKind.UI_13);

        for (int r = 0; r < 30; r++)
        {
            var uiRoundLocation = new Point(
                (int)textLeftBounds.Width + offsetX * 2 + r * (uiRoundBitmap.Width + 10),
                TILE_SIZE / 2 - uiRoundBitmap.Height / 2);

            imageContext.DrawImage(uiRoundShadowBitmap, uiRoundLocation, new GraphicsOptions
            {
                AlphaCompositionMode = PixelAlphaCompositionMode.SrcOver,
                Antialias = false,
                AntialiasSubpixelDepth = 16,
                BlendPercentage = 1,
                ColorBlendingMode = PixelColorBlendingMode.Normal
            });

            if (r < new Random().Next(10, 20))
            {
                imageContext.DrawImage(uiRoundBitmap, uiRoundLocation, new GraphicsOptions
                {
                    AlphaCompositionMode = PixelAlphaCompositionMode.SrcOver,
                    Antialias = false,
                    AntialiasSubpixelDepth = 16,
                    BlendPercentage = 1,
                    ColorBlendingMode = PixelColorBlendingMode.Normal
                });
            }
        }
    }

    private void GeneratePathsForLevel(IImageProcessingContext imageContext, RaceMap map, GetLastPlaysByLevelResponse plays)
    {
        foreach (var play in plays)
        {
            var color = GenerateColorByIndex(play.Index);
            var actualPoints = new List<PointF>();
            var start = map.Single(x => x.Kind == TileKind.R1_00);
            var lastPoint = new Point(start.X, start.Y);

            var tileLocation = CalculateBounds(lastPoint.X, lastPoint.Y);
            var offsetX = _randomGenerator.Next(TILE_SIZE / 6, TILE_SIZE - TILE_SIZE / 6);
            var offsetY = _randomGenerator.Next(TILE_SIZE / 6, TILE_SIZE - TILE_SIZE / 6);
            actualPoints.Add(new PointF(tileLocation.X + offsetX, tileLocation.Y + offsetY));

            //foreach (var step in play.SubmittedSolution.Steps)
            //{
            //    switch (step)
            //    {
            //        case SolutionStep.NorthEast:
            //            if (lastPoint.Y % 2 == 0)
            //            {
            //                lastPoint = new Point(lastPoint.X, lastPoint.Y - 1);
            //            }
            //            else
            //            {
            //                lastPoint = new Point(lastPoint.X + 1, lastPoint.Y - 1);
            //            }
            //            break;
            //        case SolutionStep.East:
            //            lastPoint = new Point(lastPoint.X + 1, lastPoint.Y);
            //            break;
            //        case SolutionStep.SouthEast:
            //            if (lastPoint.Y % 2 == 0)
            //            {
            //                lastPoint = new Point(lastPoint.X, lastPoint.Y + 1);
            //            }
            //            else
            //            {
            //                lastPoint = new Point(lastPoint.X + 1, lastPoint.Y + 1);
            //            }
            //            break;
            //        case SolutionStep.SouthWest:
            //            if (lastPoint.Y % 2 == 0)
            //            {
            //                lastPoint = new Point(lastPoint.X - 1, lastPoint.Y + 1);
            //            }
            //            else
            //            {
            //                lastPoint = new Point(lastPoint.X, lastPoint.Y + 1);
            //            }
            //            break;
            //        case SolutionStep.West:
            //            lastPoint = new Point(lastPoint.X - 1, lastPoint.Y);
            //            break;
            //        case SolutionStep.NorthWest:
            //            if (lastPoint.Y % 2 == 0)
            //            {
            //                lastPoint = new Point(lastPoint.X - 1, lastPoint.Y - 1);
            //            }
            //            else
            //            {
            //                lastPoint = new Point(lastPoint.X, lastPoint.Y - 1);
            //            }
            //            break;
            //    }

            //    tileLocation = CalculateBounds(lastPoint.X, lastPoint.Y);
            //    offsetX = _randomGenerator.Next(TILE_SIZE / 6, TILE_SIZE - TILE_SIZE / 6);
            //    offsetY = _randomGenerator.Next(TILE_SIZE / 6, TILE_SIZE - TILE_SIZE / 6);
            //    actualPoints.Add(new PointF(tileLocation.X + offsetX, tileLocation.Y + offsetY));
            //}

            imageContext.DrawLines(Color.White, 6f, actualPoints.ToArray());
            imageContext.DrawLines(color, 2f, actualPoints.ToArray());
        }
    }

    private Color GenerateColorByIndex(int index)
    {
        return index switch
        {
            1 => Color.Blue,
            2 => Color.Red,
            3 => Color.Yellow,
            4 => Color.Purple,
            5 => Color.Green,
            6 => Color.Bisque,
            7 => Color.Brown,
            8 => Color.Cyan,
            9 => Color.Magenta,
            _ => Color.Black
        };
    }

    private string BuildMapResourceName(string mapName)
    {
        return $"RaceRetreat.Blazor.Maps.{mapName}.racejson";
    }

    private Point CalculateBounds(int x, int y)
    {
        var tileWidth = TILE_SIZE;
        var tileHeight = TILE_SIZE;

        var xOffset = x * tileWidth;
        var yOffset = y * tileHeight;

        return new Point(xOffset, yOffset);
    }
}