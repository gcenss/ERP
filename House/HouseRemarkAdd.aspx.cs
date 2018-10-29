using System;
using System.Data;
using System.Collections.Generic;
using Menu = HouseMIS.EntityUtils.Menu;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TFrameWork.Log;
using TFrameWork.Web;
using HouseMIS.EntityUtils;
using HouseMIS.Web.UI;
using TCode;
using System.Text;

namespace HouseMIS.Web.House
{
    public partial class HouseRemarkAdd : EntityFormBase<HouseMIS.EntityUtils.h_PicList_YY>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected override void OnSetForm(object sender, EntityFormEventArgs e)
        {
            if (!IsPostBack)
            {
                //修改
                if (Request["ID"] != null)
                {
                    frmRemark.Text = Entity.Remark;
                }
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
           
        }


        protected override void OnSaving(object sender, EntityFormEventArgs e)
        {
            base.OnSaving(sender, e);
        }

        private string Bright(string fileName, string attachmentPath, string size)
        {
            StringBuilder sbjson = new StringBuilder();
            sbjson.Append("{");
            sbjson.Append("\"id\":\"" + attachmentPath + "\",");
            sbjson.Append("\"fileName\":\"" + fileName + "\",");
            sbjson.Append("\"attachmentPath\":\"" + attachmentPath + "\",");
            sbjson.Append("\"attachmentSize\":\"" + size + "\"");
            sbjson.Append("}");
            return sbjson.ToString();
        }
        protected override void OnSaveSuccess(object sender, EntityFormEventArgs e)
        {
           
            base.OnSaveSuccess(sender, e);
        }
    }
}