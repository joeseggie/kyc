using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UgandaTelecom.Kyc.Api.Controllers;
using UgandaTelecom.Kyc.Core.Common.OperationResults;
using UgandaTelecom.Kyc.Core.Data;
using UgandaTelecom.Kyc.Core.Services.Subscribers;
using Xunit;

namespace UgandaTelecom.Kyc.UnitTests
{
    public class SubscribersControllerValidateMsisdnShould
    {
        [Fact, Trait("Controller", "SubscribersController"), Trait("Feature", "UTLNumber")]
        public async Task CallSubscriberServiceValidateMsisdnAsync()
        {
            // Given
            var mockSubscriberService = new Mock<ISubscriberService>();
            var testController = new SubscribersController(mockSubscriberService.Object);
            var mockMsisdn = "711187734";
            mockSubscriberService.Setup(m => m.ValidateMsidnAsync(mockMsisdn)).Returns(Task.FromResult(new TaskOperationResult { Success = true, TaskResult = mockMsisdn }));

            // When
            await testController.ValidateMsisdn(mockMsisdn);

            // Then
            mockSubscriberService.Verify(m => m.ValidateMsidnAsync(mockMsisdn), Times.AtMostOnce());
        }
    }
}
