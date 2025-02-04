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
                //不能保存配置名称相同的配置
                var existconfigname = ctx.ModbusConfigs.Any(x => x.ModbusName == modbus.ModbusName);
                //plcname、slaveid、Functioncode、StartAddress不能同时相同
                var exist = ctx.ModbusConfigs.Any(x => x.PLCName == modbus.PLCName && x.SlaveId == modbus.SlaveId && x.FunctionCode == modbus.FunctionCode && x.StartAddress == modbus.StartAddress);
                if(existconfigname)
                {
                    var resultNg = new Result { Code = 400, ResultType = false, Message = "该配置名已存在，配置名称不能重复！" };
                    return resultNg;
                }
                else if(exist)
                {
                    var resultNg = new Result { Code = 400, ResultType = false, Message = "该配置已存在，配置不能重复！" };
                    return resultNg;
                }
                else 
                {
                    ctx.ModbusConfigs.Add(modbus);
                    ctx.SaveChanges();
                    var resultOk = new Result { Code = 200, ResultType = true, Message = "已将新配置保存到数据库！" };
                    return resultOk;
                }
                
               
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

        /// <summary>
        /// 删除Modbus 配置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteModbusConfig")]
        [EnableCors("AllowSpecificOrigins")] // 应用 CORS 策略
        public Result DeleteModbusConfig(int id)
        {
            using (var ctx = new TestDbContext())
            {
                var plc = ctx.ModbusConfigs.Find(id);
                if (plc != null)
                {
                    ctx.ModbusConfigs.Remove(plc);
                    ctx.SaveChanges();
                    var result = new Result { Code = 200, ResultType = true, Message = "删除成功！" };
                    return result;
                }
                else
                {
                    return new Result { Code = 404, ResultType = false, Message = "不存在该对象！" };
                }
            }
        }

        /// <summary>
        /// 修改原有的配置
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="PLCName"></param>
        /// <param name="SlaveId"></param>
        /// <param name="Functioncode"></param>
        /// <param name="StartAddr"></param>
        /// <param name="Num"></param>
        /// <param name="ConfigName"></param>
        /// <returns></returns>
        [HttpPut("UpdateModbusConfig")]
        [EnableCors("AllowSpecificOrigins")] // 应用 CORS 策略
        public Result UpdateModbusConfig(int Id, string PLCName, int SlaveId, string Functioncode,string StartAddr, int Num, string ConfigName )
        {
            try
            {
                using (var ctx = new TestDbContext())
                {
                    var modbus = ctx.ModbusConfigs.Find(Id);
                    //不能保存配置名称相同的配置
                    var existconfigname = ctx.ModbusConfigs.Any(x => x.ModbusName == modbus.ModbusName);
                    //plcname、slaveid、Functioncode、StartAddress不能同时相同
                    var exist = ctx.ModbusConfigs.Any(x => x.PLCName == modbus.PLCName && x.SlaveId == modbus.SlaveId && x.FunctionCode == modbus.FunctionCode && x.StartAddress == modbus.StartAddress);
                    if (existconfigname)
                    {
                        var resultNg = new Result { Code = 400, ResultType = false, Message = "该配置名已存在，配置名称不能重复！" };
                        return resultNg;
                    }
                    else if (exist)
                    {
                        var resultNg = new Result { Code = 400, ResultType = false, Message = "该配置已存在，配置不能重复！" };
                        return resultNg;
                    }
                    else if (modbus != null& !existconfigname & !exist)
                    {
                        modbus.PLCName = PLCName;
                        modbus.SlaveId = SlaveId;
                        modbus.FunctionCode = Functioncode;
                        modbus.StartAddress = StartAddr;
                        modbus.Num = Num;
                        modbus.ModbusName = ConfigName;
                        ctx.SaveChanges();
                        var result = new Result { Code = 200, ResultType = true, Message = "修改成功！" };
                        return result;
                    }
                    else
                    {
                        var resultwarn = new Result { Code = 400, ResultType = false, Message = "要修改的对象不存在" };
                        return resultwarn;
                    }
                }
            }
            catch (Exception ex)
            {

                var resulterr = new Result { Message = ex.Message };
                return resulterr;
            }
        }
    }
}
