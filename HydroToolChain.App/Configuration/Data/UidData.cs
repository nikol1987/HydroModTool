namespace HydroToolChain.App.Configuration.Data;

public class UidData
{
    internal UidData(Guid id)
    {
        Id = id;
    }
    
    public UidData()
        : this(Guid.NewGuid())
    {}
    
    public Guid Id { get; set; }

    public string Name { get; set; } = "";

    public string RetailUid { get; set; } = "########";
    
    public string ModdedUid { get; set; } = "########";
}