using HouseMIS.EntityUtils;
using HouseMIS.EntityUtils.DBUtility;
using HouseMIS.Web.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;

namespace HouseMIS.Web.House
{
    public partial class FollowAuditList : EntityListBase<h_FollowAudit>
    {
        protected string z_bottom = string.Empty;
        public string z_del = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                myffrmddlAudit.Items.Clear();
                myffrmddlAudit.Items.Add("");
                myffrmddlAudit.Items.Add(new ListItem("未审核", "0"));
                myffrmddlAudit.Items.Add(new ListItem("已审核", "1"));
                myffrmddlAudit.SelectedIndex = 1;
            }

            z_bottom = "<li><a class=\"iconL\" href=\"House/FollowAuditList.aspx?doType=upDateHouse&NavTabId=" + NavTabId + "&selectPage=" + (gv.PageIndex + 1).ToString() + "&doAjax=true&FollowAuditID={FollowAuditID}\" rel=\"ids\" target=\"selectedTodo\" title=\"确定要操作吗？\"><span>审核</span></a></li>";
            if (CheckRolePermission("删除"))
            {
                z_del = "<li><a class=\"delete\" href=\"House/FollowAuditList.aspx?doType=del&NavTabId=" + NavTabId + "&selectPage=" + (gv.PageIndex + 1).ToString() + "&doAjax=true&FollowAuditID={FollowAuditID}\" rel=\"ids\" target=\"selectedTodo\" title=\"确定要删除吗？\"><span>删除</span></a></li>";
            }

            if (Request.QueryString["doType"] != null)
            {
                //审核状态
                if (Request.QueryString["doType"].ToString() == "upDateHouse")
                {
                    if (!string.IsNullOrEmpty(Request["ids"]))
                    {
                        string sql = string.Format("select * from h_FollowAudit where FollowAuditID in({0})", Request["ids"]);
                        List<h_FollowAudit> list_h_FollowAudit = h_FollowAudit.FindAll(sql);
                        foreach (h_FollowAudit hfa in list_h_FollowAudit)
                        {
                            //房源跟进中增加修改状态
                            if (DbHelperSQL.ExecuteSql("exec dbo.h_update_HouseState " + hfa.StateID + ", " + hfa.HouseID + ", " + hfa.EmployeeID) > 0)
                            {
                                H_houseinfor hh = H_houseinfor.FindByHouseID(Convert.ToDecimal(hfa.HouseID));
                                //判断房源原先的状态（非委托中）改为委托中，并且当前日期已经大于房源的更新时间超过15天（不包括15天）
                                if (hh.StateID != 2 && hfa.StateID == 2 && (DateTime.Now - hh.Update_date).TotalDays > 15)
                                {
                                    //需要重新获取实体，因为在存储过程中，已经更新过
                                    hh = H_houseinfor.FindByHouseID(Convert.ToDecimal(hfa.HouseID));

                                    Employee emp = Employee.FindByEmployeeID(hfa.EmployeeID);

                                    //增加跟进记录
                                    h_FollowUp hsf = new h_FollowUp();
                                    hsf.EmployeeID = hfa.EmployeeID;
                                    hsf.HouseID = hfa.HouseID;
                                    hsf.FollowUpText = hfa.FollowText;
                                    hsf.exe_Date = hfa.CreateTime;
                                    hsf.Insert();

                                    //更新该房源的首录人，用于统计员工新增房源工作量
                                    hh.OwnerEmployeeID = Convert.ToInt32(emp.EmployeeID);
                                    hh.OperatorID = Convert.ToInt32(emp.EmployeeID);
                                    hh.OrgID = Convert.ToInt32(emp.OrgID);
                                    hh.Exe_date = hfa.CreateTime;
                                }

                                hh.Update_date = hfa.CreateTime;
                                hh.Update();

                                hfa.AuditEmpID = Current.EmployeeID.ToString().ToInt32();
                                hfa.AuditTime = DateTime.Now;
                                hfa.Update();
                            }
                        }
                    }
                }
                else if (Request.QueryString["doType"].ToString() == "del")
                {
                    if (!string.IsNullOrEmpty(Request["ids"]))
                    {
                        DbHelperSQL.ExecuteSql("delete from h_FollowAudit where FollowAuditID in(" + Request["ids"] + ")");
                    }
                }

                #region 刷新当前页[抓取当页导航数字onclick事件]

                string JavaScript = " dwzPageBreak({ targetType: \"navTab\", rel: \"\", data: { pageNum: " + Request["selectPage"] + "} });";
                JSDo_UserCallBack_Success(JavaScript, "操作成功");

                #endregion 刷新当前页[抓取当页导航数字onclick事件]
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
            temp1 = GetMySearchControlValue("HouseID");
            if (!temp1.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" and HouseID in (select houseid from h_houseinfor where shi_id like '%{0}%')", temp1);
            }
            //申请人
            temp1 = GetMySearchControlValue("Employee");
            if (!temp1.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" and EmployeeID in (select EmployeeID from e_Employee where em_name like '%{0}%')", temp1);
            }
            //审核人
            temp1 = GetMySearchControlValue("AuditEmp");
            if (!temp1.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" and AuditEmpID in (select EmployeeID from e_Employee where em_name like '%{0}%')", temp1);
            }
            //申请时间
            temp1 = GetMySearchControlValue("CreateTime");
            if (!temp1.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" and CONVERT(varchar(100), CreateTime, 23)='{0}'", temp1);
            }
            //审核时间
            temp1 = GetMySearchControlValue("AuditTime");
            if (!temp1.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" and CONVERT(varchar(100), AuditTime, 23)='{0}'", temp1);
            }
            //是否已审核
            temp1 = GetMySearchControlValue("ddlAudit");
            if (temp1 == "0")
            {
                sb.AppendFormat(" and AuditEmpID is null", temp1);
            }
            else if (temp1 == "1")
            {
                sb.AppendFormat(" and AuditEmpID is not null", temp1);
            }

            return sb.ToString();
        }
    }
}