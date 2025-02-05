using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PLCHelperStation.DB;
using PLCHelperStation.Modbel;
using PLCHelperStation.ResultObj;

namespace PLCHelperStation.Controller
{
    [ApiController]
    [Route("api/[controller]/")]
    public class AdminController : ControllerBase
    {
        //private UserManager<AppUser> _userManager;
        //public AdminController(UserManager<AppUser> userManager)
        //{
        //    _userManager = userManager;
        //}

        protected readonly ILogger<AdminController> _logger;

        public AdminController(ILogger<AdminController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [EnableCors("AllowSpecificOrigins")] // 应用 CORS 策略
        [HttpPost("Login")]
        public Result Login( User user)
        {
            using (var ctx = new TestDbContext())
            {
                var userinfo = ctx.Users.Any(x => x.Name == user.Name && x.Password == user.Password);
                if(userinfo)
                {
                    var resultOk = new Result { Code = 200, ResultType = true, Message = "登录成功！" };
                    _logger.LogInformation("用户登录操作，登录成功！");
                    return resultOk;
                }
                var resultNg = new Result { Code = 400, ResultType = false, Message = "登录失败！用户名或密码错误！！" };
                _logger.LogWarning("用户登录操作，登录失败！用户名或密码错误！！！");
                return resultNg;
            }
        }

        /// <summary>
        /// 添加用户(注册)
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>

        [EnableCors("AllowSpecificOrigins")] // 应用 CORS 策略
        [HttpPost("AddUser")]
        public Result AddUser(User user)
        {
            //if (ModelState.IsValid)
            //{
            //    AppUser appUser = new AppUser()
            //    {
            //        UserName = user.Name,
            //        PasswordHash = user.Password
            //    };
            //    var result = await _userManager.CreateAsync(appUser, user.Password);
            //    return result.Succeeded;
            //}
            //return false;

            using (var ctx = new TestDbContext())
            {
                var userinfo = ctx.Users.Any(x => x.Name == user.Name && x.Password == user.Password);
                if (!userinfo)
                {
                    ctx.Users.Add(user);
                    ctx.SaveChanges();
                    var resultOk = new Result { Code = 200, ResultType = true, Message = "添加用户成功！" };
                    _logger.LogInformation("添加用户操作，添加用户成功！");
                    return resultOk;
                }
                var resultNg = new Result { Code = 400, ResultType = false, Message = "要添加的用户已存在！" };
                _logger.LogWarning("添加用户操作，要添加的用户已存在！！");
                return resultNg;
            }

        }






    }
}
