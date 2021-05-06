using HydroneerStager.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace HydroneerStager.Pages
{
    public partial class GuidsPage : UserControl
    {
        private DataTable DataSource;   

        public GuidsPage()
        {
            InitializeComponent();

            DataSource = new DataTable();

            var tablePrimaryKey = new DataColumn("id", typeof(Guid))
            {
                Caption = "ID"
            };

            DataSource.Columns.AddRange(new[] {
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
            DataSource.PrimaryKey = new[]
            {
              tablePrimaryKey
            };

            DataSource.Rows.Clear();
            guidsGrid.DataSource = DataSource;
            guidsGrid.ClearSelection();
            for (int i = 0; i < guidsGrid.Columns.Count; i++)
            {
                var column = guidsGrid.Columns[i];
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                column.HeaderText = DataSource.Columns[column.Name].Caption;
            }
            
           menuStrip1.Items["addGuid"].Click += (object sender, System.EventArgs e) => {
               var row = DataSource.NewRow();
               row["id"] = Guid.NewGuid();

               DataSource.Rows.Add(row);
           };

            menuStrip1.Items["saveGuids"].Click += (object sender, System.EventArgs e) => {
                var guids = DataSourceToGuidItems(DataSource);
                Store.Instance.SaveGuids(guids);
            };

            var items = GuidItemsDataRows(Store.Instance.Guids);

            foreach (var item in items)
            {
                DataSource.Rows.Add(item);
            }
        }

        private void guidsGrid_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DataSource.Rows.Remove(DataSource.Rows.Find(((DataRow)e.Row.DataBoundItem).Field<Guid>("id")));
        }

        private IReadOnlyCollection<GuidItem> DataSourceToGuidItems(DataTable dt)
        {
            var result = new List<GuidItem>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var row = dt.Rows[0];

                result.Add(new GuidItem()
                {
                    Id = (Guid)row["id"],
                    ModdedGuid = (Guid)row["moddedid"],
                    OriginalGuid = (Guid)row["originalid"],
                    Name = (string)row["name"]
                });
            }

            return result;
        }

        private IReadOnlyCollection<DataRow> GuidItemsDataRows(IReadOnlyCollection<GuidItem> items)
        {
            var result = new List<DataRow>();

            foreach (var item in items) 
            {
                var row = DataSource.NewRow();
                row["id"] = item.Id;
                row["moddedid"] = item.ModdedGuid;
                row["originalid"] = item.OriginalGuid;
                row["name"] = item.Name;

                result.Add(row);
            }

            return result;
        }
    }
}
