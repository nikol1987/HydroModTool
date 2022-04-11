using HydroModTools.Contracts.Enums;
using System.Threading.Tasks;

namespace HydroModTools.Contracts.Services
{
    public interface IAppService
    {
        Task<string> LoadAboutHtml();

        Task<string> LoadAboutString();

        Task StartGame();

        Task SetGameVersion(HydroneerVersion hydroneerVersion);
    }
}
