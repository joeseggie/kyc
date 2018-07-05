using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UgandaTelecom.Kyc.Core.Common;
using UgandaTelecom.Kyc.Core.Models;

namespace UgandaTelecom.Kyc.Core.Services
{
    /// <summary>
    /// Subscriber handling service.
    /// </summary>
    public class SubscriberService : ISubscriberService
    {
        /// <summary>
        /// Archive subscriber service.
        /// </summary>
        /// <param name="msisdn">Subscriber MSISDN</param>
        /// <returns>Result from operation indicating whether it was a success.</returns>
        public Task<OperationResult> ArchiveAsync(string msisdn)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get subscriber given their MSISDN.
        /// </summary>
        /// <param name="msisdn">Subscriber MSISDN.</param>
        /// <returns>Subscriber details.</returns>
        public Task<Subscriber> GetAsync(string msisdn)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Register subscriber.
        /// </summary>
        /// <param name="subscriber">Subscriber to be registered.</param>
        /// <returns>Operation result indicating if it was a success.</returns>
        public Task<OperationResult> RegisterAsync(Subscriber subscriber)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Search subscribers registrations given a keyword(s)
        /// </summary>
        /// <param name="keyword">Search keyword(s)</param>
        /// <returns>List of subscriber details that match the search keyword.</returns>
        public Task<IEnumerable<Subscriber>> SearchAsync(string keyword)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update subscriber details.
        /// </summary>
        /// <param name="subscriber">Subscriber details update.</param>
        /// <returns>Operation result indicating if it was a success or not.</returns>
        public Task<OperationResult> UpdateAsync(Subscriber subscriber)
        {
            throw new NotImplementedException();
        }
    }
}
