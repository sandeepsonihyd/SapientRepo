using ProductCatalog.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.Repositories
{
    public interface IGenericRepository<T> where T:class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> Add(T item);
        Task Update(T item);
        Task Delete(int id);
    }
    public interface ICatalogItemRepository : IGenericRepository<CatalogItem>
    {

    }

    public interface ICatalogBrandRepository : IGenericRepository<CatalogBrand>
    {

    }

    public interface ICatalogTypeRepository : IGenericRepository<CatalogType>
    {

    }
}
