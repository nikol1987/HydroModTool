using HydroModTools.DataAccess.Contracts.Models;
using HydroModTools.DataAccess.Contracts.Services;
using HydroModTools.WinForms.ApplicationTabs;
using HydroModTools.WinForms.ViewModels;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Forms;

namespace HydroModTools.WinForms.Views.ApplicationTabs
{
    public partial class ModInstallerTabView : UserControl, IViewFor<ModInstallerTabViewModel>
    {
        private readonly IBridgepourService _bridgepourService;

        public ModInstallerTabView(IBridgepourService bridgepourService)
        {
            _bridgepourService = bridgepourService;

            ViewModel = new ModInstallerTabViewModel(bridgepourService);

            InitializeComponent();

            this.WhenActivated(d => {
                d(ViewModel
                    .WhenAnyValue(vm => vm.LockedRefresh)
                    .Subscribe(lockedRefresh =>
                    {
                        if (lockedRefresh.Locked)
                        {
                            refreshMods.Text = $"Refresh ({10 - lockedRefresh.Seconds})";
                        }
                        else
                        {
                            refreshMods.Text = "Refresh";
                        }
                    }));

                d(ViewModel
                    .WhenAnyValue(vm => vm.ModList)
                    .Subscribe(modList =>
                    {
                        CreateModItems(modList);
                    }));

                d(ViewModel
                    .WhenAnyValue(vm => vm.LoadedMods)
                    .Subscribe(modFiles =>
                    {
                        CreateModItems(ViewModel.ModList);
                    }));

                d(menuStrip1.Events()
                    .ItemClicked
                    .Select(e => e.ClickedItem.Name)
                    .InvokeCommand(ViewModel, vm => vm.ExecuteStripMenuCommand));
            });
        }

        private void CreateModItems(IReadOnlyCollection<BridgepourModModel> modList)
        {
            modsContainer.Controls.Clear();

            foreach (var mod in modList)
            {
                var loaded = ViewModel.LoadedMods.Any(modFile => Path.GetFileName(mod.Url) == Path.GetFileName(modFile));

                modsContainer.Controls.Add(new ModSegmentView(mod, ViewModel, _bridgepourService, loaded));
            }
        }

        public ModInstallerTabViewModel ViewModel { get; set; }

        object IViewFor.ViewModel { get => ViewModel; set => ViewModel = (ModInstallerTabViewModel)value; }
    }
}
