using System;
using HouseMIS.EntityUtils;
using HouseMIS.Web.UI;
using TCode;
using HouseMIS.EntityUtils.DBUtility;

namespace HouseMIS.Web.House
{
    public partial class HouseFromDK_JF : EntityListBase<c_BringCustomer>
    {
        public string HouseID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["HouseID"] != null)
                {
                    HouseID = Request.QueryString["HouseID"];
                    DK.DataSource = c_BringCustomer.Meta.Query(@"SELECT e.em_name btName,cc.Cus_id,h.shi_id,i.Integral,C.* 
                                                                        FROM c_BringCustomer C 
                                                                        LEFT JOIN c_Customer cc on cc.CustomerID = C.CID 
                                                                        LEFT JOIN h_houseinfor h on h.HouseID=C.HID 
                                                                        LEFT JOIN e_Employee e on e.EmployeeID = C.BeltmanID 
                                                                        left join I_IntegralLog i on i.KeyValue=C.BringCustomerID 
                                                                        where C.HID=" + HouseID + @" 
                                                                        and i.TableName='c_BringCustomer' 
                                                                        order by C.exe_date desc").Tables[0];
                    DK.DataBind();
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
        protected string customerBringpingzheng(object HID, object op_id, object bring_id)
        {

            string btn = "";
            bool isok = false;
            if (CheckRolePermission("客户带看凭证", op_id.ToDecimal()))
            {
                if (c_BringCustomer.FindByKey(bring_id).WebPhoto != null)
                {
                    // customerBringpin.Append("<a href=\"House/BringCustomerImg.aspx?BringCustomerID=" + cusid + "\" target=\"dialog\" width=\"510\" height=\"700\" fresh='true' maxable='false' title=\"跟进证明\" >查看</a>\n");
                    btn += "<a href=\"House/BringCustomerImg.aspx?BringCustomerID=" + bring_id + "\" target=\"dialog\" width=\"510\" height=\"700\" fresh='true' maxable='false' title=\"跟进证明\" >查看</a>\n";
                    isok = true;
                }
                else
                {
                    // customerBringpin.Append("无\n");
                    btn += "无\n";
                }
            }
            if (isok == false)
            {
                if (CheckRolePermission("成交分部"))
                {
                    string sql = "select count(1) from b_bargain b" +
                                   " left join h_houseinfor h on b.HouseID=h.HouseID" +
                                   " where h.HouseID='" + HID + "' and b.OrgID=" + Employee.Current.OrgID;
                    if (DbHelperSQL.GetSingle(sql).ToInt32() > 0)
                    {
                        btn += "<a href=\"House/BringCustomerImg.aspx?BringCustomerID=" + bring_id + "\" target=\"dialog\" width=\"510\" height=\"700\" fresh='true' maxable='false' title=\"跟进证明\" >查看</a>\n";
                    }
                    else
                        btn += "无\n";
                }
            }

            return btn.ToString2();
        }
    }
}