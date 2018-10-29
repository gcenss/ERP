using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HouseMIS.EntityUtils;
using TCode;
using HouseMIS.EntityUtils.DBUtility;
using TCode.DataAccessLayer;

namespace HouseMIS.Web.House
{
    public partial class HousePicExtS : HouseMIS.Web.UI.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            e_Alteration alter = e_Alteration.Find("EmployeeID=" + Employee.Current.EmployeeID);

            if (!IsPostBack)
            {
                //if (Request["shi_addr"] != null)
                //{
                //    H_houseinfor hh = H_houseinfor.FindByHouseID(Request["HouseID"].ToDecimal().Value);
                //    string PingGuJia = hh.PingGuJia.ToString();

                //    if (PingGuJia != Request.Form["shi_addr"])
                //    {
                //        if (Convert.ToDecimal(Request["shi_addr"]) > 0)
                //            s_HouseDicFloorPrice.SetFloorPrice(hh.HouseDicID, hh.Build_id, hh.Build_floor, Request["shi_addr"].ToDecimal().Value, hh.HouseID, true);
                //    }
                //}

                if (Request["hid"] != null && Request["uid"] != null)
                {
                    int iH = Convert.ToInt32(Request["hid"]), iU = Convert.ToInt32(Request["uid"]);
                    SelectBuilder sb = new SelectBuilder();
                    sb.Table = "h_SeeTelLog";
                    sb.Where = string.Format("EmployeeID={0} and HouseID={1} and SheBei in ('101','102') and CONVERT(DATE,exe_date,120)='" + DateTime.Now.ToString("yyyy-MM-dd") + "'", iU, iH);
                    //当日查看相同房源不算次数
                    if (h_SeeTelLog.Meta.QueryCount(sb) == 0)
                    {
                        H_houseinfor hou = H_houseinfor.FindByHouseID(iH);
                        //出售房源
                        if (hou.aType == 0)
                            H_houseinfor.Meta.Execute(string.Format("insert into h_SeeTelLog(HouseID,EmployeeID,SheBei,ComID) values({0},{1},101,{2})", iH, iU, Employee.Current.ComID));
                        else
                            H_houseinfor.Meta.Execute(string.Format("insert into h_SeeTelLog(HouseID,EmployeeID,SheBei,ComID) values({0},{1},103,{2})", iH, iU, Employee.Current.ComID));

                        //查看门牌的次数
                        int lookcount = int.Parse(DbHelperSQL.GetSingle(string.Format("select count(1) from h_SeeTelLog where EmployeeID={0} and SheBei in ('101','102') and CONVERT(DATE,exe_date,120)='" + DateTime.Now.ToString("yyyy-MM-dd") + "'", iU)).ToString());
                        //角色设置的次数
                        int count = int.Parse(DbHelperSQL.GetSingle(string.Format("select Max(Tabletnumber) as Tabletnumber from p_Role where RoleID in ({0})", Employee.Current.RoleIDs)).ToString());

                        if (lookcount > count && count != 0)
                        {
                            //alter.StateID = 5;
                            alter.Remarks = DateTime.Now.ToString("yyyy-MM-dd") + "日,查看出售房源门牌号超出角色限制" + count + "次";
                            alter.exe_Date = DateTime.Now;
                            alter.Update();

                            Response.Write(1);
                            Response.End();
                        }
                        else if (lookcount == count && count != 0)
                        {
                            Response.Write(2);
                        }
                    }
                }
            }
        }
    }
}