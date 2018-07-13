using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using UgandaTelecom.Kyc.Core.Common.OperationResults;
using UgandaTelecom.Kyc.Core.Data;
using UgandaTelecom.Kyc.Core.Models;
using UgandaTelecom.Kyc.Core.Services.Subscribers;
using Xunit;

namespace UgandaTelecom.Kyc.UnitTests
{
    public class SubscriberServiceIsRegisteredAsyncShould
    {
        [Fact, Trait("Service", "SubscriberService"), Trait("Feature", "RegistrationCheck")]
        public async Task CallsSubscriberServiceGetAsyncMethod()
        {
            // Given
            var mockService = new Mock<ISubscriberService>();
            var mockMsisdn = "711187734";

            // When
            await mockService.Object.IsRegisteredAsync(mockMsisdn);

            // Then
            mockService.Verify(m => m.GetAsync(mockMsisdn), Times.AtMostOnce());
        }

        [Fact, Trait("Service", "SubscriberService"), Trait("Feature", "RegistrationCheck")]
        public async Task ReturnsTaskOperationResult()
        {
            // Given
            var mockISqlDatabaseServer = new Mock<ISqlDatabaseServer>();
            mockISqlDatabaseServer.Setup(m => m.Connection).Returns(new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SimRegistration;Integrated Security=True;MultipleActiveResultSets=true"));
            var mockMsisdn = "711187734";
            var testService = new SubscriberService(mockISqlDatabaseServer.Object);

            // When
            var result = await testService.IsRegisteredAsync(mockMsisdn);

            // Then
            result.ShouldBeOfType<TaskOperationResult>();
        }
    }
}
