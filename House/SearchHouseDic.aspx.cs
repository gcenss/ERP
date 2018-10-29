using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TCode;
using HouseMIS.EntityUtils;
using HouseMIS.Web.UI;
using System.Text;
using System.Data;

namespace HouseMIS.Web.House
{
    public partial class SearchhHouseDic : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string keyword = "";
            keyword = string.IsNullOrEmpty(Request.QueryString["key"]) ? string.Empty : HouseMIS.EntityUtils.StringHelper.Filter(Request.QueryString["key"]);

            StringBuilder sb = new StringBuilder();
            //DataSet ds = s_HouseDic.Meta
            //string[] TestHouseDic = new string[] { "名门世家", "世纪风情", "正荣大湖之都", "万科四季花城", "万达星城", "奥林匹克花园", "恒茂红谷新城", "九里象湖城", "联泰香域尚城", "城开国际学园" };
            //sb.Append("[");
            //for (int i = 1; i <= TestHouseDic.Length; i++)
            //{
            //    //sb.Append("{\"id\":\"18\",\"data\":\"Angelina Jolie\",\"thumbnail\":\"thumbnails/jolie.jpg\",\"description\":\"Actress\"}");
            //    sb.Append("{\"id\":\"" + i + "\",\"data\":\"" + nohtml(TestHouseDic[i - 1]) + "\",\"thumbnail\":\"thumbnails/jolie.jpg\",\"description\":\"" + nohtml(TestHouseDic[i - 1]) + "\"}");

            //    if (i < TestHouseDic.Length)
            //        sb.Append(",");
            //}
            //sb.Append("]");
            //Response.Write(sb.ToString());
            //Response.End();

        }

        protected string nohtml(string str)
        {
            //str = FilterHTML(str);
            str = Microsoft.JScript.GlobalObject.escape(str);
            return str;
        }
    }
}