using HydroneerStager.DataAccess.Extensions;
using HydroneerStager.DataAccess.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace HydroneerStager.DataAccess.Services
{
    public class BridgepourService : ServiceBase
    {
        public BridgepourService(HttpClient httpClient) :
            base(httpClient, "https://api.bridgepour.com/api")
        { }

        public async Task<BridgepourModsResult> GetModsAsync()
        {
            var result = await httpClient.GetAsync(GetRoute("/mods"));

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(result.StatusCode.ToString());
            }

            return await result.Content.ToBridgepourModsResultAsync();
        }
    }
}
