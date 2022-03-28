using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreIdentityExample.Domain.Entities
{
    public class RoleClaim : ClaimBase
    {
        public string RoleId { get; set; }
    }
}

