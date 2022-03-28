using MLCCommonILibrary.Model.Violation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MLCCommonLibrary.Model
{
    public class CostCenter : ICostCenter
    {

        public int CostCenterId { get; set; }
        public string CostCenterCode { get; set; }
        public string CostCenterName { get; set; }

        public string CostCodeName
        {
            get {
                return this.CostCenterCode + " - " + this.CostCenterName;
            }
        }

    }
}
