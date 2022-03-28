using System;
using System.Collections.Generic;
using System.Text;

namespace MLCCommonLibrary.Classes
{
    public class ParamVio : Param
    {
        public int StartRow { get; set; }
        public int PagingCount { get; set; }
    }

    public class Param
    {
        public string UserName { get; set; }
        public string BrandCode { get; set; }
        public string ShipCode { get; set; }
        public string CostCenterCode { get; set; }
        public string JobCode { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
       
    }
}
