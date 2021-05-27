using HydroModTools.Contracts.Services;
using HydroModTools.WinForms.Data;
using HydroModTools.WinForms.Extensions;
using HydroModTools.WinForms.Structs;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Forms;

namespace HydroModTools.WinForms.ViewModels
{
    public sealed class GuidsViewModel : ReactiveObject
    {
        private readonly IGuidsService _guidsService;

        public GuidsViewModel(IGuidsService guidsService)
        {
            _guidsService = guidsService;

            ExecuteStripMenuCommand = ReactiveCommand.Create<string>(ExecuteStripMenu);
            SelectGuidCommand = ReactiveCommand.Create<Guid>(SelectGuid);

            SetGuids();
        }

        private void SetGuids()
        {
            var dt = new DataTable();

            var tablePrimaryKey = new DataColumn("id", typeof(GuidWrapper))
            {
                Caption = "ID",
                ReadOnly = true,
                Unique = true
            };

            dt.Columns.AddRange(new[] {
                tablePrimaryKey,
                new DataColumn("name", typeof(string)){
                    Caption = "Name"
                },
                new DataColumn("originalid", typeof(GuidWrapper)){
                    Caption = "Retail ID"
                },
                new DataColumn("moddedid", typeof(GuidWrapper)){
                    Caption = "Modded ID"
                }
            });
            dt.PrimaryKey = new[]
            {
              tablePrimaryKey
            };

            dt.Rows.Clear();

            var items = GuidItemsDataRows(dt, ApplicationStore.Store.Guids);

            foreach (var item in items)
            {
                dt.Rows.Add(item);
            }

            Guids = dt;
        }

        private IReadOnlyCollection<DataRow> GuidItemsDataRows(DataTable dt, IReadOnlyCollection<GuidItemStore> items)
        {
            var result = new List<DataRow>();

            foreach (var item in items)
            {
                var row = dt.NewRow();
                row["id"] = new GuidWrapper(item.Id);
                row["moddedid"] = new GuidWrapper(item.ModdedGuid);
                row["originalid"] = new GuidWrapper(item.OriginalGuid);
                row["name"] = item.Name;

                result.Add(row);
            }

            return result;
        }

        private Guid _selectedRow = Guid.Empty;
        internal Guid SelectedRow
        {
            get => _selectedRow;
            set => this.RaiseAndSetIfChanged(ref _selectedRow, value);
        }

        private DataTable _guids = new DataTable();
        internal DataTable Guids
        {
            get => _guids;
            set => this.RaiseAndSetIfChanged(ref _guids, value);
        }

        internal ReactiveCommand<string, Unit> ExecuteStripMenuCommand;
        private async void ExecuteStripMenu(string stripItemName)
        {
            switch (stripItemName)
            {
                case "addGuid":
                    var row = Guids.NewRow();
                    row["id"] = new GuidWrapper(Guid.NewGuid());

                    Guids.Rows.Add(row);
                    this.RaisePropertyChanged("Guids");
                    break;

                case "removeGuid":
                    Guids.Rows.Remove(Guids.Rows.Find(SelectedRow));

                    this.RaisePropertyChanged("Guids");
                    break;

                case "saveGuids":
                    await _guidsService.SaveGuids(Guids.ToGuidItems());

                    MessageBox.Show("GUID list saved.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
            }
        }

        internal ReactiveCommand<Guid, Unit> SelectGuidCommand;
        private void SelectGuid(Guid selectedRow)
        {
            SelectedRow = selectedRow;
        }
    }
}