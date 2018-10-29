using HouseMIS.EntityUtils;
using HouseMIS.Web.UI;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace HouseMIS.Web.House
{
    public partial class ShhouseEmployeeForm : EntityFormBase<Share_Personinfo>
    {
        protected override void OnInitComplete(EventArgs e)
        {
            Entity = Share_Personinfo.Find(Share_Personinfo._.EmployeeID, Employee.Current.EmployeeID);
            base.OnInitComplete(e);
        }

        private string baseurl = "http://2sf1.efw.cn";

        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Request.QueryString["type"];
            if (!IsPostBack)
            {
                string esfname = HttpGet(baseurl + "/erp/GetEfwUser?erpid=" + Employee.Current.EmployeeID + "");
                frmshEName.Text = esfname;
            }
            if (type == "add")
            {
            }
            //Share_Personinfo model = Share_Personinfo.Find(Share_Personinfo._.EmployeeID, Employee.Current.EmployeeID);
            //if (!IsPostBack)
            //{
            //    if (model != null)
            //    {
            //        frmshpwd.Attributes.Add("TextMode", "Password");
            //        frmshEName.Text = model.shEName;
            //        frmshpwd.Text = model.shPwd;
            //    }
            //}
        }

        protected override void OnSaving(object sender, EntityFormEventArgs e)
        {
            string r = HttpGetUTF(baseurl + "/erp/BindEfwUser?username=" + frmshEName.Text + "&pwd=" + frmshPwd.Text + "&erpid=" + Employee.Current.EmployeeID + "&erpname=" + Base64Encode(Employee.Current.Em_name) + "");
            if (r == "1")
            {
                base.OnSaving(sender, e);
                ShowMsg(AlertType.correct, "绑定成功！", false, false);
            }
            else
                ShowMsg(AlertType.error, "绑定失败！", false);
            //SaveValid();
            //Entity.shPwd = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(frmshpwd.Text, "MD5").ToLower();
        }

        protected override void OnSetForm(object sender, EntityFormEventArgs e)
        {
            base.OnSetForm(sender, e);
            frmEmployeeID.Value = Employee.Current.EmployeeID.ToString();
        }

        protected override void OnSaveSuccess(object sender, EntityFormEventArgs e)
        {
            //JSDo_UserCallBack_Success("", "操作成功！", false);
            //return;
            //base.OnSaveSuccess(sender, e);
        }

        public void SaveValid()
        {
            if (Entity.shEName.IsNullOrWhiteSpace()) ShowMsg(AlertType.error, "用户名不允许为空！");
            if (Entity.shPwd.IsNullOrWhiteSpace()) ShowMsg(AlertType.error, "密码不允许为空！");
        }

        public string HttpGet(string Url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("gb2312"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }

        public string HttpGetUTF(string Url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "GET";
            request.ContentType = "application/json;charset=unicode";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("gb2312"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }

        /// <summary>
        /// Base64加密，采用utf8编码方式加密
        /// </summary>
        /// <param name="source">待加密的明文</param>
        /// <returns>加密后的字符串</returns>
        public static string Base64Encode(string source)
        {
            return Base64Encode(Encoding.UTF8, source);
        }

        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="encodeType">加密采用的编码方式</param>
        /// <param name="source">待加密的明文</param>
        /// <returns></returns>
        public static string Base64Encode(Encoding encodeType, string source)
        {
            string encode = string.Empty;
            byte[] bytes = encodeType.GetBytes(source);
            try
            {
                encode = Convert.ToBase64String(bytes).Replace("/", "_a").Replace("+", "_b").Replace("=", "_c");
                //_a,_b,_c     /,+,=
            }
            catch
            {
                encode = source;
            }
            return encode;
        }
    }
}