using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HouseMIS.Web.UI;
using HouseMIS.EntityUtils;
using System.Text;
using System.Data;
using HouseMIS.EntityUtils.DBUtility;

namespace HouseMIS.Web.House.Templete
{
    public partial class ShareFindHouse : BasePage
    {
        #region 分页参数
        private int numPerPage;
        /// <summary>
        /// 每页显示的条数
        /// </summary>
        public int NumPerPage
        {
            get
            {
                if (Request.Form["numPerPage"].Split(",").Length > 1)
                {
                    return Convert.ToInt32(Request.Form["numPerPage"].Split(",")[0]);
                }
                int temp = Convert.ToInt32(Request.Form["numPerPage"]);

                return temp == 0 ? 15 : temp;

            }
            set { numPerPage = value; }
        }

        private int pageNumShown = 10;
        /// <summary>
        /// 页数导航的个数
        /// </summary>
        public int PageNumShown
        {
            get { return pageNumShown; }
            set { pageNumShown = value; }
        }

        private int pageNum;
        /// <summary>
        /// 当前显示的页数
        /// </summary>
        public int PageNum
        {
            get
            {
                int temp = Convert.ToInt32(Request.Form["pageNum"]);
                return temp == 0 ? 1 : temp;
            }
            set { pageNum = value; }
        }

        private int totalCount;
        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount
        {
            get { return totalCount; }
            set { totalCount = value; }
        }

        private int totalMax;

        public int TotalMax
        {
            get { return totalMax; }
            set { totalMax = value; }
        }

        private double totalPrice;

        public double TotalPrice
        {
            get { return totalPrice; }
            set { totalPrice = value; }
        }

        private string keywords;
        /// <summary>   
        /// where语句，不加where与空格
        /// </summary>
        public string Keywords
        {
            get
            {
                if (Request.Form["keywords"] != null)
                {
                    string temp = Request.Form["keywords"];
                    return temp;
                }
                else
                    return keywords;
            }
            set { keywords = value; }
        }

        private string orderField;
        /// <summary>
        /// 排序关键字
        /// </summary>
        public string OrderField
        {
            get
            {
                string temp = Request.Form["orderField"];
                return temp;
            }
            set { orderField = value; }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.LiteralTool.Text = GetBtn();
                this.HouseID.Value = Request.QueryString["HouseID"];
                this.Type.Value = Request.QueryString["Type"];
            }

            this.rpt1.DataSource = BindList();
            this.rpt1.DataBind();
        }

        public static string ParseTags(string HTMLStr, int count)
        {
            string s = System.Text.RegularExpressions.Regex.Replace(HTMLStr, "<[^>]*>", "");
            if (s.Length > count)
            {
                return s.Substring(0, count) + "......";
            }
            else
            {
                return s;
            }
        }


        private DataTable BindList()
        {
            string Type = Request.QueryString["Type"] == null ? Request.Form["Type"] : Request.QueryString["Type"];
            string sql = string.Empty;
            //收藏的
            if (Type == "2")
            {
                sql = @" select b.*,e.em_name from Share_Collection a left join Share_Templete b on a.TempleteID=b.id
                        left join e_Employee e on b.EmployeeID=e.EmployeeID
                        where a.EmployeeID=" + Employee.Current.EmployeeID + "  " + Getwhere() + "  ";
            }
            else 
            {
                sql = @"select a.*,b.em_name from Share_Templete a left join e_Employee b on a.EmployeeID=b.EmployeeID where  
                           Type=" + Type + "  "+Getwhere()+"   order by AddDate desc";
                            
            }

          
            DataSet ds = DbHelperSQL.PageBySqlCount(sql, NumPerPage, PageNum);
            TotalCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);           //统计数据条数
            return ds.Tables[2];
        }


        private string Getwhere()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" and 1=1 ");

            if (!Request.Form["ffrmTitle"].IsNullOrWhiteSpace())
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");

                sb.Append(" Title like '%" + HouseMIS.EntityUtils.StringHelper.Filter(Request.Form["ffrmTitle"]) + "%'");
            }

            if (!string.IsNullOrEmpty(Request.Form["ffrmShareType"]))
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.Append(" ShareType = '" + Request.Form["ffrmShareType"] + "'");

            }
          
        
            //sb.Append(GetRolePermissionEmployeeIds("查看", "a.EmployeeID"));

            return sb.Length == 0 ? null : sb.ToString();
        }


        /// <summary>
        /// 0 公共模板 1 私有模板 2收藏模板
        /// </summary>
        /// <returns></returns>
        private string GetBtn()
        {
            Dictionary<string, string> str = new Dictionary<string, string>();
            if (CheckRolePermission("私有"))
            {
                str.Add("tj", "<li><a class=\"add\" href=\"House/Templete/ShareFindHouse.aspx?NavTabId=" + NavTabId + "&doAjax=true&Type=1&HouseID=" + Request.QueryString["HouseID"] + "\" width=\"650\" height=\"550\" target=\"dialog\" title=\"选择要分享的模板\" maxable='false' ><span>私有模板</span></a></li>");
                str.Add("sytj", "<li><a class=\"edit\" href=\"House/Templete/ShareTempleteForm.aspx?NavTabId=" + NavTabId + "&doAjax=true&Type=1\"  width=\"800\" height=\"650\" target=\"dialog\" maxable='false' title=\"添加私有模板\"><span>添加私有</span></a></li>");
            }
           
            str.Add("scmb", "<li><a class=\"delete\" href=\"House/Templete/ShareFindHouse.aspx?NavTabId=" + NavTabId + "&doAjax=true&Type=2&HouseID=" + Request.QueryString["HouseID"] + "\" width=\"650\" height=\"550\" target=\"dialog\" title=\"选择要分享的模板\" maxable='false' ><span>收藏模板</span></a></li>"); ;
            
            str.Add("tjs", "<li><a class=\"edit\" href=\"House/Templete/ShareFindHouse.aspx?NavTabId=" + NavTabId + "&doAjax=true&Type=0&HouseID=" + Request.QueryString["HouseID"] + "\" width=\"650\" height=\"550\" target=\"dialog\" title=\"选择要分享的模板\" maxable='false' ><span>公共模板</span></a></li>"); ;
            StringBuilder sb = new StringBuilder();
            foreach (string i in str.Values)
            {
                sb.Append(i);
            }

            return sb.ToString();
        }

    }
}