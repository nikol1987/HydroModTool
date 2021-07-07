using HydroModTools.DataAccess.Contracts.Models;
using HydroModTools.DataAccess.Contracts.Services;
using HydroModTools.WinForms.Extensions;
using HydroModTools.WinForms.ViewModels;
using ReactiveUI;
using System.Drawing;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Forms;

namespace HydroModTools.WinForms.ApplicationTabs
{
    public partial class ModSegmentView : UserControl, IViewFor<ModSegmentViewModel>
    {
        public ModSegmentView(BridgepourModModel bridgepourModModel, IBridgepourService bridgepourService, bool isLoaded)
        {
            InitializeComponent();

            ViewModel = new ModSegmentViewModel(bridgepourModModel, bridgepourService);
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
