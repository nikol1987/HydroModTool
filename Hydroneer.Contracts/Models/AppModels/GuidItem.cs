using System;

namespace HydroneerStager.Contracts.Models.AppModels
{
    public class GuidItem
    {
        public GuidItem() { }

        public GuidItem(Guid id, string name, string moddedGuid, string originalGuid)
        {
            Id = id;
            Name = name;
            ModdedGuid = moddedGuid;
            OriginalGuid = originalGuid;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ModdedGuid { get; set; }

        public string OriginalGuid { get; set; }
    }
}
