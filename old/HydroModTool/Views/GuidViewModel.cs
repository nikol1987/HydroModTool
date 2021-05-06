using System.Collections.ObjectModel;

namespace HydroModTool.Views
{
    public class GuidViewModel : ViewModel
    {
        public ObservableCollection<GuidEntry> Data => MainWindow.Config.Guids;

        public Command AddNewRowCommand { get; set; }

        public Command SaveCommand { get; set; }

        public Command<GuidEntry> DeleteRow { get; set; }


        public GuidViewModel()
        {
            AddNewRowCommand = new Command(AddNewRow);
            DeleteRow = new Command<GuidEntry>(RemoveRow);
            SaveCommand = new Command(Save);
        }

        private void Save()
        {
            MainWindow.Config.Save(MainWindow.ConfigPath);
        }

        private void AddNewRow()
        {
            Data.Add(new GuidEntry() { Name = "New Entry" });
        }

        private void RemoveRow(GuidEntry data)
        {
            Data.Remove(data);
        }

    }
}
