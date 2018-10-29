using HouseMIS.Common;
using HouseMIS.EntityUtils;
using HouseMIS.EntityUtils.DBUtility;
using HouseMIS.Web.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using TCode;
using Menu = HouseMIS.EntityUtils.Menu;

namespace HouseMIS.Web.House
{
    public partial class HouseForm : EntityFormBase<H_houseinfor>
    {
        public decimal EmployeeID = Employee.Current.EmployeeID;
        public string EmID = Employee.Current.Em_id;
        public string ShowPoint, shi_ids;
        public string OAID;
        public string yysys;
        public string qxyy;

        public string isDisplay = string.Empty;

        public override string MenuCode
        {
            get
            {
                return "2001";
            }
        }

        public bool AllViewPic = true;

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

        /// <summary>
        /// 传入一个工号把他所有上传的视频房源全部册掉去
        /// </summary>
        /// <param name="EmId">员工工号</param>
        /// <param name="ShiId">传入房源编号</param>
        /// <returns>0:没有找到该员工,00:没有找到该房源编号,1:成功</returns>
        public string DelFile(string EmId, string ShiId)
        {
            //第一得到员工ID号
            if (EmId != "")
            {
                /////得到房源ID号
                object obj2 = DbHelperSQL.GetSingle("select top 1 HouseID from h_houseinfor where shi_id='" + ShiId.Trim() + "'");
                if (obj2 != null)
                {
                    //第三得到我上传的所有视频文件
                    DataTable dt = DbHelperSQL.Query("select FileNam,IsTran,Pic,HouseID from HouseVideo where EmployeeID=" + EmId.ToString() + " and HouseID=" + obj2.ToString()).Tables[0];
                    int length = dt.Rows.Count;
                    if (length > 0)
                    {
                        for (int i = 0; i < length; i++)
                        {
                            //第四得到我每一个上传的视频文件如果存在的话就册掉去  IsTran（0：等待转码，1：已转码， 2：转码没成功，3：图片裁剪没成功）

                            string filePath = HttpContext.Current.Server.MapPath("/UpHouseVideoTran/" + dt.Rows[i][0]);
                            string picPath = HttpContext.Current.Server.MapPath("/UpHouseVideoTran/" + dt.Rows[i][2]);

                            //////////删除视频文件
                            if (System.IO.File.Exists(filePath))
                            {
                                System.IO.File.Delete(filePath);
                            }

                            if (dt.Rows[i][1].ToString() == "1" && dt.Rows[i][1].ToString() != "3")
                            {
                                if (dt.Rows[i][2].ToString() != "wait.png")
                                {
                                    //////如果裁剪图片成功了也册掉去
                                    if (System.IO.File.Exists(picPath))
                                    {
                                        System.IO.File.Delete(picPath);
                                    }
                                }
                            }
                        }

                        /////把房源表里面的字段也标记一下该房源没有视频
                        StringBuilder sb2 = new StringBuilder();
                        for (int i = 0; i < length; i++)
                        {
                            sb2.Append(dt.Rows[i][3]);
                            sb2.Append(",");
                        }
                        string delId = sb2.Remove(sb2.Length - 1, 1).ToString();
                        DbHelperSQL.ExecuteSql("update h_houseinfor set IsVideo=0 where HouseID in(" + delId + ")");

                        ///////把数据库的信息也册掉去
                        DbHelperSQL.ExecuteSql("delete HouseVideo where EmployeeID=" + EmId.ToString() + " and HouseID=" + obj2.ToString());
                        return "1";
                    }
                }
                else
                {
                    return "00";
                }
            }

            return "0";
        }

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
            //状态是委托中，并且总部认证为空，表示新增委托中房源
            if (Request.Form["frmStateID"] != null && Entity.StateID == 2)
            {
                h_houseinfor_ZBCheck entity = new h_houseinfor_ZBCheck();
                entity.houseID = Convert.ToInt32(Entity.HouseID);
                entity.state_ZBCheck = CheckState.待认证.ToInt16();
                entity.employeeID = Convert.ToInt32(Current.EmployeeID);
                entity.exe_Date = DateTime.Now;
                entity.comID = Current.ComID;

                //如果录音ID不为空，则更新录音关联的房源ID
                if (!phoneID.Value.IsNullOrWhiteSpace())
                {
                    i_InternetPhone iip = i_InternetPhone.FindByKey(phoneID.Value);
                    iip.houseID = Entity.HouseID;
                    iip.Update();

                    entity.phoneID = phoneID.Value.ToInt32();
                }

                entity.Insert();
            }

            //如果委托方式是 速销代码或十万火急，判断是否有责任人
            if ((Entity.EntrustTypeID == 4 || Entity.EntrustTypeID == 7) && Entity.Fast_UserID.HasValue)
            {
                //增加排序积分
                s_SysParam ss = s_SysParam.FindByParamCode("houseLimit");
                //获取分隔符的值，第一个为分值，第二是否有 有效期，第三为有效期值
                string[] ssValue = ss.Value.Split('|');

                e_Integral ei = new e_Integral();
                ei.employeeID = Entity.Fast_UserID.Value;
                ei.Type = (int)integral_Type.房源与经纪人;
                ei.tableName = "h_houseinfor";
                ei.coloumnName = "HouseID";
                ei.keyID = Convert.ToInt32(Entity.HouseID);
                ei.integralParam = "houseLimit";
                ei.integralValue = ssValue[0].ToInt32();
                ei.integralDay = ssValue[1] == "1" ? ssValue[2].ToInt32() : 0;
                ei.exe_Date = DateTime.Now;
                ei.Insert();
            }

            StringBuilder sb = new StringBuilder();
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
            PubFunction.FullDropListData(typeof(h_EntrustType), frmEntrustTypeID, "Name", "EntrustTypeID", "");
            PubFunction.FullDropListData(typeof(h_Taxes), frmTaxesID, "Name", "TaxesID", "");
            PubFunction.FullDropListData(typeof(h_Appliance), frmApplianceID, "Name", "ApplianceID", "");
            PubFunction.FullDropListData(typeof(h_Assort), frmAssortID, "Name", "AssortID", "");
            PubFunction.FullDropListData(typeof(h_Cause), frmSaleMotiveID, "Name", "SaleMotiveID", "");
            //产权年限
            PubFunction.FullDropListData(typeof(h_houseinfor_propertyYear), frmpropertyYear, "Name", "propertyYearID", "");

            List<h_State> h_State_list = h_State.FindAllWithCache("name", "委托中");
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

            oa_NewClass on = oa_NewClass.FindWithCache("Name", "房源管理");
            if (on != null)
                OAID = on.NewClassID.ToString();
            else
                OAID = "";

            //房源等级设置权限
            //if (CheckRolePermission("设置房源等级"))
            //{
            //    fydj.Visible = true;
            //    frmLevelType.Visible = true;
            //}
        }

        //保存按钮
        protected StringBuilder btnSave = new StringBuilder();

        //protected int webpos = 0;
        //查看备案电话
        //protected StringBuilder LookTelScript = new StringBuilder();

        protected string hoby = "";

        protected string gd_num = Employee.Current.GD_Num;
        protected string emid = Employee.Current.EmployeeID.ToString();

        public string canbut = "0";

        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(HouseMIS.Web.House.HouseForm), Page);

            if (!IsPostBack)
            {
                frmRoomID.Style.Add("width", "130px");
                frmbID.Style.Add("width", "118px");
                frmbuild_id.Style.Add("width", "113px");
                frmbuild_room.Style.Add("width", "123px");

                AllViewPic = CheckRolePermission("上传全景照片");
                if (Request.QueryString["todo"] == "1")
                {
                    frmEntrust_Date.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
                if (Entity.HouseID > 0)
                {
                    int num2 = DbHelperSQL.GetSingle("select count(1) from h_PicList_YY where IsDel =1 and HouseID=" + Entity.HouseID).ToInt32().Value;
                    int num1 = DbHelperSQL.GetSingle("select count(1) from h_PicList_YY where HouseID=" + Entity.HouseID).ToInt32().Value;
                    int IsShNum = DbHelperSQL.GetSingle("select count(1) from h_houseinfor where IsSh=1 and HouseID=" + Entity.HouseID).ToInt32().Value;
                    if (CheckRolePermission("预约摄影师") && Entity.StateID == 2)
                    {
                        if (num2 > 0 || num1 == 0)
                            yysys = "<a class=\"button\"  id=\"sybut\" href=\"House/HouseForm.aspx?OperType=8&NavTabId=" + NavTabId + "&HouseID=" + Entity.HouseID + "&doAjax =true\" target=\"ajaxTodo\" title=\"确定要操作吗？\"><span>预约摄影师</span></a>";
                        else
                            yysys = "<span style='color:red'>此房源已预约摄影师</span>";

                        //照片审核已经通过的不能预约
                        if ((IsShNum > 0 || Entity.FitmentID == 2) && (Entity.EntrustTypeID != 4 && Entity.EntrustTypeID != 6 && Entity.EntrustTypeID != 7))
                        {
                            yysys = "";
                        }
                    }
                    else
                    {
                        if (num2 > 0 || num1 == 0)
                            yysys = "";
                        else
                            yysys = "<span style='color:red'>此房源已预约摄影师</span>";
                    }
                    //取消预约摄影师按钮
                    int numqx = int.Parse(DbHelperSQL.Query("select count(*) from h_PicList_YY where photographer is null and Finishtime is null and EmployeeID=" + Current.EmployeeID + " and HouseID=" + Entity.HouseID).Tables[0].Rows[0][0].ToString());
                    if (CheckRolePermission("预约摄影师"))
                    {
                        if (numqx > 0)
                        {
                            qxyy = "<a class=\"button\"  id=\"qxbut\" href=\"House/HouseForm.aspx?LSH=1&OperType=7&NavTabId=" + NavTabId + "&HouseID=" + Entity.HouseID + "&doAjax =true\" target=\"ajaxTodo\" title=\"确定要操作吗？\"><span>取消预约</span></a>";
                            //h_PicList_YY list_h_PicList_YY = h_PicList_YY.Find(" photographer is null and Finishtime is null and EmployeeID = " + Current.EmployeeID+" and HouseID = " + Entity.HouseID);
                            //list_h_PicList_YY.Delete();
                        }
                    }

                    if (Entity.aType == 1)
                        PriceFollowUp.Visible = false;

                    houseID = Entity.HouseID.ToString();
                    Prices.Text = Entity.Ohter2ID.ToString();
                    frmHouseDicAddress.Text = Entity.HouseDicAddress == "undefined" ? "" : Entity.HouseDicAddress;
                    button_follow.Attributes.Add("onclick", "fbwshow(" + Entity.HouseID + "1)");

                    if (CheckRolePermission("修改", Entity.OwnerEmployeeID.ToDecimal()))
                    {
                        btnSave.Append("<li id=\"hf_Buttom\"><div class=\"buttonActive\"><div class=\"buttonContent\"><button onclick=\"return ClickButs('" + Entity.HouseID.ToString() + "')\" type=\"button\">保存</button></div></div></li>");

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

                    if (!CheckRolePermission("锁盘", Entity.OwnerEmployeeID.ToDecimal()))
                    {
                        frmIsLock.Visible = false;
                    }

                    frmlandlord_tel2.ReadOnly = true;

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

                    //当是限时房源并且有责任人的时候(编辑的状态下)---十万火急，速效待卖，换新业务，有责任人电话的情况下。

                    if (Entity.HouseID > 0 && (Entity.EntrustTypeID == 4 || Entity.EntrustTypeID == 6 || Entity.EntrustTypeID == 7))
                    {
                        h_HouseTimeLimitMsg Msg = h_HouseTimeLimitMsg.Find(h_HouseTimeLimitMsg._.HouseID, Entity.HouseID);
                        if (Msg != null)
                        {
                            htlA.HRef = "House/HouseTelFormSX.aspx?HouseID=" + Entity.HouseID + "&doType=LookTel&NavTabId=" + NavTabId;
                            htlA.Target = "dialog";
                            htlA.Attributes.Add("onclick", "ShowRemark('" + Entity.HouseID.ToString() + "');");
                            htlA.Attributes.Add("rel", "TelLookHouseList" + Entity.HouseID.ToString());
                            htlA.Attributes.Add("title", Entity.Shi_id);
                        }
                    }

                    if (Current.RoleNames.Contains("信息"))
                    {
                        btnDel.Attributes["style"] = "display:block";
                        btnDel.HRef += "?OperType=6&doAjax=true&LSH=1&NavTabId=" + NavTabId + "&HouseID=" + Entity.HouseID;
                        btnEmpty_Sale.Attributes["style"] = "display:block";
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
                    btnPrice.HRef = string.Format("House/FollowUpPriceList.aspx?NavTabId={0}&doAjax=true&HouseID={1}",
                                                    NavTabId,
                                                    Entity.HouseID);
                }
                else
                {
                    //网络来电记录
                    if (!Request.QueryString["CusPhone"].IsNullOrWhiteSpace() &&
                        !Request.QueryString["phoneID"].IsNullOrWhiteSpace())
                    {
                        phoneID.Value = Request.QueryString["phoneID"].ToString();
                        frmlandlord_tel2.Text = Request.QueryString["CusPhone"].ToString();
                    }

                    isDisplay = "style=\"display: none\"";

                    btnPrice.Visible = false;
                    frmMin_priceb.ReadOnly = false;
                    frmMin_priceb.Text = "0";
                    //HouseNote.Visible = false;
                    //frmIsPrivate.Visible = false;
                    //GU_ID = DateTime.Now.ToString("yyyymmddhhmmss") + Employee.Current.EmployeeID.ToString();
                    //GU_IDS.Value = GU_ID;
                    //this.frmlandlord_tel2.ReadOnly = true;
                    if (!CheckRolePermission("锁盘"))
                    {
                        frmIsLock.Visible = false;
                    }
                    if (!CheckRolePermission("设置私盘"))
                    {
                        frmIsLock.Visible = false;
                    }
                    if (CheckRolePermission("添加"))
                    {
                        btnSave.Append("<li id=\"AddBtnHouseFrom\"><div class=\"buttonActive\"><div class=\"buttonContent\"><button onclick=\"return ClickButs('" + Entity.HouseID.ToString() + "')\" type=\"button\">保存</button></div></div></li>");
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
                }
                string OperType = Request.QueryString["OperType"];
                string P_LSH = Request.QueryString["LSH"];
                string P_HouseID = Request.QueryString["HouseID"];

                #region 删除操作

                if (OperType != null)
                {
                    if (P_LSH != null)
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

                                DbHelperSQL.ExecuteSql("delete from h_PicList where LSH= " + P_LSH);
                                if (h_PicList.FindCount("HouseID", P_HouseID) <= 0)
                                {
                                    H_houseinfor.Update("HasImage=0", "HouseID=" + P_HouseID);
                                }

                                Log log = new Log();
                                log.Action = "删除";
                                log.Category = "删除房源照片";
                                log.IP = HttpContext.Current.Request.UserHostAddress;
                                log.OccurTime = DateTime.Now;
                                log.UserID = Convert.ToInt32(Employee.Current.EmployeeID);
                                log.UserName = Employee.Current.Em_name;
                                log.Remark = "房源ID=" + P_HouseID;
                                log.Insert();

                                StringBuilder sb = new StringBuilder();

                                sb.Append("<script>$(\"#div_" + P_LSH + "\").hide();alertMsg.correct(\"操作成功！\")</script>");
                                Response.Write(sb.ToString());
                                Response.End();
                                break;

                            case "2": //删除视频
                                if (Request.QueryString["housevideoid"] != null)
                                {
                                    HouseVideo hv = HouseVideo.FindByHouseVideoID(Request.QueryString["housevideoid"].ToInt32());

                                    if (hv != null)
                                    {
                                        hv.isDel = 1;
                                        hv.delEmployeeID = Current.EmployeeID.ToInt32().Value;
                                        hv.delDate = DateTime.Now;
                                        hv.Update();

                                        if (HouseVideo.FindCount(string.Format(" houseid={0} and isdel=0", hv.HouseID), "", "", 0, 0) == 0)
                                        {
                                            HouseVideo.Meta.Execute(string.Format("update h_houseinfor set IsVideo=0 where houseid={0}",
                                                                    hv.HouseID));
                                        }
                                        Response.Write("<script>alertMsg.correct(\"操作成功！\")</script>");
                                        Response.End();
                                    }
                                    else
                                    {
                                        Response.Write("<script>alertMsg.correct(\"没有找到此视频！\")</script>");
                                        Response.End();
                                    }
                                }
                                break;

                            //case "3": //全景照片
                            //    DbHelperSQL.ExecuteSql("delete from h_AllViewPic where LSH= " + P_LSH);
                            //    StringBuilder sbs = new StringBuilder();

                            //    sbs.Append("<script>$(\"#div_" + P_LSH + "\").hide();alertMsg.correct(\"操作成功！\")</script>");
                            //    Response.Write(sbs.ToString());
                            //    Response.End();
                            //    break;

                            case "4": //删除录音
                                DbHelperSQL.ExecuteSql("update i_InternetPhone set recordUrlDel=0 where phoneID= " + P_LSH);
                                Log l = new Log();
                                l.Action = "删除";
                                l.Category = "拨号录音";
                                l.OccurTime = DateTime.Now;
                                l.Remark = "房源编号：" + P_HouseID + "录音编号：" + P_LSH;
                                l.Insert();
                                Response.Write("<script>alertMsg.correct(\"操作成功！\")</script>");
                                Response.End();
                                break;

                            case "5": //取消录音保密
                                h_RecordClose hr = h_RecordClose.Find(h_RecordClose._.phoneID, P_LSH);
                                if (hr != null)
                                {
                                    hr.IsCheck = false;
                                    hr.Update();

                                    Log ly = new Log();
                                    ly.Action = "修改";
                                    ly.Category = "取消录音保密";
                                    ly.OccurTime = DateTime.Now;
                                    ly.Remark = "房源编号：" + P_HouseID + "录音编号：" + P_LSH;
                                    ly.Insert();
                                }

                                Response.Write("<script>alertMsg.correct(\"操作成功！\")</script>");
                                Response.End();
                                break;

                            case "6": //删除全部照片
                                List<h_PicList> list_h_PicList = h_PicList.FindAllByHouseID(P_HouseID.ToDecimal().Value);
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

                            case "7": //取消预约摄影师
                                h_PicList_YY hpy = h_PicList_YY.Find(" photographer is null and Finishtime is null and EmployeeID = " + Current.EmployeeID + " and HouseID = " + Entity.HouseID);
                                if (hpy != null)
                                {
                                    hpy.Delete();

                                    //在跟进中添加记录
                                    h_FollowUp fup = new h_FollowUp();
                                    fup.HouseID = Convert.ToDecimal(P_HouseID);
                                    fup.FollowUpText = "此房源已被" + Current.Em_name + "取消预约";
                                    fup.EmployeeID = Current.EmployeeID;
                                    fup.exe_Date = DateTime.Now;
                                    fup.Insert();
                                }

                                Response.Write("<script>alertMsg.correct(\"操作成功！\")</script>");
                                Response.End();
                                break;

                            default: break;
                        }
                    }
                    else
                    {
                        int num3 = DbHelperSQL.GetSingle("select count(1) from h_PicList_YY where IsDel =1 and HouseID=" + Entity.HouseID).ToInt32().Value;
                        int num = DbHelperSQL.GetSingle("select count(1) from h_PicList_YY where HouseID=" + Entity.HouseID).ToInt32().Value;
                        if (num == 0 || num3 > 0)
                        {
                            //预约摄影师
                            h_PicList_YY yy = new h_PicList_YY();
                            yy.HouseID = int.Parse(P_HouseID);
                            yy.EmployeeID = int.Parse(Current.EmployeeID.ToString());
                            yy.Exe_date = DateTime.Now;
                            yy.Org = int.Parse(Current.OrgID.ToString());
                            yy.Insert();

                            //在跟进中添加记录
                            h_FollowUp fup = new h_FollowUp();
                            fup.HouseID = Convert.ToDecimal(P_HouseID);
                            fup.FollowUpText = "此房源已被" + Current.Em_name + "预约摄影师";
                            fup.EmployeeID = Current.EmployeeID;
                            fup.exe_Date = DateTime.Now;
                            fup.Insert();

                            StringBuilder sb1 = new StringBuilder();
                            Response.Write("<script>alertMsg.correct(\"预约成功！\")</script>");
                            Response.End();
                        }
                        else
                        {
                            StringBuilder sb1 = new StringBuilder();
                            Response.Write("<script>alertMsg.error(\"此房源已预约过！\")</script>");
                            Response.End();
                        }
                    }
                }

                #endregion 删除操作

                DataBinds();

                if (frmEntrust_Date.Text != "")
                {
                    frmEntrust_Date.Text = Convert.ToDateTime(frmEntrust_Date.Text).ToString("yyyy-MM-dd");
                }

                //string bTel = "";
                //if (CheckRolePermission("备案电话"))
                //{
                //    bTel = "selBackTelClick('" + Entity.HouseID.ToString() + "')";
                //}
            }

            int lookcount = DbHelperSQL.GetSingle(string.Format("select count(1) from h_SeeTelLog where EmployeeID={0} and SheBei in ('101','102') and CONVERT(DATE,exe_date,120)='" + DateTime.Now.ToString("yyyy-MM-dd") + "'", Employee.Current.EmployeeID)).ToInt32().Value;
            int count = DbHelperSQL.GetSingle(string.Format("select isnull(Max(Tabletnumber),0) as Tabletnumber from p_Role where RoleID in ({0})", Employee.Current.RoleIDs)).ToInt32().Value;
            if (lookcount >= count && count != 0)
            {
                canbut = "1";
            }

            seeNum.Text = "  查看次数(" + lookcount + "/" + count + ")";
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
                string s_LinkTel1 = "（1）该房屋在小区的位置（比如：沿河、沿马路、小区中心位置等）（2）所有房间的朝向（3）客厅属于明厅还是暗厅（4）装修的年数（5）房屋的明显优缺点（如全部是品牌家电，房屋有漏水、暗厕等。）（6）有阁楼、院子等须注明。（7）方便看房时间（8）房屋状态（空关、出租、自住等）";
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

                frmbID.Attributes.Add("onchange", "GetUnitItems(" + Entity.HouseID + ");");
                //this.frmUnitID.Attributes.Add("onchange", "GetRoomItems(" + Entity.HouseID + ");");
                frmRoomID.Attributes.Add("onchange", "GetDoorItems(" + Entity.HouseID + ");");
                frmBuild_area.Attributes.Add("onblur", "GetPrices('" + Entity.HouseID + "')");
                frmSum_price.Attributes.Add("onblur", "GetPrices('" + Entity.HouseID + "')");
                PriceFollowUp.Attributes.Add("href", "House/FollowUpPriceEditor.aspx?NavTabId=" + NavTabId + "&doAjax=true&HouseID=" + Entity.HouseID);
                if (Entity.HouseID > 0)
                {
                    //委托单号
                    orderNum_old.Value = Entity.orderNum.ToString();
                    //写跟进按钮显示
                    ligj.Visible = true;
                    if (Current.RoleNames.Contains("信息"))
                    {
                        //积分记录
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

                    //精耕
                    if (Entity.HouseDicType == "1")
                    {
                        frmbuild_room.CssClass = "Hinput frmbuild_room";
                        if (Entity.Bid.HasValue && Entity.Bid.Value > 0 && Entity.RoomID.HasValue && Entity.RoomID.Value > 0)
                        {
                            string sql = string.Format(@"select SeatID,SeatName
                                                        from s_Seat
                                                        where HouseDicID={0}", Entity.HouseDicID);
                            List<s_Seat> seatList = s_Seat.FindAll(sql);

                            frmbID.DataSource = seatList;
                            frmbID.DataTextField = "SeatName";
                            frmbID.DataValueField = "SeatID";
                            frmbID.DataBind();
                            frmbID.SelectedValue = Entity.Bid.Value.ToString();
                            frmbuild_id.Text = frmbID.SelectedItem.Text;

                            sql = string.Format(@"select DoorID,DoorNam
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
                                                    Entity.RoomID.Value);
                            List<s_Door> doorList = s_Door.FindAll(sql);

                            frmRoomID.DataSource = doorList;
                            frmRoomID.DataTextField = "DoorNam";
                            frmRoomID.DataValueField = "DoorID";
                            frmRoomID.DataBind();

                            frmRoomID.SelectedValue = Entity.RoomID.Value.ToString();
                            frmbuild_room.Text = frmRoomID.SelectedItem.Text;
                        }
                    }

                    if (Entity.IsPrivate)
                    {
                        frmlandlord_tel2.Text = "点击查看隐藏信息";//请从手机端拨打该号码
                    }
                    else
                    {
                        frmlandlord_tel2.Text = "点击查看隐藏信息";
                    }

                    //一般委托的才判断是否显示 申请开盘按钮
                    if (Entity.EntrustTypeID.HasValue && Entity.EntrustTypeID.Value == 1)
                    {
                        //申请开盘按钮 状态为委托中，并且总部认证状态 非合格
                        if (Entity.StateID == 2 && (!Entity.state_ZBCheck.HasValue || Entity.state_ZBCheck.Value != (int)CheckState.合格))
                        {
                            //查找此房源有没有 有录音待审核的记录，如果有，则无法申请开盘
                            //查找48小时内没有【申请】开盘记录，如果有，则无法申请开盘
                            //查找24小时内没有【驳回】开盘记录，如果有，则无法申请开盘
                            string sql = string.Format(@"IF EXISTS(SELECT 1 
                                                                      FROM   h_houseinfor_zbcheck 
                                                                      WHERE  ( phoneid IS NOT NULL 
                                                                                OR picurl IS NOT NULL ) 
                                                                             AND audit_date IS NULL 
                                                                             AND isdel = 0 
                                                                             AND houseid = {0}) 
                                                              SELECT 1 
                                                            ELSE IF EXISTS(SELECT 1 
                                                                      FROM   h_houseinfor_zbcheck 
                                                                      WHERE  ( phoneid IS NULL 
                                                                                OR picurl IS NULL ) 
                                                                             AND employee_auditid IS NULL 
                                                                             AND isdel = 0 
                                                                             AND Datediff(hh, exe_date, Getdate()) <= 48 
                                                                             AND houseid = {0}) 
                                                              SELECT 1 
                                                            ELSE IF EXISTS(SELECT 1 
                                                                      FROM   h_houseinfor_zbcheck 
                                                                      WHERE  state_zbcheck = 2 
                                                                             AND isdel = 0 
                                                                             AND Datediff(hh, exe_date, Getdate()) <= 24 
                                                                             AND houseid = {0}) 
                                                              SELECT 1 
                                                            ELSE 
                                                              SELECT 0 ",
                                                        Entity.HouseID);

                            if (DbHelperSQL.GetSingle(sql).ToInt32() == 0)
                            {
                                likp.Visible = true;
                                //征信分扣分20 分
                                //sql = string.Format(@"IF EXISTS(SELECT 1 
                                //                                FROM   empintegral
                                //                                WHERE  employeeid = {0}
                                //                                AND cause LIKE '%{1}%') 
                                //                        SELECT 1
                                //                    ELSE
                                //                        SELECT 0 ",
                                //                        Entity.OwnerEmployeeID,
                                //                        Entity.Shi_id);

                                //if (DbHelperSQL.GetSingle(sql).ToInt32() == 0)
                                //{
                                //    EntityUtils.EmpIntegral Empint = new EntityUtils.EmpIntegral();
                                //    Empint.EmployeeID = Entity.OwnerEmployeeID;
                                //    Empint.OrgID = Entity.OrgID;
                                //    Empint.Type = "其他";
                                //    Empint.Integral = 20;
                                //    Empint.Cause = "房源：" + Entity.Shi_id + "超过48小时未上传录音";
                                //    Empint.Exe_Date = DateTime.Now;
                                //    Empint.OperID = 2;
                                //    Empint.Insert();
                                //}
                            }
                        }
                    }

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

                    #endregion 暂时无用 2017-07-14 10:32:15
                }

                //if (!CheckRolePermission("不隐号"))
                //{
                //    //frmNotarizationNum.Text = "***";
                //    if (Entity.IsBeiAn.HasValue && Entity.IsBeiAn.Value)
                //    {
                //        frmbuild_id.Text = "***";
                //        //frmbuild_unit.Text = "***";
                //        frmbuild_room.Text = "***";
                //        frmbID.Items.Add(new ListItem("", "***"));
                //        //this.frmUnitID.Items.Add(new ListItem("", "***"));
                //        frmRoomID.Items.Add(new ListItem("", "***"));
                //    }
                //}

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
                if (Entity.Fast_UserID.HasValue)
                {
                    frmFast_UserID.Text = Entity.Fast_UserID.Value.ToString();

                    frmem_name.Text = Employee.FindByEmployeeID(Entity.Fast_UserID.Value.ToDecimal().Value).Em_name;
                }

                if (Entity.EntrustTypeID.HasValue &&
                    (Entity.EntrustTypeID.Value == 4 || Entity.EntrustTypeID.Value == 7))
                {
                    tblFast.Style.Add("display", "block");
                }
                //frmBackTel.Text = "***";
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
            //修改房源
            if (Entity.HouseID > 0)
            {
                //判断是否委托单号大于0，并且和原先的委托单号不同
                if (Entity.orderNum > 0 && Entity.orderNum != orderNum_old.Value.ToInt32())
                {
                    string sql = string.Format(@"IF EXISTS(SELECT 1 
                                                    FROM   h_houseinfor 
                                                    WHERE  atype = 0 
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
                //修改为限时房源时自定转入预约摄影师列表
                H_houseinfor hh1 = H_houseinfor.FindByHouseID(Entity.HouseID);
                if ((Entity.EntrustTypeID != hh1.EntrustTypeID) && (Entity.EntrustTypeID == 4 || Entity.EntrustTypeID == 6 || Entity.EntrustTypeID == 7))
                {
                    int num3 = DbHelperSQL.GetSingle("select count(1) from h_PicList_YY where IsDel =1 and HouseID=" + Entity.HouseID).ToInt32().Value;
                    int num = DbHelperSQL.GetSingle("select count(1) from h_PicList_YY where HouseID=" + Entity.HouseID).ToInt32().Value;
                    if (num == 0 || num3 > 0)
                    {
                        //预约摄影师
                        h_PicList_YY yy = new h_PicList_YY();
                        yy.HouseID = int.Parse(Entity.HouseID.ToString());
                        h_PicList hh = h_PicList.Find("HouseID", Entity.HouseID);
                        if (hh != null && hh.EmployeeID > 0)
                        {
                            yy.EmployeeID = hh.EmployeeID;
                        }
                        else
                        {
                            yy.EmployeeID = int.Parse(Current.EmployeeID.ToString());
                        }

                        yy.Exe_date = DateTime.Now;
                        yy.Org = int.Parse(Current.OrgID.ToString());
                        yy.Insert();

                        //在跟进中添加记录
                        h_FollowUp fup = new h_FollowUp();
                        fup.HouseID = Convert.ToDecimal(Entity.HouseID.ToString());
                        fup.FollowUpText = "此房源已被" + Current.Em_name + "预约摄影师";
                        fup.EmployeeID = Current.EmployeeID;
                        fup.exe_Date = DateTime.Now;
                        fup.Insert();
                    }
                }
            }
            else
            {
                //判断是否委托单号大于0，
                if (Entity.orderNum > 0)
                {
                    string sql = string.Format(@"IF EXISTS(SELECT 1 
                                                    FROM   h_houseinfor 
                                                    WHERE  atype = 0 
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

                Entity.state_ZBCheck = CheckState.待认证.ToInt16();
            }
            Entity.UnitID = Request.Form["frmUnitID"].ToInt32();
            Entity.RoomID = Request.Form["frmRoomID"].ToInt32();
            Entity.Rent_Price = Request.Form["frmRent_Price"].ToDecimal();

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

            //商圈ID
            Entity.SanjakID = Convert.ToInt32(Request.Form["ddlSanjakID"]);
            Entity.Facility = Request.Form["F.frmFacility"];

            //速销代卖责任人
            if (!Request.Form["h.frmBeltmanID"].IsNullOrWhiteSpace())
            {
                Entity.Fast_UserID = Convert.ToInt16(Request.Form["h.frmBeltmanID"]);
            }

            if (!string.IsNullOrEmpty(Request.Form["frmSum_price"]))
            {
                Entity.Sum_price = Convert.ToDecimal(Request.Form["frmSum_price"]);
            }
            if (!string.IsNullOrEmpty(Request.Form["frmBuild_area"]))
            {
                Entity.Build_area = Convert.ToDecimal(Request.Form["frmBuild_area"]);
            }
            if (Entity.Sum_price > 0 && Entity.Build_area.HasValue && Entity.Build_area.Value > 0)
            {
                Entity.Ohter2ID = ((Entity.Sum_price * 10000) / Entity.Build_area.Value).ToInt32();
            }
            else
            {
                Entity.Ohter2ID = Request.Form["Prices"].ToInt32();
            }

            string s_LinkTel1 = "（1）该房屋在小区的位置（比如：沿河、沿马路、小区中心位置等）（2）所有房间的朝向（3）客厅属于明厅还是暗厅（4）装修的年数（5）房屋的明显优缺点（如全部是品牌家电，房屋有漏水、暗厕等。）（6）有阁楼、院子等须注明。（7）方便看房时间（8）房屋状态（空关、出租、自住等）";
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

        #region 获取房源判重的参数

        [AjaxPro.AjaxMethod]
        public string GetRepeat()
        {
            s_SysParam ss = s_SysParam.FindByParamCode("HouseRepeat");
            return ss.Value;
        }

        #endregion 获取房源判重的参数

        #region 找到房源的锁盘数量,系统设置锁盘总量供前台JS 调用

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

        #endregion 找到房源的锁盘数量,系统设置锁盘总量供前台JS 调用

        #region 获取保密说明

        [AjaxPro.AjaxMethod]
        public string GetRemark(Decimal houseId)
        {
            H_houseinfor hh = H_houseinfor.FindByHouseID(houseId);
            return hh.Remarks;
        }

        #endregion 获取保密说明

        #region 获取可设置私盘数

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

        #endregion 获取可设置私盘数

        #region 获取备案号

        [AjaxPro.AjaxMethod]
        public string GetBackTel(Decimal houseId, string NavID)
        {
            if (!string.IsNullOrEmpty(MenuCode))
            {
                CurrentMenu = Menu.Find(Menu._.MenuCode, MenuCode);
                if (CurrentMenu != null)
                    MenuID = CurrentMenu.ID;
            }
            else
                MenuID = NavID.Split("_menu")[0].ToInt32();
            H_houseinfor hh = H_houseinfor.Find(GetRolePermissionEmployeeIds("备案电话", "OwnerEmployeeID") + " and HouseID=" + houseId.ToString());
            if (hh != null)
            {
                if (hh.BackTel.Length > 20)
                    return hh.BackTel.TelDecrypt(Convert.ToInt32(hh.HouseID), TelDecPoint.PC_HouseForm_ShowTel);
                else
                    return hh.BackTel;
            }
            else
            {
                return "您没有查看备案电话的权限";
            }
        }

        #endregion 获取备案号

        protected void DataBinds()
        {
            if (Request.QueryString["EditType"] != null)
            {
                h_gj.Visible = true;
                h_zp.Visible = true;
                h_cdjl.Visible = true;
                h_dk.Visible = true;
                h_dk_jf.Visible = true;
            }
            else
            {
                h_gj.Visible = false;
                h_zp.Visible = false;
                h_cdjl.Visible = false;
            }
        }

        #region 获取栋号单元室号详情   js/Function.js

        [AjaxPro.AjaxMethod]
        public string GetBuildItems(string HouseDicId)
        {
            string itemList = string.Empty;
            List<s_Seat> seatList = s_Seat.FindAll(string.Format(@"select SeatID,SeatName
                                                                    from s_Seat
                                                                    where HouseDicID={0}", HouseDicId));
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
                                                                                            and aType = 0
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
            List<s_Door> doorList = s_Door.FindAll(string.Format(@"select DoorID,DoorNam
                                                    from s_Door
                                                    where UnitID={0}
                                                    order by DoorNam", seatId));
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

        #endregion 获取栋号单元室号详情   js/Function.js

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

        [AjaxPro.AjaxMethod]
        public string IsUploadPic(int houseID)
        {
            string result = string.Empty;
            List<h_PicList> listh_PicList = h_PicList.FindAllByHouseID(houseID.ToDecimal().Value);
            if (listh_PicList.Count == 0)
            {
                result = "没有上传房源照片，请先上传房源照片";
            }
            else
            {
                //判断是否当前操作人上传的照片
                List<h_PicList> listTemp = listh_PicList.Where(x => x.EmployeeID == Employee.Current.EmployeeID).ToList();
                //没有当前上传人的照片
                if (listh_PicList.Count == 0)
                {
                    result = "你没有上传房源照片，无法修改";
                }
                //如果当前操作人上传的
                else if (listTemp.Count > 0 && listTemp.Count < 5)
                {
                    result = "你上传的房源照片不满5张，请先上传房源照片";
                }
                //判断是否上传户型图
                else
                {
                    listTemp = listTemp.Where(x => x.PicTypeID == 1).ToList();
                    if (listTemp.Count == 0)
                    {
                        result = "你没有上传房源户型图，无法修改";
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 判断房源核验积分项有没有修改
        /// </summary>
        /// <param name="houseID">房源编号</param>
        /// <param name="LinkTel1">房源描述</param>
        /// <param name="SeeHouseID">看房时间</param>
        /// <param name="NowStateID">房屋情况</param>
        /// <param name="TaxesID">税费</param>
        /// <param name="AssortID">产证情况</param>
        /// <param name="SaleMotiveID">光线情况</param>
        /// <param name="ApplianceID">外墙</param>
        /// <param name="PayServantID">带看人</param>
        /// <returns></returns>
        [AjaxPro.AjaxMethod]
        public bool CanSave(string houseID, string LinkTel1, string SeeHouseID, string NowStateID, string TaxesID, string AssortID, string SaleMotiveID, string ApplianceID, string PayServantID)
        {
            //判断房源ID是否为空
            if (!houseID.IsNullOrWhiteSpace())
            {
                if (LinkTel1.IsNullOrWhiteSpace() || SeeHouseID.IsNullOrWhiteSpace() || NowStateID.IsNullOrWhiteSpace() || TaxesID.IsNullOrWhiteSpace() || AssortID.IsNullOrWhiteSpace() || SaleMotiveID.IsNullOrWhiteSpace() || ApplianceID.IsNullOrWhiteSpace() || PayServantID.IsNullOrWhiteSpace())
                {
                    return true;
                }

                H_houseinfor model_H_houseinfor = H_houseinfor.FindByHouseID(houseID.ToDecimal().Value);

                if (LinkTel1 != model_H_houseinfor.LinkTel1 || SeeHouseID.ToDecimal() != model_H_houseinfor.SeeHouseID || NowStateID.ToDecimal() != model_H_houseinfor.NowStateID
                || TaxesID.ToDecimal() != model_H_houseinfor.TaxesID || AssortID.ToDecimal() != model_H_houseinfor.AssortID || SaleMotiveID.ToDecimal() != model_H_houseinfor.SaleMotiveID
                || ApplianceID.ToDecimal() != model_H_houseinfor.ApplianceID || PayServantID.ToDecimal() != model_H_houseinfor.PayServantID)
                {
                    return false;
                }
            }

            return true;
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

        protected string RelevantPersonnel()
        {
            string strInfo = string.Empty;
            if (Request.QueryString["HouseID"] != null)
            {
                strInfo = "房源相关人员：";
                //首录人
                Employee em = Employee.FindByEmployeeID(Entity.OwnerEmployeeID);
                strInfo += em.Em_name + ",";
                //照片核验人
                string sql = string.Format(@"select EmployeeID
                                        from h_PicList
                                        where PicTypeID=1
                                        and HouseID ={0}",
                                        Request.QueryString["HouseID"]);
                object hyempid = DbHelperSQL.GetSingle(sql);
                if (hyempid != null)
                {
                    em = Employee.FindByEmployeeID(int.Parse(hyempid.ToString()));
                    strInfo += em.Em_name + ",";
                }
                //钥匙方
                string sql1 = string.Format(@"select  top 1 EmployeeID
                                        from h_HouseKey
                                        where HouseID ={0}  order by exe_date desc",
                                        Request.QueryString["HouseID"]);
                object ysempid = DbHelperSQL.GetSingle(sql1);
                if (ysempid != null)
                {
                    em = Employee.FindByEmployeeID(int.Parse(ysempid.ToString()));
                    strInfo += em.Em_name + ",";
                }
                strInfo = strInfo.TrimEnd(',');
            }
            return strInfo;
        }
    }
}