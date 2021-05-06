using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace HydroneerStager
{
    public sealed class Configuration
    {
        private static readonly string ConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config");

        private static IConfiguration Configuraton;

        public static async Task<AppConfiguration> GetConfigurationAsync()
        {
            var appConfiguration = new AppConfiguration();

            if (!File.Exists($"{ConfigPath}.json"))
            {
                CreateConfigFile(null);
            }

            if (Configuraton == null)
            {
                var configurationBuilder = new ConfigurationBuilder();
                configurationBuilder.AddJsonFile($"{ConfigPath}.json", false, true);

                try
                {
                    Configuraton = configurationBuilder.Build();
                }
                catch (Exception)
                {
                    CreateConfigFile(null);

                    return await GetConfigurationAsync();
                } 
            }

            Configuraton.GetSection(nameof(AppConfiguration)).Bind(appConfiguration);

            return appConfiguration;
        }

        public static void Save(AppConfiguration config)
        {
            CreateConfigFile(config);
        }

        private static void CreateConfigFile(AppConfiguration appConfiguration)
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
