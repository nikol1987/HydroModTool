using System;
using System.Windows.Forms;

namespace HydroneerStager.DI
{
    public interface IFormFactory
    {
        Form CreateForm(Type formType);
    }
}
