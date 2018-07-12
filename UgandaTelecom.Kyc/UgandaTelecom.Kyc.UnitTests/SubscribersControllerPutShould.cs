using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UgandaTelecom.Kyc.Api.Controllers;
using UgandaTelecom.Kyc.Core.Common;
using UgandaTelecom.Kyc.Core.Common.OperationResults;
using UgandaTelecom.Kyc.Core.Models;
using UgandaTelecom.Kyc.Core.Services;
using UgandaTelecom.Kyc.Core.Services.Subscribers;
using Xunit;

namespace UgandaTelecom.Kyc.UnitTests
{
    public class SubscribersControllerPutShould
    {
        [Fact, Trait("Controller", "SubscribersController"), Trait("Feature", "SubscriberUpdate")]
        public async Task CallSubscriberServiceUpdateAsync()
        {
            // Given
            var mock = new Mock<ISubscriberService>();
            var testController = new SubscribersController(mock.Object);
            var subscriber = new Subscriber
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
            mock.Setup(m => m.UpdateAsync(subscriber)).Returns(Task.FromResult(new TaskOperationResult { Success = true, Message = "711187734" }));

            // When
            await testController.Put(subscriber);

            // Then
            mock.Verify(m => m.UpdateAsync(subscriber), Times.AtMostOnce());
        }
    }
}
