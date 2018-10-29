using System;
using System.Collections.Generic;
using System.Web;
using HouseMIS.EntityUtils;
using System.Text;

namespace HouseMIS.Web.House.Templete
{
    /// <summary>
    /// SharePost 的摘要说明
    /// </summary>
    public class SharePost : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            if (!string.IsNullOrEmpty(context.Request.QueryString["HouseID"]) && !string.IsNullOrEmpty(context.Request.QueryString["TempleteID"]))
            {
                try
                {
                    Share_House sh = new Share_House();
                    sh.EmployeeID = Convert.ToInt32(Employee.Current.EmployeeID);
                    sh.HouseID = context.Request.QueryString["HouseID"].ToInt32();
                    sh.TempleteID = context.Request.QueryString["TempleteID"].ToInt32();

                    Share_Templete st = Share_Templete.FindByID(sh.TempleteID);
                    sh.Title = st.Title;
                    sh.TelTotal = 0;
                    sh.Contents = st.Contents;
                    sh.Orgid = Employee.Current.OrgID;
                    sh.Name = Employee.Current.OrgName;
                    sh.ForwardTotal = st.ShareTotal + 1;
                    sh.SearchTotal = 0;
                    sh.LookTotal = 0;
                    sh.SourceType = 0;        //0 是pc 1是手机
                    sh.AddTime = DateTime.Now;
                 
                    if (sh.Save() > 0)
                    {
                        st.ShareTotal = st.ShareTotal + 1;
                        st.Update();                        //保存成功更新转发次数

                        string strsql = " update Share_House set ForwardTotal=" + st.ShareTotal + " where TempleteID =" + sh.TempleteID + "";
                        HouseMIS.EntityUtils.DBUtility.DbHelperSQL.ExecuteSql(strsql);
                        
                        StringBuilder Script = new StringBuilder();
                        string url = "http://share.efw.cn/ShareIndex-" + sh.ID + ".htm";
                        context.Response.Write(url); 
                    }
                }
                catch
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