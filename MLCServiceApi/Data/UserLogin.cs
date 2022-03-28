using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreIdentityExample.Domain.Entities
{
    public class UserLogin : UserLoginKey
    {
        public string ProviderDisplayName { get; set; }
        public string UserId { get; set; }
    }

    public class UserLoginKey
    {
        public string LoginProvider;
        public string ProviderKey;
    }
}
