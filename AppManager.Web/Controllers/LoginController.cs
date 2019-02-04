using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppManager.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AppManager.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        readonly IConfiguration config;

        public LoginController(IConfiguration config)
        {
            this.config = config;
        }

        [HttpPost]
        public IActionResult Post(LoginModel model)
        {
            if(model.Email == config["Email"] && model.Password == config["Password"])
            {
                return Ok(Guid.NewGuid());
            }

            return Unauthorized();
        }
    }
}