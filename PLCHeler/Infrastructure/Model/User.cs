using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Model
{
    public class User
    {
        //id 主键 自增
        public int Id { get; set; }
        //用户名
        public string UserName { get; set; }
        //密码
        public string Password { get; set; }
    }
}
