using System;
using System.Windows.Forms;

namespace HydroModTools.Client.WinForms.DI
{
    internal interface IFormFactory
    {
        Form CreateForm(Type formType);
    }
}
