using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MLCCommonILibrary.Model;
using MLCCommonILibrary.Model.Violation.Violator;

namespace MLCServiceIData.User.Service.dal
{
    public interface IViolationDal
    {
        Task<IBrandShipCostCenterJobCode> GetBSCCJC(string user);

        Task<List<IViolator>> GetViolator(string UserID, string brandID, string vesselID, string costcenter, string JobCode, string Datefrom, string dateTo, int StartRow, int PagingCount);

        Task<List<IViolator>> GetViolatorProc(string UserID, string brandID, string vesselID, string costcenter, string JobCode, string Datefrom, string dateTo, int StartRow, int PagingCount);



    }
}
