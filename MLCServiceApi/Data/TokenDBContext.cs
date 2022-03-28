using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace MLCServiceApi.Data
{
    public class TokenDBContext  : IdentityDbContext<TokenUser>
    {
        public TokenDBContext(DbContextOptions<TokenDBContext> options) : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
