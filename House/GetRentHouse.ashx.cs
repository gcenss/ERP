using System;
using System.Collections.Generic;
using System.Web;
using HouseMIS.EntityUtils;

namespace HouseMIS.Web.House
{
    /// <summary>
    /// GetRentHouse 的摘要说明
    /// </summary>
    public class GetRentHouse : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            if (context.Request["HouseID"] != null && context.Request["HouseID"] != "0")
            {
                try
                {
                    H_houseinfor hh = H_houseinfor.FindByHouseID(Convert.ToDecimal(context.Request["HouseID"]));
                    bool igr = false;
                    if (context.Request["num"].ToString() == "1")
                    {
                        igr = true;
                    }
                    hh.IsGetRent = igr;
                    hh.Update_date = DateTime.Now;
                    hh.Update();
                    context.Response.Write("1");
                }
                catch
                {
                    context.Response.Write("2");
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}