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

        protected readonly ILogger<ModbusConfigController> _logger;

        public ModbusConfigController(ILogger<ModbusConfigController> logger)
        {
            _logger = logger;
        }


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
                _logger.LogWarning("获取一次所有PLC的名称！");
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
                if (existconfigname)
                {
                    var resultNg = new Result { Code = 400, ResultType = false, Message = "该配置名已存在，配置名称不能重复！" };
                    _logger.LogWarning("添加Modbus配置，该配置名已存在，配置名称不能重复！");
                    return resultNg;
                }
                else if (exist)
                {
                    var resultNg = new Result { Code = 400, ResultType = false, Message = "该配置已存在，配置不能重复！" };
                    _logger.LogWarning("添加Modbus配置，该配置已存在，配置不能重复！");
                    return resultNg;
                }
                else
                {
                    ctx.ModbusConfigs.Add(modbus);
                    ctx.SaveChanges();
                    var resultOk = new Result { Code = 200, ResultType = true, Message = "已将新配置保存到数据库！" };
                    _logger.LogInformation("已将新配置保存到数据库！");
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
                _logger.LogInformation("获取一次所有Modbus配置");
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
                    _logger.LogInformation("执行删除Modbus配置操作，删除成功！");
                    return result;
                }
                else
                {
                    _logger.LogWarning("执行删除Modbus配置操作，不存在该对象！");
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
        public Result UpdateModbusConfig(int Id, string PLCName, int SlaveId, string Functioncode, string StartAddr, int Num, string ConfigName)
        {
            try
            {
                using (var ctx = new TestDbContext())
                {
                    var modbus = ctx.ModbusConfigs.Find(Id);
                    _logger.LogInformation($"修改Modbus配置操作,修改前数据为PLC名称为：{modbus.PLCName},设备ID为：{modbus.SlaveId},功能码为：{modbus.FunctionCode},开始地址为：{modbus.StartAddress},数量为：{modbus.Num},Modbus名称为：{modbus.ModbusName}");
                    //不能保存配置名称相同的配置
                    var existconfigname = ctx.ModbusConfigs.Any(x => x.ModbusName == modbus.ModbusName);
                    //plcname、slaveid、Functioncode、StartAddress不能同时相同
                    var exist = ctx.ModbusConfigs.Any(x => x.PLCName == modbus.PLCName && x.SlaveId == modbus.SlaveId && x.FunctionCode == modbus.FunctionCode && x.StartAddress == modbus.StartAddress);
                    if (existconfigname)
                    {
                        var resultNg = new Result { Code = 400, ResultType = false, Message = "该配置名已存在，配置名称不能重复！" };
                        _logger.LogWarning("修改Modbus配置操作，该配置名已存在，配置名称不能重复！");
                        return resultNg;
                    }
                    else if (exist)
                    {
                        var resultNg = new Result { Code = 400, ResultType = false, Message = "该配置已存在，配置不能重复！" };
                        _logger.LogWarning("修改Modbus配置操作，该配置已存在，配置不能重复！");
                        return resultNg;
                    }
                    else if (modbus != null & !existconfigname & !exist)
                    {
                        modbus.PLCName = PLCName;
                        modbus.SlaveId = SlaveId;
                        modbus.FunctionCode = Functioncode;
                        modbus.StartAddress = StartAddr;
                        modbus.Num = Num;
                        modbus.ModbusName = ConfigName;
                        ctx.SaveChanges();
                        var result = new Result { Code = 200, ResultType = true, Message = "修改成功！" };
                        _logger.LogInformation($"修改Modbus配置操作，修改成功！修改后的数据为PLC名称为：{PLCName},设备ID为：{SlaveId},功能码为：{Functioncode},开始地址为：{StartAddr},数量为：{Num},Modbus名称为：{ConfigName}");
                        return result;
                    }
                    else
                    {
                        var resultwarn = new Result { Code = 400, ResultType = false, Message = "要修改的对象不存在" };
                        _logger.LogWarning("修改Modbus配置操作,修改失败：要修改的对象不存在");
                        return resultwarn;
                    }
                }
            }
            catch (Exception ex)
            {

                var resulterr = new Result { Message = ex.Message };
                _logger.LogError("修改Modbus配置操作,出现异常：" + ex.Message);
                return resulterr;
            }
        }


        /// <summary>
        /// 启用Modbus配置
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPut("EnableModbusConfig")]
        [EnableCors("AllowSpecificOrigins")] // 应用 CORS 策略
        public Result EnableModbusConfig(int Id)
        {
            try
            {
                using (var ctx = new TestDbContext())
                {
                    var modbus = ctx.ModbusConfigs.FirstOrDefault(x => x.Id == Id);
                    if (modbus == null)
                    {
                        var resulterr = new Result { Code = 404, ResultType = false, Message = "不存在这个配置！" };
                        _logger.LogWarning($"启用Modbus配置，id为：{Id}的配置不存在！");
                        return resulterr;
                    }
                    else
                    {
                        var statue = modbus.IsOpen;
                        if (statue)
                        {
                            var resultwarn = new Result { Code = 401, ResultType = false, Message = "现在的状态就是启用！" };
                            _logger.LogWarning($"启用Modbus配置，启用id为：{Id} 的配置时发现本来就是启用的状态！");
                            return resultwarn;
                        }
                        else
                        {
                            modbus.IsOpen = true;
                            ctx.SaveChanges();
                            var result = new Result { Code = 200, ResultType = true, Message = "启用成功！" };
                            _logger.LogInformation($"启用Modbus配置，id为：{Id} 的配置启用成功！");
                            return result;
                        }

                    }


                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"启用Modbus配置,在启用id为：{Id}的过程中程序发生错误！！！错误信息为：" + ex.Message);
                var resulterr = new Result { Code = 400, ResultType = false, Message = ex.Message };
                return resulterr;
            }
        }



        /// <summary>
        /// 停用Modbus配置
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPut("DisableModbusConfig")]
        [EnableCors("AllowSpecificOrigins")] // 应用 CORS 策略
        public Result DisableModbusConfig(int Id)
        {
            try
            {
                using (var ctx = new TestDbContext())
                {
                    var modbus = ctx.ModbusConfigs.FirstOrDefault(x => x.Id == Id);
                    if (modbus == null)
                    {
                        var resulterr = new Result { Code = 404, ResultType = false, Message = "不存在这个配置！" };
                        _logger.LogWarning($"停用Modbus配置，id为：{Id}的配置不存在！");
                        return resulterr;
                    }
                    else
                    {
                        var statue = modbus.IsOpen;
                        if (!statue)
                        {
                            var resultwarn = new Result { Code = 401, ResultType = false, Message = "现在的状态就是停用！" };
                            _logger.LogWarning($"停用Modbus配置，启用id为：{Id} 的配置时发现本来就是停用的状态！");
                            return resultwarn;
                        }
                        else
                        {
                            modbus.IsOpen = false;
                            ctx.SaveChanges();
                            var result = new Result { Code = 200, ResultType = true, Message = "停用成功！" };
                            _logger.LogInformation($"停用Modbus配置，id为：{Id} 的配置停用成功！");
                            return result;
                        }

                    }


                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"停用Modbus配置,在停用id为：{Id}的过程中程序发生错误！！！错误信息为：" + ex.Message);
                var resulterr = new Result { Code = 400, ResultType = false, Message = ex.Message };
                return resulterr;
            }
        }
    }
}
