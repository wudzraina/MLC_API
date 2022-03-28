using MLCCommonILibrary.Model.Violation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MLCCommonLibrary.Model.Violation
{
    public class MonthYear : IMonthYear
    {

        public int Year { get; set; }
        public string Month { get; set; }
        
        public string MonthYears { get { return this.Year + " - " + this.Month; }}
    }
}
