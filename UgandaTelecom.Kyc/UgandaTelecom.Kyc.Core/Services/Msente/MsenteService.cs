using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UgandaTelecom.Kyc.Core.Common.OperationResults;
using UgandaTelecom.Kyc.Core.Data;

namespace UgandaTelecom.Kyc.Core.Services.Msente
{
    public class MsenteService : IMsenteService
    {
        private readonly IOptions<MsenteAppSettings> _msenteAppSettings;
        private readonly IUrlHandlerService _urlHandlerService;

        public MsenteService(IOptions<MsenteAppSettings> msenteAppSettings, IUrlHandlerService urlHandlerService)
        {
            _msenteAppSettings = msenteAppSettings;
            _urlHandlerService = urlHandlerService;
        }

        /// <summary>
        /// Get the MSente authentication token.
        /// </summary>
        /// <param name="msenteCredential">MSente credentials</param>
        /// <returns>Operation success result and the response from the task.</returns>
        public async Task<TaskOperationResult> GetAuthenticationTokenAsync(MsenteCredential msenteCredential)
        {
            try
            {
                var body = JsonConvert.SerializeObject(msenteCredential);
                var result = await _urlHandlerService.ProcessPOSTRequestAsync(_msenteAppSettings.Value.CredentialsApi.Url, body);
                return new TaskOperationResult { Success = true, Message = result };
            }
            catch (ApplicationException error)
            {
                return new TaskOperationResult { Success = false, Message = error.Message };
            }
        }
    }
}
