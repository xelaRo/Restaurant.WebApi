using Microsoft.AspNetCore.Mvc;
using Restaurant.WebApi.Models;
using Restaurant.WebApi.Models.SecondDb;
using Restaurant.WebApi.Repository.SecondDb.Order;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Controllers
{
    public class OrderSecondDbController : BaseController
    {
        private readonly ISecondDbOrderRepository _secondDbOrderRepository;

        public OrderSecondDbController(ISecondDbOrderRepository secondDbOrderRepository)
        {
            _secondDbOrderRepository = secondDbOrderRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _secondDbOrderRepository.Get();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderSecondDbViewModel customerViewModel)
        {
            await _secondDbOrderRepository.Create(customerViewModel);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] EditOrderViewModel customerEditViewModel)
        {
            await _secondDbOrderRepository.Update(customerEditViewModel);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int billId)
        {
            await _secondDbOrderRepository.Delete(billId);
            return Ok();
        }
    }
}
