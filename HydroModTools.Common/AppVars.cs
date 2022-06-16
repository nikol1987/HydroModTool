using System;
using System.IO;

namespace HydroModTools.Common
{
    public static class AppVars
    {
        public static readonly string ConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config");
        public static readonly string GuidsConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "guids");
        public static readonly string UIDsConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "uids");
    }
}
