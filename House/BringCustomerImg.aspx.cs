using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HouseMIS.Web.UI;
using HouseMIS.EntityUtils;
using System.Data;
using TCode;
using AjaxPro;
using System.Text;

using HouseMIS.Common;

namespace HouseMIS.Web.House
{
    public partial class BringCustomerImg : EntityListBase<c_BringCustomer>
    {
        protected StringBuilder CusStrimg = new StringBuilder();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (CheckRolePermission("带看凭证审核", RangeType.None))
            {
                CusStrimg.Append("<button id=\"showyouxiao\" onclick=\"showshenhe('1')\">有效</button><br />\n");
                CusStrimg.Append("<button id=\"showyouxiao\" onclick=\"showshenhe('0')\">无效</button><br />\n");
            }


            AjaxPro.Utility.RegisterTypeForAjax(typeof(HouseMIS.Web.House.BringCustomerImg), this.Page);
            if (!IsPostBack)
            {
                string cusid = Request.QueryString["BringCustomerID"];
                bid.Value = cusid;

                DataTable dt = c_BringCustomer.Meta.Query("SELECT * FROM c_BringCustomer WHERE BringCustomerID = " + cusid).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["WebPhoto"].ToString().ToLower().Trim('/').StartsWith(ImageHelper.dirImages))
                        img.Src = ImageHelper.GetUrl(dt.Rows[0]["WebPhoto"].ToString());
                    else
                    {
                        if (dt.Rows[0]["WebPhoto"].ToString().ToLower().IndexOf("uploadfiles") == -1)
                            img.Src = ImageHelper.GetUrl("/uploadfiles/customerpic/" + dt.Rows[0]["WebPhoto"].ToString());
                        else
                            img.Src = ImageHelper.GetUrl(dt.Rows[0]["WebPhoto"].ToString());
                    }
                }
            }
        }

        [AjaxPro.AjaxMethod]
        public int showopts(object opts,object op)
        {
            int i = 0;
            if (opts.ToInt32() == 1)
            {
                try
                {
                    HouseMIS.EntityUtils.c_BringCustomer.Meta.Query("UPDATE c_BringCustomer SET Isshenhe = 1 , shenOperatorDate = GETDATE() , shenOperatorID ='" + HouseMIS.EntityUtils.Employee.Current.EmployeeID + "' WHERE BringCustomerID = " + op);
                    //HouseMIS.EntityUtils.c_BringCustomer.Meta.Query("UPDATE c_BringCustomer SET Isshenhe = 1 WHERE BringCustomerID = " + op);
                    //HouseMIS.EntityUtils.c_BringCustomer.Meta.Query("UPDATE c_BringCustomer SET shenOperatorDate = GETDATE() AND shenOperatorID = '" + HouseMIS.EntityUtils.Employee.Current.EmployeeID + "' WHERE BringCustomerID = " + op);
                }
                catch
                {
                    i = 0;
                }
                finally
                {
                    i = 1;
                    string BID = op.ToString2();

                    c_BringCustomer cb = c_BringCustomer.FindByBringCustomerID(Convert.ToInt32(BID));
                    if (cb != null)
                    {
                        c_Customer c = c_Customer.FindByCus_id(cb.CustomerID);
                        if (c != null)
                        {
                            DataTable dt = I_IntegralLog.Meta.Query("SELECT * FROM I_IntegralLog WHERE Remak='添加客户积分' AND KeyValue ='" + c.CustomerID + "'").Tables[0];
                            if (dt.Rows.Count == 0)
                            {
                                var AddIntegral = System.Math.Abs(Math.Round((Convert.ToDecimal(c.NPrice) * 40), 0));
                                c_Customer.UpdateIntegral(AddIntegral.ToInt32().Value, "添加客户积分", c.OperatorID.ToInt32().Value, DateTime.Now, "c_Customer", "CustomerID", c.CustomerID.ToString());
                            }
                        }
                    }
                }
            }
            else if (opts.ToInt32() == 0)
            {
                try
                {
                    HouseMIS.EntityUtils.c_BringCustomer.Meta.Query("UPDATE c_BringCustomer SET Isshenhe = 0 , shenOperatorDate = GETDATE() , shenOperatorID ='" + HouseMIS.EntityUtils.Employee.Current.EmployeeID + "' WHERE BringCustomerID = " + op);
                    //HouseMIS.EntityUtils.c_BringCustomer.Meta.Query("UPDATE c_BringCustomer SET Isshenhe = 0 WHERE BringCustomerID = " + op);
                    //HouseMIS.EntityUtils.c_BringCustomer.Meta.Query("UPDATE c_BringCustomer SET shenOperatorDate = GETDATE() AND shenOperatorID = '" + HouseMIS.EntityUtils.Employee.Current.EmployeeID + "' WHERE BringCustomerID = " + op);
                }
                catch
                {
                    i = 0;
                }
                finally
                {
                    i = 1;
                    string BID = op.ToString2();

                    c_BringCustomer cb = c_BringCustomer.FindByBringCustomerID(Convert.ToInt32(BID));
                    if (cb != null)
                    {
                        c_Customer c = c_Customer.FindByCus_id(cb.CustomerID);
                        if (c != null)
                        {
                            DataTable dt = I_IntegralLog.Meta.Query("SELECT * FROM I_IntegralLog  WHERE Remak='添加客户积分' AND KeyValue ='" + c.CustomerID + "'").Tables[0];
                            if (dt.Rows.Count > 0)
                            {
                                I_IntegralLog.Meta.Query("DELETE FROM I_IntegralLog WHERE Remak='添加客户积分 AND KeyValue = " + c.CustomerID);
                            }
                        }
                    }
                }
            }
            return i;
        }

    }
}