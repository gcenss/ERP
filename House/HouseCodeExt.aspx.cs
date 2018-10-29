using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HouseMIS.EntityUtils;
using HouseMIS.Web.UI;

namespace HouseMIS.Web.House
{
    public partial class HouseCodeExt : EntityListBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["type"] != null)
            {

                H_houseinfor hh = H_houseinfor.Find("shi_id='" + Request.Form["Shi_id"] + "' and " + GetRolePermissionOrgIds("查看", "OrgID"));
                if (hh != null)
                {
                    hh.Shi_id = Request.Form["NewShi_id"];
                    if (H_houseinfor.FindCount("Shi_id", hh.Shi_id) == 0)
                    {
                        hh.Update();
                        //插入日志
                        Log log = new Log();
                        log.Action = "修改";
                        log.Category = "房源编号修改";
                        log.IP = HttpContext.Current.Request.UserHostAddress;
                        log.OccurTime = DateTime.Now;
                        log.UserID = Convert.ToInt32(HouseMIS.EntityUtils.Employee.Current.EmployeeID);
                        log.UserName = HouseMIS.EntityUtils.Employee.Current.Em_name;
                        log.Remark = "原房源编号=" + Request.Form["Shi_id"] + ",新房源编号=" + Request.Form["NewShi_id"];
                        log.Insert();

                        JSDo_UserCallBack_Success("", "操作成功!");
                    }
                    else
                    {
                        JSDo_UserCallBack_Error("", "操作失败!");
                    }
                }
                else
                {
                    JSDo_UserCallBack_Error("", "您没有操作该房源的权限!");
                }
            }

        }
    }
}