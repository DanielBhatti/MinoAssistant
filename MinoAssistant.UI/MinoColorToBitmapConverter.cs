using Avalonia.Data.Converters;
using System;
using System.Globalization;
using System.IO;
using Avalonia.Media.Imaging;
using MinoAssistant.Board;
using MinoAssistant.Board.Block;

namespace MinoAssistant.UI
{
    internal class MinoColorToBitmapConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null) return null;
            if (value.GetType() != typeof(MinoColor)) throw new ArgumentException($"Argument must be of type {nameof(MinoColor)}.");

            MinoColor minoColor = (MinoColor)value;

            return new Bitmap($@"C:\Users\bhatt\repos\MinoAssistant\MinoAssistant.UI\Assets\{minoColor.ToString().ToLower()}-block.png");
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
