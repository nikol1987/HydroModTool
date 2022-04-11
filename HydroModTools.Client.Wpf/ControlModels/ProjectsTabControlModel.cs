using HydroModTools.Contracts.Enums;
using HydroModTools.Contracts.Models;
using HydroModTools.Contracts.Services;
using ReactiveUI;
using System.Collections.Generic;
using System.Reactive;

namespace HydroModTools.Client.Wpf.ControlModels
{
    internal sealed class ProjectsTabControlModel : ReactiveObject
    {
        private readonly IAppService _appService;
        private readonly IConfigurationService _configurationService;

        public ProjectsTabControlModel(IAppService appService, IConfigurationService configurationService)
        {
            _appService = appService;
            _configurationService = configurationService;

            StartGameCommand = ReactiveCommand.Create(StartGame);
            SetGameVersionCommand = ReactiveCommand.Create<int>(SetGameVersion);

            _configurationService
                .GetAsync()
                .ContinueWith(configTask => {
                    var config = configTask.Result;

                    SelectedGame = config.HydroneerVersion;
                    ProjectList = config.Projects;
                });
        }

        public ReactiveCommand<int, Unit> SetGameVersionCommand;
        private void SetGameVersion(int gameIndex)
        {
            _appService.SetGameVersion((HydroneerVersion)gameIndex);
        }

        public ReactiveCommand<Unit, Unit> StartGameCommand;
        private void StartGame()
        {
            _appService.StartGame();
        }

        private IReadOnlyCollection<ProjectModel> _projectList = new List<ProjectModel>();
        public IReadOnlyCollection<ProjectModel> ProjectList
        {
            get => _projectList;
            set
            {
                this.RaiseAndSetIfChanged(ref _projectList, value);
            }
        }

        private HydroneerVersion _selectedVersion = HydroneerVersion.HydroneerLegacy;
        public HydroneerVersion SelectedGame
        {
            get => _selectedVersion;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedVersion, value);
            }
        }
    }
}
