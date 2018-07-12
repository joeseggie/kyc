using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UgandaTelecom.Kyc.Core.Common.OperationResults;
using UgandaTelecom.Kyc.Core.Services.Msente;

namespace UgandaTelecom.Kyc.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Msente")]
    public class MsenteController : Controller
    {
        private IMsenteService _msenteService;

        public MsenteController(IMsenteService msenteService)
        {
            _msenteService = msenteService;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]MsenteCredential msenteCredential)
        {
            var tokenRequestResponse = await _msenteService.GetAuthenticationTokenAsync(msenteCredential);
            return Ok(tokenRequestResponse);
        }
    }
}