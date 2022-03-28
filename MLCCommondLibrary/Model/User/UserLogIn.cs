using System;
using System.Collections.Generic;
using System.Text;

namespace MLCCommonLibrary.Model.User
{
    public class UserLogIn: Users
    {
        public virtual string Password { get; set; }
    }
}
