using System;
using HouseMIS.EntityUtils;
using HouseMIS.Web.UI;
using TCode;
using HouseMIS.EntityUtils.DBUtility;

namespace HouseMIS.Web.House
{
    public partial class HouseTimeLimit : EntityListBase<h_HouseTimeLimitMsg>
    {


        public string HouseID = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Request.QueryString["HouseID"] != null)
                {
                    HouseID = Request.QueryString["HouseID"];
                    string sql = string.Format(@"select ID,ee.EmployeeID,ee.em_name as name1,ee1.em_name as name2,Money,ImgPic,hh.exe_date,he.Name state,HouseID,hh.Remark from h_HouseTimeLimitMsg hh
                                                    left join e_Employee ee  on hh.EmployeeID=ee.EmployeeID
                                                    left join e_Employee ee1 on hh.ZRempID=ee1.EmployeeID
                                                    left join h_EntrustType he on hh.HouseState=he.EntrustTypeID
                                                    where HouseID={0}",
                                                    HouseID);
                    DK.DataSource = h_HouseTimeLimitMsg.Meta.Query(sql).Tables[0];
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
        protected string customerBringpingzheng(object HID, object op_id, object ID)
        {
                string btn = "";
                if (h_HouseTimeLimitMsg.FindByKey(ID).ImgPic != null)
                { 
                    btn += "<a href=\"House/HouseTimeLimitImg_Bring.aspx?ID=" + ID + "\" target=\"dialog\" width=\"510\" height=\"700\" fresh='true' maxable='false' title=\"协议照片\" >查看</a>\n";
                }
                else
                {
                    btn += "无\n";
                }
                return btn.ToString2();
        }
    }
}