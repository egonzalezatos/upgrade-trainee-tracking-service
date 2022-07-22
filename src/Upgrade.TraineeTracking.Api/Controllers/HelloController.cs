using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Identity.Web.Resource;
using Upgrade.TraineeTracking.OIDC.Extensions;
using Upgrade.TraineeTracking.Services.Abstractions.Services;

namespace Upgrade.TraineeTracking.Api.Controllers
{

    [ApiController]
    [Route("/")]
    public class HelloController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;


        public HelloController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello mister.");
        }
        
        [HttpGet("secret")]
        [Log]
        //[RequiredScope("openid")]
        [Authorize]
        public async Task<IActionResult> GetSecret()
        {
            foreach (var userClaim in HttpContext.User.Claims)
            {
                Console.WriteLine(userClaim.Type);
                Console.WriteLine(userClaim.Value);
            }
            //HttpContext.VerifyUserHasAnyAcceptedScope("openid");
            var http = _httpClientFactory.CreateClient("api");
            var response = await http.GetAsync("check-auth");
            // var response = await http.RequestWithTokenAsync(HttpMethod.Get, "http://localhost:5002/check-auth");
            response.EnsureSuccessStatusCode();
            return Ok(response.Content.ReadAsStringAsync());
        }
    }
    public class LogAttribute : ActionFilterAttribute
    {
        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            foreach (var keyValuePair in context.HttpContext.Request.Headers)
            {
                Console.WriteLine($"{keyValuePair.Key}: {keyValuePair.Value}");
            }

            return base.OnActionExecutionAsync(context, next);
        }
    }
} 
