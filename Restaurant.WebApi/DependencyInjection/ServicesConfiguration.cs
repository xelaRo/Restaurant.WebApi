﻿using Microsoft.Extensions.DependencyInjection;
using Restaurant.WebApi.Infrastructure.OracleDb;
using Restaurant.WebApi.Services;

namespace Restaurant.WebApi.DependencyInjection
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection ConfigureServiceCollection(this IServiceCollection service)
        {
            service.AddTransient<IRestaurantDbConnection, RestaurantDbConnection>();
            service.AddTransient<IUserService, UserService>();
            service.AddTransient<IOrderService, OrderService>();
            //service.AddTransient<ICountyService, CountyService>();
            //service.AddTransient<ISymptomService, SymptomService>();

            return service;
        }
    }
}