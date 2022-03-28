using System;
using System.Collections.Generic;
using System.Text;

namespace MLCCommonILibrary.Model.Violation
{
   public interface IBrand 
    {
        int BrandId { get; set; }
        string BrandName { get; set; }
        string BrandCode { get; set; }
        string B_CodeName { get;  }
        string CompanyCode { get; set; }

    }

}
