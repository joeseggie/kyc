using Microsoft.Extensions.Options;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UgandaTelecom.Kyc.Core.Common.OperationResults;
using UgandaTelecom.Kyc.Core.Data;
using UgandaTelecom.Kyc.Core.Services.Subscribers;
using Xunit;

namespace UgandaTelecom.Kyc.UnitTests
{
    public class SubscriberServiceValidateMsisdnAsyncShould
    {
        [Fact, Trait("Service", "SubscriberService"), Trait("Feature", "UTLNumber")]
        public async Task ReturnTrueWhenNumberIsValid()
        {
            // Given
            var mockOptions = new Mock<IOptions<ConnectionStringsAppSettings>>();
            mockOptions.Setup(m => m.Value.DefaultConnection).Returns("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SimRegistration;Integrated Security=True;MultipleActiveResultSets=true");
            var sqlDatabaseServerMock = new SqlDatabaseServer(mockOptions.Object);
            var testService = new SubscriberService(sqlDatabaseServerMock);

            // When
            var result = await testService.ValidateMsidnAsync("711187734");

            // Then
            result.Success.ShouldBeTrue();
        }

        [Fact, Trait("Service", "SubscriberService"), Trait("Feature", "UTLNumber")]
        public async Task ReturnFalseWhenNumberIsInValid()
        {
            // Given
            var mockOptions = new Mock<IOptions<ConnectionStringsAppSettings>>();
            mockOptions.Setup(m => m.Value.DefaultConnection).Returns("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SimRegistration;Integrated Security=True;MultipleActiveResultSets=true");
            var sqlDatabaseServerMock = new SqlDatabaseServer(mockOptions.Object);
            var testService = new SubscriberService(sqlDatabaseServerMock);

            // When
            var result = await testService.ValidateMsidnAsync("771187734");

            // Then
            result.Success.ShouldBeFalse();
        }

        [Fact, Trait("Service", "SubscriberService"), Trait("Feature", "UTLNumber")]
        public async Task ReturnTaskOperationResultObject()
        {
            // Given
            var mockOptions = new Mock<IOptions<ConnectionStringsAppSettings>>();
            mockOptions.Setup(m => m.Value.DefaultConnection).Returns("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SimRegistration;Integrated Security=True;MultipleActiveResultSets=true");
            var sqlDatabaseServerMock = new SqlDatabaseServer(mockOptions.Object);
            var testService = new SubscriberService(sqlDatabaseServerMock);

            // When
            var result = await testService.ValidateMsidnAsync("771187734");

            // Then
            result.ShouldBeOfType<TaskOperationResult>();
        }
    }
}
