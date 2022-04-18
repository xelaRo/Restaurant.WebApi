using Dapper;
using Microsoft.Extensions.Configuration;
using Restaurant.WebApi.Infrastructure.OracleDb;
using Restaurant.WebApi.Infrastructure.OracleDb.Entities;
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
                var result = await con.QueryAsync<Bill, Order_Item, Item, Delivery_Track, Bill>(
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
    }
}
