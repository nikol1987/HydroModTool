using System.Threading.Tasks;

namespace Hydroneer.Contracts.WinFormsServices
{
    public interface IAppService
    {
        Task<string> LoadAboutHtml();
    }
}
