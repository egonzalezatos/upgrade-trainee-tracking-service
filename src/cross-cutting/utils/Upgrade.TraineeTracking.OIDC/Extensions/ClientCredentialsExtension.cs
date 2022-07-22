using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace Upgrade.TraineeTracking.OIDC.Extensions
{
    public static class ClientCredentialsExtension
    {
        public static async Task<HttpResponseMessage> RequestWithTokenAsync(this HttpClient httpClient, HttpMethod method, string uri)
        {
            //Try Request
            var request = new HttpRequestMessage(method, uri);
            var response = await httpClient.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
                response = await httpClient.RequestTokenAsync(new HttpRequestMessage(method, uri));
            return response;
        }

        private static async Task<HttpResponseMessage> RequestTokenAsync(this HttpClient httpClient, HttpRequestMessage request)
        {
            var idp = new HttpClient(){BaseAddress = new Uri("http://localhost:4777/")};
            var discovery = await idp.GetDiscoveryDocumentAsync();
            var tokenResponse = await idp.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                ClientId = "trainee_tracking_service",
                ClientSecret = "ClientSecret1",
                Address = discovery.TokenEndpoint,
                Scope = "trainee-admin-service.read"
            });
            httpClient.SetBearerToken(tokenResponse.AccessToken);
            return await httpClient.SendAsync(request);
        }
    }
}