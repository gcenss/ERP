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

namespace HouseMIS.Web.House
{
    public partial class HouseCheck : EntityListBase<H_houseinfor>
    {
        public override string MenuCode
        {
            get
            {
                return "House2002";
            }
        }

        protected String GetAtype(object AtypeName)
        {
            //s_SysParam hsc = s_SysParam.FindByParamCode("HouseStateColor");
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

        public string sty = "";

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            //hsc = s_SysParam.FindByParamCode("HouseStateColor");
            gv.RowCreated += new GridViewRowEventHandler(gv_RowCreated);
            if (HouseMIS.EntityUtils.Employee.Current != null && HouseMIS.EntityUtils.Employee.Current.ComID != 20)
            {
                sty = "style='display:none'";
            }
        }

        //protected String GetAStyle(object aid, object bid)
        //{
        //    h_AssessState has = h_AssessState.FindWithCache(h_AssessState._.AssessStateID, aid);
        //    if (has != null && has.StateID.ToString() != bid.ToString())
        //    {
        //        return " style=\"color:Green\" ";
        //    }
        //    return null;
        //}

        //protected string GetBottoms()
        //{
        //    StringBuilder sb = new StringBuilder();
        //    List<h_AssessState> h_States = h_AssessState.FindAll();

        //    foreach (h_AssessState hr in h_States)
        //    {
        //        sb.Append("<li><a class=\"iconL\" href=\"House/HouseCheck.aspx?AssessStateID=" + hr.AssessStateID + "&NavTabId=" + NavTabId + "&selectPage=" + (gv.PageIndex + 1).ToString() + "&doAjax=true&HouseID={HouseID}\" rel=\"hcids\" target=\"selectedTodo\"><span>" + hr.Name + "</span></a></li>");
        //    }
        //    return sb.ToString();
        //}

        protected string GetBottom()
        {
            StringBuilder sb = new StringBuilder();
            List<h_State> h_States = h_State.FindAllWithCache();

            foreach (h_State hr in h_States)
            {
                sb.Append("<li><a class=\"iconL\" href=\"House/HouseCheck.aspx?StateID=" + hr.StateID + "&NavTabId=" + NavTabId + "&selectPage=" + (gv.PageIndex + 1).ToString() + "&doAjax=true&HouseID={HouseID}\" rel=\"hcids\" target=\"selectedTodo\"><span>" + hr.Name + "</span></a></li>");
            }
            return sb.ToString();
        }

        [AjaxPro.AjaxMethod]
        public string GetHTML(string key)
        {
            StringBuilder sb = new StringBuilder();
            List<h_FollowUp> H_FollowUps = h_FollowUp.FindAll("select top 5 * from h_FollowUp where HouseID=" + key + " order by exe_date desc");
            foreach (h_FollowUp hr in H_FollowUps)
            {
                sb.Append("<tr target=\"KeyValue\" rel=\"\">");
                sb.Append("<td>" + hr.FollowUpType + "</td>");
                sb.Append("<td>" + hr.FollowUpText + "</td>");
                sb.Append("<td>" + hr.EmployeeName + "</td>");
                sb.Append("<td>" + hr.exe_Date + "</td>");
                sb.Append("</tr>");
            }
            return sb.ToString();
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
                e.Row.Attributes.Add("onclick", "ReloadHouseCheck('" + gv.DataKeys[e.Row.RowIndex].Value + "')");
                if (gv.DataKeys[e.Row.RowIndex]["HouseID"] != null && gv.DataKeys[e.Row.RowIndex]["shi_id"] != null && gv.DataKeys[e.Row.RowIndex]["aType"] != null)
                    e.Row.Attributes.Add("ondblclick", "OpenHouseEditC('" + gv.DataKeys[e.Row.RowIndex]["HouseID"].ToString() + "','" + gv.DataKeys[e.Row.RowIndex]["shi_id"].ToString() + "','" + gv.DataKeys[e.Row.RowIndex]["aType"].ToString() + "')");
            }
        }

        //public s_SysParam hsc = s_SysParam.FindByParamCode("HouseStateColor");

        //protected String GetColStyle(object SeeHouseType)
        //{
        //    if (hsc != null)
        //    {
        //        string[] ss = hsc.Value.Split(',');
        //        foreach (string sv in ss)
        //        {
        //            string[] str = sv.Split('|');
        //            if (str[0] == SeeHouseType.ToString() && str[1] != "")
        //            {
        //                return " style=\"color:#" + str[1] + "\" ";
        //            }
        //        }
        //    }
        //    return null;
        //}

        /// <summary>
        /// 页面状态下拉框绑定
        /// </summary>
        /// <returns></returns>
        //public void HouseState()
        //{
        //    IEntityList ls = h_State.FindAll();
        //    this.ffrmState.Items.Clear();
        //    ffrmState.Items.Add(new ListItem("请选择", ""));
        //    if (ls != null)
        //    {
        //        for (int i = 0; i < ls.Count; i++)
        //        {
        //            ffrmState.Items.Add(new ListItem(ls[i]["Name"].ToString(), ls[i]["StateID"].ToString()));
        //        }
        //    }
        //}

        //protected void FullSubGroup()
        //{
        //EntityList<e_SubGroup> list = e_SubGroup.FindAll();
        //myffrmSubGroupID.Items.Add(new ListItem("", ""));
        //foreach (e_SubGroup es in list)
        //{
        //    myffrmSubGroupID.Items.Add(new ListItem(es.People, es.SubGroupID.ToString()));
        //}
        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AjaxPro.Utility.RegisterTypeForAjax(typeof(HouseCheck), this.Page);
                if (!CheckRolePermission("查看"))
                {
                    Response.End();
                }

                //if (Request.QueryString["AssessStateID"] != null)
                //{
                //    if (!String.IsNullOrEmpty(Request["hcids"]))
                //        foreach (string s in Request["hcids"].Split(','))
                //        {
                //            System.Text.StringBuilder sb = new System.Text.StringBuilder();
                //            H_houseinfor hh = H_houseinfor.FindByHouseID(Convert.ToDecimal(s));
                //            h_AssessState ha = h_AssessState.Find(h_AssessState._.AssessStateID, Request.QueryString["AssessStateID"]);
                //            if (ha != null && hh != null && ha.aType == hh.aType)
                //            {
                //                hh.AssessStateID = Request.QueryString["AssessStateID"].ToInt32();
                //                hh.Update();

                //                #region 刷新当前页[抓取当页导航数字onclick事件]

                //                // 当前页
                //                // 翻页  dwzPageBreak({ targetType: targetType, rel: rel, data: { pageNum: event.data.pageNum} });
                //                //String JavaScript = "alert('" + Request["selectPage"] + "');";
                //                String JavaScript = " dwzPageBreak({ targetType: \"navTab\", rel: \"\", data: { pageNum: " + Request["selectPage"] + "},callback:ShowNote });";
                //                JSDo_UserCallBack_Success(JavaScript, "操作成功");

                //                #endregion 刷新当前页[抓取当页导航数字onclick事件]
                //            }
                //            else
                //            {
                //                JSDo_UserCallBack_Error("", "租售状态不一致");
                //            }
                //        }
                //}
                FullDropListData(typeof(h_State), this.ffrmStateID, "Name", "StateID", "");

                string OperType = Request.QueryString["OperTypes"];
                if (OperType != null)
                {
                    string HouseID = Request["HouseID"];
                    if (HouseID == null) HouseID = "";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    switch (OperType)
                    {
                        case "3": //推见房源
                            s_SysParam ss = s_SysParam.FindByParamCode("RecommendNum");
                            int hrs = Convert.ToInt32(DbHelperSQL.GetSingle("select count(*) from h_Recommend where CONVERT(varchar,exe_date,112)=CONVERT(varchar,getdate(),112)"));
                            if (hrs < Convert.ToInt32(ss.Value))
                            {
                                DbHelperSQL.ExecuteSql(string.Format("update h_houseInfor set update_date=getdate() where HouseID = {0}", HouseID));
                                h_Recommend Hr = new h_Recommend();
                                Hr.EmployeeID = Employee.Current.EmployeeID;
                                Hr.HouseID = Convert.ToDecimal(HouseID);
                                Hr.Insert();
                            }
                            else
                            {
                                sb.Append("{\r\n");
                                sb.Append("   \"statusCode\":\"300\", \r\n");
                                sb.Append("   \"message\":\"操作失败，您今天房源的推荐数已满请明天再试！\", \r\n");
                                sb.Append("   \"navTabId\":\"" + NavTabId + "\", \r\n");
                                sb.Append("   \"rel\":\"1\", \r\n");
                                sb.Append("   \"forwardUrl\":\"\"\r\n");
                                sb.Append("}\r\n");
                                Response.Write(sb.ToString());
                                Response.End();
                            }
                            break;

                        case "4": //收藏房源
                            string sql = string.Format(@"if NOT EXISTS
                                                            (SELECT 1
                                                            FROM h_HouseCollect
                                                            WHERE HouseID={0}
                                                                    AND EmployeeID={1}) insert into h_HouseCollect(HouseID,EmployeeID) Values({0},{1})", HouseID, Employee.Current.EmployeeID);
                            DbHelperSQL.ExecuteSql(sql);
                            break;

                        case "5": //取消收藏房源
                            h_HouseCollect houseCollect = h_HouseCollect.Find(new String[] { "HouseID", "EmployeeID" }, new Object[] { HouseID, Employee.Current.EmployeeID });
                            if (houseCollect != null)
                                houseCollect.Delete();
                            break;

                        case "6": //查看钥匙
                            DataTable dt = DbHelperSQL.Query(string.Format(@"SELECT TOP 1 H.IsIn
                                                                                FROM h_HouseKey H
                                                                                WHERE H.HouseID ={0}
                                                                                        AND h.isDel=0
                                                                                ORDER BY  H.exe_date DESC",
                                                                                HouseID)).Tables[0];
                            sb.Append("{\r\n");
                            sb.Append("   \"statusCode\":\"200\", \r\n");
                            if (dt.Rows.Count > 0)
                            {
                                if (Convert.ToBoolean(dt.Rows[0][0]) == true)
                                    sb.Append("   \"message\":\"该房源已经拿到钥匙\", \r\n");
                                else
                                    sb.Append("   \"message\":\"该房源没有拿钥匙\", \r\n");
                            }
                            else
                            {
                                sb.Append("   \"message\":\"该房源没有拿钥匙\", \r\n");
                            }
                            sb.Append("}\r\n");
                            Response.Write(sb.ToString());
                            Response.End();
                            break;
                    }
                    sb.Append("{\r\n");
                    sb.Append("   \"statusCode\":\"200\", \r\n");
                    sb.Append("   \"message\":\"操作成功！\", \r\n");
                    sb.Append("}\r\n");
                    Response.Write(sb.ToString());
                    Response.End();
                }
                if (Request.QueryString["doType"] != null)
                {
                    if (Request.QueryString["doType"] == "del")
                    {
                        if (CheckRolePermission("删除房源"))
                        {
                            if (!String.IsNullOrEmpty(Request["hcids"]))
                                foreach (string s in Request["hcids"].Split(','))
                                {
                                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                    H_houseinfor h = H_houseinfor.FindByHouseID(Convert.ToDecimal(s));
                                    if (h != null && CheckRolePermission("删除房源", h.OwnerEmployeeID.ToDecimal()))
                                    {
                                        //删除房源进入回收站
                                        h.DelType = true;
                                        h.DelEmployeeID = Convert.ToInt32(Employee.Current.EmployeeID);
                                        h.DelDate = System.DateTime.Now;
                                        h.Update();
                                    }
                                }
                            JSDo_UserCallBack_Success(" formFind();$(\".houseCheck:eq(0)\").submit();", "操作成功");
                        }
                        else
                        {
                            JSDo_UserCallBack_Success(" formFind();$(\".houseCheck:eq(0)\").submit();", "操作失败,您没有删除该房源的权限！");
                        }
                    }
                }
                if (Request.QueryString["StateID"] != null)
                {
                    if (!String.IsNullOrEmpty(Request["hcids"]))
                        foreach (string s in Request["hcids"].Split(','))
                        {
                            System.Text.StringBuilder sb = new System.Text.StringBuilder();
                            H_houseinfor hh = H_houseinfor.FindByHouseID(Convert.ToDecimal(s));

                            if (H_houseinfor.Meta.Execute("exec dbo.h_update_HouseState " + Request.QueryString["StateID"] + ", " + hh.HouseID + ", " + Employee.Current.EmployeeID.ToString()) > 0)
                            {
                                string StateName = h_State.Find("StateID", Request.QueryString["StateID"]).Name;

                                #region 大积分添加处

                                //if (s_SysParam.FindByParamCode("UseBigPoint").Value == "1")
                                //{
                                //    //无效房源减分
                                //    if (StateName != "有效" && StateName != "我售")
                                //    {
                                //        I_IntegralLog.DeleteLog("添加房源积分", "h_houseinfor", hh.HouseID.ToString());
                                //    }
                                //    else
                                //    {
                                //        if (hh.SeeHouseType != "有效" && hh.SeeHouseType != "我售")
                                //        {
                                //            H_houseinfor.UpdateJF(hh.Sum_price, hh.OwnerEmployeeID, hh.HouseID, "添加房源积分");
                                //        }
                                //    }
                                //}

                                #endregion 大积分添加处

                                if (hh.Shi_id.IndexOf("KZ") > 0 || hh.Shi_id.IndexOf("MWZ") > 0)
                                {
                                }
                                else
                                {
                                    if (StateName == "重复")
                                    {
                                        if (hh.StateID.ToString() != Request.QueryString["StateID"].ToString())
                                        {
                                            TimeSpan ts = DateTime.Now - hh.Exe_date;
                                            if (ts.Days == 0)
                                            {
                                                e_Rewards er = new e_Rewards();
                                                er.AwardTypeID = 15;
                                                er.Cause = "重复房源扣款暂存-房源号：" + hh.Shi_id;
                                                //e_SubGroupDetail es = e_SubGroupDetail.Find("OrgID", hh.OrgID);
                                                er.EmployeeID = 0;
                                                er.OperID = 2;
                                                er.OrgID = hh.OrgID;
                                                er.Moneys = 5;
                                                er.Rewards_Date = DateTime.Now;
                                                er.Active_Date = DateTime.Now;
                                                er.Insert();
                                                e_Rewards ers = new e_Rewards();
                                                ers.AwardTypeID = 15;
                                                ers.Cause = "重复房源扣款-房源号：" + hh.Shi_id;
                                                ers.EmployeeID = hh.OperatorID;
                                                ers.OperID = 2;
                                                ers.OrgID = hh.OrgID;
                                                ers.Moneys = -5;
                                                ers.Rewards_Date = DateTime.Now;
                                                ers.Active_Date = DateTime.Now;
                                                ers.Insert();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (h_State.Find("StateID", hh.StateID).Name == "重复")
                                        {
                                            TimeSpan ts = DateTime.Now - hh.Exe_date;
                                            if (ts.Days == 0)
                                            {
                                                DataSet ds = e_Rewards.Meta.Query("select * from e_Rewards where Cause ='重复房源扣款暂存-房源号：" + hh.Shi_id + "'");
                                                if (ds.Tables[0].Rows.Count > 0)
                                                {
                                                    e_Rewards er = e_Rewards.Find("RewardsID", ds.Tables[0].Rows[0]["RewardsID"]);
                                                    if (er != null)
                                                    {
                                                        er.Delete();
                                                    }
                                                }
                                                DataSet dsa = e_Rewards.Meta.Query("select * from e_Rewards where Cause ='重复房源扣款-房源号：" + hh.Shi_id + "'");
                                                if (dsa.Tables[0].Rows.Count > 0)
                                                {
                                                    e_Rewards er = e_Rewards.Find("RewardsID", dsa.Tables[0].Rows[0]["RewardsID"]);
                                                    if (er != null)
                                                    {
                                                        er.Delete();
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            H_houseinfor.Meta.Execute("update h_houseinfor set update_date=getdate() where HouseID=" + hh.HouseID);
                        }

                    //保存搜索条件再刷一次列表
                    //JSDo_UserCallBack_Success(" $(\".houseCheck:eq(0)\").submit();", "操作成功");

                    #region 不刷列表，只刷新状态单元格

                    //h_State state = h_State.FindByStateID(Request.QueryString["StateID"].ToDecimal().Value);
                    //String JavaScript = "$(\"#check_" + Request.QueryString["HouseID"] + "\").html('"+state.Name+"');";
                    //JavaScript += "ReloadHouseCheck('" + Request.QueryString["HouseID"] + "')";
                    //JSDo_UserCallBack_Success(JavaScript, "操作成功");

                    #endregion 不刷列表，只刷新状态单元格

                    #region 刷新当前页[抓取当页导航数字onclick事件]

                    // 当前页
                    // 翻页  dwzPageBreak({ targetType: targetType, rel: rel, data: { pageNum: event.data.pageNum} });
                    //String JavaScript = "alert('" + Request["selectPage"] + "');";
                    String JavaScript = " dwzPageBreak({ targetType: \"navTab\", rel: \"\", data: { pageNum: " + Request["selectPage"] + "} });";
                    JSDo_UserCallBack_Success(JavaScript, "操作成功");

                    #endregion 刷新当前页[抓取当页导航数字onclick事件]

                    //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    //sb.Append("{\r\n");
                    //sb.Append("   \"statusCode\":\"200\", \r\n");
                    //sb.Append("   \"message\":\"操作成功！\", \r\n");
                    //sb.Append("   \"navTabId\":\"" + NavTabId + "\", \r\n");
                    //sb.Append("   \"rel\":\"1\", \r\n");
                    //sb.Append("   \"forwardUrl\":\"\"\r\n");
                    //sb.Append("}\r\n");
                    //Response.Write(sb.ToString());
                    //Response.End();
                }
            }
        }

        //protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        e.Row.Attributes["style"] = "cursor:pointer";
        //        e.Row.Attributes["title"] = "双击查看跟进";
        //        e.Row.Attributes.Add("onclick", "ReloadHouseCheck('" + gv.DataKeys[e.Row.RowIndex].Value + "')");

        //    }
        //}
        ///// <summary>
        ///// 推荐房源
        ///// </summary>
        ///// <param name="HouseID"></param>
        //[AjaxPro.AjaxMethod]
        //public void tuijian(string HouseID)
        //{
        //     H_houseinfor.Meta.Query("update h_houseInfor set update_date=getdate() where HouseID = " + HouseID);
        //}

        /// <summary>
        /// 重写查找条件
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        protected override string GetWhereClauseFromSearchBar(string typeName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.GetWhereClauseFromSearchBar(typeName));
            string parms = Request.QueryString["OperType"];
            switch (parms)
            {
                case "0":
                    if (sb.Length > 0)
                        sb.Append(" AND ");
                    sb.AppendFormat(" aType = " + parms);
                    break;

                case "1":
                    if (sb.Length > 0)
                        sb.Append(" AND ");
                    sb.AppendFormat(" aType = " + parms);
                    break;

                case "2":
                    if (sb.Length > 0)
                        sb.Append(" AND ");
                    sb.AppendFormat(" OrgID in (select OrgID from s_Organise where BillCode=(select BillCode from s_Organise where OrgID=" + HouseMIS.EntityUtils.Employee.Current.OrgID.ToString() + "))");
                    break;

                case "3":
                    if (sb.Length > 0)
                        sb.Append(" AND ");
                    sb.AppendFormat(" OrgID= " + HouseMIS.EntityUtils.Employee.Current.OrgID.ToString());
                    break;

                case "4":
                    if (sb.Length > 0)
                        sb.Append(" AND ");
                    sb.AppendFormat(" OwnerEmployeeID= " + HouseMIS.EntityUtils.Employee.Current.EmployeeID.ToString());
                    break;

                case "10":
                    if (Request.QueryString["HouseID"] != null)
                    {
                        string HouseID = Request.QueryString["HouseID"];
                        if (sb.Length > 0)
                            sb.Append(" AND ");
                        sb.AppendFormat("HouseID IN (select HouseID from h_HouseTelList where DelType=0 and Tel2 in (select Tel2 from h_HouseTelList where HouseID={0} and DelType=0))", HouseID);
                    }
                    break;
            }
            //string op = Request.QueryString["op"];
            //if (op != null)
            //{
            //    switch (op)
            //    {
            //        case "1":
            //            if (sb.Length > 0) { sb.Append("AND "); }
            //            sb.AppendFormat(" OrgID = " + HouseMIS.EntityUtils.Employee.Current.OrgID.ToString());
            //            break;
            //    }
            //}
            if (!GetMySearchControlValue("HouseDicName").IsNullOrWhiteSpace())
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat(" HouseDicName like '%{0}%'", HouseMIS.EntityUtils.StringHelper.Filter(GetMySearchControlValue("HouseDicName")));
            }
            if (!GetMySearchControlValue("note").IsNullOrWhiteSpace())
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat(" note like '%{0}%'", HouseMIS.EntityUtils.StringHelper.Filter(GetMySearchControlValue("note")));
            }

            //楼盘/地址note
            if (!GetMySearchControlValue("EstateOrAddress").IsNullOrWhiteSpace())
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat(" (HouseDicName like '%{0}%' OR HouseDicAddress like '%{0}%' OR shi_addr like '%{0}%') ", HouseMIS.EntityUtils.StringHelper.Filter(GetMySearchControlValue("EstateOrAddress")));
            }
            if (!GetMySearchControlValue("HouseDicID").IsNullOrWhiteSpace())
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                string[] s = HouseMIS.EntityUtils.StringHelper.Filter(GetMySearchControlValue("HosueDicID")).Split('|');
                sb.AppendFormat(" HouseDicID={0} ", s[0]);
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat(" SanjakID={0} ", s[1]);
            }
            if (!GetMySearchControlValue("area1").IsNullOrWhiteSpace())
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat(" Build_area>={0}", HouseMIS.EntityUtils.StringHelper.Filter(GetMySearchControlValue("area1")));
            }
            if (!GetMySearchControlValue("area2").IsNullOrWhiteSpace())
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat(" Build_area<={0}", HouseMIS.EntityUtils.StringHelper.Filter(GetMySearchControlValue("area2")));
            }
            if (!GetMySearchControlValue("Price1").IsNullOrWhiteSpace())
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat(" sum_price>={0} ", HouseMIS.EntityUtils.StringHelper.Filter(GetMySearchControlValue("Price1")));
            }
            if (!GetMySearchControlValue("Price2").IsNullOrWhiteSpace())
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat(" sum_price<={0} ", HouseMIS.EntityUtils.StringHelper.Filter(GetMySearchControlValue("Price2")));
            }
            if (!GetMySearchControlValue("exe_date1").IsNullOrWhiteSpace())
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat(" exe_date>='{0}' ", HouseMIS.EntityUtils.StringHelper.Filter(GetMySearchControlValue("exe_date1")));
            }
            if (!GetMySearchControlValue("exe_date2").IsNullOrWhiteSpace())
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat(" exe_date<='{0} 23:59:59' ", HouseMIS.EntityUtils.StringHelper.Filter(GetMySearchControlValue("exe_date2")));
            }
            if (!GetMySearchControlValue("update_date1").IsNullOrWhiteSpace())
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat(" update_date>='{0}' ", HouseMIS.EntityUtils.StringHelper.Filter(GetMySearchControlValue("update_date1")));
            }
            if (!GetMySearchControlValue("update_date2").IsNullOrWhiteSpace())
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat(" update_date<='{0} 23:59:59' ", HouseMIS.EntityUtils.StringHelper.Filter(GetMySearchControlValue("update_date2")));
            }
            if (!GetMySearchControlValue("build_floor1").IsNullOrWhiteSpace())
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat(" build_floor>='{0}' ", HouseMIS.EntityUtils.StringHelper.Filter(GetMySearchControlValue("build_floor1")));
            }
            if (!GetMySearchControlValue("build_floor2").IsNullOrWhiteSpace())
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat(" build_floor<='{0}' ", HouseMIS.EntityUtils.StringHelper.Filter(GetMySearchControlValue("build_floor2")));
            }
            if (!GetMySearchControlValue("Remarks").IsNullOrWhiteSpace())
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat(" Remarks like '%{0}%' ", HouseMIS.EntityUtils.StringHelper.Filter(GetMySearchControlValue("Remarks")));
            }
            if (!GetMySearchControlValue("build_id").IsNullOrWhiteSpace())
            {
                string build_id = HouseMIS.EntityUtils.StringHelper.Filter(GetMySearchControlValue("build_id"));
                if (build_id.ToInt32() > 9)
                {
                    if (sb.Length > 0)
                        sb.Append(" AND ");
                    sb.AppendFormat(" build_id like '%{0}%'", build_id);
                }
                else
                {
                    if (sb.Length > 0)
                        sb.Append(" AND ");
                    sb.AppendFormat(" (build_id='{0}' or build_id='{0}栋')", build_id);
                }
            }
            if (!GetMySearchControlValue("Build_area1").IsNullOrWhiteSpace())
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat(" Build_area>={0} ", HouseMIS.EntityUtils.StringHelper.Filter(GetMySearchControlValue("Build_area1")));
            }
            if (!GetMySearchControlValue("Build_area2").IsNullOrWhiteSpace())
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat(" Build_area<={0} ", HouseMIS.EntityUtils.StringHelper.Filter(GetMySearchControlValue("Build_area2")));
            }
            if (!GetMySearchControlValue("dprice1").IsNullOrWhiteSpace())
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat(" (sum_price * 10000)/build_area>={0} ", HouseMIS.EntityUtils.StringHelper.Filter(GetMySearchControlValue("dprice1")));
            }
            if (!GetMySearchControlValue("dprice2").IsNullOrWhiteSpace())
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat(" (sum_price * 10000)/build_area<={0} ", HouseMIS.EntityUtils.StringHelper.Filter(GetMySearchControlValue("dprice2")));
            }
            if (!GetMySearchControlValue("sum_price1").IsNullOrWhiteSpace())
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat(" sum_price>={0} ", HouseMIS.EntityUtils.StringHelper.Filter(GetMySearchControlValue("sum_price1")));
            }
            if (!GetMySearchControlValue("sum_kprice2").IsNullOrWhiteSpace())
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat(" sum_price<={0} ", HouseMIS.EntityUtils.StringHelper.Filter(GetMySearchControlValue("sum_price2")));
            }
            if (!GetMySearchControlValue("Rent_Price1").IsNullOrWhiteSpace())
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat(" Rent_Price>={0} ", HouseMIS.EntityUtils.StringHelper.Filter(GetMySearchControlValue("Rent_Price1")));
            }
            if (!GetMySearchControlValue("Rent_Price2").IsNullOrWhiteSpace())
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat(" Rent_Price<={0} ", HouseMIS.EntityUtils.StringHelper.Filter(GetMySearchControlValue("Rent_Price2")));
            }
            if (!GetMySearchControlValue("BackUpCode").IsNullOrWhiteSpace())
            {
                string aa = GetMySearchControlValue("BackUpCode");
                if (aa == "on")
                {
                    if (sb.Length > 0)
                        sb.Append(" AND ");
                    sb.Append(" BackUpCode is not null");
                }
            }
            if (!GetMySearchControlValue("OperatorID").IsNullOrWhiteSpace())
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat(" OperatorID in (select EmployeeID from e_Employee where em_name like '%{0}%') ", HouseMIS.EntityUtils.StringHelper.Filter(GetMySearchControlValue("OperatorID")));
            }
            //if (GetMySearchControlValue("AssessStateErrors") == "on")
            //{
            //    if (sb.Length > 0)
            //        sb.Append(" AND ");
            //    sb.AppendFormat(" StateID<>(select top 1 StateID from h_AssessState where AssessStateID=h_houseinfor.AssessStateID)  ");
            //}
            if (GetMySearchControlValue("HXImg") == "on")
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat(" HouseID in (select HouseID from h_PicList where PicTypeID=1) ");
            }
            if (GetMySearchControlValue("IsLock") == "on")
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat(" IsLock=1 ");
            }
            if (GetMySearchControlValue("HasKey") == "on")
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat(" HasKey=1 ");
            }
            if (GetMySearchControlValue("HasImage") == "on")
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat(" HasImage=1 ");
            }
            if (GetMySearchControlValue("IsPrivate") == "on")
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat(" IsPrivate=1 ");
            }
            if (GetMySearchControlValue("IsBeiAn") == "on")
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat(" IsBeiAn=1 ");
            }
            if (GetMySearchControlValue("EntrustType") == "on")
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat(" EntrustTypeID={0} ", h_EntrustType.Find("Name", "独家委托").EntrustTypeID.ToString());
            }
            if (!GetMySearchControlValue("Shi_id").IsNullOrWhiteSpace() && GetMySearchControlValue("Shi_id").Length > 1)
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                if (Regex.Match(GetMySearchControlValue("Shi_id").Substring(0, 1), "^[A-Za-z]+$").Success)
                {
                    sb.AppendFormat(" Shi_id like '{0}%' ", HouseMIS.EntityUtils.StringHelper.Filter(GetMySearchControlValue("Shi_id")));
                }
                else
                {
                    sb.AppendFormat(" Shi_id like '%{0}' ", HouseMIS.EntityUtils.StringHelper.Filter(GetMySearchControlValue("Shi_id")));
                }
            }
            if (!GetMySearchControlValue("HouseDicAddress").IsNullOrWhiteSpace())
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat(" HouseDicAddress like '%{0}%' ", HouseMIS.EntityUtils.StringHelper.Filter(GetMySearchControlValue("HouseDicAddress")));
            }
            if (!GetMySearchControlValue("AreaID").IsNullOrWhiteSpace())
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat(" SanjakID in (select SanjakID from s_Sanjak where AreaID={0}) ", GetMySearchControlValue("AreaID"));
            }
            if (!GetMySearchControlValue("LinkTel2").IsNullOrWhiteSpace())
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat(" LinkTel2 lkie '%{0}%' ", GetMySearchControlValue("LinkTel2").TelEncrypt2(false));
            }
            if (!GetMySearchControlValue("landlord_tel2").IsNullOrWhiteSpace())
            {
                String tel = GetMySearchControlValue("landlord_tel2").Trim();
                if (sb.Length > 0)
                    sb.Append(" AND ");
                String ids = H_houseinfor.FindHouseIDsByTel(tel);
                if (ids.IsNullOrWhiteSpace())
                {
                    tel += "00";
                    sb.AppendFormat(" HouseID in (select HouseID from h_HouseTelList where Tel2 like '{0}%' and DelType=0)", tel.TelEncrypt().Substring(0, tel.Length - 2));
                }
                else
                    sb.AppendFormat(" HouseID in ({0})", ids);
            }
            if (!GetMySearchControlValue("OrgID").IsNullOrWhiteSpace())
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat(" OwnerEmployeeID in (select EmployeeID from e_Employee where OrgID in (select orgid from s_Organise where charindex(',{0},',IdPath)>0))", GetMySearchControlValue("OrgID"));
            }
            if (!GetMySearchControlValue("landlord_name").IsNullOrWhiteSpace())
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat(" landlord_name like '%{0}%' ", HouseMIS.EntityUtils.StringHelper.Filter(GetMySearchControlValue("landlord_name")));
            }
            if (!GetMySearchControlValue("BackTel").IsNullOrWhiteSpace())
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat(" BackTel like '%{0}%' ", GetMySearchControlValue("BackTel").TelEncrypt2(false));
            }
            //if (!GetMySearchControlValue("SubGroupID").IsNullOrWhiteSpace())
            //{
            //    if (sb.Length > 0)
            //        sb.Append(" AND ");
            //    sb.AppendFormat(" OrgID in (select OrgID from e_SubGroupDetail where SubGroupID={0})", HouseMIS.EntityUtils.StringHelper.Filter(GetMySearchControlValue("SubGroupID")));
            //}

            if (sb.Length > 0)
                sb.Append(" AND ");
            if (!string.IsNullOrEmpty(Request.QueryString["OperType"]) && Request.QueryString["OperType"] == "1")
                sb.AppendFormat(" IsNull(bID,0)<>1 and DelType=0");
            else
            {
                sb.AppendFormat(" DelType=0");
            }
            if (sb.Length > 0)
                sb.Append(" AND ");
            sb.AppendFormat(GetRolePermissionEmployeeIds("查看", "OwnerEmployeeID"));
            return sb.Length == 0 ? null : sb.ToString();
        }
    }
}