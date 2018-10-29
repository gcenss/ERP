using HouseMIS.Common;
using HouseMIS.EntityUtils;
using System;
using System.Web;

namespace HouseMIS.Web.House
{
    /// <summary>
    /// CallTel 的摘要说明
    /// </summary>
    public class CallTel : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
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
            }
            else if (context.Request.QueryString["LSH"] != null)
            {
                h_HouseTelList hh = h_HouseTelList.FindByKey(context.Request.QueryString["LSH"]);
                string result = "";
                int ii = i_InternetTel.FindCount(new string[] { "EmployeeID", "IsDel" }, new string[] { Employee.Current.EmployeeID.ToString(), "0" });
                if (ii > 0)
                {
                    string tel = "";
                    //隐号拔打
                    string houseTel = hh.Tel2.TelDecrypt((Int32)hh.HouseID, TelDecPoint.PC_HouseForm_TelPhone);
                    if (context.Request.QueryString["mytel"] != null)
                    {
                        tel = context.Request.QueryString["mytel"].ToString();
                        i_InternetPhone iip = new i_InternetPhone();
                        iip.employeeID = Employee.Current.EmployeeID;
                        iip.houseID = hh.HouseID;
                        iip.dateCreated = DateTime.Now.ToString();
                        iip.fromTel = tel;
                        iip.toTel = houseTel;
                        iip.recordUrlDel = 2;
                        iip.orgID = Convert.ToInt32(Employee.Current.OrgID);
                        iip.isPcCallTel = 1;
                        iip.createTime = DateTime.Now;
                        iip.Source = 2;
                        iip.CallSystem = 0;
                        iip.RecrodType = 1;
                        iip.Insert();

                        Common.InterPhoneCall ip = new Common.InterPhoneCall();
                        Common.InterPhoneCall.CallBackMsg cbm = new Common.InterPhoneCall.CallBackMsg();

                        s_SysParam model = s_SysParam.Find(Share_Personinfo._.ID, 1279);
                        string sqlnum = @"select COUNT(*)AS Num from i_InternetPhone
                                            where createTime > DATEADD(minute, -15, GETDATE())
                                            and datediff(dd, createTime, GETDATE())= 0
                                            and employeeID =" + Employee.Current.EmployeeID + @"
                                            and houseID =" + hh.HouseID + @"
                                            and  callSid !=''";
                        int CallNum = int.Parse(EntityUtils.DBUtility.DbHelperSQL.Query(sqlnum).Tables[0].Rows[0][0].ToString());
                        if (CallNum >= int.Parse(model.Value) && int.Parse(model.Value) > 0)
                        {
                            result = "1";
                        }
                        else
                        {
                            cbm = ip.Call(tel, houseTel, iip.phoneID.ToString());
                            iip.callSid = cbm.taskId;
                            iip.Update();
                            result = cbm.errcode;
                        }
                    }
                    else
                    {
                        tel = i_InternetTel.Find("EmployeeID=" + Employee.Current.EmployeeID + " and IsDel=0").MyTel;
                        i_InternetPhone iip = new i_InternetPhone();
                        iip.employeeID = Employee.Current.EmployeeID;
                        iip.houseID = hh.HouseID;
                        iip.dateCreated = DateTime.Now.ToString();
                        iip.fromTel = tel;
                        iip.toTel = houseTel;
                        iip.recordUrlDel = 2;
                        iip.orgID = Convert.ToInt32(Employee.Current.OrgID);
                        iip.isPcCallTel = 1;
                        iip.createTime = DateTime.Now;
                        iip.Source = 2;
                        iip.CallSystem = 0;
                        iip.RecrodType = 1;
                        iip.Insert();
                        Common.InterPhoneCall ip = new Common.InterPhoneCall();
                        Common.InterPhoneCall.CallBackMsg cbm = new Common.InterPhoneCall.CallBackMsg();

                        s_SysParam model = s_SysParam.Find(Share_Personinfo._.ID, 1279);
                        string sqlnum = @"select COUNT(*)AS Num from i_InternetPhone
                                          where createTime > DATEADD(minute, -15, GETDATE()) and datediff(dd, createTime, GETDATE())= 0 and employeeID =" + Employee.Current.EmployeeID + "and houseID =" + hh.HouseID + "AND  callSid !=''";
                        int CallNum = int.Parse(HouseMIS.EntityUtils.DBUtility.DbHelperSQL.Query(sqlnum).Tables[0].Rows[0][0].ToString());
                        if (CallNum >= int.Parse(model.Value) && int.Parse(model.Value) > 0)
                        {
                            result = "1";
                        }
                        else
                        {
                            cbm = ip.Call(tel, houseTel, iip.phoneID.ToString());
                            iip.callSid = cbm.taskId;
                            iip.Update();
                            result = cbm.errcode;
                        }
                    }
                    if (result == "0")
                    {
                        context.Response.Write("连接成功！连接手机号：" + tel);
                    }
                    else if (result == "1")
                    {
                        string timesql = @"select top 1 datediff(SECOND , DATEADD(minute, -15, GETDATE()),createTime)time from i_InternetPhone where datediff(dd, createTime, GETDATE())= 0 and employeeID=" + Employee.Current.EmployeeID + "and callSid !='' and createTime > DATEADD(minute, -15, GETDATE()) and houseID=" + hh.HouseID;
                        int time = int.Parse(EntityUtils.DBUtility.DbHelperSQL.Query(timesql).Tables[0].Rows[0][0].ToString());
                        TimeSpan ts = new TimeSpan(0, 0, time);
                        string times;
                        if (ts.Minutes > 0)
                            times = ts.Minutes + "分钟" + ts.Seconds + "秒";
                        else
                            times = ts.Seconds + "秒";

                        context.Response.Write("您拨打的太频繁了！</br>请稍作休息！请" + times + "后再试!");
                    }
                    else
                    {
                        context.Response.Write("连接失败！请联系管理员");
                    }
                }
                else
                {
                    context.Response.Write("1," + Employee.Current.EmployeeID.ToString());
                }
            }
            else if (context.Request.QueryString["EmpTelFrom"] != null && context.Request.QueryString["EmpTelTo"] != null)
            {
                string result = string.Empty;

                string telFrom = context.Request.QueryString["EmpTelFrom"];
                string telTo = context.Request.QueryString["EmpTelTo"];

                i_InternetPhone iip = new i_InternetPhone();
                iip.employeeID = Employee.Current.EmployeeID;
                iip.houseID = 0;
                iip.dateCreated = DateTime.Now.ToString();
                iip.fromTel = telFrom;
                iip.toTel = telTo;
                iip.recordUrlDel = 2;
                iip.orgID = Convert.ToInt32(Employee.Current.OrgID);
                iip.isPcCallTel = 1;
                iip.createTime = DateTime.Now;
                iip.Source = 2;
                iip.CallSystem = 0;
                iip.RecrodType = 4;
                iip.Insert();

                Common.InterPhoneCall ip = new Common.InterPhoneCall();
                Common.InterPhoneCall.CallBackMsg cbm = new Common.InterPhoneCall.CallBackMsg();

                s_SysParam model = s_SysParam.Find(Share_Personinfo._.ID, 1279);
                string sqlnum = @"select COUNT(*)AS Num from i_InternetPhone
                                          where createTime > DATEADD(minute, -15, GETDATE()) and datediff(dd, createTime, GETDATE())= 0 and employeeID =" + Employee.Current.EmployeeID + "and houseID =0  and callSid !=''";
                int CallNum = int.Parse(HouseMIS.EntityUtils.DBUtility.DbHelperSQL.Query(sqlnum).Tables[0].Rows[0][0].ToString());
                if (CallNum >= int.Parse(model.Value) && int.Parse(model.Value) > 0)
                {
                    result = "1";
                }
                else
                {
                    cbm = ip.Call(telFrom, telTo, iip.phoneID.ToString());
                    iip.callSid = cbm.taskId;
                    iip.Update();
                    result = cbm.errcode;
                }
                if (result == "0")
                {
                    context.Response.Write("连接成功！连接手机号：" + telFrom);
                }
                else if (result == "1")
                {
                    string timesql = @"select top 1 datediff(SECOND , DATEADD(minute, -15, GETDATE()),createTime)time from i_InternetPhone where datediff(dd, createTime, GETDATE())= 0 and employeeID=" + Employee.Current.EmployeeID + "and callSid !='' and createTime > DATEADD(minute, -15, GETDATE()) and houseID=0";
                    int time = int.Parse(HouseMIS.EntityUtils.DBUtility.DbHelperSQL.Query(timesql).Tables[0].Rows[0][0].ToString());
                    TimeSpan ts = new TimeSpan(0, 0, time);
                    string times;
                    if (ts.Minutes > 0)
                        times = ts.Minutes + "分钟" + ts.Seconds + "秒";
                    else
                        times = ts.Seconds + "秒";

                    context.Response.Write("您拨打的太频繁了！</br>请稍作休息！请" + times + "后再试!");
                }
                else
                {
                    context.Response.Write("连接失败！请联系管理员");
                }
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