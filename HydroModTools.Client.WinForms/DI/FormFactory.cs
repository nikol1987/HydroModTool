using System;
using System.Windows.Forms;

namespace HydroModTools.Client.WinForms.DI
{
    internal static class FormFactory
    {
        private static IFormFactory _factory;

        public static void Use(IFormFactory factory)
        {
            if (_factory != null)
            {
                throw new InvalidOperationException(@"Form factory has been already set up.");
            }

            _factory = factory;
        }

        private static Form Create(Type formType)
        {
            if (_factory == null)
            {
                throw new InvalidOperationException(@"Form factory has not been set up. Call the 'Use' method to inject an IFormFactory instance.");
            }

            return _factory.CreateForm(formType);
        }

        public static T Create<T>() where T : Form
        {
            return (T)Create(typeof(T));
        }
    }
}
