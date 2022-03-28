using System;
using System.Collections.Generic;
using MLCCommonILibrary.Model.Violation.Violator;
using MLCCommonLibrary.Model.Violation.Violator;

namespace MLCCommonLibrary.Model.Violation
{
    public class Shift : RestHoursVilolation, IShift
    {

        readonly char[] s = { '^' };
        readonly int  _ShiftCount;


        string Hrs = "";
        string Header = "";
        string InOutHeader = "";

        public Shift() { }

        public Shift(string shiftcount)
        {
            this._ShiftCount = shiftcount.Split(s).Length;
        }

        //public long ViolatorID { get; set; }
        //public long EmployeeID { get; set; }
        //public long GroupID { get; set; }
        //public DateTime ViolationDate { get; set; }


        public string RestHeader => Header;
        public string IOHeader => InOutHeader;
        public string RestHours => Hrs;



        public string Detail 
        { 
            set { 
                GetShifDetail(value);
            } 
        }

       // public List<IShiftDetail> ShiftDetail { get; } = new List<IShiftDetail>();

        void GetShifDetail(string detail)
        {

            try
            {
                string IN = "", OUT = "", WORK = "", REST = "";

                char[] d = { ',' };
                string[] sn = detail.Split(s);
                string[] sd = null;

                Header = "";

                string thHr = "";
                string sTH = "<th colSpan='4'>";
                string eTH = "</th>";
                string sTR = "<tr>";
                string eTR = "</tr>";
                string std = "<td>";
                string etd = "</td>";

                for (var i = 0; i < sn.Length; i++)
                {

                    sd = sn[i].Split(d);
                    for (var e = 0; e < sd.Length; e++)
                    {
                        switch (e) 
                        { 
                            case 0:
                                IN = sd[e].ToString();
                                break;
                            case 1:
                                OUT = sd[e].ToString();
                                break;
                            case 2:
                                WORK = sd[e].ToString();
                                break;
                            case 3:
                                REST = sd[e].ToString();
                                break;
                        }
                     
                    }

                 


                    //IN = sd[0].ToString();
                    //OUT = sd[1].ToString();
                    //WORK = sd[2].ToString();
                    //REST = sd[3].ToString();

                    Header += sTH + "Shift " + (i + 1).ToString() + eTH;

                    thHr += @"<th>IN</th><th>OUT</th><th>WORK</th><th>REST</th>";

                    Hrs += std + IN + etd + std + OUT + etd + std + WORK + etd + std + REST + etd;

                    ShiftDetail.Add(new ShiftDetail
                    {

                        Detail = "Shift " + (i + 1).ToString(),
                        ShiftIn = IN,
                        ShiftOut = OUT,
                        ShiftWork = WORK,
                        ShiftRest = REST
                    });

                }



                if (_ShiftCount > sn.Length)
                {

                    var len = sn.Length;
                    for (var i = len + 1; i <= _ShiftCount; i++)
                    {
                        ShiftDetail.Add(new ShiftDetail
                        {
                            Detail = "Shift " + i,
                            ShiftIn = "",
                            ShiftOut = "",
                            ShiftWork = "",
                            ShiftRest = ""
                        });

                        Header += sTH + "Shift " + i.ToString() + eTH;
                        thHr += @"<th>IN</th><th>OUT</th><th>WORK</th><th>REST</th>";
                        Hrs += std + "" + etd + std + "" + etd + std + "" + etd + std + "" + etd;
                    }

                };

                Header = sTR + "<th rowSpan='2'>Date</th>" + Header + eTR;
                InOutHeader = sTR + thHr + eTR;
                Hrs = sTR + std + ViolationDate.ToString("dd-MM-yyyy") + etd + Hrs + eTR;


            }
            catch(Exception e)
            {
                throw e;
            }



        } 


    }
}
