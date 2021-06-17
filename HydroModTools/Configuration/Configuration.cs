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
            public static GeneralConfig DefaultGeneralConfig = new GeneralConfig();

            public static GuidsConfig DefaultGuidsConfig = new GuidsConfig() {
                Guids = new List<GuidConfigItem>()
                {
                    new GuidConfigItem()
                    {
                        Id = Guid.NewGuid(),
                        Name = "BP_ParentItem StaticMesh",
                        ModdedGuid = "aeb01d6208a47946a53e561d2b21b017",
                        OriginalGuid = "8d738624717d4344829e0b260558cdca"
                    },
                    new GuidConfigItem()
                    {
                        Id = Guid.NewGuid(),
                        Name = "BP_ParentPower LegsDown",
                        ModdedGuid = "50134f0a7d55204eab399a0af0654415",
                        OriginalGuid = "78e26f20a83b04479afbb815a028a5e0"
                    },
                    new GuidConfigItem()
                    {
                        Id = Guid.NewGuid(),
                        Name = "BP_ParentPower LegsUp",
                        ModdedGuid = "4e1dd9065e1d6046822468d7ad914559",
                        OriginalGuid = "1ed2dba1fbc69c46b221bb7bd6fc916a"
                    },
                    new GuidConfigItem()
                    {
                        Id = Guid.NewGuid(),
                        Name = "BP_ParentPower LegsLeft",
                        ModdedGuid = "7a8c1bdfbff5ef4295cddf9d608439e6",
                        OriginalGuid = "873478dc327f1d4c8c69269ea9071625"
                    },
                    new GuidConfigItem()
                    {
                        Id = Guid.NewGuid(),
                        Name = "BP_ParentPower LegsRight",
                        ModdedGuid = "899b81e396b185458fa58f3cc5998841",
                        OriginalGuid = "85e32da9ae41a445aff74300e641a555"
                    },
                    new GuidConfigItem()
                    {
                        Id = Guid.NewGuid(),
                        Name = "BP_ParentBuild -\u003E CenterPoint",
                        ModdedGuid = "adc532282f20ef46a28aa4a723c90bf2",
                        OriginalGuid = "22a70c6178ff8946ac966787e360601b"
                    },
                    new GuidConfigItem()
                    {
                        Id = Guid.NewGuid(),
                        Name = "BP_ParentBuild -\u003E CollisionTestBlock",
                        ModdedGuid = "c4dc0e16de7d6d42be2080db989a9cac",
                        OriginalGuid = "a7355f1bf28d4b45857c8a0d0d608652"
                    },
                    new GuidConfigItem()
                    {
                        Id = Guid.NewGuid(),
                        Name = "BP_ParentBuild -\u003E SecondPoint",
                        ModdedGuid = "097fef0e8c5d754d877bce793b5e032f",
                        OriginalGuid = "c7eefe8224a1d94ba161c1b31601fd1b"
                    },
                    new GuidConfigItem()
                    {
                        Id = Guid.NewGuid(),
                        Name = "BP_ParentBuild -\u003E GridGuide",
                        ModdedGuid = "2aa1e6e17512084f97a1cb4573853e04",
                        OriginalGuid = "f9d436063e13ba4c85f0adb14d9505ce"
                    },
                    new GuidConfigItem()
                    {
                        Id = Guid.NewGuid(),
                        Name = "BP_ParentWaterItem -\u003E Box1",
                        ModdedGuid = "951f1d05f0405245a24b5fc8b3b70cca",
                        OriginalGuid = "76911c089dc35b40bc3ffef81cd35c97"
                    },
                    new GuidConfigItem()
                    {
                        Id = Guid.NewGuid(),
                        Name = "BP_ParentWaterItem -\u003E Box2",
                        ModdedGuid = "f89711fccb22e14195962253cff4e416",
                        OriginalGuid = "df66f23a8d45ea408b6d09597cb203b7"
                    },
                    new GuidConfigItem()
                    {
                        Id = Guid.NewGuid(),
                        Name = "BP_ParentWaterItem -\u003E Box3",
                        ModdedGuid = "42022893e5814c4a8ec970555d5b60c1",
                        OriginalGuid = "d31649e7eb8d1f488ff885e82ce43b87"
                    },
                    new GuidConfigItem()
                    {
                        Id = Guid.NewGuid(),
                        Name = "BP_ParentWaterItem -\u003E ErrorWidget",
                        ModdedGuid = "3ed0478f3b28874da3efb5d59cd44e00",
                        OriginalGuid = "a20e2676c52da845b3f9ee89041c0033"
                    },
                    new GuidConfigItem()
                    {
                        Id = Guid.NewGuid(),
                        Name = "BP_ParentConveyor -\u003E AntiLockBox",
                        ModdedGuid = "1ce6b1ab8fe0ad4fa6cf3e43c409390f",
                        OriginalGuid = "b21c12787010824fafbd70b6a463b434"
                    },
                    new GuidConfigItem()
                    {
                        Id = Guid.NewGuid(),
                        Name = "BP_Compressor -\u003E Lever",
                        ModdedGuid = "8669ebcec8e5ec43af046409e1d89709",
                        OriginalGuid = "a3f0730b19159f45b48102c0e36d6c42"
                    },
                    new GuidConfigItem()
                    {
                        Id = Guid.NewGuid(),
                        Name = "BP_Compressor -\u003E Lid",
                        ModdedGuid = "e53e5471f4e5f3409621d79e113883f4",
                        OriginalGuid = "da0caf9d7327e4499dcf40292976db04"
                    },
                    new GuidConfigItem()
                    {
                        Id = Guid.NewGuid(),
                        Name = "BP_Compressor -\u003E RSpawn",
                        ModdedGuid = "c96ab47a1412ad48b03b649d05457500",
                        OriginalGuid = "b8ba2ac69fd14b4d841362ca1e315f55"
                    },
                    new GuidConfigItem()
                    {
                        Id = Guid.NewGuid(),
                        Name = "BP_Compressor -\u003E ESpawn",
                        ModdedGuid = "80666aa9f30c4c4c9bc18c8b1039b34c",
                        OriginalGuid = "a5590fde997a81438f9a8241ed5dea55"
                    },
                    new GuidConfigItem()
                    {
                        Id = Guid.NewGuid(),
                        Name = "BP_Compressor -\u003E SSpawn",
                        ModdedGuid = "d60967e02bba1a458be2d290a3af6c5f",
                        OriginalGuid = "2e4ae6edeab0b0479fae200bf9fe594d"
                    },
                    new GuidConfigItem()
                    {
                        Id = Guid.NewGuid(),
                        Name = "BP_Compressor -\u003E StorageBox",
                        ModdedGuid = "4838acf3286e1442820af9167485ed62",
                        OriginalGuid = "dc0f2a32c53ccd4b84f2da2847684b39"
                    },
                    new GuidConfigItem()
                    {
                        Id = Guid.NewGuid(),
                        Name = "BP_ParentLogic -\u003E Lgk1",
                        ModdedGuid = "23c9a4c659b20e499e07ad12e1f1269a",
                        OriginalGuid = "e95747e625376f4fa558daeb2c2b3118"
                    },
                    new GuidConfigItem()
                    {
                        Id = Guid.NewGuid(),
                        Name = "BP_ParentLogic -\u003E Lgk2",
                        ModdedGuid = "f8728af4f637b745af96726da513481c",
                        OriginalGuid = "cc8f4a1f4b4f824fb1c221fd037edc25"
                    },
                    new GuidConfigItem()
                    {
                        Id = Guid.NewGuid(),
                        Name = "BP_ParentLogic -\u003E Lgk3",
                        ModdedGuid = "2bb95406e8497c448a317d714f116dd8",
                        OriginalGuid = "4e4f43b4a026b546abe189831564ff76"
                    },
                    new GuidConfigItem()
                    {
                        Id = Guid.NewGuid(),
                        Name = "BP_BuyingStore -\u003E StaticMesh (the store\u0027s money bucket)",
                        ModdedGuid = "ac1f4d1e20c884469e6775f1c7262a35",
                        OriginalGuid = "58fe34c8f12dc14783bedddb7efe90ab"
                    },
                    new GuidConfigItem()
                    {
                        Id = Guid.NewGuid(),
                        Name = "BP_BuyingStore -\u003E Sphere (the money detection sphere)",
                        ModdedGuid = "51de5b3ff736784695dd9db3c104ff60",
                        OriginalGuid = "dee387e48f838f4f8f2c6d3e843584c7"
                    },
                    new GuidConfigItem()
                    {
                        Id = Guid.NewGuid(),
                        Name = "BP_BuyingStore -\u003E StaticMesh3 (the buy button\u0027s backboard)",
                        ModdedGuid = "65bf8c8f85ad1748b8858ceafb0096d5",
                        OriginalGuid = "48e4d3a8e9463a4fbd8ce17530366c62"
                    },
                    new GuidConfigItem()
                    {
                        Id = Guid.NewGuid(),
                        Name = "BP_BuyingStore -\u003E Buy (the text \\\u0022BUY\\\u0022)",
                        ModdedGuid = "243129e69b6bc84e8c6a5c980f986e36",
                        OriginalGuid = "8ad45c8e26a3fc499797460f16f5188c"
                    },
                    new GuidConfigItem()
                    {
                        Id = Guid.NewGuid(),
                        Name = "BP_BuyingStore -\u003E BuyText (the cost text)",
                        ModdedGuid = "a630048732993046ae0387cad3948ba1",
                        OriginalGuid = "8c3112119dc1a94caf36ab1b5ea7e215"
                    },
                    new GuidConfigItem()
                    {
                        Id = Guid.NewGuid(),
                        Name = "BP_BuyingStore -\u003E BuyButton",
                        ModdedGuid = "195cf0da168b7f49bc39efaf7b898c96",
                        OriginalGuid = "59279a754d77274da90f5b4219583884"
                    },
                    new GuidConfigItem()
                    {
                        Id = Guid.NewGuid(),
                        Name = "BP_BuyingStore -\u003E BuyBox",
                        ModdedGuid = "7e9f492be89e994f8787ce346ceb0f3c",
                        OriginalGuid = "01bdb51536891e448fd9a773be0208b9"
                    },
                    new GuidConfigItem()
                    {
                        Id = Guid.NewGuid(),
                        Name = "BP_BuyingStore -\u003E SecurityBox",
                        ModdedGuid = "1efc634e9f74864e91a12d37adcdb8f5",
                        OriginalGuid = "f44bf511b8df2945bcaf8dca35699538"
                    },
                    new GuidConfigItem()
                    {
                        Id = Guid.NewGuid(),
                        Name = "BP_StoreSimpleChair -\u003E StaticMesh (\\\u0022SM_BuildFloor\\\u0022)",
                        ModdedGuid = "831a9cca6605ce4bace4d9ce143d25b2",
                        OriginalGuid = "102aeb2191fd5c4787a1fe6fb0b1c270"
                    }
                }
            };
        }
    }
}
