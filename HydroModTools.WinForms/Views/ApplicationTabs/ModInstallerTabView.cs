using HydroModTools.DataAccess.Contracts.Models;
using HydroModTools.DataAccess.Contracts.Services;
using HydroModTools.WinForms.Controls;
using HydroModTools.WinForms.ViewModels;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Forms;

namespace HydroModTools.WinForms.Views.ApplicationTabs
{
    public partial class ModInstallerTabView : UserControl, IViewFor<ModInstallerTabViewModel>
    {
        private readonly IBridgepourService _bridgepourService;

        public ModInstallerTabView(ModInstallerTabViewModel modInstallerTabViewModel, IBridgepourService bridgepourService)
        {
            _bridgepourService = bridgepourService;

            InitializeComponent();

            ViewModel = modInstallerTabViewModel;

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
                modsContainer.Controls.Add(new ModSegmentView(mod, _bridgepourService));
            }
        }

        public ModInstallerTabViewModel ViewModel { get; set; }

        object IViewFor.ViewModel { get => ViewModel; set => ViewModel = (ModInstallerTabViewModel)value; }
    }
}
