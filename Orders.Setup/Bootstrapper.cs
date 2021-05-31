using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Orders.Setup
{
    public static class Bootstrapper
    {
        public static void ConfigureServices
            (IServiceCollection services)
        {

            services.AddMediatR(
                 System.Reflection.Assembly.Load("Orders.Application")
            );
            
            services.AddScoped<Domain.IClientRepository, Repository.Redis.ClientRepository>();
            services.AddScoped<Domain.IProductRepository, Repository.Redis.ProductRepository>();
            services.AddScoped<Domain.IOrderRepository,  Repository.Redis.OrderRepository>();
            services.AddScoped<Domain.IOrderQuery, Repository.Redis.OrderRepository>();
            services.AddScoped<Application.Commands.Create.OrderCreateValidator>();
        }
    }
}
