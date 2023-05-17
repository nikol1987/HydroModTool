// ReSharper disable StringLiteralTypo

using System.Diagnostics;
using HydroToolChain.App;
using HydroToolChain.App.Configuration;
using HydroToolChain.App.Configuration.Data;
using HydroToolChain.App.Tools;
using HydroToolChain.Client.Business.Abstracts;
using HydroToolChain.Client.Components.Dialogs;
using MatBlazor;

namespace HydroToolChain.Client.Business;

internal class AppFacade : IAppFacade, IDisposable
{
    private readonly IWritableOptions<AppData> _appDataOptions;
    private readonly IMatToaster _matToaster;
    private readonly IMatDialogService _matDialogService;
    private readonly IToolsService _toolsService;

    private readonly AppContext _context;

    public AppFacade(
        IWritableOptions<AppData> appDataOptions,
        IMatToaster matToaster,
        IMatDialogService matDialogService,
        IToolsService toolsService,
        AppContext context)
    {
        _appDataOptions = appDataOptions;
        _matToaster = matToaster;
        _matDialogService = matDialogService;
        _toolsService = toolsService;
        _context = context;
        
        _appDataOptions.OnStateUpdated += OnAppStateUpdated;
        _appDataOptions.Update(options =>
        {
            var existingCurrentProject = options.CurrentProject;
            if (!options.Projects.Exists(p => p.Id == existingCurrentProject))
                options.CurrentProject = options.Projects
                    .FirstOrDefault()?.Id ?? Guid.Empty;
            
            var existingCurrentGuid = options.CurrentGuid;
            if (!options.Guids.Exists(p => p.Id == existingCurrentGuid))
                options.CurrentGuid = options.Guids
                    .FirstOrDefault()?.Id ?? Guid.Empty;
            
            var existingCurrentUid = options.CurrentUid;
            if (!options.Uids.Exists(p => p.Id == existingCurrentUid))
                options.CurrentUid = options.Uids
                    .FirstOrDefault()?.Id ?? Guid.Empty;
        });

    }

    public event Action OnAppStateChanged
    {
        add => _context.OnAppStateChanged += value;
        remove => _context.OnAppStateChanged -= value;
    }

    public event Action<Boolean> OnAppLoaded
    {
        add => _context.OnAppLoaded += value;
        remove => _context.OnAppLoaded -= value;
    }

    public event Action<Guid>? OnProjectChanged
    {
        add => _context.OnProjectChanged += value;
        remove => _context.OnProjectChanged += value;
    }

    public event Action<Guid>? OnUidChanged
    {
        add => _context.OnUidChanged += value;
        remove => _context.OnUidChanged -= value;
    }

    public event Action<Guid>? OnGuidChanged
    {
        add => _context.OnGuidChanged += value;
        remove => _context.OnGuidChanged -= value;
    }

    public void LoadSettings(bool silent = false)
    {
        _context.SetLoaded(true);
        //TODO: throw new NotImplementedException();

        if (!silent)
        {
            ShowToast("NOT IMPLEMENTED YET", MatToastType.Warning);
        }
    }

    public void SaveSettings(bool silent = false)
    {
        //TODO: throw new NotImplementedException();

        if (!silent)
        {
            ShowToast("NOT IMPLEMENTED YET", MatToastType.Warning);
        }
    }

    public void AddProject()
    {
        Task.Run(() =>
        {
            _matDialogService.OpenAsync(typeof(AddProjectDialog), new MatDialogOptions
            {
                SurfaceClass = "add-project-dialog"
            }).ContinueWith(task =>
            {
                var result = (ProjectData?)task.Result;

                if (result == null)
                {
                    return Task.CompletedTask;
                }
            
                return _appDataOptions.Update(options =>
                {
                    options.CurrentProject = result.Id;
                    options.Projects.Add(result);
                });
            });
        });
    }

    public void SetCurrentProject(Guid projectId)
    {
        _appDataOptions.Update(options =>
        {
            options.CurrentProject = projectId;
        }).ContinueWith(_ =>
        {
            _context.ProjectChanged(projectId);
        });
    }

    public void EditProject(Guid projectId)
    {
        var project = _appDataOptions.Value.Projects
            .First(p => p.Id == projectId);
        
        _matDialogService.OpenAsync(typeof(EditProjectDialog), new MatDialogOptions
        {
            SurfaceClass = "edit-project-dialog",
            Attributes = new Dictionary<string, object>
            {
                {"Project", project}
            }
        }).ContinueWith(task =>
        {
            var result = (ProjectData?)task.Result;

            if (result == null)
            {
                return Task.CompletedTask;
            }
            
            return _appDataOptions.Update(options =>
            {
                var projectToUpdate = options.Projects
                    .Find(p => p.Id == projectId)!;

                projectToUpdate.OutputPath = result.OutputPath;
                projectToUpdate.CookedAssetsPath = result.CookedAssetsPath;
                projectToUpdate.ModIndex = result.ModIndex;
                projectToUpdate.Name = result.Name;
            });
        });
    }

    public void DeleteProject(Guid projectId)
    {
        _appDataOptions.Update(appData =>
        {
            appData.Projects = appData.Projects
                .Where(p => p.Id != projectId)
                .ToList();

            if (appData.CurrentProject != projectId) return;
            
            appData.CurrentProject = appData.Projects
                .FirstOrDefault()?.Id ?? Guid.Empty;
            
            _context.ProjectChanged(appData.CurrentProject);
        });
    }
    
    public ProjectData? GetCurrentProject()
    {
        var data = _appDataOptions.Value;

        return data.Projects
            .FirstOrDefault(p => p.Id == data.CurrentProject);
    }

    public ProjectData GetProjectById(Guid projectId)
    {
        return _appDataOptions.Value.Projects
            .First(p => p.Id == projectId);
    }

    public async Task AddAssets()
    {
        var currentProject = GetCurrentProject()!;

        var selectedFiles = await ChooseFilesHelper("Select assets", currentProject.CookedAssetsPath);

        await _appDataOptions.Update(options =>
        {
            var projectToUpdate = options.Projects
                .First(p => p.Id == options.CurrentProject);
            
            projectToUpdate.Items
                .AddRange(selectedFiles
                    .Select(path =>
                    {
                        var file = new FileInfo(path);

                        return new ProjectItemData
                        {
                            Name = file.Name,
                            Path = file.FullName.Replace(currentProject.CookedAssetsPath, "")
                        };
                    }));
        });
    }

    public async Task RemoveAssets(IReadOnlyCollection<Guid> assetsToDelete)
    {
        await _appDataOptions.Update(options =>
        {
            var projectToUpdate = options.Projects
                .First(p => p.Id == options.CurrentProject);
            
            projectToUpdate.Items = 
                projectToUpdate.Items
                    .Where(i => !assetsToDelete.Contains(i.Id))
                    .ToList();
        });
    }

    public async Task Stage()
    {
        var project = GetCurrentProject();

        if (project == null)
        {
            ShowToast("No Selected Project", MatToastType.Warning);
            return;
        }
        
        var data = _appDataOptions.Value;

        await _toolsService.StageAsync(project, data.Guids, data.Uids);
        
        ShowToast($"Mod [{project.Name}] Staged", MatToastType.Info);
    }

    public async Task Package()
    {
        var project = GetCurrentProject();

        if (project == null)
        {
            ShowToast("No Selected Project", MatToastType.Warning);
            return;
        }

        await _toolsService.PackageAsync(project);
        
        
        ShowToast($"Mod [{project.Name}] Packaged", MatToastType.Info);
    }

    public async Task Copy()
    {
        var project = GetCurrentProject();

        if (project == null)
        {
            ShowToast("No Selected Project", MatToastType.Warning);
            return;
        }

        await _toolsService.CopyFiles(project);

        ShowToast($"Mod [{project.Name}] Copied", MatToastType.Info);
    }

    public void LaunchGame()
    {
        Process.Start(new ProcessStartInfo
        {
            UseShellExecute = true,
            FileName = $"steam://rungameid/{Constants.GameId}"
        });

        ShowToast("Game Launched", MatToastType.Info);
    }

    public async Task DevExpress()
    {
        await Stage();
        await Package();
        await Copy();
        LaunchGame();
    }

    public void OpenModsFolder()
    {
        try
        {
            var startInfo = new ProcessStartInfo
            {
                Arguments = Constants.PaksFolder,
                FileName = "explorer.exe"
            };

            Process.Start(startInfo);
        }
        catch (Exception)
        {
            ShowToast($"Error opening asset folder [{Constants.PaksFolder}]", MatToastType.Warning);
        }
    }
    
    public void ClearLegacyMods()
    {
        try
        {
            Directory.Delete(Constants.PaksFolder, true);
            Directory.CreateDirectory(Constants.PaksFolder);
        }
        catch (Exception)
        {
            // ignored
        }
    }

    public void OpenAssetsFolder(Guid projectId)
    {
        var path = _appDataOptions.Value.Projects
            .First(p => p.Id == projectId)
            .CookedAssetsPath;

        try
        {
            var startInfo = new ProcessStartInfo
            {
                Arguments = path,
                FileName = "explorer.exe"
            };

            Process.Start(startInfo);
        }
        catch (Exception)
        {
            ShowToast($"Error opening asset folder [{path}]", MatToastType.Warning);
        }
    }

    public void OpenDistFolder(Guid projectId)
    {
        var path = _appDataOptions.Value.Projects
            .First(p => p.Id == projectId)
            .OutputPath;

        try
        {
            var startInfo = new ProcessStartInfo
            {
                Arguments = path,
                FileName = "explorer.exe"
            };

            Process.Start(startInfo);
        }
        catch (Exception)
        {
            ShowToast($"Error opening dist folder [{path}]", MatToastType.Warning);
        }
    }
    
    public void AddUid()
    {
        _appDataOptions.Update(options =>
        {
            var newUid = new UidData
            {
                Name = $"Uid #{options.Uids.Count}"
            };

            options.CurrentGuid = newUid.Id;
            options.Uids.Add(newUid);
            
            _context.GuidChanged(options.CurrentGuid );
        });
    }

    public void RemoveUid()
    {
        _appDataOptions.Update(options =>
        {
            options.Uids = options.Uids
                .Where(g => g.Id != options.CurrentUid)
                .ToList();

            options.CurrentUid = options.Uids
                .FirstOrDefault()?.Id ?? Guid.Empty;
            
            _context.GuidChanged(options.CurrentGuid );
        });
    }

    public void SetCurrentUid(Guid uidId)
    {
        _appDataOptions.Update(options =>
        {
            options.CurrentUid = uidId;
        }).ContinueWith(_ =>
        {
            _context.UidChanged(uidId);
        });
    }
    
    public UidData? GetCurrentUid()
    {
        var data = _appDataOptions.Value;

        return data.Uids
            .FirstOrDefault(p => p.Id == data.CurrentUid);
    }
    
    public void AddGuid()
    {
        _appDataOptions.Update(options =>
        {
            var newGuid = new GuidData
            {
                Name = $"Guid #{options.Guids.Count}"
            };

            options.CurrentGuid = newGuid.Id;
            options.Guids.Add(newGuid);
            
            _context.GuidChanged(options.CurrentGuid );
        });
    }

    public void RemoveGuid()
    {
        _appDataOptions.Update(options =>
        {
            options.Guids = options.Guids
                .Where(g => g.Id != options.CurrentGuid)
                .ToList();

            options.CurrentGuid = options.Guids
                .FirstOrDefault()?.Id ?? Guid.Empty;
            
            _context.GuidChanged(options.CurrentGuid );

        });
    }

    public void SetCurrentGuid(Guid guidId)
    {
        _appDataOptions.Update(options =>
        {
            options.CurrentGuid = guidId;
        }).ContinueWith(_ =>
        {
            _context.GuidChanged(guidId);
        });
    }
    
    public GuidData? GetCurrentGuid()
    {
        var data = _appDataOptions.Value;

        return data.Guids
            .FirstOrDefault(p => p.Id == data.CurrentGuid);
    }


    public void UpdateItem(UidData item)
    {
        _appDataOptions.Update(options =>
        {
            var guid = options.Uids
                .First(g => g.Id == item.Id);

            guid.Name = item.Name;
            guid.RetailUid = item.RetailUid;
            guid.ModdedUid = item.ModdedUid;
        });
    }

    public void UpdateItem(GuidData item)
    {
        _appDataOptions.Update(options =>
        {
            var guid = options.Guids
                .First(g => g.Id == item.Id);

            guid.Name = item.Name;
            guid.RetailGuid = item.RetailGuid;
            guid.ModdedGuid = item.ModdedGuid;
        });
    }

    private void ShowToast(string message, MatToastType type)
    {
        _matToaster.Add(message, type);
    }

    public void Dispose()
    {
        _appDataOptions.OnStateUpdated -= OnAppStateUpdated;
    }

    private void OnAppStateUpdated()
    {
        _context.StateChanged();
    }

    private async Task<IReadOnlyCollection<string>> ChooseFilesHelper(string title, string rootPath)
    {
        var result = new List<string>();
        var thread = new Thread(obj =>
        {

            var mbresult = MessageBox.Show("Do you want to select a file? ('No' to select folder)", "Select Files?",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (mbresult == DialogResult.Cancel)
            {
                return;
            }

            var files = (List<string>)obj!;

            if (mbresult == DialogResult.Yes)
            {
                using var dialog = new OpenFileDialog();
                dialog.Title = title;
                dialog.Multiselect = true;
                dialog.Filter = "UE Assets|*.uasset;*.uexp;*.ubulk;*.ini;*.bin;*.umap;*.uplugin;*.uproject";
                dialog.InitialDirectory = rootPath;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    files.AddRange(dialog.FileNames);
                }

                return;
            }

            using (var dialog = new FolderBrowserDialog())
            {
                dialog.UseDescriptionForTitle = true;
                dialog.Description = title;
                dialog.SelectedPath = rootPath;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    files.AddRange(Directory.GetFiles(dialog.SelectedPath, "*.uasset", SearchOption.AllDirectories)
                        .ToList());
                    files.AddRange(Directory.GetFiles(dialog.SelectedPath, "*.uexp", SearchOption.AllDirectories)
                        .ToList());
                    files.AddRange(Directory.GetFiles(dialog.SelectedPath, "*.ubulk", SearchOption.AllDirectories)
                        .ToList());
                    files.AddRange(Directory.GetFiles(dialog.SelectedPath, "*.ini", SearchOption.AllDirectories)
                        .ToList());
                    files.AddRange(Directory.GetFiles(dialog.SelectedPath, "*.bin", SearchOption.AllDirectories)
                        .ToList());
                    files.AddRange(Directory.GetFiles(dialog.SelectedPath, "*.umap", SearchOption.AllDirectories)
                        .ToList());
                    files.AddRange(Directory.GetFiles(dialog.SelectedPath, "*.uplugin", SearchOption.AllDirectories)
                        .ToList());
                    files.AddRange(Directory.GetFiles(dialog.SelectedPath, "*.uproject", SearchOption.AllDirectories)
                        .ToList());
                }
            }
        });

        thread.SetApartmentState(ApartmentState.STA);
        thread.Start(result);

        while (thread.IsAlive)
        {
            Thread.Sleep(100);
        }

        return await Task.FromResult(result);
    }
}