using System;
using HouseMIS.EntityUtils;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;

namespace HouseMIS.Web.House
{
    public partial class HouseFromCD : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["HouseID"] != null)
                {
                    List<h_SeeTelLog> entity = h_SeeTelLog.FindAll("HouseID=" + Request.QueryString["HouseID"], "exe_Date desc", "", 0, 100);

                    //调电
                    gv.DataSource = entity.Where(x => x.SheBei == 1).ToList();
                    gv.DataBind();

                    //调门牌
                    gv_pai.DataSource = entity.Where(x => x.SheBei > 1).ToList();
                    gv_pai.DataBind();

                    //拨打记录
                    gv1.DataSource = h_SeeTelLog.Meta.Query(string.Format(@"select CONVERT(varchar,createTime,120) as a,C.Name+'('+B.em_id+'-'+B.em_name+')' as c 
                                                                    from i_InternetPhone A 
                                                                    left join e_Employee B on A.employeeID=B.EmployeeID 
                                                                    left join s_Organise C on B.OrgID=C.OrgID 
                                                                    where RecrodType=1 
                                                                    and houseID={0}
                                                                    order by createTime desc",
                                                                    Request.QueryString["HouseID"]));
                    gv1.DataBind();

                    if (gv.Rows.Count > 0)
                    {
                        gv.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                    if (gv_pai.Rows.Count > 0)
                    {
                        gv_pai.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                    if (gv1.Rows.Count > 0)
                    {
                        gv1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                }
            }
        }
    }
}