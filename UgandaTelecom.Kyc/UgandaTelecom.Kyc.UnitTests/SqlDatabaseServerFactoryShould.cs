using Microsoft.Extensions.Options;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using UgandaTelecom.Kyc.Core.Data;
using Xunit;

namespace UgandaTelecom.Kyc.UnitTests
{
    public class SqlDatabaseServerFactoryShould
    {
        [Fact, Trait("Factory", "SqlServer")]
        public void CreateSqlConnectionToRegistrationDb()
        {
            // Given
            var appSettingsOptions = Options.Create(new ConnectionStringsAppSettings { DefaultConnection = "Data Source=172.25.0.5;Initial Catalog=SimRegistration;User Id=subscriber_base_user;Password=subscriber_base_user1234;" });
            var dbServer = new SqlDatabaseServer(appSettingsOptions);

            // Then
            dbServer.Connection.ConnectionString.ShouldBeSameAs("Data Source=172.25.0.5;Initial Catalog=SimRegistration;User Id=subscriber_base_user;Password=subscriber_base_user1234;");
        }
    }
}
