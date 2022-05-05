using System.Runtime.Serialization;

namespace HydroModTools.Client.Wpf.Models
{
    [DataContract]
    public class GuidTableRowItem
    {
        [DataMember]
        public GuidWrapper Id { get; set; }
        
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public GuidWrapper RetailId { get; set; }
        
        [DataMember]
        public GuidWrapper ModdedId { get; set; }
    }
}