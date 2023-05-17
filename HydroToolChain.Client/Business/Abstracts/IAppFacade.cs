using HydroToolChain.App.Configuration.Data;

namespace HydroToolChain.Client.Business.Abstracts;

public interface IAppFacade
{
    public event Action OnAppStateChanged;
    public event Action<Boolean> OnAppLoaded;
    public event Action<Guid> OnProjectChanged;
    public event Action<Guid> OnUidChanged;
    public event Action<Guid> OnGuidChanged;

    #region AppActions

    void LoadSettings(bool silent = false);
    void SaveSettings(bool silent = false);
    
    #endregion
    
    #region Project Main Actions
    
    void AddProject();

    Task Stage();
    
    Task Package();
    
    Task Copy();

    void LaunchGame();

    Task DevExpress();
    
    void OpenModsFolder();
    
    void ClearLegacyMods();
    
    #endregion

    #region Project Actions

    void SetCurrentProject(Guid projectId);
    
    void EditProject(Guid projectId);
    
    void DeleteProject(Guid projectId);

    void OpenAssetsFolder(Guid projectId);
    
    void OpenDistFolder(Guid projectId);
    
    ProjectData? GetCurrentProject();

    ProjectData GetProjectById(Guid projectId);
    
    Task AddAssets();
    
    Task RemoveAssets(IReadOnlyCollection<Guid> assetsToDelete);
    #endregion

    #region UIDs Actions

    void AddUid();

    void RemoveUid();
    
    void SetCurrentUid(Guid uidId);
    
    UidData? GetCurrentUid();
    
    void UpdateItem(UidData item);
    
    #endregion

    #region GUIDs Actions
    void AddGuid();

    void RemoveGuid();
    
    void SetCurrentGuid(Guid guidId);
    
    GuidData? GetCurrentGuid();
    
    void UpdateItem(GuidData item);

    #endregion
}