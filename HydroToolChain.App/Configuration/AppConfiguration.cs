using HydroToolChain.App.Configuration.Models;

namespace HydroToolChain.App.Configuration;

public class AppConfiguration : IAppConfiguration
{
    public void ExportConfig(ConfigPartials partial)
    {
        throw new NotImplementedException();
    }

    public bool TryImport(string filePath)
    {
        return false;
    }
}