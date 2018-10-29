using System;
using System.Collections.Generic;
using Menu = HouseMIS.EntityUtils.Menu;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HouseMIS.Web.UI;
using HouseMIS.EntityUtils;
using TCode;
using System.Data;
using HouseMIS.EntityUtils.DBUtility;

namespace HouseMIS.Web.House
{
    public partial class HouseBhReasonAdd : HouseMIS.Web.UI.EntityFormBase<h_FollowUp>
    {
        string HouseID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Key.Value = Request["HouseID"];
            }
        }

        /// <summary>
        /// 表单提交前执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnSaving(object sender, EntityFormEventArgs e)
        {
            //base.OnSaving(sender, e);
          
                if (Request.Form["Key"] != null)
                {
                    HouseID = Request["Key"];

                    Entity.HouseID = Convert.ToDecimal(HouseID);
                    Entity.EmployeeID = Employee.Current.EmployeeID;
                    Entity.FollowUpText = "此房源照片及描述被信息部驳回,原因为:"+ frmRemark.Text;
                    Entity.exe_Date = DateTime.Now;

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

                    DbHelperSQL.ExecuteSql("delete from h_PicList where HouseID= " + HouseID);
                    H_houseinfor hh = H_houseinfor.FindByHouseID(Convert.ToDecimal(HouseID));
                    hh.HasImage = false;
                    hh.IsSh = null;
                    hh.LinkTel1 = null;
                    hh.SeeHouseID = null;
                    hh.NowStateID = null;
                    hh.TaxesID = null;
                    hh.AssortID = null;
                    hh.SaleMotiveID = null;
                    hh.ApplianceID = null;
                    hh.PayServantID = null;
                    hh.Update();

                    Log log1 = new Log();
                    log1.Action = "删除";
                    log1.Category = "删除房源照片";
                    log1.IP = HttpContext.Current.Request.UserHostAddress;
                    log1.OccurTime = DateTime.Now;
                    log1.UserID = Convert.ToInt32(Employee.Current.EmployeeID);
                    log1.UserName = Employee.Current.Em_name;
                    log1.Remark = "房源ID=" + HouseID;
                    log1.Insert();

                  try
                    {
                        h_FollowUp.Meta.BeginTrans();
                        Entity.Insert();
                        H_houseinfor.Meta.Execute("update h_houseinfor set IsBh=1 where HouseID=" + Entity.HouseID.ToString());
                        h_FollowUp.Meta.Commit();
                       
                        
                    }
                    catch
                    {
                        h_FollowUp.Meta.Rollback();
                        Response.Write(error());
                        e.Cancel = true;
                        return;
                    }
                }
  
        }

        public string success(DateTime notRemindSzTime, string notRemindSzId)
        {
            return "{\"statusCode\":\"200\", \"message\":\"操作成功!\", \"notRemindSzTime\":\"" + notRemindSzTime + "\", \"notRemindSzId\":\"" + notRemindSzId + "\"}";
        }

        public string error()
        {
            return "{\"statusCode\":\"300\", \"message\":\"操作失败!\"}";
        }
    }
}