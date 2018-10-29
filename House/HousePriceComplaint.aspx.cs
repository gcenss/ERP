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
    public partial class HousePriceComplaint : EntityListBase<h_PriceComplaint>
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

            z_bottom = "<li><a class=\"iconL\" href=\"House/HousePriceComplaint.aspx?Type=1&doType=upDateHouse&NavTabId=" + NavTabId + "&selectPage=" + (gv.PageIndex + 1).ToString() + "&doAjax=true&FollowAuditID={FollowAuditID}\" rel=\"ids\" target=\"selectedTodo\" title=\"确定要操作吗？\"><span>举报通过</span></a></li>";
            z_bottom += "<li><a class=\"iconL\" href=\"House/HousePriceComplaint.aspx?Type=0&doType=upDateHouse&NavTabId=" + NavTabId + "&selectPage=" + (gv.PageIndex + 1).ToString() + "&doAjax=true&FollowAuditID={FollowAuditID}\" rel=\"ids\" target=\"selectedTodo\" title=\"确定要操作吗？\"><span>举报不通过</span></a></li>";
            if (CheckRolePermission("删除"))
            {
                z_del = "<li><a class=\"delete\" href=\"House/HousePriceComplaint.aspx?doType=del&NavTabId=" + NavTabId + "&selectPage=" + (gv.PageIndex + 1).ToString() + "&doAjax=true&FollowAuditID={FollowAuditID}\" rel=\"ids\" target=\"selectedTodo\" title=\"确定要删除吗？\"><span>删除</span></a></li>";
            }

            if (Request.QueryString["doType"] != null)
            {
                //投诉通过状态
                if (Request.QueryString["doType"].ToString() == "upDateHouse")
                {
                    if (!string.IsNullOrEmpty(Request["ids"]))
                    {
                        string sql = string.Format("select * from h_PriceComplaint where ID in({0})", Request["ids"]);
                        List<h_PriceComplaint> list_h_PriceComplaint = h_PriceComplaint.FindAll(sql);
                        foreach (h_PriceComplaint hpc in list_h_PriceComplaint)
                        {
                            H_houseinfor hh = H_houseinfor.FindByHouseID(Convert.ToDecimal(hpc.houseID));
                            if (Request.QueryString["Type"].ToString() == "1")
                            {
                                //原始价格维护人
                                Employee ee = Employee.FindByEmployeeID(hpc.priceEmpID);

                                //增加房源跟进
                                h_FollowUp hsf = new h_FollowUp();
                                hsf.EmployeeID = hpc.priceEmpID;
                                hsf.HouseID = hpc.houseID;
                                hsf.FollowUpText = "房源价格维护人:" + ee.Em_name + "→" + hpc.EmployeeName;
                                hsf.Insert();

                                h_PriceFollowUp hpf = h_PriceFollowUp.FindByKey(hpc.h_PriceFollowUpID);
                                if (hpf != null)
                                {
                                    hpf.State = 1;
                                    hpf.Update();
                                }

                                sql = string.Format(@"update h_houseinfor set update_date='{0}' where HouseID={1}",
                                            hpc.createTime,
                                            hpc.houseID);
                                DbHelperSQL.ExecuteSql(sql);

                                Common.MsgPush.PushMsg("房源价格维护：" + hh.Shi_id + " 的投诉——审核通过！", new string[] { hpc.priceEditEmpID.ToString() }, (int)Common.msgType.价格维护);
                            }
                            else if (Request.QueryString["Type"].ToString() == "0")
                            {
                                h_PriceFollowUp hpf = h_PriceFollowUp.FindByKey(hpc.h_PriceFollowUpID);
                                if (hpf != null)
                                {
                                    hpf.State = 0;
                                    hpc.Update();
                                }
                                Common.MsgPush.PushMsg("房源价格维护：" + hh.Shi_id + " 的投诉——审核未通过！", new string[] { hpc.priceEditEmpID.ToString() }, (int)Common.msgType.价格维护);
                            }
                            hpc.HandlingResults = Request.QueryString["Type"].ToString() == "1" ? true : false;
                            hpc.HandlingEmpID = Convert.ToInt32(Current.EmployeeID);
                            hpc.HandlingTime = DateTime.Now;
                            hpc.Update();
                        }
                    }
                }
                else if (Request.QueryString["doType"].ToString() == "del")
                {
                    if (!string.IsNullOrEmpty(Request["ids"]))
                    {
                        DbHelperSQL.ExecuteSql("delete from h_PriceComplaint where ID in(" + Request["ids"] + ")");
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
            temp1 = GetMySearchControlValue("shi_id");
            if (!temp1.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" and HouseID in (select houseid from h_houseinfor where shi_id like '%{0}%')", temp1);
            }
            //申请人
            temp1 = GetMySearchControlValue("priceEmpID");
            if (!temp1.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" and priceEmpID in (select EmployeeID from e_Employee where em_name like '%{0}%')", temp1);
            }
            //审核人
            temp1 = GetMySearchControlValue("HandlingEmpID");
            if (!temp1.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" and HandlingEmpID in (select EmployeeID from e_Employee where em_name like '%{0}%')", temp1);
            }
            //申请时间
            temp1 = GetMySearchControlValue("ComplaintTime");
            if (!temp1.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" and CONVERT(varchar(100), ComplaintTime, 23)='{0}'", temp1);
            }
            //审核时间
            temp1 = GetMySearchControlValue("HandlingTime");
            if (!temp1.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" and CONVERT(varchar(100), HandlingTime, 23)='{0}'", temp1);
            }
            //是否已审核
            temp1 = GetMySearchControlValue("HandlingResults");
            if (!temp1.IsNullOrWhiteSpace() && temp1 != "2")
            {
                sb.AppendFormat(" and HandlingResults={0}", temp1);
            }
            //类型
            temp1 = GetMySearchControlValue("state");
            if (temp1 != "2")
            {
                if (temp1 == "0")
                {
                    sb.AppendFormat(" and IsOK=1", temp1);
                }
                else if (temp1 == "1")
                {
                    sb.AppendFormat(" and IsComplaint=1", temp1);
                }
                else
                {
                    sb.AppendFormat(" and IsComplaint=1", temp1);
                }
            }

            return sb.ToString();
        }
    }
}