namespace HydroToolChain.App.Configuration.Legacy;

internal sealed class AppConfig
{
    public List<Projects> Projects { get; set; }
    public object? DefaultProject { get; set; }
    public int HydroneerVersion { get; set; }
}