namespace HydroModTools.Client.Wpf.Structs
{
    internal struct ListItem<TKey, TValue>
    {
        public TKey Key { get; }
        public TValue Value { get; }

        public ListItem(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }

        public void Deconstruct(out TKey key, out TValue value)
        {
            key = Key;
            value = Value;
        }

        public override string ToString()
        {
            if (Value == null)
            {
                return "[null]";
            }

            return Value.ToString() ?? "[null]";
        }
    }
}
