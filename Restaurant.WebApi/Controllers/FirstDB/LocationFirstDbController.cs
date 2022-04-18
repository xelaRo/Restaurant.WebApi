using Microsoft.AspNetCore.Mvc;
using Restaurant.WebApi.Models;
using Restaurant.WebApi.Repository.FirstDb.Location;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Controllers.FirstDB
{
    public class LocationFirstDbController : BaseController
    {
        private readonly IFirstDbLocationRepository _firstDbCustomerRepository;
        public LocationFirstDbController(IFirstDbLocationRepository firstDbOrderRepository)
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
        public async Task<IActionResult> Create([FromBody] LocationViewModel customerViewModel)
        {
            await _firstDbCustomerRepository.Create(customerViewModel);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] LocationEditViewModel customerEditViewModel)
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
