using HydroModTools.DataAccess.Contracts.Models;
using HydroModTools.DataAccess.Contracts.Services;
using HydroModTools.DataAccess.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using static HydroModTools.Common.Constants;

namespace HydroModTools.DataAccess.Services
{
    public sealed class BridgepourService : IBridgepourService
    {
        private readonly ApiBridgepourService _apiBridgepourService;

        public BridgepourService(HttpClient httpClient)
        {
            _apiBridgepourService = new ApiBridgepourService(httpClient);
        }

        public async Task ClearMods()
        {
            await Task.Run(() => { 
                Directory.Delete(PaksFolder, true);
                Directory.CreateDirectory(PaksFolder);
            });
        }

        public async Task DeleteMod(string url)
        {
            await Task.Run(() => {
                var uri = new Uri(url);
                var fileName = Path.GetFileName(uri.LocalPath);

                File.Delete(Path.Combine(PaksFolder, fileName));
            });
        }

        public async Task DownloadMod(string url)
        {
            var uri = new Uri(url);
            var fileName = Path.GetFileName(uri.LocalPath);

            if (!Directory.Exists(PaksFolder))
            {
                Directory.CreateDirectory(PaksFolder);
            }

            using (var httpClient = new HttpClient())
            {
                var downloadedModFinalPath = Path.Combine(PaksFolder, fileName);
                var response = await httpClient.GetAsync(uri);
                var fs = new FileStream(downloadedModFinalPath, FileMode.Create, FileAccess.Write);
                await (await response.Content.ReadAsStreamAsync())
                    .CopyToAsync(fs)
                    .ContinueWith((task) =>
                    {
                      fs.Close();  
                    });
                
                if (new List<string>{ ".ZIP", ".RAR"}.Any(ex => fileName.ToUpper().EndsWith(ex)))
                {
                    try
                    {
                        ZipFile.ExtractToDirectory(downloadedModFinalPath, PaksFolder, true);
                        File.Delete(downloadedModFinalPath);
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }
            }
        }

        public async Task<IReadOnlyCollection<BridgepourModModel>> GetModList()
        {
            try
            {
                var result = await _apiBridgepourService.GetModsAsync();

                if (result.Status != 200)
                {
                    return new List<BridgepourModModel>();
                }

                return result.Results.ToModel();
            }
            catch (Exception)
            {

                return new List<BridgepourModModel>();
            }
        }

        public async Task<IReadOnlyCollection<string>> LoadedMods()
        {
            var files = Directory.EnumerateFiles(PaksFolder, "*_P.pak", new EnumerationOptions() {
                RecurseSubdirectories = false
            });

            return await Task.FromResult(files.ToList());
        }

        public Task OpenModFolder()
        {
            Process.Start("explorer.exe", PaksFolder);

            return Task.CompletedTask;
        }
    }
}
