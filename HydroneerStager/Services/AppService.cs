using Hydroneer.Contracts.WinFormsServices;
using Markdig;
using System.Net;
using System.Threading.Tasks;

namespace HydroneerStager.Services
{
    public sealed class AppService : IAppService
    {
        public async Task<string> LoadAboutHtml()
        {
            var markdown = string.Empty;

            using (var webClient = new WebClient())
            {
                markdown = await webClient.DownloadStringTaskAsync("https://raw.githubusercontent.com/ResaloliPT/HydroModTool/master/Readme.md");
            }

            var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            return Markdown.ToHtml(markdown, pipeline);
        }
    }
}
