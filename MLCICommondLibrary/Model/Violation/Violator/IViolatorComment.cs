using System;
using System.Collections.Generic;
using System.Text;

namespace MLCCommonILibrary.Model.Violation.Violator
{
    public interface IViolatorComment //: IViolationType
    {

        int? RootCauseCategoryID { get; set; }
        //int? RootCauseID { get; set; }
        string RootCauseComment { get; set; }
        string CorrectiveAction { get; set; }
        long? MngerID { get; set; }
        string MngerPosition { get; set; }
        //string MngerName { get; set; }
        int? MngrRootCauseID { get; set; }
        long ViolatorsID { get; set; }
       // int ViolationTypeId { get; set; }
        string UserName { get; set; }
    } 
}