using System;
using System.Windows.Forms;

namespace HydroModTools.DI
{
    public interface IFormFactory
    {
        Form CreateForm(Type formType);
    }
}
