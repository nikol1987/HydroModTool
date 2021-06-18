using HydroneerStager.Contracts.Models.Api.Bridgepour;
using HydroneerStager.DataAccess.Extensions;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace HydroneerStager.DataAccess.Services
{
    public class ApiBridgepourService : ServiceBase
    {
        public ApiBridgepourService(HttpClient httpClient) :
            base(httpClient, "https://api-temporary-1.bridgepour.repl.co/api")
        { }

        public async Task<BridgepourModsResult> GetModsAsync()
        {
            var result = await httpClient.GetAsync(GetRoute("/mods"));

            if (result.StatusCode == System.Net.HttpStatusCode.PermanentRedirect)
            {
                result = await RedoRedirect(result.Headers.Location.ToString());
            }

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(result.StatusCode.ToString());
            }

            return await result.Content.ToBridgepourModsResultAsync();
        }

        private async Task<HttpResponseMessage> RedoRedirect(string url)
        {
            var result = await httpClient.GetAsync(url);

            if (result.StatusCode == System.Net.HttpStatusCode.PermanentRedirect)
            {
                return await RedoRedirect(result.Headers.Location.ToString());
            }

            return result;
        }
    }
}
