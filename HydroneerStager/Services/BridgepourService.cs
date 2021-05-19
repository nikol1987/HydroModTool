using Hydroneer.Contracts.Models.WinformModels;
using Hydroneer.Contracts.WinFormsServices;
using HydroneerStager.DataAccess.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hydroneer.Contracts.Extensions;
using System.Net;
using System.IO;
using System;

namespace HydroneerStager.Services
{
    public sealed class BridgepourService : IBridgepourService
    {
        private readonly string PaksFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Mining", "Saved", "Paks");

        private readonly ApiBridgepourService _apiBridgepourService;

        public BridgepourService(ApiBridgepourService apiBridgepourService)
        {
            _apiBridgepourService = apiBridgepourService;
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
    }
}
