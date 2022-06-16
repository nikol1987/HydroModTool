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
            
            if (!File.Exists($"{AppVars.UIDsConfigPath}.json"))
            {
                CreateConfigFileAsync(ConfigFile.UIDs).Wait();
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
                await CreateConfigFileAsync(
                    ConfigFile.All,
                    _appConfiguration.ToModel().ToGeneralConfig(),
                    _appConfiguration.ToModel().ToGuidsConfig(),
                    _appConfiguration.ToModel().ToUIDsConfig());

                return;
            }

            await CreateConfigFileAsync(
                ConfigFile.All,
                appConfigModel.ToGeneralConfig(),
                appConfigModel.ToGuidsConfig(),
                appConfigModel.ToUIDsConfig());

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
                
                var uidConfiguration = new UIDsConfig();
                configuration.GetSection("UIDConfiguration").Bind(uidConfiguration);

                _appConfiguration = new AppConfig(generalConfig, guidConfiguration, uidConfiguration);
            }
            catch (Exception)
            {
                await CreateConfigFileAsync(ConfigFile.All);

                await LoadConfigAsync(configuration);
            }
        }

        private static async Task CreateConfigFileAsync(ConfigFile configFile, GeneralConfig? generalConfig = null, GuidsConfig? guidsConfig = null, UIDsConfig? uiDsConfig = null)
        {
            var jsonOptions = new JsonSerializerOptions()
            {
                WriteIndented = true
            };

            var configTasks = new List<Task>();

            if ((configFile == ConfigFile.General || configFile == ConfigFile.All) && File.Exists($"{AppVars.ConfigPath}.json"))
            {
                if (generalConfig == null)
                {
                    File.Move($"{AppVars.ConfigPath}.json", AppVars.ConfigPath + "-" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".json");
                }
                
                string generalJson = JsonSerializer.Serialize(new { AppConfiguration = generalConfig == null ? Defaults.DefaultGeneralConfig : generalConfig }, jsonOptions);
                configTasks.Add(File.WriteAllTextAsync($"{AppVars.ConfigPath}.json", generalJson));
            } else if (configFile == ConfigFile.General || configFile == ConfigFile.All)
            {
                string generalJson = JsonSerializer.Serialize(new { AppConfiguration = generalConfig == null ? Defaults.DefaultGeneralConfig : generalConfig }, jsonOptions);
                configTasks.Add(File.WriteAllTextAsync($"{AppVars.ConfigPath}.json", generalJson));
            }

            if ((configFile == ConfigFile.Guids || configFile == ConfigFile.All) && File.Exists($"{AppVars.GuidsConfigPath}.json"))
            {
                if (guidsConfig == null)
                {
                    File.Move($"{AppVars.GuidsConfigPath}.json", AppVars.GuidsConfigPath + "-" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".json");
                }
                
                string guidJson = JsonSerializer.Serialize(new { GuidConfiguration = guidsConfig == null ? Defaults.DefaultGuidsConfig : guidsConfig }, jsonOptions);
                configTasks.Add(File.WriteAllTextAsync($"{AppVars.GuidsConfigPath}.json", guidJson));
            } else if (configFile == ConfigFile.Guids || configFile == ConfigFile.All)
            {
                string guidJson = JsonSerializer.Serialize(new { GuidConfiguration = guidsConfig == null ? Defaults.DefaultGuidsConfig : guidsConfig }, jsonOptions);
                configTasks.Add(File.WriteAllTextAsync($"{AppVars.GuidsConfigPath}.json", guidJson));
            }

            if ((configFile == ConfigFile.UIDs || configFile == ConfigFile.All) && File.Exists($"{AppVars.UIDsConfigPath}.json"))
            {
                if (uiDsConfig == null)
                {
                    File.Move($"{AppVars.UIDsConfigPath}.json", AppVars.UIDsConfigPath + "-" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".json");
                }
                
                string uidJson = JsonSerializer.Serialize(new { UIDConfiguration = uiDsConfig == null ? Defaults.DefaultUIDsConfig : uiDsConfig }, jsonOptions);
                configTasks.Add(File.WriteAllTextAsync($"{AppVars.UIDsConfigPath}.json", uidJson));
            } else if (configFile == ConfigFile.UIDs || configFile == ConfigFile.All)
            {
                string uidJson = JsonSerializer.Serialize(new { UIDConfiguration = uiDsConfig == null ? Defaults.DefaultUIDsConfig : uiDsConfig }, jsonOptions);
                configTasks.Add(File.WriteAllTextAsync($"{AppVars.UIDsConfigPath}.json", uidJson));
            }

            if (configTasks.Count == 0)
            {
                return;
            }
            
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

            public static readonly UIDsConfig DefaultUIDsConfig = new UIDsConfig()
            {
                UIDs = new List<UIDsConfigItem>()
                {
                    new UIDsConfigItem()
                    {
                        Id = Guid.NewGuid(),
                        Name = "DT_Fish->Type",
                        ModdedUID = "Type_2_4C84CF4E4AD838576148EF9583F4E553",
                        OriginalUID = "Type_4_0BC375574CB011A04F011A80D3B0B877"
                    },
                    new UIDsConfigItem()
                    {
                        Id = Guid.NewGuid(),
                        Name = "DT_Fish->Name",
                        ModdedUID = "Name_7_88DB23D24DF8BBB564F47FAFE89A9E3D",
                        OriginalUID = "Name_14_A74E44DC49472A95C3508585ED17534C"
                    },
                    new UIDsConfigItem()
                    {
                        Id = Guid.NewGuid(),
                        Name = "DT_Fish->Description",
                        ModdedUID = "Description_9_AB48C14841BC3D330B6A6BB115D41C65",
                        OriginalUID = "Description_8_B6A3E9DD491F3B8D1CD0C28C9783C569"
                    },
                    new UIDsConfigItem()
                    {
                        Id = Guid.NewGuid(),
                        Name = "DT_Fish->Price",
                        ModdedUID = "Price_11_7350157F41013C928069CFA5ABD0BFD2",
                        OriginalUID = "Price_15_8AF2CC804DC195FE706F7997D3222616"
                    },
                    new UIDsConfigItem()
                    {
                        Id = Guid.NewGuid(),
                        Name = "DT_Fish->Mesh",
                        ModdedUID = "Mesh_14_282B16EB4AE7BF6D1263DCB9836FC9E4",
                        OriginalUID = "Mesh_18_CCCE00EF4DA316470947819EE1BEE9E7"
                    },
                    new UIDsConfigItem()
                    {
                        Id = Guid.NewGuid(),
                        Name = "DT_Functionless->Name",
                        ModdedUID = "Name_2_4C84CF4E4AD838576148EF9583F4E553",
                        OriginalUID = "Name_6_F7DF8D9644204240F823B7957E99127A"
                    },
                    new UIDsConfigItem()
                    {
                        Id = Guid.NewGuid(),
                        Name = "DT_Functionless->Description",
                        ModdedUID = "Description_4_3BC821B34599A15BEED7D786B78BB034",
                        OriginalUID = "Description_7_72D3ABB040B2D309AE8AD38B9F03198C"
                    },
                    new UIDsConfigItem()
                    {
                        Id = Guid.NewGuid(),
                        Name = "DT_Functionless->StoreSellPrice",
                        ModdedUID = "StoreSellPrice_10_F584A93F4279DB6CDF2E84853E735714",
                        OriginalUID = "StoreSellPrice_9_87B7F9F340952C3B014CF5888331009B"
                    },
                    new UIDsConfigItem()
                    {
                        Id = Guid.NewGuid(),
                        Name = "DT_Functionless->URL",
                        ModdedUID = "URL_11_02CBA44E4B53F4E256B0839087028F7D",
                        OriginalUID = "URL_12_BDEBE72747BFD50FEFDAE89565A18D38"
                    },
                    new UIDsConfigItem()
                    {
                        Id = Guid.NewGuid(),
                        Name = "DT_Functionless->PlacementType",
                        ModdedUID = "PlacementType_13_377CA2434D2FCA64D952F19EFD0BD281",
                        OriginalUID = "PlacementType_21_B2C702A846D4FE4D7F584D9AB4CEB3D9"
                    },
                    new UIDsConfigItem()
                    {
                        Id = Guid.NewGuid(),
                        Name = "DT_Functionless->CanRTY?",
                        ModdedUID = "CanRTY?_15_93765AE14231B0B0D4D99C892E9FD1A8",
                        OriginalUID = "CanRTY?_25_290FAB484E0C0DDA5BC2429A81A6197A"
                    },
                    new UIDsConfigItem()
                    {
                        Id = Guid.NewGuid(),
                        Name = "DT_Functionless->Mesh",
                        ModdedUID = "Mesh_18_8896812D439492850AB77FAA48D7A932",
                        OriginalUID = "Mesh_30_7E1891BD4211200E9F0BCAB8D7DD4452"
                    },
                    new UIDsConfigItem()
                    {
                        Id = Guid.NewGuid(),
                        Name = "DT_Functionless->DropSound",
                        ModdedUID = "DropSound_21_38EF9B874019E81F578FEC975E545DAA",
                        OriginalUID = "DropSound_15_047AD14B493506B16705209C6E5046F5"
                    },
                    new UIDsConfigItem()
                    {
                        Id = Guid.NewGuid(),
                        Name = "DT_Functionless->GridSquare",
                        ModdedUID = "GridSquare_24_6F9FD637408F87FBB7E4D6B42E80FB45",
                        OriginalUID = "GridSquare_31_5D24B1F0427187E5917B3694993C9A72"
                    },
                    new UIDsConfigItem()
                    {
                        Id = Guid.NewGuid(),
                        Name = "DT_MasterworkMoulds->Name",
                        ModdedUID = "Name_2_4C84CF4E4AD838576148EF9583F4E553",
                        OriginalUID = "Name_19_BDAAD5534ED637B65E392B82E8844517"
                    },
                    new UIDsConfigItem()
                    {
                        Id = Guid.NewGuid(),
                        Name = "DT_MasterworkMoulds->CraftableClass",
                        ModdedUID = "CraftableClass_5_3568F3894D5480561AE65A86E1EBCA28",
                        OriginalUID = "CraftableClass_5_A1018E4D4AB08DE2F4A8A0ABC2545E1F"
                    },
                    new UIDsConfigItem()
                    {
                        Id = Guid.NewGuid(),
                        Name = "DT_MasterworkMoulds->StoreSellPrice",
                        ModdedUID = "StoreSellPrice_8_F3B5048B4142BCF13C12209C20224F66",
                        OriginalUID = "StoreSellPrice_22_426D9A36487FF8738860B1A7E298F99F"
                    },
                    new UIDsConfigItem()
                    {
                        Id = Guid.NewGuid(),
                        Name = "DT_MasterworkMoulds->Mesh",
                        ModdedUID = "Mesh_11_6479B29449C81ACB258F9F8C6E78BC12",
                        OriginalUID = "Mesh_12_E95105BA46848F65220581B740FE2257"
                    },
                    new UIDsConfigItem()
                    {
                        Id = Guid.NewGuid(),
                        Name = "DT_MasterworkMoulds->Requirements",
                        ModdedUID = "Requirements_15_A353DC974C5F34A7F2C35B8B7DA0EDB4",
                        OriginalUID = "Requirements_13_8D84B51546F36C758B7B19A998B47358"
                    },
                    new UIDsConfigItem()
                    {
                        Id = Guid.NewGuid(),
                        Name = "DT_Rod->Type",
                        ModdedUID = "Type_8_4C84CF4E4AD838576148EF9583F4E553",
                        OriginalUID = "Type_25_0BC375574CB011A04F011A80D3B0B877"
                    },
                    new UIDsConfigItem()
                    {
                        Id = Guid.NewGuid(),
                        Name = "DT_Rod->Name",
                        ModdedUID = "Name_11_358F36674E991957884324A361CDEB16",
                        OriginalUID = "Name_14_A74E44DC49472A95C3508585ED17534C"
                    },
                    new UIDsConfigItem()
                    {
                        Id = Guid.NewGuid(),
                        Name = "DT_Rod->Description",
                        ModdedUID = "Description_13_3C1789354CE5AF9D0676DAA6D36D563B",
                        OriginalUID = "Description_8_B6A3E9DD491F3B8D1CD0C28C9783C569"
                    },
                    new UIDsConfigItem()
                    {
                        Id = Guid.NewGuid(),
                        Name = "DT_Rod->Luck",
                        ModdedUID = "Luck_15_3E8AD9CB4530C441EA264E9E85E72EB0",
                        OriginalUID = "Luck_32_7BAF4BEF4D241621F91E3DA2BE535D24"
                    },
                    new UIDsConfigItem()
                    {
                        Id = Guid.NewGuid(),
                        Name = "DT_Rod->Mesh",
                        ModdedUID = "Mesh_17_51DAD5CF472ADF2C2C3658B406420639",
                        OriginalUID = "Mesh_18_CCCE00EF4DA316470947819EE1BEE9E7"
                    },
                    new UIDsConfigItem()
                    {
                        Id = Guid.NewGuid(),
                        Name = "DT_Rod->LineColor",
                        ModdedUID = "LineColor_19_57B775F04F6853D727324BA1C7BE77EF",
                        OriginalUID = "LineColor_29_3D848A784259CAA3A1117DA7691D4EB1"
                    }
                }
            };
        }
    }
}
