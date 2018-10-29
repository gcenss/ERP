using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HouseMIS.EntityUtils;
using HouseMIS.EntityUtils.DBUtility;

namespace HouseMIS.Web.House
{
    public partial class HouseFromGJ : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["HouseID"] != null)
                {
                    string sql = string.Format(@"SELECT b.Name AS FollowUpType,
                                                         a.FollowUpText ,d.OrgID,
                                                         d.BillCode + d.Name + '-' + c.em_name AS EmployeeName , a.exe_Date
                                                FROM h_FollowUp a
                                                LEFT JOIN h_FollowUpType b
                                                    ON a.FollowUpTypeID = b.FollowUpTypeID
                                                INNER JOIN e_Employee c
                                                    ON a.EmployeeID = c.EmployeeID
                                                INNER JOIN s_Organise d
                                                    ON c.OrgID = d.OrgID
                                                WHERE a.HouseID = {0}
                                                ORDER BY exe_Date desc",
                                                Request.QueryString["HouseID"]);
                    this.FU.DataSource = DbHelperSQL.Query(sql);
                    this.FU.DataBind();
                }
            }
        }
    }
}