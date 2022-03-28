
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

 

namespace MLCCommonLibrary.System.Config
{

    public class ApplicationSettings 
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
}
