using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MLCCommonILibrary.Model;

namespace MLCServiceIData.User.Service.dal
{
    public interface IUsersDal
    {
        Task<string> LogIn(IUsers user);
        Task<List<IUserProfile>> GetUsers();
    }

}
