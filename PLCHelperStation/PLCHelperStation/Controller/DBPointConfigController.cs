using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PLCHelperStation.DB;
using PLCHelperStation.Migrations.TestDb;
using PLCHelperStation.Modbel;
using PLCHelperStation.ResultObj;
using S7Config = PLCHelperStation.Modbel.S7Config;

namespace PLCHelperStation.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class DBPointConfigController : ControllerBase
    {

        protected readonly ILogger<DBPointConfigController> _logger;

        public DBPointConfigController(ILogger<DBPointConfigController> logger)
        {
            _logger = logger;
        }


        /// <summary>
        /// 添加数据点配置
        /// </summary>
        /// <param name="dbPoint"></param>
        /// <returns></returns>
        [HttpPost("AddDBPointConfig")]
        [EnableCors("AllowSpecificOrigins")] // 应用 CORS 策略
        public Result AddDBPointConfig(DBPointConfig dbPoint)
        {
            try
            {
                using (var ctx = new TestDbContext())
                {
                    //保存前检查配置名是否存在
                    var existConfigName = ctx.DBConfigs.Any(x => x.DBName == dbPoint.DBName);
                    //保存前检查配置是否存在
                    var existConfig = ctx.DBConfigs.Any(x => x.DBName == dbPoint.S7Name && x.DBType == dbPoint.DBType && x.DBAddress == dbPoint.DBAddress && x.DBOffset == dbPoint.DBOffset);
                    if (existConfigName)
                    {
                        var resultNg = new Result { Code = 400, ResultType = false, Message = "该配置名已存在，配置名称不能重复！" };
                        _logger.LogError("添加数据点配置，该配置名已存在，配置名称不能重复！");
                        return resultNg;
                    }
                    else if (existConfig)
                    {
                        var resultNg = new Result { Code = 400, ResultType = false, Message = "该配置已存在，不能重复！" };
                        _logger.LogError("添加数据点配置，该配置已存在，不能重复！");
                        return resultNg;
                    }
                    else
                    {
                        ctx.DBConfigs.Add(dbPoint);
                        ctx.SaveChanges();
                        _logger.LogWarning("添加数据点配置,添加成功！");
                        var result = new Result { Code = 200, ResultType = true, Message = "添加新配置成功！" };
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"添加数据点配置，添加S7配置名为{dbPoint.DBName}的过程中系统发生错误，错误信息为：{ex.Message}");
                return new Result { Code = 400, ResultType = false, Message = ex.Message };
            }
        }

        /// <summary>
        /// 获取下拉列表中的S7信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllS7")]
        [EnableCors("AllowSpecificOrigins")]
        public ActionResult<List<S7Config>> GetAllS7()
        {
            using (var ctx = new TestDbContext())
            {
                var s7List = ctx.S7Configs.Select(x => x.S7Name).ToList();
                _logger.LogWarning("获取一次S7配置");
                return Ok(s7List);
            }
        }

        /// <summary>
        /// 获取数据点配置
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllDBPointConfig")]
        [EnableCors("AllowSpecificOrigins")]
        public ActionResult<List<DBPointConfig>> GetAllDBPointConfig()
        {
            using (var ctx = new TestDbContext())
            {
                var dbPointList = ctx.DBConfigs.ToList();
                _logger.LogWarning("获取一次数据点配置");
                return Ok(dbPointList);
            }
        }


        /// <summary>
        /// 删除数据点配置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteDBPointConfig")]
        [EnableCors("AllowSpecificOrigins")] // 应用 CORS 策略
        public Result DeleteDBPointConfig(int id)
        {
            try
            {
                using (var ctx = new TestDbContext())
                {
                    var dbPoint = ctx.DBConfigs.Find(id);
                    if (dbPoint != null)
                    {
                        ctx.DBConfigs.Remove(dbPoint);
                        ctx.SaveChanges();
                        var result = new Result { Code = 200, ResultType = true, Message = "删除成功！" };
                        _logger.LogWarning("删除数据点配置操作，删除成功！");
                        return result;
                    }
                    else
                    {
                        var resultwarn = new Result { Code = 400, ResultType = false, Message = "要删除的对象不存在" };
                        _logger.LogError("删除数据点配置操作，要删除的对象不存在");
                        return resultwarn;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"删除数据点配置，删除数据点配置ID为{id}的过程中系统发生错误，错误信息为：{ex.Message}");
                return new Result { Code = 400, ResultType = false, Message = ex.Message };
            }
        }


        /// <summary>
        /// 修改数据点配置
        /// </summary>
        /// <param name="id"></param>
        /// <param name="S7Name"></param>
        /// <param name="DBName"></param>
        /// <param name="DBType"></param>
        /// <param name="DBAddress"></param>
        /// <param name="DBOffset"></param>
        /// <param name="Remark"></param>
        /// <param name="IsOpen"></param>
        /// <returns></returns>
        [HttpPut("UpdateDBPointConfig")]
        [EnableCors("AllowSpecificOrigins")] // 应用 CORS 策略
        public Result UpdateDBPointConfig(int id, string S7Name, string DBName, string DBType, string DBAddress, string DBOffset, string Remark, bool IsOpen)
        {
            try
            {
                using (var ctx = new TestDbContext())
                {
                    // 查找要更新的配置
                    var dbConfig = ctx.DBConfigs.Find(id);
                    if (dbConfig == null)
                    {
                        return new Result { Code = 404, ResultType = false, Message = "要修改的配置不存在" };
                    }

                    // 检查配置名称是否冲突
                    var existingConfigName = ctx.DBConfigs.Any(x => x.DBName == DBName);
                    // 检查配置是否存在
                    //var existConfig = ctx.DBConfigs.Any(x => x.S7Name == S7Name && x.DBType == DBType && x.DBAddress == DBAddress && x.DBOffset == DBOffset && x.Remark == Remark);
                    if (existingConfigName)
                    {
                        return new Result { Code = 400, ResultType = false, Message = $"修改DB数据点配置:在修改名为{DBName}的配置时配置名已存在，不允许使用该名称" };
                    }
                    //else if (existConfig)
                    //{
                    //    var resultNg = new Result { Code = 400, ResultType = false, Message = $"修改DB数据点配置:在修改名为{DBName}的配置时该配置已存在，不能重复！" };
                    //    _logger.LogError("修改DB数据点配置，该配置已存在，不能重复！");
                    //    return resultNg;
                    //}
                    else
                    {

                        // 更新配置信息
                        dbConfig.DBName = DBName;
                        dbConfig.S7Name = S7Name;
                        dbConfig.DBType = DBType;
                        dbConfig.DBAddress = DBAddress;
                        dbConfig.DBOffset = DBOffset;
                        dbConfig.Remark = Remark;
                        dbConfig.IsOpen = IsOpen;
                        ctx.SaveChanges();
                        _logger.LogWarning("修改DB数据点配置:修改成功！");
                        return new Result { Code = 200, ResultType = true, Message = "修改成功" };
                    }

                }
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "修改DB数据点配置:修改失败,程序出现异常，异常信息为：" + ex.Message);
                return new Result { Code = 400, ResultType = false, Message = $"修改失败，系统异常异常信息为{ ex.Message}" };
            }
        }


        /// <summary>
        /// 启用数据点
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("EnableDBPointConfig")]
        [EnableCors("AllowSpecificOrigins")] // 应用 CORS 策略
        public Result EnableDBPointConfig(int id)
        {
            try
            {
                using (var ctx = new TestDbContext())
                {
                    var dbpoint = ctx.DBConfigs.Find(id);
                    if (dbpoint != null)
                    {
                        if(dbpoint.IsOpen)
                        {
                            var resultwarn = new Result { Code = 401, ResultType = false, Message = "现在的状态就是启用！" };
                            _logger.LogWarning($"启用数据点配置操作，启用id为：{id} 的配置时发现本来就是启用的状态！");
                            return resultwarn;
                        }
                        dbpoint.IsOpen = true;
                        ctx.SaveChanges();
                        var result = new Result { Code = 200, ResultType = true, Message = "启用成功！" };
                        _logger.LogWarning("启用数据点配置操作，启用成功！");
                        return result;
                    }
                    else
                    {
                        var resultwarn = new Result { Code = 404, ResultType = false, Message = "要启用的对象不存在" };
                        _logger.LogError("启用数据点配置操作，要启用的对象不存在");
                        return resultwarn;
                    }
                }

            }
            catch (Exception ex)
            {

                _logger.LogError($"启用数据点配置，启用数据点配置ID为{id}的过程中系统发生错误，错误信息为：{ex.Message}");
                return new Result { Code = 400, ResultType = false, Message = $"启用数据点配置，启用数据点配置ID为{id}的过程中系统发生错误，错误信息为：{ex.Message}" };
            }
        }




        /// <summary>
        /// 停用数据点
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("DisableDBPointConfig")]
        [EnableCors("AllowSpecificOrigins")] // 应用 CORS 策略
        public Result DisableDBPointConfig(int id)
        {
            try
            {
                using (var ctx = new TestDbContext())
                {
                    var dbpoint = ctx.DBConfigs.Find(id);
                    if (dbpoint != null)
                    {
                        if (!dbpoint.IsOpen)
                        {
                            var resultwarn = new Result { Code = 401, ResultType = false, Message = "现在的状态就是停用！" };
                            _logger.LogWarning($"停用数据点配置操作，启用id为：{id} 的配置时发现本来就是停用的状态！");
                            return resultwarn;
                        }
                        dbpoint.IsOpen = false;
                        ctx.SaveChanges();
                        var result = new Result { Code = 200, ResultType = true, Message = "停用成功！" };
                        _logger.LogWarning("停用数据点配置操作，停用成功！");
                        return result;
                    }
                    else
                    {
                        var resultwarn = new Result { Code = 404, ResultType = false, Message = "要停用的对象不存在" };
                        _logger.LogError("停用数据点配置操作，要停用的对象不存在");
                        return resultwarn;
                    }
                }

            }
            catch (Exception ex)
            {

                _logger.LogError($"停用数据点配置，停用数据点配置ID为{id}的过程中系统发生错误，错误信息为：{ex.Message}");
                return new Result { Code = 400, ResultType = false, Message = $"停用数据点配置，停用数据点配置ID为{id}的过程中系统发生错误，错误信息为：{ex.Message}" };
            }
        }
    }
  

}

