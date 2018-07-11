using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UgandaTelecom.Kyc.Core.Models;
using UgandaTelecom.Kyc.Core.Services;

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
        public async Task<IActionResult> Post([FromBody]Subscriber subscriber)
        {
            var operationResult = await _subscriberService.RegisterAsync(subscriber);
            return Ok(operationResult);
        }
    }
}