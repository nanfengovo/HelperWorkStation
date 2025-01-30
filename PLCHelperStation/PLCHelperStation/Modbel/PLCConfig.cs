using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PLCHelperStation.Modbel
{
    public class PLCConfig
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string IP { get; set; }

        [Required]
        public int Port { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
