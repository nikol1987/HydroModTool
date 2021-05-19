using Hydroneer.Contracts.Models.WinformModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hydroneer.Contracts.WinFormsServices
{
    public interface IBridgepourService
    {
        Task<IReadOnlyCollection<BridgepourModModel>> GetModList();
        Task DownloadMod(string url);
    }
}