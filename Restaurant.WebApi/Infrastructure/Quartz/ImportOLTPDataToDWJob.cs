using Quartz;
using Restaurant.WebApi.Repository;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Infrastructure.Quartz
{
    [DisallowConcurrentExecution]
    public class ImportOLTPDataToDWJob : IJob
    {
        private readonly IDataRepository _dataRepository;
        public ImportOLTPDataToDWJob(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                await _dataRepository.ImportOLTPDataToDw();
            }
            catch (System.Exception ex)
            {

                throw;
            }
            
        }
    }
}
