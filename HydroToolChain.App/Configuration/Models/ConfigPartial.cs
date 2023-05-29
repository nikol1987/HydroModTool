using HydroToolChain.App.Configuration.Data;

namespace HydroToolChain.App.Configuration.Models;

public sealed class ConfigPartial
{
    public ConfigPartials PartialType { get; set; }

    public List<GuidData> Guids { get; set; } = new(0);
    
    public List<UidData> Uids { get; set; } = new(0);
}