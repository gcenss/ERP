using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HouseMIS.Web.UI;
using System.Data;
using HouseMIS.EntityUtils.DBUtility;
using System.Text;
using HouseMIS.EntityUtils;

namespace HouseMIS.Web.House.Templete
{
    public partial class ShareHouseList : EntityListBase<Share_House>
    {
        //#region 分页参数
        //private int numPerPage;
        ///// <summary>
        ///// 每页显示的条数
        ///// </summary>
        //public int NumPerPage
        //{
        //    get
        //    {
        //        if (Request.Form["numPerPage"].Split(",").Length > 1)
        //        {
        //            return Convert.ToInt32(Request.Form["numPerPage"].Split(",")[0]);
        //        }
        //        int temp = Convert.ToInt32(Request.Form["numPerPage"]);

        //        return temp == 0 ? 15 : temp;

        //    }
        //    set { numPerPage = value; }
        //}

        //private int pageNumShown = 10;
        ///// <summary>
        ///// 页数导航的个数
        ///// </summary>
        //public int PageNumShown
        //{
        //    get { return pageNumShown; }
        //    set { pageNumShown = value; }
        //}

        //private int pageNum;
        ///// <summary>
        ///// 当前显示的页数
        ///// </summary>
        //public int PageNum
        //{
        //    get
        //    {
        //        int temp = Convert.ToInt32(Request.Form["pageNum"]);
        //        return temp == 0 ? 1 : temp;
        //    }
        //    set { pageNum = value; }
        //}

        //private int totalCount;
        ///// <summary>
        ///// 总条数
        ///// </summary>
        //public int TotalCount
        //{
        //    get { return totalCount; }
        //    set { totalCount = value; }
        //}

        //private int totalMax;

        //public int TotalMax
        //{
        //    get { return totalMax; }
        //    set { totalMax = value; }
        //}

        //private double totalPrice;

        //public double TotalPrice
        //{
        //    get { return totalPrice; }
        //    set { totalPrice = value; }
        //}

        //private string keywords;
        ///// <summary>   
        ///// where语句，不加where与空格
        ///// </summary>
        //public string Keywords
        //{
        //    get
        //    {
        //        if (Request.Form["keywords"] != null)
        //        {
        //            string temp = Request.Form["keywords"];
        //            return temp;
        //        }
        //        else
        //            return keywords;
        //    }
        //    set { keywords = value; }
        //}

        //private string orderField;
        ///// <summary>
        ///// 排序关键字
        ///// </summary>
        //public string OrderField
        //{
        //    get
        //    {
        //        string temp = Request.Form["orderField"];
        //        return temp;
        //    }
        //    set { orderField = value; }
        //}
        //#endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //this.ltlToolBar.Text = GetBtn();
                FullCanViewOrgShopDropList(this.ShareHouseList_OrgID, null);
                //string sql = @"SELECT  SUM(LookTotal) Look ,SUM(SearchTotal) Search ,SUM(TelTotal)+(select SUM(CallNum) from  e_CallNum) TelTotal FROM [Share_House]";
                //string where = GetWhereClauseFromSearchBar(string.Empty);
                //if (where.Length > 0)
                //    sql += " where";
                //sql += where;

                //DataTable table = DbHelperSQL.Query(sql).Tables[0];
                //this.ltlToolBar.Text += @" <div style='margin-top:5px ;font-size:14px ; font-weight:bold; color:Red;float:right; margin-right:30px'>
                //<a rel='ReportInforList' target='navtab' href='Report/ReportInforList.aspx?NavTabId=103_menu7001&amp;ID=75'  rid='75' style='font-size:14px ; font-weight:bold; color:Red;'> 
                //拨打电话 " + table.Rows[0]["TelTotal"] + " </a>  浏览总计 " + table.Rows[0]["Look"] + " 搜索点击总计 " + table.Rows[0]["Search"] + "</div>";
            }

            //this.rpt1.DataSource = BindList();
            //this.rpt1.DataBind();
        }

        //public static string ParseTags(string HTMLStr, int count)
        //{
        //    string s = System.Text.RegularExpressions.Regex.Replace(HTMLStr, "<[^>]*>", "");
        //    if (s.Length > count)
        //    {
        //        return s.Substring(0, count) + "......";
        //    }
        //    else
        //    {
        //        return s;
        //    }
        //}

        private string GetBtn()
        {
            Dictionary<string, string> str = new Dictionary<string, string>();
            str.Add("tj", "<li><a class=\"add\" href=\"House/Templete/ShareTempleteForm.aspx?NavTabId=" + NavTabId + "&doAjax=true\"  width=\"800\" height=\"650\" target=\"dialog\" maxable='false' title=\"添加故事\"><span>添加</span></a></li>");
            str.Add("xg", "<li><a class=\"edit\" href=\"House/Templete/ShareTempleteForm.aspx?NavTabId=" + NavTabId + "&doAjax=true&ID={id}\" width=\"800\" height=\"650\" target=\"dialog\" title=\"修改故事\" maxable='false'><span>修改</span></a></li>");
            str.Add("sc", "<li><a class=\"delete\" href=\"House/Templete/ShareTempleteForm.aspx?NavTabId=" + NavTabId + "&doAjax=true&doType=del&ID={id}\" target=\"dialog\" title=\"确定要删除吗?\"><span>删除</span></a></li>");

            if (!CheckRolePermission("添加"))
            {
                str.Remove("tj");
            }
            if (!CheckRolePermission("修改"))
            {
                str.Remove("xg");
            }
            if (!CheckRolePermission("删除"))
            {
                str.Remove("sc");
            }

            StringBuilder sb = new StringBuilder();
            foreach (string i in str.Values)
            {
                sb.Append(i);
            }

            return sb.ToString();
        }

//        private DataTable BindList()
//        {


//            string sql = @"select s.*,e.em_name,h.shi_id HouseBH from Share_House s
//                         left join e_Employee e on e.EmployeeID=s.EmployeeID
//                         left join h_houseinfor h on s.HouseID=h.HouseID" + getwhere() + " order by AddTime desc";
//            DataSet ds = DbHelperSQL.PageBySqlCount(sql, NumPerPage, PageNum);

//            TotalCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);           //统计数据条数
            
//            return ds.Tables[2];
//        }

        protected override string GetWhereClauseFromSearchBar(string typeName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.GetWhereClauseFromSearchBar(typeName));

            if (!Request.Form["ShareHouseList_OrgID"].IsNullOrWhiteSpace())
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat("OrgID={0} ", HouseMIS.EntityUtils.StringHelper.Filter(Request.Form["ShareHouseList_OrgID"]));
            }

            if (!string.IsNullOrEmpty(Request.Form["start"]))
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.Append(" AddTime >= '" + Request.Form["start"] + "'");
            }
            if (!string.IsNullOrEmpty(Request.Form["end"]))
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.Append(" AddTime <= '" + Request.Form["end"] + "'");
            }

            if (!string.IsNullOrEmpty(Request.Form["fsemployeeid"]))
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.Append(" EmployeeID in ( select EmployeeID from e_Employee where em_name='" + Request.Form["fsemployeeid"] + "') ");
            }
            if (!string.IsNullOrEmpty(Request.QueryString["sEmployeeID"]))
            {
                if (sb.Length > 0)
                    sb.Append(" and ");
                sb.Append(" EmployeeID ='" + Request.QueryString["sEmployeeID"] + "' and CONVERT(varchar(11), AddTime,21)=CONVERT(varchar(11),GETDATE(),21) ");
            }

            return sb.ToString();
                      
        }

        //private string getwhere()
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append(" where ");
        //    sb.Append(GetRolePermissionEmployeeIds("查看", "s.EmployeeID"));


        //    if (Request.QueryString["type"] == "jrms")
        //    {
        //        if (sb.Length > 0)
        //            sb.Append(" AND ");
        //        sb.Append(" s.EmployeeID='" + Employee.Current.EmployeeID + "' and AddTime > '" + DateTime.Now.ToString("yyyy-MM-dd") + "' ");
        //    }
        //    if (!Request.Form["ffrmTitle"].IsNullOrWhiteSpace())
        //    {
        //        if (sb.Length > 0)
        //            sb.Append(" AND ");
        //        sb.Append(" Title  like '%" + HouseMIS.EntityUtils.StringHelper.Filter(Request.Form["ffrmTitle"]) + "%'");
        //    }

        //    if (!string.IsNullOrEmpty(Request.Form["ffrmname"]))
        //    {
        //        if (sb.Length > 0)
        //            sb.Append(" AND ");
        //        sb.Append(" em_name like '%" + Request.Form["ffrmname"] + "%'");

        //    }
        //    if (!Request.Form["ShareHouseList_OrgID"].IsNullOrWhiteSpace())
        //    {
        //        if (sb.Length > 0)
        //            sb.Append(" AND ");
        //        sb.AppendFormat("s.OrgID={0} ", HouseMIS.EntityUtils.StringHelper.Filter(Request.Form["ShareHouseList_OrgID"]));
        //    }

        //    if (!string.IsNullOrEmpty(Request.Form["ffrmstart"]))
        //    {
        //        if (sb.Length > 0)
        //            sb.Append(" AND ");
        //        sb.Append(" AddTime >= '" + Request.Form["ffrmstart"] + "'");
        //    }
        //    if (!string.IsNullOrEmpty(Request.Form["ffrmend"]))
        //    {
        //        if (sb.Length > 0)
        //            sb.Append(" AND ");
        //        sb.Append(" AddTime <= '" + Request.Form["ffrmend"] + "'");
        //    }
        //    return sb.Length == 0 ? null : sb.ToString();
        //}
    }
}