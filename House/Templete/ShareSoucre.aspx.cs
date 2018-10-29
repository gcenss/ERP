using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HouseMIS.Web.UI;
using HouseMIS.EntityUtils;
using System.Text;

namespace HouseMIS.Web.House.Templete
{
    public partial class ShareSoucre : EntityListBase<Share_source>
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected override string GetWhereClauseFromSearchBar(string typeName)
        {
            StringBuilder str = new StringBuilder();
            str.Append(base.GetWhereClauseFromSearchBar(typeName));
            if (!string.IsNullOrEmpty(Request.QueryString["houseid"]))
            {
                if (str.Length > 0)
                    str.Append(" and ");
                str.Append("ShareHouseID = " + Request.QueryString["houseid"] + "");
            }
            return str.ToString();
        }
    }
}