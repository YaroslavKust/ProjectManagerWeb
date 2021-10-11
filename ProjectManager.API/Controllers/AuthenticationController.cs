using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using ProjectManager.API.Services.Authentication;
using ProjectManager.Entities.DTO;
using ProjectManager.Entities.Models;

namespace ProjectManager.API.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IAuthenticationManager _authManager;

        public AuthenticationController(IAuthenticationManager authManager)
        {
            _authManager = authManager;
        }


        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] UserForAuth user)
        {
            if (await _authManager.ValidateUser(user))
            {
                return Ok(new { Token = _authManager.GenerateToken() });
            }

            return Unauthorized();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync([FromBody] UserForAuth user)
        {
            if (await _authManager.CreateUser(user))
            {
                return StatusCode(201);
            }

            return BadRequest();
        }
    }
}
