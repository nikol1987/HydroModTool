using System;
using System.Collections.Generic;
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
    internal class GuidsTabControlModel : ReactiveObject
    {
        private readonly IConfigurationService _configurationService;
        private readonly INotificationManager _notificationManager;

        public GuidsTabControlModel(
            IConfigurationService configurationService,
            INotificationManager notificationManager)
        {
            _configurationService = configurationService;
            _notificationManager = notificationManager;

            SaveGuidsCommand = ReactiveCommand.Create(SaveGuids);
            RemoveGuidCommand = ReactiveCommand.Create(RemoveGuid);
            AddGuidCommand = ReactiveCommand.Create(AddGuid);

            LoadGuids();
        }

        public readonly ObservableCollection<GuidTableRowItem> GuidList = new ObservableCollection<GuidTableRowItem>();

        public GuidTableRowItem? SelectedItem = null;
        
        public readonly ReactiveCommand<Unit, Unit> SaveGuidsCommand;
        private void SaveGuids()
        {
            Task.Run(async () =>
            {
                await _configurationService.SaveGuids(GuidList
                    .Select(e => new GuidItemModel(
                        e.Id.GetGuid(),
                        e.Name,
                        e.ModdedId.GetGuid(),
                        e.RetailId.GetGuid()))
                    .ToList());
                
                _notificationManager.Show(Utilities.CreateInfoNotification("Guids", $"All Guids were saved."));
            });
        }
        
        public readonly ReactiveCommand<Unit, Unit> RemoveGuidCommand;
        private void RemoveGuid()
        {
            Task.Run(async () =>
            {
                if (SelectedItem == null)
                {
                    return;
                }

                await _configurationService.SaveGuids(GuidList
                    .Where(e => e.Id.GetGuid() != SelectedItem.Id.GetGuid())
                    .Select(e => new GuidItemModel(
                        e.Id.GetGuid(),
                        e.Name,
                        e.ModdedId.GetGuid(),
                        e.RetailId.GetGuid()))
                    .ToList());
                
                _notificationManager.Show(Utilities.CreateInfoNotification("Guids", $"Removed guid '{SelectedItem.Name}'."));
                
                LoadGuids();
            });
        }
        
        public readonly ReactiveCommand<Unit, Unit> AddGuidCommand;
        private void AddGuid()
        {
            Task.Run(async () =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    GuidList.Add(new GuidTableRowItem()
                    {
                        Id = Guid.NewGuid(),
                        Name = "New Guid",
                        ModdedId = Guid.Empty,
                        RetailId = Guid.Empty
                    });
                });
                
                await _configurationService.SaveGuids(GuidList
                    .Select(e => new GuidItemModel(
                        e.Id.GetGuid(),
                        e.Name,
                        e.ModdedId.GetGuid(),
                        e.RetailId.GetGuid()))
                    .ToList());
                
                _notificationManager.Show(Utilities.CreateInfoNotification("Guids", $"Created a new guid."));
            });
        }
        
        private void LoadGuids()
        {
            _configurationService
                .GetAsync()
                .ContinueWith(configTask =>
                {
                    var config = configTask.Result;

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        GuidList.Clear();
                    
                        foreach (var guid in config.Guids)  
                        {
                            GuidList.Add(new GuidTableRowItem()
                            {
                                Id = guid.Id,
                                Name = guid.Name,
                                RetailId = guid.OriginalGuid,
                                ModdedId = guid.ModdedGuid
                            });
                        }
                    });
                });
        }
    }
}