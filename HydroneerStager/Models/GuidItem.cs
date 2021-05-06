using System;

namespace HydroneerStager.Models
{
    public class GuidItem
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid ModdedGuid { get; set; }

        public Guid OriginalGuid { get; set; }
    }
}
