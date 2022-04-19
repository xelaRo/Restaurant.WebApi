using Microsoft.AspNetCore.Mvc;
using Restaurant.WebApi.Repository.GlobalDb.Item;
using Restaurant.WebApi.Repository.GlobalDb.Vendor;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Controllers.GlobalDB
{
    public class ItemGlobalDbController : BaseController
    {
        private readonly IGlobalDbItemRepository _globalDbItemRepository;

        public ItemGlobalDbController(IGlobalDbItemRepository globalDbItemRepository)
        {
            _globalDbItemRepository = globalDbItemRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _globalDbItemRepository.Get();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Models.SecondDb.ItemSecondDbViewModel createNewOrderViewModel)
        {
            await _globalDbItemRepository.Create(createNewOrderViewModel);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] Models.SecondDb.ItemEditViewModel editOrderViewModel)
        {
            await _globalDbItemRepository.Update(editOrderViewModel);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int billId)
        {
            await _globalDbItemRepository.Delete(billId);
            return Ok();
        }
    }
}
