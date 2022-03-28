using System;
using System.Collections.Generic;
using System.Text;

namespace MLCCommonILibrary.Model.Violation
{
    public interface IAuditPeriod
    {
        int PeriodID { get; set; }
        string AuditPeriods { get; set; }
        short StartDay { get; set; }
        short NoOfDays { get; set; }

    }
}
