using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Data.Entity;
using DatingApp.API.DTO;
using DatingApp.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{ 
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            this._authService = authService;
            
        }
        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegisterDTO user)
        {
            try
            {
                string token=_authService.Register(user);
                return Ok(token);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserDTO user)
        {
            try
            {
                string token=_authService.Login(user);
                return Ok(token);
            }
            catch (BadHttpRequestException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
        
    }
}