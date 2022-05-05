using HydroModTools.Contracts.Enums;
using HydroModTools.Contracts.Services;
using Markdig;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace HydroModTools.Services
{
    internal sealed class AppService : IAppService
    {
        private readonly IConfigurationService _configurationService;

        public AppService(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }

        public async Task<string> LoadAboutString()
        {
            var markdown = await File.ReadAllTextAsync("Readme.md");

            return markdown;
        }

        public async Task<string> LoadAboutHtml()
        {
            var markdown = await LoadAboutString();

            var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            return Markdig.Markdown.ToHtml(markdown, pipeline);
        }

        public async Task StartGame()
        {
            var selectedGame = (await _configurationService.GetAsync())
                .HydroneerVersion;

            var gameId = selectedGame switch
            {
                HydroneerVersion.HydroneerLegacy => "1106840",
                HydroneerVersion.Hydroneer2 => "1912920",
                _ => throw new InvalidOperationException($"Invalid game selected. [Got: {selectedGame}]")
            };

            Process.Start(new ProcessStartInfo()
            {
                UseShellExecute = true,
                FileName = $"steam://rungameid/{gameId}"
            });
        }

        public async Task SetGameVersion(HydroneerVersion hydroneerVersion)
        {
            await _configurationService.SetGameVersion(hydroneerVersion);
        }
    }
}
