using Newtonsoft.Json;

namespace HydroneerStager.DataAccess.Models
{
    public sealed class BridgepourMod
    {
        public BridgepourMod(string author,
                             string description,
                             string name,
                             string ribbonColor,
                             string ribbonText,
                             string status,
                             string url)
        {
            Author = author;
            Description = description;
            Name = name;
            RibbonColor = ribbonColor;
            RibbonText = ribbonText;
            Status = status;
            Url = url;
        }

        public string Author { get; }

        public string Description { get; }

        public string Name { get; }

        [JsonProperty("ribbon_color")]
        public string RibbonColor { get; }

        [JsonProperty("ribbon_text")]
        public string RibbonText { get; }

        public string Status { get; }

        public string Url { get; }
    }
}