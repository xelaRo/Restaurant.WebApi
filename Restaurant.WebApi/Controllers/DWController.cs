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
    }
}
