using System;

namespace HydroModTool.Views
{
    public class DisplayAttribute : Attribute
    {
        public string Name;
        public string Description;
        public string ToolTip;
        public string GroupName;
        public int Order;
        public bool Enabled = true;
        public bool Visible = true;
        public bool ReadOnly = false;
        public Type EditorType = null;

        public DisplayAttribute()
        { }
    }
}
