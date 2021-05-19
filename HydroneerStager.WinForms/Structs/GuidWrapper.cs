using System;

namespace HydroneerStager.WinForms.Structs
{
    internal struct GuidWrapper
    {
        private Guid guidValue;

        public GuidWrapper(Guid guid)
        {
            guidValue = guid;
        }

        public GuidWrapper(string guid)
        {
            guidValue = new Guid(guid);
        }

        public override string ToString()
        {
            return guidValue.ToString("N").ToUpper();
        }

        public Guid GetGuid()
        {
            return guidValue;
        }
    }
}
