using HydroModTools.DataAccess.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace HydroModTools.DataAccess.Extensions
{
    internal static class HttpContentExtensions
    {
        public static async Task<BridgepourModsResult> ToBridgepourModsResultAsync(this HttpContent content)
        {
            return JsonConvert.DeserializeObject<BridgepourModsResult>(await content.ReadAsStringAsync());
        }
    }
}
