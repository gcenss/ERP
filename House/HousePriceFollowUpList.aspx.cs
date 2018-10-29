using HouseMIS.EntityUtils;
using HouseMIS.EntityUtils.DBUtility;
using HouseMIS.Web.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HouseMIS.Web.House
{
    public partial class HousePriceFollowUpList : EntityListBase<h_PriceFollowUp>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FullCanViewOrgShopDropList(myffrmOrgID);
            }
        }

        protected override string GetWhereClauseFromSearchBar(string typeName)
        {
            string temp1 = string.Empty;
            StringBuilder sb = new StringBuilder();
            sb.Append(base.GetWhereClauseFromSearchBar(typeName));
            if (sb.ToString().IsNullOrWhiteSpace())
            {
                sb.Append("1=1");
            }

            //房源编号
            temp1 = GetMySearchControlValue("shi_id");
            if (!temp1.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" and HouseID in (select houseid from h_houseinfor where shi_id like '%{0}%')", temp1);
            }
            //门店
            temp1 = GetMySearchControlValue("OrgID");
            if (!temp1.IsNullOrWhiteSpace() && temp1.ToInt32() > 0)
            {
                sb.AppendFormat(@"AND 
                                    employeeid IN 
                                    ( 
                                               SELECT     a.employeeid 
                                               FROM       e_employee a 
                                               INNER JOIN s_organise b 
                                               ON         a.orgid = b.orgid 
                                               WHERE      b.idpath LIKE '%,{0},%')",
                                               temp1);
            }
            //压价人
            temp1 = GetMySearchControlValue("PriceEmpID");
            if (!temp1.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" and PriceEmpID in (select EmployeeID from e_Employee where em_name like '%{0}%')", temp1);
            }
            //压价日期开始
            temp1 = GetMySearchControlValue("AddDate_begin");
            if (!temp1.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" and AddDate>='{0}'", temp1);
            }
            //压价日期结束
            temp1 = GetMySearchControlValue("AddDate_end");
            if (!temp1.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" and AddDate<='{0} 23:59:59'", temp1);
            }

            return sb.ToString();
        }
    }
}