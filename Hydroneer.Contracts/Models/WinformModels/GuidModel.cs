using ReactiveUI;
using System;

namespace HydroneerStager.Contracts.Models.WinformModels
{
    public sealed class GuidModel : ReactiveObject
    {
        public GuidModel(Guid id,
                         string name,
                         Guid moddedGuid,
                         Guid originalGuid)
        {
            Id = id;
            Name = name;
            ModdedGuid = moddedGuid;
            OriginalGuid = originalGuid;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid ModdedGuid { get; set; }

        public Guid OriginalGuid { get; set; }
    }
}