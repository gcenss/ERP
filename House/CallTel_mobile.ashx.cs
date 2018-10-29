using HouseMIS.Common;
using HouseMIS.EntityUtils;
using System;
using System.Web;

namespace HouseMIS.Web.House
{
    /// <summary>
    /// CallTel 的摘要说明
    /// </summary>
    public class CallTel_mobile : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string telFrom = string.Empty;
            string telTo = string.Empty;
            string result = string.Empty;
            decimal houseID = 0;
            i_InternetPhone iip = new i_InternetPhone();

            if (context.Request.QueryString["aTyp"] != null && context.Request.QueryString["aTyp"].ToString() == "del")
            {
                h_HouseTelList hh = h_HouseTelList.FindByKey(context.Request.QueryString["LSH"]);
                hh.DelEmployeeID = Employee.Current.EmployeeID;
                hh.DelType = true;
                hh.Update();
                h_FollowUp hfo = new h_FollowUp();
                hfo.HouseID = hh.HouseID;
                hfo.EmployeeID = Employee.Current.EmployeeID;
                hfo.FollowUpText = "电话删除";
                hfo.Insert();

                // 电话修改记录
                string oldTel = hh.Tel2;
                TelChange tc = new TelChange();
                tc.AddEmployeeID = Employee.Current.EmployeeID;
                tc.HouseID = hh.HouseID;
                tc.NewTel = "";
                tc.OldTel = oldTel.TelDecrypt((Int32)hh.HouseID, 0);
                tc.Insert();

                context.Response.Write("del");
                return;
            }
            //房源电话拨打
            else if (context.Request.QueryString["LSH"] != null)
            {
                int ii = i_InternetTel.FindCount(new string[] { "EmployeeID", "IsDel" }, new string[] { Employee.Current.EmployeeID.ToString(), "0" });
                if (ii > 0)
                {
                    iip.RecrodType = 1;
                    h_HouseTelList hh = h_HouseTelList.FindByKey(context.Request.QueryString["LSH"]);
                    houseID = hh.HouseID;
                    telTo = hh.Tel2.TelDecrypt((Int32)hh.HouseID, TelDecPoint.PC_HouseForm_TelPhone);
                    if (context.Request.QueryString["mytel"] != null)
                    {
                        telFrom = context.Request.QueryString["mytel"].ToString();
                    }
                    else
                    {
                        telFrom = i_InternetTel.Find("EmployeeID=" + Employee.Current.EmployeeID + " and IsDel=0").MyTel;
                    }
                }
                else
                {
                    context.Response.Write("1," + Employee.Current.EmployeeID.ToString());
                    return;
                }
            }
            //装修拨打买家电话
            else if (context.Request.QueryString["bTel"] != null)
            {
                int ii = i_InternetTel.FindCount(new string[] { "EmployeeID", "IsDel" }, new string[] { Employee.Current.EmployeeID.ToString(), "0" });
                if (ii > 0)
                {
                    iip.RecrodType = 5;
                    telTo = context.Request.QueryString["bTel"];
                    houseID = Convert.ToDecimal(context.Request.QueryString["HouseID"]);
                    if (context.Request.QueryString["mytel"] != null)
                    {
                        telFrom = context.Request.QueryString["mytel"].ToString();
                    }
                    else
                    {
                        telFrom = i_InternetTel.Find("EmployeeID=" + Employee.Current.EmployeeID + " and IsDel=0").MyTel;
                    }
                }
                else
                {
                    context.Response.Write("1," + Employee.Current.EmployeeID.ToString());
                    return;
                }
            }
            //员工通讯录拨打
            else if (context.Request.QueryString["EmpTelFrom"] != null && context.Request.QueryString["EmpTelTo"] != null)
            {
                iip.RecrodType = 4;
                telFrom = context.Request.QueryString["EmpTelFrom"];
                telTo = context.Request.QueryString["EmpTelTo"];
                houseID = 0;
            }

            iip.employeeID = Employee.Current.EmployeeID;
            iip.houseID = houseID;
            iip.dateCreated = DateTime.Now.ToString();
            iip.fromTel = telFrom;
            iip.toTel = telTo;
            iip.recordUrlDel = 2;
            iip.orgID = Convert.ToInt32(Employee.Current.OrgID);
            iip.isPcCallTel = 1;
            iip.createTime = DateTime.Now;
            iip.Source = 0;
            iip.CallSystem = 0;

            iip.Insert();

            InterPhoneCall ip = new InterPhoneCall();
            InterPhoneCall.CallBackMsg cbm = new InterPhoneCall.CallBackMsg();

            //是否大众员工
            if (Employee.Current.MyTopOrgA.OrgID == 1062)
            {
                cbm = ip.Call_Mobile(telFrom, telTo, iip.phoneID.ToString(), 1);
            }
            else
            {
                cbm = ip.Call_Mobile(telFrom, telTo, iip.phoneID.ToString());
            }

            iip.callSid = cbm.taskId;
            iip.Update();
            result = cbm.errcode;

            if (result == "0")
            {
                context.Response.Write("连接成功！连接手机号：" + telFrom);
            }
            else
            {
                context.Response.Write("连接失败！请联系人事-检查是否绑定隐号！");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}