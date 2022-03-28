using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MLCServiceApi.Data
{
    public class SeedDatabase
    {

        public static void Initialize(IServiceProvider serviceProvider)
        {

            var context = serviceProvider.GetRequiredService<TokenDBContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<TokenUser>>();

            context.Database.EnsureCreated();

            if (!context.Users.Any())
            {
                TokenUser user = new TokenUser()
                {
                    UserName = "Muhallidin",
                    NormalizedUserName = "MUHALLIDIN",
                    Email = "wmuhallidin@gmail.com",
                    NormalizedEmail = "wmuhallidin@gmail.com",
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                try
                {
                    userManager.CreateAsync(user, "Password@123");
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }

            }



        }
    }
}
