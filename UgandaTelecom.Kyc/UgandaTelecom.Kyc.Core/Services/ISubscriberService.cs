using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UgandaTelecom.Kyc.Core.Common;
using UgandaTelecom.Kyc.Core.Models;

namespace UgandaTelecom.Kyc.Core.Services
{
    public interface ISubscriberService
    {
        Task<OperationResult> RegisterAsync(Subscriber subscriber);
        Task<OperationResult> UpdateAsync(Subscriber subscriber);
        Task<IEnumerable<Subscriber>> SearchAsync(string keyword);
        Task<OperationResult> ArchiveAsync(string msisdn);
        Task<Subscriber> GetAsync(string msisdn);
    }
}
