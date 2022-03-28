using MLCCommonILibrary.Model.Violation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MLCCommonLibrary.Model.Violation
{
    public class Ship: Brand, IShip
    {
        public int ShipId  { get; set; }
        public string ShipCode { get; set; }
        public string ShipName { get; set; }

        public string ShipCodeName
        {
            get
            {
                return this.ShipCode + " - " + this.ShipName;
            }
        }
    }
}
