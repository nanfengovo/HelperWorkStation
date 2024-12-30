using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PLCHeler.Controllers.Sys
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {


        [AllowAnonymous]
        [HttpPost]
        public string Login()
        {
            return "success";
        }

 

    }
}
