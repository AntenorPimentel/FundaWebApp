using AutoMapper;
using Funda.Business.Models;
using Funda.Business.Services.BaseServices;
using Funda.Business.Services.Interfaces;
using Funda.Data.DTO;
using Funda.Data.Gateways.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Funda.Business.Services
{
    public class MakelaarService : MakelaarServiceBase, IMakelaarService
    {
        private readonly IKoopwoningenGateway _koopwoningenGateway;
        private readonly APIPaggingConfig _apiPaggingConfig;

        public MakelaarService(IKoopwoningenGateway koopwoningenGateway, IOptions<APIPaggingConfig> apiPaggingConfig, IMapper mapper)
        {
            _koopwoningenGateway = koopwoningenGateway;
            _apiPaggingConfig = apiPaggingConfig.Value;
            _mapper = mapper;
        }

        public async Task<List<MakelaarDetails>> GetMakelaarWithMostHouseForSale(int numberOfTopMakelaar)
        {
            if(numberOfTopMakelaar == 0)
                throw new ArgumentException("NumberOfTopMakelaar is not valid");

            var localization = GetEnumDescription(Enums.Cities.Amsterdam);
            var makelaarDetails = await GetAllHousesForSale(new List<string> { localization });

            return FilterHousesForSalesByMakelaar(makelaarDetails).Take(numberOfTopMakelaar).ToList();
        }

        public async Task<List<MakelaarDetails>> GetMakelaarWithMostHouseForSaleWithTuin(int numberOfTopMakelaar)
        {
            if (numberOfTopMakelaar == 0)
                throw new ArgumentException("NumberOfTopMakelaar is not valid");

            var localization = GetEnumDescription(Enums.Cities.Amsterdam);
            var buitenruimte = GetEnumDescription(Enums.Buitenruimte.Tuin);
            var makelaarDetails = await GetAllHousesForSale(new List<string> { localization, buitenruimte });

            return FilterHousesForSalesByMakelaar(makelaarDetails).Take(numberOfTopMakelaar).ToList();
        }

        private async Task<List<MakelaarDetails>> GetAllHousesForSale(List<string> filter)
        {
            var pageNumber = _apiPaggingConfig.PageNumber;
            var pageSize = _apiPaggingConfig.PageSize;

            var makelaarsPersistence = new MakelaarsPersistence();
            var housesForSale = new List<HouseForSalePersistence>();

            do
            {
                makelaarsPersistence = await _koopwoningenGateway.GetHousesForSale(filter, pageNumber, pageSize);

                if (makelaarsPersistence == null)
                    throw new ArgumentNullException(nameof(makelaarsPersistence));

                if(IsValidMakelaarsPersistence(makelaarsPersistence))
                    housesForSale = housesForSale.Concat(makelaarsPersistence.HouseForSale).ToList();

            } while (makelaarsPersistence.Paging.AantalPaginas > pageNumber++);

            return _mapper.Map<List<HouseForSalePersistence>, List<MakelaarDetails>>(housesForSale);
        }

        private static IEnumerable<MakelaarDetails> FilterHousesForSalesByMakelaar(List<MakelaarDetails> makelaarDetails) =>
            makelaarDetails
                .GroupBy(obj => new { obj.MakelaarId, obj.MakelaarNaam })
                .Select(group => new MakelaarDetails { MakelaarId = group.Key.MakelaarId, MakelaarNaam = group.Key.MakelaarNaam, Count = group.Count() })
                .OrderByDescending(obj => obj.Count);
    }
}