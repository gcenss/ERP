using HouseMIS.EntityUtils;
using HouseMIS.EntityUtils.DBUtility;
using HouseMIS.Web.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using TCode;

namespace HouseMIS.Web.House
{
    public partial class HousePicListYY : EntityListBase<h_PicList_YY>
    {
        //public string pHasImage = "1";
        protected string z_bottom = string.Empty;
        public string z_del = string.Empty;
        public string ExportToolBar = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                myffrmwc.Items.Clear();
                myffrmwc.Items.Add(new ListItem("全部", "2"));
                myffrmwc.Items.Add(new ListItem("未完成", "0"));
                myffrmwc.Items.Add(new ListItem("已完成", "1"));

                string temp = GetMySearchControlValue("wc");
                if (!temp.IsNullOrWhiteSpace())
                    myffrmwc.SelectedValue = temp;
                else
                    myffrmwc.SelectedIndex = 1;

                myffrmlq.Items.Clear();
                myffrmlq.Items.Add(new ListItem("全部", ""));
                myffrmlq.Items.Add(new ListItem("未领取", "0"));
                myffrmlq.Items.Add(new ListItem("已领取", "1"));

                myffrmsh.Items.Clear();
                myffrmsh.Items.Add(new ListItem("全部", ""));
                myffrmsh.Items.Add(new ListItem("未审核", "0"));
                myffrmsh.Items.Add(new ListItem("已审核", "1"));

                FullDropListData(typeof(h_EntrustType), this.myffrmEntrustTypeID, "Name", "EntrustTypeID", "");
            }

            if (CheckRolePermission("修改"))
            {
                z_bottom = "<li><a class=\"iconL\" href=\"House/HousePicListYY.aspx?Type=0&doType=lq&NavTabId=" + NavTabId + "&selectPage=" + (gv.PageIndex + 1).ToString() + "&doAjax=true\" rel=\"ids\" target=\"selectedTodo\" title=\"确定要操作吗？\"><span>领取</span></a></li>";
                z_bottom += "<li><a class=\"iconL\" href=\"House/HousePicListYY.aspx?Type=0&doType=qxlq&NavTabId=" + NavTabId + "&selectPage=" + (gv.PageIndex + 1).ToString() + "&doAjax=true\" rel=\"ids\" target=\"selectedTodo\" title=\"确定要操作吗？\"><span>取消领取</span></a></li>";
                z_bottom += "<li><a class=\"iconL\" href=\"House/HousePicListYY.aspx?Type=0&doType=wc&NavTabId=" + NavTabId + "&selectPage=" + (gv.PageIndex + 1).ToString() + "&doAjax=true\" rel=\"ids\" target=\"selectedTodo\" title=\"确定要操作吗？\"><span>完成</span></a></li>";
                z_bottom += "<li><a class=\"delete\" href=\"House/HousePicListYY.aspx?doType=del&NavTabId=" + NavTabId + "&selectPage=" + (gv.PageIndex + 1).ToString() + "&doAjax=true\" rel=\"ids\" target=\"selectedTodo\" title=\"确定要删除吗？\"><span>删除照片</span></a></li>";
                z_bottom += "<li><a class=\"edit\" href=\"House/HouseRemarkAdd.aspx?NavTabId=" + NavTabId + "&doAjax=true&ID={ID}\" rel='Bargain1' width=\"360\" height=\"230\" target=\"dialog\" mask=\"true\"><span>增加备注</span></a></li>";
            }
            if (CheckRolePermission("删除"))
            {
                z_bottom += "<li><a class=\"iconL\" href=\"House/HousePicListYY.aspx?doType=sh&NavTabId=" + NavTabId + "&selectPage=" + (gv.PageIndex + 1).ToString() + "&doAjax=true\" rel=\"ids\" target=\"selectedTodo\"><span>审核</span></a></li>";
            }
            if (CheckRolePermission("驳回"))
            {
                z_bottom += "<li><a class=\"iconL\" href=\"House/HousePicListYY.aspx?doType=bh&NavTabId=" + NavTabId + "&selectPage=" + (gv.PageIndex + 1).ToString() + "&doAjax=true\" rel=\"ids\" target=\"selectedTodo\" title=\"确定要驳回吗？\"><span>驳回</span></a></li>";
            }

            #region 执行操作
            if (Request.QueryString["doType"] != null)
            {
                //领取
                if (Request.QueryString["doType"].ToString() == "lq")
                {
                    if (!string.IsNullOrEmpty(Request["ids"]))
                    {
                        string sql = string.Format("select * from h_PicList_YY where ID in({0})", Request["ids"]);
                        List<h_PicList_YY> list_h_PicList_YY = h_PicList_YY.FindAll(sql);
                        foreach (h_PicList_YY hfa in list_h_PicList_YY)
                        {
                            //摄影师以提示已经领取任务
                            if (hfa.Photographer != null)
                            {
                                Response.Write("<script>alertMsg.error(\"任务已被领取，不可在领！\")</script>");
                                Response.End();
                            }
                            else
                            {
                                hfa.Photographer = int.Parse(Current.EmployeeID.ToString());
                                hfa.PotoOrg = int.Parse(Current.OrgID.ToString());
                                hfa.Update();
                            }
                        }
                    }
                }
                //取消领取
                else if (Request.QueryString["doType"].ToString() == "qxlq")
                {
                    if (!string.IsNullOrEmpty(Request["ids"]))
                    {
                        string sql = string.Format("select * from h_PicList_YY where ID in({0})", Request["ids"]);
                        List<h_PicList_YY> list_h_PicList_YY = h_PicList_YY.FindAll(sql);
                        foreach (h_PicList_YY hfa in list_h_PicList_YY)
                        {
                            hfa.Photographer = null;
                            hfa.PotoOrg = null;
                            hfa.Update();
                        }
                    }
                }
                //完成
                else if (Request.QueryString["doType"].ToString() == "wc")
                {
                    if (!string.IsNullOrEmpty(Request["ids"]))
                    {
                        string sql = string.Format("select * from h_PicList_YY where ID in({0})", Request["ids"]);
                        List<h_PicList_YY> list_h_PicList_YY = h_PicList_YY.FindAll(sql);
                        foreach (h_PicList_YY hfa in list_h_PicList_YY)
                        {
                            //完成时间更新
                            hfa.Finishtime = DateTime.Now;
                            hfa.Update();
                        }
                    }
                }
                else if (Request.QueryString["doType"].ToString() == "del")
                {
                    if (!string.IsNullOrEmpty(Request["ids"]))
                    {
                        string sql = string.Format("select * from h_PicList_YY where ID in({0})", Request["ids"]);
                        List<h_PicList_YY> list_h_PicList_YY = h_PicList_YY.FindAll(sql);
                        h_PicList hp;
                        string HouseID;

                        foreach (h_PicList_YY hfa in list_h_PicList_YY)
                        {
                            HouseID = hfa.HouseID.ToString();
                            //删除全部照片
                            List<h_PicList> list_h_PicList = h_PicList.FindAllByHouseID(HouseID.ToDecimal().Value);

                            foreach (h_PicList item in list_h_PicList)
                            {
                                h_PicListDel list_h_PicListDel = new h_PicListDel();
                                list_h_PicListDel.ComID = item.ComID;
                                list_h_PicListDel.EmployeeID = item.EmployeeID;
                                list_h_PicListDel.Exe_date = item.exe_date;
                                list_h_PicListDel.HouseID = item.HouseID;
                                list_h_PicListDel.PicTypeID = item.PicTypeID;
                                list_h_PicListDel.PicURL = item.PicURL;
                                list_h_PicListDel.OrgID = item.OrgID;
                                list_h_PicListDel.DelEmployeeID = Current.EmployeeID.ToInt32();
                                list_h_PicListDel.DelOrgID = Current.OrgID.ToInt32();
                                list_h_PicListDel.DelDate = DateTime.Now;
                                list_h_PicListDel.Insert();
                            }

                            DbHelperSQL.ExecuteSql("delete from h_PicList where PicTypeID!=9 and HouseID= " + HouseID);

                            H_houseinfor.Update("HasImage=0", "HouseID=" + HouseID);

                            Log log1 = new Log();
                            log1.Action = "删除";
                            log1.Category = "删除房源照片";
                            log1.IP = HttpContext.Current.Request.UserHostAddress;
                            log1.OccurTime = DateTime.Now;
                            log1.UserID = Convert.ToInt32(Employee.Current.EmployeeID);
                            log1.UserName = Employee.Current.Em_name;
                            log1.Remark = "房源ID=" + HouseID;
                            log1.Insert();
                        }

                        StringBuilder sb1 = new StringBuilder();
                        sb1.Append("<script>alertMsg.correct(\"操作成功！\")</script>");
                        Response.Write(sb1.ToString());
                        Response.End();
                    }
                }
                //驳回
                else if (Request.QueryString["doType"].ToString() == "bh")
                {
                    if (!string.IsNullOrEmpty(Request["ids"]))
                    {
                        string sql = string.Format("select * from h_PicList_YY where ID in({0})", Request["ids"]);
                        List<h_PicList_YY> list_h_PicList_YY = h_PicList_YY.FindAll(sql);
                        foreach (h_PicList_YY hfa in list_h_PicList_YY)
                        {
                            hfa.Delete();
                        }
                    }
                }
                //审核
                else if (Request.QueryString["doType"].ToString() == "sh")
                {
                    if (!string.IsNullOrEmpty(Request["ids"]))
                    {
                        string sql = string.Format("select * from h_PicList_YY where ID in({0})", Request["ids"]);
                        List<h_PicList_YY> list_h_PicList_YY = h_PicList_YY.FindAll(sql);
                        foreach (h_PicList_YY hfa in list_h_PicList_YY)
                        {
                            if (hfa.IsCheck == 1)
                            {
                                hfa.IsCheck = 0;
                            }
                            else
                            {
                                hfa.IsCheck = 1;
                            }
                            hfa.Update();
                        }
                    }
                }

                #region 刷新当前页[抓取当页导航数字onclick事件]

                string JavaScript = " dwzPageBreak({ targetType: \"navTab\", rel: \"\", data: { pageNum: " + Request["selectPage"] + "} });";
                JSDo_UserCallBack_Success(JavaScript, "操作成功");

                #endregion 刷新当前页[抓取当页导航数字onclick事件]
            }
            #endregion
        }

        protected override string GetWhereClauseFromSearchBar(string typeName)
        {
            string temp1 = string.Empty, temp2 = string.Empty;
            StringBuilder sb = new StringBuilder();
            StringBuilder sbExcel = new StringBuilder();
            //sb.Append(base.GetWhereClauseFromSearchBar(typeName));
            sb.Append("1=1");

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
                    strSearch += "HouseID in (select HouseID from h_houseinfor where SanjakID in (select SanjakID from s_Sanjak where AreaID in(" + strQ + ")))";
                }
                if (strS.Length > 0)
                {
                    strS = strS.Substring(1);
                    if (strSearch.Length > 1)
                        strSearch += " or HouseID in(select HouseID from h_houseinfor where SanjakID  in(" + strS + ")) ";
                    else
                        strSearch += " HouseID in (select HouseID from h_houseinfor where SanjakID  in(" + strS + ")) ";
                }
                if (strX.Length > 0)
                {
                    strX = strX.Substring(1);
                    if (strSearch.Length > 1)
                        strSearch += " or  HouseID in(select HouseID from h_houseinfor where HouseDicID  in(" + strX + ")) ";
                    else
                        strSearch += " HouseID in (select HouseID from h_houseinfor where HouseDicID  in(" + strX + ")) ";
                }
                strSearch += ")";

                sb.AppendFormat(" and " + strSearch);
            }

            #endregion 区域商圈小区搜索

            //房源编号
            temp1 = GetMySearchControlValue("HouseID");
            if (!temp1.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" and HouseID in (select houseid from h_houseinfor where shi_id like '%{0}%')", temp1);
                sbExcel.AppendFormat(" and py.HouseID in (select houseid from h_houseinfor where shi_id like '%{0}%')", temp1);
            }
            //预约人
            temp1 = GetMySearchControlValue("employee");
            if (!temp1.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" and EmployeeID in (select EmployeeID from e_Employee where em_name like '%{0}%')", temp1);
                sbExcel.AppendFormat(" and py.EmployeeID in (select EmployeeID from e_Employee where em_name like '%{0}%')", temp1);
            }
            //摄影师
            temp1 = GetMySearchControlValue("photoer");
            if (!temp1.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" and photographer in (select EmployeeID from e_Employee where em_name like '%{0}%')", temp1);
                sbExcel.AppendFormat(" and py.photographer in (select EmployeeID from e_Employee where em_name like '%{0}%')", temp1);
            }
            //完成起始时间
            temp1 = GetMySearchControlValue("Finishtimestart");
            if (!temp1.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" and convert(char(10),Finishtime,120) >='{0}'", temp1);
                sbExcel.AppendFormat(" and convert(char(10),Finishtime,120) >='{0}'", temp1);
            }
            //temp1 = GetMySearchControlValue("HasImage");
            //if (temp1 == "on")
            //{
            //    pHasImage = "0";
            //    sb.AppendFormat(" and HouseID in (select houseid from h_houseinfor where HasImage !=1)");
            //}
            //完成时间
            temp1 = GetMySearchControlValue("Finishtimeend");
            if (!temp1.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" and convert(char(10),Finishtime,120) <='{0}'", temp1);
                sbExcel.AppendFormat(" and convert(char(10),Finishtime,120) <='{0}'", temp1);
            }

            //是否完成
            temp1 = GetMySearchControlValue("wc");
            if (temp1 == "0")
            {
                sb.AppendFormat(" and Finishtime is null", temp1);
                sbExcel.AppendFormat(" and Finishtime is null", temp1);
            }
            else if (temp1 == "1")
            {
                sb.AppendFormat(" and Finishtime is not null", temp1);
                sbExcel.AppendFormat(" and Finishtime is not null", temp1);
            }
            else if (temp1 == "2")
            {
            }
            else
            {
                sb.AppendFormat(" and Finishtime is null", temp1);
                sbExcel.AppendFormat("  and Finishtime is null", temp1);
            }
            //委托类别
            temp1 = GetMySearchControlValue("EntrustTypeID");
            if (!temp1.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" and HouseID in (select houseid from h_houseinfor where EntrustTypeID ='{0}')", temp1);
                sbExcel.AppendFormat(" and HouseID in (select houseid from h_houseinfor where EntrustTypeID ='{0}')", temp1);
            }

            //是否领取
            temp1 = GetMySearchControlValue("lq");
            if (temp1 == "0")
            {
                sb.AppendFormat(" and photographer is null", temp1);
                sbExcel.AppendFormat(" and photographer is null", temp1);
            }
            else if (temp1 == "1")
            {
                sb.AppendFormat(" and photographer is not null", temp1);
                sbExcel.AppendFormat(" and photographer is not null", temp1);
            }
            //是否审核
            temp1 = GetMySearchControlValue("sh");
            if (temp1 == "0")
            {
                sb.AppendFormat(" and (IsCheck is null or IsCheck=0) ", temp1);
                sbExcel.AppendFormat(" and (IsCheck is null or IsCheck=0)", temp1);
            }
            else if (temp1 == "1")
            {
                sb.AppendFormat(" and IsCheck=1", temp1);
                sbExcel.AppendFormat(" and IsCheck=1", temp1);
            }
            //是否已经成交
            temp1 = GetMySearchControlValue("cj");
            if (temp1 == "0")
            {
                sb.AppendFormat(" and HouseID in (select houseid from b_bargain WHERE  DATEDIFF(DAY,exe_date,GETDATE())<30)");
                sbExcel.AppendFormat(" and HouseID  in (select houseid from b_bargain WHERE  DATEDIFF(DAY,exe_date,GETDATE())<30)", temp1);
            }
            else if (temp1 == "1")
            {
                sb.AppendFormat(" and HouseID not in (select houseid from b_bargain DATEDIFF(DAY,exe_date,GETDATE())<30)");
                sbExcel.AppendFormat(" and HouseID not in (select houseid from b_bargain DATEDIFF(DAY,exe_date,GETDATE())<30)", temp1);
            }

            //是否照片
            temp1 = GetMySearchControlValue("HasImage");
            if (temp1 == "0")
            {
                sb.AppendFormat(" and HouseID in (select houseid from h_houseinfor where HasImage =1)");
                sbExcel.AppendFormat(" and HouseID in (select houseid from h_houseinfor where HasImage =1)", temp1);
            }
            else if (temp1 == "1")
            {
                sb.AppendFormat(" and HouseID in (select houseid from h_houseinfor where HasImage !=1)");
                sbExcel.AppendFormat(" and HouseID in (select houseid from h_houseinfor where HasImage !=1)", temp1);
            }
            //是否通过信息部审核
            temp1 = GetMySearchControlValue("xinxibush");
            if (temp1 == "0")
            {
                sb.AppendFormat(" and HouseID in (select houseid from h_houseinfor where IsSh =1)");
                sbExcel.AppendFormat(" and HouseID in (select houseid from h_houseinfor where IsSh =1)", temp1);
            }
            else if (temp1 == "1")
            {
                sb.AppendFormat(" and HouseID in (select houseid from h_houseinfor where IsSh is null)");
                sbExcel.AppendFormat(" and HouseID in (select houseid from h_houseinfor where IsSh is null)", temp1);
            }
            //栋号搜索
            temp1 = GetMySearchControlValue("build_id");
            if (!temp1.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" and HouseID in (select houseid from h_houseinfor where build_id ='{0}')", temp1);
                sbExcel.AppendFormat(" and py.HouseID in (select houseid from h_houseinfor where build_id ='{0}')", temp1);
            }
            //户号搜索
            temp1 = GetMySearchControlValue("build_room");
            if (!temp1.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" and HouseID in (select houseid from h_houseinfor where build_room ='{0}')", temp1);
                sbExcel.AppendFormat(" and py.HouseID in (select houseid from h_houseinfor where build_room ='{0}')", temp1);
            }

            if (CheckRolePermission("导出"))
            {
                ExportToolBar = "<li><a  runat=\"server\" class=\"icon\" href=\"{0}\" target=\"_blank\"><span>导出</span></a></li> ";
                ExportToolBar = string.Format(ExportToolBar, "Report/ReportExcel.ashx?tableName=photoyy&Term=" + EncodUrlParameter.EncryptPara(sbExcel.Length <= 0 ? "" : sbExcel.ToString()));
            }
            return sb.ToString();
        }
    }
}