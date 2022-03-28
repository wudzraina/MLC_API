using MLCCommonILibrary.Model;
using MLCCommonILibrary.Model.Violation;
using MLCCommonILibrary.System.Config;
using MLCCommonLibrary.Model.Violation;
using MLCServicesData.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Linq;
using MLCServicesData.T_Sql;
using MLCCommonLibrary.Model;
using MLCCommonILibrary.Model.Violation.Violator;
using MLCCommonLibrary.Model.Violation.Violator;
using Dapper;
using MLCServiceData.User.Service.dal;
using MLCServiceData.User.Services.dal;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Reflection.Metadata.Ecma335;

namespace MLCServicesData.User.Services.dal
{
    public class ViolationDapper : IViolationDal
    {
        private readonly ConnectionString con;

        public ViolationDapper() { }

        public ViolationDapper(IApplicationSettings appSetting, IConnectionSetting con)
        {
            this.con = new ConnectionString(appSetting, con);
        }

        public async  Task<IBrandShipCostCenterJobCode> GetBSCCJC(string user)
        {
            try
            {
                IBrandShipCostCenterJobCode obj = new BrandShipCostCenterJobCode();
                await Task.Run(() =>
                {
                    using (  SqlConnection conn = new SqlConnection(con.DatabaseConnection))
                    {
                        conn.Open();
                        using (var multi = conn.QueryMultipleAsync(Query_ViolationDapper.Sel_Brand, commandTimeout:0).Result)
                        {
                            obj.brand = multi.Read<Brand>().ToList<IBrand>();
                            obj.ship = multi.Read<Ship>().ToList<IShip>();
                            obj.costCenter =  multi.Read<CostCenter>().ToList<ICostCenter>();
                            obj.position = multi.Read<Position>().ToList<IPosition>();
                            obj.monthYear = multi.Read<MonthYear>().ToList<IMonthYear>();
                            obj.auditPeriod = multi.Read<AuditPeriod>().ToList<IAuditPeriod>();
                        }
                    }
                });

                return obj;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<IViolator>> GetViolator(string UserID, string brandID, string vesselID, string costcenter, string JobCode, string Datefrom, string dateTo, int StartRow, int PagingCount)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(con.DatabaseConnection))
                {
                    conn.Open();
                    using (var multi = await conn.QueryMultipleAsync(Query_ViolationDapper.Sel_Violation,
                            new
                            {
                                pUserID = UserID,
                                pBrandID = brandID,
                                pVesselID = vesselID,
                                pCostcenter = costcenter,
                                pJobeCode = JobCode,
                                pDateFrom = Datefrom,
                                pDateTo = dateTo,
                                pStartRow = StartRow,
                                pPagingCount = PagingCount
                            }, commandTimeout: 0))
                    {
                        var vio = multi.Read<ViolatorModel>().ToList();
                        var shifts = multi.Read<ShiftModel>().ToList();
                        var rootCauses = multi.Read<RootCauseModel>().ToList();
                        var detail = multi.Read<Details>().ToList();
                        return ParseViolation(vio, shifts, rootCauses, detail.Select(e => e).FirstOrDefault().Detail);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<IViolators>> AllViolator(string UserID, string brandID, string vesselID, string costcenter, string JobCode, string Datefrom, string dateTo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(con.DatabaseConnection))
                {
                    conn.Open();
                    using (var multi = await conn.QueryMultipleAsync(Query_ViolationDapper.Sel_AllViolation,
                            new
                            {
                                pUserID = UserID,
                                pBrandID = brandID,
                                pVesselID = vesselID,
                                pCostcenter = costcenter,
                                pJobeCode = JobCode,
                                pDateFrom = Datefrom,
                                pDateTo = dateTo,
                            }, commandTimeout: 0))
                    {

                        var violation = multi.Read<ViolatorModel>().ToList();
                        var rootCauses = multi.Read<RootCauseModel>().ToList();

                        return (from a in violation
                                select new Violators
                                {

                                    ViolatorsID = a.ViolatorsID,
                                    ViolationDate = GlobalCode.Field2DateTime(a.ViolationDate).Value.ToString("MM-dd-yyyy"),
                                    EmployeeID = a.EmployeeID,
                                    FirstName = a.FirstName,
                                    LastName = a.LastName,
                                    MiddleName = a.MiddleName,
                                    Ship = a.Ship,
                                    CostCenter = a.CostCenter,
                                    JobCode = a.JobCode,
                                    ViolationType = a.ViolationType,
                                    ViolationTypeID = a.ViolationTypeID,
                                    RootCauseComment = a.RootCauseComment,
                                    CorrectiveAction = a.CorrectiveAction,
                                    RootCauseID = a.RootCauseID,
                                    MngerID = a.MngerID,
                                    MngerPosition = a.MngerPosition,
                                    IsLock = a.IsLock,
                                    IsManager = a.IsManager,
                                    MngrRootCauseID = a.MngrRootCauseID,
                                    RootCauseCategoryID = a.RootCauseCategoryID,
                                    RootCause = (from e in rootCauses
                                                select new RootCaused
                                                {
                                                    RootCauseID = e.RootCauseID,
                                                    RootCause = e.RootCause,
                                                    ViolationTypeID = e.ViolationTypeID,
                                                    ViolationType = e.ViolationType
                                                }).ToList<IRootCause>(),
                                            
                                }).ToList<IViolators>();
                    }
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IList<IShift>> GetRestHours(long vId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(con.DatabaseConnection))
                {
                    conn.Open();
                    using (var multi = await conn.QueryMultipleAsync(Query_ViolationDapper.Sel_RestHours,
                            new
                            {
                                pViolatorsID = vId
                            }, commandTimeout: 0))
                    {

                        var shift = multi.Read<ShiftModel>().ToList();
                        var detail = multi.Read<Details>().ToList();

                        var result = (from n in shift
                                      select new Shift(detail.Count > 0 ? detail[0].Detail : "")
                                      {
                                          GroupID = n.GroupID,
                                          EmployeeID = n.EmployeeID,
                                          ViolationDate = n.ViolationDate,
                                          ViolatorID = n.ViolatorsID,
                                          Detail = n.Detail,
                                      }).ToList<IShift>();
                        return result;

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        List<IViolator> ParseViolation(List<ViolatorModel> vio, List<ShiftModel> shift, List<RootCauseModel> rootCause, string detail  )
        {
            var ressult = (from a in vio
                           select new Violator
                           {

                               ViolatorsID =  a.ViolatorsID,
                               ViolationDate = GlobalCode.Field2DateTime(a.ViolationDate).Value.ToString("MM-dd-yyyy"),
                               EmployeeID = a.EmployeeID,
                               FirstName = a.FirstName,
                               LastName = a.LastName,
                               MiddleName = a.MiddleName,
                               Ship =a.Ship,
                               CostCenter = a.CostCenter,
                               JobCode = a.JobCode,
                               ViolationType =a.ViolationType,
                               ViolationTypeID = a.ViolationTypeID,
                               RootCauseComment =a.RootCauseComment,
                               CorrectiveAction = a.CorrectiveAction,
                               RootCauseID = a.RootCauseID,
                               MngerID = a.MngerID,
                               MngerPosition = a.MngerPosition,
                               IsLock = a.IsLock,
                               IsManager = a.IsManager,
                               MngrRootCauseID = a.MngrRootCauseID,
                               RootCauseCategoryID = a.RootCauseCategoryID,

                               Shift = (from n in shift
                                        where n.GroupID == a.ViolatorsID
                                        select new Shift(shift.Count > 0 ? detail.ToString() : "")
                                        {

                                            GroupID = n.GroupID,
                                            EmployeeID = n.EmployeeID,
                                            ViolationDate = n.ViolationDate,
                                            ViolatorID = n.ViolatorsID,

                                        }).ToList<IShift>(),

                               RootCause = (from e in rootCause
                                            select new RootCaused
                                            {
                                                RootCauseID =e.RootCauseID,
                                                RootCause = e.RootCause,
                                                ViolationTypeID = e.ViolationTypeID,
                                                ViolationType = e.ViolationType
                                            }).ToList<IRootCause>(),
                           }).ToList<IViolator>();

            return ressult;

        }

        public async Task<string> SaveComment(ViolatorCommentModel comment)
        {
            try
            {
                return await Task.Run(() =>
                {
                    using (SqlConnection conn = new SqlConnection(con.DatabaseConnection))
                    {
                        conn.Open();


                       return conn.ExecuteScalar<string>(Query_ViolationDapper.SaveComment,
                               new
                               {

                                   pViolatorsID = comment.ViolatorsID,
                                   pViolationTypeID = comment.ViolationTypeId,
                                   pRootCauseID = comment.RootCauseID,
                                   pRootCauseComment = comment.RootCauseComment,
                                   pCorrectiveActionTaken = comment.CorrectiveAction,
                                   pMngerID = comment.MngerID,
                                   pMngerPosition = comment.MngerPosition,
                                   pUserID = comment.UserName,
                                   pMngrRootCauseID =comment.MngrRootCauseID,
                                   pRootCauseCategoryID = comment.RootCauseCategoryID


                               }, commandTimeout: 0).ToString();
 
                    };
                });
            }
            catch(Exception ex) 
            {
                throw ex;
                //return "No inserted record";
                
            }
        }

        public async Task<int?> Delete(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(con.DatabaseConnection))
                {
                    conn.Open();
                    return await conn.ExecuteScalarAsync<int?>(Query_ViolationDapper.Del_Comment, new { pRootCauseCategoryID = id }, commandTimeout: 0);
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> LoadComment(IList<CommentVm> comment) {
            try
            {
                using (SqlConnection conn = new SqlConnection(con.DatabaseConnection))
                {
                    conn.Open();
                    return await conn.ExecuteScalarAsync<int>(Query_ViolationDapper.SaveComment,
                        new
                        {
                            pComment = comment
                        }, commandTimeout: 0);

                };
            }
            catch(Exception e) {
                throw e;
            }
        }
    }
}
