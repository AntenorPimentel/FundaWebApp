using Funda.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Funda.Business.Services.Interfaces
{
    public interface IMakelaarService
    {
        Task<List<MakelaarDetails>> GetMakelaarWithMostHouseForSale(int numberOfTopMakelaar);
        Task<List<MakelaarDetails>> GetMakelaarWithMostHouseForSaleWithTuin(int numberOfTopMakelaar);
    }
}