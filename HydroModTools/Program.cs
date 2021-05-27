using System;

namespace HydroModTools
{
    public static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            (new HydroModTools()).StartApplication();
        }
    }
}
