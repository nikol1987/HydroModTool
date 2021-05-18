using Hydroneer.Contracts.Models.AppModels;
using HydroneerStager.Contracts.Models.AppModels;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace HydroneerStager
{
    public sealed class Configuration
    {
        private readonly string ConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config");
        private readonly string GuidsConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "guids");

        private ConfigurationModel _configurationModel;

        public async Task<ConfigurationModel> GetConfigurationAsync()
        {

            if (!File.Exists($"{ConfigPath}.json"))
            {
                CreateConfigFile(null, ConfigFile.General);
            }

            if (!File.Exists($"{GuidsConfigPath}.json"))
            {
                CreateConfigFile(null, ConfigFile.Guids);
            }

            if (_configurationModel == null)
            {
                var configurationBuilder = new ConfigurationBuilder();
                configurationBuilder.AddJsonFile($"{ConfigPath}.json", false, true);
                configurationBuilder.AddJsonFile($"{GuidsConfigPath}.json", false, true);

                try
                {
                    var configuration = configurationBuilder.Build();

                    var appConfiguration = new AppConfiguration();
                    configuration.GetSection(nameof(AppConfiguration)).Bind(appConfiguration);

                    var guidConfiguration = new GuidConfiguration();
                    configuration.GetSection(nameof(GuidConfiguration)).Bind(guidConfiguration);

                    _configurationModel = new ConfigurationModel(appConfiguration, guidConfiguration);

                    return _configurationModel;
                }
                catch (Exception)
                {
                    CreateConfigFile(null, ConfigFile.Both);

                    return await GetConfigurationAsync();
                }
            }


            return _configurationModel;
        }

        public async Task Save(ConfigurationModel config, ConfigFile configFile)
        {
            await Task.Run(() =>
            {
                CreateConfigFile(config, configFile);
            });

            _configurationModel = config;
        }

        private void CreateConfigFile(ConfigurationModel configurationModel, ConfigFile configFile)
        {
            if (configurationModel == null)
            {
                if ((configFile == ConfigFile.General || configFile == ConfigFile.Both) && File.Exists($"{ConfigPath}.json"))
                {
                    File.Copy($"{ConfigPath}.json", ConfigPath + "-" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".json");
                }

                if ((configFile == ConfigFile.Guids || configFile == ConfigFile.Both) && File.Exists($"{GuidsConfigPath}.json"))
                {
                    File.Copy($"{GuidsConfigPath}.json", GuidsConfigPath + "-" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".json");
                }
            }


            if (configFile == ConfigFile.General || configFile == ConfigFile.Both)
            {
                string json = JsonSerializer.Serialize(new GeneralConfig(configurationModel?.AppConfiguration), new JsonSerializerOptions()
                {
                    WriteIndented = true
                });
                File.WriteAllText($"{ConfigPath}.json", json);
            }

            if (configFile == ConfigFile.Guids || configFile == ConfigFile.Both)
            {
                string json = JsonSerializer.Serialize(new GuidsConfig(configurationModel?.GuidConfiguration), new JsonSerializerOptions()
                {
                    WriteIndented = true
                });
                File.WriteAllText($"{GuidsConfigPath}.json", json);
            }
        }

        internal async Task Save()
        {
            await Save(_configurationModel, ConfigFile.Both);
        }

        private class GeneralConfig
        {
            public GeneralConfig(AppConfiguration appConfiguration)
            {
                AppConfiguration = appConfiguration ?? new AppConfiguration();
            }

            public AppConfiguration AppConfiguration { get; set; }
        }

        private class GuidsConfig
        {
            public GuidsConfig(GuidConfiguration guidConfiguration)
            {
                GuidConfiguration = guidConfiguration ?? new GuidConfiguration();
            }

            public GuidConfiguration GuidConfiguration { get; set; }
        }


        public enum ConfigFile
        {
            General,
            Guids,
            Both
        }
    }
}
