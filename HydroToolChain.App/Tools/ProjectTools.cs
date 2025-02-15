﻿using HydroToolChain.App.Configuration.Data;
using HydroToolChain.App.Models;
using Microsoft.Extensions.Options;

namespace HydroToolChain.App.Tools;

public class ProjectTools
{
    private readonly IOptions<ServiceCollectionExtensions.ToolsServiceOptions> _options;

    public ProjectTools(
        IOptions<ServiceCollectionExtensions.ToolsServiceOptions> options
    )
    {
        _options = options;
    }
    
    internal Task CopyFiles(ProjectData project)
    {
        return Task.Run(() =>
        {
            var outFile = Utilities.GetOutFile(project);

            if (!File.Exists(outFile))
            {
                return;
            }

            var gamePak = Path.Combine(Constants.PaksFolder, Path.GetFileName(outFile));

            try
            {
                if (File.Exists(gamePak))
                {
                    File.Delete(gamePak);
                }
                
                File.Copy(outFile, gamePak);
            }
            catch (IOException)
            {
                _options.Value.ShowMessage(@"Check if the game is open or try to delete manually.", MessageType.Warning);
            }
        });
    }
}