using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HouseMIS.Web.UI;
using HouseMIS.EntityUtils;
using System.Net;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using TCode.DataAccessLayer;

namespace HouseMIS.Web.House
{
    public partial class HouseTelForm_SMS : EntityListBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["LSH"] != null)
                {
                    LSH.Value = Request.QueryString["LSH"];
                }

                h_HouseTelList hTel = h_HouseTelList.FindByLSH(Convert.ToDecimal(Request.QueryString["LSH"]));
                H_houseinfor hh = H_houseinfor.FindByHouseID(hTel.HouseID);
                txtMsg.Text = "房东你好，我是" + (Current.OrgName.Contains("易房") ? "易房" : "中山") + "房产员工，现有客户准备去看你" + hh.HouseDicName + "的房屋，想和你确认什么时间方便看房";
            }
        }

        protected override void OnPreInit(EventArgs e)
        {
            //保存
            if (Request.Form["doAjax"] == "true")
            {
                HttpWebRequest hwq = (HttpWebRequest)WebRequest.Create("http://120.27.150.137:8080/softswitch/internal/getDisplayNum.jsp?e164=" + Current.UserName);

                HttpWebResponse responseSorce = (HttpWebResponse)hwq.GetResponse();
                StreamReader reader = new StreamReader(responseSorce.GetResponseStream(), Encoding.UTF8);
                string content = reader.ReadToEnd();
                responseSorce.Close();
                responseSorce = null;
                reader = null;

                CallBackMsg cm = JsonConvert.DeserializeObject<CallBackMsg>(content);
                if (cm.errcode != "0")
                {
                    ShowMsg(AlertType.error, "没有绑定隐号，请联系人事部绑定隐号");
                }
                else
                {
                    h_HouseTelList hTel = h_HouseTelList.FindByLSH(Convert.ToDecimal(Request.Form["LSH"]));
                    if (hTel != null)
                    {
                        string tel = hTel.TelDe;
                        if (!tel.IsNullOrWhiteSpace())
                        {
                            i_InternetPhone ip = i_InternetPhone.Find("toTel='" + tel + "' and CONVERT(varchar,createTime,112)='" + DateTime.Now.ToString("yyyyMMdd") + "'");
                            if (ip == null)
                            {
                                ShowMsg(AlertType.error, "请先拨打电话后再发送短信");
                            }
                            else
                            {
                                List<e_SmsLog> listSMS = e_SmsLog.FindAll("select * from e_SmsLog where phone='" + tel + "' and CONVERT(varchar,datetime,112)='" + DateTime.Now.ToString("yyyyMMdd") + "'");
                                if (listSMS.Count > 5)
                                {
                                    ShowMsg(AlertType.error, "该房东今天已发送5次短信，请明天再试");

                                }
                                else
                                {
                                    if (listSMS.Count(x => x.Phone == Current.UserName && x.Datetime.ToShortDateString() == DateTime.Now.ToShortDateString()) > 0)
                                    {
                                        ShowMsg(AlertType.error, "你今天已经给该房东发送过信息，请明天再试");
                                    }
                                    else
                                    {
                                        string txtMsg = Request.Form["txtMsg"];
                                        txtMsg += ",请回电:" + cm.displayNum + ",回T退订【" + (Current.OrgName.Contains("易房") ? "易房网房产" : "中山房产") + "】";
                                        string errMsg = e_SmsLog.SendSMS(Employee.Current.EmployeeID, tel, txtMsg, hTel.HouseID, hTel.LSH);

                                        ShowMsg(AlertType.info, errMsg);
                                    }
                                }
                            }
                        }
                        else
                        {
                            ShowMsg(AlertType.error, "电话号码错误");
                        }
                    }
                }
            }
        }

        public class CallBackMsg
        {
            public string errcode { get; set; }
            public string errmsg { get; set; }

            public string displayNum { get; set; }
        }
    }
}