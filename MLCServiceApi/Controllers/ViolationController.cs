using System;
using System.Collections.Generic;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MLCCommonILibrary.Model.Violation.Violator;
using MLCCommonILibrary.System.Config;
using MLCCommonLibrary.Classes;
using MLCCommonLibrary.Model.Violation.Violator;
using MLCServiceApi.Data;
using MLCServiceData.User.Service.dal;
using MLCServiceData.User.Services.dal;
using MLCServicesData.User.Services.dal;

namespace MLCServiceApi.Controllers
{
    [Route("api")]
    [ApiController]
    [Authorize]
    public class ViolationController : ControllerBase
    {

        private IViolationDal udal;
        //private readonly UserManager<TokenUser> userManager;
        public ViolationController(IOptions<ApplicationSettings> appSetting, IOptions<ConnectionSetting> con)
        {
            this.udal = new ViolationDapper(appSetting.Value, con.Value);
        }

        [HttpGet("[controller]/{UserName}")]
        public async Task<object> Get(string UserName)
        {
            return await udal.GetBSCCJC(UserName);
        }

        [HttpPost("[controller]")]
        public async Task<object> GetViolation([FromBody] ParamVio param)
        {
            var result = await udal.GetViolator(param.UserName, param.BrandCode, param.ShipCode, param.CostCenterCode, param.JobCode, param.DateFrom, param.DateTo, param.StartRow, param.PagingCount);
            try
            {
                return Ok(result);
            }
            catch
            {
                return BadRequest(new { message = "Username or password is invalid" });
            }
        }

       
        [HttpPost("AllViolation")]
        public async Task<object> AllViolation([FromBody] Param param)
        {
            try
            {
                var result = await udal.AllViolator(param.UserName, param.BrandCode, param.ShipCode, param.CostCenterCode, param.JobCode, param.DateFrom, param.DateTo);
                return Ok(result);




            }
            catch
            {
                return BadRequest(new { message = "Username or password is invalid" });
            }
        }

        [HttpDelete("[controller]/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await udal.Delete(id);
                return Ok(result);
            }
            catch
            {
                return BadRequest(new { message = "Username or password is invalid" });
            }
        }


        [HttpGet("[controller]/RestHour")]
        public async Task<object> GetRestHours(long vId)
        {
            var result = await udal.GetRestHours(vId);
            try
            {
                if (result.Count > 0)
                {
                    IList<RestHoursVilolation> list = new List<RestHoursVilolation>();
                    StringBuilder doc = new StringBuilder();

                    doc.Append(@"<Table>
                                 <thead>");
                    doc.Append(result[0].RestHeader);
                    doc.Append(result[0].IOHeader);
                    doc.Append("</thead>");
                    doc.Append("<tbody'>");

                    foreach (var item in result)
                    {
                        doc.Append(item.RestHours);

                        list.Add(new RestHoursVilolation
                        {
                            EmployeeID = item.EmployeeID,
                            GroupID = item.GroupID,
                            ShiftDetail = item.ShiftDetail,
                            ViolationDate = item.ViolationDate,
                            ViolatorID = item.ViolatorID

                        });
                    }
                    doc.Append("</tbody></table>");
                    return Ok(new
                    {
                        status = true,
                        detail = doc.ToString(),
                        result = list
                    }); ;

                }
                else
                {
                    return NotFound(new { message = "Username or password is invalid" });
                }
               
            }
            catch
            {
                return BadRequest(new { message = "Username or password is invalid" });
            }
        }

    }
}