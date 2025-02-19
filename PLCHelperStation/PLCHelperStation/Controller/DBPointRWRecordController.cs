using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PLCHelperStation.DB;
using PLCHelperStation.Modbel;

namespace PLCHelperStation.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class DBPointRWRecordController : ControllerBase
    {
        protected readonly ILogger<DBPointRWRecordController> _logger;

        public DBPointRWRecordController(ILogger<DBPointRWRecordController> logger)
        {
            _logger = logger;
        }


        /// <summary>
        /// 获取DB块读写记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("DBPointRWRecord")]
        [EnableCors("AllowSpecificOrigins")] // 应用 CORS 策略
        public ActionResult<List<S7DBRWRecord>> GetDBPointRWRecord()
        {
            using (var ctx = new TestDbContext())
            {
                var RecordList = ctx.S7DBRWRecords.OrderByDescending(x => x.DateTime).ToList();
                _logger.LogWarning("用户获取一次DB块的读写记录！");
                return RecordList;
            }
        }
    }
}
