﻿using HouseMIS.EntityUtils;
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
    public partial class FollowComplaintList : EntityListBase<h_ComplaintList>
    {
        protected string z_bottom = string.Empty;
        public string z_del = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                myffrmHandlingResults.Items.Clear();
                myffrmHandlingResults.Items.Add(new ListItem("全部", "2"));
                myffrmHandlingResults.Items.Add(new ListItem("未审核", "0"));
                myffrmHandlingResults.Items.Add(new ListItem("已审核", "1"));

                if (!GetMySearchControlValue("HandlingResults").IsNullOrWhiteSpace())
                    myffrmHandlingResults.SelectedValue = GetMySearchControlValue("HandlingResults");
                else
                    myffrmHandlingResults.SelectedIndex = 1;

                myffrmstate.Items.Clear();
                myffrmstate.Items.Add(new ListItem("全部", "2"));
                myffrmstate.Items.Add(new ListItem("确认", "0"));
                myffrmstate.Items.Add(new ListItem("投诉", "1"));

                if (!GetMySearchControlValue("state").IsNullOrWhiteSpace())
                    myffrmstate.SelectedValue = GetMySearchControlValue("state");
                else
                    myffrmstate.SelectedIndex = 2;
            }

            z_bottom = "<li><a class=\"iconL\" href=\"House/FollowComplaintList.aspx?Type=1&doType=upDateHouse&NavTabId=" + NavTabId + "&selectPage=" + (gv.PageIndex + 1).ToString() + "&doAjax=true&FollowAuditID={FollowAuditID}\" rel=\"ids\" target=\"selectedTodo\" title=\"确定要操作吗？\"><span>举报通过</span></a></li>";
            z_bottom += "<li><a class=\"iconL\" href=\"House/FollowComplaintList.aspx?Type=0&doType=upDateHouse&NavTabId=" + NavTabId + "&selectPage=" + (gv.PageIndex + 1).ToString() + "&doAjax=true&FollowAuditID={FollowAuditID}\" rel=\"ids\" target=\"selectedTodo\" title=\"确定要操作吗？\"><span>举报不通过</span></a></li>";
            if (CheckRolePermission("删除"))
            {
                z_del = "<li><a class=\"delete\" href=\"House/FollowComplaintList.aspx?doType=del&NavTabId=" + NavTabId + "&selectPage=" + (gv.PageIndex + 1).ToString() + "&doAjax=true&FollowAuditID={FollowAuditID}\" rel=\"ids\" target=\"selectedTodo\" title=\"确定要删除吗？\"><span>删除</span></a></li>";
            }

            if (Request.QueryString["doType"] != null)
            {
                //投诉通过状态
                if (Request.QueryString["doType"].ToString() == "upDateHouse")
                {
                    if (!string.IsNullOrEmpty(Request["ids"]))
                    {
                        string sql = string.Format("select * from h_ComplaintList where ID in({0})", Request["ids"]);
                        List<h_ComplaintList> list_h_ComplaintList = h_ComplaintList.FindAll(sql);
                        foreach (h_ComplaintList hfa in list_h_ComplaintList)
                        {
                            H_houseinfor hh = H_houseinfor.FindByHouseID(Convert.ToDecimal(hfa.HouseID));
                            if (Request.QueryString["Type"].ToString() == "1")
                            {
                                //房源跟进中增加修改状态
                                HouseMIS.EntityUtils.Employee ee = HouseMIS.EntityUtils.Employee.FindByEmployeeID(hfa.Slemployee);
                                //判断房源状态是否本来状态，如果不是的话还原，并且首录入改为举报人
                                if (hh.StateID != hfa.HouseOldType)
                                {
                                    //增加专管员跟进记录
                                    h_FollowUp hsf = new h_FollowUp();
                                    hsf.EmployeeID = hfa.Operator;
                                    hsf.HouseID = hfa.HouseID;
                                    hsf.FollowUpText = "审核状态:" + h_State.FindByStateID(decimal.Parse(hfa.HouseNewType.ToString())).Name + "→" + h_State.FindByStateID(decimal.Parse(hfa.HouseOldType.ToString())).Name;
                                    hsf.Insert();

                                    string uphouse = "update H_houseinfor set StateID=" + hfa.HouseOldType + ",OwnerEmployeeID=" + hfa.Slemployee + ", OrgID=" + ee.OrgID + " where HouseID=" + hfa.HouseID;
                                    DbHelperSQL.ExecuteSql(uphouse);

                                }

                                //审核通过扣除征信分数
                                HouseMIS.EntityUtils.Employee ee1 = HouseMIS.EntityUtils.Employee.FindByEmployeeID(hfa.Operator);
                                EntityUtils.EmpIntegral Empint = new EntityUtils.EmpIntegral();
                                Empint.EmployeeID = ee1.EmployeeID;
                                Empint.OrgID = ee1.OrgID;
                                Empint.Type = "其他";
                                Empint.Integral = int.Parse(myffrmzcfs.SelectedValue);
                                Empint.Cause = "投诉审核通过扣除征信分";
                                Empint.Exe_Date = DateTime.Now;
                                Empint.OperID = 2;
                                Empint.Insert();
                                hfa.IntegralID = Empint.ID;

                                Common.MsgPush.PushMsg("房源：" + hh.Shi_id + " 的投诉——审核通过！", new string[] { hfa.Slemployee.ToString() }, (int)Common.msgType.房源状态投诉);
                            }
                            else if (Request.QueryString["Type"].ToString() == "0")
                            {
                                Common.MsgPush.PushMsg("房源：" + hh.Shi_id + " 的投诉——审核未通过！", new string[] { hfa.Slemployee.ToString() }, (int)Common.msgType.房源状态投诉);
                            }
                            hfa.HandlingResults = int.Parse(Request.QueryString["Type"]);
                            hfa.HandlingEmp = Current.EmployeeID.ToString().ToInt32();
                            hfa.HandlingTime = DateTime.Now;
                            hfa.Update();
                        }
                    }
                }
                else if (Request.QueryString["doType"].ToString() == "del")
                {
                    if (!string.IsNullOrEmpty(Request["ids"]))
                    {
                        DbHelperSQL.ExecuteSql("delete from h_ComplaintList where ID in(" + Request["ids"] + ")");
                    }
                }

                #region 刷新当前页[抓取当页导航数字onclick事件]
                string JavaScript = " dwzPageBreak({ targetType: \"navTab\", rel: \"\", data: { pageNum: " + Request["selectPage"] + "} });";
                JSDo_UserCallBack_Success(JavaScript, "操作成功");
                #endregion
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
            temp1 = GetMySearchControlValue("Slemployee");
            if (!temp1.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" and Slemployee in (select EmployeeID from e_Employee where em_name like '%{0}%')", temp1);
            }
            //审核人
            temp1 = GetMySearchControlValue("HandlingEmp");
            if (!temp1.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" and HandlingEmp in (select EmployeeID from e_Employee where em_name like '%{0}%')", temp1);
            }
            //申请时间
            temp1 = GetMySearchControlValue("RevertTime");
            if (!temp1.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" and CONVERT(varchar(100), RevertTime, 23)='{0}'", temp1);
            }
            //审核时间
            temp1 = GetMySearchControlValue("HandlingTime");
            if (!temp1.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" and CONVERT(varchar(100), HandlingTime, 23)='{0}'", temp1);
            }
            //是否已审核
            temp1 = GetMySearchControlValue("HandlingResults");
            if (temp1 == "0")
            {
                sb.AppendFormat(" and HandlingResults is null", temp1);
            }
            else if (temp1 == "1")
            {
                sb.AppendFormat(" and HandlingResults is not null", temp1);
            }
            else if (temp1 == "2")
            {

            }
            else
            {
                sb.AppendFormat(" and HandlingResults is null", temp1);
            }
            //类型
            temp1 = GetMySearchControlValue("state");
            if (temp1 == "0")
            {
                sb.AppendFormat(" and IsOK=1", temp1);
            }
            else if (temp1 == "1")
            {
                sb.AppendFormat(" and IsComplaint=1", temp1);
            }
            else if (temp1 == "2")
            {

            }
            else
            {
                sb.AppendFormat(" and IsComplaint=1", temp1);
            }
            return sb.ToString();
        }
    }
}