using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UgandaTelecom.Kyc.Core.Common.OperationResults;

namespace UgandaTelecom.Kyc.Core.Services.Msente
{
    public interface IMsenteService
    {
        Task<TaskOperationResult> GetAuthenticationTokenAsync(MsenteCredential msenteCredential);
    }
}
