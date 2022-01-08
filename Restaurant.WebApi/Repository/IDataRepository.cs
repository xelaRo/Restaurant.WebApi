using System.Threading.Tasks;

namespace Restaurant.WebApi.Repository
{
    public interface IDataRepository
    {
        Task ImportOLTPDataToDw();
    }
}