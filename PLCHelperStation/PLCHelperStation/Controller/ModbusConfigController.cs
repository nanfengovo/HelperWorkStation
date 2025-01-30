using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PLCHelperStation.DB;
using PLCHelperStation.Modbel;
using PLCHelperStation.ResultObj;

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

        /// <summary>
        /// 添加Modbus配置
        /// </summary>
        /// <param name="modbus"></param>
        /// <returns></returns>
        [HttpPost("AddModbusConfug")]
        [EnableCors("AllowSpecificOrigins")] // 应用 CORS 策略
        public Result AddModbusConfug(ModbusConfig modbus)
        {
            using (var ctx = new TestDbContext())
            {
                var exist = ctx.ModbusConfigs.Any(x => x.ModbusName == modbus.ModbusName);
                if(!exist)
                {
                    ctx.ModbusConfigs.Add(modbus);
                    ctx.SaveChanges();
                    var resultOk = new Result { Code = 200, ResultType = true, Message = "已将新配置保存到数据库！" };
                    return resultOk;
                }
                var resultNg = new Result { Code = 400, ResultType = false, Message = "想要保存的这个配置已存在！" };
                return resultNg;
            }
        }

        /// <summary>
        /// 获取Modbus配置
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetModbusConfug")]
        [EnableCors("AllowSpecificOrigins")] // 应用 CORS 策略
        public ActionResult<List<ModbusConfig>> GetModbusConfug()
        {
            using (var ctx = new TestDbContext())
            {
                var modbusList = ctx.ModbusConfigs.ToList();
                return Ok(modbusList);
            }
        }
    }
}
