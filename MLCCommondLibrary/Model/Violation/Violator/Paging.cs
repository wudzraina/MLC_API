using MLCCommonILibrary.Model.Violation.Violator;
using System;
using System.Collections.Generic;
using System.Text;

namespace MLCCommonLibrary.Model.Violation.Violator
{
    public class Paging :  IPaging
    {
        public int? Pagings { get; set; }
        public int? StartRow { get; set; }
        public short? PagingCount { get; set; }
    }
     

     
}
