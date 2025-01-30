using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PLCHelperStation.DB;

namespace PLCHelperStation.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModbusConfigController : ControllerBase
    {
        /// <summary>
        /// 拿到所有的PLC名称给下拉列表用
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetPLCConfig")]
        [EnableCors("AllowSpecificOrigins")] // 应用 CORS 策略
        public object GetPLCConfig()
        {
            using (var ctx = new TestDbContext())
            {
                var plcList = ctx.PLCs.Select(plc => plc.Name).ToList();
                return Ok(plcList);
            }
        }
    }
}
