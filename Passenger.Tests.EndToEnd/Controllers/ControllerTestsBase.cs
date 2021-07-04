using System.IO;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Passenger.Api;

namespace Passenger.Tests.EndToEnd.Controllers
{
    public abstract class ControllerTestsBase
    {
        protected readonly TestServer Server;
        protected readonly HttpClient Client;
        public ControllerTestsBase()
        {
            Server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Development")
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseConfiguration(new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build())
                .UseStartup<Startup>());
            Client = Server.CreateClient();
        }

        protected static StringContent GetPayload(object data)
        {
            var json = JsonConvert.SerializeObject(data);

            return new StringContent(json, Encoding.UTF8,"application/json");
        }
    }
}