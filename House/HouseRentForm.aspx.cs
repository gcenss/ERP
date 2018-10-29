using HouseMIS.EntityUtils;
using HouseMIS.EntityUtils.DBUtility;
using HouseMIS.Web.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using TCode;

namespace HouseMIS.Web.House
{
    public partial class HouseRentForm : EntityFormBase<H_houseinfor>
    {
        public decimal EmployeeID = Employee.Current.EmployeeID;
        public string EmID = Employee.Current.Em_id;
        public string ShowPoint, shi_ids;
        public string OAID;

        public override string MenuCode
        {
            get
            {
                return "2001";
            }
        }

        public int ConvertInts(object a)
        {
            if (a.ToInt32() > 0)
            {
                return Convert.ToInt32(a.ToInt32());
            }
            else
            {
                return 0;
            }
        }

        protected string BillCs = "";

        /// <summary>
        /// 得到二维码图片
        /// </summary>
        protected string TwoCodePic()
        {
            string picUrl = "/TwoCode/" + Entity.HouseID + ".png";
            if (System.IO.File.Exists(Server.MapPath(picUrl)))
            {
                return "<img src='" + picUrl + "' />";
            }
            else
            {
                return "暂无二维码图片";
            }
        }

        //艾李明修改
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            if (Request.QueryString["KeyValue"] != null)
            {
                if (Request.QueryString["type"] == "comeFlow") //从跟进列表，查看房源信息
                {
                    B_bargain B = B_bargain.FindByBargainCode(Request.QueryString["KeyValue"].Split(",")[0]);
                    if (B != null)
                        Entity = H_houseinfor.FindByHouseID(Convert.ToDecimal(B.HouseID));
                }
                else
                {
                    if (Request.QueryString["KeyValue"].Split(",").Length > 1)
                        Entity = H_houseinfor.FindByHouseID(decimal.Parse(Request.QueryString["KeyValue"].Split(",")[1]));
                }
            }
        }

        protected override void OnSaveSuccess(object sender, EntityFormEventArgs e)
        {
            //if (Request.Form["GU_IDS"] != "")
            //{
            //    TCode.EntityList<h_HouseTelList> hh = h_HouseTelList.FindAll("GU_ID", Request.Form["GU_IDS"]);
            //    foreach (h_HouseTelList ht in hh)
            //    {
            //        ht.GU_ID = null;
            //        ht.HouseID = Entity.HouseID;
            //        ht.Update();
            //    }
            //}

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("{\r\n");
            sb.Append("   \"statusCode\":\"200\", \r\n");
            sb.Append("   \"message\":\"操作成功！\", \r\n");
            sb.Append("   \"navTabId\":\"" + EntityForm.NavTabId + "\", \r\n");
            sb.Append("   \"rel\":\"" + Entity.FollowID + "\", \r\n");
            if (EntityForm.AjaxAction == "addoredit")
                sb.Append("   \"callbackType\":\"closeCurrent\",\r\n");
            sb.Append("   \"forwardUrl\":\"\"\r\n");
            sb.Append("}\r\n");

            Response.Write(sb.ToString());
            //Response.End();
        }

        public string houseID = "0";

        /// <summary>
        /// 最初加载在Load之前 绑定页面值
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInitComplete(EventArgs e)
        {
            base.OnInitComplete(e);
            for (int i = -2; i < 100; i++)
            {
                frmbuild_floor.Items.Add(new ListItem(i.ToString(), i.ToString()));
                frmbuild_levels.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            PubFunction.FullDropListData(typeof(c_Visit), frmVisitID, "Name", "VisitID", "");
            PubFunction.FullDropListData(typeof(h_PayServant), frmPayServantID, "Name", "PayServantID", "");
            PubFunction.FullDropListData(typeof(h_SeeHouse), frmSeeHouseID, "Name", "SeeHouseID", "");
            PubFunction.FullDropListData(typeof(h_Type), frmTypeCode, "Name", "TypeCode", "");
            PubFunction.FullDropListData(typeof(h_Property), frmPropertyID, "Name", "PropertyID", "");
            PubFunction.FullDropListData(typeof(h_NowState), frmNowStateID, "Name", "NowStateID", "");
            PubFunction.FullDropListData(typeof(h_Fitment), frmFitmentID, "Name", "FitmentID", "");
            PubFunction.FullDropListData(typeof(h_Faceto), frmFacetoID, "Name", "FacetoID", "");
            PubFunction.FullDropListData(typeof(h_Use), frmUseID, "Name", "UseID", "");
            PubFunction.FullDropListData(typeof(h_Year), frmYearID, "Name", "YearID", "");
            PubFunction.FullDropListData(typeof(H_landlord), frmLandlordID, "Name", "LandlordID", "");
            //房源类型
            PubFunction.FullDropListData(typeof(h_RentState), frmRentStateID, "Name", "RentStateID", "");
            //委托类型
            PubFunction.FullDropListData(typeof(h_EntrustType), frmEntrustTypeID, "Name", "EntrustTypeID", "");

            List<h_State> h_State_list = h_State.FindAll("select StateID,Name from h_State where name='委托中' or name='无效委托'");
            frmStateID.DataSource = h_State_list;
            frmStateID.DataTextField = "Name";
            frmStateID.DataValueField = "StateID";
            frmStateID.DataBind();
            //委托类别，判断如果不是信息部或者超级管理员，则不能修改委托类别
            if (Entity.HouseID == 0)
            {

                lblstate.Visible = true;
                frmStateID.Visible = true;
            }
            //PubFunction.FullDropListData(typeof(h_Taxes), frmTaxesID, "Name", "TaxesID", "");
            PubFunction.FullDropListData(typeof(h_Appliance), frmApplianceID, "Name", "ApplianceID", "");
            PubFunction.FullDropListData(typeof(h_Assort), frmAssortID, "Name", "AssortID", "");
            PubFunction.FullDropListData(typeof(h_Cause), frmSaleMotiveID, "Name", "SaleMotiveID", "");
            //PubFunction.FullDropListData(typeof(s_Area), ddlArea, "Name", "AreaID", "");
            ////删除空行
            //ddlArea.Items.RemoveAt(0);
            ////删除江苏省
            //ddlArea.Items.RemoveAt(0);
            ////删除无锡市
            //ddlArea.Items.RemoveAt(0);
            //List<s_Sanjak> list_s_Sanjak = s_Sanjak.FindAll("select * from s_Sanjak where AreaID=" + ddlArea.SelectedValue);
            //ddlSanjakID.DataSource = list_s_Sanjak;
            //ddlSanjakID.DataTextField = "name";
            //ddlSanjakID.DataValueField = "SanjakID";
            //ddlSanjakID.DataBind();

            oa_NewClass on = oa_NewClass.FindWithCache("Name", "房源管理");
            if (on != null)
                OAID = on.NewClassID.ToString();
            else
                OAID = "";
        }

        protected StringBuilder btnSaveRent = new StringBuilder();

        //protected StringBuilder LookTelScript = new StringBuilder();
        protected string hoby = "";

        private StringBuilder customerBringpin = new StringBuilder();
        protected string gd_num = Employee.Current.GD_Num;
        protected string emid = Employee.Current.EmployeeID.ToString();

        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(HouseRentForm), Page);

            if (!IsPostBack)
            {
                frmRoomID.Style.Add("width", "130px");
                frmbID.Style.Add("width", "118px");
                frmbuild_id.Style.Add("width", "113px");
                frmbuild_room.Style.Add("width", "123px");

                if (Request.QueryString["todo"] == "1")
                {
                    frmBackUpDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    frmEntrust_Date.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
                if (Entity.HouseID > 0)
                {
                    #region
                    frmStateID.SelectedValue = Entity.StateID.Value.ToString();
                    if (Entity.aType == 1)
                        PriceFollowUp.Visible = false;
                    houseID = Entity.HouseID.ToString();
                    Prices.Text = Entity.Ohter2ID.ToString();
                    button_follow.Attributes.Add("onclick", "fbwshow(" + Entity.HouseID + "1)");

                    if (CheckRolePermission("修改", Entity.OwnerEmployeeID.ToDecimal()))
                    {
                        btnSaveRent.Append("<li><div class=\"buttonActive\"><div class=\"buttonContent\"><button  onclick=\"return ClickButs('" + Entity.HouseID.ToString() + "')\" id=\"hf_Buttom\" type=\"button\">保存</button></div></div></li>");

                        frmMin_priceb.Attributes.Add("onfocus", "show_money('" + Entity.HouseID + "')");
                    }
                    if (CheckRolePermission("查看实价"))
                    {
                        frmMin_priceb.Attributes.Add("onfocus", "show_money('" + Entity.HouseID + "')");
                    }

                    BillCs = Entity.BillCode;

                    if (Entity.OwnerEmployeeID > 0)
                    {
                        Employee em = Employee.FindByEmployeeID(Entity.OwnerEmployeeID);
                        frmOperName.Text = "(" + em.Em_id + ")" + em.Em_name;
                    }
                    fexe_date.Text = Entity.Exe_date.ToString();
                    frmlandlord_tel2.ReadOnly = true;

                    #endregion
                    showAHouse.Attributes.Add("onclick", "ShowRemark('" + Entity.HouseID.ToString() + "');");
                    showAHouse.Attributes.Add("href", "House/HouseTelForm.aspx?HouseID=" + Entity.HouseID + "&doType=LookTel&NavTabId=" + NavTabId);
                    showAHouse.Attributes.Add("target", "dialog");
                    showAHouse.Attributes.Add("rel", "TelLookHouseList" + Entity.HouseID.ToString());
                    showAHouse.Attributes.Add("title", Entity.Shi_id);
                    htlA.HRef = "House/HouseTelForm.aspx?HouseID=" + Entity.HouseID + "&doType=LookTel&NavTabId=" + NavTabId;
                    htlA.Target = "dialog";
                    htlA.Attributes.Add("onclick", "ShowRemark('" + Entity.HouseID.ToString() + "');");
                    htlA.Attributes.Add("rel", "TelLookHouseList" + Entity.HouseID.ToString());
                    htlA.Attributes.Add("title", Entity.Shi_id);

                    if (Current.RoleNames.Contains("信息"))
                    {
                        btnDel.Attributes["style"] = "display:block";
                        btnDel.HRef += "?OperType=6&doAjax=true&LSH=1&NavTabId=" + NavTabId + "&HouseID=" + Entity.HouseID;
                    }
                    else
                    {
                        frmHouseDicName.ReadOnly = true;
                    }

                    //委托单号判断
                    if (!CheckRolePermission("修改委托单号", Entity.OwnerEmployeeID.ToDecimal()))
                    {
                        frmorderNum.ReadOnly = true;
                    }
                }
                else
                {
                    #region
                    if (CheckRolePermission("添加"))
                    {
                        btnSaveRent.Append("<li id=\"AddBtnHouseFrom\"><div class=\"buttonActive\"><div class=\"buttonContent\"><button  onclick=\"return ClickButs('" + Entity.HouseID.ToString() + "')\" id=\"hf_Buttom\" type=\"button\">保存</button></div></div></li>");
                    }
                    else
                    {
                        frmHouseDicName.ReadOnly = true;
                    }
                    shi_ids = H_houseinfor.NewHouseCode();
                    frmshi_id.Text = shi_ids;
                    frmOperName.Text = "(" + Employee.Current.Em_id + ")" + Employee.Current.Em_name;
                    fexe_date.Text = DateTime.Now.ToString();
                    frmform_bedroom.SelectedIndex = 0;
                    frmform_foreroom.SelectedIndex = 0;
                    BillCs = Employee.Current.OrgCode;
                    #endregion
                }
                string OperType = Request.QueryString["OperType"];
                string P_LSH = Request.QueryString["LSH"];
                string P_HouseID = Request.QueryString["HouseID"];

                #region 删除操作
                if (OperType != null && P_LSH != null)
                {
                    switch (OperType)
                    {
                        case "1": //删除图片/户型图
                            h_PicList model_h_PicList = h_PicList.FindByLsh(P_LSH.ToDecimal().Value);
                            h_PicListDel model_h_PicListDel = new h_PicListDel();
                            model_h_PicListDel.ComID = model_h_PicList.ComID;
                            model_h_PicListDel.EmployeeID = model_h_PicList.EmployeeID;
                            model_h_PicListDel.Exe_date = model_h_PicList.exe_date;
                            model_h_PicListDel.HouseID = model_h_PicList.HouseID;
                            model_h_PicListDel.PicTypeID = model_h_PicList.PicTypeID;
                            model_h_PicListDel.PicURL = model_h_PicList.PicURL;
                            model_h_PicListDel.OrgID = model_h_PicList.OrgID;
                            model_h_PicListDel.DelEmployeeID = Current.EmployeeID.ToInt32();
                            model_h_PicListDel.DelOrgID = Current.OrgID.ToInt32();
                            model_h_PicListDel.DelDate = DateTime.Now;
                            model_h_PicListDel.Insert();

                            Log log = new Log();
                            log.Action = "删除";
                            log.Category = "删除房源照片";
                            log.IP = HttpContext.Current.Request.UserHostAddress;
                            log.OccurTime = DateTime.Now;
                            log.UserID = Convert.ToInt32(Employee.Current.EmployeeID);
                            log.UserName = Employee.Current.Em_name;
                            log.Remark = "房源ID=" + P_HouseID;
                            log.Insert();

                            DbHelperSQL.ExecuteSql("delete from h_PicList where LSH= " + P_LSH);
                            if (h_PicList.FindCount("HouseID", P_HouseID) <= 0)
                            {
                                H_houseinfor.Meta.Query("update H_houseinfor set HasImage=0 where HouseID=" + P_HouseID);
                            }
                            else
                            {
                                //判断当前人员上传的房源照片（不包括户型图）是否小于5张，如果小于，则需要删除该人员的照片积分
                                string sql = string.Format("SELECT count(1) FROM h_PicList Where PicTypeID>1 And EmployeeID=" + model_h_PicList.EmployeeID + " And HouseID={0}", P_HouseID);
                                if (Convert.ToInt32(DbHelperSQL.GetSingle(sql)) < 5)
                                {
                                    UpdateIntegral("删除房源照片", model_h_PicList.EmployeeID.ToInt32().Value, DateTime.Now, "H_houseinfor", "HouseID", P_HouseID);
                                    //判断是否有其他人员上传照片满5张，如果有，则增加照片积分
                                    sql = "select * from h_PicList where EmployeeID<>" + model_h_PicList.EmployeeID + " and HouseID =" + P_HouseID + " and PicTypeID> 1 order by exe_date desc";
                                    List<h_PicList> list_h_PicList = h_PicList.FindAll(sql);
                                    if (list_h_PicList.Count >= 5)
                                    {
                                        if (Entity != null && Entity.Renovation != "毛坯")
                                        {
                                            UpdateIntegral("上传5张房源照片(租)", list_h_PicList[0].EmployeeID.ToInt32().Value, DateTime.Now, "H_houseinfor", "HouseID", P_HouseID);
                                        }
                                    }
                                }
                            }
                            StringBuilder sb = new StringBuilder();

                            sb.Append("<script>$(\"#div_" + P_LSH + "\").hide();alertMsg.correct(\"操作成功！\")</script>");
                            Response.Write(sb.ToString());
                            Response.End();
                            break;
                        case "4": //删除录音
                            DbHelperSQL.ExecuteSql("update i_InternetPhone set recordUrlDel=0 where phoneID= " + P_LSH);
                            Log l = new Log();
                            l.Action = "删除";
                            l.Category = "拨号录音";
                            l.OccurTime = DateTime.Now;
                            l.Remark = "房源编号：" + Entity.Shi_id;
                            l.Insert();
                            Response.Write("<script>alertMsg.correct(\"操作成功！\")</script>");
                            Response.End();
                            break;

                        case "6": //删除全部照片
                            List<h_PicList> list_h_PicList_All = h_PicList.FindAllByHouseID(P_HouseID.ToDecimal().Value);
                            foreach (h_PicList item in list_h_PicList_All)
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

                            DbHelperSQL.ExecuteSql("delete from h_PicList where HouseID= " + P_HouseID);

                            H_houseinfor.Update("HasImage=0", "HouseID=" + P_HouseID);

                            Log log1 = new Log();
                            log1.Action = "删除";
                            log1.Category = "删除房源照片";
                            log1.IP = HttpContext.Current.Request.UserHostAddress;
                            log1.OccurTime = DateTime.Now;
                            log1.UserID = Convert.ToInt32(Employee.Current.EmployeeID);
                            log1.UserName = Employee.Current.Em_name;
                            log1.Remark = "房源ID=" + P_HouseID;
                            log1.Insert();

                            StringBuilder sb1 = new StringBuilder();
                            sb1.Append("<script>$(\"#div_" + P_LSH + "\").hide();alertMsg.correct(\"操作成功！\")</script>");
                            Response.Write(sb1.ToString());
                            Response.End();
                            break;

                        default: break;
                    }
                }
                #endregion

                DataBinds();

                if (frmBackUpDate.Text != "")
                {
                    frmBackUpDate.Text = Convert.ToDateTime(frmBackUpDate.Text).ToString("yyyy-MM-dd");
                }
                if (frmEntrust_Date.Text != "")
                {
                    frmEntrust_Date.Text = Convert.ToDateTime(frmEntrust_Date.Text).ToString("yyyy-MM-dd");
                }
            }
        }

        /// <summary>
        /// 表单数据赋值前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnSetForm(object sender, EntityFormEventArgs e)
        {
            base.OnSetForm(sender, e);
            if (!IsPostBack)
            {
                string s_LinkTel1 = "（1）所有房间的朝向 （2）房内设施情况 （3）装修保养情况 （4）目前是否有人居住";
                string s_note = "是否有车位、什么时候可腾房等";
                string s_Remarks = "钥匙是否在外中介或其他地方的位置";

                if (Entity.LinkTel1.IsNullOrWhiteSpace())
                {
                    frmLinkTel1.ForeColor = System.Drawing.Color.Gray;
                    frmLinkTel1.Text = s_LinkTel1;
                }
                if (Entity.Note.IsNullOrWhiteSpace())
                {
                    frmnote.ForeColor = System.Drawing.Color.Gray;
                    frmnote.Text = s_note;
                }
                if (Entity.Remarks.IsNullOrWhiteSpace())
                {
                    frmRemarks.ForeColor = System.Drawing.Color.Gray;
                    frmRemarks.Text = s_Remarks;
                }

                frmLinkTel1.Attributes.Add("onfocus", "if(this.value=='" + s_LinkTel1 + "')this.value='';this.style.color='#000'");
                frmnote.Attributes.Add("onfocus", "if(this.value=='" + s_note + "')this.value='';this.style.color='#000'");
                frmRemarks.Attributes.Add("onfocus", "if(this.value=='" + s_Remarks + "')this.value='';this.style.color='#000'");

                frmLinkTel1.Attributes.Add("onblur", " this.style.color='#000';if(this.value==''){this.value='" + s_LinkTel1 + "';this.style.color='#999'}");
                frmnote.Attributes.Add("onblur", "this.style.color='#000';if(this.value==''){this.value='" + s_note + "';this.style.color='#999'}");
                frmRemarks.Attributes.Add("onblur", "this.style.color='#000';if(this.value==''){this.value='" + s_Remarks + "';this.style.color='#999'}");

                frmbID.Attributes.Add("onchange", "GetUnitItems_Rent(" + Entity.HouseID + ");");
                //this.frmUnitID.Attributes.Add("onchange", "GetRoomItems_Rent(" + Entity.HouseID + ");");
                frmRoomID.Attributes.Add("onchange", "GetDoorItems_Rent(" + Entity.HouseID + ");");
                //this.frmBuild_area.Attributes.Add("onblur", "GetPrices('" + Entity.HouseID + "')");
                //this.frmSum_price.Attributes.Add("onblur", "GetPrices('" + Entity.HouseID + "')");
                PriceFollowUp.Attributes.Add("href", "House/FollowUpPriceEditor.aspx?NavTabId=" + NavTabId + "&doAjax=true&HouseID=" + Entity.HouseID);
                PriceFollowUp.Attributes.Add("title", "增加压价跟进");
                if (Entity.HouseID > 0)
                {
                    //委托单号
                    orderNum_old.Value = Entity.orderNum.ToString();

                    //跟进
                    ligjRent.Visible = true;
                    if (Current.RoleNames.Contains("信息"))
                    {
                        //积分
                        lijf.Visible = true;
                    }

                    if (!Current.RoleNames.Contains("超级") && !Current.RoleNames.Contains("信息"))
                    {
                        //栋号不能修改
                        frmbID.Enabled = false;
                        frmbuild_id.Attributes.Add("ReadOnly", "true");
                        //室号不能修改
                        frmRoomID.Enabled = false;
                        frmbuild_room.Attributes.Add("ReadOnly", "true");
                    }

                    if (Entity.HouseDicType == "1")
                    {
                        frmbuild_room.CssClass = "Hinput frmbuild_room";
                        if (Entity.Bid.HasValue && Entity.Bid.Value > 0 && Entity.RoomID.HasValue && Entity.RoomID.Value > 0)
                        {
                            List<s_Seat> seatList = s_Seat.FindAll(string.Format(@"select SeatID,SeatName
                                                                                from s_Seat
                                                                                where HouseDicID={0}", Entity.HouseDicID));

                            frmbID.DataSource = seatList;
                            frmbID.DataTextField = "SeatName";
                            frmbID.DataValueField = "SeatID";
                            frmbID.DataBind();
                            frmbID.SelectedValue = Entity.Bid.Value.ToString();
                            frmbuild_id.Text = frmbID.SelectedItem.Text;

                            List<s_Door> doorList = s_Door.FindAll(string.Format(@"select DoorID,DoorNam
                                                                                    from s_Door
                                                                                    where SeatID={0}
                                                                                    and DoorID not in(select RoomID
                                                                                                        from h_houseinfor
                                                                                                        where bID={0}
                                                                                                        and RoomID<>{1}
                                                                                                        and aType=0
                                                                                                        and DelType=0
                                                                                                        and RoomID>0)
                                                                                    order by DoorNam",
                                                                                    Entity.Bid.Value,
                                                                                    Entity.RoomID.Value));

                            frmRoomID.DataSource = doorList;
                            frmRoomID.DataTextField = "DoorNam";
                            frmRoomID.DataValueField = "DoorID";
                            frmRoomID.DataBind();
                            frmRoomID.SelectedValue = Entity.RoomID.Value.ToString();
                            frmbuild_room.Text = frmRoomID.SelectedItem.Text;
                        }
                    }

                    frmlandlord_tel2.Text = "点击查看隐藏信息！";

                    #region 暂时无用 2017-07-14 10:32:15
                    //if (Entity.Bid.HasValue && Entity.Bid > 0)
                    //{
                    //    //var list_s_Seat = s_Seat.Find(string.Format("HouseDicID={0} and SeatID={1}", Entity.HouseDicID, Entity.Bid));
                    //    //frmbID.DataSource = list_s_Seat;
                    //    //frmbID.DataTextField = "SeatName";
                    //    //frmbID.DataValueField = "SeatID";
                    //    //frmbID.DataBind();
                    //    //this.frmUnitID.DataSource = s_Unit.FindAll("SeatID", Entity.Bid);
                    //    //this.frmUnitID.DataTextField = "UnitNam";
                    //    //this.frmUnitID.DataValueField = "UnitID";
                    //    //this.frmUnitID.DataBind();
                    //    //foreach (ListItem li in frmUnitID.Items)
                    //    //{
                    //    //    if (li.Text == Entity.Build_unit)
                    //    //        li.Selected = true;
                    //    //}
                    //}
                    //else
                    //{
                    //    frmbID.Items.Add(new ListItem(Entity.Build_id, "0"));
                    //    //this.frmUnitID.Items.Add(new ListItem(Entity.Build_unit, "0"));
                    //}

                    //if (Entity.UnitID.HasValue && Entity.UnitID > 0)
                    //{
                    //    //List<s_Door> list_s_Door = s_Door.FindAll("UnitID", Entity.UnitID);
                    //    //list_s_Door.OrderBy(x => x.DoorNam);
                    //    //this.frmRoomID.DataSource = list_s_Door;
                    //    //this.frmRoomID.DataTextField = "DoorNam";
                    //    //this.frmRoomID.DataValueField = "DoorID";
                    //    //this.frmRoomID.DataBind();
                    //    //foreach (ListItem li in frmRoomID.Items)
                    //    //{
                    //    //    if (li.Text == Entity.Build_room)
                    //    //        li.Selected = true;
                    //    //}
                    //}
                    //else
                    //    frmRoomID.Items.Add(new ListItem(Entity.Build_room, "0"));
                    #endregion
                }

                if (!CheckRolePermission("不隐号"))
                {
                    //frmNotarizationNum.Text = "***";
                    if (Entity.IsBeiAn.HasValue && Entity.IsBeiAn.Value)
                    {
                        frmbuild_id.Text = "***";
                        //frmbuild_unit.Text = "***";
                        frmbuild_room.Text = "***";
                        this.frmbID.Items.Add(new ListItem("", "***"));
                        //this.frmUnitID.Items.Add(new ListItem("", "***"));
                        frmRoomID.Items.Add(new ListItem("", "***"));
                    }
                }

                //如果没有权限，则委托方式不能修改
                if (!CheckRolePermission("修改委托"))
                {
                    if (Entity.EntrustTypeID.HasValue)
                    {
                        frmEntrustTypeID.SelectedValue = Entity.EntrustTypeID.Value.ToString();
                        ListItem li = frmEntrustTypeID.SelectedItem;
                        frmEntrustTypeID.Items.Clear();
                        frmEntrustTypeID.Items.Add(li);
                    }
                    else
                    {
                        frmEntrustTypeID.Items.Clear();
                        frmEntrustTypeID.Items.Insert(0, new ListItem("一般委托", "1"));
                    }
                }
                
                frmRemarks.Text = "***";
                frmMin_priceb.Text = "***";

                if (Entity.SanjakID > 0)
                {
                    s_Sanjak model_s_Sanjak = s_Sanjak.FindBySanjakID(Entity.SanjakID);
                    if (model_s_Sanjak != null)
                    {
                        ddlSanjakID.Items.Add(new ListItem(model_s_Sanjak.Name, model_s_Sanjak.SanjakID.ToString()));

                        s_Area model_s_Area = s_Area.FindByAreaID(model_s_Sanjak.AreaID);

                        if (model_s_Area != null)
                        {
                            ddlArea.Items.Add(new ListItem(model_s_Area.Name, model_s_Area.AreaID.ToString()));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 插入之前执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnSaving(object sender, EntityFormEventArgs e)
        {
            if (Entity.HouseID > 0)
            {
                //判断是否委托单号大于0，并且和原先的委托单号不同
                if (Entity.orderNum > 0 && Entity.orderNum != orderNum_old.Value.ToInt32())
                {
                    string sql = string.Format(@"IF EXISTS(SELECT 1 
                                                    FROM   h_houseinfor 
                                                    WHERE  atype = 1 
                                                            AND ordernum = {0}) 
                                                    SELECT 1 
                                                ELSE 
                                                    SELECT 0 ",
                                                    Entity.orderNum);
                    if (DbHelperSQL.GetSingle(sql).ToInt16() == 1)
                    {
                        ShowMsg(AlertType.error, "已存在该委托单号，请重新输入！");
                    }
                }

                Entity.CurrentEmployee = Employee.Current.EmployeeID;
            }
            else
            {
                //判断是否委托单号大于0，
                if (Entity.orderNum > 0)
                {
                    string sql = string.Format(@"IF EXISTS(SELECT 1 
                                                    FROM   h_houseinfor 
                                                    WHERE  atype = 1 
                                                            AND ordernum = {0}) 
                                                    SELECT 1 
                                                ELSE 
                                                    SELECT 0 ",
                                                    Entity.orderNum);
                    if (DbHelperSQL.GetSingle(sql).ToInt16() == 1)
                    {
                        ShowMsg(AlertType.error, "已存在该委托单号，请重新输入！");
                    }
                }

                Entity.Shi_id = H_houseinfor.NewHouseCode();
                Entity.OrgID = Convert.ToInt32(Employee.Current.OrgID);
                Entity.OwnerEmployeeID = Convert.ToInt32(Employee.Current.EmployeeID);
                Entity.OperatorID = Convert.ToInt32(Employee.Current.EmployeeID);
                //新增时默认状态为委托中
                //Entity.StateID = 2;
            }
            Entity.UnitID = Request.Form["frmUnitID"].ToInt32();
            Entity.RoomID = Request.Form["frmRoomID"].ToInt32();
            Entity.Rent_Price = Request.Form["frmRent_Price"].ToDecimal();
            Entity.SanjakID = Request.Form["ddlSanjakID"].ToInt32();
            Entity.Facility = Request.Form["F.frmFacility"];

            if (Request.Form["h.frmbuild_id"].TrimStart('0').Length > 0)
            {
                Entity.Build_id = Request.Form["h.frmbuild_id"].TrimStart('0');
            }
            else
            {
                Entity.Build_id = "0";
            }
            if (Request.Form["h.frmbuild_room"].TrimStart('0').Length > 0)
            {
                Entity.Build_room = Request.Form["h.frmbuild_room"].TrimStart('0');
            }
            else
            {
                Entity.Build_room = "0";
            }

            if (Request.Form["frmSum_price"] != "")
            {
                Entity.Sum_price = Convert.ToDecimal(Request.Form["frmSum_price"]);
            }
            Entity.Ohter2ID = Request.Form["Prices"].ToInt32();

            string s_LinkTel1 = "（1）所有房间的朝向 （2）房内设施情况 （3）装修保养情况 （4）目前是否有人居住";
            string s_note = "是否有车位、什么时候可腾房等";
            string s_Remarks = "钥匙是否在外中介或其他地方的位置";

            if (Request.Form["frmLinkTel1"].ToString().Trim().Equals(s_LinkTel1))
            {
                Entity.LinkTel1 = string.Empty;
            }
            if (Request.Form["frmnote"].ToString().Trim().Equals(s_note))
            {
                Entity.Note = string.Empty;
            }
            if (Request.Form["frmRemarks"].ToString().Trim().Equals(s_Remarks))
            {
                Entity.Remarks = string.Empty;
            }

            base.OnSaving(sender, e);
        }

        #region   获取房源判重的参数

        [AjaxPro.AjaxMethod]
        public string GetRepeat()
        {
            s_SysParam ss = s_SysParam.FindByParamCode("HouseRepeat");
            return ss.Value;
        }

        #endregion

        #region   找到房源的锁盘数量,系统设置锁盘总量供前台JS 调用
        //[AjaxPro.AjaxMethod]
        //public string GetNum()
        //{
        //    int houseNum = 0;
        //    int systemparms = 0;
        //    DataSet dsHouse = H_houseinfor.Meta.Query("SELECT COUNT(*) NUM FROM h_houseinfor WHERE IsLock = 1 AND OrgID = " + HouseMIS.EntityUtils.Employee.Current.OrgID);
        //    DataSet dsSystem = s_SysParam.Meta.Query("SELECT * FROM s_SysParam WHERE ParamCode = 'LockNum'");
        //    if (dsHouse != null)
        //    {
        //        houseNum = Convert.ToInt32(dsHouse.Tables[0].Rows[0][0]);
        //    }
        //    if (dsSystem != null)
        //    {
        //        systemparms = Convert.ToInt32(dsSystem.Tables[0].Rows[0]["Value"]);
        //    }
        //    return houseNum + "|" + systemparms;
        //}
        #endregion

        #region  获取保密说明

        [AjaxPro.AjaxMethod]
        public string GetRemark(Decimal houseId)
        {
            H_houseinfor hh = H_houseinfor.FindByHouseID(houseId);
            return hh.Remarks;
        }

        #endregion

        #region  获取可设置私盘数
        //[AjaxPro.AjaxMethod]
        //public string GetPrivateNum(string OwnerEmployeeIDP)
        //{
        //    string result = "True";
        //    if (H_houseinfor.Meta.Query("exec [dbo].[h_GetIsPrivateNum] " + OwnerEmployeeIDP).Tables[0].Rows[0][0].ToString() != "True")
        //    {
        //        result = "False";
        //    }
        //    return result;
        //}
        #endregion

        protected void DataBinds()
        {
            if (Request.QueryString["EditType"] != null)
            {
                this.h_gj.Visible = true;
                this.h_hxt.Visible = true;
                this.h_zp.Visible = true;
                this.h_cdjl.Visible = true;
                this.h_dk.Visible = true;
            }
            else
            {
                this.h_gj.Visible = false;
                this.h_hxt.Visible = false;
                this.h_zp.Visible = false;
                this.h_cdjl.Visible = false;
            }
        }

        #region   获取栋号单元室号详情   js/Function.js

        [AjaxPro.AjaxMethod]
        public string GetBuildItems(string HouseDicId)
        {
            string itemList = string.Empty;
            List<s_Seat> seatList = s_Seat.FindAll("select SeatID,SeatName from s_Seat where HouseDicID=" + HouseDicId + " order by [Index],SeatID");
            itemList += "<option value=''></option>";
            if (seatList != null && seatList.Count > 0)
            {
                foreach (s_Seat seat in seatList)
                {
                    itemList += "<option value='" + seat.SeatID + "'>" + seat.SeatName + "</option>";
                }
            }
            return itemList;
        }

        [AjaxPro.AjaxMethod]
        public string GetUnitItems(string seatId)
        {
            string itemList = string.Empty;

            List<s_Door> doorList = s_Door.FindAll(string.Format(@"select DoorID,DoorNam
                                                                        from s_Door
                                                                        where SeatID={0}
                                                                        and DoorID not in(select RoomID
                                                                                            from h_houseinfor
                                                                                            where bID ={0}
                                                                                            and aType = 1
                                                                                            and DelType=0
                                                                                            and RoomID> 0)
                                                                        order by DoorNam",
                                                                    seatId));
            itemList += "0@<option value=''></option>";
            if (doorList != null && doorList.Count > 0)
            {
                foreach (s_Door door in doorList)
                {
                    itemList += "<option value='" + door.DoorID + "'>" + door.DoorNam + "</option>";
                }
            }

            return itemList;
        }

        [AjaxPro.AjaxMethod]
        public string GetRoomItems(string seatId)
        {
            string itemList = string.Empty;
            List<s_Door> doorList = s_Door.FindAll("select DoorID,DoorNam from s_Door where UnitID=" + seatId + " order by DoorNam");
            itemList += "<option value=''></option>";
            if (doorList != null && doorList.Count > 0)
            {
                foreach (s_Door door in doorList)
                {
                    itemList += "<option value='" + door.DoorID + "'>" + door.DoorNam + "</option>";
                }
            }

            return itemList;
        }

        [AjaxPro.AjaxMethod]
        public string GetDoorItems(string RoomId)
        {
            string itemList = string.Empty;
            s_Door door = s_Door.Find("DoorID", RoomId);
            if (door != null)
            {
                itemList += door.Area + "|";
                itemList += door.Balcony + "|";
                itemList += door.FacetoID + "|";
                itemList += door.Floor + "|";
                itemList += door.Hall + "|";
                itemList += door.Room + "|";
                itemList += door.Toilet + "|";
                itemList += door.YearID + "|";
                s_Seat temp_s_Seat = s_Seat.Find(s_Seat._.SeatID, door.SeatID);
                if (temp_s_Seat != null && temp_s_Seat.Floor.HasValue)
                {
                    itemList += temp_s_Seat.Floor.Value;
                }
            }

            return itemList;
        }

        #endregion

        //public string GU_ID = "";

        [AjaxPro.AjaxMethod]
        public string GetSanjak(string areaID)
        {
            StringBuilder sb = new StringBuilder();

            List<s_Sanjak> list_s_Sanjak = s_Sanjak.FindAll("select * from s_Sanjak where AreaID=" + areaID);
            if (list_s_Sanjak.Count > 0)
            {
                //sb.Append("<option value=\"0\">请选择</option>");
                for (int i = 0; i < list_s_Sanjak.Count; i++)
                {
                    sb.Append("<option value=\"" + list_s_Sanjak[i].SanjakID + "\">" + list_s_Sanjak[i].Name + "</option>");
                }
            }
            else
            {
                sb.Append("<option value=\"0\">无数据</option>");
            }

            return sb.ToString();
        }

        /// <summary>
        /// 根据商圈ID获取商圈名称
        /// </summary>
        /// <param name="SanjakID"></param>
        /// <returns></returns>
        [AjaxPro.AjaxMethod]
        public string GetDDLSanjak(string SanjakID)
        {
            StringBuilder sb = new StringBuilder();

            s_Sanjak model_s_Sanjak = s_Sanjak.FindBySanjakID(Convert.ToDecimal(SanjakID));
            if (model_s_Sanjak != null)
            {
                sb.Append("<option value=\"" + model_s_Sanjak.SanjakID + "\">" + model_s_Sanjak.Name + "</option>");
            }
            else
            {
                sb.Append("<option value=\"0\">无数据</option>");
            }

            return sb.ToString();
        }

        /// <summary>
        /// 根据商圈ID获取区域名称
        /// </summary>
        /// <param name="SanjakID"></param>
        /// <returns></returns>
        [AjaxPro.AjaxMethod]
        public string GetDDLArea(string SanjakID)
        {
            StringBuilder sb = new StringBuilder();

            s_Sanjak model_s_Sanjak = s_Sanjak.FindBySanjakID(Convert.ToDecimal(SanjakID));
            if (model_s_Sanjak != null)
            {
                s_Area model_s_Area = s_Area.FindByAreaID(model_s_Sanjak.AreaID);
                if (model_s_Area != null)
                {
                    sb.Append("<option value=\"" + model_s_Area.AreaID + "\">" + model_s_Area.Name + "</option>");
                }
                else
                {
                    sb.Append("<option value=\"0\">无数据</option>");
                }
            }
            else
            {
                sb.Append("<option value=\"0\">无数据</option>");
            }

            return sb.ToString();
        }

        protected string z_keys()
        {
            string strInfo = "";
            if (Request.QueryString["HouseID"] != null)
            {
                string HouseID = Request.QueryString["HouseID"];
                DataTable dt = DbHelperSQL.Query(string.Format(@"SELECT TOP 1 H.IsIn,
                                                                                (S.BillCode+'-'+S.Name) AS Name,H.InOhterCompany,H.Remarks,S.Tel,H.exe_date,H.TEL T,O.OhterCompanyName,H.IsLandran
                                                                    FROM h_HouseKey H
                                                                    LEFT JOIN s_Organise S
                                                                        ON S.OrgID = H.OrgID
                                                                    LEFT JOIN h_OhterCompanyName O
                                                                        ON O.OhterCompanyNameID = H.OhterCompanyNameID
                                                                    WHERE H.HouseID ={0}
                                                                            AND h.isDel=0
                                                                    ORDER BY  H.exe_date DESC",
                                                                    HouseID)).
                                                                    Tables[0];

                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(dt.Rows[0][0]) == true)
                    {
                        // 其他中介
                        if (dt.Rows[0]["InOhterCompany"].ToString() == "True")
                        {
                            if (dt.Rows[0]["OhterCompanyName"].ToString() != "")
                                strInfo = "<span title=\" 电话: " + dt.Rows[0]["T"] + "; 备注: (" + dt.Rows[0]["Remarks"] + ")时间: " + dt.Rows[0]["exe_date"] + "\">钥匙在" + dt.Rows[0]["OhterCompanyName"] + "</span>";
                            else
                                strInfo = "<span title=\"备注:(" + dt.Rows[0]["Remarks"] + ")；时间:" + dt.Rows[0]["exe_date"] + ")\">钥匙在其他中介</span>";
                        }
                        // 选择了房东
                        else if (dt.Rows[0]["IsLandran"].ToString() == "1")
                        {
                            strInfo = "<span title=\"备注:(" + dt.Rows[0]["Remarks"] + ")；时间:" + dt.Rows[0]["exe_date"] + ")\">钥匙在房东手上</span>";
                        }
                        else
                        {
                            strInfo = "<span title=\"$tit$\">钥匙在:" + dt.Rows[0][1] + "</span>";
                            if (dt.Rows[0][4] != null && !dt.Rows[0][4].ToString().IsNullOrWhiteSpace())
                            {
                                strInfo = strInfo.Replace("$tit$", "分部电话:" + dt.Rows[0][4] + "；时间:" + dt.Rows[0]["exe_date"]);
                            }
                            else
                                strInfo = strInfo.Replace("$tit$", "");
                        }
                    }
                    else
                    {
                        strInfo = "该房源没有拿钥匙";
                    }
                }
                else
                {
                    strInfo = "该房源没有拿钥匙";
                }
            }

            return strInfo;
        }
    }
}