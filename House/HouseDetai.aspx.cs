using HouseMIS.EntityUtils;
using HouseMIS.Web.UI;
using System;
using System.Data;
using System.Web.UI;
using TCode;

namespace HouseMIS.Web.House
{
    public partial class HouseDetai : EntityFormBase<H_houseinfor>
    {
        public override string MenuCode
        {
            get
            {
                return "2001";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(HouseMIS.Web.House.HouseDetai), this.Page);
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["HouseID"] == null)
                {
                    Response.End();
                }
                else
                {
                    this.h_img_list.DataSource = h_PicList.Meta.Query("select h.PicURL,e.em_id,e.em_name,o.BillCode,o.Name,h.exe_date from h_PicList h left join e_Employee e on e.EmployeeID=h.EmployeeID left join s_Organise o on o.OrgID=e.OrgID where h.PicTypeID<>10 and h.HouseID=" + Entity.HouseID.ToString());
                    this.h_img_list.DataBind();

                    s_HouseDic sh = s_HouseDic.FindByKey(Entity.HouseDicID);
                    if (sh != null)
                    {
                        this.hf_label_pt.Text = sh.Environment + "<br>" + sh.Traffic + "<br>" + sh.Remarks;
                    }
                }
            }
        }

        //[AjaxPro.AjaxMethod]
        //public string ShowTel(Decimal houseId, string NavID)
        //{
        //    if (!string.IsNullOrEmpty(MenuCode))
        //    {
        //        CurrentMenu = Menu.Find(Menu._.MenuCode, MenuCode);
        //        if (CurrentMenu != null)
        //            MenuID = CurrentMenu.ID;
        //    }
        //    else
        //        this.MenuID = NavID.Split("_menu")[0].ToInt32();
        //    //s_SysParam ss = s_SysParam.FindByParamCode("TelLookAuthority");
        //    H_houseinfor hh = H_houseinfor.FindByHouseID(houseId);
        //    int sa = 0;
        //    //if (ss.Value != "")
        //    //{
        //    //    string[] strs = ss.Value.Split(',');
        //    //    foreach (string str in strs)
        //    //    {
        //    //        if (hh.StateID.ToString() == str)
        //    //        {
        //    //            sa = 1;
        //    //            break;
        //    //        }
        //    //    }
        //    //}
        //    if (sa == 1 && !CheckRolePermission("电话特殊查看权限"))
        //    {
        //        return "您没有该状态房源的查看权限！";
        //    }
        //    else
        //    {
        //        //s_SysParam ssp = s_SysParam.FindByParamCode("StateLook");
        //        int sap = 0;
        //        //if (ssp.Value != "")
        //        //{
        //        //    string[] strs = ssp.Value.Split(',');
        //        //    foreach (string str in strs)
        //        //    {
        //        //        if (hh.StateID.ToString() == str)
        //        //        {
        //        //            sap = 1;
        //        //            break;
        //        //        }
        //        //    }
        //        //}
        //        H_houseinfor house = null;
        //        if (hh.IsPrivate)
        //        {
        //            house = H_houseinfor.Find(GetRolePermissionEmployeeIds("查看私盘", "OwnerEmployeeID") + " and HouseID=" + houseId.ToString());
        //        }
        //        else
        //        {
        //            house = H_houseinfor.FindByHouseID(houseId);
        //        }
        //        string tel1 = "";
        //        string tel2 = "";
        //        if (house != null)
        //        {
        //            try
        //            {
        //                DataSet dt = H_houseinfor.Meta.Query("exec p_h_GetTel " + house.HouseID + "," + Employee.Current.EmployeeID.ToString() + "," + sap);
        //                if (dt.Tables.Count > 0)
        //                {
        //                    if (dt.Tables[0].Rows[0][0].ToString() != "")
        //                    {
        //                    }
        //                    if (dt.Tables[0].Rows[0][1].ToString() != "")
        //                    {
        //                    }
        //                    //楼栋|房号|单元号|房东电话|联系电话|备案电话
        //                    return tel1 + "&nbsp;&nbsp;" + tel2;
        //                }
        //                else
        //                {
        //                    return "不允许查看该状态房源！";
        //                }
        //            }
        //            catch (Exception e)
        //            {
        //                return e.ToString();
        //            }
        //        }
        //        else
        //        {
        //            return "您没有权限查看该私盘房源";
        //        }
        //    }
        //}

        //[AjaxPro.AjaxMethod]
        //public string CanGetTel(Decimal houseId)
        //{
        //    string result = "False";
        //    H_houseinfor hhs = H_houseinfor.FindByHouseID(houseId);
        //    //s_SysParam ssp = s_SysParam.FindByParamCode("StateLook");
        //    //if (ssp.Value != "")
        //    //{
        //    //    string[] strs = ssp.Value.Split(',');
        //    //    foreach (string str in strs)
        //    //    {
        //    //        if (hhs.StateID.ToString() == str)
        //    //        {
        //    //            result = "True";
        //    //            break;
        //    //        }
        //    //    }
        //    //}
        //    H_houseinfor hh = H_houseinfor.FindByHouseID(houseId);
        //    if (hh.OwnerEmployeeID == Employee.Current.EmployeeID || hh.OrgID == Employee.Current.OrgID)
        //    {
        //        result = "True";
        //    }
        //    if (result == "False")
        //    {
        //        if (H_houseinfor.Meta.Query("exec [dbo].[h_CanGetTel] " + houseId + " ,'" + Employee.Current.OrgCode + "'," + Employee.Current.EmployeeID).Tables[0].Rows[0][0].ToString() == "True")
        //        {
        //            result = "True";
        //        }
        //    }
        //    return result;
        //}
    }
}