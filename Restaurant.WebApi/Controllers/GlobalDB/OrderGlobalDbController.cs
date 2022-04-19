using Microsoft.AspNetCore.Mvc;
using Restaurant.WebApi.Models;
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateNewOrderViewModel createNewOrderViewModel)
        {
            await _globalDbOrderRepository.Create(createNewOrderViewModel);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] EditOrderViewModel editOrderViewModel)
        {
            await _globalDbOrderRepository.Update(editOrderViewModel);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int billId)
        {
            await _globalDbOrderRepository.Delete(billId);
            return Ok();
        }
    }
}
