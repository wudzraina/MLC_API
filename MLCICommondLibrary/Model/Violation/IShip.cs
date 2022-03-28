using System;
using System.Collections.Generic;
using System.Text;

namespace MLCCommonILibrary.Model.Violation
{


    public interface IShip
    {

        int ShipId { get; set; }
        string ShipCode { get; set; }
        string ShipName { get; set; }
        string ShipCodeName { get; }
        int BrandId { get; set; }

    }
}
