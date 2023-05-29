using HydroToolChain.App.Configuration.Models;

namespace HydroToolChain.App.Configuration.Data;

public class AppData
{
    public ConfigVersion Version { get; set; } = ConfigVersion._1;
    
    public List<ProjectData> Projects { get; set; } = new();

    public Guid CurrentProject { get; set; } = Guid.Empty;

    public List<GuidData> Guids { get; set; } = new();
    
    public List<UidData> Uids { get; set; } = new();
}