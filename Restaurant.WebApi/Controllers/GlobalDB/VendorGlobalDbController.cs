using Microsoft.AspNetCore.Mvc;
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
    }
}
