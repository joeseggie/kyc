using System;
using System.Collections.Generic;
using System.Text;

namespace UgandaTelecom.Kyc.Core.Data
{
    public class MsenteAppSettings
    {
        public virtual MsenteApiConfiguration CredentialsApi { get; set; }
        public virtual MsenteApiConfiguration RegisterApi { get; set; }
        public virtual MsenteApiConfiguration ValidateNumberApi { get; set; }
    }
}
