using Microsoft.AspNetCore.Mvc;
using Restaurant.WebApi.Models;
using Restaurant.WebApi.Repository.FirstDb.Customer;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Controllers.FirstDB
{
    public class CustomerFirstDbController : BaseController
    {
        private readonly IFirstDbCustomerRepository _firstDbCustomerRepository;
        public CustomerFirstDbController(IFirstDbCustomerRepository firstDbOrderRepository)
        {
            _firstDbCustomerRepository = firstDbOrderRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _firstDbCustomerRepository.Get();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CustomerViewModel customerViewModel)
        {
            await _firstDbCustomerRepository.Create(customerViewModel);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody]CustomerEditViewModel customerEditViewModel)
        {
            await _firstDbCustomerRepository.Update(customerEditViewModel);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int billId)
        {
            await _firstDbCustomerRepository.Delete(billId);
            return Ok();
        }
    }
}
