using PLCHelperStation.ResultObj;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace PLCHelperStation.Modbel
{
    public class S7DBRWRecord
    {
        public S7DBRWRecord(string _DBName, string _Remark, string _result, string? _ExceptionInfo, DateTime _DateTime)
        {
            DBName = _DBName;
            Remark = _Remark;
            result = _result;
            ExceptionInfo = _ExceptionInfo;
            DateTime = _DateTime;
        }

        public S7DBRWRecord()
        {
            DBName = string.Empty;
            Remark = string.Empty;
            result = string.Empty;
            DateTime = DateTime.Now;
        }

        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        /// <summary>
        /// 数据点名称
        /// </summary>
        [Required]
        public  string DBName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Required]
        public string Remark { get; set; }


        /// <summary>
        /// 结果
        /// </summary>
        [Required]
        public string result { get; set; }


        /// <summary>
        /// 异常信息
        /// </summary>
        public string? ExceptionInfo { get; set; }


        /// <summary>
        /// 时间
        /// </summary>
        [Required]
        public DateTime DateTime { get; set; }
    }
}
