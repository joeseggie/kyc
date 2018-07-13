using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UgandaTelecom.Kyc.Api.Controllers;
using UgandaTelecom.Kyc.Core.Common.OperationResults;
using UgandaTelecom.Kyc.Core.Services.Subscribers;
using Xunit;

namespace UgandaTelecom.Kyc.UnitTests
{
    public class SubscribersControllerIsRegisteredShould
    {
        [Fact, Trait("Controller", "SubscribersController"), Trait("Feature", "RegistrationCheck")]
        public async Task CallSubscriberServiceValidateMsisdnAsync()
        {
            // Given
            var mockSubscriberService = new Mock<ISubscriberService>();
            var testController = new SubscribersController(mockSubscriberService.Object);
            var mockMsisdn = "711187734";
            mockSubscriberService.Setup(m => m.IsRegisteredAsync(mockMsisdn)).Returns(Task.FromResult(new TaskOperationResult { Success = true, TaskResult = mockMsisdn }));

            // When
            await testController.IsRegistered(mockMsisdn);

            // Then
            mockSubscriberService.Verify(m => m.IsRegisteredAsync(mockMsisdn), Times.AtMostOnce());
        }

        [Fact, Trait("Controller", "SubscribersController")]
        public async Task ReturnOkObjectResult()
        {
            // Given
            var mockSubscriberService = new Mock<ISubscriberService>();
            var testController = new SubscribersController(mockSubscriberService.Object);
            var mockMsisdn = "711187734";
            mockSubscriberService.Setup(m => m.IsRegisteredAsync(mockMsisdn)).Returns(Task.FromResult(new TaskOperationResult { Success = true, TaskResult = mockMsisdn }));

            // When
            var result = await testController.IsRegistered(mockMsisdn);

            // Then
            result.ShouldBeOfType<OkObjectResult>();
        }

        [Fact, Trait("Controller", "SubscribersController")]
        public async Task ReturnsOkObjectResultWithValueOfTypeTaskOperationResult()
        {
            // Given
            var mockSubscriberService = new Mock<ISubscriberService>();
            var testController = new SubscribersController(mockSubscriberService.Object);
            var mockMsisdn = "711187734";
            mockSubscriberService.Setup(m => m.IsRegisteredAsync(mockMsisdn)).Returns(Task.FromResult(new TaskOperationResult { Success = true, TaskResult = mockMsisdn }));

            // When
            var result = await testController.IsRegistered(mockMsisdn);

            // Then
            var okResult = result.ShouldBeOfType<OkObjectResult>();
            okResult.Value.ShouldBeOfType<TaskOperationResult>();
        }
    }
}
