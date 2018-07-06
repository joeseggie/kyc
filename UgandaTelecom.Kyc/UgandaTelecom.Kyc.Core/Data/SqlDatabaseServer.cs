using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace UgandaTelecom.Kyc.Core.Data
{
    public class SqlDatabaseServer : DatabaseServer, ISqlDatabaseServer
    {
        public SqlDatabaseServer(IOptions<ConnectionStringsAppSettings> connectionStringAppSettings) : base(connectionStringAppSettings)
        {
        }

        public override void CreateConnection(IOptions<ConnectionStringsAppSettings> connectionStringAppSettings)
        {
            Connection = new SqlConnection(connectionStringAppSettings.Value.DefaultConnection);
        }
    }
}
