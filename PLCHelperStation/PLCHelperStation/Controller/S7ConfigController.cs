using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PLCHelperStation.DB;
using PLCHelperStation.Migrations.TestDb;
using PLCHelperStation.ResultObj;
using PLCHelperStation.Modbel;
using S7Config = PLCHelperStation.Modbel.S7Config;

namespace PLCHelperStation.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class S7ConfigController : ControllerBase
    {
        protected readonly ILogger<S7ConfigController> _logger;

        public S7ConfigController(ILogger<S7ConfigController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 添加S7配置
        /// </summary>
        /// <param name="s7Config"></param>
        /// <returns></returns>
        [HttpPost("AddS7Confug")]
        [EnableCors("AllowSpecificOrigins")] // 应用 CORS 策略
        public Result AddS7Confug(S7Config s7Config)
        {
            try
            {
                using (var ctx = new TestDbContext())
                {
                    //保存前检查配置名是否存在
                    var existConfigName = ctx.S7Configs.Any(x =>x.S7Name == s7Config.S7Name);
                    //保存前检查配置是否存在
                    var existConfig = ctx.S7Configs.Any(x => x.IP == s7Config.IP && x.Port == s7Config.Port && x.CPUType == s7Config.CPUType && x.Slot == s7Config.Slot && x.Rack == s7Config.Rack);
                    if(existConfigName)
                    {
                        var resultNg = new Result { Code = 400, ResultType = false, Message = "该配置名已存在，配置名称不能重复！" };
                        _logger.LogError("添加S7配置，该配置名已存在，配置名称不能重复！");
                        return resultNg;
                    }
                    else if(existConfig)
                    {
                        var resultNg = new Result { Code = 400, ResultType = false, Message = "该配置已存在，不能重复！" };
                        _logger.LogError("添加S7配置，该配置已存在，不能重复！");
                        return resultNg;
                    }
                    else
                    {
                        ctx.S7Configs.Add(s7Config);
                        ctx.SaveChanges();
                        _logger.LogWarning("添加S7配置,添加成功！");
                        var result = new Result { Code = 200, ResultType = true, Message = "添加新配置成功！" };
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"添加S7配置，添加S7配置名为{s7Config.S7Name}的过程中系统发生错误，错误信息为：{ex.Message}");
                return new Result { Code = 400, ResultType = false, Message = ex.Message };
            }
        }
    }
}
