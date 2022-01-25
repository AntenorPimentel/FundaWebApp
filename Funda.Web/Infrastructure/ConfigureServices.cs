using Funda.Business.Models;
using Funda.Business.Services;
using Funda.Business.Services.Interfaces;
using Funda.Data.Gateways;
using Funda.Data.Gateways.Interfaces;
using Funda.Data.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Funda.Infrastructure
{
    internal static class ConfigureServices
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            //Appsettigns Mappings
            services.Configure<FundaAPIClientConfiguration>(configuration.GetSection("FundaAPIConfiguration"));
            services.Configure<APIPaggingConfig>(configuration.GetSection("APIPaggingConfig"));

            //Services Classes
            services.AddTransient<IMakelaarService, MakelaarService>();

            // Gateways
            services.AddTransient<IKoopwoningenGateway, KoopwoningenGateway>();
        }
    }
}