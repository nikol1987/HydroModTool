using HydroneerStager.Contracts.Models.AppModels;
using HydroneerStager.WinForms.Structs;
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

                if (dataRow["id"] == DBNull.Value ||
                    dataRow["name"] == DBNull.Value ||
                    dataRow["moddedid"] == DBNull.Value ||
                    dataRow["originalid"] == DBNull.Value)
                {
                    continue;
                }

                result.Add(new GuidItem(((GuidWrapper)dataRow["id"]).GetGuid(), (string)dataRow["name"], ((GuidWrapper)dataRow["moddedid"]).ToString(), ((GuidWrapper)dataRow["originalid"]).ToString()));
            }

            return result;
        }
    }
}
