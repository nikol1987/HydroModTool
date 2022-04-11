using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Autofac;

namespace HydroModTools.Client.Abstractions
{
    public abstract class ClientBase<TMainView> : IClient
    {
        protected TMainView? MainForm { get; set; }

        public event Action? ShutdownApp;
        
        protected Thread? ClientThread;

        protected void Shutdown()
        {
            ShutdownApp?.Invoke();
        }
        
        public abstract Task ToggleSplash(bool show);

        public abstract Task RunClient();
        
        public abstract void RegisterClientTypes(Action<IEnumerable<Type>> configure);
        
        public abstract void ConfigureServices(IContainer services);
    }
}