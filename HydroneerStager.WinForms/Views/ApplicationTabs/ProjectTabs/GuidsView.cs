using HydroneerStager.WinForms.Structs;
using HydroneerStager.WinForms.ViewModels;
using ReactiveUI;
using System;
using System.ComponentModel;
using System.Data;
using System.Reactive.Linq;
using System.Windows.Forms;

namespace HydroneerStager.WinForms.Views.ApplicationTabs.ProjectTabs
{
    public partial class GuidsView : UserControl, IViewFor<GuidsViewModel>
    {
        public GuidsView(GuidsViewModel guidsViewModel)
        {
            InitializeComponent();

            ViewModel = guidsViewModel;

            this.WhenActivated(d =>
            {
                d(ViewModel
                    .WhenAnyValue(vm => vm.Guids)
                    .Subscribe((guids) =>
                    {
                        guidsDataGrid.DataSource = guids;

                        for (int i = 0; i < guidsDataGrid.Columns.Count; i++)
                        {
                            var column = guidsDataGrid.Columns[i];
                            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                            column.HeaderText = guids.Columns[column.Name].Caption;
                        }
                    }));

                d(menuStrip1.Events()
                    .ItemClicked
                    .Select(e => e.ClickedItem.Name)
                    .InvokeCommand(ViewModel, vm => vm.ExecuteStripMenuCommand));

                d(this.guidsDataGrid.Events()
                    .RowStateChanged
                    .Where(ea => ea.StateChanged == DataGridViewElementStates.Selected)
                    .Where(ea => this.guidsDataGrid.SelectedRows.Count == 1)
                    .Select(ea => ((GuidWrapper)this.guidsDataGrid.SelectedRows[0].Cells["id"].Value).GetGuid())
                    .Where(val => !val.Equals(ViewModel.SelectedRow))
                    .InvokeCommand(ViewModel.SelectGuidCommand));
            });
        }

        public GuidsViewModel ViewModel { get; set; }

        object IViewFor.ViewModel { get => ViewModel; set => ViewModel = (GuidsViewModel)value; }
    }
}
