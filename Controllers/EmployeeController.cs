using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_With_JWT.JWTConfiguration;
using WebAPI_With_JWT.Models;

namespace WebAPI_With_JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IJwtAuthenticationManager jwtAuthenticationManager;

        //public EmployeeController(IOptionsMonitor<JwtConfig> jwtConfig)
        //{
        //    jwtAuthenticationManager = new JwtAuthenticationManager(jwtConfig.CurrentValue);
        //}
        public EmployeeController(IJwtAuthenticationManager jwtAuthenticationManager)
        {
            this.jwtAuthenticationManager = jwtAuthenticationManager;

        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] EmployeeCred crendentials)
        {
           var token =  jwtAuthenticationManager.Authenticate(crendentials);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
            
        }
    }
}
