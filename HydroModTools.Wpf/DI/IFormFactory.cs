using HandyControl.Controls;
using System;

namespace HydroModTools.Wpf.DI
{
    internal interface IFormFactory
    {
        Window CreateForm(Type formType);
    }
}
