using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HouseMIS.Web.UI;
using System.Text;
using System.Data;
using HouseMIS.EntityUtils.DBUtility;
using HouseMIS.EntityUtils;

namespace HouseMIS.Web.House.Templete
{
    public partial class WeiXinTempleteList : BasePage
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
            }
            this.rpt1.DataSource = BindList();
            this.rpt1.DataBind();
        }


        public static string ParseTags(string HTMLStr,int count)
        {
            string s=System.Text.RegularExpressions.Regex.Replace(HTMLStr, "<[^>]*>", "");
            if(s.Length>count)
            {
                return s.Substring(0, count) + "......";
            }
            else
            {
                return s;
            }
        }

        private string GetBtn()
        {
            Dictionary<string, string> str = new Dictionary<string, string>();

            if (CheckRolePermission("添加"))
            {
                str.Add("tj", "<li><a class=\"add\" href=\"House/Templete/WeiXinTempleteForm.aspx?NavTabId=" + NavTabId + "&doAjax=true\"  width=\"800\" height=\"650\" target=\"dialog\" maxable='false' title=\"添加\"><span>添加</span></a></li>");
            }
            if (CheckRolePermission("编辑"))
            {
                str.Add("bj", "<li><a class=\"edit\" href=\"House/Templete/WeiXinTempleteForm.aspx?NavTabId=" + NavTabId + "&doAjax=true&ID={MasterKeyValue}\" width=\"800\" height=\"650\" target=\"dialog\" title=\"编辑\" maxable='false'><span>编辑</span></a></li>");
            }
            if (CheckRolePermission("删除"))
            {
                str.Add("sc", "<li><a class=\"delete\" href=\"House/Templete/WeiXinTempleteForm.aspx?NavTabId=" + NavTabId + "&doAjax=true&doType=del&ID={MasterKeyValue}\" rel=\"PortID\" target=\"ajaxTodo\" title=\"确定要删除吗?\"><span>删除</span></a></li>");
            }

        
            StringBuilder sb = new StringBuilder();
            foreach (string i in str.Values)
            {
                sb.Append(i);
            }

            return sb.ToString();
        }


        private DataTable BindList()
        {
            string sql = @"select a.*,b.em_name,s.Name from Share_Templete a left join e_Employee b on a.EmployeeID=b.EmployeeID  
                            left join s_Organise  s on b.OrgID=s.OrgID where ShareType ='公众号图片' or ShareType ='公众号简介' " + getwhere() + " order by AddDate desc ";
            DataSet ds = DbHelperSQL.PageBySqlCount(sql, NumPerPage, PageNum);
            TotalCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);           //统计数据条数
            return ds.Tables[2];
        }



        private string getwhere()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" AND 1=1");

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
            if (!string.IsNullOrEmpty(Request.Form["ffrmName"]))
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.Append(" b.em_name like '%" + Request.Form["ffrmName"] + "%'");
            }
            if (!string.IsNullOrEmpty(Request.Form["ffrmstart"]))
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.Append(" AddDate >= '" + Request.Form["ffrmstart"] + "'");
            }
            if (!string.IsNullOrEmpty(Request.Form["ffrmend"]))
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.Append(" AddDate <= '" + Request.Form["ffrmend"] + "'");
            }
            if (!Request.Form["ShareTempleteList_OrgID"].IsNullOrWhiteSpace())
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat("s.OrgID={0} ", HouseMIS.EntityUtils.StringHelper.Filter(Request.Form["ShareTempleteList_OrgID"]));
            }

            return sb.Length == 0 ? null : sb.ToString();
        }
    }
}