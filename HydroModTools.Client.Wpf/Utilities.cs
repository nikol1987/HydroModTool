using System.Windows.Media;

namespace HydroModTools.Client.Wpf
{
    internal static class Utilities
    {
        public static Brush GetColorFromBridgepourRibbonColor(string name)
        {
            var color = name switch
            {
                "orange" => "#f2711c",
                "green" => "#21ba45",
                "red" => "#db2828",
                "blue" => "#2185d0",
                _ => "#FF3C3C3C"
            };

            var brush = new BrushConverter().ConvertFrom(color) as SolidColorBrush;

            return brush!;
        }
    }
}
