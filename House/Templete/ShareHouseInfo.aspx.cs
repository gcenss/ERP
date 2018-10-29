using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HouseMIS.Web.UI;
using System.Text;
using System.Data;
using HouseMIS.EntityUtils;

using HouseMIS.Common;

namespace HouseMIS.Web.House.Templete
{

    public partial class ShareHouseInfo : System.Web.UI.Page
    {
        public string PicUrlList = "";
        public new string Title = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            string houseid = Request.QueryString["HouseID"] != null ? Request.QueryString["HouseID"] : Request.Form["HouseID"];
            if (!string.IsNullOrEmpty(houseid))
            {
                this.HouseID.Value = houseid;
                string sql1 = "select top 5 PicURL from h_PicList where PicTypeID!=1 and HouseID=" + HouseMIS.EntityUtils.StringHelper.Filter(houseid);
                StringBuilder sb = new StringBuilder();
                sb.Append(" select h.HouseDicName '楼盘地址',h.sum_price as 总价,h.Ohter2ID  as 单价,h.build_area as 面积,");
                sb.Append(" CONVERT(varchar(5),h.form_bedroom)+'室'+CONVERT(varchar(5),h.form_foreroom)+'厅'+CONVERT(varchar(5),h.form_toilet)+'卫'+CONVERT(varchar(5),h.form_terrace)+'阳台' as 户型,");
                sb.Append(" CONVERT(varchar(5),h.build_floor)+'/'+CONVERT(varchar(5),h.build_levels) as 楼层,");
                sb.Append(" t.Name as 房屋类型,f.Name as 装修,hf.Name as 朝向,s.Name as 小区楼盘");
                sb.Append(" ,h.note '房源描述',s.latitude,s.longitude, hy.Name as 年代,s.Environment as 周边配套,h.aType,Rent_Price as 租金");
                sb.Append(" from h_houseinfor h ");
                sb.Append(" left join s_HouseDic s on h.HouseDicID=s.HouseDicID ");
                sb.Append(" left join h_Type t on h.TypeCode=t.TypeCode");
                sb.Append(" left join h_Fitment f on h.FitmentID=f.FitmentID ");
                sb.Append(" left join h_Faceto hf on h.FacetoID=hf.FacetoID");
                sb.Append(" left join s_Organise so on so.OrgID=h.OrgID");
                sb.Append(" left join h_Year hy on hy.YearID=h.YearID");
                sb.Append(" where HouseID=" + houseid);

                DataTable dt = HouseMIS.EntityUtils.DBUtility.DbHelperSQL.Query(sb.ToString()).Tables[0];
                string content;

                //售房
                if (Convert.ToInt32(dt.Rows[0]["aType"]) == 0)
                {
                    content = TableToHtml(dt, "ShareTemplete.html");
                }
                else                       //租房
                {
                    content = TableToHtml(dt, "ShareTemplete_ZF.html");
                }

                string sql = "select top 5 PicURL from h_PicList where PicTypeID!=1  and  PicTypeID!=10 and HouseID=" + houseid;         //取五张房源图片
                DataTable imageTable = HouseMIS.EntityUtils.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                for (int i = 0; i < 5; i++)
                {
                    if (imageTable.Rows.Count > i)
                    {
                        PicUrlList += ImageHelper.GetUrl(imageTable.Rows[i]["PicURL"].ToString()) + ",";
                    }
                    else
                    {
                        PicUrlList += ",";
                    }
                }
                if (PicUrlList.Length > 0)
                    PicUrlList = PicUrlList.Remove(PicUrlList.Length - 1, 1);

                string Templeteid = Request.QueryString["TempleteID"] != null ? Request.QueryString["TempleteID"] : Request.Form["TempleteID"];
                this.TempleteID.Value = Templeteid;
                sql1 = "select * from Share_Templete where id=" + Templeteid + " ";
                dt = HouseMIS.EntityUtils.DBUtility.DbHelperSQL.Query(sql1).Tables[0];
                content = TableToContent(dt, content);

                Title = dt.Rows[0]["Title"].ToString();

                string EmployeeID = Request.QueryString["EmployeeID"] == null ? Employee.Current.EmployeeID.ToString() : Request.QueryString["EmployeeID"];
                sql = "select * from Share_Personinfo where EmployeeID =" + EmployeeID + "";

                DataTable PersonTable = HouseMIS.EntityUtils.DBUtility.DbHelperSQL.Query(sql).Tables[0];                 //取个人资料
                content = TableToContent(PersonTable, content);

                this.HtmlContent.Text = content;
            }
        }

        public string TableToHtml(DataTable table, string TempletePath)
        {
            string templete = string.Empty;
            string path = System.Web.HttpContext.Current.Server.MapPath(TempletePath);
            if (FileHelper.IsExistFile(path))
            {
                templete = FileHelper.FileToString(path);
            }

            for (int i = 0; i < table.Rows.Count; i++)
            {

                for (int j = 0; j < table.Columns.Count; j++)
                {
                    string tag = "$" + table.Columns[j].ColumnName;
                    if (templete.IndexOf(tag) > 0)
                    {

                        templete = templete.Replace(tag, table.Rows[i][j].ToString());
                    }
                }
            }
            return templete;
        }

        public string TableToContent(DataTable table, string content)
        {
            string templete = content;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    string tag = "$" + table.Columns[j].ColumnName;
                    if (templete.IndexOf(tag) > 0)
                    {
                        templete = templete.Replace(tag, table.Rows[i][j].ToString());
                    }
                }
            }
            return templete;
        }
    }
}