using Hydroneer.Contracts.WinFormsServices;
using HydroneerStager.Contracts.Models.WinformModels;
using HydroneerStager.WinForms.Data;
using HydroneerStager.WinForms.Extensions;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Forms;

namespace HydroneerStager.WinForms.ViewModels
{
    public sealed class GuidsViewModel : ReactiveObject
    {
        private readonly ApplicationStore _applicationStore;
        private readonly IGuidsService _guidsService;

        public GuidsViewModel(ApplicationStore applicationStore, IGuidsService guidsService)
        {
            _applicationStore = applicationStore;
            _guidsService = guidsService;

            _applicationStore
                .WhenAnyValue(appStore => appStore.AppState)
                .Subscribe(newState =>
                {
                    SetGuids();
                });


            ExecuteStripMenuCommand = ReactiveCommand.Create<string>(ExecuteStripMenu);
            SelectGuidCommand = ReactiveCommand.Create<Guid>(SelectGuid);
        }

        private void SetGuids()
        {
            var dt = new DataTable();

            var tablePrimaryKey = new DataColumn("id", typeof(Guid))
            {
                Caption = "ID"
            };

            dt.Columns.AddRange(new[] {
                tablePrimaryKey,
                new DataColumn("name", typeof(string)){
                    Caption = "Name"
                },
                new DataColumn("originalid", typeof(Guid)){
                    Caption = "Retail ID"
                },
                new DataColumn("moddedid", typeof(Guid)){
                    Caption = "Modded ID"
                }
            });
            dt.PrimaryKey = new[]
            {
              tablePrimaryKey
            };

            dt.Rows.Clear();

            var items = GuidItemsDataRows(dt, _applicationStore.AppState.Guids);

            foreach (var item in items)
            {
                dt.Rows.Add(item);
            }

            Guids = dt;
        }

        private IReadOnlyCollection<DataRow> GuidItemsDataRows(DataTable dt, IReadOnlyCollection<GuidModel> items)
        {
            var result = new List<DataRow>();

            foreach (var item in items)
            {
                var row = dt.NewRow();
                row["id"] = item.Id;
                row["moddedid"] = item.ModdedGuid;
                row["originalid"] = item.OriginalGuid;
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
                    row["id"] = Guid.NewGuid();

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