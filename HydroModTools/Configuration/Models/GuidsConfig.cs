using System;
using System.Collections.Generic;

namespace HydroModTools.Configuration.Models
{
    internal sealed class GuidsConfig
    {
        public List<GuidConfigItem> Guids { get; set; } = new List<GuidConfigItem>();
    }

    internal sealed class GuidConfigItem
    {
        public Guid Id { get; set; } = Guid.Empty;

        public string Name { get; set; } = "";

        public string ModdedGuid { get; set; } = "";

        public string OriginalGuid { get; set; } = "";
    }
}
