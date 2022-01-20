using Microsoft.AspNetCore.Mvc;
using Restaurant.WebApi.Services;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DWController : ControllerBase
    {
        private readonly IDataWarehouseService _dataWarehouseService;
        public DWController(IDataWarehouseService dataWarehouseService)
        {
            _dataWarehouseService = dataWarehouseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSales()
        {
            var result = await _dataWarehouseService.GetAllSales();

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetSalesTotalValueFor10Years()
        {
            var result = await _dataWarehouseService.GetItemSalesForLast10Years();

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetItemTotalSalesOnMenu()
        {
            var result = await _dataWarehouseService.GetItemTotalSalesOnMenu();

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetTotalItemSalesBetweenThisYearAndLastYear()
        {
            var result = await _dataWarehouseService.GetTotalItemSalesBetweenThisYearAndLastYear();

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAvergeDeliveryTimeOnCountyAndQuarter()
        {
            var result = await _dataWarehouseService.GetAvergeDeliveryTimeOnCountyAndQuarter();

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetSalesForItemsThatRequireCooking()
        {
            var result = await _dataWarehouseService.GetSalesForItemsThatRequireCooking();

            return Ok(result);
        }
    }
}
