using Restaurant.WebApi.Infrastructure.OracleDb.Entities;
using Restaurant.WebApi.Models;
using Restaurant.WebApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<Bill>> GetOrdersWithDeliveryByCustomerId(int customerId)
        {
            var bill = await _orderRepository.GetOrdersWithDeliveryByCustomerId(customerId);

            return bill;
        }

        public async Task<IEnumerable<Bill>> GetAllOrders()
        {
            var deliveryTrack = await _orderRepository.GetAllOrders();

            return deliveryTrack;
        }

        public async Task<IEnumerable<Menu>> GetMenusAndItems()
        {
            return await _orderRepository.GetMenusAndItems();
        }

        public async Task CreateNewOrder(CreateNewOrderViewModel createNewOrderViewModel)
        {
            await _orderRepository.CreateOrder(createNewOrderViewModel.CustomerId,createNewOrderViewModel.IsDelivery, createNewOrderViewModel.Items);
        }
    }
}
