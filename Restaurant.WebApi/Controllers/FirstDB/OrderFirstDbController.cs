using Microsoft.AspNetCore.Mvc;
using Restaurant.WebApi.Models;
using Restaurant.WebApi.Repository.FirstDb.Order;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Controllers.FirstDB
{
    public class OrderFirstDbController : BaseController
    {
        private readonly IFirstDbOrderRepository _firstDbOrderRepository;

        public OrderFirstDbController(IFirstDbOrderRepository firstDbOrderRepository)
        {
            _firstDbOrderRepository = firstDbOrderRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _firstDbOrderRepository.Get();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateNewOrderViewModel createNewOrderViewModel)
        {
             await _firstDbOrderRepository.Create(createNewOrderViewModel);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] EditOrderViewModel editOrderViewModel)
        {
            await _firstDbOrderRepository.Update(editOrderViewModel);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int billId)
        {
            await _firstDbOrderRepository.Delete(billId);
            return Ok();
        }
    }
}
