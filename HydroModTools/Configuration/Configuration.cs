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

        public void SetupConfiguration()
        {
            if (!File.Exists($"{AppVars.ConfigPath}.json"))
            {
                CreateConfigFileAsync(ConfigFile.General).Wait();
            }


            if (!File.Exists($"{AppVars.GuidsConfigPath}.json"))
            {
                CreateConfigFileAsync(ConfigFile.Guids).Wait();
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


            ((IConfigurationRoot)_configuration).Reload();

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

        private static async Task CreateConfigFileAsync(ConfigFile configFile, GeneralConfig? generalConfig = null, GuidsConfig? guidsConfig = null)
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
            string generalJson = JsonSerializer.Serialize(new { AppConfiguration = generalConfig == null ? Defaults.DefaultGeneralConfig : generalConfig }, jsonOptions);
            configTasks.Add(File.WriteAllTextAsync($"{AppVars.ConfigPath}.json", generalJson));

            if ((configFile == ConfigFile.Guids || configFile == ConfigFile.Both) && File.Exists($"{AppVars.GuidsConfigPath}.json"))
            {
                if (guidsConfig == null)
                {
                    File.Move($"{AppVars.GuidsConfigPath}.json", AppVars.GuidsConfigPath + "-" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".json");
                }
            }

            string guidJson = JsonSerializer.Serialize(new { GuidConfiguration = guidsConfig == null ? Defaults.DefaultGuidsConfig : guidsConfig }, jsonOptions);
            configTasks.Add(File.WriteAllTextAsync($"{AppVars.GuidsConfigPath}.json", guidJson));

            await Task.WhenAll(configTasks);
        }

        private static class Defaults
        {
            public static readonly GeneralConfig DefaultGeneralConfig = new GeneralConfig();

            public static readonly GuidsConfig DefaultGuidsConfig = new GuidsConfig() {
                Guids = new List<GuidConfigItem>()
                {
                    new GuidConfigItem()
                    {
                        Id = Guid.NewGuid(),
                        Name = "2.0 BP_ParentItem -\u003EStatic Mesh",
                        ModdedGuid = "67eeadddd1ef1c4896df32d704eaa325",
                        OriginalGuid = "8d738624717d4344829e0b260558cdca"
                    },
                    new GuidConfigItem()
                    {
                      Id = Guid.NewGuid(),
                      Name = "2.0 BP_WorldStore -\u003EStatic Mesh",
                      ModdedGuid = "df2b4482a4e39f4d904aae745dfdefb3",
                      OriginalGuid = "eaaf41d695ee154790461a770cf4e146"
                    },
                    new GuidConfigItem()
                    {
                      Id = Guid.NewGuid(),
                      Name = "2.0 BP_WorldStore -\u003EStatic Mesh 3",
                      ModdedGuid = "b0a3d24cc6020845a3b63c31c2c710d4",
                      OriginalGuid = "48e4d3a8e9463a4fbd8ce17530366c62"
                    },
                    new GuidConfigItem()
                    {
                      Id = Guid.NewGuid(),
                      Name = "2.0 BP_WorldStore -\u003EAC_StoreFunctions",
                      ModdedGuid = "75f7e0bfd4d870488e55b8de00fe5923",
                      OriginalGuid = "a67d7c65ff06f2448ce1904b495585dd"
                    },
                    new GuidConfigItem()
                    {
                      Id = Guid.NewGuid(),
                      Name = "2.0 BP_WorldStore -\u003E Price Text",
                      ModdedGuid = "7f776e0aec4566439840a51cf4482381",
                      OriginalGuid = "8c3112119dc1a94caf36ab1b5ea7e215"
                    },
                    new GuidConfigItem()
                    {
                      Id = Guid.NewGuid(),
                      Name = "2.0 BP_WorldStore -\u003E Buy",
                      ModdedGuid = "925aaf99e6f5ca43bed0acd2c8205c50",
                      OriginalGuid = "8ad45c8e26a3fc499797460f16f5188c"
                    },
                    new GuidConfigItem()
                    {
                      Id = Guid.NewGuid(),
                      Name = "2.0 BP_WorldStore -\u003E Bucket",
                      ModdedGuid = "d97c755eea6f1c4eb13dd2b83eba1650",
                      OriginalGuid = "58fe34c8f12dc14783bedddb7efe90ab"
                    },
                    new GuidConfigItem()
                    {
                      Id = Guid.NewGuid(),
                      Name = "2.0 BP_WorldStore -\u003E CoinSphere",
                      ModdedGuid = "b1668ad0cc87b141b2363f708aee6ebb",
                      OriginalGuid = "dee387e48f838f4f8f2c6d3e843584c7"
                    },
                    new GuidConfigItem()
                    {
                      Id = Guid.NewGuid(),
                      Name = "2.0 BP_WorldStore -\u003E SecurityBox",
                      ModdedGuid = "37b7ff7832336c49bda9480acfeb668e",
                      OriginalGuid = "f44bf511b8df2945bcaf8dca35699538"
                    },
                    new GuidConfigItem()
                    {
                      Id = Guid.NewGuid(),
                      Name = "2.0 BP_WorldStore -\u003E BuyBox",
                      ModdedGuid = "feb523e40a9ec446bd023b1d13d67a97",
                      OriginalGuid = "01bdb51536891e448fd9a773be0208b9"
                    },
                    new GuidConfigItem()
                    {
                      Id = Guid.NewGuid(),
                      Name = "2.0 BP_ParentBuild -\u003E SM_Plane5x5",
                      ModdedGuid = "4e9e455904caf94db6303fb01a78df06",
                      OriginalGuid = "1cfbe137e1335a489e9c618c62ca3303"
                    },
                    new GuidConfigItem()
                    {
                      Id = Guid.NewGuid(),
                      Name = "2.0 BP_ParentBuild -\u003E Decal",
                      ModdedGuid = "a36e9d2bb869c045af3bd240c4a433bb",
                      OriginalGuid = "752bc7eee19d4a4b9fa735c63a85d21e"
                    },
                    new GuidConfigItem()
                    {
                      Id = Guid.NewGuid(),
                      Name = "2.0 BP_ParentBuild -\u003E GridGuide",
                      ModdedGuid = "f1abe920283e3d4cbc764873444224e4",
                      OriginalGuid = "f9d436063e13ba4c85f0adb14d9505ce"
                    }
                }
            };
        }
    }
}
