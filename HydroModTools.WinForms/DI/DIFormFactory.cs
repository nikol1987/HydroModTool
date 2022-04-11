using System;
using System.Windows.Forms;
using Autofac;

namespace HydroModTools.WinForms.DI
{
    internal sealed class DiFormFactory : IFormFactory
    {
        private readonly IContainer _services;

        public DiFormFactory(IContainer services)
        {
            _services = services;
        }

        public Form CreateForm(Type formType)
        {
            var form = (Form) _services.Resolve(formType);
            return form;
        }
    }
}