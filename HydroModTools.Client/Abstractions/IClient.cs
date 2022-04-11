using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;

namespace HydroModTools.Client.Abstractions
{
    public interface IClient
    {
        public event Action ShutdownApp;
        
        public Task ToggleSplash(bool show);
        
        public Task RunClient();

        public void RegisterClientTypes(Action<IEnumerable<Type>> configure);
        
        public void ConfigureServices(IContainer services);
    }
}