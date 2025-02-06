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
            using (var ctx = new TestDbContext())
            {
                var LogsList = ctx.logs.ToList();
                _logger.LogInformation("获取一次所有日志");
                return Ok(LogsList);
            }
        }
    }
}
