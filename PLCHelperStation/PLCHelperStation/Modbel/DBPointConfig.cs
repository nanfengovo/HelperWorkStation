using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PLCHelperStation.Modbel
{
    public class DBPointConfig
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// S7名称
        /// </summary>
        [Required]
        public required string S7Name { get; set; }

        /// <summary>
        /// 数据点名称
        /// </summary>
        [Required]
        public required string DBName { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        [Required]
        public required string DBType { get; set; }

        /// <summary>
        /// DB块地址
        /// </summary>
        [Required]
        public required string DBAddress { get; set; }

        /// <summary>
        /// 偏移量
        /// </summary>
        [Required]
        public required string DBOffset { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Required]
        public string Remark { get; set; }

        /// <summary>
        /// 是否启用该配置点
        /// </summary>
        [Required]
        public bool IsOpen { get; set; }
    }
}
