using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HouseMIS.Web.UI;
using HouseMIS.EntityUtils;

namespace HouseMIS.Web.House
{
    public partial class DoorBack : EntityListBase<s_Door>
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 页面加载前加入双击事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            gv.RowCreated += new GridViewRowEventHandler(gv_RowCreated);
        }
        protected override string GetWhereClauseFromSearchBar(string typeName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.GetWhereClauseFromSearchBar(typeName));
            if (Request.QueryString["UnitID"] != null)
            {
                sb.Append("UnitID=" + Request.QueryString["UnitID"]);
            }
            return sb.Length == 0 ? null : sb.ToString();
        }

        /// <summary>
        /// 绑定行双击带回事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void gv_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HouseMIS.EntityUtils.s_Door drv = ((HouseMIS.EntityUtils.s_Door)e.Row.DataItem);
                e.Row.Attributes.Add("ondblclick", "$.bringBack({frmbuild_room:'" + drv.Room + "',RoomID:'" + drv.DoorID + "'})");
            }
        }
    }
}