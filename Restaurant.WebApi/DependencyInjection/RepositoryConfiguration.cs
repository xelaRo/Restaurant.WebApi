using Microsoft.Extensions.DependencyInjection;
using Restaurant.WebApi.Infrastructure.OracleDb;
using Restaurant.WebApi.Repository;

namespace Restaurant.WebApi.DependencyInjection
{
    public static class RepositoryConfiguration
    {
        public static IServiceCollection ConfigureRepositoryCollection(this IServiceCollection service)
        {
            service.AddTransient<ICrudOperations, CrudOperations>();
            service.AddTransient<ICustomerRepository, CustomerRepository>();
            service.AddTransient<IOrderRepository, OrderRepository>();
            //service.AddScoped<ICountyRepository, CountyRepository>();
            //service.AddScoped<ISymptomRepository, SymptomRepository>();
            //service.AddScoped<ITestingRepository, TestingRepository>();
            //service.AddScoped<IPatientRepository, PatientRepository>();
            return service;
        }
    }
}
