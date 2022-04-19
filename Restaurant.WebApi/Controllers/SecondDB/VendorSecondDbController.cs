using Microsoft.AspNetCore.Mvc;
using Restaurant.WebApi.Repository.SecondDb.Vendor;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Controllers.SecondDB
{
    public class VendorSecondDbController : BaseController
    {
        private readonly ISecondDbVendorRepository _secondDbVendorRepository;

        public VendorSecondDbController(ISecondDbVendorRepository secondDbVendorRepository)
        {
            _secondDbVendorRepository = secondDbVendorRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _secondDbVendorRepository.Get();

            return Ok(result);
        }
    }
}
