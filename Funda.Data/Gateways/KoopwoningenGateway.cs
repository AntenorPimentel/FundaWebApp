using Funda.Data.Clients;
using Funda.Data.DTO;
using Funda.Data.Gateways.Interfaces;
using Funda.Data.Infrastructure;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Funda.Data.Gateways
{
    public class KoopwoningenGateway : APIClientBase, IKoopwoningenGateway
    {
        public KoopwoningenGateway(IOptions<FundaAPIClientConfiguration> options) : base(options) { }

        public async Task<MakelaarsPersistence> GetHousesForSale(List<string> filter, int page, int pageSize)
        {
            var filterString = BuildStringForURL(filter);
            var response = await GetAsync($"?type=koop&zo={filterString}&page={page}&pageSize={pageSize}", "Error trying to get Houses from Sale from FundaAPI.");

            return JsonConvert.DeserializeObject<MakelaarsPersistence>(response);
        }
    }
}