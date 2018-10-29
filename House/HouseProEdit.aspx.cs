using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HouseMIS.Web.UI;
using HouseMIS.EntityUtils;

namespace HouseMIS.Web.House
{
    public partial class HouseProEdit : EntityFormBase<h_houseinfor_Problem>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected override void OnSaving(object sender, EntityFormEventArgs e)
        {
            if (Entity.hProID > 0)
            {
                Entity.updateDate = DateTime.Now;
                if (Request.Form["frmremark"] != null && Request.Form["frmremark"] != string.Empty)
                {
                    Entity.remark = Request.Form["frmremark"].ToString();
                    Entity.isFinish = "1";
                }
                Entity.employeeID_Finish = Request.Form["employeeID_Finish"].ToInt32();
            }
            else
            {
                if (Request.Form["houseid"] != null)
                {
                    Entity.houseID = Request.Form["houseid"].ToInt32();
                }
                //质疑人
                Entity.employeeID = Current.EmployeeID.ToString().ToInt32();
                //被质疑人
                Entity.employeeID_Pro = Request.Form["employeeID_Pro"].ToInt32();
                //是否处理
                Entity.isFinish = "0";
                Entity.exe_Date = DateTime.Now;
            }

            base.OnSaving(sender, e);
        }

        protected override void OnSetForm(object sender, EntityFormEventArgs e)
        {
            H_houseinfor hh = H_houseinfor.FindByHouseID(Entity.houseID);
            houseid.Value = hh.HouseID.ToString();
            employeeID_Pro.Value = hh.OwnerEmployeeID.ToString();

            if (hh != null)
            {
                frmshi_id.Text = hh.Shi_id;
            }

            if (Entity.hProID > 0)
            {
                //房源核验
                frmh_Check.Checked = Entity.h_Check.ToBool();
                //户型图
                frmh_hxt.Checked = Entity.h_hxt.ToBool();
                //室内外图
                frmh_pic.Checked = Entity.h_pic.ToBool();

                employeeID_Finish.Value = Current.EmployeeID.ToString();
                Employee ee = Employee.FindByEmployeeID(Current.EmployeeID);
                frmemployeeID_FinishName.Text = ee.Em_name;

                tr1.Visible = tr2.Visible = true;
            }
            else
                tr1.Visible = tr2.Visible = false;

            base.OnSetForm(sender, e);
        }
    }
}