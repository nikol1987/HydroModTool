using HydroModTools.Contracts.Models;
using HydroModTools.Contracts.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HydroModTools.Services
{
    internal sealed class GuidsService : IGuidsService
    {
        private readonly IConfigurationService _configurationService;

        public GuidsService(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }

        public async Task SaveGuids(IReadOnlyCollection<GuidItemModel> guids)
        {
            await _configurationService.SaveGuids(guids);
        }
    }
}
