using System;
using System.Windows.Forms;

namespace HydroModTools.WinForms.DI
{
    internal interface IFormFactory
    {
        Form CreateForm(Type formType);
    }
}
