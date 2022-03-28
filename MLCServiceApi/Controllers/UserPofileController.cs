
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.DependencyInjection;



using Microsoft.Extensions.Options;
using MLCCommonILibrary.System.Config;

using MLCServiceApi.Data;
using MLCServiceData.User.Service.dal;
using MLCServicesData.User.Services.dal;

namespace MLCServiceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserPofileController : ControllerBase
    {

        private IUsersDal udal;
        private UserManager<TokenUser> userManager;

        public UserPofileController(IOptions<ApplicationSettings> appSetting, IOptions<ConnectionSetting> con, UserManager<TokenUser> userManager)
        {

            this.userManager = userManager; //serviceProvider.GetRequiredService<UserManager<TokenUser>>();
            this.udal = new UserDal(appSetting.Value, con.Value );
        }

        [HttpGet]
        public async Task<object> Get()
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            //var user = await userManager.FindByNameAsync(userId);
            var user = await userManager.FindByIdAsync(userId);
            return new
            {
                user.UserName,
                user.Email,
                user.PhoneNumber
            };
        }

        [HttpGet]
        [Route("UserList")]
        [AllowAnonymous]
        public async Task<ActionResult> UserList()
        {
            try
            {
                var result = await udal.GetUsers();

                return Ok(result);
            }
            catch(Exception e) 
            {
                return BadRequest( new {
                    result = e.Message.ToList()
                });
            }

           

            //if (uProfile.Count > 0)
            //{
            //    foreach (var up in uProfile)
            //    {

            //        TokenUser user = new TokenUser()
            //        {
            //            UserName = up.UserName,
            //            NormalizedUserName = up.UserName.ToUpper(),
            //            Email = up.Email,
            //            NormalizedEmail = up.Email,
            //            SecurityStamp = Guid.NewGuid().ToString()
            //        };

            //        await userManager.CreateAsync(user,"" );
            //    }

            //   // TokenUser user = new TokenUser()
            //   // {
            //   //     UserName = "Muhallidin",
            //   //     NormalizedUserName = "MUHALLIDIN",
            //   //     Email = "wmuhallidin@gmail.com",
            //   //     NormalizedEmail = "wmuhallidin@gmail.com",
            //   //     SecurityStamp = Guid.NewGuid().ToString()
            //   // };
            //   //await userManager.CreateAsync(user, "Password@123");

            //}
            //return "";

        }

    }
}