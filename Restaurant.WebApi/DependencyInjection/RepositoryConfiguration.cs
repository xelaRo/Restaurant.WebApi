using Microsoft.Extensions.DependencyInjection;
using Restaurant.WebApi.Repository.FirstDb.Customer;
using Restaurant.WebApi.Repository.FirstDb.Item;
using Restaurant.WebApi.Repository.FirstDb.Location;
using Restaurant.WebApi.Repository.FirstDb.Order;
using Restaurant.WebApi.Repository.FirstDb.Vendor;
using Restaurant.WebApi.Repository.GlobalDb.Order;
using Restaurant.WebApi.Repository.GlobalDb.Vendor;
using Restaurant.WebApi.Repository.SecondDb.Order;

namespace Restaurant.WebApi.DependencyInjection
{
    public static class RepositoryConfiguration
    {
        public static IServiceCollection ConfigureRepositoryCollection(this IServiceCollection service)
        {
            //service.AddTransient<ICustomerRepository, CustomerRepository>();
            //service.AddTransient<IOrderRepository, OrderRepository>();
            //service.AddTransient<IDataRepository, DataRepository>();
            //service.AddTransient<IDataWarehouseRepository, DataWarehouseRepository>();
            //service.AddScoped<ITestingRepository, TestingRepository>();
            //service.AddScoped<IPatientRepository, PatientRepository>();

            //FirstDB
            service.AddScoped<IFirstDbOrderRepository, FirstDbOrderRepository>();
            service.AddScoped<IFirstDbCustomerRepository, FirstDbCustomerRepository>();
            service.AddScoped<IFirstDbLocationRepository, FirstDbLocationRepository>();
            service.AddScoped<IFirstDbVendorRepository, FirstDbVendorRepository>();
            service.AddScoped<IFirstDbItemRepository, FirstDbItemRepository>();

            //FirstDB
            service.AddScoped<ISecondDbOrderRepository, SecondDbOrderRepository>();

            //GlobalDb
            service.AddScoped<IGlobalDbVendorRepository, GlobalDbVendorRepository>();
            service.AddScoped<IGlobalDbOrderRepository, GlobalDbOrderRepository>();

            return service;
        }
    }
}
