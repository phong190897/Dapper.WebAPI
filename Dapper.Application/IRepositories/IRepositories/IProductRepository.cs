using Dapper.Core.CustomEntities;
using Dapper.Core.Entities;
using Dapper.Core.QueryFilters;

namespace Dapper.Application.IRepositories.IRepositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        PagedList<Product> GetAllProductsWithPaging(ProductQueryFilter filter);
    }
}
