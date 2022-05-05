using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HydroModTools
{
    public static class Program
    {
        public static void Main(params string[] args)
        {
            var app = new HydroModTools();
            
            if (args.Contains("--legacy"))
            {
                app.UseWinForms();
            }
            else
            {
                app.UseWpf();
            }

            app.PrepareApplication();

            app.RunApplication().Wait();
        }
    }
}
