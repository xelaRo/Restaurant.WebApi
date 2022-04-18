using Microsoft.AspNetCore.Mvc;
using Restaurant.WebApi.Models;
using Restaurant.WebApi.Services;
using System;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Controllers
{
    //[Route("api/[controller]/[action]")]
    //[ApiController]
    //public class OrderController : ControllerBase
    //{
    //    private IOrderService _orderService;

    //    public OrderController(IOrderService orderService)
    //    {
    //        _orderService = orderService;
    //    }

    //    [HttpGet]
    //    public async Task<IActionResult> GetOrdersByCustomerId(int? customerId)
    //    {
    //        if (customerId == null)
    //        {
    //            throw new ArgumentNullException("The customer id is null");
    //        }

    //        var result = await _orderService.GetOrdersWithDeliveryByCustomerId(customerId.Value);
    //        return Ok(result);
    //    }

    //    [HttpGet]
    //    public async Task<IActionResult> GetAllOrders()
    //    {
    //        var result = await _orderService.GetAllOrders();
    //        return Ok(result);
    //    }

    //    [HttpGet]
    //    public async Task<IActionResult> GetItemsAndMenus()
    //    {
    //        var result = await _orderService.GetMenusAndItems();
    //        return Ok(result);
    //    }

    //    [HttpPost]
    //    public async Task<IActionResult> CreateNewOrder([FromBody] CreateNewOrderViewModel createNewOrder)
    //    {
    //        await _orderService.CreateNewOrder(createNewOrder);

    //        return Ok();
    //    }

    //    [HttpPost]
    //    public async Task<IActionResult> EditOrder ([FromBody] EditOrderViewModel createNewOrder)
    //    {
    //        await _orderService.EditOrder(createNewOrder);

    //        return Ok();
    //    }
    //}
}
