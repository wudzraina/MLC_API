using System;
using System.Collections.Generic;
using System.Text;

namespace MLCCommonILibrary.Model.Violation.Violator
{
    public interface IViolator 
    {

        long ViolatorsID { get; set; }
        string ViolationDate { get; set; }
        long EmployeeID { get; set; }
        string LastName { get; set; }
        string FirstName { get; set; }
        string MiddleName { get; set; }
        string FullName { get; }
        string Ship { get; set; }
        string CostCenter { get; set; }
        string JobCode { get; set; }
        string ViolationType { get; set; }
        bool? IsLock { get; set; }
        bool? IsManager { get; set; }
        List<IShift> Shift { get; set; } 
        List<IRootCause> RootCause { get; set; }
        List<IPaging> Paging { get; set; }
    }  
}
