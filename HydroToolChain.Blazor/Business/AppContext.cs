﻿using HydroToolChain.App.Configuration.Data;

namespace HydroToolChain.Blazor.Business;

public class AppContext
{
    public event Action<bool> OnAppLoaded = loaded => {};

    public event Action OnAppStateChanged = () => {};

    public event Action<ProjectData> OnProjectChanged = project => {};
    
    public void SetLoaded(bool loaded)
    {
        if (loaded)
        {
            OnAppLoaded.Invoke(loaded);
        }
    }

    public void ProjectChanged(ProjectData projectId)
    {
        OnProjectChanged?.Invoke(projectId);
    }
    
    public void StateChanged()
    {
        OnAppStateChanged?.Invoke();
    }
}