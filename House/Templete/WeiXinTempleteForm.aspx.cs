using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HouseMIS.Web.UI;
using HouseMIS.EntityUtils;

namespace HouseMIS.Web.House.Templete
{
    public partial class WeiXinTempleteForm : EntityFormBase<Share_Templete>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
        }


        protected override void OnSaving(object sender, EntityFormEventArgs e)
        {
            if (Entity.ID > 0)
            {

            }
            else                          //添加
            {
                
                Entity.EmployeeID = Employee.Current.EmployeeID;
                Entity.AddDate = DateTime.Now;
            }
            base.OnSaving(sender, e);
            
        }

        
    }
}