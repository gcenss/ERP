using HouseMIS.EntityUtils;
using HouseMIS.Web.UI;
using System;

namespace HouseMIS.Web.House
{
    public partial class HouseRecordFrom : EntityFormBase<h_RecordClose>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["phoneID"] == null)
            {
                Response.End();
            }
            else
            {
                frmphoneID.Value = Request.QueryString["phoneID"].ToString();
            }
            if (Request.QueryString["id"] == null)
            {
                Response.End();
            }
            else
            {
                frmContorlID.Value = Request.QueryString["id"].ToString();
            }
        }

        public string CID = "";

        protected override void OnSaving(object sender, EntityUtils.EntityFormEventArgs e)
        {
            Entity.EmployeeID = Employee.Current.EmployeeID;
            Entity.CheckEmployeeID = Employee.Current.EmployeeID;
            Entity.CheckDate = DateTime.Now;
            Entity.IsCheck = true;

            if (Request.Form["frmContorlID"] != "")
            {
                CID = Request.Form["frmContorlID"].ToString();
            }
            base.OnSaving(sender, e);
        }

        protected override void OnSaveSuccess(object sender, EntityUtils.EntityFormEventArgs e)
        {
            JSDo_UserCallBack_Success("$(\"#" + CID + "\").hide();$.pdialog.closeCurrent(); ", "操作成功！", false);
            return;
        }
    }
}