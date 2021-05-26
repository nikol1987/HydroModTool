using HydroModTools.Contracts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HydroModTools.Contracts.Services
{
    public interface IGuidsService
    {
        Task SaveGuids(IReadOnlyCollection<GuidItemModel> guids);
    }
}
