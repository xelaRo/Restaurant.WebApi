using Dapper;
using Restaurant.WebApi.Infrastructure.OracleDb;
using System.Data;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Repository
{
    //public class DataRepository : IDataRepository
    //{
    //    private readonly Infrastructure.OracleDb.IApplicationDbConnection _dbCon;

    //    public DataRepository(Infrastructure.OracleDb.IApplicationDbConnection dbCon)
    //    {
    //        _dbCon = dbCon;
    //    }

    //    public async Task ImportOLTPDataToDw()
    //    {
    //        var dwCon = await _dbCon.GetDwConnection();

    //        using (var transaction = dwCon.BeginTransaction())
    //        {
    //            var seqScript = "SELECT COUNT(*) " +
    //                " FROM user_sequences" +
    //                " WHERE sequence_name = 'SALES_SEQ'";

    //            var sequenceExists = await dwCon.QueryFirstAsync<int>(seqScript);

    //            if(sequenceExists > 0)
    //            {
    //                var dropResult = await dwCon.ExecuteAsync(@"DROP SEQUENCE SALES_SEQ");
    //            }

    //            transaction.Commit();
    //        }

    //        var newSeqResult = await dwCon.ExecuteAsync(@"CREATE SEQUENCE SALES_SEQ START WITH 1 INCREMENT BY 1");

    //        try
    //        {
    //            var result = await dwCon.ExecuteAsync("SPINSERTDATAFROMOLTPTODW", null, null, null, commandType: CommandType.StoredProcedure);
    //        }
    //        catch (System.Exception ex)
    //        {
    //            throw;
    //        }
          
    //    }
    //}
}
