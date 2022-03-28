using MLCCommonILibrary.Model.Violation.Violator;
using System;
using System.Collections.Generic;
using System.Text;

namespace MLCCommonLibrary.Model.Violation.Violator
{
    public class RestHoursVilolation
    {

        public long ViolatorID { get; set; }
        public long EmployeeID { get; set; }
        public long GroupID { get; set; }
        public DateTime ViolationDate { get; set; }

        public List<IShiftDetail> ShiftDetail { get; set; } = new List<IShiftDetail>();

    }
}
