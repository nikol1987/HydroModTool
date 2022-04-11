using HydroModTools.Client.WinForms.Structs;
using HydroModTools.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace HydroModTools.Client.WinForms.Extensions
{
    internal static class DataTableExtensions
    {
        public static IReadOnlyCollection<GuidItemModel> ToGuidItems(this DataTable dt)
        {
            var result = new List<GuidItemModel>();

            if (dt == null || dt.Rows.Count == 0)
            {
                return result;
            }

            foreach (var row in dt.Rows)
            {
                var dataRow = (DataRow)row;

                if (dataRow["id"] == DBNull.Value ||
                    dataRow["name"] == DBNull.Value ||
                    dataRow["moddedid"] == DBNull.Value ||
                    dataRow["originalid"] == DBNull.Value)
                {
                    continue;
                }

                result.Add(new GuidItemModel(((GuidWrapper)dataRow["id"]).GetGuid(), (string)dataRow["name"], ((GuidWrapper)dataRow["moddedid"]).GetGuid(), ((GuidWrapper)dataRow["originalid"]).GetGuid()));
            }

            return result;
        }
    }
}
