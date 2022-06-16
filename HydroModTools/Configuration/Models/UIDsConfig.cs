using System;
using System.Collections.Generic;

namespace HydroModTools.Configuration.Models
{
    internal sealed class UIDsConfig
    {
        public List<UIDsConfigItem> UIDs { get; set; } = new List<UIDsConfigItem>();
    }

    internal sealed class UIDsConfigItem
    {
        public Guid Id { get; set; } = Guid.Empty;
        
        public string Name { get; set; } = "";
        
        public string ModdedUID { get; set; } = "";

        public string OriginalUID { get; set; } = "";
    }
}