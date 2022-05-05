using HydroModTools.Client.WinForms;
using HydroModTools.Client.WinForms.Extensions;
using HydroModTools.Client.WinForms.ViewModels;
using HydroModTools.DataAccess.Contracts.Models;
using HydroModTools.DataAccess.Contracts.Services;
using ReactiveUI;
using System.Drawing;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Forms;

namespace HydroModTools.Winforms.Client.Views.ApplicationTabs
{
    public partial class ModSegmentView : UserControl, IViewFor<ModSegmentViewModel>
    {
        public ModSegmentView(BridgepourModModel bridgepourModModel, ModInstallerTabViewModel modInstallerTabViewModel, IBridgepourService bridgepourService, bool isLoaded)
        {
            InitializeComponent();

            ViewModel = new ModSegmentViewModel(bridgepourModModel, bridgepourService);
            ViewModel.OnModDownload += async () => await modInstallerTabViewModel.RefreshModList(true);
            ViewModel.CanDownload = !isLoaded;

            SetFonts();

            modName.Parent = kryptonPanel1;
            author.Parent = kryptonPanel1;
            description.Parent = kryptonPanel1;

            this.WhenActivated(d => {

                d(this.OneWayBind(ViewModel, vm => vm.CanDownload, v => v.downloadMod.Enabled));
                d(this.OneWayBind(ViewModel, vm => vm.CanRemove, v => v.removeMod.Enabled));
                d(this.OneWayBind(ViewModel, vm => vm.BridgepourModModel.Author, v => v.author.Text));
                d(this.OneWayBind(ViewModel, vm => vm.BridgepourModModel.Description, v => v.description.Text));
                d(this.OneWayBind(ViewModel, vm => vm.BridgepourModModel.Name, v => v.modName.Text));

                d(this.downloadMod.Events()
                    .Click
                    .Select(ea => Unit.Default)
                    .InvokeCommand(ViewModel.DownloadModCommand));

                d(this.removeMod.Events()
                    .Click
                    .Select(ea => Unit.Default)
                    .InvokeCommand(ViewModel.RemoveModCommand));
            });
        }

        private void SetFonts()
        {
            modName.Font = new Font(Utilities.FontCollection.Families[Utilities.Fonts.Almenda.ToInt()], modName.Font.Size);
            author.Font = new Font(Utilities.FontCollection.Families[Utilities.Fonts.Almenda.ToInt()], author.Font.Size);
        }

        public ModSegmentViewModel ViewModel { get; set; }

        object IViewFor.ViewModel { get => ViewModel; set => ViewModel = (ModSegmentViewModel)value; }
    }
}
