using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MLCCommonILibrary.System.Config;
using MLCServiceData.User.Services.dal;
using MLCServicesData.User.Services.dal;
using MLCServicesData.Classes;
using Microsoft.AspNetCore.Authorization;

namespace MLCServiceApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentController : ControllerBase
    {
        private IViolationDal udal;
        public CommentController(IOptions<ApplicationSettings> appSetting, IOptions<ConnectionSetting> con)
        {
            this.udal = new ViolationDapper(appSetting.Value, con.Value);
        }

        [HttpPost]
        public async Task<ActionResult<string>> SaveComment([FromBody]ViolatorCommentModel data)
        {
            string Res;
            try
            {
                Res = await udal.SaveComment(data);
                return  Ok(Res);
            }
            catch {
                return BadRequest("Bad request");
            
            }
        }

    }
}
