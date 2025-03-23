using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webApiClass.DTO;
using webApiClass.Iservice;

namespace webApiClass.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuth _auth;

        public AuthController(IAuth auth)
        {
            _auth = auth;
        }
        [HttpPost("CreateNewUser")]
        public async Task<IActionResult> CreateNewUser([FromBody]RegisterDTO registerDTO)
        {
            var response = await _auth.CreateUser(registerDTO);
            return Ok(response);
        }
        [HttpPost("LogInUser")]
        public async Task<IActionResult> LogInUser([FromBody] LoginDTO loginDTO)
        {
            var response = await _auth.LogInUser(loginDTO);
            return Ok(response);
        }
    }
}
