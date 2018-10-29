using System;
using System.Collections.Generic;
using HouseMIS.Web.UI;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace HouseMIS.Web.House
{
    public partial class HouseFromXX : EntityListBase
    {
        public string HouseID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["HouseID"] != null)
            {
                HouseID = Request.QueryString["HouseID"];
            }
        }
    }
}