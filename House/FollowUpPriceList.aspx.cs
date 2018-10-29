using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TFrameWork.Log;
using TFrameWork.Web;
using TCode;

using HouseMIS.EntityUtils;
using HouseMIS.Web.UI;

namespace HouseMIS.Web.House
{
    public partial class FollowUpPriceList : EntityListBase<h_PriceFollowUp>
    {
        protected override void OnPreInit(EventArgs e)
        {
            ods.SelectParameters["whereClause"].DefaultValue = "HouseID=" + Request["HouseID"].ToInt32();
            base.OnPreInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }        
    }
}