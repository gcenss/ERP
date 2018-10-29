using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HouseMIS.Web.UI;
using HouseMIS.EntityUtils;
using System.Text;

namespace HouseMIS.Web.House
{
    public partial class HouseProList : EntityListBase<h_houseinfor_Problem>
    {
        protected StringBuilder EmpStr = new StringBuilder();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (CheckRolePermission("修改", RangeType.None))
            {
                EmpStr.Append("<li><a class=\"edit\" href=\"House/HouseProEdit.aspx?NavTabId=" + NavTabId + "&doAjax=true&hProID={hProID}\" width=\"400\" height=\"470\" target=\"dialog\" rel='HouseProEdit' maxable=\'false\'><span>修改</span></a></li>\n");
            }
        }

        protected override string GetWhereClauseFromSearchBar(string typeName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("1=1");

            //如果不是信息部，则只能查看自己质疑和被质疑的房源列表
            if (!Current.RoleNames.Contains("信息"))
            {
                sb.Append(" and (employeeID=" + Current.EmployeeID + " or employeeID_Pro=" + Current.EmployeeID + ")");
            }

            //房源编号
            string temp = GetMySearchControlValue("shi_id");
            sb.Append(" and houseid in (select houseid from h_houseinfor where shi_id like '%" + temp + "%')");

            //质疑人
            temp = GetMySearchControlValue("employeeName");
            sb.Append(" and employeeID in (select employeeID from e_Employee where em_name like '%" + temp + "%')");

            //被质疑人
            temp = GetMySearchControlValue("employee_ProName");
            sb.Append(" and employeeID_Pro in (select employeeID from e_Employee where em_name like '%" + temp + "%')");

            //是否处理
            temp = GetMySearchControlValue("isFinish");
            if (!temp.IsNullOrWhiteSpace())
            {
                sb.Append(" and isFinish='" + temp + "'");
            }

            //房源核验
            temp = GetMySearchControlValue("h_Check");
            if (!temp.IsNullOrWhiteSpace() && temp != "0")
            {
                sb.Append(" and h_Check='" + temp + "'");
            }

            //户型图
            temp = GetMySearchControlValue("h_hxt");
            if (!temp.IsNullOrWhiteSpace() && temp != "0")
            {
                sb.Append(" and h_hxt='" + temp + "'");
            }

            //室内外图
            temp = GetMySearchControlValue("h_pic");
            if (!temp.IsNullOrWhiteSpace() && temp != "0")
            {
                sb.Append(" and h_pic='" + temp + "'");
            }

            //质疑房源
            temp = GetMySearchControlValue("ZY");
            //我的质疑
            if (temp == "1")
            {
                sb.Append(" and employeeID=" + Current.EmployeeID);
            }
            //我的被质疑
            if (temp == "2")
            {
                sb.Append(" and employeeID_Pro=" + Current.EmployeeID);
            }

            return sb.Length == 0 ? null : sb.ToString();
        }
    }
}