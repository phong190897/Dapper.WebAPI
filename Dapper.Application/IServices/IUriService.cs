using Dapper.Core.QueryFilters;
using System;

namespace Dapper.Application.IServices
{
    public interface IUriService
    {
        Uri GetProductPaginationUri(ProductQueryFilter filter, string actionUrl);
    }
}