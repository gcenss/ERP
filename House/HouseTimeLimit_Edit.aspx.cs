using System;
using HouseMIS.EntityUtils;
using HouseMIS.Web.UI;
using System.Data;
using TCode;
namespace HouseMIS.Web.House
{
    public partial class HouseTimeLimit_Edit : EntityFormBase<h_HouseTimeLimitMsg>
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                NavTabId.Value = Request.QueryString["NavTabId"];
                ffrmHouseState.SelectedValue = "7";
            }
        }

        protected override void OnSetForm(object sender, EntityFormEventArgs e)
        {
            base.OnSetForm(sender, e);
            string HouseID = Request.QueryString["HouseID"];
            Hid.Value = HouseID; 
        }

        protected override void OnSaving(object sender, EntityFormEventArgs e)
        {
            if (Request.Form["frmImgPic"] != "")
            {
                if (Request.Form["Hid"] != null)
                {
                    decimal cid = Convert.ToDecimal(Request.Form["C.CustomerID"]);
                    int hid = Convert.ToInt32(Request.Form["Hid"]);
                    H_houseinfor h = H_houseinfor.FindByHouseID(hid);
                    if (h != null)
                    {
                                //带看凭证
                                Entity.ImgPic = Request.Form["frmImgPic"];
                                //操作人
                                Entity.EmployeeID = Employee.Current.EmployeeID;
                                //状态、
                                Entity.HouseState = int.Parse(ffrmHouseState.SelectedValue);
                                //房源ID
                                Entity.HouseID = hid;
                                //责任人
                                Entity.ZRempID = Request.Form["f.frmEscortEmployeeID1"].ToDecimal();
                                //时间
                                Entity.Exe_date = DateTime.Now;
                                Entity.Update();

                                //房源状态改为选择的状态
                                h.EntrustTypeID = Entity.HouseState;
                                h.Update();
                    }
                    else
                    {
                        Response.Write("<script>alertMsg.error('参数错误，请重新操作')</script>");
                        //取消执行
                        e.Cancel = true;
                        return;
                    }
                }
                else
                {
                    Response.Write("<script>alertMsg.error('参数错误，请重新操作')</script>");
                    //取消执行
                    e.Cancel = true;
                    return;
                }
            }
            else
            {
                Response.Write("<script>alertMsg.error('操作失败！请上传带看凭证')</script>");
                //取消执行
                e.Cancel = true;
                return;
            }
        }
    }
}