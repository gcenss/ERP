using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HouseMIS.Web.UI;
using HouseMIS.EntityUtils;
using System.Data;
using TCode;

namespace HouseMIS.Web.House
{
    public partial class OutSetAdd : EntityListBase<HouseMIS.EntityUtils.Employee>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HouseOurtHouseID.Value = Request.QueryString["HouseID"].ToString2();
            }
        }

        /// <summary>
        /// 提交时触发
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            if (Request.Form["doAjax"] == "true" && Request["SearchBarPost"] != "true")
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                Employee.Meta.BeginTrans();
                try
                {
                    string where = GetWhereClauseFromSearchBar(null);
                    if (!Request.Form["ids"].IsNullOrWhiteSpace())
                    {
                        if (where != null)
                        {
                            if (where.Length > 0)
                                where += " AND ";
                        }
                        where += " EmployeeID in (" + Request.Form["ids"] + ")";

                        TCode.EntityList<Employee> list = Employee.FindAll(where, null, null, 0, 0);

                        foreach (Employee EMP in list)
                        {
                            h_SeeTelLog hs = new h_SeeTelLog();
                            hs.HouseID = Convert.ToDecimal(Request.Form["HouseOurtHouseID"]);
                            hs.EmployeeID = EMP.EmployeeID;
                            hs.exe_Date = DateTime.Now;
                            hs.IsPower = false;
                            hs.Insert();
                        }

                        Employee.Meta.Commit();
                        Response.Write("<script type='text/javascript'>alertMsg.correct('操作成功');$.pdialog.closeCurrent();$('#outsetRel').reload();</script>");
                    }
                    else
                    {
                        Employee.Meta.Rollback();
                        Say(sb, "请选择员工！", false, false, false);
                    }
                }
                catch
                {
                    Employee.Meta.Rollback();
                    Say(sb, "操作失败！", true, false, false);
                }
                Response.End();
            }
            else
                base.OnPreInit(e);
        }

        /// <summary>
        /// 弹出提示
        /// </summary>
        /// <param name="sayWhat">弹出字</param>
        /// <param name="isClose">是否关闭窗口</param>
        /// <param name="isTrue">是否成功</param>
        /// <param name="isTrue">是否另加JS</param>
        private void Say(System.Text.StringBuilder sb, string sayWhat, bool isClose, bool isTrue, bool isJS)
        {
            sb.Append("{\r\n");
            if (isTrue == true)
            {
                sb.Append("   \"statusCode\":\"200\", \r\n");
            }
            else
            {
                sb.Append("   \"statusCode\":\"300\", \r\n");
            }
            sb.Append("   \"message\":\"" + sayWhat + "\", \r\n");
            sb.Append("   \"navTabId\":\"" + NavTabId + "\", \r\n");
            if (isClose == true)
            {
                sb.Append("   \"callbackType\":\"closeCurrent\",\r\n");
            }
            if (isJS == true)
            {
                sb.Append("   \"userCallBack\":\"" + Microsoft.JScript.GlobalObject.escape("$.pdialog.closeCurrent();$.pdialog.reload(null,{dialogId:'outsetRel'})") + "\", \r\n");
            }
            sb.Append("   \"rel\":\"1\", \r\n");
            sb.Append("   \"forwardUrl\":\"\"\r\n");
            sb.Append("}\r\n");
            Response.Write(sb.ToString());

        }
    }
}