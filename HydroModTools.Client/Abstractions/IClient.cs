using System;
using System.Threading.Tasks;
using Autofac;

namespace HydroModTools.Client.Abstractions
{
    public interface IClient
    {
        public event Action ShutdownApp;
        
        public Task ToggleSplash(bool show);
        
        public Task RunClient();

        public void RegisterClientTypes(ContainerBuilder services);
        
        public void ConfigureServices(IContainer services);
    }
}