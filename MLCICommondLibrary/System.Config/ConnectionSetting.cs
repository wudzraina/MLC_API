using System;
using System.Collections.Generic;
using System.Text;
 

namespace MLCCommonILibrary.System.Config
{

    
    public interface IConnectionSetting
    {

        string DefaultConnection { get; set; }
        string DatabaseConnection { get; set; }
        string DBSecurityConnection { get; set; }
        string TravelmartConnection { get; set; }

    }
    public class ConnectionSetting : IConnectionSetting
    {
    

       public string DefaultConnection { get; set; }
        public string  DatabaseConnection { get; set; }
        public string  DBSecurityConnection { get; set; }
        public string  TravelmartConnection { get; set; }

    }
}
