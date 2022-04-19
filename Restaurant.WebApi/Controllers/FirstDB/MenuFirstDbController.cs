using Microsoft.AspNetCore.Mvc;
using Restaurant.WebApi.Repository.FirstDb.Menu;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Controllers.FirstDB
{
    public class MenuFirstDbController : BaseController
    {
        private readonly IFirstDbMenuItemRepository _firstDbMenuItemRepository;

        public MenuFirstDbController(IFirstDbMenuItemRepository firstDbMenuItemRepository)
        {
            _firstDbMenuItemRepository = firstDbMenuItemRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _firstDbMenuItemRepository.GetMenusAndItems();

            return Ok(result);
        }
    }
}
