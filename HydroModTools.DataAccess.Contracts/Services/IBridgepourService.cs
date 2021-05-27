using HydroModTools.DataAccess.Contracts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HydroModTools.DataAccess.Contracts.Services
{
    public interface IBridgepourService
    {
        Task<IReadOnlyCollection<BridgepourModModel>> GetModList();
        Task DownloadMod(string url);
    }
}
