using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MLCServiceApi.Data;
using MLCCommonILibrary.System.Config;
using MLCServicesData.User.Services.dal;
using MLCServiceData.User.Service.dal;

using MLCServiceData.User.Services.dal;
using MLCCommonLibrary.Model.User;
using Microsoft.AspNetCore.Authorization;
using MLCCommonLibrary.Model;

namespace MLCServiceApi.Controllers
{

    [Route("api/v1.0")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IApplicationSettings _appSetting;
        private readonly IUsersDal user;
        private readonly UserManager<TokenUser> _userManager;

        public UsersController(IOptions<ApplicationSettings> appSetting, IOptions<ConnectionSetting> con, UserManager<TokenUser> userManager)
        { 
            _userManager = userManager;
            _appSetting = appSetting.Value; 
            //udal = new ViolationDapper(appSetting.Valu, con.Value);
            user = new UserDal(appSetting.Value, con.Value);

        }

        [HttpGet("[controller]")]
        public IActionResult Get([FromBody]Users user )
        {
            try {

                return Ok(user);
            }
            catch {
                return BadRequest(new
                {
                    Status = false,
                    Result = "Bad request!"
                });
            }

            //var user = await _userManager.FindByNameAsync(model.UserName);
            
            //if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            //{
            //    var key = Encoding.UTF8.GetBytes(_appSetting.JWT_Secret);

            //    var tokenDescriptor = new SecurityTokenDescriptor
            //    {
            //        Subject = new ClaimsIdentity(new Claim[] {
            //            new Claim("UserID", user.Id.ToString())
            //        }),
            //        Expires = DateTime.UtcNow.AddHours(3),
            //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            //    };
            //    var tokenHandler = new JwtSecurityTokenHandler();
            //    var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            //    var token = tokenHandler.WriteToken(securityToken);

            //    return Ok(new
            //    {
            //        Token =token,
            //        Expiration = tokenDescriptor.Expires
            //    });

            //}
            //else
            //{
            //    return BadRequest(new { message = "Username or password is invalid" });
            //}

        }


        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody]LogIn model)
        {
            try
            {
                var result = await user.LogIn(model);
                if (result.Count > 0)
                {

                    var key = Encoding.UTF8.GetBytes(_appSetting.JWT_Secret);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[] {
                        new Claim("UserID",  model.UserName)
                    }),

                        Expires = DateTime.UtcNow.AddHours(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                    var token = tokenHandler.WriteToken(securityToken);

                    return Ok(new
                    {
                        Status = true,
                        Token = token,
                        Expiration = tokenDescriptor.Expires,
                        Result = result
                    });

                }
                else
                {
                    return NotFound(new
                    {
                        Status = false,
                        Result = "Invalid user name or password!"
                    });
                }

            }
            catch {
                return BadRequest(new  {
                    Status = false,
                    Result = "Bad request!"
                });
            }

            //var user = await _userManager.FindByNameAsync(model.UserName);


        }
         
    }
}