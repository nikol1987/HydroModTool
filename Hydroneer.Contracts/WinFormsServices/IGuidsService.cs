using HydroneerStager.Contracts.Models.AppModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hydroneer.Contracts.WinFormsServices
{
    public interface IGuidsService
    {
        Task SaveGuids(IReadOnlyCollection<GuidItem> guids);
    }
}
