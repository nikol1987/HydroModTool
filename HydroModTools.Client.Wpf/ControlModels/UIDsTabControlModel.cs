using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using System.Windows;
using HydroModTools.Client.Wpf.Models;
using HydroModTools.Contracts.Models;
using HydroModTools.Contracts.Services;
using Notifications.Wpf;
using ReactiveUI;

namespace HydroModTools.Client.Wpf.ControlModels
{
    public class UIDsTabControlModel : ReactiveObject
    {
        private readonly IConfigurationService _configurationService;
        private readonly INotificationManager _notificationManager;

        public UIDsTabControlModel(
            IConfigurationService configurationService,
            INotificationManager notificationManager)
        {
            _configurationService = configurationService;
            _notificationManager = notificationManager;

            SaveUIDsCommand = ReactiveCommand.Create(SaveUIDs);
            RemoveUIDCommand = ReactiveCommand.Create(RemoveUID);
            AddUIDCommand = ReactiveCommand.Create(AddUID);

            LoadUIDs();
        }

        public readonly ObservableCollection<UIDTableRowItem> UIDList = new ObservableCollection<UIDTableRowItem>();

        public UIDTableRowItem? SelectedItem = null;
        
        public readonly ReactiveCommand<Unit, Unit> SaveUIDsCommand;
        private void SaveUIDs()
        {
            Task.Run(async () =>
            {
                await _configurationService.SaveUIDs(UIDList
                    .Select(e => new UIDItemModel(
                        e.Id.GetGuid(),
                        e.Name,
                        e.ModdedId,
                        e.RetailId))
                    .ToList());
                
                _notificationManager.Show(Utilities.CreateInfoNotification("UIDs", $"All UIDs were saved."));
            });
        }
        
        public readonly ReactiveCommand<Unit, Unit> RemoveUIDCommand;
        private void RemoveUID()
        {
            Task.Run(async () =>
            {
                if (SelectedItem == null)
                {
                    return;
                }

                await _configurationService.SaveUIDs(UIDList
                    .Where(e => e.Id.GetGuid() != SelectedItem.Id.GetGuid())
                    .Select(e => new UIDItemModel(
                        e.Id.GetGuid(),
                        e.Name,
                        e.ModdedId,
                        e.RetailId))
                    .ToList());
                
                _notificationManager.Show(Utilities.CreateInfoNotification("UIDs", $"Removed guid '{SelectedItem.Name}'."));
                
                LoadUIDs();
            });
        }
        
        public readonly ReactiveCommand<Unit, Unit> AddUIDCommand;
        private void AddUID()
        {
            Task.Run(async () =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    UIDList.Add(new UIDTableRowItem()
                    {
                        Id = Guid.NewGuid(),
                        Name = "New UID",
                        ModdedId = "",
                        RetailId = ""
                    });
                });
                
                await _configurationService.SaveUIDs(UIDList
                    .Select(e => new UIDItemModel(
                        e.Id.GetGuid(),
                        e.Name,
                        e.ModdedId,
                        e.RetailId))
                    .ToList());
                
                _notificationManager.Show(Utilities.CreateInfoNotification("UIDs", $"Created a new guid."));
            });
        }
        
        private void LoadUIDs()
        {
            _configurationService
                .GetAsync()
                .ContinueWith(configTask =>
                {
                    var config = configTask.Result;

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        UIDList.Clear();
                    
                        foreach (var uid in config.UIDs)  
                        {
                            UIDList.Add(new UIDTableRowItem()
                            {
                                Id = uid.Id,
                                Name = uid.Name,
                                RetailId = uid.OriginalUID,
                                ModdedId = uid.ModdedUID
                            });
                        }
                    });
                });
        }
    }
}