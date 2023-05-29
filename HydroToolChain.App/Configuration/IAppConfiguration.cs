using HydroToolChain.App.Configuration.Models;

namespace HydroToolChain.App.Configuration;

public interface IAppConfiguration
{
    public Task<string?> ExportConfig(ConfigPartials? partial);

    public Task<bool> TryImport(string filePath);
}