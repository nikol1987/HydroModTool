using HydroToolChain.App.Configuration.Data;

namespace HydroToolChain.App.Tools;

public class ToolsService : IToolsService
{
    private readonly Stager _stager;
    private readonly Packager _packager;
    private readonly ProjectTools _projectTools;

    public ToolsService(
        Stager stager,
        Packager packager,
        ProjectTools projectTools)
    {
        _stager = stager;
        _packager = packager;
        _projectTools = projectTools;
    }
    
    public Task PackageAsync(ProjectData project)
    {
        return _packager.PackageAsync(project);
    }

    public Task StageAsync(ProjectData project, IReadOnlyCollection<GuidData> guids, IReadOnlyCollection<UidData> uids)
    {
        return _stager.StageAsync(project, guids, uids);
    }

    public Task CopyFiles(ProjectData project)
    {
        return _projectTools.CopyFiles(project);
    }
}