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
        private DbConnection _dbSecondDb;
        private DbConnection _dbConnectionFirstDb;
        private DbConnection _dbConnectionGlobalDb;
        private IDbConnection _dbConSecondDb;
        private IDbConnection _dbConFirstDb;
        private IDbConnection _dbConGlobalDb;

        public GlobalDbOrderRepository(IConfiguration configuration)
        {
            _dbConnectionFirstDb = new DbConnection(configuration.GetConnectionString("FirstDB"));
            _dbConFirstDb = _dbConnectionFirstDb.GetConnection().Result;

            _dbSecondDb = new DbConnection(configuration.GetConnectionString("SecondDB"));
            _dbConSecondDb = _dbSecondDb.GetConnection().Result;

            _dbConnectionGlobalDb = new DbConnection(configuration.GetConnectionString("SecondDB"));
            _dbConGlobalDb = _dbConnectionGlobalDb.GetConnection().Result;
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

                var result = await _dbConGlobalDb.QueryAsync<Bill, Order_Item, Infrastructure.OracleDb.Entities.Item, Delivery_Track, Bill>(
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

                _dbConGlobalDb.Close();

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task Create(CreateNewOrderViewModel createNewOrderViewModel)
        {
            var billId = 0;

            if (createNewOrderViewModel.IsDelivery)
            {
                using (var tran = _dbConFirstDb.BeginTransaction(IsolationLevel.ReadCommitted))
                {
                    var firstDbBillSequenceNumber = "SELECT bill_first_seq.nextval FROM dual";

                    try
                    {
                        billId = await _dbConFirstDb.QueryFirstOrDefaultAsync<int>(firstDbBillSequenceNumber);

                        var billInsertScript = "INSERT INTO Bill (ID, STATUS, SHIPPINGCOST, CREATEDAT)" +
                                         " SELECT " +
                                         $"{billId}," +
                                         "'New'," +
                                         $"{new Random(1).Next(5, 25)}," +
                                         $"'{DateTime.Now.ToString("dd'-'MMMM'-'yyyy")}'" +
                                         " FROM dual";

                        var billInsertResult = await _dbConFirstDb.ExecuteAsync(billInsertScript);

                        if (billInsertResult > 0)
                        {
                            foreach (var item in createNewOrderViewModel.Items)
                            {
                                var itemScript = $"SELECT * FROM ITEM Where Id = {item.Id}";
                                var itemResult = await _dbConFirstDb.QueryFirstOrDefaultAsync<Infrastructure.OracleDb.Entities.Item>(itemScript);

                                var firstDbOrderItemSequenceNumber = "SELECT orderItem_first_seq.nextval FROM dual";
                                var orderItemId = await _dbConFirstDb.QueryFirstOrDefaultAsync<int>(firstDbOrderItemSequenceNumber);

                                var orderDetailInsertScript = "INSERT INTO ORDER_ITEM(ID, ORDERID, ITEMID, PRICE, QUANTITY)" +
                                    " SELECT " +
                                    $"{orderItemId}," +
                                    $"{billId}," +
                                    $"{item.Id}," +
                                    $"{itemResult.Price * item.Quantity} ," +
                                    $"{item.Quantity}" +
                                    " FROM dual";

                                var orderDetailInsertResult = await _dbConFirstDb.ExecuteAsync(orderDetailInsertScript);

                            }
                        }

                        var deliveryInsertScript =
                         "INSERT INTO DELIVERY_TRACK(ID, PICKEDUPAT, DELIVEREDAT, RETURNEDAT, CUSTOMERID, BILLID)" +
                         " SELECT" +
                         "(SELECT MAX(Id) + 1 FROM DELIVERY_TRACK)  AS Id," +
                         "NULL," +
                         "NULL," +
                         "NULL," +
                         $"{createNewOrderViewModel.CustomerId}," +
                         $"{billId}" +
                         " FROM DUAL";

                        var deliveryInsertResult = await _dbConFirstDb.ExecuteAsync(deliveryInsertScript);

                        tran.Commit();
                    }
                    catch(Exception ex)
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
            else
            {
                using (var tran = _dbConSecondDb.BeginTransaction())
                {
                    try
                    {
                        var secondBillSequenceNumber = "SELECT bill_second_seq.nextval FROM dual";
                        billId = await _dbConSecondDb.QueryFirstOrDefaultAsync<int>(secondBillSequenceNumber);

                        var billInsertScript = "INSERT INTO bill (ID, STATUS, SHIPPINGCOST, CREATEDAT)" +
                                      " SELECT " +
                                      $"{billId}," +
                                      "'New'," +
                                      $"{0}," +
                                      $"'{DateTime.Now.ToString("dd'-'MMMM'-'yyyy")}'" +
                                      " FROM dual";

                        var billInsertResult = await _dbConSecondDb.ExecuteAsync(billInsertScript);

                        if (billInsertResult > 0)
                        {
                            foreach (var item in createNewOrderViewModel.Items)
                            {
                                var itemScript = $"SELECT * FROM ITEM Where Id = {item.Id}";
                                var itemResult = await _dbConSecondDb.QueryFirstOrDefaultAsync<Infrastructure.OracleDb.Entities.Item>(itemScript);

                                var secondOrderItemSequenceNumber = "SELECT orderItem_second_seq.nextval FROM dual";

                                var orderItemId = await _dbConSecondDb.QueryFirstOrDefaultAsync<int>(secondOrderItemSequenceNumber);

                                var orderDetailInsertScript = "INSERT INTO ORDER_ITEM(ID, ORDERID, ITEMID, PRICE, QUANTITY)" +
                                    " SELECT " +
                                    $"{orderItemId}," +
                                    $"{billId}," +
                                    $"{item.Id}," +
                                    $"{itemResult.Price * item.Quantity} ," +
                                    $"{item.Quantity}" +
                                    " FROM dual";

                                var orderDetailInsertResult = await _dbConSecondDb.ExecuteAsync(orderDetailInsertScript);

                                //testCon.Close();
                            }

                            tran.Commit();
                            _dbConSecondDb.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        _dbConSecondDb.Close();
                        tran.Rollback();
                    }
                }
            }
        }
        public async Task Delete(int id)
        {
            var billScript = $"SELECT * FROM BILL Where Id = {id}";

            var billFIrstDb = await _dbConFirstDb.QueryFirstOrDefaultAsync<Bill>(billScript);
            var billSecondDb = await _dbConSecondDb.QueryFirstOrDefaultAsync<Bill>(billScript);

            if(billFIrstDb != null)
            {
                using (var tran = _dbConFirstDb.BeginTransaction())
                {
                    try
                    {
                        var orderItemScript = $"SELECT * FROM ORDER_ITEM Where OrderId = {billFIrstDb.Id}";
                        var orderItems = await _dbConFirstDb.QueryAsync<Order_Item>(orderItemScript);

                        var deliveryTrackScript = $"SELECT * FROM DELIVERY_TRACK Where BillId = {billFIrstDb.Id}";
                        var deliveryTrack = await _dbConFirstDb.QueryFirstOrDefaultAsync<Delivery_Track>(deliveryTrackScript);



                        foreach (var order_Item in orderItems)
                        {
                            var orderItemDeleteScript = $"DELETE FROM ORDER_ITEM Where Id = {order_Item.Id}";
                            var result = await _dbConFirstDb.ExecuteAsync(orderItemDeleteScript);
                        }

                        var deliveryTrackDeleteScript = $"DELETE FROM DELIVERY_TRACK Where Id = {deliveryTrack.Id}";
                        var deliveryTrackDeleteResult = await _dbConFirstDb.ExecuteAsync(deliveryTrackDeleteScript);


                        var billDeleteScript = $"DELETE FROM BILL Where Id = {billFIrstDb.Id}";
                        var billDeleteScriptresult = await _dbConFirstDb.ExecuteAsync(billDeleteScript);

                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }

            if (billSecondDb != null)
            {
                using (var tran = _dbConSecondDb.BeginTransaction())
                {
                    try
                    {
                        var orderItemScript = $"SELECT * FROM ORDER_ITEM Where OrderId = {billFIrstDb.Id}";
                        var orderItems = await _dbConSecondDb.QueryAsync<Order_Item>(orderItemScript);

                        foreach (var order_Item in orderItems)
                        {
                            var orderItemDeleteScript = $"DELETE FROM ORDER_ITEM Where Id = {order_Item.Id}";
                            var result = await _dbConSecondDb.ExecuteAsync(orderItemDeleteScript);
                        }

                        var billDeleteScript = $"DELETE FROM BILL Where Id = {billFIrstDb.Id}";
                        var billDeleteScriptresult = await _dbConFirstDb.ExecuteAsync(billDeleteScript);

                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }
        public async Task Update(EditOrderViewModel editOrderViewModel)
        {
            var billScript = $"SELECT * FROM BILL Where Id = {editOrderViewModel.BillId}";

            var billFIrstDb = await _dbConFirstDb.QueryFirstOrDefaultAsync<Bill>(billScript);
            var billSecondDb = await _dbConSecondDb.QueryFirstOrDefaultAsync<Bill>(billScript);

            if(billFIrstDb != null)
            {
                using (var tran = _dbConFirstDb.BeginTransaction())
                {
                    try
                    {
                        var bill = await _dbConFirstDb.QueryFirstOrDefaultAsync<Bill>(billScript);

                        if (bill == null)
                        {
                            throw new ArgumentNullException($"The bill with id {editOrderViewModel.BillId} was not found");
                        }

                        var billUpdateScript = $"UPDATE BILL SET STATUS = '{(OrderState)editOrderViewModel.StatusId}' WHERE id = {bill.Id}";

                        var result = await _dbConFirstDb.ExecuteAsync(billUpdateScript);

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

            if (billSecondDb != null)
            {
                using (var tran = _dbConSecondDb.BeginTransaction())
                {
                    try
                    {
                        var bill = await _dbConSecondDb.QueryFirstOrDefaultAsync<Bill>(billScript);

                        if (bill == null)
                        {
                            throw new ArgumentNullException($"The bill with id {editOrderViewModel.BillId} was not found");
                        }

                        var billUpdateScript = $"UPDATE BILL SET STATUS = '{(OrderState)editOrderViewModel.StatusId}' WHERE id = {bill.Id}";

                        var result = await _dbConSecondDb.ExecuteAsync(billUpdateScript);

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
}
