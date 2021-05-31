using HydroModTools.Common;
using HydroModTools.Configuration.Models;
using HydroModTools.Contracts.Models;
using HydroModTools.Extensions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace HydroModTools.Configuration
{
    internal sealed class Configuration
    {
        private AppConfig? _appConfiguration;
        private IConfiguration? _configuration;

        public async Task SetupConfigurationAsync()
        {
            if (!File.Exists($"{AppVars.ConfigPath}.json") ||
                !File.Exists($"{AppVars.GuidsConfigPath}.json"))
            {
                await CreateConfigFileAsync(ConfigFile.Both);
            }
        }

        public async Task<AppConfig> GetConfigurationAsync()
        {
            if (_appConfiguration == null)
            {
                throw new Exception("Config not loaded!");
            }

            return await Task.FromResult(_appConfiguration);
        }

        public async Task SaveConfigurationAsync(AppConfigModel? appConfigModel = null)
        {
            if (_configuration == null || _appConfiguration == null)
            {
                throw new Exception("Config not loaded!");
            }

            if (appConfigModel == null)
            {
                await CreateConfigFileAsync(ConfigFile.Both, _appConfiguration.ToModel().ToGeneralConfig(), _appConfiguration.ToModel().ToGuidsConfig());

                return;
            }

            await CreateConfigFileAsync(ConfigFile.Both, appConfigModel.ToGeneralConfig(), appConfigModel.ToGuidsConfig());


            await LoadConfigAsync(_configuration);
        }

        public async Task LoadConfigAsync(IConfiguration configuration)
        {
            _configuration = configuration;

            try
            {
                var generalConfig = new GeneralConfig();
                configuration.GetSection("AppConfiguration").Bind(generalConfig);

                var guidConfiguration = new GuidsConfig();
                configuration.GetSection("GuidConfiguration").Bind(guidConfiguration);

                _appConfiguration = new AppConfig(generalConfig, guidConfiguration);
            }
            catch (Exception)
            {
                await CreateConfigFileAsync(ConfigFile.Both);

                await LoadConfigAsync(configuration);
            }
        }

        private async Task CreateConfigFileAsync(ConfigFile configFile, GeneralConfig generalConfig = null, GuidsConfig guidsConfig = null)
        {
            var jsonOptions = new JsonSerializerOptions()
            {
                WriteIndented = true
            };

            var configTasks = new List<Task>();

            if ((configFile == ConfigFile.General || configFile == ConfigFile.Both) && File.Exists($"{AppVars.ConfigPath}.json"))
            {
                if (generalConfig == null)
                {
                    File.Move($"{AppVars.ConfigPath}.json", AppVars.ConfigPath + "-" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".json");
                }
            }
            string generalJson = JsonSerializer.Serialize(new { GeneralConfig = generalConfig == null ? Defaults.DefaultGeneralConfig : generalConfig }, jsonOptions);
            configTasks.Add(File.WriteAllTextAsync($"{AppVars.ConfigPath}.json", generalJson));

            if ((configFile == ConfigFile.Guids || configFile == ConfigFile.Both) && File.Exists($"{AppVars.GuidsConfigPath}.json"))
            {
                if (guidsConfig == null)
                {
                    File.Move($"{AppVars.GuidsConfigPath}.json", AppVars.GuidsConfigPath + "-" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".json");
                }
            }

            string guidJson = JsonSerializer.Serialize(new { GuidsConfig = guidsConfig == null ? Defaults.DefaultGuidsConfig : guidsConfig }, jsonOptions);
            configTasks.Add(File.WriteAllTextAsync($"{AppVars.GuidsConfigPath}.json", guidJson));

            await Task.WhenAll(configTasks);
        }

        private static class Defaults
        {
            public static GeneralConfig DefaultGeneralConfig = new GeneralConfig();

            public static GuidsConfig DefaultGuidsConfig = new GuidsConfig() { 
                Guids = new List<GuidConfigItem>()
                {
                    // TODO: Ship some default GUIDs
                }
            };
        }
    }
}
