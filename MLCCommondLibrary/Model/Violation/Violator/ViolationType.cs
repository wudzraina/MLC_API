using MLCCommonILibrary.Model.Violation.Violator;
using System;
using System.Collections.Generic;
using System.Text;

namespace MLCCommonLibrary.Model.Violation.Violator
{
    public class Violation : Users
    {
        public int? ViolationTypeID { get; set; }
        public string ViolationType { get; set; }
 
    }
}
