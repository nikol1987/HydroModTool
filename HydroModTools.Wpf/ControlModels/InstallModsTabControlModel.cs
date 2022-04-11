using HydroModTools.DataAccess.Contracts.Services;
using ReactiveUI;

namespace HydroModTools.Wpf.ControlModels
{
    internal sealed class InstallModsTabControlModel : ReactiveObject
    {
        private readonly IBridgepourService _bridgepourService;

        public InstallModsTabControlModel(IBridgepourService bridgepourService)
        {
            _bridgepourService = bridgepourService;
        }
    }
}
