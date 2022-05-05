using System;
using System.ComponentModel;
using HydroModTools.Client.Wpf.Converters;

namespace HydroModTools.Client.Wpf.Models
{
    [TypeConverter(typeof(GuiWrapperConverter))]
    public struct GuidWrapper
    {
        private Guid guidValue;

        public GuidWrapper(Guid guid)
        {
            guidValue = guid;
        }

        public GuidWrapper(string guid)
        {
            try
            {
                guidValue = new Guid(guid);
            }
            catch
            {
                guidValue = Guid.Empty;
            }
        }


        public static implicit operator GuidWrapper(string guid)
        {
            return new GuidWrapper(guid);
        }
        
        public static implicit operator GuidWrapper(Guid guid)
        {
            return new GuidWrapper(guid);
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
