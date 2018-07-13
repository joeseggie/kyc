using Moq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using UgandaTelecom.Kyc.Core.Data;
using UgandaTelecom.Kyc.Core.Services.Subscribers;
using Xunit;

namespace UgandaTelecom.Kyc.UnitTests
{
    public class SubscribersServiceGetAsyncShould
    {
        [Fact, Trait("Service", "SubscriberService"), Trait("Feature", "GetSubscriber")]
        public async Task CallSqlDatabaseServerConnectionProperty()
        {
            // Given
            var mockSqlDatabaseServer = new Mock<ISqlDatabaseServer>();
            mockSqlDatabaseServer.Setup(m => m.Connection).Returns(new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SimRegistration;Integrated Security=True;MultipleActiveResultSets=true"));
            var testService = new SubscriberService(mockSqlDatabaseServer.Object);
            var mockMsisdn = "711187734";

            // When
            await testService.GetAsync(mockMsisdn);

            // Then
            mockSqlDatabaseServer.Verify(m => m.Connection, Times.AtMostOnce());
        }
    }
}
