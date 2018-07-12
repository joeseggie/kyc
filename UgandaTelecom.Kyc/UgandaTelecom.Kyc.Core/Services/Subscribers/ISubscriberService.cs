using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UgandaTelecom.Kyc.Core.Common;
using UgandaTelecom.Kyc.Core.Common.OperationResults;
using UgandaTelecom.Kyc.Core.Models;

namespace UgandaTelecom.Kyc.Core.Services.Subscribers
{
    public interface ISubscriberService
    {
        Task<TaskOperationResult> RegisterAsync(Subscriber subscriber);
        Task<TaskOperationResult> UpdateAsync(Subscriber subscriber);
        Task<IEnumerable<Subscriber>> SearchAsync(string keyword);
        Task<TaskOperationResult> ArchiveAsync(string msisdn);
        Task<Subscriber> GetAsync(string msisdn);
        Task<bool> ValidateMsidnAsync(string msisdn);
    }
}
