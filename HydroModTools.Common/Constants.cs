using System;
using System.IO;

namespace HydroModTools.Common
{
    public static class Constants
    {
        private static string _paksFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Mining", "Saved", "Paks");
        public static string PaksFolder {
            get
            {
                if (!Directory.Exists(_paksFolder))
                {
                    Directory.CreateDirectory(_paksFolder);
                }

                return _paksFolder;
            }
        }
    }
}
