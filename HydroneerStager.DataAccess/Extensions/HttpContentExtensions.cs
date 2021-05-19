using HydroneerStager.Contracts.Models.Api.Bridgepour;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace HydroneerStager.DataAccess.Extensions
{
    internal static class HttpContentExtensions
    {
        public static async Task<BridgepourModsResult> ToBridgepourModsResultAsync(this HttpContent content)
        {
            return JsonConvert.DeserializeObject<BridgepourModsResult>(await content.ReadAsStringAsync());
        }
    }
}
