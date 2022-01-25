using Funda.Business.Services.Interfaces;
using Funda.Controllers;
using Funda.Models;
using Funda.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Funda.Web.Controllers
{
    public class HomeController : APIControllerBase
    {
        private readonly IMakelaarService _makelaarService;

        public HomeController(IMakelaarService makelaarService)
        {
            _makelaarService = makelaarService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var makelaarsList = await _makelaarService.GetMakelaarWithMostHouseForSale(numberOfTopMakelaar: 10);
                var makelaarsListWithTuin = await _makelaarService.GetMakelaarWithMostHouseForSaleWithTuin(numberOfTopMakelaar: 10);

                return View(new MakelaarDetailsModel { MakelaarDetails = makelaarsList, MakelaarDetailsWithTuin = makelaarsListWithTuin });
            }
            catch (Exception ex)
            {
                LogError(ex, ex.Message);
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier , ErrorMessage = ex.InnerException.Message});
            };
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}