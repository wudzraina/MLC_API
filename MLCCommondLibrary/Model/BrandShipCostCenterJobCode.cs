using MLCCommonILibrary.Model;
using MLCCommonILibrary.Model.Violation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MLCCommonLibrary.Model.Violation
{
   public class BrandShipCostCenterJobCode : IBrandShipCostCenterJobCode
    {

        public List<IBrand> brand { get; set; }
        public List<IShip> ship { get; set; }
        public List<ICostCenter> costCenter { get; set; }
        public List<IPosition> position { get; set; }
        public List<IMonthYear> monthYear { get; set; }
        public List<IAuditPeriod> auditPeriod { get; set; }
    }
}
