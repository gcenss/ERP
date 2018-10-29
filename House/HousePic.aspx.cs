using System;

namespace HouseMIS.Web.House
{
    public partial class HousePic : System.Web.UI.Page
    {
        protected string HouseID = string.Empty;
        protected string PicTypeID = string.Empty;
        protected string EmployeeID = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["HouseID"] != null)
                {
                    HouseID = Request.QueryString["HouseID"].ToString();
                    PicTypeID = Request.QueryString["PicTypeID"].ToString();
                    EmployeeID = HouseMIS.EntityUtils.Employee.Current.EmployeeID.ToString();

                    if (!string.IsNullOrEmpty(Request.QueryString["EmployeeID"]) && Request.QueryString["EmployeeID"] != "0")
                    {
                        EmployeeID = Request.QueryString["EmployeeID"].ToString();
                      
                    }            
                }
            }
        }
    }
}