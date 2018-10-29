using System;
using System.Web;
using HouseMIS.EntityUtils;

namespace HouseMIS.Web.House
{
    /// <summary>
    /// HouseRepeatExt 的摘要说明
    /// </summary>
    public class HouseRepeatExt : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string html = context.Request["HTML"].Replace(",", " and");

            if (html.IndexOf("Build_area") > 0)
            {
                if (context.Request["Build_area"] != null)
                {
                    html = html.Replace("@Build_area", EntityUtils.StringHelper.Filter(context.Request["Build_area"]));
                }
            }
            if (html.IndexOf("sum_price") > 0)
            {
                if (context.Request["sum_price"] != null)
                {
                    html = html.Replace("@sum_price", EntityUtils.StringHelper.Filter(context.Request["sum_price"]));
                }
            }
            if (html.IndexOf("HouseDicName") > 0)
            {
                if (context.Request["HouseDicName"] != null)
                {
                    html = html.Replace("@HouseDicName", "'" + EntityUtils.StringHelper.Filter(context.Request["HouseDicName"]) + "'");
                }
            }
            if (html.IndexOf("HouseDicAddress") > 0)
            {
                //html = html.Replace("@HouseDicAddress", "'" + HouseMIS.EntityUtils.StringHelper.Filter(context.Request["HouseDicAddress"]) + "'");
                html = html.Replace("HouseDicAddress=@HouseDicAddress", "");
            }
            if (html.IndexOf("build_id") > 0)
            {
                //判断栋座去除0后长度是否为空
                if (context.Request["build_id"] != null &&
                    context.Request["build_id"].TrimStart('0').Length > 0)
                {
                    html = html.Replace("@build_id", "'" + context.Request["build_id"].TrimStart('0') + "'");
                }
                else
                {
                    html = html.Replace("@build_id", "'0'");
                }
            }
            if (html.IndexOf("build_unit") > 0)
            {
                html = html.Replace("@build_unit", "'" + EntityUtils.StringHelper.Filter(context.Request["build_unit"]) + "'");
            }
            if (html.IndexOf("build_room") > 0)
            {
                //判断室号去除0后长度是否为空
                if (context.Request["build_room"] != null &&
                    context.Request["build_room"].TrimStart('0').Length > 0)
                {
                    html = html.Replace("@build_room", "'" + context.Request["build_room"].TrimStart('0') + "'");
                }
                else
                {
                    html = html.Replace("@build_room", "'0'");
                }
            }
            if (html.IndexOf("landlord_name") > 0)
            {
                html = html.Replace("@landlord_name", "'" + EntityUtils.StringHelper.Filter(context.Request["landlord_name"]) + "'");
            }
            if (html.IndexOf("landlord_tel2") > 0)
            {
                if (context.Request["landlord_tel2"] == "" ||
                    context.Request["landlord_tel2"].IndexOf("点击") > -1 ||
                    context.Request["landlord_tel2"].IndexOf("拨") > -1)
                {
                    html = html.Replace("landlord_tel2=@landlord_tel2", "1=1");
                }
                else
                {
                    string ids = H_houseinfor.FindHouseIDsByTel(context.Request["landlord_tel2"].ToString());
                    if (ids.IsNullOrWhiteSpace())
                    {
                        html = html.Replace("landlord_tel2=@landlord_tel2", "1=1");
                    }
                    else
                        html = html.Replace("landlord_tel2=@landlord_tel2", "HouseID in (" + ids + ")");
                }
            }

            if (int.Parse(context.Request["HouseID"]) > 0)
            {
                H_houseinfor hh = H_houseinfor.FindByHouseID(Convert.ToDecimal(context.Request["HouseID"]));

                if (context.Request["build_id"] == "")
                {
                    html = html.Replace("@build_id", "'" + hh.Build_id + "'");
                }
                if (context.Request["build_room"] == "")
                {
                    html = html.Replace("@build_room", "'" + hh.Build_room + "'");
                }
                if (context.Request["build_unit"] == "")
                {
                    html = html.Replace("@build_unit", "'" + hh.Build_unit + "'");
                }
                html += " and HouseID<>" + context.Request["HouseID"];
            }

            if (context.Request["aType"] != null)
            {
                //判断小区+地址+楼栋号+室号是否重复，并且不是回收站房源，新增修改重复时不允许录入或者修改
                string sql = "select count(1) from h_houseinfor where " + html + " and DelType=0 and aType=" + context.Request["aType"];
                int bal = Convert.ToInt32(EntityUtils.DBUtility.DbHelperSQL.GetSingle(sql));
                if (bal > 0)
                {
                    context.Response.Write("1");
                }
                else
                {
                    context.Response.Write("0");
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