using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using ShadUI;

namespace EOLauncher.Converters;

public static class ThemeModeConverters
{
    private static readonly Dictionary<ThemeMode, string> Icons = new()
    {
        { ThemeMode.System, "\uE2B2" },
        { ThemeMode.Light, "\uE2B1" },
        { ThemeMode.Dark, "\uE122" }
    };

    public static readonly IValueConverter ToLucideIcon =
        new FuncValueConverter<ThemeMode, string>(mode => Icons.TryGetValue(mode, out var icon) ? icon : Icons[0]);
}