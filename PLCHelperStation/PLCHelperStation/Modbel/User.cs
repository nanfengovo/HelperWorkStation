using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace PLCHelperStation.Modbel
{
    public class User
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [DisplayName("用户名")]
        public string Name { get; set; } = null!;
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("密码")]
        public string Password { get; set; } = null!;
    }
}
