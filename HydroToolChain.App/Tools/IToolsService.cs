using HydroToolChain.App.Configuration.Data;

namespace HydroToolChain.App.Tools;

public interface IToolsService
{
    Task PackageAsync(ProjectData project);

    Task StageAsync(ProjectData project, IReadOnlyCollection<GuidData> guids, IReadOnlyCollection<UidData> uids);

    Task CopyFiles(ProjectData project);
}