using System;
using HouseMIS.Web.UI;
using HouseMIS.EntityUtils;
using System.Data;
using HouseMIS.Common;

namespace HouseMIS.Web.House
{
    public partial class HouseTimeLimitImg_Bring : EntityListBase<h_HouseTimeLimitMsg>
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            AjaxPro.Utility.RegisterTypeForAjax(typeof(HouseMIS.Web.House.HouseTimeLimitImg_Bring), this.Page);
            if (!IsPostBack)
            {
                string ID = Request.QueryString["ID"];
                bid.Value = ID;

                DataTable dt = c_BringCustomer.Meta.Query("SELECT ImgPic FROM h_HouseTimeLimitMsg WHERE  ID = " + ID).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["ImgPic"].ToString().ToLower().Trim('/').StartsWith(ImageHelper.dirImages))
                        img.Src = ImageHelper.GetUrl(dt.Rows[0]["ImgPic"].ToString());
                    else
                    {
                        if (dt.Rows[0]["ImgPic"].ToString().ToLower().IndexOf("ImgPic") == -1)
                            img.Src = ImageHelper.GetUrl("/uploadfiles/customerpic/" + dt.Rows[0]["ImgPic"].ToString());
                        else
                            img.Src = ImageHelper.GetUrl(dt.Rows[0]["ImgPic"].ToString());
                    }
                }
            }
        }

    }
}