using MLCCommonILibrary.Model.Violation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MLCCommonILibrary.Model
{
    public interface IBrandShipCostCenterJobCode
    {
        List<IBrand> brand { get; set; }
        List<IShip> ship { get; set; }
        List<ICostCenter> costCenter { get; set; }
        List<IPosition> position { get; set; }
        List<IMonthYear> monthYear { get; set; }
        List<IAuditPeriod> auditPeriod { get; set; }





    }
}
