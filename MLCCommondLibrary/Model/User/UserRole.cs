using MLCCommonILibrary.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MLCCommonLibrary.Model
{
    public class UserRole: Users, IRole
    {
        public string RoleID { get; set; }
        public string Role { get; set; } 

        public bool Insert { get; set; }
        public bool Delete { get; set; }
        public bool Update { get; set; }
        public bool Select { get; set; }
        public string Email { get; set; }
    }



}
