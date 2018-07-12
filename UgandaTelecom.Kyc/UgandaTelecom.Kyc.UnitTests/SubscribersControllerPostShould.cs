﻿using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UgandaTelecom.Kyc.Api.Controllers;
using UgandaTelecom.Kyc.Core.Common;
using UgandaTelecom.Kyc.Core.Models;
using UgandaTelecom.Kyc.Core.Services;
using Xunit;

namespace UgandaTelecom.Kyc.UnitTests
{
    public class SubscribersControllerPostShould
    {
        [Fact, Trait("Feature", "Subscriber registration"), Trait("Controller", "SubscribersController")]
        public async Task CallSubscriberServiceRegisterAsync()
        {
            // Given
            var mock = new Mock<ISubscriberService>();
            var testController = new SubscribersController(mock.Object);
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
            mock.Setup(m => m.RegisterAsync(testSubscriber)).Returns(Task.FromResult(new OperationResult { Success = true, Message = "711187734" }));

            // When
            await testController.Post(testSubscriber);

            // Then
            mock.Verify(m => m.RegisterAsync(testSubscriber), Times.AtMostOnce());
        }
    }
}