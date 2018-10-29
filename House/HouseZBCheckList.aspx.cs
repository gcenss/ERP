using HouseMIS.EntityUtils;
using HouseMIS.EntityUtils.DBUtility;
using HouseMIS.Web.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;

namespace HouseMIS.Web.House
{
    public partial class HouseZBCheckList : EntityListBase<h_houseinfor_ZBCheck>
    {
        public string toolStr()
        {
            StringBuilder sb = new StringBuilder();
            if (CheckRolePermission("总部认证"))
            {
                string[] state = Enum.GetNames(typeof(CheckState));
                for (int i = 1; i < state.Length; i++)
                {
                    sb.Append("<a class=\"iconL\" href=\"house/HouseZBCheckEdit.aspx?StateID=" + i + "&NavTabId=" + NavTabId + "&ID={ID}&EditType=Edit&doAjax=true\" width=\"805\" height=\"550\" target=\"dialog\" mask=\"true\" rel=\"" + NavTabId + "\" ><span>" + state[i] + "</span></a>");
                }
                //添加一个导入2手房的操作
                sb.Append("<li><a class=\"iconL\" href=\"house/HouseZBCheckEdit.aspx?StaTeID=1&isefw=1&NavTabId=" + NavTabId + "&ID={ID}&EditType=Edit&doAjax=true\"  width=\"850\" height=\"550\" target=\"dialog\" mask=\"true\" rel=\"" + NavTabId + "\" ><span>合格并导入e房网</span></a></li>");

                sb.Append("<li><a class=\"delete\" href=\"house/HouseZBCheckList.aspx?NavTabId=" + NavTabId + "&doAjax=true&doType=del\" rel=\"ids\" target=\"selectedTodo\" title=\"确定要删除吗?\"><span>删除</span></a></li>");
            }

            return sb.ToString();
        }

        protected override void OnInit(EventArgs e)
        {
            gv.RowDataBound += Gv_RowDataBound;
            if (!IsPostBack)
            {
                List<s_Organise> Organises = s_Organise.FindAllChildsByParent(0).FindAll(x => x.aType != 1);
                myffrmOrg_ZB.Items.Add(new ListItem("", ""));
                foreach (s_Organise Organise in Organises)
                {
                    myffrmOrg_ZB.Items.Add(new ListItem(Organise.TreeNodeName2 + "(" + Organise.BillCode + ")", Organise.OrgID.ToString()));
                }

                //是否审核
                myffrmddlAudit.Items.Clear();
                myffrmddlAudit.Items.Add("");
                myffrmddlAudit.Items.Add(new ListItem("未审核", "0"));
                myffrmddlAudit.Items.Add(new ListItem("已审核", "1"));

                //开盘录音
                myffrmPhoneID.Items.Clear();
                myffrmPhoneID.Items.Add("");
                myffrmPhoneID.Items.Add(new ListItem("无", "0"));
                myffrmPhoneID.Items.Add(new ListItem("有", "1"));

                //开盘凭证
                myffrmpicUrl.Items.Clear();
                myffrmpicUrl.Items.Add("");
                myffrmpicUrl.Items.Add(new ListItem("无", "0"));
                myffrmpicUrl.Items.Add(new ListItem("有", "1"));

                //是否删除
                myffrmIsDel.Items.Clear();
                myffrmIsDel.Items.Add("");
                myffrmIsDel.Items.Add(new ListItem("否", "0"));
                myffrmIsDel.Items.Add(new ListItem("是", "1"));

                //房源状态
                myffrmHouseState.Items.Clear();
                myffrmHouseState.DataSource = h_State.FindAllWithCache();
                myffrmHouseState.DataTextField = "Name";
                myffrmHouseState.DataValueField = "StateID";
                myffrmHouseState.DataBind();
                myffrmHouseState.Items.Insert(0, "");

                //委托类型
                myffrmEntrustTypeID.Items.Clear();
                myffrmEntrustTypeID.DataSource = h_EntrustType.FindAllWithCache();
                myffrmEntrustTypeID.DataTextField = "Name";
                myffrmEntrustTypeID.DataValueField = "EntrustTypeID";
                myffrmEntrustTypeID.DataBind();
                myffrmEntrustTypeID.Items.Insert(0, "");

                //默认值
                if (Current.RoleNames.Contains("信息"))
                {
                    myffrmddlAudit.SelectedIndex = 1;
                    myffrmPhoneID.SelectedIndex = 2;
                    myffrmHouseState.SelectedValue = "2";
                    myffrmIsDel.SelectedIndex = 1;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["doType"] != null)
                {
                    if (Request.QueryString["doType"] == "del")
                    {
                        string sql = string.Format(@"UPDATE h_houseinfor_zbcheck 
                                                        SET    isdel = 1, 
                                                               delempid = {1}, 
                                                               deldate = Getdate() 
                                                        WHERE  id IN( {0} ) ",
                                                        Request["ids"],
                                                        Current.EmployeeID);

                        DbHelperSQL.ExecuteSql(sql);

                        ShowMsg(AlertType.info, "删除成功", NavTabId, false);
                    }
                }
            }
        }

        private void Gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("ondblclick", "OpenZBCheck('" + gv.DataKeys[e.Row.RowIndex].Value + "')");
            }
        }

        protected override string GetWhereClauseFromSearchBar(string typeName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("1=1");
            if (!IsPostBack && Current.RoleNames.Contains("信息"))
            {
                //无审核人
                sb.AppendFormat(" and employee_auditID is null");
                //有开盘录音
                sb.AppendFormat(" and PhoneID is not null");
                //默认委托中
                sb.AppendFormat(@" and houseid in(select houseid from h_houseinfor
                                                    where StateID = 2)");
                //默认未删除
                sb.AppendFormat(@" and isdel=0");
            }

            sb.Append(" and " + GetRolePermissionEmployeeIds("查看", "employeeID"));

            string temp1 = GetMySearchControlValue("IsDel");
            //是否删除
            if (!temp1.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" and isDel={0}", temp1);
            }
            //房源编号
            temp1 = GetMySearchControlValue("shi_id");
            if (!temp1.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" and HouseID in (select houseid from h_houseinfor where shi_id like '%{0}%')", temp1);
            }
            //申请人门店
            temp1 = GetMySearchControlValue("Org_ZB");
            if (!temp1.IsNullOrWhiteSpace() && temp1 != "0")
            {
                sb.AppendFormat(@" and EmployeeID in (SELECT employeeid
                                                        FROM   e_Employee
                                                        WHERE  orgid IN(SELECT OrgID
                                                                         FROM   s_Organise
                                                                         WHERE  idpath LIKE '%,{0},%')
                                                        )",
                                                        temp1);
            }
            //申请人
            temp1 = GetMySearchControlValue("Employee");
            if (!temp1.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" and EmployeeID in (select EmployeeID from e_Employee where em_name like '%{0}%')", temp1);
            }
            //申请时间
            temp1 = GetMySearchControlValue("CreateTime");
            if (!temp1.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" and CONVERT(varchar(100), exe_Date, 23)='{0}'", temp1);
            }
            //审核人
            temp1 = GetMySearchControlValue("AuditEmp");
            if (!temp1.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" and employee_auditID in (select EmployeeID from e_Employee where em_name like '%{0}%')", temp1);
            }

            //审核时间
            temp1 = GetMySearchControlValue("AuditTime");
            if (!temp1.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" and CONVERT(varchar(100), audit_Date, 23)='{0}'", temp1);
            }

            //委托类型
            temp1 = GetMySearchControlValue("EntrustTypeID");
            if (!temp1.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(@" and houseid in(select houseid from h_houseinfor
                                                        where EntrustTypeID = {0}
                                                        and deltype=0)",
                                                        temp1);
            }

            ////是否删除
            //temp1 = GetMySearchControlValue("IsDel");
            //if (!temp1.IsNullOrWhiteSpace())
            //{
            //    sb.AppendFormat(" and isDel={0}", temp1);
            //}


            //是否已审核
            temp1 = GetMySearchControlValue("ddlAudit");
            if (!temp1.IsNullOrWhiteSpace())
            {
                if (temp1 == "0")
                {
                    sb.AppendFormat(" and employee_auditID is null");
                }
                else if (temp1 == "1")
                {
                    sb.AppendFormat(" and employee_auditID is not null");
                }
            }

            //开盘录音
            temp1 = GetMySearchControlValue("PhoneID");
            if (!temp1.IsNullOrWhiteSpace())
            {
                if (temp1 == "0")
                {
                    sb.AppendFormat(" and PhoneID is null");
                }
                else if (temp1 == "1")
                {
                    sb.AppendFormat(" and PhoneID is not null");
                }
            }

            //开盘凭证
            temp1 = GetMySearchControlValue("picUrl");
            if (!temp1.IsNullOrWhiteSpace())
            {
                if (temp1 == "0")
                {
                    sb.AppendFormat(" and picUrl is null");
                }
                else if (temp1 == "1")
                {
                    sb.AppendFormat(" and picUrl is not null");
                }
            }

            //房源状态
            temp1 = GetMySearchControlValue("HouseState");
            if (!temp1.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(@" and houseid in(select houseid from h_houseinfor
                                                        where StateID = {0}
                                                        and deltype=0)",
                                                    temp1);
            }

            return sb.ToString();
        }
    }
}