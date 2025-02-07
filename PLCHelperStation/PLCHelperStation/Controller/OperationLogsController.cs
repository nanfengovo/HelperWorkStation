using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PLCHelperStation.DB;
using PLCHelperStation.Modbel;
using PLCHelperStation.ResultObj;

namespace PLCHelperStation.Controller.Log
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationLogsController : ControllerBase
    {
        protected readonly ILogger<OperationLogsController> _logger;

        public OperationLogsController(ILogger<OperationLogsController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 获取所有日志
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllLogs")]
        [EnableCors("AllowSpecificOrigins")] // 应用 CORS 策略
        public object GetAllLogs()
        {
            try
            {
                using (var ctx = new TestDbContext())
                {
                    var LogsList = ctx.logs.ToList();
                    _logger.LogInformation("获取一次所有日志");
                    return Ok(LogsList);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"在获取所有日志的过程中，发生错误，详情为：{ex.Message}");
                return new Result { Code = 400, ResultType = false, Message = ex.Message };
            }
           
        }

        /// <summary>
        /// 根据关键字查询
        /// </summary>
        /// <param name="KeyWord"></param>
        /// <returns></returns>
        [HttpGet("GetLogsByKeyWord")]
        [EnableCors("AllowSpecificOrigins")] // 应用 CORS 策略
        public object GetLogsByKeyWord(string KeyWord)
        {
            try
            {
                using (var ctx = new TestDbContext())
                {
                    var LogList = ctx.logs.Where(x => x.message.Contains(KeyWord)).ToList();
                    _logger.LogInformation($"用户查询了一次关键字为：{KeyWord}的日志");
                    return LogList;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"在根据关键字为{KeyWord}查询的过程中，发生错误，详情为：{ex.Message}");
                return new Result { Code = 400, ResultType = false, Message = ex.Message };
            }
        }

        /// <summary>
        /// 根据日志级别查询
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        [HttpGet("GetLogsByLevel")]
        [EnableCors("AllowSpecificOrigins")] // 应用 CORS 策略
        public object GetLogsByLevel(string level)
        {
            try
            {
                using (var ctx = new TestDbContext())
                {
                    var loglist = ctx.logs.Where(x => x.level == level).ToList();
                    _logger.LogInformation($"根据日志级别为：{level}查询，查询成功");
                    return loglist;
                }
            }
            catch (Exception ex)
            {

                _logger.LogInformation($"根据日志级别为：{level}查询，查询失败，详细信息为：{ex.Message}");
                return new Result { Code = 400, ResultType = false, Message = ex.Message };
            }
        }
    }
}
