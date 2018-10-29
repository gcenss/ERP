using System;
using System.Collections.Generic;
using AjaxPro;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HouseMIS.Web.UI;
using HouseMIS.EntityUtils;

namespace HouseMIS.Web.House
{
    public partial class AddHouseImage : EntityFormBase<h_PicList>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //decimal i = Entity.HouseID;
                //flv.InnerHtml = "HouseID=" + Entity.HouseID + "&url=http://localhost:11419/";
            }
        }

        /// <summary>
        /// 设置页面参数
        /// </summary>
        /// <returns></returns>
        [AjaxPro.AjaxMethod]
        public string setValues()
        {
            string a = "HouseID=" + Entity.HouseID + "&url=http://localhost:11419/";

            return a;
        }
    }
}