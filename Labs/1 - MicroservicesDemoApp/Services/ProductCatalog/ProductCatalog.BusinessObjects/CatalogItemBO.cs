using ProductCatalog.Domain;
using ProductCatalog.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.BusinessObjects
{
    public class CatalogItemBO : ICatalogItemBO
    {
        private readonly ICatalogItemRepository _repo;

        public CatalogItemBO(ICatalogItemRepository repository)
        {
            _repo = repository;
        }
        public async Task<CatalogItem> Add(CatalogItem item)
        {
            return await _repo.Add(item);
            //Email campaign
        }

        public async Task Delete(int id)
        {
            await _repo.Delete(id);
        }

        public async Task<CatalogItem> GetCatalogItem(int id)
        {
            return await _repo.GetById(id);
        }

        public async Task<IEnumerable<CatalogItem>> GetCatalogItems()
        {
            return await _repo.GetAll();
        }

        public async Task Update(CatalogItem item)
        {
            await _repo.Update(item);
        }
    }
}
