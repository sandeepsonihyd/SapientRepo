using ProductCatalog.Domain;
using ProductCatalog.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductCatalog.EFRepositories
{
    class CatalogTypeRepository : GenericRepository<CatalogType>, ICatalogTypeRepository
    {
        private readonly ProductCatalogContext _context;

        public CatalogTypeRepository(ProductCatalogContext context) : base(context)
        {
            _context = context;
        }
    }
}
