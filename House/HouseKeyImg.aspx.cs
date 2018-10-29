using System;
using HouseMIS.EntityUtils;
using HouseMIS.Common;

namespace HouseMIS.Web.House
{
    public partial class HouseKeyImg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }

        private void BindData()
        {
            if (Request.QueryString["HouseKeyID"] != null)
            {
                h_HouseKey entity = h_HouseKey.FindByHouseKeyID(Convert.ToDecimal(Request.QueryString["HouseKeyID"]));
                if (entity != null)
                {
                    imgUrl.ImageUrl = ImageHelper.GetUrl(entity.imgUrl);
                }
            }
        }
    }
}