using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PLCHelperStation.DB;
using PLCHelperStation.Migrations.TestDb;
using PLCHelperStation.ResultObj;
using PLCHelperStation.Modbel;
using S7Config = PLCHelperStation.Modbel.S7Config;
using Microsoft.OpenApi.Expressions;

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
        [HttpPost("AddS7Config")]
        [EnableCors("AllowSpecificOrigins")] // 应用 CORS 策略
        public Result AddS7Config(S7Config s7Config)
        {
            try
            {
                using (var ctx = new TestDbContext())
                {
                    //保存前检查配置名是否存在
                    var existConfigName = ctx.S7Configs.Any(x => x.S7Name == s7Config.S7Name);
                    //保存前检查配置是否存在
                    var existConfig = ctx.S7Configs.Any(x => x.IP == s7Config.IP && x.Port == s7Config.Port && x.CPUType == s7Config.CPUType && x.Slot == s7Config.Slot && x.Rack == s7Config.Rack);
                    if (existConfigName)
                    {
                        var resultNg = new Result { Code = 400, ResultType = false, Message = "该配置名已存在，配置名称不能重复！" };
                        _logger.LogError("添加S7配置，该配置名已存在，配置名称不能重复！");
                        return resultNg;
                    }
                    else if (existConfig)
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



        /// <summary>
        /// 获取S7配置信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetS7Confug")]
        [EnableCors("AllowSpecificOrigins")] // 应用 CORS 策略
        public ActionResult<List<S7Config>> GetS7Confug()
        {
            try
            {
                using (var ctx = new TestDbContext())
                {
                    var s7list = ctx.S7Configs.ToList();
                    _logger.LogWarning("获取一次S7配置信息");
                    return Ok(s7list);
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"获取S7配置失败！出现系统错误，错误信息为{ex.Message}");
                return StatusCode(404, $"获取S7配置失败！出现系统错误，错误信息为{ex.Message}");
            }
        }


        /// <summary>
        /// 删除S7 配置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteS7Config")]
        [EnableCors("AllowSpecificOrigins")] // 应用 CORS 策略
        public Result DeleteS7Config(int id)
        {
            using (var ctx = new TestDbContext())
            {
                var s7 = ctx.S7Configs.Find(id);
                if (s7 != null)
                {
                    ctx.S7Configs.Remove(s7);
                    ctx.SaveChanges();
                    var result = new Result { Code = 200, ResultType = true, Message = "删除成功！" };
                    _logger.LogWarning($"执行删除id为:{id}S7配置操作，删除成功！");
                    return result;
                }
                else
                {
                    _logger.LogError($"执行删除id为:{id}S7配置操作，不存在该对象！");
                    return new Result { Code = 404, ResultType = false, Message = "不存在该对象！" };
                }
            }
        }


        /// <summary>
        /// 修改S7配置
        /// </summary>
        /// <param name="S7Name"></param>
        /// <param name="IP"></param>
        /// <param name="Port"></param>
        /// <param name="CPUType"></param>
        /// <param name="Rack"></param>
        /// <param name="Slot"></param>
        /// <returns></returns>
        [HttpPut("UpdateS7Config")]
        [EnableCors("AllowSpecificOrigins")] // 应用 CORS 策略
        public Result UpdateS7Config(int id, string S7Name, string IP, int Port, string CPUType, string Rack, string Slot)
        {
            try
            {
                using (var ctx = new TestDbContext())
                {
                    // 查找要更新的配置
                    var s7Config = ctx.S7Configs.Find(id);
                    if (s7Config == null)
                    {
                        _logger.LogError($"在修改配置名为：{S7Name}的配置时，出现错误要修改的配置不存在");
                        return new Result { Code = 404, ResultType = false, Message =$"在修改配置名为：{S7Name}的配置时，出现错误要修改的配置不存在" };
                    }

                    // 检查配置名称是否冲突
                    var existingConfigName = ctx.S7Configs.Any(x => x.S7Name == S7Name);
                    // 检查配置是否存在
                    var existConfig = ctx.S7Configs.Any(x => x.IP == IP && x.Port == Port && x.CPUType == CPUType && x.Slot == Slot && x.Rack == Rack);
                    if (existingConfigName)
                    {
                        _logger.LogError($"在修改的配置名为{S7Name}的配置时，修改的目标配置已存在，请检查是否修改的必要性！");
                        return new Result { Code = 400, ResultType = false, Message = $"在修改的配置名为{S7Name}的配置时，修改的目标配置已存在，请检查是否修改的必要性！" };
                    }
                    else if (existConfig)
                    {
                        var resultNg = new Result { Code = 400, ResultType = false, Message = "该配置已存在，不能重复！" };
                        _logger.LogError("添加S7配置，该配置已存在，不能重复！");
                        return resultNg;
                    }
                    else
                    {

                        // 更新配置信息
                        s7Config.S7Name = S7Name;
                        s7Config.IP = IP;
                        s7Config.Port = Port;
                        s7Config.CPUType = CPUType;
                        s7Config.Rack = Rack;
                        s7Config.Slot = Slot;
                        ctx.SaveChanges();
                        _logger.LogWarning("修改S7配置信息成功！");
                        return new Result { Code = 200, ResultType = true, Message = "修改成功" };
                    }

                }
            }
            catch (Exception ex)
            {
                // 记录异常信息到日志
                 _logger.LogError(ex, "更新S7配置时发生错误");

                return new Result { Code = 500, ResultType = false, Message = "修改失败，系统异常" };
            }
        }

    }
}
