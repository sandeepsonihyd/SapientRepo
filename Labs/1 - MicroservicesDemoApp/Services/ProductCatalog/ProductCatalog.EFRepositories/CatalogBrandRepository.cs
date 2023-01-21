using ProductCatalog.Domain;
using ProductCatalog.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductCatalog.EFRepositories
{
    class CatalogBrandRepository : GenericRepository<CatalogBrand>, ICatalogBrandRepository
    {
        private readonly ProductCatalogContext _context;

        public CatalogBrandRepository(ProductCatalogContext context) : base(context)
        {
            _context = context;
        }
    }
}
