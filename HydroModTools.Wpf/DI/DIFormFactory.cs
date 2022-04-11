using System;
using Autofac;
using HandyControl.Controls;

namespace HydroModTools.Wpf.DI
{
    internal sealed class DiFormFactory : IFormFactory
    {
        private readonly IContainer _services;

        public DiFormFactory(IContainer services)
        {
            _services = services;
        }

        public Window CreateForm(Type formType)
        {
            var form = (Window) _services.Resolve(formType);
            return form;
        }
    }
}