using HydroToolChain.App.Configuration.Models;

namespace HydroToolChain.App.Configuration;

public interface IAppConfiguration
{
    public void ExportConfig(ConfigPartials partial);

    public bool TryImport(string filePath);
}