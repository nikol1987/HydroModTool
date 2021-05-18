using System.Net.Http;

namespace HydroneerStager.DataAccess.Services
{
    public abstract class ServiceBase
    {
        protected HttpClient httpClient { get; }

        protected string BaseAddress { get; }

        protected ServiceBase(HttpClient httpClient, string baseAddress)
        {
            this.httpClient = httpClient;
            BaseAddress = baseAddress;
        }

        protected string GetRoute(string route)
        {
            return BaseAddress.TrimEnd('/') + "/" + route.TrimStart('/');
        }
    }
}