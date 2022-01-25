using Funda.Data.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Funda.Data.Gateways.Interfaces
{
    public interface IKoopwoningenGateway
    {
        Task<MakelaarsPersistence> GetHousesForSale(List<string> filter, int page, int pageSize);
    }
}