namespace HydroModTools.DataAccess.Contracts.Models
{
    public sealed class BridgepourModModel
    {
        public BridgepourModModel(string author,
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

        public string RibbonColor { get; }

        public string RibbonText { get; }

        public string Status { get; }

        public string Url { get; }
    }
}
