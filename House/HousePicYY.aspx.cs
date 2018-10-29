
using System;
using System.Web.UI.WebControls;
using HouseMIS.EntityUtils;
using HouseMIS.Web.UI;
using System.Data;
using HouseMIS.Common;
using HouseMIS.EntityUtils.DBUtility;
using Menu = HouseMIS.EntityUtils.Menu;
using System.Web;
using System.Text;
using System.Collections.Generic;
using TCode;

namespace HouseMIS.Web.House
{
    public partial class HousePicYY : EntityListBase<h_PicList>
    {
        public override string MenuCode
        {
            get
            {
                return "2001";
            }

        }
        public string HouseID = "";
        public string EmployeeID = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            string P_LSH = Request.QueryString["LSH"];
            HouseID = Request.QueryString["HouseID"];
            HouseMIS.EntityUtils.h_PicList_YY hp1 = h_PicList_YY.Find("HouseID", HouseID.ToDecimal().Value);
            if (hp1!=null)
            {
                EmployeeID = hp1.EmployeeID.ToString();
            }
            else
                EmployeeID = "0";

            //EmployeeID
            if (HouseID != null)
            {
                //全部删除按钮

                HouseID = Request.QueryString["HouseID"];

               

                //hx_imgs.DataSource = DbHelperSQL.Query(string.Format(@"select h.PicURL,e.em_id,e.em_name,o.BillCode,o.Name,h.exe_date,h.LSH,h.EmployeeID
                //                                                            from h_PicList h 
                //                                                            left join e_Employee e on e.EmployeeID=h.EmployeeID 
                //                                                            left join s_Organise o on o.OrgID=h.OrgID 
                //                                                            where h.HouseID={0} 
                //                                                            and h.PicTypeID=1",
                //                                                            HouseID));
                //hx_imgs.DataBind();

                DataSet hpt = DbHelperSQL.Query(@"select Name,PicTypeID 
                                                    from h_PicType 
                                                    order by PicIndex");
                h_zp_list.DataSource = hpt;
                h_zp_list.DataBind();
                h_img_list.DataSource = hpt;
                h_img_list.DataBind();

                if (Current.RoleNames.Contains("信息"))
                {

                    picDel.Visible = true;
                    rptDel.Visible = true;

                    DataSet dsrpt = DbHelperSQL.Query(string.Format(@"SELECT b.LSH,
                                                                               b.PicURL,
                                                                               c.Name,
                                                                               d.em_name                              AS 上传人,
                                                                               ss.Name                                AS 上传人门店,
                                                                               b.exe_date                             AS 上传时间,
                                                                               e.em_name                              AS 删除人,
                                                                               b.DelDate                              AS 删除时间
                                                                        FROM   h_PicListDel b
                                                                               INNER JOIN h_PicType c
                                                                                       ON b.PicTypeID = c.PicTypeID
                                                                               LEFT JOIN e_Employee d
                                                                                      ON b.EmployeeID = d.EmployeeID
                                                                               LEFT JOIN e_Employee e
                                                                                      ON b.DelEmployeeID = e.EmployeeID
                                                                               LEFT JOIN s_Organise ss
                                                                                      ON b.orgid = ss.OrgID
                                                                        WHERE  b.HouseID = {0}",
                                                                        HouseID));
                    rptDel.DataSource = dsrpt;
                    rptDel.DataBind();
                }
            }
        }
        protected void hil_imgs_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HyperLink rep = (HyperLink)e.Item.FindControl("dellink");
                DataRowView hp = (DataRowView)e.Item.DataItem;
                if (!CheckRolePermission("删除照片", Convert.ToDecimal(hp["EmployeeID"])))
                {
                    rep.Visible = false;
                }
                else
                {
                    rep.NavigateUrl = rep.NavigateUrl + "?OperType=1&doAjax=true&LSH=" + hp["LSH"].ToString() + "&NavTabId=" + NavTabId + "&HouseID=" + HouseID;
                }
            }
        }
        protected void h_img_list_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rep = (Repeater)e.Item.FindControl("hil_imgs");
                DataRowView hp = (DataRowView)e.Item.DataItem;

                rep.DataSource = h_PicList.Meta.Query(string.Format(@"select h.PicURL,e.em_id,e.em_name,o.BillCode,o.Name,h.exe_date,h.LSH,h.EmployeeID 
                                                                        from h_PicList h 
                                                                        left join e_Employee e on e.EmployeeID=h.EmployeeID 
                                                                        left join s_Organise o on o.OrgID=e.OrgID 
                                                                        where h.HouseID={0}
                                                                        and h.PicTypeID={1}",
                                                                        HouseID,
                                                                        hp["PicTypeID"]));
                rep.DataBind();
            }
        }
        protected void hx_imgs_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HyperLink rep = (HyperLink)e.Item.FindControl("dellinks");
                DataRowView hp = (DataRowView)e.Item.DataItem;
                if (!CheckRolePermission("删除照片", Convert.ToDecimal(hp["EmployeeID"])))
                {
                    rep.Visible = false;
                }
                else
                {
                    rep.NavigateUrl = rep.NavigateUrl + "?OperType=1&doAjax=true&LSH=" + hp["LSH"].ToString() + "&NavTabId=" + NavTabId + "&HouseID=" + HouseID;
                }
            }
        }
        protected string GetUrl(object path)
        {
            return ImageHelper.GetUrl(path.ToString());
        }
    }
}