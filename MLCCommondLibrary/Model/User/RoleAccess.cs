using MLCCommonILibrary.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MLCCommonLibrary.Model
{
    public abstract class RoleAccess : IRoleAccess
    {

        public bool canInsert { get; set; }
        public bool canDelete { get; set; }
        public bool canUpdate { get; set; }
        public bool canSelect { get; set; }
    }
}
