using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NDataMapper.Attributes;

namespace NDataModel
{
    public class LoginModel
    {
        [DataNames("LoginName", "LoginName")]
        public string LoginName { get; set; }

        [DataNames("Password", "Password")]
        public string Password { get; set; }
    }
}
