using HouseMIS.EntityUtils;
using HouseMIS.EntityUtils.DBUtility;
using HouseMIS.Web.UI;
using System;
using System.Data;
using System.Text;

namespace HouseMIS.Web.House
{
    public partial class HouseFromSP : EntityListBase
    {
        public string HouseID = string.Empty;
        public string EmployeeID = Employee.Current.EmployeeID.ToString();
        public string Shi_id = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (CheckRolePermission("视频上传"))
                {
                    fileUpload.Visible = true;
                }
                if (Request.QueryString["Shi_id"] != null)
                {
                    Shi_id = Request.QueryString["Shi_id"];
                }
                if (Request.QueryString["HouseID"] != null)
                {
                    HouseID = Request.QueryString["HouseID"];
                }
            }
        }

        public override string MenuCode
        {
            get
            {
                return "2001";
            }
        }

        /// <summary>
        /// 得到视频房源
        /// </summary>
        protected string HouseVideo()
        {
            string sql = string.Format(@"SELECT a.housevideoid,
                                                A.filenam,
                                                A.pic,
                                                CONVERT(VARCHAR, A.tim, 120)   AS tim,
                                                ( C.NAME + ' - ' + B.em_name ) AS nam,
                                                A.istran,
                                                B.em_id,
                                                A.employeeid
                                        FROM   housevideo A
                                                LEFT JOIN e_employee B
                                                        ON A.employeeid = B.employeeid
                                                LEFT JOIN s_organise C
                                                        ON B.orgid = C.orgid
                                        WHERE  a.isdel=0
                                                and houseid = {0} 
                                        order by a.tim desc",
                HouseID);
            DataTable dt = DbHelperSQL.Query(sql).Tables[0];
            int length = dt.Rows.Count;
            if (length > 0)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < length; i++)
                {
                    String videoUrl = dt.Rows[i]["filenam"].ToString().ToLower();
                    //if (!videoUrl.StartsWith(ImageHelper.dirImages))
                    //    videoUrl = "/" + ImageHelper.dirImages + "/videotran/" + videoUrl;
                    //videoUrl = ImageHelper.GetUrl(videoUrl);

                    string videopicUrl = "/img/wait.jpg";
                    //String videopicUrl = dt.Rows[i][1].ToString().ToLower().Trim('/').Replace("video/", "videotran/");
                    //if (!videopicUrl.StartsWith(ImageHelper.dirImages))
                    //    videopicUrl = "/" + ImageHelper.dirImages + "/videotran/" + videopicUrl;
                    //videopicUrl = ImageHelper.GetUrl(videopicUrl);

                    sb.Append("<fieldset><table width='100%' border='0' cellspacing='0' cellpadding='0' height='80'>");
                    sb.Append("<tr>");
                    //if (dt.Rows[i][4].ToString() == "1" || dt.Rows[i][0].ToString().IndexOf(".mp4") > 0 || dt.Rows[i][0].ToString().IndexOf(".mov") > 0)
                    //{
                    //< a width = '800' height = '600' title = '房源视频预览' href = 'http://vod.erp.efw.cn:8010/palyVideo.aspx?url={0}' target = 'dialog' >
                    sb.Append(string.Format(@"<td width='100' rowspan='2'>
                                                <a width='800' height='600' title='房源视频预览' href='/chat/PhoneWeb/PlayVideo.aspx?url={0}' target='dialog'>
                                                    <img src='{1}' width='100' height='56' />
                                                </a>
                                                </td>",
                                               videoUrl,
                                               videopicUrl));
                    //}
                    //else
                    //{
                    //    sb.Append("<td width='100' rowspan='2'>该视频已上传成功正在转码中，请稍候预览.</td>");
                    //}
                    sb.Append(string.Format("<td>上传员工：{0}</td>",
                                dt.Rows[i]["nam"]));
                    sb.Append(string.Format("<td>上传时间：{0}</td>",
                                dt.Rows[i]["tim"]));
                    sb.Append("</tr>");

                    //sb.Append("<tr>");
                    //sb.Append(string.Format("<td colspan='2'>视频地址(复制到浏览器观看)：{0}<input type=\"button\" value=\"复制地址\" onclick=\"copyToClipboard('{0}')\" /></td>",
                    //            videoUrl));
                    //sb.Append("</tr>");

                    sb.Append(string.Format("<tr><td><a href=\"{0}\" target=\"_blank\"><span style=\"color: red\">下载视频</span></a></td>",
                                videoUrl));
                    if (CheckRolePermission("视频删除"))
                    {
                        sb.Append(string.Format("<td><a href=\"House/HouseForm.aspx?OperType=2&LSH=1&housevideoid={0}&em_id={1}&shi_id={2}\" target=\"ajaxTodo\" title='确定删除吗？'><span style=\"color: red\">删除视频</span></a></td>",
                                    dt.Rows[i]["housevideoid"],
                                    dt.Rows[i]["employeeid"],
                                    Shi_id));
                    }
                    sb.Append("</tr>");
                    sb.Append("</table></fieldset>");
                }
                return sb.ToString();
            }
            return "暂无视频";
        }

        [System.Web.Services.WebMethod]
        public static string HouseVideoInsert(int houseID, int employeeID, string fileName)
        {
            if (!fileName.IsNullOrWhiteSpace())
            {
                HouseVideo hv = new HouseVideo();
                hv.HouseID = houseID;
                hv.EmployeeID = employeeID;
                hv.Typ = 0;
                hv.FileNam = fileName;
                hv.Insert();

                EntityUtils.HouseVideo.Meta.Execute(string.Format("update h_houseinfor set IsVideo=1 where houseid={0}",
                                                                hv.HouseID));

                return "上传成功！";
            }
            return "没有收到文件!";
        }
    }
}