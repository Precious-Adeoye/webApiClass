using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webApiClass.Iservice;

namespace webApiClass.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerifyNumberController : ControllerBase
    {
        private readonly INumberCheckService _numberCheckService;

        public VerifyNumberController(INumberCheckService numberCheckService)
        {
            _numberCheckService = numberCheckService;
        }

        [HttpGet("GetNumberDetails")]
        public async Task<IActionResult> GetNumberDetails([FromQuery]string number)
        {
                var details = await _numberCheckService.GetCountryNumber(number);
                return Ok(Response);
           
        }
    }
}
