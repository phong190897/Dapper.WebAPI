using Dapper.Application.IRepositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dapper.Application.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
    }
}
