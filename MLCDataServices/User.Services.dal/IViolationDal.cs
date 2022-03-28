using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MLCCommonILibrary.Model;
using MLCCommonILibrary.Model.Violation.Violator;
using MLCServicesData.Classes;

namespace MLCServiceData.User.Services.dal
{
    public interface IViolationDal 
    {
        Task<IBrandShipCostCenterJobCode> GetBSCCJC(string user);
        Task<List<IViolator>> GetViolator(string UserID, string brandID, string vesselID, string costcenter, string JobCode, string Datefrom, string dateTo, int StartRow, int PagingCount);
        Task<List<IViolators>> AllViolator(string UserID, string brandID, string vesselID, string costcenter, string JobCode, string Datefrom, string dateTo);
        Task<string> SaveComment(ViolatorCommentModel comment);
        Task<int?> Delete(int id);
        Task<IList<IShift>> GetRestHours(long vId);

    }
}
