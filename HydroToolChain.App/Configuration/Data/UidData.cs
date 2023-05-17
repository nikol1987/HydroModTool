namespace HydroToolChain.App.Configuration.Data;

public class UidData
{
    public UidData()
    {
        Id = Guid.NewGuid();
    }
    
    public Guid Id { get; set; }

    public string Name { get; set; } = "";

    public string RetailUid { get; set; } = "########";
    
    public string ModdedUid { get; set; } = "########";
}