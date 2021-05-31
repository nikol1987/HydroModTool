using HydroModTools.DataAccess.Contracts.Models;
using HydroModTools.DataAccess.Contracts.Services;
using HydroModTools.DataAccess.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace HydroModTools.DataAccess.Services
{
    public sealed class BridgepourService : IBridgepourService
    {
        private readonly string PaksFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Mining", "Saved", "Paks");

        private readonly ApiBridgepourService _apiBridgepourService;

        public BridgepourService(HttpClient httpClient)
        {
            _apiBridgepourService = new ApiBridgepourService(httpClient);
        }

        public Task ClearMods()
        {
            Directory.Delete(PaksFolder, true);
            Directory.CreateDirectory(PaksFolder);

            return Task.CompletedTask;
        }

        public async Task DeleteMod(string url)
        {
            var uri = new Uri(url);
            var fileName = Path.GetFileName(uri.LocalPath);

            File.Delete(Path.Combine(PaksFolder, fileName));
        }

        public async Task DownloadMod(string url)
        {
            var uri = new Uri(url);
            var fileName = Path.GetFileName(uri.LocalPath);

            using (var webClient = new WebClient())
            {
                await webClient.DownloadFileTaskAsync(uri, Path.Combine(PaksFolder, fileName));
            }
        }

        public async Task<IReadOnlyCollection<BridgepourModModel>> GetModList()
        {
            var result = await _apiBridgepourService.GetModsAsync();

            if (result.Status != 200)
            {
                return new List<BridgepourModModel>();
            }

            return result.Results.ToModel();
        }

        public async Task<IReadOnlyCollection<string>> LoadedMods()
        {
            var files = Directory.EnumerateFiles(PaksFolder, "*_P.pak", new EnumerationOptions() {
                RecurseSubdirectories = false
            });

            return await Task.FromResult(files.ToList());
        }
    }
}
