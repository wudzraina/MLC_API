using System;
using System.Collections.Generic;
using System.Text;

namespace MLCCommonLibrary.Model.Violation
{
    public class CommentVm : Users
    {
        public int? RootCauseCategoryID { get; set; }
        public long ViolatorsID { get; set; }
        public string VerifiedRootCause { get; set; }
        public string RootCause { get; set; }
        public string Comment { get; set; }
        public string CorrectiveAction { get; set; }
        public long? MngerID { get; set; }
        public string MngerPosition { get; set; }
        
        

    }




}
