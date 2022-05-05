using HydroModTools.WinForms;

namespace HydroModTools.Client.WinForms.Extensions
{
    internal static class EnumExtensions
    {
        public static int ToInt(this Utilities.Fonts font)
        {
            return (int)font;
        }
    }
}
