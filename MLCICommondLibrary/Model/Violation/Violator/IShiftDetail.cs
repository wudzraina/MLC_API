using System;
using System.Collections.Generic;
using System.Text;

namespace MLCCommonILibrary.Model.Violation.Violator
{
    public interface IShiftDetail
    {
        string Detail { get; set; }
        string ShiftIn { get; set; }
        string ShiftOut { get; set; }
        string ShiftWork { get; set; }
        string ShiftRest { get; set; }
    }
}
