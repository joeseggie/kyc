using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace UgandaTelecom.Kyc.Core.Data
{
    public class SqlDatabaseServer : ISqlDatabaseServer
    {
        private SqlConnection _connection;

        public SqlDatabaseServer(IOptions<ConnectionStringsAppSettings> connectionStringAppSettings)
        {
            CreateConnection(connectionStringAppSettings);
        }

        public DbConnection Connection => _connection;

        public void CreateConnection(IOptions<ConnectionStringsAppSettings> connectionStringAppSettings)
        {
            _connection = new SqlConnection(connectionStringAppSettings.Value.DefaultConnection);
        }
    }
}
