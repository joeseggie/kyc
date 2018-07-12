using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UgandaTelecom.Kyc.Core.Common.OperationResults;
using UgandaTelecom.Kyc.Core.Data;
using UgandaTelecom.Kyc.Core.Services;
using UgandaTelecom.Kyc.Core.Services.Msente;
using Xunit;

namespace UgandaTelecom.Kyc.IntegrationTests
{
    public class MsenteServiceGetAuthenticationTokenAsyncShould
    {
        [Fact, Trait("Service", "Msente"), Trait("Feature", "MsenteToken")]
        public async Task ReturnTaskOperationResult()
        {
            // Given
            var mockOptions = new Mock<IOptions<MsenteAppSettings>>();
            mockOptions.Setup(m => m.Value.CredentialsApi).Returns(new MsenteApiConfiguration
            {
                Url = "http://172.18.3.19:9081/SimRegistrationService/authentication/credentials",
                HttpMethod = "POST"
            });
            var credentials = new MsenteCredential
            {
                username = "reg_kyc321",
                password = "9c7l8t"
            };
            var body = JsonConvert.SerializeObject(credentials);
            var mockUrlHandlerService = new Mock<IUrlHandlerService>();
            mockUrlHandlerService.Setup(m => m.ProcessPOSTRequestAsync(mockOptions.Object.Value.CredentialsApi.Url, body))
                .Returns(Task.FromResult("{\"Referenceid\":\"120180712000071\",\"ResponseResult\":{\"token\":\"eyJraWQiOiIxIiwiYWxnIjoiUlMyNTYifQ.eyJpc3MiOiJ1dGwiLCJleHAiOjE1MzEzOTI3MTYsImp0aSI6Im1OWm1ldmtib3dGYjdraXZDbEhmdlEiLCJpYXQiOjE1MzEzOTA5MTYsIm5iZiI6MTUzMTM5MDc5Niwic3ViIjoicmVnX2t5YzMyMSIsInJvbGVzIjoiVVNFUiJ9.LodPqoBEb6XTdE7tgAj66Il_Tt6DgKLN7yIQnV4q2ChHpPkx79wbfjOo6uufpfr2a0c5AP3xjdmDKQnSGZxqs3MoP-iDBfDX4FvH7oh_Q4lpa3bafC4UFn5w4f_V54AyqM3PzVjgsr5bEDvfIp66Wkxg4043slHEX9teVlJrv9DDxibllGiQ9Mue3mb5b2Isxe3Qs-G6GjRHZerqaGbwgwELWa29Z47PIpVneKuoLAbdDD5dDzImVOCGA5gV-NCYiaFZdAty2rdlAvBmEwDo6onR31xQrepreOmpORVPJvNGlP-gdpSIaM1fn3aaMepjNt-RUfotOWfQs4LRynLwLg\"}}"));
            var testService = new MsenteService(mockOptions.Object, mockUrlHandlerService.Object);

            // When
            var result = await testService.GetAuthenticationTokenAsync(credentials);

            // Assert
            result.ShouldBeOfType<TaskOperationResult>();
        }
    }
}
