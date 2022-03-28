using System;
using System.Collections.Generic;
using System.Text;

namespace MLCServicesData.Classes
{
    public class ViolatorCommentModel
    {

        public string CorrectiveAction { get; set; }
        public long? MngerID { get; set; }
        public string MngerPosition { get; set; }
        public int? MngrRootCauseID { get; set; }
        public int? RootCauseCategoryID { get; set; }
        public string RootCauseComment { get; set; }
        public int? RootCauseID { get; set; }
        public virtual string UserName { get; set; }
        public int ViolationTypeId { get; set; }
        public virtual long ViolatorsID { get; set; }
        
         

    }
}
