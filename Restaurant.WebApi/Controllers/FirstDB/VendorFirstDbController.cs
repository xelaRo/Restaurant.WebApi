using Microsoft.AspNetCore.Mvc;
using Restaurant.WebApi.Models;
using Restaurant.WebApi.Repository.FirstDb.Vendor;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Controllers.FirstDB
{
    public class VendorFirstDbController : BaseController
    {
        private readonly IFirstDbVendorRepository _firstDbOrderRepository;

        public VendorFirstDbController(IFirstDbVendorRepository firstDbOrderRepository)
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
        public async Task<IActionResult> Create([FromBody] VendorViewModel createNewOrderViewModel)
        {
            await _firstDbOrderRepository.Create(createNewOrderViewModel);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] VendorEditViewModel editOrderViewModel)
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
