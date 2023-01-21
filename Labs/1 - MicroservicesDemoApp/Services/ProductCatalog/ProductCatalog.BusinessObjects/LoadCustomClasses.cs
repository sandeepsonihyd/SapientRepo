using Microsoft.Extensions.DependencyInjection;
using ProductCatalog.EFRepositories;
using ProductCatalog.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductCatalog.BusinessObjects
{
    public class LoadCustomServices
    {
        public static void Initialize(IServiceCollection services)
        {
            services.AddTransient<ICatalogItemRepository, CatalogItemRepository>();
        }
    }

}
