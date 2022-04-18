using Microsoft.AspNetCore.Mvc;
using Restaurant.WebApi.Repository.FirstDb.Item;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Controllers.FirstDB
{
    public class ItemFirstDbController : BaseController
    {
        private readonly IFirstDbItemRepository _secondDbItemRepository;

        public ItemFirstDbController(IFirstDbItemRepository secondDbItemRepository)
        {
            _secondDbItemRepository = secondDbItemRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _secondDbItemRepository.Get();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Models.SecondDb.ItemSecondDbViewModel createNewOrderViewModel)
        {
            await _secondDbItemRepository.Create(createNewOrderViewModel);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] Models.SecondDb.ItemEditViewModel editOrderViewModel)
        {
            await _secondDbItemRepository.Update(editOrderViewModel);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int billId)
        {
            await _secondDbItemRepository.Delete(billId);
            return Ok();
        }
    }
}
