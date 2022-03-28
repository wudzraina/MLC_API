using MLCCommonILibrary.Model.Violation.Violator;
using System;
using System.Collections.Generic;
using System.Text;

namespace MLCCommonLibrary.Model.Violation.Violator
{
    public class Violator : Violators, IViolator
    {
        public Violator()
        {
            Shift = new List<IShift>();
            Paging = new List<IPaging>();
        } 
        public  List<IShift> Shift { get; set; }
         
        public List<IPaging> Paging { get; set; } 

    }

    public class Violators : ViolatorComment, IViolators
    {
        public Violators()
        {
            RootCause = new List<IRootCause>();
        }
        public override long ViolatorsID { get; set; }
        public string ViolationDate { get; set; }
        public long EmployeeID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string FullName
        {
            get
            {
                return this.LastName + " " + this.MiddleName + " " + this.FirstName;
            }
        }
        public string Ship { get; set; }
        public string CostCenter { get; set; }
        public string JobCode { get; set; }
        public new string ViolationType { get; set; }
        public bool? IsLock { get; set; }
        public bool? IsManager { get; set; }


        public new  List<IRootCause> RootCause { get; set; }

    }
}
