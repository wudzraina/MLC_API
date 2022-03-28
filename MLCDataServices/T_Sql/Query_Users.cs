using System;
using System.Collections.Generic;
using System.Text;

namespace MLCServicesData.T_Sql
{ 
    public struct Query_Users
    {
        #region select_Login
        const string select_Login = @"
                           
                    select distinct Z.RoleID,D.Role ,S.UserName,[Insert] ,[Update]  ,[Select] ,[Delete]
                        from  Users S
	                        inner join  [UserRole] Z on S.UserID = Z.UserID 
	                        left join [dbo].[RoleAccess] D on S.IsManager = D.RoleAccessID
                            where s.UserName = @pUserName and [Password] = @pPassword
                        ";
        public static string sel_Login { get { return select_Login; } }

        #endregion select_Login


        #region Select User
        const string select_Users = @"
                    select D.Role  ,Z.*  
                    from   [UserRole] Z
	                    inner join Users S on S.UserID = Z.UserID 
	                    left join [dbo].[RoleAccess] D on S.IsManager = D.RoleAccessID
                     where Z.IsActive = 1
            ";

        public static string sel_Users { get { return select_Users; } }
        #endregion Selec User
    }
}
