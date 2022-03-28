using System;
using System.Collections.Generic;
using System.Text;

namespace MLCCommonILibrary.Model.Violation
{
   public interface IPosition
    {

       int PostionId { get; set; }
       string PositionCode { get; set; }
       string PositionName { get; set; }
        

    }
}
