namespace HydroToolChain.App.Configuration.Data;

public class ProjectItemData
{
    public ProjectItemData()
    {
        Id = Guid.NewGuid();
    }
    
    public Guid Id { get; set; }
    
    public string Name { get; set; } = "######";

    public string Path { get; set; } = "######";
}