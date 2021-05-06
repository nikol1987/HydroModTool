using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HydroneerStager
{
    internal abstract class AppStoreBase : INotifyPropertyChanged
    {
        protected static AppConfiguration AppConfiguration;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnChange([CallerMemberName] string propName = "")
        {
            Save();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        protected void SetValue<T>(ref T field, T value, [CallerMemberName] string propName = "")
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return;

            field = value;
            OnChange(propName);
        }

        internal static void Save()
        {
            Configuration.Save(AppConfiguration);
        }
    }
}
