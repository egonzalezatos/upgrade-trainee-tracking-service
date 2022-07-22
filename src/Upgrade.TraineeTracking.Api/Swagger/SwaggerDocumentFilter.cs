using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Upgrade.TraineeTracking.Api.Swagger
{
    public class SwaggerDocumentFilter : IDocumentFilter
    {
        private  IServer Server { get; set; }

        public SwaggerDocumentFilter(IServer server)
        {
            Server = server;
        }

        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            foreach (string address in Server.Features.Get<IServerAddressesFeature>().Addresses)
                swaggerDoc.Servers = new List<OpenApiServer> {
                    new() { Url = address }
                };
        }
    }
}