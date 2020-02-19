using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Helpers;

namespace BackendHost.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class RedeemController : Controller
    {
        private readonly Service.IService<Service.RedeemCodeRequest, Models.RedeemResult> _service;
        //
        private readonly ILogger<RedeemController> _logger;
        private IHttpContextAccessor _httpcontext;

        public RedeemController(Service.IService<Service.RedeemCodeRequest, Models.RedeemResult> service, IHttpContextAccessor httpcontext, ILogger<RedeemController> logger)
        {
            _service = service ?? throw new System.ArgumentNullException("service");
            _httpcontext = httpcontext;
            _logger = logger;
        }
        
        
        [HttpPost("{code}")]
        public async Task<IActionResult> Post(string code)
        {
            /*
            POST only
            Looks up code in MySql RDS database and then determines result
            Replies with JSON for success, already redeemed, invalid code
            When API method is called, write the attempt into a separate table that will have the data we need for final report
            */
            if (String.IsNullOrWhiteSpace(code)|| !Models.RedeemCode.IsValid(code))
            {
                var r = new Models.RedeemResult(Models.RedeemResultEnum.InvalidCode);
                return Ok(r);
            }

            try
            {
                var codedata = new Models.RedeemCode(code);
                var caller = new Models.CallerInfo()
                {
                    IP = _httpcontext?.HttpContext.Connection.RemoteIpAddress.ToString()??""
                    , UserAgent = _httpcontext?.HttpContext.Request.Headers["User-Agent"].ToString() ?? ""
                };
                var request = new Service.RedeemCodeRequest(codedata, caller);
                var result = await _service.Process(request).ConfigureAwait(false);
                return Ok(result);
            }
            catch(Exception ex)
            {
                var r = new Models.RedeemResult(ex);
                //return BadRequest(r);
                //  OR
                return StatusCode(StatusCodes.Status500InternalServerError, r);
            }
        }
    }
}