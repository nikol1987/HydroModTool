using System;
using System.Threading;

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
            var app = new HydroModTools();
            
            app.StartApplication();

            app.RunApplication();
        }
    }
}
