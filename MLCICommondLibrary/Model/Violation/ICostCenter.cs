using System;
using System.Collections.Generic;
using System.Text;

namespace MLCCommonILibrary.Model.Violation
{
    public interface ICostCenter
    {
        int CostCenterId { get; set; }
        string CostCenterCode { get; set; }
        string CostCenterName { get; set; }
        string CostCodeName { get; }
    }
}
