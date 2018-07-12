using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UgandaTelecom.Kyc.Api.Controllers;
using UgandaTelecom.Kyc.Core.Common.OperationResults;
using UgandaTelecom.Kyc.Core.Services.Msente;
using Xunit;

namespace UgandaTelecom.Kyc.IntegrationTests
{
    public class MsenteControllerAuthenticateShould
    {
        [Fact, Trait("Controller", "MsenteController"), Trait("Feature", "Msente"), Trait("Feature", "MsenteToken")]
        public async Task CallSubscriberServiceUpdateAsync()
        {
            // Given
            var mock = new Mock<IMsenteService>();
            var testController = new MsenteController(mock.Object);
            var credential = new MsenteCredential
            {
                username = "male",
                password = "711187744"
            };
            mock.Setup(m => m.GetAuthenticationTokenAsync(credential)).Returns(Task.FromResult(new TaskOperationResult { Success = true, TaskResult = "{\"Referenceid\":\"120180712000071\",\"ResponseResult\": \"INVALID CREDENTIALS\" }" }));

            // When
            await testController.Authenticate(credential);

            // Then
            mock.Verify(m => m.GetAuthenticationTokenAsync(credential), Times.AtMostOnce());
        }
    }
}
