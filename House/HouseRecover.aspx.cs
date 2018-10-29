using HouseMIS.Common;
using HouseMIS.EntityUtils;
using HouseMIS.Web.UI;
using System;
using System.Text;
using System.Web.UI.WebControls;
using TCode;

namespace HouseMIS.Web.House
{
    public partial class HouseRecover : EntityListBase<H_houseinfor>
    {
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

        protected string GetEmployeeName(object EmployeeID)
        {
            if (EmployeeID != null)
            {
                Employee emp = Employee.FindByEmployeeID((int)EmployeeID);
                if (emp != null)
                    return emp.Em_name;
                else
                    return string.Empty;
            }
            else
                return string.Empty;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            //hsc = s_SysParam.FindByParamCode("HouseStateColor");
            //hspa = s_SysParam.FindByParamCode("TodayHouseColor");
        }

        //private s_SysParam hsc;
        //private s_SysParam hspa;

        //protected String GetColStyle(object SeeHouseType)
        //{
        //    //s_SysParam hsc = s_SysParam.FindByParamCode("HouseStateColor");
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

        protected String GetState(object SeeHouseType)
        {
            //s_SysParam hsc = s_SysParam.FindByParamCode("HouseStateColor");
            if (Request.QueryString["OperType"] != null && Request.QueryString["OperType"].ToString() == "1")
            {
                return null;
            }
            return SeeHouseType.ToString();
        }

        protected String GetAtype(object AtypeName)
        {
            //s_SysParam hsc = s_SysParam.FindByParamCode("HouseStateColor");
            if (AtypeName != null && AtypeName.ToString() == "租房")
            {
                return "0080ff";
            }
            return null;
        }

        #region

        ///// <summary>
        ///// 取得房源编号颜色[改用GetShi_idColor(Object exe_date)方法]
        ///// </summary>
        ///// <param name="shi_id"></param>
        ///// <returns></returns>
        //protected String GetColNum(object shi_id)
        //{
        //    //s_SysParam hsc = s_SysParam.FindByParamCode("HouseStateColor");
        //    if (hspa != null)
        //    {
        //        //这里使用 shi_id 重复取 房源的 exe_date 不可取；因为列表中本身就有exe_date
        //        //直接传过来就是了[李江涛]
        //        if (H_houseinfor.Find("shi_id", shi_id).Exe_date.ToString().Substring(0, 10) == DateTime.Now.ToString().Substring(0, 10))
        //        {
        //            return " style=\"color:#" + hspa.Value + "\" ";
        //        }
        //    }
        //    return null;
        //}

        #endregion

        /// <summary>
        /// 取得房源编号颜色
        /// </summary>
        /// <param name="exe_date"></param>
        /// <returns></returns>
        //protected String GetShi_idColor(Object exe_date)
        //{
        //    if (exe_date == null) return null;
        //    if (hspa != null && exe_date.ToString().Substring(0, 10) == DateTime.Now.ToString().Substring(0, 10))
        //        return " style=\"color:#" + hspa.Value + "\" ";
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string act = string.Empty;
                if (CheckRolePermission("还原"))
                {
                    act += "<li><a class=\"add\" href=\"House/HouseRecover.aspx?NavTabId=" + NavTabId + "&doAjax=true&doType=del&ID={ID}\" rel=\"ids\" target=\"selectedTodo\" title=\"确定要还原房源吗?\"><span>房源还原</span></a></li>";
                }
                this.LiteralID.Text = act;
                if (Request.QueryString["doType"] != null)
                {
                    if (Request.QueryString["doType"] == "del")
                    {
                        if (CheckRolePermission("还原"))
                        {
                            if (!String.IsNullOrEmpty(Request["ids"]))
                                foreach (string s in Request["ids"].Split(','))
                                {
                                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                    H_houseinfor h = H_houseinfor.FindByHouseID(Convert.ToDecimal(s));
                                    if (h != null && CheckRolePermission("还原", h.OwnerEmployeeID.ToDecimal()))
                                    {
                                        h.DelType = false;
                                        h.Update();
                                    }
                                }
                            JSDo_UserCallBack_Success(" formFind();$(\".HouseRecover:eq(0)\").submit();", "操作成功");
                        }
                        else
                        {
                            JSDo_UserCallBack_Success(" formFind();$(\".HouseRecover:eq(0)\").submit();", "操作失败,您没有还原房源的权限！");
                        }
                    }
                }
                FullDropListData(typeof(h_State), this.ffrmStateID, "Name", "StateID", null, null);
                FullAreaDropListData(mysfrmAreaID, "-请选择-", "");
                FullDropListData(typeof(h_Year), this.ffrmYearID, "Name", "YearID", "");
                FullDropListData(typeof(h_Fitment), this.myffrmFitmentID, "Name", "FitmentID", "");
                FullDropListData(typeof(h_Type), this.ffrmTypeCode, "Name", "TypeCode", "");
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
                            TCode.DataAccessLayer.SelectBuilder sbs = new TCode.DataAccessLayer.SelectBuilder();
                            sbs.Table = "h_Recommend";
                            sbs.Where = "CONVERT(varchar,exe_date,112)=CONVERT(varchar,getdate(),112) and EmployeeID=" + Employee.Current.EmployeeID.ToString();
                            int hrs = h_Recommend.Meta.QueryCount(sbs);
                            if (hrs < Convert.ToInt32(ss.Value))
                            {
                                H_houseinfor.Meta.Query("update h_houseInfor set update_date=getdate() where HouseID = " + HouseID);
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
                            //case "6": //查看钥匙
                            //    DataTable dt = h_HouseKey.Meta.Query("SELECT TOP 1 H.IsIn,S.Name,H.InOhterCompany FROM h_HouseKey H LEFT JOIN s_Organise S ON S.OrgID = H.OrgID WHERE H.HouseID = " + HouseID + " ORDER BY H.exe_date DESC").Tables[0];

                            //    if (dt.Rows.Count > 0)
                            //    {
                            //        if (Convert.ToBoolean(dt.Rows[0][0]) == true)
                            //        {
                            //            if (dt.Rows[0]["InOhterCompany"].ToString() == "True")
                            //                //Response.Write("<script>alertMsg.correct('钥匙在其他中介')</script>");
                            //                JSDo_UserCallBack_Success("", "钥匙在其他中介");
                            //            else
                            //                //Response.Write("<script>alertMsg.correct('钥匙在" + dt.Rows[0][1] + "')</script>");
                            //                JSDo_UserCallBack_Success("", "钥匙在" + dt.Rows[0][1]);
                            //        }
                            //        else
                            //            //Response.Write("<script>alertMsg.correct('该房源没有拿钥匙！')</script>");
                            //            JSDo_UserCallBack_Error("", "该房源没有拿钥匙");
                            //    }
                            //    else
                            //    {
                            //        //Response.Write("<script>alertMsg.correct('该房源没有拿钥匙！')</script>");
                            //        JSDo_UserCallBack_Error("", "该房源没有拿钥匙");
                            //    }

                            //    //Response.End();
                            //    break;
                    }
                    sb.Append("{\r\n");
                    sb.Append("   \"statusCode\":\"200\", \r\n");
                    sb.Append("   \"message\":\"操作成功！\" \r\n");
                    sb.Append("}\r\n");
                    Response.Write(sb.ToString());
                    Response.End();
                }
                if (Request.QueryString["doType"] != null)
                {
                    if (Request.QueryString["doType"] == "del")
                    {
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        H_houseinfor.Meta.Query("delete from h_houseinfor where HouseID=" + Request["HouseID"]);
                        sb.Append("{\r\n");
                        sb.Append("   \"statusCode\":\"200\", \r\n");
                        sb.Append("   \"message\":\"操作成功！\" \r\n");
                        sb.Append("}\r\n");
                        Response.Write(sb.ToString());
                        Response.End();
                    }
                }
            }
        }

        ///// <summary>
        ///// 推荐房源
        ///// </summary>
        ///// <param name="HouseID"></param>
        //[AjaxPro.AjaxMethod]
        //public void tuijian(string HouseID)
        //{
        //     H_houseinfor.Meta.Query("update h_houseInfor set update_date=getdate() where HouseID = " + HouseID);
        //}
        public StringBuilder sb = new StringBuilder();

        /// <summary>
        /// 重写查找条件
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        protected override string GetWhereClauseFromSearchBar(string typeName)
        {
            sb.Append(base.GetWhereClauseFromSearchBar(typeName));
            if (Request.QueryString["CustomerID"] != null)
            {
                c_Customer myCC = c_Customer.FindByCustomerID(Convert.ToDecimal(Request.QueryString["CustomerID"]));
                //小区地址暂时没加
                if (myCC.NeedBuildAreaID > 0)
                {
                    c_NeedBuildArea Cn = c_NeedBuildArea.FindByNeedBuildAreaID(myCC.NeedBuildAreaID);
                    if (sb.Length > 0)
                        sb.Append(" AND ");
                    sb.AppendFormat(" build_area>={0} ", Cn.bValue);
                    sb.Append(" AND ");
                    sb.AppendFormat(" build_area<={0} ", Cn.eValue);
                }
                if (myCC.NeedHouseTypeID > 0)
                {
                    c_NeedHouseType Cn = c_NeedHouseType.FindByNeedHouseTypeID(myCC.NeedHouseTypeID);
                    if (sb.Length > 0)
                        sb.Append(" AND ");
                    sb.AppendFormat(" form_bedroom>={0} ", Cn.bValue);
                    sb.Append(" AND ");
                    sb.AppendFormat(" form_bedroom<={0} ", Cn.eValue);
                }
                if (myCC.NeedFloorID > 0)
                {
                    c_NeedFloor Cn = c_NeedFloor.FindByNeedFloorID(myCC.NeedFloorID);
                    if (sb.Length > 0)
                        sb.Append(" AND ");
                    sb.AppendFormat(" build_floor>={0} ", Cn.bValue);
                    sb.Append(" AND ");
                    sb.AppendFormat(" build_floor<={0} ", Cn.eValue);
                }
                if (myCC.NeedPriceID > 0)
                {
                    c_NeedPrice Cn = c_NeedPrice.FindByNeedPriceID(myCC.NeedPriceID);
                    if (sb.Length > 0)
                        sb.Append(" AND ");
                    sb.AppendFormat(" sum_price>={0} ", Cn.bValue);
                    sb.Append(" AND ");
                    sb.AppendFormat(" sum_price<={0} ", Cn.eValue);
                }
                if (myCC.NeedFitmentID > 0)
                {
                    if (sb.Length > 0)
                        sb.Append(" AND ");
                    sb.AppendFormat(" FitmentID={0} ", myCC.NeedFitmentID);
                }
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.Append(" StateID=2 ");
            }
            string parms = Request.QueryString["OperType"];
            switch (parms)
            {
                case "0":
                    if (sb.Length > 0)
                        sb.Append(" AND ");
                    sb.Append(" aType = " + parms);
                    break;

                case "1":
                    if (sb.Length > 0)
                        sb.Append(" AND ");
                    sb.Append(" aType = " + parms);
                    break;

                case "2":
                    if (sb.Length > 0)
                        sb.Append(" AND ");
                    sb.Append(" aType=1 and  OrgID in " + GetEmployeeOrgTableIds(Convert.ToInt32(HouseMIS.EntityUtils.Employee.Current.EmployeeID), RangeType.本店));
                    break;

                case "3":
                    if (sb.Length > 0)
                        sb.Append(" AND ");
                    sb.Append(" aType=1 and  OwnerEmployeeID= " + HouseMIS.EntityUtils.Employee.Current.EmployeeID.ToString());
                    break;

                case "4":
                    if (sb.Length > 0)
                        sb.Append(" AND ");
                    sb.Append(" aType=0 and  OwnerEmployeeID= " + HouseMIS.EntityUtils.Employee.Current.EmployeeID.ToString());
                    break;

                case "5":
                    if (sb.Length > 0)
                        sb.Append(" AND ");
                    sb.Append(" HouseID in(select HouseID from h_HouseCollect where EmployeeID=" + HouseMIS.EntityUtils.Employee.Current.EmployeeID.ToString() + ")");
                    break;

                case "6":
                    if (Request.QueryString["HouseID"] != null)
                    {
                        string HouseID = Request.QueryString["HouseID"];
                        if (sb.Length > 0)
                            sb.Append(" AND ");
                        sb.AppendFormat("HouseID in (select A.HouseID from h_houseinfor A left join h_houseinfor B on A.HouseDicName=B.HouseDicName and A.build_id=B.build_id and A.build_unit=B.build_unit and A.build_room=B.build_room where B.HouseID={0})", HouseID);
                    }
                    break;

                case "7":
                    if (Request.QueryString["HouseID"] != null)
                    {
                        string HouseID = Request.QueryString["HouseID"];
                        if (sb.Length > 0)
                            sb.Append(" AND ");
                        sb.AppendFormat("HouseID in (select A.HouseID from h_houseinfor A left join h_houseinfor B on A.HouseDicAddress=B.HouseDicAddress and A.form_bedroom=B.form_bedroom and A.Build_area<B.Build_area+5 and A.Build_area>B.Build_area-5  where B.HouseID={0})", HouseID);
                    }
                    break;

                case "8": //今日房源 且有效的房源
                    if (sb.Length > 0)
                        sb.Append(" AND ");
                    sb.Append("exe_date>'" + DateTime.Now.ToString("yyyy-MM-dd") + "' AND CONVERT(varchar(12),exe_date,103)=CONVERT(varchar(12),getdate(),103) AND StateID=2");
                    break;

                case "9":
                    if (sb.Length > 0)
                        sb.Append(" AND ");
                    sb.Append(" aType=0 and  OrgID in " + GetEmployeeOrgTableIds(Convert.ToInt32(HouseMIS.EntityUtils.Employee.Current.EmployeeID), RangeType.本店));
                    break;

                default: break;
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
                    sb.AppendFormat(" build_id='{0}'", build_id);
                }
            }
            if (!GetMySearchControlValue("note").IsNullOrWhiteSpace())
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat(" note like '%{0}%'", HouseMIS.EntityUtils.StringHelper.Filter(GetMySearchControlValue("note")));
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
            //if (!GetMySearchControlValue("shi_addr1").IsNullOrWhiteSpace() && !GetMySearchControlValue("shi_addr2").IsNullOrWhiteSpace())
            //{
            //    if (sb.Length > 0)
            //        sb.Append(" AND ");
            //    sb.AppendFormat(" HouseID in (select HouseID from h_houseinfor h left join s_HouseDicFloorPrice s on h.build_floor=s.Floor and h.HouseDicID=s.HouseDicID where Price>={0} and Price<={1}) ", HouseMIS.EntityUtils.StringHelper.Filter(GetMySearchControlValue("shi_addr1")), HouseMIS.EntityUtils.StringHelper.Filter(GetMySearchControlValue("shi_addr2")));
            //}
            //else if (!GetMySearchControlValue("shi_addr1").IsNullOrWhiteSpace())
            //{
            //    if (sb.Length > 0)
            //        sb.Append(" AND ");
            //    sb.AppendFormat(" HouseID in (select HouseID from h_houseinfor h left join s_HouseDicFloorPrice s on h.build_floor=s.Floor and h.HouseDicID=s.HouseDicID where Price>={0}) ", HouseMIS.EntityUtils.StringHelper.Filter(GetMySearchControlValue("shi_addr1")));
            //}
            //else if (!GetMySearchControlValue("shi_addr2").IsNullOrWhiteSpace())
            //{
            //    if (sb.Length > 0)
            //        sb.Append(" AND ");
            //    sb.AppendFormat(" HouseID in (select HouseID from h_houseinfor h left join s_HouseDicFloorPrice s on h.build_floor=s.Floor and h.HouseDicID=s.HouseDicID where Price<={0}) ", HouseMIS.EntityUtils.StringHelper.Filter(GetMySearchControlValue("shi_addr2")));
            //}
            if (!GetMySearchControlValue("Ohter2ID1").IsNullOrWhiteSpace())
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat(" Ohter2ID>={0} ", HouseMIS.EntityUtils.StringHelper.Filter(GetMySearchControlValue("Ohter2ID1")));
            }
            if (!GetMySearchControlValue("Ohter2ID2").IsNullOrWhiteSpace())
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat(" Ohter2ID<={0} ", HouseMIS.EntityUtils.StringHelper.Filter(GetMySearchControlValue("Ohter2ID2")));
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
            if (!GetMySearchControlValue("FollowUp_Date1").IsNullOrWhiteSpace())
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat(" FollowUp_Date>='{0}' ", HouseMIS.EntityUtils.StringHelper.Filter(GetMySearchControlValue("FollowUp_Date1")));
            }
            if (!GetMySearchControlValue("FollowUp_Date2").IsNullOrWhiteSpace())
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat(" FollowUp_Date<='{0} 23:59:59' ", HouseMIS.EntityUtils.StringHelper.Filter(GetMySearchControlValue("FollowUp_Date2")));
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
                sb.AppendFormat(" build_floor>='{0}' ", HouseMIS.EntityUtils.StringHelper.Filter(GetMySearchControlValue("build_floor1").ToInt32().ToString()));
            }
            if (!GetMySearchControlValue("build_floor2").IsNullOrWhiteSpace())
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat(" build_floor<='{0}' ", HouseMIS.EntityUtils.StringHelper.Filter(GetMySearchControlValue("build_floor2").ToInt32().ToString()));
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
            if (!GetMySearchControlValue("sum_price2").IsNullOrWhiteSpace())
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
            if (!GetMySearchControlValue("FitmentID").IsNullOrWhiteSpace())
            {
                if (h_Fitment.Find("FitmentID", GetMySearchControlValue("FitmentID")).Name == "装修")
                {
                    if (sb.Length > 0)
                        sb.Append(" AND ");
                    sb.AppendFormat(" FitmentID is not null and FitmentID<>{0} ", h_Fitment.Find("Name", "毛坯").FitmentID);
                }
                else
                {
                    if (sb.Length > 0)
                        sb.Append(" AND ");
                    sb.AppendFormat(" FitmentID={0} ", HouseMIS.EntityUtils.StringHelper.Filter(GetMySearchControlValue("FitmentID")));
                }
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
            if (!GetMySearchControlValue("OwenEmployeeID").IsNullOrWhiteSpace())
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat(" OwnerEmployeeID in (select EmployeeID from e_Employee where em_name like '%{0}%') ", HouseMIS.EntityUtils.StringHelper.Filter(GetMySearchControlValue("OwenEmployeeID")));
            }
            //if (GetMySearchControlValue("shi_addr") == "on")
            //{
            //    if (sb.Length > 0)
            //        sb.Append(" AND ");
            //    sb.AppendFormat("  HouseID in (select HouseID from h_houseinfor h left join s_HouseDicFloorPrice s on h.build_floor=s.Floor and h.HouseDicID=s.HouseDicID and h.Build_id=s.Build_id  where Price>0) ");
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
            if (GetMySearchControlValue("HasRecord") == "on")
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat(" (HasRecord=1 or HouseID in(select houseID from i_InternetPhone  where recordUrlDel=1))");
            }
            //if (GetMySearchControlValue("AllView") == "on")
            //{
            //    if (sb.Length > 0)
            //        sb.Append(" AND ");
            //    sb.AppendFormat(" HouseID  in (select HouseID from h_AllViewPic)  ");
            //}
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
            if (GetMySearchControlValue("IsVideo") == "on")
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat(" IsVideo=1 ");
            }
            if (GetMySearchControlValue("IsBring") == "on")
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat(" shi_id in (select HouseList from c_BringCustomer)");
            }
            if (GetMySearchControlValue("IsPrivate") == "on")
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat(" IsPrivate=1 ");
            }
            if (GetMySearchControlValue("EntrustType") == "on")
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat(" EntrustTypeID={0} ", h_EntrustType.Find("Name", "独家委托").EntrustTypeID.ToString());
            }
            if (!GetMySearchControlValue("Shi_id").IsNullOrWhiteSpace())
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat(" Shi_id like '%{0}%' ", HouseMIS.EntityUtils.StringHelper.Filter(GetMySearchControlValue("Shi_id")));
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
                sb.AppendFormat(" LinkTel2 like '%{0}%' ", GetMySearchControlValue("LinkTel2").TelEncrypt2(false));
            }
            //if (!GetMySearchControlValue("landlord_tel2").IsNullOrWhiteSpace())
            //{
            //    if (sb.Length > 0)
            //        sb.Append(" AND ");
            //    sb.AppendFormat(" landlord_tel2 like '%{0}%' ", GetMySearchControlValue("landlord_tel2").TelEncrypt2(false));
            //}
            //楼盘/地址
            if (!GetMySearchControlValue("EstateOrAddress").IsNullOrWhiteSpace())
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat(" (HouseDicName like '%{0}%' OR HouseDicAddress like '%{0}%') ", HouseMIS.EntityUtils.StringHelper.Filter(GetMySearchControlValue("EstateOrAddress")));
            }
            else if (Request["HouseDicID"].ToInt32() > 0)
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat("(HouseDicID={0}) ", Request["HouseDicID"].ToInt32());
                if (this.pagerForm.Action.IndexOf("HouseDicID") == -1)
                    this.pagerForm.Action += "&HouseDicID=" + Request["HouseDicID"];
            }

            //if (!GetMySearchControlValue("landlord_tel2").IsNullOrWhiteSpace())
            //{
            //    if (sb.Length > 0)
            //        sb.Append(" AND ");
            //    sb.AppendFormat(" (landlord_tel2 like '%{0}%' OR LinkTel2 like '%{0}%') ", GetMySearchControlValue("landlord_tel2").TelEncrypt2(false));
            //}
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

            //if (sb.Length > 0)
            //    sb.Append(" AND ");
            //sb.Append(GetRolePermissionEmployeeIds("查看", "OwnerEmployeeID"));

            if (sb.Length > 0)
                sb.Append(" AND ");
            sb.Append(" (IsLock=0 or " + GetRolePermissionOrgIds("查看锁盘", "OrgID") + ") and IsNull(bID,0)<>1 and DelType=1");
            return sb.Length == 0 ? null : sb.ToString();
        }
    }
}