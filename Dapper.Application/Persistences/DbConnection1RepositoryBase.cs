using Dapper.Application.IRepositories;
using Dapper.Core.Enumerations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Dapper.Application.Persistences
{
    public abstract class DbConnection1RepositoryBase
    {
        public IDbConnection DbConnection { get; private set; }

        public DbConnection1RepositoryBase(IDbConnectionFactory dbConnectionFactory)
        {
            // Now it's the time to pick the right connection string!
            // Enum is used. No magic string!
            this.DbConnection = dbConnectionFactory.CreateDbConnection(ConnectionType.Connection1);
        }
    }
}
