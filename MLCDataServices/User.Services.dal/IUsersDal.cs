using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MLCCommonILibrary.Model;
using MLCCommonLibrary.Model.User;
using MLCServicesData.User.Services.dal;

namespace MLCServiceData.User.Service.dal
{
    public interface IUsersDal 
    {
        Task<IList<IRole>> LogIn(LogIn user);
        Task<List<IRole>> GetUsers();

    }
     

}
