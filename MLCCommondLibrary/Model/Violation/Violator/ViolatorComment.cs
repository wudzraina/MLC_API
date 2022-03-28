using MLCCommonILibrary.Model;
using MLCCommonILibrary.Model.Violation.Violator;
using System;
using System.Collections.Generic;
using System.Text;

namespace MLCCommonLibrary.Model.Violation.Violator
{
    public class ViolatorComment : RootCaused, IViolatorComment
    {

        public int? RootCauseCategoryID { get; set; }
        public string RootCauseComment { get; set; }
        public string CorrectiveAction { get; set; }
        public long? MngerID { get; set; }
        public string MngerPosition { get; set; }
        public int? MngrRootCauseID { get; set; }
        public  long ViolatorsID { get; set;}
        //public  string UserName { get; set; }

    }
}
