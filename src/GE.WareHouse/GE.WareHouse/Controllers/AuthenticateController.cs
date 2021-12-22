using GE.WareHouse.Helpers;
using GE.WareHouse.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GE.WareHouse.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticateController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        public AuthenticateController(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(AuthenticateRequest model)
        {
            var ado = new UserModelADO(_appSettings);
            var response = ado.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }
    }
}
