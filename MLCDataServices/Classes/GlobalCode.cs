using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace MLCServicesData.Classes
{
    public static class GlobalCode
    {



        public const string DateTimeFormat = "MM/dd/yyyy HH:mm:ss:fff";
        public const string UserCultureInfo = "en-US";
        /// <summary>
        /// Date Created:   20/04/2012
        /// Created By:     Muhallidin
        /// (description)   Convert string to double with validation
        /// =============================================================
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        public static double Field2Double(object sender)
        {
            double vDouble = 0;
            try
            {
                if (sender != null)
                {
                    switch (sender.GetType().Name.ToString())
                    {
                        case "String":
                            string sType = (string)sender;
                            vDouble = Convert.ToDouble(sType.ToString());
                            break;
                        default:
                            vDouble = Convert.ToDouble(sender.ToString());
                            break;
                    }
                }
                return vDouble;
            }
            catch
            {
                return 0;
            }
        }

        public static long Field2Long(object sender)
        {
            long vLong = 0;
            try
            {
                if (sender != null)
                {
                    switch (sender.GetType().Name.ToString())
                    {

                        case "String":
                            string sType = (string)sender;
                            vLong = Convert.ToInt64(sType.ToString());
                            break;
                        default:
                            vLong = Convert.ToInt64(sender.ToString());
                            break;

                    }
                }
                return vLong;
            }
            catch
            {
                return 0;
            }
        }
        public static Int32 Field2Int(object sender)
        {
            Int32 vInt = 0;
            try
            {
                if (sender != null)
                {
                    switch (sender.GetType().Name.ToString())
                    {


                        case "String":
                            string sType = (string)sender;
                            vInt = Convert.ToInt32(sType.ToString());
                            break;
                        default:
                            vInt = Convert.ToInt32(sender.ToString());
                            break;
                    }
                }
                return vInt;
            }
            catch
            {
                return 0;
            }
        }

        public static float Field2Float(object sender)
        {
            float vInt = 0;
            try
            {
                if (sender != null)
                {
                    switch (sender.GetType().Name.ToString())
                    {


                        case "String":
                            string sType = (string)sender;
                            vInt = Convert.ToSingle(sType.ToString());
                            break;
                        default:
                            vInt = Convert.ToSingle(sender.ToString());
                            break;
                    }
                }
                return vInt;
            }
            catch
            {
                return 0;
            }
        }


        public static Int16 Field2TinyInt(object sender)
        {
            Int16 vInt = 0;
            try
            {
                if (sender != null)
                {
                    switch (sender.GetType().Name.ToString())
                    {

                        case "String":
                            String SType = (String)sender;
                            vInt = Convert.ToInt16(SType.ToString());
                            break;
                        default:
                            vInt = Convert.ToInt16(sender.ToString());
                            break;
                    }
                }
                return vInt;
            }
            catch
            {
                return 0;
            }
        }

        public static bool Field2Bool(object sender)
        {
            bool vbool = false;
            try
            {
                if (sender != null)
                {
                    switch (sender.GetType().ToString())
                    {

                        case "String":
                            String SType = (String)sender;
                            vbool = Convert.ToBoolean(SType.ToString());
                            break;
                        default:
                            vbool = Convert.ToBoolean(sender);
                            break;
                    }
                }
                return vbool;
            }
            catch
            {
                return false;
            }
        }


        public static string Field2String(object sender)
        {
            try
            {
                if (sender != null)
                    return sender.ToString();
                else
                    return "";
            }
            catch
            {
                return "";
            }

        }

        public static DateTime? Field2DateTime(object sender)
        {
            CultureInfo enCulture = new CultureInfo(UserCultureInfo);
            DateTime vDateTime = DateTime.Now;
            try
            {
                if (sender != null)
                {
                    switch (sender.GetType().Name.ToString())
                    {

                        case "String":
                            String SType = (String)sender;
                            vDateTime = DateTime.Parse(SType.ToString(), enCulture);
                            break;
                        default:
                            vDateTime = DateTime.Parse(sender.ToString(), enCulture);
                            break;
                    }
                    return vDateTime;
                }
                else
                {
                    return null;
                }

            }
            catch
            {
                return null;
            }
        }

        public static DateTime Field2DateTime1(object sender)
        {
            CultureInfo enCulture = new CultureInfo(UserCultureInfo);
            DateTime vDateTime = DateTime.Now;
            try
            {
                if (sender != null)
                {
                    switch (sender.GetType().Name.ToString())
                    {

                        case "String":
                            String SType = (String)sender;
                            vDateTime = DateTime.Parse(SType.ToString(), enCulture);
                            break;
                        default:
                            vDateTime = DateTime.Parse(sender.ToString(), enCulture);
                            break;
                    }
                }
                return vDateTime;
            }
            catch
            {
                return DateTime.Now;
            }
        }

        /// <summary>
        /// Date Created:   02/01/2011
        /// Created By:     Josephine Gad
        /// (description)   Convert string to decimal with validation
        /// =============================================================
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        public static decimal Field2Decimal(object sender)
        {
            decimal vDec = 0;
            try
            {
                if (sender != null)
                {
                    switch (sender.GetType().Name.ToString())
                    {
                        case "String":
                            string sType = (string)sender;
                            vDec = Convert.ToDecimal(sType.ToString());
                            break;
                        default:
                            vDec = Convert.ToDecimal(sender.ToString());
                            break;
                    }
                }
                return vDec;
            }
            catch
            {
                return 0;
            }
        }


        /// <summary>
        /// Author:       Muhallidin G Wali
        /// Date Created: 04/11/2013
        /// Description:  convert list to datatable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public static DataTable GetDataTable<T>(List<T> items)
        {
            var tb = new DataTable(typeof(T).Name);
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in props)
            {
                //Type t = GetCoreType(prop.PropertyType);
                //tb.Columns.Add(prop.Name, t);
                tb.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            foreach (T item in items)
            {
                var values = new object[props.Length];
                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }
                tb.Rows.Add(values);
            }
            return tb;
        }

        /// <summary>
        /// Author:       Muhallidin G Wali
        /// Date Created: 04/11/2013
        /// Description:  get item type
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Type GetCoreType(Type t)
        {
            if (t != null)
            {
                if (!t.IsValueType)
                {
                    return t;
                }
                else
                {
                    return Nullable.GetUnderlyingType(t);
                }
            }
            else
            {
                return t;
            }
        }

        public static   int GetdateDiff(DateTime dateFrom, DateTime dateTo)
        {
            int dateDiff = 0;
            System.DateTime dtdateFrom = new System.DateTime(dateFrom.Year, dateFrom.Month, dateFrom.Day, 12, 0, 0);
            System.DateTime dtdateTo = new System.DateTime(dateTo.Year, dateTo.Month, dateTo.Day, 12, 0, 0);

            System.TimeSpan diffResult = dtdateTo - dtdateFrom;

            System.TimeSpan diffResults = dateTo - dateFrom;


            //TimeSpan GiveTheTimeOfTheDay = dateFrom.Date - dateTo.Date;
            dateDiff = int.Parse(diffResult.Days.ToString());

            try
            {
                return dateDiff;
            }
            catch
            {
                return 0;
            }
        }
         
    }
}
