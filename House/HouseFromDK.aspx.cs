using System;
using HouseMIS.EntityUtils;
using HouseMIS.Web.UI;
using TCode;
using HouseMIS.EntityUtils.DBUtility;

namespace HouseMIS.Web.House
{
    public partial class HouseFromDK : EntityListBase<c_BringCustomer>
    {
        public string HouseID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["HouseID"] != null)
                {
                    HouseID = Request.QueryString["HouseID"];
                    string sql = string.Format(@"SELECT e.em_name as btName,ee.em_name as pkName,cc.Cus_id,h.shi_id,C.Remarks,
                                                    c.HID,case cc.PubliceType when '0' then '私客' else '公客' end as isGongKe,
                                                    c.OperatorID,c.BringCustomerID,c.exe_Date
                                                    FROM c_BringCustomer C
                                                    LEFT JOIN c_Customer cc on cc.CustomerID = C.CID
                                                    LEFT JOIN h_houseinfor h on h.HouseID = C.HID
                                                    LEFT JOIN e_Employee e on e.EmployeeID = C.BeltmanID
                                                    LEFT JOIN e_Employee ee on ee.EmployeeID = C.EscortEmployeeID1
                                                    where C.HID ={0}
                                                    and (cc.PubliceType=1 
                                                        or (cc.PubliceType=0 {1})
                                                        )
                                                    order by C.exe_date desc",
                                                    HouseID,
                                                    "and " + GetRolePermissionEmployeeIds("查看带看", "c.OperatorID"));
                    DK.DataSource = c_BringCustomer.Meta.Query(sql).Tables[0];
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