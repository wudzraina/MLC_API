
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

 

namespace MLCCommonILibrary.System.Config
{
    public interface IApplicationSettings
    {

        string WebpagesVersion { get; set; }
        string PreserveLoginUrl { get; set; }
        string ClientValidationEnabled { get; set; }
        string UnobtrusiveJavaScriptEnabled { get; set; }
        string UseAPI { get; set; }
        string FileLocation { get; set; }
        string OffLine { get; set; }
        string UseCostomConnection { get; set; }
        string FileName { get; set; }
        string JWT_Secret { get; set; }
        string Client_Url { get; set; }

    }
    public class ApplicationSettings : IApplicationSettings
    {

        public string WebpagesVersion { get; set; }

        public string PreserveLoginUrl { get; set; }
        public string ClientValidationEnabled { get; set; }
        public string UnobtrusiveJavaScriptEnabled { get; set; }
        public string UseAPI { get; set; }

        public string FileLocation { get; set; }
        public string OffLine { get; set; }
        public string UseCostomConnection { get; set; }
        public string FileName { get; set; }

        public string JWT_Secret { get; set; }
        public string Client_Url { get; set; }


    }
}
