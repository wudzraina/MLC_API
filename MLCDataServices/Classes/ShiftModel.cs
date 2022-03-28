using System;
using System.Collections.Generic;
using System.Text;

namespace MLCServicesData.Classes
{
    internal class ShiftModel
    {
        public long ViolatorsID { get; set; }
        public long EmployeeID { get; set; }
        public long GroupID { get; set; }
        public DateTime ViolationDate { get; set; }
        public string Detail { get; set; }
    }
}
