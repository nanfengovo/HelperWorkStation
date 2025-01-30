using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PLCHelperStation.Modbel
{
    public class ModbusConfig
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string ModbusName { get; set; }

        [Required]
        public string PLCName { get; set; }

        [Required]
        public int SlaveId { get; set; }

        [Required]
        public string FunctionCode { get; set; }

        [Required]
        public string StartAddress { get; set; }

        [Required]
        public int Num { get; set; }
    }
}
