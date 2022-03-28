using System;
using System.IO;
using System.Linq;
using System.Text;

using Microsoft.Extensions.Options;
using MLCCommonILibrary.System.Config;

namespace MLCServicesData.Classes
{

    public class ConnectionString
    {
        public ConnectionString(IApplicationSettings appSetting, IConnectionSetting con)
        {
            try
            {
                var path  = @appSetting.FileLocation + appSetting.FileName;



                string[] lines = File.ReadAllLines(path, Encoding.UTF8);

                if (lines.Length > 0)
                {
                    string cridential = "";

                    foreach (string res in lines)
                    {
                        cridential = cridential + res.ToString();
                    }
                    cridential = "User " + cridential;

                    this.DatabaseConnection = cridential + con.DatabaseConnection;
                    this.SecurityconnectionString = cridential + con.DBSecurityConnection;
                    this.TravelmartConnection = cridential + con.TravelmartConnection;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string DatabaseConnection { get; }
        public string TravelmartConnection { get; }
        public string SecurityconnectionString { get; }  

    }
}
