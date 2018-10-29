using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HouseMIS.Web.UI;
using HouseMIS.EntityUtils;

namespace HouseMIS.Web.House.Templete
{
    public partial class ShareTempleteForm : EntityFormBase<Share_Templete>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Type"] == "sc")
                {
                    Share_Collection sc = new Share_Collection();
                    sc.EmployeeID = Employee.Current.EmployeeID;
                    sc.TempleteID = Convert.ToInt32(Request.QueryString["TempleteID"]);
                    sc.AddTime = DateTime.Now;
                    sc.Save();
                    AlertMsg_Success("收藏成功");
                }
                else if (Request.QueryString["Type"] == "yc")
                {
                    Share_Collection sc = Share_Collection.Find("TempleteID =" + Request.QueryString["TempleteID"] + "");
                    sc.Delete();
                    AlertMsg_Success("移除成功");
                }
                else
                {
                    this.Type.Value = Request.QueryString["Type"];
                }
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
                Entity.ShareTotal = 0;
                Entity.AddDate = DateTime.Now;
                Entity.Type =Convert.ToInt32( this.Type.Value);
            }
            base.OnSaving(sender, e);
            //if (!string.IsNullOrEmpty(Request.Form["frmContents"]))
            //{
            //    if (Request.Form["frmContents"].Split("<img").Length <= 5)
            //    {
            //        e.Cancel = true;
            //    }
            //}
        }

        //protected override void OnSaveSuccess(object sender, EntityFormEventArgs e)
        //{
        //    //if (!string.IsNullOrEmpty(Request.Form["frmContents"]))
        //    //{
        //    //    if (Request.Form["frmContents"].Split("<img").Length > 5)
        //    //    {
        //    //        base.OnSaveSuccess(sender, e);
        //    //    }
        //    //    else
        //    //    {
        //    //        AlertMsg_Error("图片没有五张不能保存！");
        //    //    }
        //    //}
        //    if (Request.QueryString["doType"] == "del")
        //    {
        //        base.OnSaveSuccess(sender, e);
        //    }

            
        //}
    }
}