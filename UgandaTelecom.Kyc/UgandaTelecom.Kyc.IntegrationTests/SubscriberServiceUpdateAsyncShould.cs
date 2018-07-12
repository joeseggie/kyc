using Microsoft.Extensions.Options;
using Moq;
using Shouldly;
using System;
using System.Threading.Tasks;
using UgandaTelecom.Kyc.Core.Common;
using UgandaTelecom.Kyc.Core.Common.OperationResults;
using UgandaTelecom.Kyc.Core.Data;
using UgandaTelecom.Kyc.Core.Models;
using UgandaTelecom.Kyc.Core.Services;
using UgandaTelecom.Kyc.Core.Services.Subscribers;
using Xunit;

namespace UgandaTelecom.Kyc.IntegrationTests
{
    public class SubscriberServiceUpdateAsyncShould
    {
        [Fact, Trait("Service", "Subscriber"), Trait("Feature", "SubscriberUpdate")]
        public async Task ReturnOperationResult()
        {
            // Given
            var mockOptions = new Mock<IOptions<ConnectionStringsAppSettings>>();
            mockOptions.Setup(m => m.Value.DefaultConnection).Returns("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SimRegistration;Integrated Security=True;MultipleActiveResultSets=true");
            var sqlDatabaseServerMock = new SqlDatabaseServer(mockOptions.Object);
            var testService = new SubscriberService(sqlDatabaseServerMock);
            var testSubscriber = new Subscriber
            {
                Gender = "male",
                Msisdn = "711187744",
                Village = "Kisowera",
                District = "Mukono",
                FaceImg = "samplefaceimage",
                IdFrontimg = "sampleidfrontimage",
                IdBackimg = "sampleidbackimage",
                VisaExpiry = DateTime.Now
            };

            // When
            var result = await testService.UpdateAsync(testSubscriber);

            // Then
            result.ShouldBeOfType<TaskOperationResult>();
        }
    }
}
