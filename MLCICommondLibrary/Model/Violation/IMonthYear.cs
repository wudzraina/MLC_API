using System;
using System.Collections.Generic;
using System.Text;

namespace MLCCommonILibrary.Model.Violation
{
    public interface IMonthYear
    {
        int Year { get; set; }
        string Month { get; set; }

        string MonthYears { get; }

    }
}
