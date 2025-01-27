using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PLCHelperStation.Modbel;

namespace PLCHelperStation.Controller
{
    [ApiController]
    [Route("api/[controller]/")]
    public class AdminController : ControllerBase
    {
        private UserManager<AppUser> _userManager;
        public AdminController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }


        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        
        
        [HttpPost]
        public async Task<bool> AddUser(User user)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser()
                {
                    UserName = user.Name,
                    PasswordHash = user.Password
                };
                var result = await _userManager.CreateAsync(appUser, user.Password);
                return result.Succeeded;
            }
            return false;
        }




    }
}
