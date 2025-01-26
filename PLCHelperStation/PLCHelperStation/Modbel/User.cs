using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace PLCHelperStation.Modbel
{
    public class User
    {
        [Required]
        [DisplayName("用户名")]
        public string Name { get; set; } = null!;
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("密码")]
        public string Password { get; set; } = null!;
    }
}
