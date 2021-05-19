using Hydroneer.Contracts.Models.WinformModels;
using HydroneerStager.Contracts.Models.Api.Bridgepour;
using System.Collections.Generic;

namespace Hydroneer.Contracts.Extensions
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
