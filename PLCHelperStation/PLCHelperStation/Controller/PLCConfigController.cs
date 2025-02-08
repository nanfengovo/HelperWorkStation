using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PLCHelperStation.DB;
using PLCHelperStation.Modbel;
using PLCHelperStation.ResultObj;

namespace PLCHelperStation.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PLCConfigController : ControllerBase
    {

        protected readonly ILogger<PLCConfigController> _logger;

        public PLCConfigController(ILogger<PLCConfigController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 添加PLC配置
        /// </summary>
        /// <param name="pLC"></param>
        /// <returns></returns>
        [HttpPost("AddPLCConfig")]
        [EnableCors("AllowSpecificOrigins")] // 应用 CORS 策略
        public Result AddPLCConfig([FromBody] PLCConfig pLC)
        {
            using (var ctx = new TestDbContext())
            {
                #region 检查是否存在相同 IP 和端口的记录,如果相同就不保存到数据库
                var exists = ctx.PLCs.Any(x => x.IP == pLC.IP && x.Port == pLC.Port && x.Name == pLC.Name);
                if (!exists)
                {
                    ctx.PLCs.Add(pLC);
                    ctx.SaveChanges();
                    var reult = new Result { Code = 200, ResultType = true, Message = "新的 PLC 配置已成功保存到数据库！" };
                    _logger.LogWarning("添加PLC配置操作,新的 PLC 配置已成功保存到数据库！");
                    return reult;
                }
                #endregion


            }

            var reulterr = new Result { Code = 400, ResultType = false, Message = "想要保存的这个PLC配置已存在！" };
            _logger.LogError("添加PLC配置操作,想要保存的这个PLC配置已存在！");
            return reulterr;

        }

        /// <summary>
        /// 获取PLC配置
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllPLC")]
        [EnableCors("AllowSpecificOrigins")]
        public ActionResult<List<PLCConfig>> GetAllPLC()
        {
            using (var ctx = new TestDbContext())
            {
                var plcList = ctx.PLCs.ToList();
                _logger.LogWarning("获取一次PLC配置");
                return Ok(plcList);
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        [HttpPut("UpdatePLCConfig")]
        [EnableCors("AllowSpecificOrigins")]
        public Result UpdatePLCConfig(int id, string? ip, int port, string name)
        {
            try
            {
                using (var ctx = new TestDbContext())
                {
                    var plc = ctx.PLCs.Find(id);
                    if (plc != null)
                    {
                        plc.IP = ip;
                        plc.Port = port;
                        plc.Name = name;
                        ctx.SaveChanges();
                        var result = new Result { Code = 200, ResultType = true, Message = "修改成功！" };
                        _logger.LogWarning("修改PLC配置操作，修改成功！");
                        return result;
                    }
                    else
                    {
                        var resultwarn = new Result { Code = 400, ResultType = false, Message = "要修改的对象不存在" };
                        _logger.LogError("修改PLC配置操作，要修改的对象不存在");
                        return resultwarn;
                    }
                }
            }
            catch (Exception ex)
            {

                var resulterr = new Result { Message = ex.Message };
                _logger.LogError($"修改PLC配置操作，出现异常：{ex.Message}");
                return resulterr;
            }

        }

        /// <summary>
        /// 删除PLC配置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("DeletePLCConfig")]
        [EnableCors("AllowSpecificOrigins")]
        public Result DeletePLCConfig(int id)
        {
            using (var ctx = new TestDbContext())
            {
                var plc = ctx.PLCs.Find(id);
                if (plc != null)
                {
                    ctx.PLCs.Remove(plc);
                    ctx.SaveChanges();
                    var result = new Result { Code = 200, ResultType = true, Message = "删除成功！" };
                    _logger.LogWarning($"删除PLC操作，删除的PLC为{plc.Name}");
                    return result;
                }
                else
                {
                    _logger.LogError("删除PLC操作，不存在该对象");
                    return new Result { Code = 404, ResultType = false, Message = "不存在该对象！" };

                }
            }

        }
    }
}
