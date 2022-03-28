using MLCCommonILibrary.Model.Violation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MLCCommonLibrary.Model.Violation
{
   public  class Brand : IBrand
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public string BrandCode { get; set; }

        public string B_CodeName
        {
            get { return this.BrandCode + " - " + this.BrandName; }
        }
        public string CompanyCode { get; set; }

    }

}
