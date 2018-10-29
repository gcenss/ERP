using HouseMIS.Common;
using HouseMIS.EntityUtils;
using HouseMIS.Web.UI;
using System;
using System.Text;
using System.Web.UI.WebControls;

namespace HouseMIS.Web.House
{
    public partial class HouseTelFormSX : EntityListBase<h_HouseTimeLimitMsg>
    {
        #region 参数

        public string OldTel;
        public string Tel;
        /// <summary>
        /// 关联房源
        /// </summary>
        public H_houseinfor House;

        /// <summary>
        /// 是否显示电话
        /// </summary>
        public Boolean IsShowTel = false;

        public Boolean IsCanShowTel = false;

        #endregion 参数

        protected override void OnPreInit(EventArgs e)
        {
            #region 加载参数

            HouseID.Value = Request["HouseID"];
            House = H_houseinfor.FindByHouseID((Decimal)HouseID.Value.ToInt32());
            if (House == null)
            {
                Response.Write("参数错误！");
                Response.End();
            }
            if (Request.QueryString["GU_ID"] != null && Request.QueryString["GU_ID"].ToString() != "")
            {
                GU_ID.Value = Request.QueryString["GU_ID"].ToString();
            }

            IsShowTel = Request.QueryString["a"] != null;

            IsCanShowTel = CheckRolePermission("查看电话");

            //出售
            //if (House.aType == 0)
            //{
            //    IsCanShowTel = H_houseinfor.CanShowTel(House.HouseID, Employee.Current.EmployeeID.ToString().ToInt32(), 50);
            //}
            ////出租
            //else
            //{
            //    IsCanShowTel = H_houseinfor.CanShowTel(House.HouseID, Employee.Current.EmployeeID.ToString().ToInt32(), 480);
            //}

            #endregion 加载参数

            base.OnPreInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.HTFpagerForm.ID += House.HouseID;

                #region 1.显示 [查看电话] 按钮

                if (!IsShowTel) //如果不显示电话
                {
                    //判断调电权限
                    //if (H_houseinfor.NeedTelPower(Convert.ToDecimal(Request["HouseID"]), Convert.ToInt32(Employee.Current.EmployeeID)))
                    //{
                    //    LokTelButa.Visible = IsCanShowTel;
                    //}
                    //if (LokTelButa.Visible == true)
                    //{
                    //    LokTelButa.Attributes.Add("onclick", "return LookTelClick('" + House.HouseID + "')");
                    //}
                }
                else
                {
                    //LokTelButa.Visible = false;

                    #region 插入调电记录

                    if (CheckRolePermission("不隐号") || IsCanShowTel)
                    {
                        h_SeeTelLog hs = new h_SeeTelLog();
                        hs.EmployeeID = Employee.Current.EmployeeID;
                        hs.HouseID = Convert.ToDecimal(HouseID.Value);
                        hs.IsPower = H_houseinfor.NeedTelPower(House.HouseID, Employee.Current.EmployeeID.ToString().ToInt32());
                        hs.Insert();
                    }

                    #endregion 插入调电记录

                    //显示室号
                    room.Value = House.Build_room;
                }
                cbTel.DataSource = i_InternetTel.FindAll(new string[] { "EmployeeID", "IsDel" }, new string[] { Employee.Current.EmployeeID.ToString(), "0" });
                cbTel.DataTextField = "MyTel";
                cbTel.DataValueField = "MyTel";
                cbTel.DataBind();
                #endregion 1.显示 [查看电话] 按钮

            }

            if (Request.Form["LSH"] != null && Request.Form["LSH"] != "")
            {
                #region 编辑电话

                h_HouseTelList hh = h_HouseTelList.Find("LSH", Request.Form["LSH"]);
                hh.EmployeeID = Employee.Current.EmployeeID;
                if (Request.Form["HouseID"] != "0")
                {
                    hh.HouseID = Convert.ToDecimal(Request.Form["HouseID"]);
                }
                else
                {
                    hh.HouseID = 0;
                    hh.GU_ID = Request.Form["GU_ID"].ToString();
                }
                hh.landlord_name = Request.Form["frmName"].ToString();
                hh.Tel1 = H_houseinfor.TelDispose(Request.Form["Tel"]);
                string oldTel = hh.Tel2;
                hh.Tel2 = Request.Form["Tel"].TelEncrypt();
                hh.TelType = Request.Form["TelType"].ToString();
                hh.Update();

                #region 插入跟进

                h_FollowUp hfo = new h_FollowUp();
                hfo.HouseID = hh.HouseID;
                hfo.EmployeeID = Employee.Current.EmployeeID;
                hfo.FollowUpText = "电话修改";
                hfo.Insert();

                #endregion 插入跟进

                #region 电话修改记录

                TelChange tc = new TelChange();
                tc.AddEmployeeID = Employee.Current.EmployeeID;
                tc.HouseID = hh.HouseID;
                tc.NewTel = Request.Form["Tel"];
                tc.OldTel = oldTel.TelDecrypt((Int32)hh.HouseID, 0);
                tc.Insert();
                LSH.Value = "";

                #endregion 电话修改记录

                #endregion 编辑电话
            }
            else if (Request.Form["frmName"] != null && Request.Form["frmName"] != "")
            {
                #region 添加电话

                h_HouseTelList hh = new h_HouseTelList();
                hh.EmployeeID = Employee.Current.EmployeeID;
                if (Request.Form["HouseID"] != "0")
                {
                    hh.HouseID = Convert.ToDecimal(Request.Form["HouseID"]);
                }
                else
                {
                    hh.HouseID = 0;
                    hh.GU_ID = Request.Form["GU_ID"].ToString();
                }
                hh.landlord_name = Request.Form["frmName"].ToString();
                hh.Tel1 = H_houseinfor.TelDispose(Request.Form["Tel"]);
                hh.Tel2 = Request.Form["Tel"].TelEncrypt();
                hh.TelType = Request.Form["TelType"].ToString();
                hh.Insert();
                if (hh.HouseID > 0)
                {
                    h_FollowUp hfo = new h_FollowUp();
                    hfo.HouseID = hh.HouseID;
                    hfo.EmployeeID = Employee.Current.EmployeeID;
                    hfo.FollowUpText = "添加新电话,编号：" + hh.LSH;
                    hfo.Insert();
                }

                #endregion 添加电话

                Response.Write("<script>alertMsg.correct(\"操作成功请点调电拨打电话！\");</script>");
            }
            if (!CheckRolePermission("不隐号"))
            {
                //if (!CheckRolePermission("添加电话", Convert.ToDecimal(House.OwnerEmployeeID)))
                //hTelAdd.Visible = false;
            }
            else
            {
                if (CheckRolePermission("不隐号"))
                {
                    h_SeeTelLog hs = new h_SeeTelLog();
                    hs.EmployeeID = Employee.Current.EmployeeID;
                    hs.HouseID = Convert.ToDecimal(HouseID.Value);
                    hs.IsPower = false;
                    hs.Insert();
                }
            }
        }

        protected override string GetWhereClauseFromSearchBar(string typeName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.GetWhereClauseFromSearchBar(typeName));
            if (Request.QueryString["HouseID"] != null && Request.QueryString["doType"] != null && Request.QueryString["doType"].ToString() == "LookTel")
            {
                if (sb.Length > 0)
                    sb.Append(" and ");
                sb.AppendFormat(" HouseID={0}", Request.QueryString["HouseID"].ToString());
            }
            return sb.ToString();
        }

        /// <summary>
        /// 处理电话号码，只显示****号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem == null)
                return;

            //电话号码
            Employee ee = Employee.FindByEmployeeID(Convert.ToDecimal(((h_HouseTimeLimitMsg)e.Row.DataItem).ZRempID));
            string tel = string.Empty;
            if (ee != null)
            {
                tel = ee.Tel.ToString();
                e.Row.Cells[0].Text = ee.Em_name;
            }

            //隐藏的电话号码
            string tel_Hidden = "*********";
            if (tel.Length > 8)
            {
                //将中间4位取出替换为*
                string pad = tel.Substring(tel.Length - 8, 4);
                tel_Hidden = tel.Replace(pad, "****");
            }
            if (!CheckRolePermission("不隐号"))
            {
                #region 1.默认都隐号

                if (!IsShowTel || !IsCanShowTel) //电话显示***号
                {
                    e.Row.Cells[1].Text = tel_Hidden;
                }

                #endregion 1.默认都隐号

                #region 2.点“查看电话”，显示号码，

                else
                {
                    e.Row.Cells[1].Text = tel;
                }

                #endregion 2.点“查看电话”，显示号码，
            }
            else
            {
                e.Row.Cells[1].Text = tel;
            }

            //操作
            //if (CheckRolePermission("房东电话修改"))
            //{
            //    e.Row.Cells[4].Text = "<a class=\"btnEdit\" href=\"javascript:EditCallTel('" + ((h_HouseTelList)e.Row.DataItem).LSH + "','" + ((h_HouseTelList)e.Row.DataItem).landlord_name + "','" + ((h_HouseTelList)e.Row.DataItem).TelDe + "','" + ((h_HouseTelList)e.Row.DataItem).TelType + "','" + ((h_HouseTelList)e.Row.DataItem).HouseID + "')\" title=\"编辑\">编辑</a>" +
            //    "<a class=\"btnDel\" href=\"javascript:DelCallTel('" + ((h_HouseTelList)e.Row.DataItem).LSH + "')\" title=\"删除\">删除</a>";
            //}
        }

        /// <summary>
        /// 处理电话号码，只显示****号
        /// </summary>
        /// <param name=""></param>
        protected string findtel(object empID)
        {
            if (empID != null)
            {
                Employee ee = Employee.FindByEmployeeID(Convert.ToDecimal(empID));
                return ee.Tel;
            }
            else
            {
                return "";
            }
        }
    }
}