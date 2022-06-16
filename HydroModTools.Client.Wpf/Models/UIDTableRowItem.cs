using System.Runtime.Serialization;

namespace HydroModTools.Client.Wpf.Models
{
    [DataContract]
    public class UIDTableRowItem
    {
        [DataMember]
        public GuidWrapper Id { get; set; }
        
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string RetailId { get; set; }
        
        [DataMember]
        public string ModdedId { get; set; }
    }
}