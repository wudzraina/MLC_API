using MLCCommonLibrary.Model.Violation.Violator;
using System;
using System.Collections.Generic;
using System.Text;

namespace MLCServicesData.Classes
{
    internal class ViolatorModel : ViolatorComment
    {
        public override long ViolatorsID { get; set; }
        public string ViolationDate { get; set; }
        public long EmployeeID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }

        public string Ship { get; set; }
        public string CostCenter { get; set; }
        public string JobCode { get; set; }
        public bool? IsLock { get; set; }
        public bool? IsManager { get; set; }
        public short? Manager { get; set; }

    }
     
}
