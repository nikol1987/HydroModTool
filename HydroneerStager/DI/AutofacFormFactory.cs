using Autofac;
using System;
using System.Windows.Forms;

namespace HydroneerStager.DI
{
    internal sealed class AutofacFormFactory : IFormFactory
    {
        private readonly ILifetimeScope currentScope;

        public AutofacFormFactory(ILifetimeScope currentScope)
        {
            this.currentScope = currentScope;
        }

        public Form CreateForm(Type formType)
        {
            var scope = this.currentScope.BeginLifetimeScope();

            var form = (Form)scope.Resolve(formType);

            form.Disposed += (s, e) =>
            {
                scope.Dispose();
            };

            return form;
        }
    }
}