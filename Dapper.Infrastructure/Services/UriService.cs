using Dapper.Application.IServices;
using Dapper.Core.QueryFilters;
using System;

namespace Dapper.Infrastructure.Services
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;

        public UriService(string baseUri)
        {
            this._baseUri = baseUri;
        }

        public Uri GetProductPaginationUri(ProductQueryFilter filter, string actionUrl)
        {
            string baseUrl = $"{_baseUri}{actionUrl}";
            return new Uri(baseUrl);
        }
    }
}
