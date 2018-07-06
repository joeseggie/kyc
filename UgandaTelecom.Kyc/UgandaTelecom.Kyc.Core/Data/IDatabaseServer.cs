using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace UgandaTelecom.Kyc.Core.Data
{
    public interface IDatabaseServer
    {
        DbConnection Connection { get; set; }
        void CreateConnection(IOptions<ConnectionStringsAppSettings> connectionStringAppSettings);
    }
}
