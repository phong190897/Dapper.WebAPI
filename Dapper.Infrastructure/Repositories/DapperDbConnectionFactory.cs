using Dapper.Application.IRepositories;
using Dapper.Core.Enumerations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Dapper.Infrastructure.Repositories
{
    public class DapperDbConnectionFactory : IDbConnectionFactory
    {
        private readonly IDictionary<ConnectionType, string> _connectionDict;

        public DapperDbConnectionFactory(IDictionary<ConnectionType, string> connectionDict)
        {
            _connectionDict = connectionDict;
        }

        public IDbConnection CreateDbConnection(ConnectionType connectionName)
        {
            string connectionString = null;
            if (_connectionDict.TryGetValue(connectionName, out connectionString))
            {
                return new SqlConnection(connectionString);
            }

            throw new ArgumentNullException();
        }
    }
}
