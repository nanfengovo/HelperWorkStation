using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PLCHelperStation.Modbel
{
    public class Log
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime date { get; set; }

        [Required]
        public string level { get; set; }

        [Required]
        public string logger { get; set; }

        [Required]
        public string message { get; set; }
    }
}
