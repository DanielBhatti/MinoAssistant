using Avalonia.Data.Converters;
using System;
using System.Globalization;
using System.IO;
using Avalonia.Media.Imaging;
using MinoAssistant.Board;

namespace MinoAssistant.UI
{
    internal class CellToMinoConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null) return null;
            if (value.GetType() != typeof(Cell)) throw new ArgumentException($"Argument must be of type {nameof(Cell)}.");

            Cell cell = (Cell)value;
            if (cell.VisibleValue == null || (string)cell.VisibleValue == "") return null;

            if (cell.VisibleValue.GetType() == typeof(string))
            {
                try
                {
                    string targetPath = (string)cell.VisibleValue;
                    return new Bitmap(targetPath);
                }
                catch
                {
                    return cell.VisibleValue;
                }
            }
            return null;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
