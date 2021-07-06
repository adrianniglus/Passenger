using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text;
using Autofac.Extensions.DependencyInjection;
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
        private readonly IConfiguration _configuration;
        public ControllerTestsBase()
        {
            var path = Path.GetDirectoryName(Assembly.GetAssembly(typeof(ControllerTestsBase)).Location);
            //path = "C:\Passenger\Passenger.Tests.EndToEnd\bin\Debug\\net5.0";
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables().Build();
                
            

            var hostBuilder = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseConfiguration(config)
                .ConfigureServices(services => services.AddAutofac())
                .UseStartup<Startup>();

            Server = new TestServer(hostBuilder);
            Client = Server.CreateClient();
        }

        protected static StringContent GetPayload(object data)
        {
            var json = JsonConvert.SerializeObject(data);

            return new StringContent(json, Encoding.UTF8,"application/json");
        }
    }
}