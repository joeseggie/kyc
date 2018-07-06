using Moq;
using Shouldly;
using System;
using System.Threading.Tasks;
using UgandaTelecom.Kyc.Core.Common;
using UgandaTelecom.Kyc.Core.Data;
using UgandaTelecom.Kyc.Core.Models;
using UgandaTelecom.Kyc.Core.Services;
using Xunit;

namespace UgandaTelecom.Kyc.UnitTests
{
    public class SubscriberServiceRegisterAsyncShould
    {
        [Fact, Trait("Service", "Subscriber")]
        public async Task ReturnOperationResult()
        {
            // Given
            var sqlDatabaseServerMock = new Mock<ISqlDatabaseServer>();
            var testService = new SubscriberService(sqlDatabaseServerMock.Object);
            var subscriberMock = new Mock<Subscriber>();
            
            // When
            var result = await testService.RegisterAsync(subscriberMock.Object);

            // Then
            result.ShouldBeOfType<OperationResult>();
        }
    }
}
