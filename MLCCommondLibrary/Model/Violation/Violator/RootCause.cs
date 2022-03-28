using MLCCommonILibrary.Model.Violation.Violator;
using System;
using System.Collections.Generic;
using System.Text;

namespace MLCCommonLibrary.Model.Violation.Violator
{
    public class RootCaused :Violation, IRootCause
    {
        public int? RootCauseID { get; set; }
        public string RootCause { get; set; }
    }
}
