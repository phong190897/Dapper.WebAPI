using Dapper.Core.Enumerations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Dapper.Application.IRepositories
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateDbConnection(ConnectionType connectionName);
    }
}
