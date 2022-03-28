using System;
using System.Collections.Generic;
using System.Text;

namespace MLCCommonILibrary.Model
{
    public interface IRoleAccess
    {
        bool canInsert { get; set; }
        bool canDelete { get; set; }
        bool canUpdate { get; set; }
        bool canSelect { get; set; }
    }
}
