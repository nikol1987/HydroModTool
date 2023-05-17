namespace HydroToolChain.Client.Business;

public class AppContext
{
    public event Action<bool> OnAppLoaded = loaded => {};

    public event Action OnAppStateChanged = () => {};

    public event Action<Guid> OnProjectChanged = guid => {};
    public event Action<Guid> OnUidChanged = guid => {};
    public event Action<Guid> OnGuidChanged = guid => {};
    
    public void SetLoaded(bool loaded)
    {
        if (loaded)
        {
            OnAppLoaded.Invoke(loaded);
        }
    }

    public void ProjectChanged(Guid projectId)
    {
        OnProjectChanged?.Invoke(projectId);
    }
    
    public void StateChanged()
    {
        OnAppStateChanged?.Invoke();
    }

    public void GuidChanged(Guid guidId)
    {
        OnGuidChanged?.Invoke(guidId);
    }
    
    public void UidChanged(Guid guidId)
    {
        OnUidChanged?.Invoke(guidId);
    }
}