using MLCCommonILibrary.Model.Violation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MLCCommonLibrary.Model.Violation
{
   public class Position : CostCenter, IPosition
    {

        public int PostionId { get; set; }
        public string PositionCode { get; set; }
        public string PositionName { get; set; }
        public string PostCodeName
        {
            get
            {
                return this.PositionCode + " - " + this.CostCenterName;
            }
        }

    }
}
