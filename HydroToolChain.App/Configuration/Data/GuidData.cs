namespace HydroToolChain.App.Configuration.Data;

public class GuidData
{
    public GuidData()
    {
        Id = Guid.NewGuid();
    }
    
    public Guid Id { get; set; }

    public string Name { get; set; } = "";
    
    public Guid RetailGuid { get; set; } = Guid.Empty;
    
    public Guid ModdedGuid { get; set; } = Guid.Empty;
}