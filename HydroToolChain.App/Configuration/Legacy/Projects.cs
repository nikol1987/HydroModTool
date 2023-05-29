namespace HydroToolChain.App.Configuration.Legacy;

internal sealed class Projects
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int ModIndex { get; set; }
    public string Path { get; set; }
    public string OutputPath { get; set; }
    public List<Items> Items { get; set; }
}