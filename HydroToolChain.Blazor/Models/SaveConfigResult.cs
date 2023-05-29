using HydroToolChain.App.Configuration.Models;

namespace HydroToolChain.Blazor.Models;

public class SaveConfigResult
{
    public SaveConfigResult(
        bool save,
        ConfigPartials? partialType)
    {
        Save = save;
        PartialType = partialType;
    }

    public bool Save { get; } = false;
    public ConfigPartials? PartialType { get; } = null;
}