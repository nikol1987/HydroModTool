using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace HydroneerStager
{
    internal abstract class AppStoreBase : INotifyPropertyChanged
    {
        protected AppStoreBase() { }

        protected static AppStoreBase _instance;

        protected AppConfiguration AppConfiguration;

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

        public void Save()
        {
            Configuration.Save(AppConfiguration);
        }

        internal abstract Task InitAsync();
    }
}
