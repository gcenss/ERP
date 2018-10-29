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
    public partial class HouseFindBack : EntityListBase<s_Seat>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["HouseDicID"] == "")
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("{\r\n");
                    sb.Append("   \"statusCode\":\"300\", \r\n");
                    sb.Append("   \"message\":\"请先选择楼盘！\", \r\n");
                    sb.Append("   \"navTabId\":\"" + NavTabId + "\", \r\n");
                    sb.Append("   \"rel\":\"1\", \r\n");
                    sb.Append("   \"forwardUrl\":\"\"\r\n");
                    sb.Append("}\r\n");
                    Response.Write(sb.ToString());
                    Response.End();
                }
            }
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
            if (Request.QueryString["HouseDicID"] != null)
            {
                sb.Append("HouseDicID=" + Request.QueryString["HouseDicID"]);
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
                HouseMIS.EntityUtils.s_Seat drv = ((HouseMIS.EntityUtils.s_Seat)e.Row.DataItem);
                e.Row.Attributes.Add("ondblclick", "$.bringBack({frmbuild_id:'" + drv.SeatName + "',SeatID:'" + drv.SeatID + "'})");
            }
        }
    }
}