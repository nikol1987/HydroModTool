using HydroneerStager.DI;
using HydroneerStager.WinForms.Views;
using System;
using System.Windows.Forms;
//using System.Runtime.InteropServices;


namespace HydroneerStager
{
    static class Program
    {

        /*[DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();*/

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            /* AllocConsole();

             foreach (var item in new List<string>(){
                 "C429442EC954004DA7BAFD47EFD2F95A",
                 "F9D436063E13BA4C85F0ADB14D9505CE",
                 "A7355F1BF28D4B45857C8A0D0D608652",
                 "C7EEFE8224A1D94BA161C1B31601FD1B",
                 "22A70C6178FF8946AC966787E360601B",
                 "8D738624717D4344829E0B260558CDCA"
             })
             {
                 var bytes = (new Guid(item)).ToByteArray();
                 var bytesString = bytes.Select(b => b.ToString()).Aggregate((curr, exx) =>
                 {
                     return $"{(exx == null ? "" : $"{exx}, ")}{curr}";
                 });

                 Console.WriteLine($"Bytes [{bytesString}]");
             }

             Console.WriteLine($"===============");


             foreach (var item in new List<string>(){
                 "29FFD2161CC7334AB400D0E5D2371746",
                 "2AA1E6E17512084F97A1CB4573853E04",
                 "C4DC0E16DE7D6D42BE2080DB989A9CAC",
                 "097FEF0E8C5D754D877BCE793B5E032F",
                 "ADC532282F20EF46A28AA4A723C90BF2",
                 "AEB01D6208A47946A53E561D2B21B017"
             })
             {
                 var bytes = (new Guid(item)).ToByteArray();
                 var bytesString = bytes.Select(b => b.ToString()).Aggregate((curr, exx) =>
                 {
                     return $"{(exx == null ? "" : $"{exx}, ")}{curr}";
                 });

                 Console.WriteLine($"Bytes [{bytesString}]");
             }


             Console.ReadLine();*/

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            AutoFac.BuildDependencies();

            Application.ApplicationExit += (object sender, EventArgs e) =>
            {
                Store.GetInstance().Save();
            };

            var appForm = FormFactory.Create<ApplicationView>();
            appForm.Hide();

            var spashForm = FormFactory.Create<SpashView>();
            spashForm.ShowDialog();

            Application.Run(appForm);
        }

        public static void ShowApp()
        {
            var appForm = FormFactory.Create<ApplicationView>();
            appForm.Show();
        }
    }
}
