using System;
using System.Collections.Generic;
using System.Text;

namespace MLCCommonILibrary.Model.Violation.Violator
{
    public interface IShift
    {
        long GroupID { get; set; }

        long ViolatorID { get; set; }

        long EmployeeID { get; set; }
        string RestHeader { get;  }
        string IOHeader { get; }
        string RestHours { get;  }

        DateTime ViolationDate { get; set; }

        List<IShiftDetail> ShiftDetail { get; }

    }
}
