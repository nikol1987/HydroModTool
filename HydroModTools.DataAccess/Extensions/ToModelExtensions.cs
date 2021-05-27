using HydroModTools.DataAccess.Contracts.Models;
using HydroModTools.DataAccess.Models;
using System.Collections.Generic;

namespace HydroModTools.DataAccess.Extensions
{
    public static class ToModelExtensions
    {
        public static IReadOnlyCollection<BridgepourModModel> ToModel(this IReadOnlyCollection<BridgepourMod> bridgepourMods)
        {
            var result = new List<BridgepourModModel>();

            if (bridgepourMods == null)
            {
                return result;
            }

            foreach (var bridgepourMod in bridgepourMods)
            {
                result.Add(new BridgepourModModel(bridgepourMod.Author, bridgepourMod.Description, bridgepourMod.Name, bridgepourMod.RibbonColor, bridgepourMod.RibbonText, bridgepourMod.Status, bridgepourMod.Url));
            }

            return result;
        }
    }
}
