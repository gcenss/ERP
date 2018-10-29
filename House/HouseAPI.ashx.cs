using System;
using System.Collections.Generic;
using System.Web;
using HouseMIS.EntityUtils;
using System.Text;
namespace HouseMIS.Web.House
{
    /// <summary>
    /// HouseAPI 的摘要说明
    /// </summary>
    public class HouseAPI : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            if (context.Request["HouseDicID"] != null)
            {
                context.Response.Write(H_houseinfor.GetHouseDicInfo(Convert.ToDecimal(context.Request.QueryString["HouseDicID"])));
            }
            if (context.Request["HouseID"] != null)
            {
                context.Response.Write(GetHTML(context.Request["HouseID"].ToString()));
            }
        }
        public string GetHTML(string key)
        {
            StringBuilder sb = new StringBuilder();
            List<h_FollowUp> H_FollowUps = h_FollowUp.FindAll("select top 3 * from h_FollowUp where HouseID=" + key + " order by exe_date desc");
            foreach (h_FollowUp hr in H_FollowUps)
            {
                sb.Append("<tr target=\"KeyValue\" rel=\"\">");
                sb.Append("<td>" + hr.FollowUpType + "</td>");
                sb.Append("<td>" + hr.FollowUpText + "</td>");
                sb.Append("<td>" + hr.EmployeeName + "</td>");
                sb.Append("<td>" + hr.exe_Date + "</td>");
                sb.Append("</tr>");
            }
            return sb.ToString();
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