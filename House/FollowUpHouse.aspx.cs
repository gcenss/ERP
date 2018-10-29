using System;
using System.Collections.Generic;
using HouseMIS.Web.UI;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HouseMIS.EntityUtils;
using TCode;
using System.Text;
namespace HouseMIS.Web.House
{
    public partial class FollowUpHouse : EntityFormBase<H_houseinfor>
    {
        protected StringBuilder LookTelBut = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["HouseID"] != null)
            {
                H_houseinfor hh = H_houseinfor.FindByHouseID(Convert.ToDecimal(Request.QueryString["HouseID"]));
                //PapersID.SelectedValue = hh.PapersID.ToString();
                //frmPayServantID.SelectedValue = hh.PayServantID.ToString();
                //frmSeeHouseID.SelectedValue = hh.SeeHouseID.ToString();
                //frmSaleMotiveID.SelectedValue = hh.SaleMotiveID.ToString();
                //frmNowStateID.SelectedValue = hh.NowStateID.ToString();
                ////PayTypeID.SelectedValue = hh.PayTypeID.ToString();
                //frmApplianceID.SelectedValue = hh.ApplianceID.ToString();
                //frmAssortID.SelectedValue = hh.AssortID.ToString();
                shi_id.Text = hh.Shi_id;
                
                HouseID.Value = hh.HouseID.ToString();

         

                //if (CheckRolePermission("修改", hh.OwnerEmployeeID))
                //{
                //    if (h_State.FindByKey(hh.StateID).Name != "我售")
                //    {
                //        LookTelBut.Append("<li><div class=\"buttonActive\"><div class=\"buttonContent\"><button type=\"submit\">保存</button></div></div></li>");

                //    }
                //    else
                //    {

                //    }
                //}
                //else
                //{
                //    if (hh.OperatorID == Employee.Current.EmployeeID || hh.OwnerEmployeeID == Employee.Current.EmployeeID)
                //    {
                //        LookTelBut.Append("<li><div class=\"buttonActive\"><div class=\"buttonContent\"><button type=\"submit\">保存</button></div></div></li>");
                //    }
                //    else if (h_SeeTelLog.FindCount(new string[] { "HouseID", "EmployeeID" }, new string[] { hh.HouseID.ToString2(), Employee.Current.EmployeeID.ToString2() }) > 0)
                //    {
                //        LookTelBut.Append("<li><div class=\"buttonActive\"><div class=\"buttonContent\"><button type=\"submit\">保存</button></div></div></li>");
                //    }
                //    else if (i_InternetPhone.FindCount(new string[] { "houseID", "employeeID" }, new string[] { hh.HouseID.ToString2(), Employee.Current.EmployeeID.ToString2() }) > 0)
                //    {
                //        LookTelBut.Append("<li><div class=\"buttonActive\"><div class=\"buttonContent\"><button type=\"submit\">保存</button></div></div></li>");
                //    }
                //}
      
            }
            //else if (Request.Form["HouseID"] != null)
            //{
            //    H_houseinfor hh = H_houseinfor.FindByHouseID(Convert.ToDecimal(Request.Form["HouseID"]));
            //    hh.SeeHouseID = Request.Form["SeeHouseID"].ToDecimal();
            //    hh.NowStateID = Request.Form["NowStateID"].ToDecimal();
            //    hh.PayServantID = Request.Form["PayServantID"].ToDecimal();
            //    hh.AssortID = Request.Form["AssortID"].ToDecimal();
            //    hh.SaleMotiveID = Request.Form["SaleMotiveID"].ToDecimal();
            //    hh.ApplianceID = Request.Form["ApplianceID"].ToDecimal();
            //    hh.CurrentEmployee = Employee.Current.EmployeeID;
            //    hh.Update();
            //    Response.Write("{\"statusCode\":\"200\", \"message\":\"操作成功!\"}");
            //    Response.End();

            //}

        }

        private void UpdateIntegral(String HouseID, String FText, DateTime exe_Date)
        {
            #region 增加积分操作
            foreach (String s in FText.ToString().Split(','))
            {
                if (s.Split(':').Length > 1)
                {
                    String type = s.Split(':')[0];
                    if (s.Split(':')[1].Split('→').Length > 1 && s.Split(':')[1].Split('→')[0] != s.Split(':')[1].Split('→')[1])
                    {
                        String typeValue = s.Split(':')[1].Split('→')[1];
                        String RemarkTemplate = String.Empty;
                        switch (type)
                        {
                            case "看房":
                                RemarkTemplate = "更正看房时间";
                                break;
                            case "现状":
                                RemarkTemplate = "更正房源现状";
                                break;
                            case "钥匙情况":
                                RemarkTemplate = "更正房源钥匙";
                                break;

                            case "租金情况":
                                RemarkTemplate = "更正房源租金";
                                break;

                            case "产权人":
                                RemarkTemplate = "更正房源产权人";
                                break;

                            case "带看人":
                                RemarkTemplate = "更正房源带看人";
                                break;

                            case "产证情况":
                                RemarkTemplate = "更正房源产证情况";
                                break;

                            case "光线情况":
                                RemarkTemplate = "更正房源光线情况";
                                break;

                            case "外墙":
                                RemarkTemplate = "更正房源外墙";
                                break;

                            case "产证地址":
                                RemarkTemplate = "更正房源产证地址";
                                break;
                        }

                        if (!RemarkTemplate.IsNullOrWhiteSpace())
                        {
                            UpdateIntegral(RemarkTemplate, exe_Date, "H_houseinfor", "HouseID", HouseID);
                        }
                    }
                }
            }
            #endregion
        }
        protected override void OnSaving(object sender, EntityFormEventArgs e)
        {
            Entity.CurrentEmployee = Employee.Current.EmployeeID;
            base.OnSaving(sender, e);
        }

        protected override void OnInitComplete(EventArgs e)
        {
            base.OnInitComplete(e);
            //PubFunction.FullDropListData(typeof(h_Papers), PapersID, "Name", "PapersID", "");
            PubFunction.FullDropListData(typeof(h_PayServant), frmPayServantID, "Name", "PayServantID", "");
            PubFunction.FullDropListData(typeof(h_SeeHouse), frmSeeHouseID, "Name", "SeeHouseID", "");
            PubFunction.FullDropListData(typeof(h_Cause), frmSaleMotiveID, "Name", "SaleMotiveID", "");
            PubFunction.FullDropListData(typeof(h_NowState), frmNowStateID, "Name", "NowStateID", "");
            //PubFunction.FullDropListData(typeof(h_Business), BusinessID, "Name", "BusinessID", "");
            //PubFunction.FullDropListData(typeof(h_Ohter1), PayTypeID, "Name", "Ohter1ID", "");
            PubFunction.FullDropListData(typeof(h_Appliance), frmApplianceID, "Name", "ApplianceID", "");
            PubFunction.FullDropListData(typeof(h_Assort), frmAssortID, "Name", "AssortID", "");
            PubFunction.FullDropListData(typeof(h_EntrustType), frmEntrustTypeID, "Name", "EntrustTypeID", "");
        }
    }
}