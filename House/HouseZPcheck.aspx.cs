using HouseMIS.Common;
using HouseMIS.EntityUtils;
using HouseMIS.EntityUtils.DBUtility;
using HouseMIS.Web.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using TCode;
using System.Linq;

namespace HouseMIS.Web.House
{
    public partial class HouseZPcheck : EntityListBase<H_houseinfor>
    {
        protected string efw()
        {
            if (CheckRolePermission("查看"))
            {
                if (Current.RoleNames.Contains("信息") || CurrentEmployee.EmployeeID == 2750 || CurrentEmployee.EmployeeID == 6591 || CurrentEmployee.EmployeeID == 6592)
                    return "<li><a class=\"add\" href=\"House/HouseZPcheck.aspx?NavTabId=" + NavTabId + "&doAjax=true&doType=addefw\" rel=\"ids\" target=\"selectedTodo\" title=\"确定导入吗 ?\"><span>导入e房网</span></a></li>";
                else
                    return "";
            }
            else
                return "";
        }
        protected string z_bottom()
        {
            StringBuilder sb = new StringBuilder();
            if (CheckRolePermission("查看"))
            {

                //信息部审核照片
                if (Current.RoleNames.Contains("信息"))
                {
                    sb.Append("<li><a class=\"iconL\" href=\"House/HouseZPcheck.aspx?IsSh=1&NavTabId=" + NavTabId + "&selectPage=" + (gv.PageIndex + 1).ToString() + "&doAjax=true&HouseID={HouseID}\" rel=\"ids\" target=\"selectedTodo\" title=\"确定要操作吗？\"><span>照片审核通过</span></a></li>");
                    sb.Append("<li><a class=\"iconL\" href=\"House/HouseZPcheck.aspx?IsBh=1&NavTabId=" + NavTabId + "&selectPage=" + (gv.PageIndex + 1).ToString() + "&doAjax=true&HouseID={HouseID}\" rel=\"ids\" target=\"selectedTodo\" title=\"确定要操作吗？\"><span>待定</span></a></li>");
                    sb.Append("<li><a class=\"edit\" href=\"House/HouseBhReasonAdd.aspx?NavTabId=" + NavTabId + "&doAjax=true&HouseID={HouseID}\" rel='Bargain1' width=\"360\" height=\"230\" target=\"dialog\" mask=\"true\"><span>照片驳回</span></a></li>");
                }
            }

            return sb.ToString();
        }

        public string pHasImage = "0", pHasKey = "0";

        public bool bRoom = false;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            //hsc = s_SysParam.FindByParamCode("HouseStateColor");
            //hspa = s_SysParam.FindByParamCode("TodayHouseColor");
            gv.RowCreated += new GridViewRowEventHandler(gv_RowCreated);
            if (!IsPostBack)
            {
                if (!this.Request.Params["HouseDicID"].IsNullOrWhiteSpace())
                {
                    isMapForm = true;
                }
            }
        }

        /// <summary>
        /// gv行创建事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gv_RowCreated(object sender, GridViewRowEventArgs e)
        {
            //如果是数据行
            if (e.Row.RowType == DataControlRowType.DataRow) ////注意： DataKeys里能取到的值，来前台页面的GridView中 DataKeyNames设置的值，多个可用逗号隔开
            {
                if (gv.DataKeys[e.Row.RowIndex]["HouseID"] != null && gv.DataKeys[e.Row.RowIndex]["shi_id"] != null && gv.DataKeys[e.Row.RowIndex]["shi_id"].ToString() != "")
                {
                    if (gv.DataKeys[e.Row.RowIndex]["aType"].ToString() == "1")
                        e.Row.Attributes.Add("no", "true");
                    e.Row.Attributes.Add("ondblclick", "OpenHouseEdit_SF('" + gv.DataKeys[e.Row.RowIndex]["HouseID"].ToString() + "','" + gv.DataKeys[e.Row.RowIndex]["shi_id"].ToString() + "','0')");
                }
            }
        }

        //private s_SysParam hsc;
        //private s_SysParam hspa;

        //protected string GetColStyle(object SeeHouseType)
        //{
        //    //s_SysParam hsc = s_SysParam.FindByParamCode("HouseStateColor");
        //    if (hsc != null)
        //    {
        //        string[] ss = hsc.Value.Split(',');
        //        foreach (string sv in ss)
        //        {
        //            string[] str = sv.Split('|');

        //            if (str.Length > 1 && str[0] == SeeHouseType.ToString() && str[1] != "")
        //            {
        //                return " style=\"color:#" + str[1] + "\" ";
        //            }
        //        }
        //    }
        //    return null;
        //}

        protected string GetState(object SeeHouseType, object aType)
        {
            if (aType.ToString() == "1")
            {
                return null;
            }
            return SeeHouseType.ToString();
        }

        protected string GetAtype(object AtypeName)
        {
            if (AtypeName != null && AtypeName.ToString() == "租房")
            {
                return "blue";
            }
            if (AtypeName != null && AtypeName.ToString() == "售房")
            {
                return "red";
            }
            return "black";
        }

        /// <summary>
        /// 取得房源编号颜色
        /// </summary>
        /// <param name="exe_date"></param>
        /// <returns></returns>
        protected string GetShi_idColor(Object exe_date, Object FollowUp_Date)
        {
            if (exe_date == null) return "";
            if (FollowUp_Date == null || FollowUp_Date.ToString() == "") FollowUp_Date = exe_date;
            s_SysParam ss = s_SysParam.FindByParamCode("RedDay");
            int pd = 0;
            if (ss != null)
                pd = Convert.ToInt32(ss.Value);
            //if (hspa != null && Convert.ToDateTime(exe_date).ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd"))
            //{
            //    return " style=\"color:#" + hspa.Value + "\" ";
            //}
            else if (Convert.ToDateTime(FollowUp_Date) < DateTime.Now.AddDays(-pd))
            {
                return " style=\"color:Red\" ";
            }
            return "";
        }

        //判断导出权限
        public string ExpotA = "";

        //public string AreaGroupBtn = "";

        public bool isMapForm = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FullDropListData(typeof(h_State), this.ffrmStateID, "Name", "StateID", "");
                this.ffrmStateID.Items[0].Text = "全部";
                this.ffrmStateID.Items[0].Value = "0";

                if (!GetMySearchControlValue("StateID").IsNullOrWhiteSpace())
                    ffrmStateID.SelectedValue = GetMySearchControlValue("StateID");
                else
                    ffrmStateID.SelectedValue = "2";



                if (!GetMySearchControlValue("IsSh").IsNullOrWhiteSpace())
                    myffrmIsSh.SelectedValue = GetMySearchControlValue("IsSh");
                else
                    myffrmIsSh.SelectedValue = "0";

                if (!GetMySearchControlValue("IsBh").IsNullOrWhiteSpace())
                    myffrmIsBh.SelectedValue = GetMySearchControlValue("IsBh");
                else
                    myffrmIsBh.SelectedValue = "0";



                if (Request.QueryString["aType"] != null)
                    ffrmaType.SelectedValue = Request.QueryString["aType"];

                mysfrmAreaID.DataTextField = s_Area._.Name;
                mysfrmAreaID.DataValueField = s_Area._.AreaID;
                EntityList<s_Area> EList = s_Area.FindAllComDistrict();
                mysfrmAreaID.Items.Add(new ListItem("", ""));
                for (int i = 0; i < EList.Count; i++)
                {
                    mysfrmAreaID.Items.Add(new ListItem(EList[i].Name, EList[i].AreaID.ToString()));
                }

                //FullDropListData(typeof(h_EntrustType), this.ffrmEntrustTypeID, "Name", "EntrustTypeID", "");

                myffrmstate_ZBCheck.Items.Clear();
                string[] checkState = Enum.GetNames(typeof(CheckState));
                for (int i = 0; i < checkState.Length; i++)
                {
                    myffrmstate_ZBCheck.Items.Add(new ListItem(checkState[i], i.ToString()));
                }
                myffrmstate_ZBCheck.Items.Insert(0, "");

                #region 推荐，收藏房源
                string OperType = Request.QueryString["OperTypes"];
                if (OperType != null)
                {
                    string HouseID = Request["HouseID"];
                    if (HouseID == null) HouseID = "";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    switch (OperType)
                    {
                        case "3": //推荐房源
                            s_SysParam ss = s_SysParam.FindByParamCode("RecommendNum");
                            TCode.DataAccessLayer.SelectBuilder sbs = new TCode.DataAccessLayer.SelectBuilder();
                            sbs.Table = "h_Recommend";
                            sbs.Where = "CONVERT(varchar,exe_date,112)=CONVERT(varchar,getdate(),112) and EmployeeID=" + Employee.Current.EmployeeID.ToString();
                            int hrs = h_Recommend.Meta.QueryCount(sbs);
                            if (hrs < Convert.ToInt32(ss.Value))
                            {
                                h_Recommend Hr = new h_Recommend();
                                Hr.EmployeeID = Employee.Current.EmployeeID;
                                Hr.HouseID = Convert.ToDecimal(HouseID);
                                Hr.Insert();
                            }
                            else
                            {
                                sb.Append("{\r\n");
                                sb.Append("   \"statusCode\":\"300\", \r\n");
                                sb.Append("   \"message\":\"操作失败，您今天房源的推荐数已满请明天再试！\" \r\n");
                                sb.Append("}\r\n");
                                Response.Write(sb.ToString());
                                Response.End();
                            }

                            break;

                        case "4": //收藏房源
                            H_houseinfor.Meta.Query("if not exists(select 1 from h_HouseCollect where HouseID=" +
                                HouseID + " and EmployeeID=" +
                                HouseMIS.EntityUtils.Employee.Current.EmployeeID.ToString() + ") insert into h_HouseCollect(HouseID,EmployeeID) Values(" +
                                HouseID + "," + HouseMIS.EntityUtils.Employee.Current.EmployeeID.ToString() + ")");
                            break;

                        case "5": //取消收藏房源
                            H_houseinfor.Meta.Query("delete from h_HouseCollect where HouseID=" +
                                HouseID + " and EmployeeID=" +
                                HouseMIS.EntityUtils.Employee.Current.EmployeeID.ToString());
                            break;

                        case "6": //取消推荐
                            H_houseinfor.Meta.Query("delete from h_Recommend where HouseID=" +
                                HouseID + " and EmployeeID=" +
                                HouseMIS.EntityUtils.Employee.Current.EmployeeID.ToString());
                            break;
                    }
                    sb.Append("{\r\n");
                    sb.Append("   \"statusCode\":\"200\", \r\n");
                    sb.Append("   \"message\":\"操作成功！\" \r\n");
                    sb.Append("}\r\n");
                    Response.Write(sb.ToString());
                    Response.End();
                }
                #endregion 推荐，收藏房源

                #region 删除房源
                if (Request.QueryString["doType"] != null)
                {
                    if (Request.QueryString["doType"] == "del")
                    {
                        if (CheckRolePermission("删除房源"))
                        {
                            if (!string.IsNullOrEmpty(Request["ids"]))
                                foreach (string s in Request["ids"].Split(','))
                                {
                                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                    H_houseinfor h = H_houseinfor.FindByHouseID(Convert.ToDecimal(s));
                                    if (h.StateID == 1)
                                    {
                                        if (h != null && CheckRolePermission("删除房源", h.OwnerEmployeeID.ToDecimal()))
                                        {
                                            //删除房源进入回收站
                                            h.DelType = true;
                                            h.DelEmployeeID = Convert.ToInt32(Employee.Current.EmployeeID);
                                            h.DelDate = System.DateTime.Now;
                                            h.Update();
                                        }
                                        else
                                        {
                                            JSDo_UserCallBack_Success(" formFind();$(\".HList:eq(0)\").submit();", "操作失败,您没有删除该房源的权限！");
                                        }
                                    }
                                    else
                                    {
                                        JSDo_UserCallBack_Success(" formFind();$(\".HList:eq(0)\").submit();", "操作失败,只能删除未审核的房源！");
                                    }
                                }
                            JSDo_UserCallBack_Success(" formFind();$(\".HList:eq(0)\").submit();", "操作成功");
                        }
                        else
                        {
                            JSDo_UserCallBack_Success(" formFind();$(\".HList:eq(0)\").submit();", "操作失败,您没有删除该房源的权限！");
                        }
                    }

                    else if (Request.QueryString["doType"] == "addefw")
                    {
                        if (!string.IsNullOrEmpty(Request["ids"]))
                        {
                            DataSet dtimg = new DataSet();
                            foreach (string s in Request["ids"].Split(','))
                            {
                                H_houseinfor h = H_houseinfor.FindByHouseID(Convert.ToDecimal(s));
                                if (DbHelperSQL.Query("select * from api_Addhouse where houseid='" + h.HouseID + "'").Tables[0].Rows.Count > 0)
                                { }
                                else
                                    DbHelperSQL.ExecuteSql("insert into api_Addhouse(erp_userid, w_userid, houseid, type) values('" + Employee.Current.EmployeeID + "', '', '" + h.HouseID + "', 'efw')");
                            }
                        }
                        JSDo_UserCallBack_Success(" formFind();$(\".HList:eq(0)\").submit();", "操作成功");
                    }

                }
                #endregion 删除房源

                #region 信息部审核照片，驳回删除照片
                if (Request.QueryString["IsSh"] != null)
                {
                    //审核
                    string IsSh = Request.QueryString["IsSh"];
                    if (!string.IsNullOrEmpty(Request["ids"]))
                    {
                        foreach (string s in Request["ids"].Split(','))
                        {
                            StringBuilder sb = new StringBuilder();
                            H_houseinfor hh = H_houseinfor.FindByHouseID(Convert.ToDecimal(s));
                            hh.IsSh = true;
                            hh.Update();

                            //在跟进中添加记录
                            h_FollowUp fup = new h_FollowUp();
                            fup.HouseID = Convert.ToDecimal(s);
                            fup.FollowUpText = "此房源照片及描述已通过信息部审核";
                            fup.EmployeeID = Current.EmployeeID;
                            fup.exe_Date = DateTime.Now;
                            fup.Insert();

                            //增加排序积分
                            s_SysParam ss = s_SysParam.FindByParamCode("houseHY");
                            //获取分隔符的值，第一个为分值，第二是否有 有效期，第三为有效期值
                            string[] ssValue = ss.Value.Split('|');

                            e_Integral ei = new e_Integral();
                            ei.employeeID = Current.EmployeeID.ToInt32().Value;
                            ei.Type = (int)integral_Type.房源与经纪人;
                            ei.tableName = "h_houseinfor";
                            ei.coloumnName = "HouseID";
                            ei.keyID = Convert.ToInt32(s);
                            ei.integralParam = "houseHY";
                            ei.integralValue = ssValue[0].ToInt32();
                            ei.integralDay = ssValue[1] == "1" ? ssValue[2].ToInt32() : 0;
                            ei.exe_Date = DateTime.Now;
                            ei.Insert();
                        }
                    }
                    #region 刷新当前页[抓取当页导航数字onclick事件]
                    string JavaScript = " dwzPageBreak({ targetType: \"navTab\", rel: \"\", data: { pageNum: " + Request["selectPage"] + "} });";
                    JSDo_UserCallBack_Success(JavaScript, "操作成功");
                    #endregion
                }

                //驳回
                if (Request.QueryString["IsBh"] != null)
                {
                    string IsBh = Request.QueryString["IsBh"];
                    if (!string.IsNullOrEmpty(Request["ids"]))
                    {
                        foreach (string s in Request["ids"].Split(','))
                        {
                            StringBuilder sb = new StringBuilder();
                            H_houseinfor hh = H_houseinfor.FindByHouseID(Convert.ToDecimal(s));
                            hh.IsBh = true;
                            hh.Update();
                        }
                    }

                    #region 刷新当前页[抓取当页导航数字onclick事件]
                    string JavaScript = " dwzPageBreak({ targetType: \"navTab\", rel: \"\", data: { pageNum: " + Request["selectPage"] + "} });";
                    JSDo_UserCallBack_Success(JavaScript, "操作成功");
                    #endregion
                }
                #endregion

                #region 审核房源
                if (Request.QueryString["StateID"] != null)
                {
                    string stateID = Request.QueryString["StateID"];
                    string msg = "操作成功";

                    if (Request["ids"].IndexOf(',') > -1)
                    {
                        if (!Current.RoleNames.Contains("信息"))
                        {
                            string JavaScript = " dwzPageBreak({ targetType: \"navTab\", rel: \"\", data: { pageNum: " + Request["selectPage"] + "} });";
                            JSDo_UserCallBack_Error(JavaScript, "不能选择多个房源操作，只能单项操作");
                        }
                    }
                    if (!string.IsNullOrEmpty(Request["ids"]))
                    {
                        foreach (string s in Request["ids"].Split(','))
                        {
                            StringBuilder sb = new StringBuilder();
                            H_houseinfor hh = H_houseinfor.FindByHouseID(Convert.ToDecimal(s));

                            //信息部审核
                            if (stateID == "999")
                            {
                                DbHelperSQL.ExecuteSql("update h_houseinfor set IsState=1 where HouseID=" + hh.HouseID);
                                DbHelperSQL.ExecuteSql(string.Format("declare @ComID int select @ComID=ComID from e_Employee where EmployeeID={1} insert h_FollowUp(HouseID,FollowUpTypeID,FollowUpText,EmployeeID,ComID) values({0},null,'信息部审核',{1},@ComID)", hh.HouseID, HouseMIS.EntityUtils.Employee.Current.EmployeeID.ToString()));
                            }
                            else
                            {
                                #region 新方法
                                //如果是信息部，则直接修改房源状态，     判断是房源是否改为委托中                //判断是否从其他状态改为非委托中
                                if (Current.RoleNames.Contains("信息") || (stateID == "2" && hh.StateID != 2) || (stateID != "2" && hh.StateID != 2))
                                {
                                    if (Current.RoleNames.Contains("信息"))
                                    {
                                        hh.state_ZBCheck = CheckState.合格.ToInt16();

                                        if (stateID == "2" && hh.StateID != 2)
                                        {
                                            //清空钥匙
                                            //增加跟进记录
                                            h_FollowUp hsf = new h_FollowUp();
                                            hsf.EmployeeID = Current.EmployeeID;
                                            hsf.HouseID = hh.HouseID;
                                            hsf.FollowUpText = "房源从" + h_State.FindByStateID(hh.StateID.Value).Name + "转为" + h_State.FindByStateID(stateID.ToInt16().Value).Name + ":清空钥匙记录";
                                            hsf.Insert();

                                            string sql = string.Format("update h_HouseKey set isdel=1 where HouseID={0}", hh.HouseID);
                                            DbHelperSQL.ExecuteSql(sql);
                                        }
                                    }
                                    //不是信息部，经纪人修改房源状态为委托中
                                    else if (stateID == "2" && hh.StateID != 2)
                                    {
                                        h_houseinfor_ZBCheck entity = new h_houseinfor_ZBCheck();
                                        entity.houseID = Convert.ToInt32(hh.HouseID);
                                        entity.state_ZBCheck = CheckState.待认证.ToInt16();
                                        entity.employeeID = Convert.ToInt32(Current.EmployeeID);
                                        entity.exe_Date = DateTime.Now;
                                        entity.comID = Current.ComID;
                                        entity.Insert();

                                        hh.state_ZBCheck = CheckState.待认证.ToInt16();

                                        //清空钥匙
                                        //增加跟进记录
                                        h_FollowUp hsf = new h_FollowUp();
                                        hsf.EmployeeID = Current.EmployeeID;
                                        hsf.HouseID = hh.HouseID;
                                        hsf.FollowUpText = "房源从" + h_State.FindByStateID(hh.StateID.Value).Name + "转为" + h_State.FindByStateID(stateID.ToInt16().Value).Name + ":清空钥匙记录";
                                        hsf.Insert();

                                        string sql = string.Format("update h_HouseKey set isdel=1 where HouseID={0}", hh.HouseID);
                                        DbHelperSQL.ExecuteSql(sql);

                                        hh.Exe_date = DateTime.Now;
                                    }
                                    hh.StateID = stateID.ToInt16();
                                    hh.IsState = 0;
                                    hh.CurrentEmployee = Current.EmployeeID;
                                    hh.Update_date = DateTime.Now;
                                    hh.Update();
                                }
                                //将修改状态存入待审核列表，由系统自动判断是否超过24小时后，再更新房源状态
                                else if (stateID != "2" && hh.StateID == 2)
                                {
                                    string str = "你的房源【编号：" + hh.Shi_id + "】被【" + Current.Em_name + "】修改为" + h_State.FindBySingleCache(stateID).Name;

                                    h_ComplaintList hc = new h_ComplaintList();
                                    hc.HouseID = hh.HouseID;
                                    hc.Exe_date = DateTime.Now;
                                    hc.Context = str;
                                    hc.Operator = Current.EmployeeID.ToInt32().Value;
                                    hc.Slemployee = hh.OwnerEmployeeID;
                                    hc.IsComplaint = 0;
                                    hc.IsOk = 0;
                                    hc.HouseOldType = hh.StateID;
                                    hc.HouseNewType = stateID.ToInt16();
                                    hc.Insert();

                                    try
                                    {
                                        MsgPush.PushMsg(str, new string[] { hh.OwnerEmployeeID.ToString() }, (int)Common.msgType.房源状态投诉); //hh.OwnerEmployeeID.ToString()
                                    }
                                    catch
                                    {
                                        continue;
                                    }
                                }
                                #endregion 新方法

                                #region 2017-09-26 旧方法
                                ////如果是信息部或者其他状态改为委托中，则直接修改房源状态，否则进入待审核房源状态列表，由信息部审核
                                //if (Current.RoleNames.Contains("信息") || (stateID == "2" && hh.StateID > 1))
                                //{
                                //    //房源跟进中增加修改状态
                                //    if (DbHelperSQL.ExecuteSql("exec dbo.h_update_HouseState " + stateID + ", " + hh.HouseID + ", " + Current.EmployeeID) > 0)
                                //    {
                                //        //判断房源原先的状态（非委托中）改为委托中，并且当前日期已经大于房源的更新时间超过15天（不包括15天）
                                //        if (hh.StateID != 2 && stateID == "2" && (DateTime.Now - hh.Update_date).TotalDays > 15)
                                //        {
                                //            //需要重新获取实体，因为在存储过程中，已经更新过
                                //            hh = H_houseinfor.FindByHouseID(Convert.ToDecimal(s));

                                //            //增加专管员跟进记录
                                //            h_FollowUp hsf = new h_FollowUp();
                                //            hsf.EmployeeID = Employee.Current.EmployeeID;
                                //            hsf.HouseID = hh.HouseID;
                                //            hsf.FollowUpText = "首录人:" + hh.OwnerEmployeeName + "→" + Employee.Current.Em_name;
                                //            hsf.Insert();

                                //            //更新该房源的首录人，用于统计员工新增房源工作量
                                //            hh.OwnerEmployeeID = Convert.ToInt32(Employee.Current.EmployeeID);
                                //            hh.OperatorID = Convert.ToInt32(Employee.Current.EmployeeID);
                                //            hh.OrgID = Convert.ToInt32(Employee.Current.OrgID);
                                //            hh.Exe_date = DateTime.Now;
                                //            hh.Update();
                                //        }
                                //        DbHelperSQL.ExecuteSql("update h_houseinfor set update_date=getdate() where HouseID=" + hh.HouseID);
                                //    }
                                //}
                                //else
                                //{
                                //    //未审核状态，信息部正在审核
                                //    if (hh.StateID == 1)
                                //    {
                                //        msg = "操作失败，请等待信息部审核";
                                //    }
                                //    else
                                //    {
                                //        if (DbHelperSQL.Exists(string.Format(@"select count(*)
                                //                                                from h_FollowAudit
                                //                                                where AuditTime is null
                                //                                                and HouseID={0}",
                                //                                                hh.HouseID)))
                                //        {
                                //            msg = "操作失败，此房源正在审核中，请等待信息部审核";
                                //        }
                                //        else
                                //        {
                                //            msg = "操作成功，请等待信息部审核";

                                //            string FollowUpText = "设置状态：";
                                //            FollowUpText += h_State.FindBySingleCache(hh.StateID).Name + "->";
                                //            FollowUpText += h_State.FindBySingleCache(stateID).Name;

                                //            h_FollowAudit hfa = new h_FollowAudit();
                                //            hfa.HouseID = hh.HouseID.ToInt32().Value;
                                //            hfa.StateID = stateID.ToInt32();
                                //            hfa.FollowText = FollowUpText;
                                //            hfa.EmployeeID = Current.EmployeeID.ToInt32().Value;
                                //            hfa.CreateTime = DateTime.Now;
                                //            hfa.Insert();

                                //            //将改房源状态改为未审核
                                //            DbHelperSQL.ExecuteSql("update h_houseinfor set StateID=1 where HouseID=" + hh.HouseID);
                                //        }
                                //    }
                                //}
                                #endregion 2017-09-26 旧方法
                            }
                        }

                        #region 刷新当前页[抓取当页导航数字onclick事件]
                        string JavaScript = " dwzPageBreak({ targetType: \"navTab\", rel: \"\", data: { pageNum: " + Request["selectPage"] + "} });";
                        JSDo_UserCallBack_Success(JavaScript, msg);
                        #endregion 刷新当前页[抓取当页导航数字onclick事件]
                    }
                }
                #endregion 审核房源
            }

            if (CheckRolePermission("导出"))
            {
                string temp = "<li><a id=\"AWageExport\" runat=\"server\" class=\"icon\" href=\"{0}\" target=\"_blank\"><span>导出</span></a></li> ";
                ExpotA = string.Format(temp, "SysConfig/ExportPage.aspx?ActionType=Export&Key=h_houseinfor&Term=" + EncodUrlParameter.EncryptPara(sb.ToString()));
            }
            else
                ExpotA = string.Format(ExpotA, "");
        }

        public StringBuilder sb = new StringBuilder();

        /// <summary>
        /// 重写查找条件
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        protected override string GetWhereClauseFromSearchBar(string typeName)
        {
            string temp1 = string.Empty, temp2 = string.Empty;

            sb.Append(base.GetWhereClauseFromSearchBar(typeName));
            if (sb.ToString().IsNullOrWhiteSpace())
            {
                sb.Append("1=1");
            }
            //过滤回收站房源和待审核房源
            sb.Append(" and deltype = 0 and aType=0");
            //sb.Append(" and deltype = 0 and HouseID not in(select HouseID from h_FollowAudit where AuditEmpID is null) ");

            if (Current.seeHouseDicList.Length > 0)
            {
                sb.Append(" and (" + GetRolePermissionEmployeeIds("查看", "OwnerEmployeeID"));
                sb.Append(" and (IsLock=0 or " + GetRolePermissionOrgIds("查看锁盘", "OrgID") + ")");
                sb.Append(" or HouseDicID in(" + Current.seeHouseDicList + "))");
            }
            else
            {
                sb.Append(" and " + GetRolePermissionEmployeeIds("查看", "OwnerEmployeeID"));
                sb.Append(" and (IsLock=0 or " + GetRolePermissionOrgIds("查看锁盘", "OrgID") + ")");
            }

            //bool isJM = false;
            //for (int i = 0; i < Current.Roles.Count; i++)
            //{
            //    //惠山区加盟店角色  2017-07-03 14:59:31
            //    if (!Current.Roles[i].Remarks.IsNullOrWhiteSpace() &&
            //        Current.Roles[i].Remarks.Contains("无锡市惠山区") &&
            //        !Current.Roles[i].seeOrgList.IsNullOrWhiteSpace())
            //    {
            //        sb.Append(" and (" + GetRolePermissionEmployeeIds("查看", "OwnerEmployeeID"));
            //        sb.Append(" and (IsLock=0 or " + GetRolePermissionOrgIds("查看锁盘", "OrgID") + ")");
            //        sb.Append(" or HouseDicID in(" + Current.Roles[i].seeOrgList + "))");
            //        isJM = true;
            //        break;
            //    }
            //}

            //if (!isJM)
            //{
            //    sb.Append(" and " + GetRolePermissionEmployeeIds("查看", "OwnerEmployeeID"));
            //    sb.Append(" and (IsLock=0 or " + GetRolePermissionOrgIds("查看锁盘", "OrgID") + ")");
            //}

            #region 客户需求匹配
            // 客户需求匹配
            if (Request.QueryString["CustomerID"] != null)
            {
                c_Customer myCC = c_Customer.FindByCustomerID(Convert.ToDecimal(Request.QueryString["CustomerID"]));

                //需求区域
                if (!myCC.NeedAreaID.IsNullOrWhiteSpace())
                {
                    s_Sanjak model_s_Sanjak = s_Sanjak.FindBySanjakID(myCC.NeedAreaID.ToDecimal().Value);
                    if (model_s_Sanjak != null)
                    {
                        sb.AppendFormat(" and SanjakID={0} ", model_s_Sanjak.AreaID);
                    }
                }
                if (myCC.NeedBuildAreaID.HasValue)       // 需求面积
                {
                    c_NeedBuildArea Cn = c_NeedBuildArea.FindByNeedBuildAreaID(myCC.NeedBuildAreaID);
                    if (Cn != null)
                    {
                        sb.AppendFormat(" and build_area>={0} ", Cn.bValue);
                        sb.AppendFormat(" and build_area<={0} ", Cn.eValue);
                    }
                }
                if (myCC.NPrice.HasValue)           // 需求价位
                {
                    sb.AppendFormat(" and sum_price>={0} ", (myCC.NPrice - 15));
                    sb.AppendFormat(" and sum_price<={0} ", (myCC.NPrice + 15));
                }

                //默认为委托中
                sb.Append(" and StateID=2");
            }
            #endregion 客户需求匹配

            #region 房源右键项目
            if (Request.QueryString["OperType"] != null)
            {
                string parms = Request.QueryString["OperType"].ToString();
                switch (parms)
                {
                    case "0":
                        sb.Append(" and aType = " + parms);
                        break;

                    case "1":
                        sb.Append(" and aType = " + parms);
                        break;
                    //我分部租房
                    case "2":
                        sb.Append(" and aType=1 and  OrgID in " + GetEmployeeOrgTableIds(Convert.ToInt32(Employee.Current.EmployeeID), RangeType.本店));
                        break;
                    //我的租房
                    case "3":
                        sb.Append(" and aType=1 and  OwnerEmployeeID= " + Employee.Current.EmployeeID.ToString());
                        break;
                    //我的售房
                    case "4":
                        sb.Append(" and aType=0 and  OwnerEmployeeID= " + Employee.Current.EmployeeID.ToString());
                        break;
                    //我的收藏
                    case "5":
                        sb.Append(" and HouseID in(select HouseID from h_HouseCollect where EmployeeID=" + Employee.Current.EmployeeID.ToString() + ")");
                        break;
                    //重复房源
                    case "6":
                        if (Request.QueryString["HouseID"] != null)
                        {
                            string HouseID = Request.QueryString["HouseID"];
                            sb.AppendFormat(" and HouseID in (select A.HouseID from h_houseinfor A left join h_houseinfor B on A.HouseDicName=B.HouseDicName and A.build_id=B.build_id and A.build_unit=B.build_unit and A.build_room=B.build_room where B.HouseID={0} and A.HouseID<>B.HouseID)", HouseID);
                        }
                        break;

                    case "7":
                        if (Request.QueryString["HouseID"] != null)
                        {
                            string HouseID = Request.QueryString["HouseID"];
                            sb.AppendFormat(" and HouseID in (select A.HouseID from h_houseinfor A left join h_houseinfor B on A.HouseDicAddress=B.HouseDicAddress and A.form_bedroom=B.form_bedroom and A.Build_area<B.Build_area+5 and A.Build_area>B.Build_area-5  where B.HouseID={0}  and A.HouseID<>B.HouseID)", HouseID);
                        }
                        break;

                    case "8": //今日房源 且有效的房源 改参数化
                        sb.Append(" and exe_date>'" + DateTime.Now.ToString("yyyy-MM-dd") + "' AND CONVERT(varchar(12),exe_date,103)=CONVERT(varchar(12),getdate(),103) AND StateID=2");
                        break;
                    //我分部售房
                    case "9":
                        if (Current.EmployeeType == "3")
                        {
                            sb.Append(" and aType=0 and  OrgID in " + GetEmployeeOrgTableIds(Convert.ToInt32(Employee.Current.EmployeeID), RangeType.大区));
                        }
                        else
                        {
                            sb.Append(" and aType=0 and  OrgID in " + GetEmployeeOrgTableIds(Convert.ToInt32(Employee.Current.EmployeeID), RangeType.本店));
                        }
                        break;

                    case "10":
                        if (Request.QueryString["HouseID"] != null)
                        {
                            string HouseID = Request.QueryString["HouseID"];
                            sb.AppendFormat(" and HouseID IN (select HouseID from h_HouseTelList where DelType=0 and Tel2 in (select Tel2 from h_HouseTelList where HouseID={0} and DelType=0))", HouseID);
                        }
                        break;
                    //我的核验房源
                    case "11":
                        sb.AppendFormat(" and HouseID IN (select distinct HouseID from h_PicList where EmployeeID = {0})", Employee.Current.EmployeeID);
                        break;
                    //导入到二手房
                    case "12": //导入到二手房
                        if (Request.QueryString["HouseID"] != null)
                        {
                            string HouseID = Request.QueryString["HouseID"];
                            H_houseinfor hs = H_houseinfor.FindByHouseID(Convert.ToDecimal(HouseID));
                            //AddHouse(用户名,密码,房源类型,室,厅,卫生间,最低价,面积,楼层,总楼层,小区名称,地址,标题,标签,是否满5年(true/false),商圈,城市,联系人,电话,内部编号,描述,备注,描述,性质(住宅),房屋朝向,房屋装修,类型)
                            shhouse.houseSoapClient aa = new shhouse.houseSoapClient();
                            DataTable d1 = H_houseinfor.Meta.Query("select  *  from Share_Personinfo where EmployeeID=" +
                                HouseMIS.EntityUtils.Employee.Current.EmployeeID.ToString()).Tables[0];
                            int s = aa.AddHouse(
                                d1.Rows[0]["shEName"].ToString(),
                                System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(d1.Rows[0]["shPwd"].ToString(), "MD5").ToLower(),
                                hs.TypeCodeName,
                                hs.Form_bedroom.ToString(),
                                hs.Form_foreroom.ToString(),
                                hs.Form_toilet.ToString(),
                                hs.Min_price.ToString(),
                                hs.Build_area.ToString(),
                                hs.Build_floor.ToString(),
                                hs.Build_levels.ToString(),
                                hs.HouseDicName.ToString(),
                                hs.HouseDicAddress.ToString(),
                                "",
                                hs.Label.ToString(),
                                hs.Pastdue.ToString(),
                                hs.SanjakName,
                                hs.AreaName,
                                hs.Landlord_name,
                                hs.Landlord_tel1,
                                hs.Shi_id,
                                hs.Facility,
                                hs.Remarks,
                                hs.LinkTel1,
                                hs.UseName,
                                hs.Orientation,
                                hs.Renovation,
                                hs.aType.ToString());
                            if (s == -2)
                            {
                                string JavaScript = " dwzPageBreak({ targetType: \"navTab\", rel: \"\", data: { pageNum: " + Request["selectPage"] + "} });";
                                JSDo_UserCallBack_Error(JavaScript, "登陆错误");
                            }
                            else if (s == -10)
                            {
                                string JavaScript = " dwzPageBreak({ targetType: \"navTab\", rel: \"\", data: { pageNum: " + Request["selectPage"] + "} });";
                                JSDo_UserCallBack_Error(JavaScript, "此房源已被导入过");
                            }
                            else
                            {
                                string JavaScript = " dwzPageBreak({ targetType: \"navTab\", rel: \"\", data: { pageNum: " + Request["selectPage"] + "} });";
                                JSDo_UserCallBack_Success(JavaScript, "导入成功");
                            }
                        }
                        break;

                    default: break;
                }
            }
            #endregion 房源右键项目

            //if (Request.QueryString["agroupId"] != null)
            //{
            //    string aGroupId = Request.QueryString["agroupId"].ToString();

            //    if (aGroupId != "0")
            //    {
            //        sb.Append(" and HouseID in(select HouseID from h_houseinfor where SanjakID in (select SanjakID from s_Sanjak where AreaID in (select AreaID from s_agroupa where AreaGroupID=" + aGroupId + " and ComID=" + TCode.YUN.yun_Company.CurrentID.ToString() + ")))");
            //    }
            //}
            if (Request.QueryString["aType"] != null)
            {
                ffrmaType.SelectedValue = Request.QueryString["aType"];

                sb.Append(" and aType=" + Request.QueryString["aType"]);
            }

            temp1 = GetMySearchControlValue("aType");
            if (!temp1.IsNullOrWhiteSpace())
            {
                ffrmaType.SelectedValue = temp1;
            }

            temp1 = GetMySearchControlValue("HouseDicName");
            if (!temp1.IsNullOrWhiteSpace())
            {
                string ids = s_HouseDic.FindIdsByHouseDicORAddress(temp1);
                if (!ids.IsNullOrWhiteSpace())
                {
                    sb.AppendFormat(" and (HouseDicID in ({0}) or HouseDicAddress like '%" + temp1 + "%')", ids);
                }
            }

            #region 楼栋号区间搜索
            temp1 = GetMySearchControlValue("build_id");
            temp2 = GetMySearchControlValue("build_id2");
            if (!temp1.IsNullOrWhiteSpace() && !temp2.IsNullOrWhiteSpace())
            {
                myffrmbuild_id.Text = temp1;
                string str_or = "(";
                int iBid = Convert.ToInt16(temp1), iBid2 = Convert.ToInt16(temp2);
                if (iBid < iBid2)
                {
                    for (int i = iBid; i < iBid2 + 1; i++)
                        str_or += " or build_id='" + i.ToString() + "'";

                    str_or = str_or.Substring(4);
                }
                str_or = "(" + str_or + ")";

                sb.Append(" and " + str_or);
            }
            else if (!temp1.IsNullOrWhiteSpace())
            {
                myffrmbuild_id.Text = temp1;

                sb.AppendFormat(" and build_id= '{0}'", temp1);
            }
            #endregion 楼栋号区间搜索

            #region 房号区间搜索
            temp1 = GetMySearchControlValue("build_room");
            temp2 = GetMySearchControlValue("build_room2");
            if (!temp1.IsNullOrWhiteSpace() && !temp2.IsNullOrWhiteSpace())
            {
                myffrmbuild_room.Text = temp1;

                string str_or = "(";
                int iBid = Convert.ToInt16(temp1), iBid2 = Convert.ToInt16(temp2);
                if (iBid < iBid2)
                {
                    for (int i = iBid; i < iBid2 + 1; i++)
                        str_or += " or build_room='" + i.ToString() + "'";

                    str_or = str_or.Substring(4);
                }
                str_or = "(" + str_or + ")";

                sb.Append(" and " + str_or);
            }
            else if (!temp1.IsNullOrWhiteSpace())
            {
                myffrmbuild_room.Text = temp1;

                sb.AppendFormat(" and build_room='{0}'", temp1);
            }
            #endregion 房号区间搜索

            temp1 = GetMySearchControlValue("note");
            if (!temp1.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" and note like '%{0}%'", temp1);
            }
            temp1 = GetMySearchControlValue("HouseDicID");
            if (!temp1.IsNullOrWhiteSpace())
            {
                string[] s = temp1.Split('|');
                sb.AppendFormat(" and HouseDicID={0} ", s[0]);

                sb.AppendFormat(" and SanjakID={0} ", s[1]);
            }
            temp1 = GetMySearchControlValue("area1");
            if (!temp1.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" and Build_area>={0}", temp1);
            }
            temp1 = GetMySearchControlValue("area2");
            if (!temp1.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" and Build_area<={0}", temp1);
            }
            temp1 = GetMySearchControlValue("Price1");
            if (!temp1.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" and sum_price>={0} ", temp1);
            }
            temp1 = GetMySearchControlValue("Price2");
            if (!temp1.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" and sum_price<={0} ", temp1);
            }
            temp1 = GetMySearchControlValue("Ohter2ID1");
            if (!temp1.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" and Ohter2ID>={0} ", temp1);
            }
            temp1 = GetMySearchControlValue("Ohter2ID2");
            if (!temp1.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" and Ohter2ID<={0} ", temp1);
            }
            temp1 = GetMySearchControlValue("exe_date1");
            if (!temp1.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" and exe_date>='{0}' ", temp1);
            }
            temp1 = GetMySearchControlValue("exe_date2");
            if (!temp1.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" and exe_date<='{0} 23:59:59' ", temp1);
            }
            temp1 = GetMySearchControlValue("FollowUp_Date1");
            if (!temp1.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" and FollowUp_Date>='{0}' ", temp1);
            }
            temp1 = GetMySearchControlValue("FollowUp_Date2");
            if (!temp1.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" and FollowUp_Date<='{0} 23:59:59' ", temp1);
            }
            temp1 = GetMySearchControlValue("update_date1");
            if (!temp1.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" and update_date>='{0}' ", temp1);
            }
            temp1 = GetMySearchControlValue("update_date2");
            if (!temp1.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" and update_date<='{0} 23:59:59' ", temp1);
            }
            temp1 = GetMySearchControlValue("build_floor1");
            if (!temp1.IsNullOrWhiteSpace())
            {
                myffrmbuild_floor1.Text = temp1;

                sb.AppendFormat(" and build_floor>='{0}' ", temp1.ToInt32());
            }
            temp1 = GetMySearchControlValue("build_floor2");
            if (!temp1.IsNullOrWhiteSpace())
            {
                myffrmbuild_floor2.Text = temp1;

                sb.AppendFormat(" and build_floor<='{0}' ", temp1.ToInt32());
            }
            temp1 = GetMySearchControlValue("Build_area1");
            if (!temp1.IsNullOrWhiteSpace())
            {
                myffrmarea1.Text = temp1;

                sb.AppendFormat(" and Build_area>={0} ", temp1);
            }
            temp1 = GetMySearchControlValue("Build_area2");
            if (!temp1.IsNullOrWhiteSpace())
            {
                myffrmarea2.Text = temp1;

                sb.AppendFormat(" and Build_area<={0} ", temp1);
            }
            temp1 = GetMySearchControlValue("dprice1");
            if (!temp1.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" and (sum_price * 10000)/build_area>={0} ", temp1);
            }
            temp1 = GetMySearchControlValue("dprice2");
            if (!temp1.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" and (sum_price * 10000)/build_area<={0} ", temp1);
            }
            //总价
            temp1 = GetMySearchControlValue("sum_price1");
            if (!temp1.IsNullOrWhiteSpace())
            {
                myffrmPrice1.Text = temp1;

                sb.AppendFormat(" and sum_price>={0} ", temp1);
            }
            temp1 = GetMySearchControlValue("sum_price2");
            if (!temp1.IsNullOrWhiteSpace())
            {
                myffrmPrice2.Text = temp1;

                sb.AppendFormat(" and sum_price<={0} ", temp1);
            }
            //单价
            temp1 = GetMySearchControlValue("Ohter2ID1");
            if (!temp1.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" and Ohter2ID>={0} ", temp1);
            }
            temp1 = GetMySearchControlValue("Ohter2ID2");
            if (!temp1.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" and Ohter2ID<={0} ", temp1);
            }
            temp1 = GetMySearchControlValue("Rent_Price1");
            if (!temp1.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" and Rent_Price>={0} ", temp1);
            }
            temp1 = GetMySearchControlValue("Rent_Price2");
            if (!temp1.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" and Rent_Price<={0} ", temp1);
            }


            temp1 = GetMySearchControlValue("OwnerEmployeeID");
            if (!temp1.IsNullOrWhiteSpace())
            {
                myffrmOwnerEmployeeID.Text = temp1;
                sb.AppendFormat(" and OwnerEmployeeID in (select EmployeeID from e_Employee where em_name like '%{0}%') ", temp1);
            }
            temp1 = GetMySearchControlValue("OwenEmployeeID");
            if (!temp1.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" and OwnerEmployeeID in (select EmployeeID from e_Employee where em_name like '%{0}%') ", temp1);
            }
            //temp1 = GetMySearchControlValue("shi_addr");
            //if (temp1 == "on")
            //{
            //    sb.AppendFormat(" and HouseID in (select HouseID from h_houseinfor h left join s_HouseDicFloorPrice s on h.build_floor=s.Floor and h.HouseDicID=s.HouseDicID and h.Build_id=s.Build_id  where Price>0) ");
            //}
            temp1 = GetMySearchControlValue("HXImg");
            if (temp1 == "on")
            {
                sb.AppendFormat(" and HouseID in (select HouseID from h_PicList where PicTypeID=1) ");
            }
            temp1 = GetMySearchControlValue("IsLock");
            if (temp1 == "on")
            {
                sb.AppendFormat(" and IsLock=1 ");
            }
            temp1 = GetMySearchControlValue("HasRecord");
            if (temp1 == "on")
            {
                sb.AppendFormat(" and (HasRecord=1 or HouseID in(select houseID from i_InternetPhone  where recordUrlDel=1))");
            }
            //temp1 = GetMySearchControlValue("AllView");
            //if (temp1 == "on")
            //{
            //    sb.AppendFormat(" and HouseID  in (select HouseID from h_AllViewPic)  ");
            //}
            temp1 = GetMySearchControlValue("HasKey");
            if (temp1 == "on")
            {
                pHasKey = "1";
                sb.AppendFormat(" and HasKey=1 ");
            }
            temp1 = GetMySearchControlValue("IsBeiAn");
            if (temp1 == "on")
            {
                sb.AppendFormat(" and IsBeiAn=1 ");
            }
            //查询有照片的
            pHasImage = "1";
            sb.AppendFormat(" and HasImage=1 ");

            temp1 = GetMySearchControlValue("SuXiao");
            if (temp1 == "on")
            {
                sb.AppendFormat(" and EntrustTypeID in (4,6,7) ");
            }
            temp1 = GetMySearchControlValue("CanKP");
            if (temp1 == "on")
            {
                sb.AppendFormat(" and StateID=2 and state_ZBCheck!=1  and HouseID not in (select houseid from  h_houseinfor_ZBCheck   WHERE phoneid is NOT null AND audit_Date is null AND isDel=0 AND houseid=HouseID) and HouseID not in(SELECT houseid FROM h_houseinfor_ZBCheck WHERE phoneid IS NULL AND employee_auditid IS NULL AND isdel = 0 AND Datediff(hh, exe_date, Getdate()) <= 48 AND houseid=HouseID) ");
            }

            temp1 = GetMySearchControlValue("finPhoto");
            if (temp1 == "on")
            {
                sb.AppendFormat(" and HouseID in (select HouseID from h_PicList_YY where Finishtime is not null and IsCheck=1) ");
            }
            temp1 = GetMySearchControlValue("IsVideo");
            if (temp1 == "on")
            {
                sb.AppendFormat(" and IsVideo=1 ");
            }
            temp1 = GetMySearchControlValue("IsBring");
            if (temp1 == "on")
            {
                sb.AppendFormat(" and shi_id in (select HouseZPcheck from c_BringCustomer)");
            }
            temp1 = GetMySearchControlValue("IsPrivate");
            if (temp1 == "on")
            {
                sb.AppendFormat(" and IsPrivate=1 ");
            }
            //temp1 = GetMySearchControlValue("AssessStateError");
            //if (temp1 == "on")
            //{
            //    sb.AppendFormat(" and StateID<>(select top 1 StateID from h_AssessState where AssessStateID=h_houseinfor.AssessStateID)  ");
            //}
            temp1 = GetMySearchControlValue("EntrustType");
            if (temp1 == "on")
            {
                sb.AppendFormat(" and EntrustTypeID={0} ", h_EntrustType.Find("Name", "独家委托").EntrustTypeID.ToString());
            }

            temp1 = GetMySearchControlValue("Shi_id");
            if (!temp1.IsNullOrWhiteSpace() && temp1.Length > 1)
            {
                myffrmShi_id.Text = temp1;

                if (Regex.Match(temp1.Substring(0, 1), "^[A-Za-z]+$").Success)
                {
                    sb.AppendFormat(" and Shi_id like '{0}%' ", temp1);
                }
                else
                {
                    sb.AppendFormat(" and Shi_id like '%{0}' ", temp1);
                }
            }
            temp1 = GetMySearchControlValue("Remarks");
            if (!temp1.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" and Remarks like '%{0}%' ", temp1);
            }
            temp1 = GetMySearchControlValue("HouseDicAddress");
            if (!temp1.IsNullOrWhiteSpace())
            {
                string ids = s_HouseDic.FindIdsByHouseDicORAddress(temp1);
                if (!ids.IsNullOrWhiteSpace())
                {
                    sb.AppendFormat(" and HouseDicID in ({0}) ", ids);
                }
            }
            temp1 = GetMySearchControlValue("AreaID");
            if (!temp1.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" and SanjakID in (select SanjakID from s_Sanjak where AreaID={0}) ", temp1);
            }
            //照片是否审核
            temp1 = GetMySearchControlValue("IsSh");
            if (temp1 == "1")
            {
                sb.AppendFormat(" and IsSh ={0}", temp1);
            }
            else if (temp1 == "0")
            {
                sb.AppendFormat(" and IsSh is NULL", temp1);
            }
            else if (temp1 == "2")
            {

            }
            else
            {
                sb.AppendFormat(" and IsSh is NULL", temp1);
            }
            //待定照片
            temp1 = GetMySearchControlValue("IsBh");
            if (temp1 == "1")
            {
                sb.AppendFormat(" and IsBh =1", temp1);
            }
            else if (temp1 == "0")
            {
                sb.AppendFormat(" and IsBh is NULL", temp1);
            }
            else if (temp1 == "2")
            {

            }
            else
            {
                sb.AppendFormat(" and IsBh is NULL", temp1);
            }

            //查询上传第一张照片已经相距20分钟的房源
            sb.AppendFormat(" and datediff(mi,update_date,GETDATE())>20 and CONVERT(varchar(10), update_date, 23)>'2018-01-03' and HouseID in(select HouseID from h_PicList where CONVERT(varchar(10), exe_date, 23)>'2018-01-03')", temp1);

            #region 区域商圈小区搜索
            temp1 = GetMySearchControlValue("txtArea");
            temp2 = GetMySearchControlValue("txtArea2");

            if (!temp2.IsNullOrWhiteSpace())
            {
                myffrmtxtArea2.Text = temp2;

                if (!temp1.IsNullOrWhiteSpace())
                    myffrmtxtArea.Text = temp1;

                string strQ = "", strS = "", strX = "", strVal = "", strVal2 = "", strSearch = "";
                string[] sArry = temp2.Split(',');
                for (int i = 0; i < sArry.Length - 1; i++)
                {
                    strVal = sArry[i].Split('_')[0].ToString();
                    strVal2 = sArry[i].Split('_')[1].ToString();
                    if (strVal == "q")
                        strQ += "," + strVal2;
                    else if (strVal == "s")
                        strS += "," + strVal2;
                    else if (strVal == "x")
                        strX += "," + strVal2;
                }

                strSearch = "(";
                if (strQ.Length > 0)
                {
                    strQ = strQ.Substring(1);
                    strSearch += " SanjakID in (select SanjakID from s_Sanjak where AreaID in(" + strQ + ")) ";
                }
                if (strS.Length > 0)
                {
                    strS = strS.Substring(1);
                    if (strSearch.Length > 1)
                        strSearch += " or  SanjakID  in(" + strS + ") ";
                    else
                        strSearch += " SanjakID  in(" + strS + ") ";
                }
                if (strX.Length > 0)
                {
                    strX = strX.Substring(1);
                    if (strSearch.Length > 1)
                        strSearch += " or  HouseDicID  in(" + strX + ") ";
                    else
                        strSearch += " HouseDicID  in(" + strX + ") ";
                }
                strSearch += ")";

                sb.AppendFormat(" and " + strSearch);
            }
            #endregion 区域商圈小区搜索

            #region
            temp1 = GetMySearchControlValue("form_bedroom1");
            temp2 = GetMySearchControlValue("form_bedroom2");
            if (!temp1.IsNullOrWhiteSpace() && !temp2.IsNullOrWhiteSpace())
            {
                myffrmform_bedroom1.Text = temp1;
                myffrmform_bedroom2.Text = temp2;
                //myffrmform_bedroom.SelectedValue = temp1;
                bRoom = true;

                if (temp1 == "999" && temp2 == "999")
                    sb.AppendFormat(" and form_bedroom>5");
                else
                    sb.AppendFormat(" and form_bedroom>={0} and form_bedroom<={1} ", temp1, temp2);
            }
            #endregion

            #region 总部认证
            temp1 = GetMySearchControlValue("state_ZBCheck");
            if (!temp1.IsNullOrWhiteSpace())
            {
                if (temp1.ToInt16() == CheckState.待认证.ToInt16())
                {
                    sb.AppendFormat(" and (state_ZBCheck is null or state_ZBCheck={0}) ", temp1);
                }
                else
                {
                    sb.AppendFormat(" and state_ZBCheck={0} ", temp1);
                }
            }
            #endregion


            #region 是否导入efw
            temp1 = GetMySearchControlValue("isefw");

            if (!temp1.IsNullOrWhiteSpace())
            {
                if (temp1 == "1")//待上架
                    sb.AppendFormat(" and houseid in (select houseid from api_Addhouse where type='efw' and w_url is null) ", temp1);
                else if (temp1 == "2")//否
                    sb.AppendFormat(" and houseid not in (select houseid from api_Addhouse where type='efw') ", temp1);
                else if (temp1 == "3")//是
                    sb.AppendFormat(" and houseid in (select houseid from api_Addhouse where type='efw' and w_url is not null) ", temp1);
            }
            #endregion

            #region 标签
            temp1 = GetMySearchControlValue("Label");

            if (!temp1.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" and Label={0} ", temp1);
            }
            #endregion

            temp1 = GetMySearchControlValue("LinkTel2");
            if (!temp1.IsNullOrWhiteSpace())
            {
                myffrmlandlord_tel2.Text = temp1;

                sb.AppendFormat(" and LinkTel2 like '%{0}%' ", temp1.TelEncrypt2(false));
            }

            //楼盘/地址
            temp1 = GetMySearchControlValue("EstateOrAddress");
            if (!temp1.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" and (HouseDicName like '%{0}%') ", temp1);//
            }
            else if (Request["HouseDicID"].ToInt32() > 0)
            {
                sb.AppendFormat(" and (HouseDicID={0}) ", Request["HouseDicID"].ToInt32());
                if (this.pagerForm.Action.IndexOf("HouseDicID") == -1)
                    this.pagerForm.Action += "&HouseDicID=" + Request["HouseDicID"];
            }

            temp1 = GetMySearchControlValue("landlord_tel2");
            if (!temp1.IsNullOrWhiteSpace())
            {
                string ids = H_houseinfor.FindHouseIDsByTel(temp1.Trim());
                if (ids.IsNullOrWhiteSpace())
                {
                    sb.Append(" and 1=2");
                }
                else
                    sb.AppendFormat(" and HouseID in ({0})", ids);
            }
            temp1 = GetMySearchControlValue("landlord_name");
            if (!temp1.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" and landlord_name like '%{0}%' ", temp1);
            }
            temp1 = GetMySearchControlValue("BackTel");
            if (!temp1.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" and BackTel like '%{0}%' ", temp1.TelEncrypt2(false));
            }
            temp1 = GetMySearchControlValue("OrgID");
            if (!temp1.IsNullOrWhiteSpace() && temp1.ToInt32() > 0)
            {
                sb.AppendFormat(" and OrgID in (select orgid from s_Organise where charindex(',{0},',IdPath)>0)", temp1);
            }

            if (sb.ToString().IndexOf("StateID") == -1)
            {
                sb.AppendFormat(" and StateID in (1,2)");
            }
            else if (sb.ToString().IndexOf("StateID=0") > -1)
            {
                sb.Replace("StateID=0", "1=1");
            }
            if (isMapForm)
            {
                h_State hstate = h_State.Find(" Name='委托中' and comid=" + Employee.Current.ComID);

                sb.AppendFormat(" and StateID=" + hstate.StateID.ToString());
            }

            return sb.Length == 0 ? null : sb.ToString();
        }

        protected string str(object num)
        {
            if (num != null)
            {
                string arraylist = Convert.ToString(num);
                int temp = 0;
                string str1 = "";
                for (int i = arraylist.Length - 1; i >= 0; i--)
                {
                    if (arraylist[i].ToString() != "0")
                    {
                        temp = i;
                        break;
                    }
                }
                for (int i = 0; i <= temp; i++)
                {
                    str1 += arraylist[i].ToString();
                }
                if (str1.IndexOf(".") == str1.Length - 1)
                    str1 = str1.Replace(".", "");
                return str1;
            }
            else
            {
                return "";
            }
        }
    }
}
