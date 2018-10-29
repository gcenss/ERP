using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HouseMIS.Web.UI;
using HouseMIS.EntityUtils;

namespace HouseMIS.Web.House
{
    public partial class ShareEmployeeForm : EntityFormBase<Share_Personinfo>
    {
        protected override void OnInitComplete(EventArgs e)
        {
            Entity = Share_Personinfo.Find(Share_Personinfo._.EmployeeID, Employee.Current.EmployeeID);
            base.OnInitComplete(e);

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Share_Personinfo model = Share_Personinfo.Find(Share_Personinfo._.EmployeeID, Employee.Current.EmployeeID);
            if (!IsPostBack)
            {
                if (model == null)
                {
                    frmEName.Text = Employee.Current.Em_name;
                    frmEPhone.Text = Employee.Current.Tel;
                }
            }
            
        }

        protected override void OnSetForm(object sender, EntityFormEventArgs e)
        {
            base.OnSetForm(sender, e);
            frmEmployeeID.Value = Employee.Current.EmployeeID.ToString();
        }

        protected override void OnSaveSuccess(object sender, EntityFormEventArgs e)
        {
            JSDo_UserCallBack_Success("", "操作成功！",false);
            return;
            //base.OnSaveSuccess(sender, e);
        }

    }
}