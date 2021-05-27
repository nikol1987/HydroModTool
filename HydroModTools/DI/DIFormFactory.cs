using System;
using System.Windows.Forms;

namespace HydroModTools.DI
{
    internal sealed class DIFormFactory : IFormFactory
    {
        private readonly IServiceProvider services;

        public DIFormFactory(IServiceProvider services)
        {
            this.services = services;
        }

        public Form CreateForm(Type formType)
        {
            var form = (Form)services.GetService(formType);

            return form;
        }
    }
}