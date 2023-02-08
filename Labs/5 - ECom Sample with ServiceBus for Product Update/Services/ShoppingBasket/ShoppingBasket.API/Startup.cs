using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using ShoppingBasket.API.Repository;
using EventBus.Abstractions;
using EventBus.Implementations;
using ShoppingBasket.API.IntegrationEventHandlers;
using ShoppingBasket.IntegrationEvents;

namespace ShoppingBasket.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddAuthentication("Bearer")
             .AddIdentityServerAuthentication(options =>
             {
                 options.Authority = Configuration["IdentityServerUrl"];
                 options.RequireHttpsMetadata = false;
                 options.ApiName = "basket";
             });
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "Catalog API Demo",
                    Version = "v1",
                    Description = "The is a demo of how to use Swagger in Catalog API"
                });
            });

            services.AddSingleton<ConnectionMultiplexer>(sp =>
            {
                var configuration = ConfigurationOptions.Parse(Configuration["RedisConnectionString"], true);
                configuration.ResolveDns = true;
                configuration.AbortOnConnectFail = false;
                return ConnectionMultiplexer.Connect(configuration);
            });

            services.AddTransient<IBasketRepository, BasketRepository>();

            services.AddSingleton<IEventBus>(x =>
            {
                var bus = new AzureEventBus(Configuration["ServiceBusConnectionString"], "eshop-topic", "basket");
                ProductChangedIntegrationEventHandlers handler = new ProductChangedIntegrationEventHandlers(x.GetRequiredService<IBasketRepository>());
                bus.Subscribe<ProductPriceUpdatedIntegrationEvent, ProductChangedIntegrationEventHandlers>(handler);
                return bus;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger().UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            }); 
            
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
