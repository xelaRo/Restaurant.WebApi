using Dapper;
using Restaurant.WebApi.Infrastructure.OracleDb;
using Restaurant.WebApi.Infrastructure.OracleDb.Entities;
using Restaurant.WebApi.Models;
using Restaurant.WebApi.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IRestaurantDbConnection _dbCon;

        public OrderRepository(IRestaurantDbConnection dbCon)
        {
            _dbCon = dbCon;
        }

        public async Task<IEnumerable<Bill>> GetOrdersWithDeliveryByCustomerId(int customerId)
        {
            try
            {
                var billDictionary = new Dictionary<int, Bill>();

                var script = "SELECT * FROM bill b" +
                    " JOIN order_item oi on b.id = oi.orderId" +
                    " JOIN item i on oi.itemid = i.id" +
                    " JOIN Delivery_Track dt on dt.billid = b.id" +
                    $" WHERE dt.CustomerId = {customerId}";

                var con = await _dbCon.GetOltpConnection();
                var result = await con.QueryAsync<Bill,Order_Item,Item, Delivery_Track, Bill >(
                        script,
                        (bill, orderItem, item, deliveryTrack) =>
                        {
                            orderItem.Item = item;

                            Bill billEntry;

                            if (!billDictionary.TryGetValue(bill.Id, out billEntry))
                            {
                                billEntry = bill;
                                billEntry.OrderItems = new List<Order_Item>();
                                billDictionary.Add(billEntry.Id, billEntry);
                            }

                            billEntry.OrderItems.Add(orderItem);
                            bill.DeliveryTrack = deliveryTrack;

                            return billEntry;
                        }
                    );

                _dbCon.CloseConnection(con);

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Bill>> GetAllOrders()
        {
            try
            {
                var billDictionary = new Dictionary<int, Bill>();

                var script =
                    "SELECT * FROM(" +
                    " SELECT * FROM bill b" +
                    " JOIN order_item oi on b.id = oi.orderId" +
                    " JOIN item i on oi.itemid = i.id" +
                    " LEFT JOIN Delivery_Track dt on dt.billid = b.id" +
                    " ORDER BY b.Id Desc)" +
                    " WHERE rownum <= 100";

                var con = await _dbCon.GetOltpConnection();
                var result = await con.QueryAsync<Bill, Order_Item, Item, Delivery_Track, Bill >(
                        script,
                        (bill, orderItem, item, deliveryTrack) =>
                        {
                            orderItem.Item = item;

                            Bill billEntry;

                            if (!billDictionary.TryGetValue(bill.Id, out billEntry))
                            {
                                billEntry = bill;
                                billEntry.OrderItems = new List<Order_Item>();
                                billDictionary.Add(billEntry.Id, billEntry);
                            }

                            billEntry.OrderItems.Add(orderItem);
                            billEntry.DeliveryTrack = deliveryTrack;

                            return billEntry;
                        }
                    );

                _dbCon.CloseConnection(con);

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Menu>> GetMenusAndItems()
        {
            try
            {
                var menuDictionary = new Dictionary<int, Menu>();

                var script = "SELECT * FROM Menu m" +
                    " JOIN Menu_Item mi on mi.menuid = m.Id" +
                    " JOIN Item i on i.Id = mi.ItemId";

                var con = await _dbCon.GetOltpConnection();
                var result = await con.QueryAsync<Menu, MenuItem, Item, Menu>(
                        script,
                        (menu, menuItem, item) =>
                        {
                            Menu menuEntry;

                            if (!menuDictionary.TryGetValue(menu.Id, out menuEntry))
                            {
                                menuEntry = menu;
                                menu.Items = new List<Item>();
                                menuDictionary.Add(menuEntry.Id, menuEntry);
                            }

                            menuEntry.Items.Add(item);

                            return menuEntry;
                        }
                    );

                _dbCon.CloseConnection(con);

                return menuDictionary.Select(x => x.Value).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task CreateOrder(int customerId, bool isDelivery, List<ItemViewModel> items)
        {
            var billMaxIdScript = "SELECT MAX(Id) + 1 FROM BILL";
          
            var con = await _dbCon.GetOltpConnection();
            using (var tran = con.BeginTransaction())
            {
                try
                {
                    var billMaxId = await con.QueryFirstOrDefaultAsync<int>(billMaxIdScript);

                    var billInsertScript = "INSERT INTO Bill (ID, STATUS, SHIPPINGCOST)" +
                                     " SELECT" +
                                     $" {billMaxId}," +
                                     "'New'," +
                                     $"{new Random(1).Next(5, 25)}" +
                                     " FROM dual";

                    var billInsertResult = await con.ExecuteAsync(billInsertScript);

                    if(billInsertResult > 0)
                    {
                        foreach(var item in items)
                        {
                            var itemScript = $"SELECT * FROM ITEM Where Id = {item.Id}";
                            var itemResult = await con.QueryFirstOrDefaultAsync<Item>(itemScript);

                            var orderDetailInsertScript = "INSERT INTO ORDER_ITEM(ID, ORDERID, ITEMID, PRICE, QUANTITY)" +
                                " SELECT" +
                                " (SELECT MAX(Id) + 1 FROM ORDER_ITEM) AS Id," +
                                $"{billMaxId}," +
                                $"{item.Id}," +
                                $"{itemResult.Price * item.Quantity} ," +
                                $"{item.Quantity}" +
                                " FROM dual";

                            var orderDetailInsertResult = await con.ExecuteAsync(orderDetailInsertScript);
                        }

                        if (isDelivery)
                        {
                            var deliveryInsertScript =
                                "INSERT INTO DELIVERY_TRACK(ID, PICKEDUPAT, DELIVEREDAT, RETURNEDAT, CUSTOMERID, BILLID)" +
                                " SELECT" +
                                "(SELECT MAX(Id) + 1 FROM DELIVERY_TRACK)  AS Id," +
                                "NULL," +
                                "NULL," +
                                "NULL," +
                                $"{customerId}," +
                                $"{billMaxId}" +
                                " FROM DUAL";

                            var deliveryInsertResult = await con.ExecuteAsync(deliveryInsertScript);
                        }

                        tran.Commit();
                    }
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }
        }
    }
}
