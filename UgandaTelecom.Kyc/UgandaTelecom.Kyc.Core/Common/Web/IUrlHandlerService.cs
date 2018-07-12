using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UgandaTelecom.Kyc.Core.Services
{
    public interface IUrlHandlerService
    {
        Task<string> ProcessPOSTRequestAsync(string url, string body, string bearerToken);
        Task<string> ProcessPOSTRequestAsync(string url, string body);
        Task<string> ProcessGETRequestAsync();
    }
}
