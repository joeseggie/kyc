using System;
using System.Collections.Generic;
using System.Text;

namespace UgandaTelecom.Kyc.Core.Common.OperationResults
{
    public class MsenteTokenRequestResult
    {
        public virtual string ReferenceId { get; set; }
        public virtual object ResponseResult { get; set; }
    }
}
