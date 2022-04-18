using Dapper;
using Dapper.Oracle;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using Restaurant.WebApi.Infrastructure.OracleDb;
using Restaurant.WebApi.Infrastructure.OracleDb.Entities.DW;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Repository
{
    //public class DataWarehouseRepository : IDataWarehouseRepository
    //{
    //    private readonly Infrastructure.OracleDb.IApplicationDbConnection _dbCon;

    //    public DataWarehouseRepository(Infrastructure.OracleDb.IApplicationDbConnection dbCon)
    //    {
    //        _dbCon = dbCon;
    //    }

    //    public async Task<IEnumerable<Sales>> GetAllSales()
    //    {
    //        var script =
    //            "SELECT * FROM (" +
    //            " SELECT* FROM sales s" +
    //            " JOIN DwTime t on t.Id = s.TimeId" +
    //            " JOIN Bill b on b.Id = s.BillId" +
    //            " JOIN Customer c on c.Id = s.CustomerId" +
    //            " JOIN LOCATIONADDRESS la on la.Id = s.ADDRESSLOCATIONID" +
    //            " LEFT JOIN Delivery d on d.Id = s.DeliveryId" +
    //            " JOIN Item i on i.Id = s.ItemId" +
    //            " Order by t.FullDate Desc )" +
    //            " WHERE rownum <= 100";

    //        var con = await _dbCon.GetDwConnection();

    //        var result = await con.QueryAsync<Sales, Bill, Customer, LocationAddress, Delivery, Item, Sales>(script,
    //            (sales, bill, customer, locationAddress, delivery, item) =>
    //            {
    //                sales.Bill = bill;
    //                sales.Customer = customer;
    //                sales.LocationAddress = locationAddress;
    //                sales.Delivery = delivery;
    //                sales.Item = item;

    //                return sales;
    //            });

    //        return result;
    //    }

    //    public async Task<string> GetItemSalesForLast10Years()
    //    {
    //        var dwCon = await _dbCon.GetDwConnection();

    //        try
    //        {
    //            var p = new OracleDynamicParameters();
    //            p.Add("refcurs ", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

    //            var result = await dwCon.QueryAsync<object>("SPGETITEMTOTALVALUESOLDONTHELAST10YEARS".ToUpper(), p, commandType: CommandType.StoredProcedure);

    //            return JsonConvert.SerializeObject(result);
    //        }
    //        catch (System.Exception ex)
    //        {

    //            throw;
    //        }

    //    }

    //    public async Task<string> GetItemTotalSalesOnMenu()
    //    {
    //        var dwCon = await _dbCon.GetDwConnection();

    //        try
    //        {
    //            var p = new OracleDynamicParameters();
    //            p.Add("refcurs ", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

    //            var result = await dwCon.QueryAsync<object>("SPGETITEMTOTALSOLDVALUEONMENU".ToUpper(), p, commandType: CommandType.StoredProcedure);

    //            return JsonConvert.SerializeObject(result);
    //        }
    //        catch (System.Exception ex)
    //        {

    //            throw;
    //        }

    //    }

    //    public async Task<string> GetTotalItemSalesBetweenThisYearAndLastYear()
    //    {
    //        var dwCon = await _dbCon.GetDwConnection();

    //        try
    //        {
    //            var p = new OracleDynamicParameters();
    //            p.Add("refcurs ", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

    //            var result = await dwCon.QueryAsync<object>("SPCOMPARETOTALITEMSALESBETWEENCURRENTYEARANDLASTYEAR".ToUpper(), p, commandType: CommandType.StoredProcedure);

    //            return JsonConvert.SerializeObject(result);
    //        }
    //        catch (System.Exception ex)
    //        {

    //            throw;
    //        }

    //    }

    //    public async Task<string> GetAvergeDeliveryTimeOnCountyAndQuarter()
    //    {
    //        var dwCon = await _dbCon.GetDwConnection();

    //        try
    //        {
    //            var p = new OracleDynamicParameters();
    //            p.Add("refcurs ", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

    //            var result = await dwCon.QueryAsync<object>("SPGETAVERGEDELIVERYTYMEBYCOUNTYANDQUARTERS".ToUpper(), p, commandType: CommandType.StoredProcedure);

    //            return JsonConvert.SerializeObject(result);
    //        }
    //        catch (System.Exception ex)
    //        {

    //            throw;
    //        }

    //    }

    //    public async Task<string> GetSalesForItemsThatRequireCooking()
    //    {
    //        var dwCon = await _dbCon.GetDwConnection();

    //        try
    //        {
    //            var p = new OracleDynamicParameters();
    //            p.Add("refcurs ", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

    //            var result = await dwCon.QueryAsync<object>("SPGETSALESFORITEMSTHATREQUIRECOOKING".ToUpper(), p, commandType: CommandType.StoredProcedure);

    //            return JsonConvert.SerializeObject(result);
    //        }
    //        catch (System.Exception ex)
    //        {

    //            throw;
    //        }

    //    }
    //}
}
