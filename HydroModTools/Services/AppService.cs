using HydroModTools.Contracts.Services;
using Markdig;
using System.IO;
using System.Threading.Tasks;

namespace HydroModTools.Services
{
    internal sealed class AppService : IAppService
    {
        public async Task<string> LoadAboutString()
        {
            var markdown = await File.ReadAllTextAsync("Readme.md");

            return markdown;
        }

        public async Task<string> LoadAboutHtml()
        {
            var markdown = await LoadAboutString();

            var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            return Markdig.Markdown.ToHtml(markdown, pipeline);
        }
    }
}
