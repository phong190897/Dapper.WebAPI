using Dapper.Application.IRepositories;
using Dapper.Application.IRepositories.IRepositories;

namespace Dapper.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IProductRepository products)
        {
            Products = products;
        }

        public IProductRepository Products { get; }
    }
}
