using HouseMIS.EntityUtils;
using HouseMIS.EntityUtils.DBUtility;
using HouseMIS.Web.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace HouseMIS.Web.House
{
    public partial class HouseZBCheckEdit : EntityFormBase<h_houseinfor_ZBCheck>
    {
        public bool? _isCheck;

        public bool isCheck
        {
            get
            {
                if (_isCheck.HasValue)
                {
                    return _isCheck.Value;
                }
                else
                {
                    _isCheck = CheckRolePermission("总部认证");
                    return _isCheck.Value;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Form.Action += "?StateID=" + Request.QueryString["StateID"] + "&isefw=" + Request.QueryString["isefw"] + "";
            if (!IsPostBack)
            {
                DataSet dts = new DataSet();
                //判断已审核并且有录音
                if ((Entity.employee_auditID.HasValue || isCheck) && Entity.phoneID.HasValue)
                {
                    dts = DbHelperSQL.Query(string.Format(@"SELECT A.recordUrl                         AS a,
                                                                    C.Name + '(' + B.em_id + '-' + B.em_name + ')' + '  '
                                                                    + CONVERT(VARCHAR, createTime, 120) AS c,
                                                                    A.phoneID,
                                                                    A.employeeID,
                                                                    source,
                                                                    a.talktime - a.realSecond           AS startTime,
                                                                    a.realSecond,
                                                                    Isnull(d.IsCheck, 0)                AS IsCheck
                                                            FROM   i_InternetPhone A
                                                                    LEFT JOIN e_Employee B
                                                                            ON A.employeeID = B.EmployeeID
                                                                    LEFT JOIN s_Organise C
                                                                            ON B.OrgID = C.OrgID
                                                                    LEFT JOIN h_RecordClose d
                                                                            ON a.phoneID = d.phoneID
                                                            WHERE  a.phoneID = {0}",
                                                            Entity.phoneID));
                }
                else
                {
                    dts = DbHelperSQL.Query(string.Format(@"SELECT a.recordurl                         AS a, 
                                                                   c.NAME + '(' + B.em_id + '-' + B.em_name + ')' + '  ' 
                                                                   + CONVERT(VARCHAR, createtime, 120) AS c, 
                                                                   a.phoneid, 
                                                                   a.employeeid, 
                                                                   source, 
                                                                   a.talktime - a.realsecond           AS startTime, 
                                                                   a.realsecond, 
                                                                   Isnull(d.ischeck, 0)                AS IsCheck 
                                                            FROM   i_internetphone a 
                                                                   LEFT JOIN e_employee b 
                                                                          ON a.employeeid = b.employeeid 
                                                                   LEFT JOIN s_organise c 
                                                                          ON b.orgid = c.orgid 
                                                                   LEFT JOIN h_recordclose d 
                                                                          ON a.phoneid = d.phoneid 
                                                            WHERE  houseid = {0} 
                                                                   AND a.employeeid = {1} 
                                                                   AND a.talktime > 0 
                                                                   AND a.recordurldel = 1 
                                                                   AND a.phoneid NOT IN(SELECT isnull(phoneid,0)
                                                                                        FROM   h_houseinfor_zbcheck 
                                                                                        WHERE  houseid = {0}  
                                                                                               AND employeeid = {1} 
                                                                                               AND state_zbcheck = 2) 
                                                            ORDER  BY a.createtime DESC ",
                                                                Entity.houseID,
                                                                Current.EmployeeID));
                }

                hf_ly2.DataSource = dts.Tables[0];
                hf_ly2.DataBind();

                if (Request.QueryString["StateID"] != null)
                {
                    lblRemark.Visible = false;
                    txtRemark.Visible = true;
                }
                else
                {
                    lblRemark.Visible = true;
                    txtRemark.Visible = false;
                }
            }
        }

        protected override void OnSaveSuccess(object sender, EntityFormEventArgs e)
        {
            if (Request.Form["txtRemark"] != null)
            {
                H_houseinfor hh = H_houseinfor.FindByHouseID(Entity.houseID);
                //总部认证状态
                hh.state_ZBCheck = Request.QueryString["StateID"].ToInt32();
                //更新日期
                hh.Update_date = DateTime.Now;
                //认证通过
                if (Request.QueryString["StateID"].ToInt32() == (int)CheckState.合格)
                {
                    //当房源首录人和申请认证的人不一致时，修改首录人和门店
                    if (Entity.employeeID.HasValue && hh.OwnerEmployeeID != Entity.employeeID.Value)
                    {
                        Employee ee = Employee.FindByEmployeeID(Entity.employeeID.Value);
                        hh.OwnerEmployeeID = (int)ee.EmployeeID;
                        hh.OrgID = (int)ee.OrgID;
                    }

                    //hh.Exe_date = Entity.exe_Date.Value;

                    //找出此房源所有未审核，未上传开盘录音的记录，将其清空
                    List<h_houseinfor_ZBCheck> listZB = h_houseinfor_ZBCheck.FindAll(string.Format(@"select * from h_houseinfor_ZBCheck
		                                                                                                where houseid={0}
		                                                                                                and phoneID is null 
		                                                                                                and employee_auditID is null
		                                                                                                and isDel=0",
                                                                                                        Entity.houseID));

                    if (listZB.Count > 0)
                    {
                        DbHelperSQL.ExecuteSql("update h_houseinfor_ZBCheck set isdel=1 where id in(" + string.Join(",", listZB.Select(x => x.ID).ToArray()) + ")");
                    }
                    if (Entity.phoneID.HasValue)
                    {
                        h_RecordClose hrc = h_RecordClose.Find(h_RecordClose._.phoneID, Entity.phoneID.Value);
                        if (hrc != null)
                        {
                            hrc.IsCheck = true;
                            hrc.Update();
                        }
                        else
                        {
                            hrc = new h_RecordClose();
                            i_InternetPhone iip = i_InternetPhone.FindByKey(Entity.phoneID.Value);
                            hrc.EmployeeID = iip.employeeID;
                            hrc.phoneID = Entity.phoneID.Value;
                            hrc.aType = 0;
                            hrc.CheckEmployeeID = iip.employeeID;
                            hrc.IsCheck = true;
                            hrc.CheckDate = DateTime.Now;
                            hrc.exe_date = DateTime.Now;
                            hrc.ComID = Current.ComID;
                            hrc.Insert();
                        }
                    }

                    //增加排序积分
                    s_SysParam ss = s_SysParam.FindByParamCode("houseKP");
                    //获取分隔符的值，第一个为分值，第二是否有 有效期，第三为有效期值
                    string[] ssValue = ss.Value.Split('|');

                    e_Integral ei = new e_Integral();
                    ei.employeeID = Entity.employeeID.Value;
                    ei.Type = (int)integral_Type.房源与经纪人;
                    ei.tableName = "h_houseinfor";
                    ei.coloumnName = "HouseID";
                    ei.keyID = Entity.houseID;
                    ei.integralParam = "houseKP";
                    ei.integralValue = ssValue[0].ToInt32();
                    ei.integralDay = ssValue[1] == "1" ? ssValue[2].ToInt32() : 0;
                    ei.exe_Date = DateTime.Now;
                    ei.Insert();
                }
                //认证驳回
                else if (Request.QueryString["StateID"].ToInt32() == (int)CheckState.驳回)
                {
                    h_houseinfor_ZBCheck hhz = new h_houseinfor_ZBCheck();
                    hhz.houseID = Entity.houseID;
                    hhz.employeeID = Entity.employeeID;
                    hhz.exe_Date = DateTime.Now;
                    hhz.state_ZBCheck = (int)CheckState.待认证;
                    hhz.rejectNum = Entity.rejectNum.Value + 1;
                    hhz.comID = Current.ComID;
                    hhz.Insert();
                }
                //认证 虚位以待
                else if (Request.QueryString["StateID"].ToInt32() == (int)CheckState.虚位以待)
                {
                    hh.OwnerEmployeeID = 0;
                    hh.OrgID = 0;
                    //修改为无效状态
                    hh.StateID = 10;
                    hh.CurrentEmployee = Current.EmployeeID;

                    //找出此房源所有未审核，未上传开盘录音的记录，将其清空
                    List<h_houseinfor_ZBCheck> listZB = h_houseinfor_ZBCheck.FindAll(string.Format(@"select * from h_houseinfor_ZBCheck
		                                                                                                where houseid={0}
		                                                                                                and phoneID is null 
		                                                                                                and employee_auditID is null
		                                                                                                and isDel=0",
                                                                                                        Entity.houseID));

                    if (listZB.Count > 0)
                    {
                        DbHelperSQL.ExecuteSql("update h_houseinfor_ZBCheck set isdel=1 where id in(" + string.Join(",", listZB.Select(x => x.ID).ToArray()) + ")");
                    }
                }

                hh.Update();
            }

            ShowMsg(AlertType.correct, "保存成功");
            //base.OnSaveSuccess(sender, e);
        }

        protected override void OnSaving(object sender, EntityFormEventArgs e)
        {
            //非委托中房源无法保存
            //if (Entity.houseStateID != 2)
            //{
            //    ShowMsg(AlertType.correct, "非委托中房源无法保存！");
            //    return;
            //}
            if (Request.Form["rb1"] != null)
            {
                Log log1 = new Log();
                log1.Action = "上传开盘录音";
                log1.Category = "上传开盘录音";
                log1.IP = HttpContext.Current.Request.UserHostAddress;
                log1.OccurTime = DateTime.Now;
                log1.UserID = Convert.ToInt32(Employee.Current.EmployeeID);
                log1.UserName = Employee.Current.Em_name;
                log1.Remark = string.Format("房源录音ID={0},房源ID={1},房源编号={2}",
                                                Request.Form["rb1"],
                                                Entity.houseID,
                                                H_houseinfor.FindByHouseID(Entity.houseID).Shi_id);
                log1.Insert();

                Entity.phoneID = Request.Form["rb1"].ToInt32();
                if (!Entity.employeeID.HasValue)
                {
                    Entity.employeeID = (int)Current.EmployeeID;
                }
                if (!Entity.exe_Date.HasValue)
                {
                    Entity.exe_Date = DateTime.Now;
                }
            }

            if (Request.Form["txtRemark"] != null)
            {
                //审核意见
                Entity.Remark = Request.Form["txtRemark"];
                //审核日期
                Entity.audit_Date = DateTime.Now;
                //审核人
                Entity.employee_auditID = Convert.ToInt32(Current.EmployeeID);
                //审核状态
                Entity.state_ZBCheck = Request.QueryString["StateID"].ToInt32();

                string FollowUpText = "总部认证：";
                FollowUpText += lblState.Text;
                FollowUpText += "->" + Enum.GetName(typeof(CheckState), Request.QueryString["StateID"].ToInt32());
                if (Request.QueryString["StateID"].ToInt32() == (int)CheckState.合格)
                {
                    H_houseinfor hh = H_houseinfor.FindByHouseID(Entity.houseID);
                    //房源首录人和申请认证不一致，申请认证人员改为房源首录人
                    if (Entity.employeeID.HasValue && hh.OwnerEmployeeID != Entity.employeeID.Value)
                    {
                        Employee ee = Employee.FindByEmployeeID(Entity.employeeID.Value);
                        FollowUpText += ",首录人：" + hh.OwnerEmployeeName + "->" + ee.Em_name;
                    }
                }
                else if (Request.QueryString["StateID"].ToInt32() == (int)CheckState.虚位以待)
                {
                    H_houseinfor hh = H_houseinfor.FindByHouseID(Entity.houseID);
                    FollowUpText += ",首录人：" + hh.OwnerEmployeeName + "->无";
                }
                h_FollowUp hf = new h_FollowUp();
                hf.HouseID = Entity.houseID;
                hf.FollowUpText = FollowUpText;
                hf.EmployeeID = Current.EmployeeID;
                hf.Insert();
                if (Request.QueryString["isefw"] == "1")
                {
                    DbHelperSQL.ExecuteSql("insert into api_Addhouse(erp_userid, w_userid, houseid, type) values('" + Employee.Current.EmployeeID + "', '', '" + Entity.houseID + "', 'efw')");
                }
            }
            base.OnSaving(sender, e);
        }

        protected override void OnSetForm(object sender, EntityFormEventArgs e)
        {
            if (Entity.ID > 0)
            {
                //房源编号
                lblHouseID.Text = Entity.shi_id;
                //申请人
                lblEmpName.Text = Entity.EmployeeName;
                //申请时间
                if (Entity.exe_Date.HasValue)
                {
                    lblexe_Date.Text = Entity.exe_Date.Value.ToShortDateString();
                }
                //申请状态
                if (Entity.state_ZBCheck.HasValue)
                {
                    lblState.Text = Enum.GetName(typeof(CheckState), Entity.state_ZBCheck);
                }
                //审核人
                if (Entity.employee_auditID.HasValue)
                {
                    lblauditName.Text = Entity.AuditEmployeeName;
                }
                //审核时间
                if (Entity.audit_Date.HasValue)
                {
                    lblaudit_Date.Text = Entity.audit_Date.Value.ToShortDateString();
                }
                //是否删除
                if (Entity.isDel.Value)
                {
                    btnSave.Visible = false;
                }
                //审核意见
                if (!Entity.Remark.IsNullOrWhiteSpace())
                {
                    lblRemark.Text = Entity.Remark;
                    //已审核的 无法修改,并且没有权限
                    if (!isCheck)
                    {
                        btnSave.Visible = false;
                    }

                    if (Request.QueryString["StateID"] != null)
                        txtRemark.Text = Entity.Remark;
                }
                else
                {
                    if (Request.QueryString["StateID"] != null)
                    {
                        txtRemark.Text = Enum.GetName(typeof(CheckState), Request.QueryString["StateID"].ToInt32());
                    }
                }

                //开盘凭证
                if (!Entity.picUrl.IsNullOrWhiteSpace())
                {
                    h_houseinfor_ZBCheckPic.InnerHtml = "<img src='" + GetAllUrl(Entity.picUrl) + "' width='300' height='350' />";
                }
            }
        }

        protected void hf_ly2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView hp = (DataRowView)e.Item.DataItem;
                //录音屏蔽按钮
                HyperLink reps = (HyperLink)e.Item.FindControl("LYClose");
                //录音保密
                reps.NavigateUrl = reps.NavigateUrl + "?doAjax=true&phoneID=" + hp["phoneID"].ToString() + "&NavTabId=" + NavTabId + "&id=" + reps.ClientID;
                reps.Attributes.Add("width", "255");
                reps.Attributes.Add("height", "140");

                HtmlControl spanRb = (HtmlControl)e.Item.FindControl("spanRb");
                //已审核过或者已删除
                if (!Entity.Remark.IsNullOrWhiteSpace() || Entity.isDel.Value)
                {
                    spanRb.Visible = false;
                }
            }
        }
    }
}