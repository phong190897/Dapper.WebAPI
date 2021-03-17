using Dapper.Application.IRepositories;
using Dapper.Application.IRepositories.IRepositories;
using Dapper.Application.Persistences;
using Dapper.Core.CustomEntities;
using Dapper.Core.Entities;
using Dapper.Core.QueryFilters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dapper.Infrastructure.Repositories.Repositories
{
    public class ProductRepository : DbConnection1RepositoryBase, IProductRepository
    {
        private readonly PaginationOption _paginationOption;
        public ProductRepository(IDbConnectionFactory dbConnectionFactory, IOptions<PaginationOption> options) : base(dbConnectionFactory) 
        {
            this._paginationOption = options.Value;
        }
        public async Task<int> AddAsync(Product entity)
        {
            entity.AddedOn = DateTime.Now;
            var sql = "Insert into Products (Name,Description,Barcode,Rate,AddedOn) VALUES (@Name,@Description,@Barcode,@Rate,@AddedOn)";
            var result = await base.DbConnection.ExecuteAsync(sql, entity);

            return result;
        }
        public async Task<int> DeleteAsync(int id)
        {
            var sql = "DELETE FROM Products WHERE Id = @Id";
            var result = await base.DbConnection.ExecuteAsync(sql, new { Id = id });
            return result;
        }

        public async Task<int> DeleteAsync(string id)
        {
            var sql = "DELETE FROM Products WHERE Id = @Id";
            var result = await base.DbConnection.ExecuteAsync(sql, new { Id = id });
            return result;
        }

        public async Task<IReadOnlyList<Product>> GetAllAsync()
        {
            var sql = "SELECT * FROM Products";
            var result = await base.DbConnection.QueryAsync<Product>(sql);
            return result.ToList();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Products WHERE Id = @Id";
            var result = await base.DbConnection.QuerySingleOrDefaultAsync<Product>(sql, new { Id = id });
            return result;
        }

        public async Task<Product> GetByIdAsync(string id)
        {
            var sql = "SELECT * FROM Products WHERE Id = @Id";
            var result = await base.DbConnection.QuerySingleOrDefaultAsync<Product>(sql, new { Id = id });
            return result;
        }

        public async Task<int> UpdateAsync(Product entity)
        {
            entity.ModifiedOn = DateTime.Now;
            var sql = "UPDATE Products SET Name = @Name, Description = @Description, Barcode = @Barcode, Rate = @Rate, ModifiedOn = @ModifiedOn  WHERE Id = @Id";
            var result = await base.DbConnection.ExecuteAsync(sql, entity);
            return result;
        }

        public PagedList<Product> GetAllProductsWithPaging(ProductQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOption.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOption.DefaultPageSize : filter.PageSize;

            var sql = "SELECT * FROM Products";
            var result =  base.DbConnection.Query<Product>(sql);

            var pagedProducts = PagedList<Product>.Create(result, filter.PageNumber, filter.PageSize);

            return pagedProducts;
        }
    }
}
