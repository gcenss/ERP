using HouseMIS.EntityUtils;
using HouseMIS.Web.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseMIS.Web.House
{
    public partial class AssignList : EntityListBase<h_Assign>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckRolePermission("查看"))
                Response.End();

            Dictionary<string, string> dys = ToolBar();
            //if (!CheckRolePermission("删除"))
            //{
            //    RemovBtns(dys, "delete");
            //}
            tBar = GetBtns(dys).ToString();

            if (!IsPostBack)
            {
            }
        }

        public string tBar = "";

        private Dictionary<string, string> RemovBtns(Dictionary<string, string> dys, string key)
        {
            dys.Remove(key);
            return dys;
        }

        private StringBuilder GetBtns(Dictionary<string, string> newdys)
        {
            StringBuilder sbBtns = new StringBuilder();
            foreach (KeyValuePair<string, string> kvp in newdys)
            {
                sbBtns.Append(kvp.Value);
            }
            return sbBtns;
        }

        private Dictionary<string, string> ToolBar()
        {
            List<string> bts = new List<string>();
            bts.Add("<li><a class='delete' href='House/AssignList.aspx?NavTabId=" + NavTabId + "&doAjax=true&doType=delall' rel='Followids' target='selectedTodo' title='确定要删除吗?'><span>删除</span></a></li>");
            Dictionary<string, string> dys = new Dictionary<string, string>();
            dys.Add("delete", bts[0]);
            return dys;
        }

        /// <summary>
        /// 重写查找条件
        ///  </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        protected override string GetWhereClauseFromSearchBar(string typeName)
        {
            StringBuilder sb = new StringBuilder();
            //sb.Append(base.GetWhereClauseFromSearchBar(typeName));
            sb.Append("1=1");
            sb.Append(" AND " + GetRolePermissionEmployeeIds("查看", "EmployeeID"));

            string temp = GetMySearchControlValue("Housecode");
            if (!temp.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" AND (OldShi_id LIKE '{0}%' or  NewShi_id LIKE '{0}%')", temp);
            }

            temp = GetMySearchControlValue("OutDate1");
            if (!temp.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" AND exe_Date >= '{0}'", temp);
            }

            temp = GetMySearchControlValue("OutDate2");
            if (!temp.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" AND exe_Date <= '{0}'", temp);
            }

            return sb.Length == 0 ? null : sb.ToString();
        }
    }
}