using System.Text.RegularExpressions;

namespace Common.Utils.RgbNormalizer;

public static partial class RgbNormalizer
{
    [GeneratedRegex(@"rgba\((\d+),\s*(\d+),\s*(\d+),\s*1\.?0*\)")]
    private static partial Regex RgbaRegex();
    
    [GeneratedRegex(@"rgb\((\d+),\s*(\d+),\s*(\d+)\)")]
    private static partial Regex RgbRegex();

    public static string Normalize(string color)
    {
        var rgbaMatch = RgbaRegex().Match(color);
        if (rgbaMatch.Success)
            return $"rgb({rgbaMatch.Groups[1].Value}, {rgbaMatch.Groups[2].Value}, {rgbaMatch.Groups[3].Value})";

        var rgbMatch = RgbRegex().Match(color);
        return rgbMatch.Success
            ? color
            : string.Empty;
    }
}