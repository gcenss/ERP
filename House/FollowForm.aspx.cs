using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HouseMIS.EntityUtils;

namespace HouseMIS.Web.House
{
    public partial class FollowForm : HouseMIS.Web.UI.EntityFormBase<h_FollowUp>
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 扩展表单执行事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnOtherSaveForm(object sender, EntityFormEventArgs e)
        {
            if (EntityForm.IsAjaxAction && EntityForm.AjaxAction == "delall")
            {
                if (!String.IsNullOrEmpty(Request["Followids"]))
                {
                    foreach (string s in Request["Followids"].Split(','))
                    {
                        //判断权限
                        if (!CheckRolePermission("删除", h_FollowUp.FindByFollowUpID(decimal.Parse(s)).EmployeeID))
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Deljs", "<script>alertMsg.error('你无权删除!');</script>");
                            return;
                        }
                        else
                        {
                            h_FollowUp.Delete(new String[] { "FollowUpID" }, new String[] { s });
                        }
                    }
                }
            }
            else
                base.OnOtherSaveForm(sender, e);
        }
    }
}