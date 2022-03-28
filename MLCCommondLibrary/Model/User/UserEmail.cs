using MLCCommonILibrary.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MLCCommonLibrary.Model
{
    public class UserEmail : Users, IUserEmail
    {
        public string Email { get; set; }
 
    }
}
