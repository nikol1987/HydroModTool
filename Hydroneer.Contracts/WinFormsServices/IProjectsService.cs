using System;
using System.Threading.Tasks;

namespace Hydroneer.Contracts.WinFormsServices
{
    public interface IProjectsService
    {
        Task AddProject(Guid id, string name, string assetsPath, string outputPath);
    }
}
