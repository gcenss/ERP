using System;
using System.Collections.Generic;
using TCode;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HouseMIS.Web.UI;
using HouseMIS.EntityUtils;
using System.Text;
namespace HouseMIS.Web.House
{
    public partial class RecordList : EntityListBase<h_RecordClose>
    {
        protected StringBuilder EmpStr = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (CheckRolePermission("删除"))
            {
                EmpStr.Append("  <li><a class=\"delete\" href=\"House/RecordList.aspx?NavTabId="+NavTabId+"&doAjax=true&doType=delall&RecordCloseID={RecordCloseID}\" target=\"ajaxTodo\" title=\"删除吗?\"><span>删除</span></a></li>");
            }
            // 审核通过
            if (Request.QueryString["doType"] != null && Request.QueryString["doType"].ToString() == "CheckPass")
            {
                h_RecordClose ii = h_RecordClose.Find("RecordCloseID", Request.QueryString["RecordCloseID"].ToString());
                ii.CheckEmployeeID = Employee.Current.EmployeeID;
                ii.IsCheck = true;
                ii.CheckDate = DateTime.Now;
                ii.Update();
                JSDo_UserCallBack_Success(" formFind();$(\".RDpagerForm:eq(0)\").submit();", "操作成功！");
            }
            // 审核不通过
            else  if (Request.QueryString["doType"] != null && Request.QueryString["doType"].ToString() == "CheckRefuse")
            {
                h_RecordClose ii = h_RecordClose.Find("RecordCloseID", Request.QueryString["RecordCloseID"].ToString());
                ii.CheckEmployeeID = Employee.Current.EmployeeID;
                ii.IsCheck = false;
                ii.CheckDate = DateTime.Now;
                ii.Update();
                JSDo_UserCallBack_Success(" formFind();$(\".RDpagerForm:eq(0)\").submit();", "操作成功！");
            } 
            // 删除
            else if (Request.QueryString["doType"] != null && Request.QueryString["doType"].ToString() == "delall")
            {
                h_RecordClose ii = h_RecordClose.Find("RecordCloseID", Request.QueryString["RecordCloseID"].ToString());
                ii.Delete();
                JSDo_UserCallBack_Success(" formFind();$(\".RDpagerForm:eq(0)\").submit();", "操作成功");
            }
        }
        private void FullDropListData(Type aType, DropDownList d, string Name, string Value, string key, string keyValue)
        {
            var op = EntityFactory.CreateOperate(aType);
            IEntityList ls = null;
            if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(keyValue))
            {
                ls = op.FindAll(key, keyValue);
            }
            else
            {
                ls = op.FindAll();
            }
            d.Items.Add(new ListItem("-请选择-", ""));
            for (int i = 0; i < ls.Count; i++)
            {
                if (ls[i][Name].ToString() != "0")
                {
                    d.Items.Add(new ListItem(ls[i][Name].ToString(), ls[i][Value].ToString()));
                }
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            FullCanViewOrgShopDropList(myffrmRecordOrgID, null);
            myffrmRecordOrgID.Items.Insert(0, new ListItem("", ""));
            FullAreaDropListData(myffrmAreaID, "-请选择-", "");
        }
        protected override string GetWhereClauseFromSearchBar(string typeName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.GetWhereClauseFromSearchBar(typeName));
            if (!GetMySearchControlValue("AreaID").IsNullOrWhiteSpace())
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat("phoneID in (select phoneID from i_InternetPhone where houseID in (select HouseID from h_houseinfor where SanjakID in (select SanjakID from s_Sanjak where AreaID={0})))", HouseMIS.EntityUtils.StringHelper.Filter(GetMySearchControlValue("AreaID")));
            }
            if (!GetMySearchControlValue("OutDate1").IsNullOrWhiteSpace())
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat(" exe_date>='{0}' ", HouseMIS.EntityUtils.StringHelper.Filter(GetMySearchControlValue("OutDate1")));
            }
            if (!GetMySearchControlValue("OutDate2").IsNullOrWhiteSpace())
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat(" exe_date<='{0} 23:59:59' ", HouseMIS.EntityUtils.StringHelper.Filter(GetMySearchControlValue("OutDate2")));
            }
            if (!GetMySearchControlValue("CheckDate1").IsNullOrWhiteSpace())
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat(" CheckDate>='{0}' ", HouseMIS.EntityUtils.StringHelper.Filter(GetMySearchControlValue("CheckDate1")));
            }
            if (!GetMySearchControlValue("CheckDate2").IsNullOrWhiteSpace())
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat(" CheckDate<='{0} 23:59:59' ", HouseMIS.EntityUtils.StringHelper.Filter(GetMySearchControlValue("CheckDate2")));
            }
            if (!GetMySearchControlValue("name").IsNullOrWhiteSpace())
            {
                if (sb.Length > 0)
                {
                    sb.Append(" AND ");
                }
                sb.AppendFormat(" EmployeeID in (select EmployeeID from e_Employee where em_id='{0}')", HouseMIS.EntityUtils.StringHelper.Filter(GetMySearchControlValue("name")));
            }
            if (!GetMySearchControlValue("RecordOrgID").IsNullOrWhiteSpace())
            {
                if (sb.Length > 0)
                {
                    sb.Append(" AND ");
                }
                sb.AppendFormat(" EmployeeID in (select EmployeeID from e_Employee where OrgID={0})", HouseMIS.EntityUtils.StringHelper.Filter(GetMySearchControlValue("RecordOrgID")));
            }
            if (!GetMySearchControlValue("Emid").IsNullOrWhiteSpace())
            {
                if (sb.Length > 0)
                {
                    sb.Append(" AND ");
                }
                sb.AppendFormat(" phoneID in (select phoneID from i_InternetPhone where EmployeeID in (select EmployeeID from e_Employee where em_id='{0}'))", HouseMIS.EntityUtils.StringHelper.Filter(GetMySearchControlValue("Emid")));
            }
            //if (GetMySearchControlValue("IsCheck") == "on")
            //{
            //    if (sb.Length > 0)
            //        sb.Append(" AND ");
            //    sb.AppendFormat(" IsCheck=1 ");
            //}
            return sb.ToString();
        }
    }
}