namespace HydroModTools.Common.Models
{
    public sealed class ProgressbarStateModel
    {
        public ProgressbarStateModel(int value, string label)
        {
            Value = value;
            Label = label;
        }

        public int Value { get; }

        public string Label { get; }
    }
}
