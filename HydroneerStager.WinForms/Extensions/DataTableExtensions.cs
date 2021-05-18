using HydroneerStager.Contracts.Models.AppModels;
using System;
using System.Collections.Generic;
using System.Data;

namespace HydroneerStager.WinForms.Extensions
{
    internal static class DataTableExtensions
    {
        public static IReadOnlyCollection<GuidItem> ToGuidItems(this DataTable dt)
        {
            var result = new List<GuidItem>();

            if (dt == null || dt.Rows.Count == 0)
            {
                return result;
            }

            foreach (var row in dt.Rows)
            {
                var dataRow = (DataRow)row;

                result.Add(new GuidItem((Guid)dataRow["id"], (string)dataRow["name"], (Guid)dataRow["moddedid"], (Guid)dataRow["originalid"]));
            }

            return result;
        }
    }
}
