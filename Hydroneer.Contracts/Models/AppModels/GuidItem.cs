using Newtonsoft.Json;
using System;

namespace HydroneerStager.Contracts.Models.AppModels
{
    public class GuidItem
    {
        public GuidItem() { }

        public GuidItem(Guid id, string name, Guid guiModdedGuid, Guid guiOriginalGuid)
        {
            Id = id;
            Name = name;
            GuidModdedGuid = guiModdedGuid;
            GuidOriginalGuid = guiOriginalGuid;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ModdedGuid { get; set; }

        public string OriginalGuid { get; set; }

        [JsonIgnore]
        public virtual Guid GuidModdedGuid
        {
            get => new Guid(ModdedGuid);
            set => ModdedGuid = value.ToString("N");
        }

        [JsonIgnore]
        public virtual Guid GuidOriginalGuid
        {
            get => new Guid(OriginalGuid);
            set => OriginalGuid = value.ToString("N");
        }
    }
}
