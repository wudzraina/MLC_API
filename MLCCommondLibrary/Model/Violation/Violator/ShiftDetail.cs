using MLCCommonILibrary.Model.Violation.Violator;
using System;
using System.Collections.Generic;
using System.Text;


namespace MLCCommonLibrary.Model.Violation
{
    public class ShiftDetail  : IShiftDetail
    {

    
        public string Detail { get; set; }
        public string ShiftIn { get; set; }
        public string ShiftOut { get; set; }
        public string ShiftWork { get; set; }
        public string ShiftRest { get; set; }


    }

}
