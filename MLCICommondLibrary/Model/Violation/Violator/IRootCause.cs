using System;
using System.Collections.Generic;
using System.Text;

namespace MLCCommonILibrary.Model.Violation.Violator
{
    public interface IRootCause
    {

        int? RootCauseID { get; set; }
        string RootCause { get; set; }
        //string ViolationType { get; set; }


    }
}
