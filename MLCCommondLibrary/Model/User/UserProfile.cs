using MLCCommonILibrary.Model;
using MLCCommonLibrary.Model.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace MLCCommonLibrary.Model
{
    public class UserProfile : UserLogIn//, IUserProfile
    {

        public string Id { get; set; }

        //public IUserEmail Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneConfirmed { get; set; }
        public bool FactorEnabled { get; set; }
        public DateTime LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }

        public override string UserName { get => base.UserName; set => base.UserName = value; }
        public override string Password { get => "Password@123"; }

    }
}
