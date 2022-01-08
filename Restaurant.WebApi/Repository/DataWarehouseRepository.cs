using Dapper;
using Restaurant.WebApi.Infrastructure.OracleDb;
using Restaurant.WebApi.Infrastructure.OracleDb.Entities.DW;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Repository
{
    public class DataWarehouseRepository : IDataWarehouseRepository
    {
        private readonly IRestaurantDbConnection _dbCon;

        public DataWarehouseRepository(IRestaurantDbConnection dbCon)
        {
            _dbCon = dbCon;
        }

        public async Task<IEnumerable<Sales>> GetAllSales()
        {
            var script =
                "SELECT * FROM (" +
                " SELECT* FROM sales s" +
                " JOIN DwTime t on t.Id = s.TimeId" +
                " JOIN Bill b on b.Id = s.BillId" +
                " JOIN Customer c on c.Id = s.CustomerId" +
                " JOIN LOCATIONADDRESS la on la.Id = s.ADDRESSLOCATIONID" +
                " LEFT JOIN Delivery d on d.Id = s.DeliveryId" +
                " JOIN Item i on i.Id = s.ItemId" +
                " Order by t.FullDate Desc )" +
                " WHERE rownum <= 100";

            var con = await _dbCon.GetDwConnection();

            var result = await con.QueryAsync<Sales, Bill, Customer, LocationAddress, Delivery,Item, Sales>(script,
                (sales, bill, customer, locationAddress, delivery, item ) =>
                {
                    sales.Bill = bill;
                    sales.Customer = customer;
                    sales.LocationAddress = locationAddress;
                    sales.Delivery = delivery;
                    sales.Item = item;

                    return sales;
                });

            return result;
        }
    }
}
