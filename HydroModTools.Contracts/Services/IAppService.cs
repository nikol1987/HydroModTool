using System.Threading.Tasks;

namespace HydroModTools.Contracts.Services
{
    public interface IAppService
    {
        Task<string> LoadAboutHtml();
    }
}
