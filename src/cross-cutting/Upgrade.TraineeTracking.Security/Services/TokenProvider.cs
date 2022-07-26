using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Upgrade.TraineeTracking.Security.Services.Abstractions;

namespace Upgrade.TraineeTracking.Security.Services
{
    public class TokenProvider : ITokenProvider
    {
        public string Token { get; set; }
        protected readonly IConfiguration _configuration;
        protected readonly JwtSecurityTokenHandler _tokenHandler;
        protected readonly HttpClient _identityServer;
        public TokenProvider(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _identityServer = httpClientFactory.CreateClient();
            _identityServer.BaseAddress = new Uri(_configuration["IAM_ADDRESS"]);
            _tokenHandler = new JwtSecurityTokenHandler();
        }
        public virtual async Task<string> GetTokenAsync()
        {
            if (ValidateToken(Token)) return Token;
            //else request token
            var discovery = await _identityServer.GetDiscoveryDocumentAsync();
            var tokenResponse = await _identityServer.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                ClientId = _configuration["ClientId"],
                ClientSecret = _configuration["ClientSecret"],
                Address = discovery.TokenEndpoint,
                Scope = "trainee-admin-service.read"
            });
            Token = tokenResponse.AccessToken;
            return Token;
        }

        protected bool ValidateToken(string token)
        {
            try
            {
                _tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateLifetime = true,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = validatedToken as JwtSecurityToken;

                // return user id from JWT token if validation successful
                return jwtToken.Claims.Any(x => x.Type == "id");
            }
            catch
            {
                return false;
            }
        }
    }
}