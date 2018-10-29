using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HouseMIS.EntityUtils;
namespace HouseMIS.Web.House
{
    public partial class SchoolList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                rep1.DataSource = h_School.FindAll();
                rep1.DataBind();
            }
        }
    }
}