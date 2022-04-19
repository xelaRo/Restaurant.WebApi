using Dapper;
using Microsoft.Extensions.Configuration;
using Restaurant.WebApi.Infrastructure.OracleDb;
using Restaurant.WebApi.Infrastructure.OracleDb.Entities;
using Restaurant.WebApi.Models;
using Restaurant.WebApi.Models.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Repository.GlobalDb.Order
{
    public class GlobalDbOrderRepository : IGlobalDbOrderRepository
    {
        private DbConnection _dbConnection;
        private IDbConnection _dbCon;

        public GlobalDbOrderRepository(IConfiguration configuration)
        {
            _dbConnection = new DbConnection(configuration.GetConnectionString("RestaurantGlobal"));
            _dbCon = _dbConnection.GetConnection().Result;
        }
        public async Task<IEnumerable<Bill>> Get()
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

                var con = await _dbConnection.GetConnection();
                var result = await con.QueryAsync<Bill, Order_Item, Infrastructure.OracleDb.Entities.Item, Delivery_Track, Bill>(
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

                _dbConnection.CloseConnection(con);

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task Create(CreateNewOrderViewModel createNewOrderViewModel)
        {
            var billMaxIdScript = "SELECT MAX(Id) + 1 FROM BILL";

            using (var tran = _dbCon.BeginTransaction())
            {
                try
                {
                    var billMaxId = await _dbCon.QueryFirstOrDefaultAsync<int>(billMaxIdScript);
                    var shippingCost = createNewOrderViewModel.IsDelivery ? new Random(1).Next(5, 25) : 0;

                    var billInsertScript = "INSERT INTO Bill (ID, STATUS, SHIPPINGCOST, CREATEDAT)" +
                                     " SELECT " +
                                     $"{billMaxId}," +
                                     "'New'," +
                                     $"{shippingCost}," +
                                     $"'{DateTime.Now.ToString("dd'-'MMMM'-'yyyy")}'" +
                                     " FROM dual";

                    var billInsertResult = await _dbCon.ExecuteAsync(billInsertScript);

                    if (billInsertResult > 0)
                    {
                        foreach (var item in createNewOrderViewModel.Items)
                        {
                            var itemScript = $"SELECT * FROM ITEM Where Id = {item.Id}";
                            var itemResult = await _dbCon.QueryFirstOrDefaultAsync<Infrastructure.OracleDb.Entities.Item>(itemScript);

                            var orderDetailInsertScript = "INSERT INTO ORDER_ITEM(ID, ORDERID, ITEMID, PRICE, QUANTITY)" +
                                " SELECT" +
                                " (SELECT MAX(Id) + 1 FROM ORDER_ITEM) AS Id," +
                                $"{billMaxId}," +
                                $"{item.Id}," +
                                $"{itemResult.Price * item.Quantity} ," +
                                $"{item.Quantity}" +
                                " FROM dual";

                            var orderDetailInsertResult = await _dbCon.ExecuteAsync(orderDetailInsertScript);
                        }

                        if (createNewOrderViewModel.IsDelivery)
                        {
                            var deliveryInsertScript =
                                "INSERT INTO DELIVERY_TRACK(ID, PICKEDUPAT, DELIVEREDAT, RETURNEDAT, CUSTOMERID, BILLID)" +
                                " SELECT" +
                                "(SELECT MAX(Id) + 1 FROM DELIVERY_TRACK)  AS Id," +
                                "NULL," +
                                "NULL," +
                                "NULL," +
                                $"{createNewOrderViewModel.CustomerId}," +
                                $"{billMaxId}" +
                                " FROM DUAL";

                            var deliveryInsertResult = await _dbCon.ExecuteAsync(deliveryInsertScript);
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

        public async Task Delete(int id)
        {
            using (var tran = _dbCon.BeginTransaction())
            {
                try
                {
                    var billScript = $"SELECT * FROM BILL Where Id = {id}";
                    var bill = await _dbCon.QueryFirstOrDefaultAsync<Bill>(billScript);

                    var orderItemScript = $"SELECT * FROM ORDER_ITEM Where OrderId = {bill.Id}";
                    var orderItems = await _dbCon.QueryAsync<Order_Item>(orderItemScript);

                    foreach (var order_Item in orderItems)
                    {
                        var orderItemDeleteScript = $"DELETE FROM ORDER_ITEM Where Id = {order_Item.Id}";
                        var result = await _dbCon.ExecuteAsync(orderItemDeleteScript);
                    }

                    var billDeleteScript = $"DELETE FROM BILL Where Id = {bill.Id}";
                    var billDeleteScriptresult = await _dbCon.ExecuteAsync(billDeleteScript);

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        public async Task Update(EditOrderViewModel editOrderViewModel)
        {
            var billScript = $"SELECT * FROM BILL WHERE Id = {editOrderViewModel.BillId}";

            using (var tran = _dbCon.BeginTransaction())
            {
                try
                {
                    var bill = await _dbCon.QueryFirstOrDefaultAsync<Bill>(billScript);

                    if (bill == null)
                    {
                        throw new ArgumentNullException($"The bill with id {editOrderViewModel.BillId} was not found");
                    }

                    var billUpdateScript = $"UPDATE BILL SET STATUS = '{(OrderState)editOrderViewModel.StatusId}' WHERE id = {bill.Id}";

                    var result = await _dbCon.ExecuteAsync(billUpdateScript);

                    if (result > 0)
                    {
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
