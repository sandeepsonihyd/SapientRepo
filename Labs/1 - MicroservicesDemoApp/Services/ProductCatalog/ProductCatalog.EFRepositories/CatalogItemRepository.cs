using Microsoft.EntityFrameworkCore;
using ProductCatalog.Domain;
using ProductCatalog.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.EFRepositories
{
    public class CatalogItemRepository : GenericRepository<CatalogItem>, ICatalogItemRepository
    {
        private readonly ProductCatalogContext _context;

        public CatalogItemRepository(ProductCatalogContext context) : base (context)
        {
            _context = context;
        }

        public async override Task<IEnumerable<CatalogItem>> GetAll()
        {
            return await _context.CatalogItems.Include("CatalogType").Include("CatalogBrand").ToListAsync();
        }

        public async override Task<CatalogItem> GetById(int id)
        {
            return await _context.CatalogItems.Include("CatalogType").Include("CatalogBrand").Where(item1 => item1.Id == id).FirstOrDefaultAsync<CatalogItem>();
        }
    }
}
