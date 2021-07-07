﻿using Hydroneer.Contracts.Models.WinformModels;
using Hydroneer.Contracts.WinFormsServices;
using HydroneerStager.WinForms.Extensions;
using HydroneerStager.WinForms.ViewModels;
using ReactiveUI;
using System.Drawing;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Forms;

namespace HydroneerStager.WinForms.Controls
{
    public partial class ModSegmentView : UserControl, IViewFor<ModSegmentViewModel>
    {
        public ModSegmentView(BridgepourModModel bridgepourModModel, IBridgepourService bridgepourService)
        {
            InitializeComponent();

            ViewModel = new ModSegmentViewModel(bridgepourModModel, bridgepourService);

            SetFonts();

            modName.Parent = kryptonPanel1;
            author.Parent = kryptonPanel1;
            description.Parent = kryptonPanel1;

            this.WhenActivated(d => {

                d(this.OneWayBind(ViewModel, vm => vm.CanDownload, v => v.downloadMod.Enabled));
                d(this.OneWayBind(ViewModel, vm => vm.BridgepourModModel.Author, v => v.author.Text));
                d(this.OneWayBind(ViewModel, vm => vm.BridgepourModModel.Description, v => v.description.Text));
                d(this.OneWayBind(ViewModel, vm => vm.BridgepourModModel.Name, v => v.modName.Text));

                d(this.downloadMod.Events()
                    .Click
                    .Select(ea => Unit.Default)
                    .InvokeCommand(ViewModel.DownloadModCommand));
            });
        }

        private void SetFonts()
        {
            modName.Font = new Font(Utilities.FontCollection.Families[Utilities.Fonts.Almenda.ToInt()], modName.Font.Size);
            author.Font = new Font(Utilities.FontCollection.Families[Utilities.Fonts.Almenda.ToInt()], modName.Font.Size);
        }

        public ModSegmentViewModel ViewModel { get; set; }

        object IViewFor.ViewModel { get => ViewModel; set => ViewModel = (ModSegmentViewModel)value; }
    }
}