using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PLCHelperStation.Modbel
{
    public class S7Config
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string S7Name {  get; set; }

        [Required]
        public string IP { get; set; }

        [Required]
        public string CPUType {  get; set; }

        [Required]
        public int Port { get; set; }

        [Required]
        public string Rack { get; set; }

        [Required]
        public string  Slot { get; set; }


    }
}
