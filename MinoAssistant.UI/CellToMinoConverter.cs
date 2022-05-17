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
            if (cell.Value == null) return null;

            if (cell.Value.GetType() == typeof(string))
            {
                string targetPath = (string)cell.Value;
                if (File.Exists(targetPath))
                {
                    try
                    {

                        return new Bitmap(targetPath);
                    }
                    catch
                    {
                        return cell.Value;
                    }
                }
                else return cell.Value;
            }
            return null;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
