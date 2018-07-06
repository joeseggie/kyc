using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace UgandaTelecom.Kyc.Core.Data
{
    public abstract class DatabaseServer : IDatabaseServer
    {
        public DatabaseServer(IOptions<ConnectionStringsAppSettings> connectionStringAppSettings)
        {
            CreateConnection(connectionStringAppSettings);
        }

        public DbConnection Connection { get; set; }

        public abstract void CreateConnection(IOptions<ConnectionStringsAppSettings> connectionStringAppSettings);
    }
}
