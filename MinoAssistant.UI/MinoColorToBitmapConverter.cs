using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using MinoAssistant.Game.Block;
using System;
using System.Globalization;
using System.IO;

namespace MinoAssistant.UI;

internal class MinoColorToBitmapConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if(value == null) return null;
        if(value.GetType() != typeof(MinoColor)) throw new ArgumentException($"Argument must be of type {nameof(MinoColor)}.");

        var minoColor = (MinoColor)value;

        string filePath = $@"C:\Users\bhatt\repos\MinoAssistant\MinoAssistant.UI\Assets\{minoColor.ToString().ToLower()}-block.png";
        if(File.Exists(filePath)) return new Bitmap(filePath);
        else return new Bitmap($@"C:\Users\bhatt\repos\MinoAssistant\MinoAssistant.UI\Assets\{MinoColor.Black.ToString().ToLower()}-block.png");
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
}
