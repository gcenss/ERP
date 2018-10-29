using System;
using System.Collections.Generic;
using HouseMIS.EntityUtils;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HouseMIS.Web.House
{
    public partial class MetroList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                rep1.DataSource = h_Metro.FindAll();
                rep1.DataBind();
            }
        }
    }
}