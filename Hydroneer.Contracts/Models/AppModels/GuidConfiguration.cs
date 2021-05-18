using System.Collections.Generic;
using System.Linq;

namespace HydroneerStager.Contracts.Models.AppModels
{
    public class GuidConfiguration
    {
        public GuidConfiguration() { }

        public GuidConfiguration(IReadOnlyCollection<GuidItem> guids)
        {
            Guids = guids.ToList();
        }

        public List<GuidItem> Guids { get; set; } = new List<GuidItem>();
    }
}