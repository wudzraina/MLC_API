using System;
using System.Collections.Generic;
using System.Text;

namespace MLCCommonILibrary.Model
{
   public interface IUserProfile : IUsers, IUserEmail
    {
        string Id { get; set; }
        string PhoneNumber { get; set; }
        bool PhoneConfirmed { get; set; }
        bool FactorEnabled { get; set; }
        DateTime LockoutEnd { get; set; }
        bool LockoutEnabled { get; set; }
        int AccessFailedCount { get; set; }

    }
}
