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

        private AppConfiguration _appConfiguration;

        public async Task<AppConfiguration> GetConfigurationAsync()
        {

            if (!File.Exists($"{ConfigPath}.json"))
            {
                CreateConfigFile(null);
            }

            if (_appConfiguration == null)
            {
                var configurationBuilder = new ConfigurationBuilder();
                configurationBuilder.AddJsonFile($"{ConfigPath}.json", false, true);

                try
                {
                    var appConfiguration = new AppConfiguration();

                    var configuration = configurationBuilder.Build();
                    configuration.GetSection(nameof(AppConfiguration)).Bind(appConfiguration);

                    _appConfiguration = appConfiguration;

                    return appConfiguration;
                }
                catch (Exception)
                {
                    CreateConfigFile(null);

                    return await GetConfigurationAsync();
                } 
            }


            return _appConfiguration;
        }

        public void Save(AppConfiguration config)
        {
            CreateConfigFile(config);

            _appConfiguration = config;
        }

        private void CreateConfigFile(AppConfiguration appConfiguration)
        {
            if (File.Exists($"{ConfigPath}.json") && appConfiguration == null)
            {
                File.Copy($"{ConfigPath}.json", ConfigPath + "-" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".json");
            }

            string json = JsonSerializer.Serialize(new FileConfig(appConfiguration), new JsonSerializerOptions()
            {
                WriteIndented = true
            });
            File.WriteAllText($"{ConfigPath}.json", json);
        }

        private class FileConfig
        {
            public FileConfig(AppConfiguration appConfiguration)
            {
                AppConfiguration = appConfiguration ?? new AppConfiguration();
            }

            public AppConfiguration AppConfiguration { get; set; }
        }
    }
}
