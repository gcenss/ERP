using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HouseMIS.EntityUtils;
using HouseMIS.Web.UI;
using System.Text;

namespace HouseMIS.Web.House.Templete
{
    public partial class ShareTempleteList1 : EntityListBase<HouseMIS.EntityUtils.Share_Templete>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ltlToolBar.Text = GetBtn();
                this.Type.Value = Request.QueryString["Type"];
            }
        }

        private string GetBtn()
        {
            Dictionary<string, string> str = new Dictionary<string, string>();

            if (CheckRolePermission("私有"))                //包括私有所有操作，不细分
            {
                str.Add("symy", "<li><a class=\"add\" href=\"House/Templete/ShareTempleteList1.aspx?NavTabId=" + NavTabId + "&doAjax=true&Type=1\"  target=\"navtab\" rel=\"357_menu3005\"  ><span>私有模板</span></a></li>");
                str.Add("sytj", "<li><a class=\"edit\" href=\"House/Templete/ShareTempleteForm.aspx?NavTabId=" + NavTabId + "&doAjax=true&Type=1\"  width=\"800\" height=\"650\" target=\"dialog\" maxable='false' title=\"添加私有模板\"><span>添加私有</span></a></li>");
            }
            if (CheckRolePermission("添加"))
            {
                str.Add("tj", "<li><a class=\"add\" href=\"House/Templete/ShareTempleteForm.aspx?NavTabId=" + NavTabId + "&doAjax=true&Type=0\"  width=\"800\" height=\"650\" target=\"dialog\" maxable='false' title=\"添加公共模板\"><span>添加公共</span></a></li>");
            }
            if (CheckRolePermission("修改"))
            {
                str.Add("xg", "<li><a class=\"edit\" href=\"House/Templete/ShareTempleteForm.aspx?NavTabId=" + NavTabId + "&doAjax=true&ID={id}\" width=\"800\" height=\"650\" target=\"dialog\" title=\"修改公共模板\" maxable='false'><span>修改</span></a></li>");
            }
            if (CheckRolePermission("删除"))
            {
                str.Add("sc", "<li><a class=\"delete\" href=\"House/Templete/ShareTempleteForm.aspx?NavTabId=" + NavTabId + "&doAjax=true&doType=del&ID={id}\" rel=\"PortID\" target=\"ajaxTodo\" title=\"确定要删除吗?\"><span>删除</span></a></li>");
            }

            str.Add("mds", "<li><a class=\"edit\" href=\"House/Templete/ShareTempleteList1.aspx?NavTabId=" + NavTabId + "&doAjax=true&Type=0\" target=\"navtab\" rel=\"357_menu3005\"  ><span>公共模板</span></a></li>");

          

            StringBuilder sb = new StringBuilder();
            foreach (string i in str.Values)
            {
                sb.Append(i);
            }

            return sb.ToString();
        }


        protected override string GetWhereClauseFromSearchBar(string typeName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.GetWhereClauseFromSearchBar(typeName));

            if (sb.Length > 0)
                sb.Append(" AND ");
            sb.Append(" ShareType not in ('公众号图片','公众号简介') ");

            if (!Request.Form["ffrmTitle"].IsNullOrWhiteSpace())
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");

                sb.Append(" Title like '%" + HouseMIS.EntityUtils.StringHelper.Filter(Request.Form["ffrmTitle"]) + "%'");
            }

            if (!string.IsNullOrEmpty(Request.Form["ffrmShareType"]))
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.Append(" ShareType = '" + Request.Form["ffrmShareType"] + "'");

            }
            if (!string.IsNullOrEmpty(Request.Form["Name"]))
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.Append(" EmployeeID in (select EmployeeID from e_Employee where em_name like '%" + Request.Form["Name"] + "%')");
            }
            if (!string.IsNullOrEmpty(Request.Form["start"]))
            {
                string d1 = Request.Form["start"].ToDate().ToString("yyyy-MM-dd") + " 00:00:00";
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.Append(" AddDate >= '" + d1 + "'");
            }
            if (!string.IsNullOrEmpty(Request.Form["end"]))
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                string d2 = Request.Form["end"].ToDate().ToString("yyyy-MM-dd ") + " 23:59:59";
                sb.Append(" AddDate <= '" + d2+ "'");
            }
            if (!Request.Form["ShareTempleteList_OrgID"].IsNullOrWhiteSpace())
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat("s.OrgID={0} ", HouseMIS.EntityUtils.StringHelper.Filter(Request.Form["ShareTempleteList_OrgID"]));
            }


            string Type = Request.QueryString["Type"] == null ? Request.Form["Type"] : Request.QueryString["Type"];
            //公共的
            if (Type == "0")
            {
                if (sb.Length > 0)
                    sb.Append("and");
                sb.Append(" Type=" + Type + "");
            }
            //私有的分权限
            if (Type == "1")
            {
                if (sb.Length > 0)
                    sb.Append("and");
                sb.Append(" Type=" + Type + " and ");

                sb.Append(GetRolePermissionEmployeeIds("查看", "a.EmployeeID"));
            }

            return sb.Length == 0 ? null : sb.ToString();
            
        }



      
    }
}