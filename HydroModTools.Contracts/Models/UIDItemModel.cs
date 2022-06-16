using System;

namespace HydroModTools.Contracts.Models
{
    public class UIDItemModel
    {
        public UIDItemModel(Guid id, string name, string moddedUID, string originalUID)
        {
            Id = id;
            Name = name;
            ModdedUID = moddedUID;
            OriginalUID = originalUID;
        }

        public Guid Id { get; }

        public string Name { get; }

        public string ModdedUID { get; }

        public string OriginalUID { get; }
    }
}