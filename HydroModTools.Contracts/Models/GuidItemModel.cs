using System;

namespace HydroModTools.Contracts.Models
{
    public sealed class GuidItemModel
    {
        public GuidItemModel(Guid id, string name, Guid moddedGuid, Guid originalGuid)
        {
            Id = id;
            Name = name;
            ModdedGuid = moddedGuid;
            OriginalGuid = originalGuid;
        }

        public Guid Id { get; }

        public string Name { get; }

        public Guid ModdedGuid { get; }

        public Guid OriginalGuid { get; }
    }
}
