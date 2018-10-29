using System;
using System.Collections.Generic;
using HouseMIS.EntityUtils;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HouseMIS.Web.UI;
using HouseMIS.EntityUtils.DBUtility;
using System.Linq;

namespace HouseMIS.Web.House
{
    public partial class HouseFromLY : EntityListBase
    {
        public string HouseID = "";
        string seeRecord = string.Empty;
        string seeRecord_Mask = string.Empty;
        List<h_RecordClose> list_h_RecordClose = new List<h_RecordClose>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                seeRecord = GetRolePermissionEmployeeIds("查看录音", "EmployeeID");
                seeRecord_Mask = GetRolePermissionEmployeeIds("查看屏蔽录音", "EmployeeID");

                if (Request.QueryString["HouseID"] != null)
                {
                    string vSql = string.Format(@"select b.recordUrl as a,
                                                    d.Name+'('+c.em_id+'-'+c.em_name+')' as c,
                                                    CONVERT(varchar,createTime,120) as d,
                                                    b.phoneID,b.employeeID,source,
                                                    b.talktime-b.realSecond as startTime
                                                    from i_InternetPhone b
                                                    inner join e_Employee c on b.employeeID=c.EmployeeID
                                                    inner join s_Organise d on d.OrgID=c.OrgID 
                                                    where b.HouseId={0}
                                                    and b.recordUrlDel=1 
                                                    and b.RecrodType=1 
                                                    and b.talktime>0
                                                    and b.EmployeeID in(select EmployeeID  
                                                                        from e_Employee 
                                                                        where {1})
                                                    and b.phoneID not in (select a.phoneID 
                                                                            from i_InternetPhone a
                                                                            inner join h_RecordClose b on a.phoneID = b.phoneID
                                                                            where a.houseID ={0}
                                                                            and b.IsCheck = 1  
                                                                            and a.EmployeeID not in (select EmployeeID 
                                                                                                        from e_Employee
                                                                                                        where {2}))
                                                    order by b.phoneID desc",
                                                    Request.QueryString["HouseID"],
                                                    seeRecord,
                                                    seeRecord_Mask);

                    DataSet dts = DbHelperSQL.Query(vSql);

                    if (dts.Tables[0].Rows.Count > 0)
                    {
                        vSql = string.Format(@"select phoneID,EmployeeID from h_RecordClose where IsCheck=1 and phoneID in({0})",
                            string.Join(",", dts.Tables[0].AsEnumerable().Select(x => x.Field<decimal>("phoneID"))));

                        list_h_RecordClose = h_RecordClose.FindAll(vSql);
                    }

                    hf_ly2.DataSource = dts.Tables[0];
                    hf_ly2.DataBind();
                    HouseID = Request.QueryString["HouseID"];
                }
            }
        }
        public override string MenuCode
        {
            get
            {
                return "2001";
            }

        }
        protected void hf_ly2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView hp = (DataRowView)e.Item.DataItem;
                Panel pan = (Panel)e.Item.FindControl("Panel1");

                //录音是否保密
                Label lblClose = (Label)e.Item.FindControl("lblClose");

                decimal empID = 0;
                if (list_h_RecordClose.Count > 0)
                {
                    var h_RecordClose = list_h_RecordClose.Find(x => x.phoneID == Convert.ToDecimal(hp["phoneID"].ToString()));
                    if (h_RecordClose != null)
                    {
                        //empID = h_RecordClose.EmployeeID;
                        //if (empID > 0 && empID != Employee.Current.EmployeeID)
                        //{
                        lblClose.Text = "(录音已保密)";
                        if (!CheckRolePermission("查看录音", Convert.ToDecimal(hp["employeeID"])))
                        {
                            pan.Visible = false;
                            return;
                        }
                        //}
                    }
                }

                //删除按钮
                HyperLink deltelLY = (HyperLink)e.Item.FindControl("deltelLY");
                //录音屏蔽按钮
                HyperLink LYClose = (HyperLink)e.Item.FindControl("LYClose");
                //录音时间
                Label lblDate = (Label)e.Item.FindControl("lblDate");

                if (!Current.RoleNames.Contains("信息") &&
                    (!CheckRolePermission("申请录音屏蔽", Convert.ToDecimal(hp["employeeID"]))
                    || Employee.Current.EmployeeID != Convert.ToDecimal(hp["employeeID"])))
                {
                    LYClose.Visible = false;
                }
                else
                {
                    //录音保密
                    LYClose.NavigateUrl = LYClose.NavigateUrl + "?doAjax=true&phoneID=" + hp["phoneID"].ToString() + "&NavTabId=" + NavTabId + "&id=" + LYClose.ClientID;
                    LYClose.Attributes.Add("width", "255");
                    LYClose.Attributes.Add("height", "140");

                    if (empID > 0 && empID != Employee.Current.EmployeeID)
                    {
                        if (CheckRolePermission("查看录音", Convert.ToDecimal(hp["employeeID"])))
                        {
                            LYClose.Text = "取消录音保密";
                            LYClose.Target = "dialog";
                            LYClose.NavigateUrl = "House/HouseForm.aspx?OperType=5&doAjax=true&LSH=" + hp["phoneID"].ToString() + "&NavTabId=" + NavTabId + "&HouseID=" + HouseID;

                        }
                    }
                }

                if (!Current.RoleNames.Contains("信息") &&
                    (!CheckRolePermission("删除录音", Convert.ToDecimal(hp["employeeID"]))
                    || Employee.Current.EmployeeID != Convert.ToDecimal(hp["employeeID"])))
                {
                    deltelLY.Visible = false;
                }
                else
                {
                    //删除录音
                    deltelLY.NavigateUrl = deltelLY.NavigateUrl + "?OperType=4&doAjax=true&LSH=" + hp["phoneID"].ToString() + "&NavTabId=" + NavTabId + "&HouseID=" + HouseID;
                }

                ////2016-07-06之前屏蔽所有录音，个人只能看个人的录音
                //if (lblDate.Text.ToDate() <= new DateTime(2016, 7, 7))
                //{
                //    if (!CheckRolePermission("查看录音", Convert.ToDecimal(hp["employeeID"])))
                //    {
                //        pan.Visible = false;
                //    }
                //}
                //else
                //{
                //    h_RecordClose hr = h_RecordClose.Find("IsCheck=1 and phoneID=" + hp["phoneID"]);
                //    //判断当前录音是否被屏蔽
                //    if (hr != null)
                //    {
                //        lblClose.Text = "(录音已保密)";
                //        //判断当前操作人是否被屏蔽录音的操作人
                //        if (hr.EmployeeID != Employee.Current.EmployeeID)
                //        {
                //            if (!CheckRolePermission("查看录音", Convert.ToDecimal(hp["employeeID"])))
                //                pan.Visible = false;
                //        }
                //        else
                //        {
                //            LYClose.Text = "取消录音保密";
                //            LYClose.Target = "dialog";
                //            LYClose.NavigateUrl = "House/HouseForm.aspx?OperType=5&doAjax=true&LSH=" + hp["phoneID"].ToString() + "&NavTabId=" + NavTabId + "&HouseID=" + HouseID;
                //        }
                //    }
                //}
            }
        }
    }
}