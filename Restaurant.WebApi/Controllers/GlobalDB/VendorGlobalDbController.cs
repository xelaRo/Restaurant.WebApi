using Microsoft.AspNetCore.Mvc;
using Restaurant.WebApi.Models;
using Restaurant.WebApi.Repository.GlobalDb.Vendor;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Controllers.GlobalDB
{
    public class VendorGlobalDbController : BaseController
    {
        private readonly IGlobalDbVendorRepository _globalDbVendorRepository;

        public VendorGlobalDbController(IGlobalDbVendorRepository globalDbVendorRepository)
        {
            _globalDbVendorRepository = globalDbVendorRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _globalDbVendorRepository.Get();

            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] VendorViewModel vendorViewModel)
        {
            await _globalDbVendorRepository.Create(vendorViewModel);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] VendorEditViewModel vendorEditViewModel)
        {
            await _globalDbVendorRepository.Update(vendorEditViewModel);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _globalDbVendorRepository.Delete(id);
            return Ok();
        }
    }
}
