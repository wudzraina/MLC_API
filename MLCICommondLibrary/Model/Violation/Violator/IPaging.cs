using System;
using System.Collections.Generic;
using System.Text;

namespace MLCCommonILibrary.Model.Violation.Violator
{
    public interface IPaging
    {
        int? Pagings { get; set; }
        int? StartRow { get; set; }
        short? PagingCount { get; set; }
    }
}
