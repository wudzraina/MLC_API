using System;
using System.Collections.Generic;
using System.Text;

namespace MLCCommonILibrary.Model
{
    public interface IRole : IUsers, IUserEmail
    {
        string RoleID { get; set; }
        string Role { get; set; }

        bool Insert { get; set; }
        bool Delete { get; set; }
        bool Update { get; set; }
        bool Select { get; set; }


    }
}
