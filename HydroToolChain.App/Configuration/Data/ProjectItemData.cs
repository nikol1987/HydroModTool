namespace HydroToolChain.App.Configuration.Data;

public class ProjectItemData
{
    internal ProjectItemData(Guid id)
    {
        Id = id;
    }
    
    public ProjectItemData()
        : this(Guid.NewGuid())
    {}
    
    public Guid Id { get; set; }
    
    public string Name { get; set; } = "######";

    public string Path { get; set; } = "######";
}