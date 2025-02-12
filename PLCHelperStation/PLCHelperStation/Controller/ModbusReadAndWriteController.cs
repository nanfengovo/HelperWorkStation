using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NModbus;
using PLCHelperStation.DB;
using PLCHelperStation.ResultObj;
using System.Net;
using System.Net.Sockets;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PLCHelperStation.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModbusReadAndWriteController : ControllerBase
    {
        protected readonly ILogger<ModbusReadAndWriteController> _logger;

        public ModbusReadAndWriteController(ILogger<ModbusReadAndWriteController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 读取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Read")]
        [EnableCors("AllowSpecificOrigins")] // 应用 CORS 策略
        public ActionResult<Result> Read(int id)
        {
            try
            {
                using (var ctx = new TestDbContext())
                {
                    //1、将要读的modbus存在
                    var modbus = ctx.ModbusConfigs.Find(id);
                    if (modbus == null)
                    {
                        _logger.LogError($"读取Modbus信号，数据库不存在id为{id}的modbus配置");
                        return new Result { Code = 404, ResultType = false, Message = $"数据库不存在id为{id}的modbus配置" };
                    }
                    else
                    {
                        //2、读的这个是启用的
                        if (!modbus.IsOpen)
                        {
                            _logger.LogError($"读取Modbus信号，对名称为{modbus.ModbusName}的信号点读取，无法访问因为是禁用的！！");
                            return new Result { Code = 403, ResultType = false, Message = $"Modbus 配置 {modbus.ModbusName} 被禁用" };
                        }
                        else
                        {
                            //满足读取的条件 ，需要提供：ip、port、slaveid、function、address、query
                            var plcname = modbus.PLCName;
                            var plc = ctx.PLCs.Find(plcname);
                            var ip = plc.IP;
                            var port = plc.Port;
                            var slaveid =(byte) modbus.SlaveId;
                            var function = modbus.FunctionCode;
                            var address =Convert.ToUInt16( modbus.StartAddress);
                            var query = (ushort)modbus.Num;

                            using (var Client = new TcpClient(ip,port))
                            {
                                //使用NMdobus工厂创建NModbus主站
                                var factory = new ModbusFactory();
                                var master = factory.CreateMaster(Client);
                                switch (function)
                                {
                                    case "01 Read Coils（0x）":
                                        var result1 = master.ReadCoils(slaveid, address, query);
                                        //PrintResult(result1,Address);
                                        for (int i = 0; i < result1.Length; i++)
                                        {
                                            _logger.LogWarning($"读取Modbus信号,成功对ip为{ip},端口为{port}的设备起始地址为{address}数量为{query}读取，设备id为{slaveid},读取结果为线圈地址 {address + i}: {result1[i]}\r\n");
                                            return new Result {Code = 200, ResultType = true , Message = "读取成功" ,Data = result1  };
                                        }
                                        break;
                                    case "02 Read Discrete Inputs (1x)":
                                        var result2 = master.ReadInputs(slaveid, address, query);
                                        //PrintResult(result1,Address);
                                        for (int i = 0; i < result2.Length; i++)
                                        {
                                            _logger.LogWarning($"读取Modbus信号,成功对ip为{ip},端口为{port}的设备起始地址为{address}数量为{query}读取，设备id为{slaveid},读取结果为输入状态地址 {address + i}: {result2[i]}\r\n");
                                            return new Result { Code = 200, ResultType = true, Message = "读取成功", Data = result2 };
                                        }
                                        break;
                                    case "03 Read Holding Registers (4x)":
                                        var result3 = master.ReadHoldingRegisters(slaveid, address, query);
                                        //PrintResult(result3,Address);
                                        for (int i = 0; i < result3.Length; i++)
                                        {
                                            _logger.LogWarning($"读取Modbus信号,成功对ip为{ip},端口为{port}的设备起始地址为{address}数量为{query}读取，设备id为{slaveid},读取结果为输入状态地址 {address + i}: {result3[i]}\r\n");
                                            return new Result { Code = 200, ResultType = true, Message = "读取成功", Data = result3 };
                                        }
                                        break;
                                    case "04 Read Input Registers (3x)":
                                        var result4 = master.ReadInputRegisters(slaveid, address, query);
                                        //PrintResult(result3,Address);
                                        for (int i = 0; i < result4.Length; i++)
                                        {
                                            _logger.LogWarning($"读取Modbus信号,成功对ip为{ip},端口为{port}的设备起始地址为{address}数量为{query}读取，设备id为{slaveid},读取结果为输入状态地址 {address + i}: {result4[i]}\r\n");
                                            return new Result { Code = 200, ResultType = true, Message = "读取成功", Data = result4 };
                                        }
                                        break;
                                }
                                return new Result { Code = 200, ResultType = true, Message = "读取成功" };
                            }

                        }

                    }
                }
            }
            catch (Exception ex)
            {

                _logger.LogError("读取Modbus信号,出现异常，异常信息为："+ex.Message);
                return new Result { Code = 400 , ResultType = false, Message = $"异常信息为：{ex.Message}"};
            }

           
        }
    }
}
