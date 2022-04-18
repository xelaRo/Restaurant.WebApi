using Microsoft.AspNetCore.Mvc;
using Restaurant.WebApi.Repository.GlobalDb.Order;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Controllers.GlobalDB
{
    public class OrderGlobalDbController : BaseController
    {
        private readonly IGlobalDbOrderRepository _globalDbOrderRepository;

        public OrderGlobalDbController(IGlobalDbOrderRepository globalDbOrderRepository)
        {
            _globalDbOrderRepository = globalDbOrderRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _globalDbOrderRepository.Get();

            return Ok(result);
        }
    }
}
