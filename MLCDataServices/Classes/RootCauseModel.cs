using MLCCommonLibrary.Model.Violation.Violator;
using System;
using System.Collections.Generic;
using System.Text;

namespace MLCServicesData.Classes
{
    internal class RootCauseModel: Violation 
    {
        public int? RootCauseID { get; set; }
        public string RootCause { get; set; }
    }
}
