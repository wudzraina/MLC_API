using MLCCommonILibrary.Model.Violation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MLCCommonLibrary.Model.Violation
{

    public class AuditPeriod : IAuditPeriod
    {
        public int PeriodID { get; set; }

        public string AuditPeriods { get; set; }
        public short StartDay{ get; set; }
        public short NoOfDays { get; set; }

    }
}
