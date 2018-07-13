using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UgandaTelecom.Kyc.Core.Models;
using UgandaTelecom.Kyc.Core.Services;
using UgandaTelecom.Kyc.Core.Services.Subscribers;

namespace UgandaTelecom.Kyc.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Subscribers")]
    public class SubscribersController : Controller
    {
        private readonly ISubscriberService _subscriberService;

        public SubscribersController(ISubscriberService subscriberService)
        {
            _subscriberService = subscriberService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]Subscriber subscriber)
        {
            var operationResult = await _subscriberService.RegisterAsync(subscriber);
            return Ok(operationResult);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody]Subscriber subscriber)
        {
            var operationResult = await _subscriberService.UpdateAsync(subscriber);
            return Ok(operationResult);
        }

        [HttpGet("validatemsisdn/{msisdn}")]
        public async Task<IActionResult> ValidateMsisdn([FromRoute]string msisdn)
        {
            var operationResult = await _subscriberService.ValidateMsidnAsync(msisdn);
            return Ok(operationResult);
        }
    }
}