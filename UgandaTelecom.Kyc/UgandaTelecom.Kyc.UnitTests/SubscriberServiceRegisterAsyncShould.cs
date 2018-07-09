using Microsoft.Extensions.Options;
using Moq;
using Shouldly;
using System;
using System.Data.SqlClient;
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
            var mockOptions = new Mock<IOptions<ConnectionStringsAppSettings>>();
            mockOptions.Setup(m => m.Value.DefaultConnection).Returns("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SimRegistration;Integrated Security=True;MultipleActiveResultSets=true");
            var sqlDatabaseServerMock = new SqlDatabaseServer(mockOptions.Object);
            var testService = new SubscriberService(sqlDatabaseServerMock);
            var testSubscriber = new Subscriber
            {
                AgentMsisdn = "711187744",
                DateOfBirth = DateTime.Now,
                District = "Mukono",
                GivenName = "George",
                IdentificationNumber = "CM901239012380",
                IdentificationType = "NIN",
                Mode = "APP",
                Msisdn = "711187744",
                OtherNames = "Joseph",
                Surname = "Serunjogi",
                VerificationRequest = "RETURNED",
                Verified = true,
                Village = "Kisowera"
            };

            // When
            var result = await testService.RegisterAsync(testSubscriber);

            // Then
            result.ShouldBeOfType<OperationResult>();
        }
    }
}
