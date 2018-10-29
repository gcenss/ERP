using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HouseMIS.Web.UI;
using HouseMIS.EntityUtils;
using TCode;
using System.Text;
using AjaxPro;
namespace HouseMIS.Web.House
{
    public partial class HouseHS : EntityListBase<H_houseinfor>
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            gv.RowCreated += new GridViewRowEventHandler(gv_RowCreated);
        }
        /// <summary>
        /// gv行创建事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void gv_RowCreated(object sender, GridViewRowEventArgs e)
        {
            //如果是数据行
            if (e.Row.RowType == DataControlRowType.DataRow) ////注意： DataKeys里能取到的值，来前台页面的GridView中 DataKeyNames设置的值，多个可用逗号隔开
                e.Row.Attributes.Add("ondblclick", "OpenHouseEdit('" + gv.DataKeys[e.Row.RowIndex]["HouseID"].ToString() + "','" + gv.DataKeys[e.Row.RowIndex]["shi_id"].ToString() + "')");
        }
        /// <summary>
        /// 页面状态下拉框绑定
        /// </summary>
        /// <returns></returns>
        //public string HouseState()
        //{
        //    IEntityList ls = h_State.FindAll();
        //    string s = "";
        //    if (ls != null)
        //    {
        //        for (int i = 0; i < ls.Count; i++)
        //        {
        //            s = s + "<option " + ls[i]["StateID"].ToString() + ">" + ls[i]["Name"].ToString() + "</option>";
        //        }
        //    }
        //    return s;
        //}
        [AjaxPro.AjaxMethod]
        public string GetEmployee(decimal value)
        {
            DataSet ds = Employee.Meta.Query("select e.EmployeeID,e.em_name from h_houseinfor h left join e_Employee e on h.OwnerEmployeeID=e.EmployeeID where h.OrgID=" + value + " group by e.EmployeeID,e.em_name");
            StringBuilder sb = new StringBuilder();
            if (ds.Tables[0].Rows.Count > 0)
            {
                sb.Append("0|请选择");
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    sb.Append("," + dr["EmployeeID"].ToString() + "|" + dr["em_name"].ToString());
                }
                return sb.ToString().Substring(1);
            }
            else
            {
                sb.Append("|该分部无员工");
                return sb.ToString();
            }
        }


        [AjaxPro.AjaxMethod]
        public string GetEmployees(decimal value)
        {
            DataSet ds = Employee.Meta.Query("select EmployeeID,em_name from e_Employee where StateID=1 and OrgID=" + value);
            StringBuilder sb = new StringBuilder();
            if (ds.Tables[0].Rows.Count > 0)
            {
                sb.Append("0|请选择");
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    sb.Append("," + dr["EmployeeID"].ToString() + "|" + dr["em_name"].ToString());
                }
                return sb.ToString().Substring(1);
            }
            else
            {
                sb.Append("|该分部无员工");
                return sb.ToString();
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            FullCanViewOrgShopDropList(this.OrgSel, null);
            this.OrgSel.Items.Insert(0, new ListItem("", ""));
            FullCanViewOrgShopDropList(this.OrgSelM, null);
        }
        private void FullDropListData(Type aType, DropDownList d, string Name, string Value, string key, string keyValue)
        {
            var op = EntityFactory.CreateOperate(aType);
            IEntityList ls = op.FindAll(key, keyValue);
            d.Items.Add(new ListItem("", "0"));
            for (int i = 0; i < ls.Count; i++)
            {
                if (ls[i][Name].ToString() != "0")
                {
                    d.Items.Add(new ListItem(ls[i][Name].ToString(), ls[i][Value].ToString()));
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AjaxPro.Utility.RegisterTypeForAjax(typeof(HouseMIS.Web.House.HouseHS), this.Page);
                if (!CheckRolePermission("查看"))
                {
                    Response.End();
                }
                if (Request.QueryString["OrgSel"] != null)
                {
                    this.OrgSel.SelectedValue = Request.QueryString["OrgSel"].ToString();
                    string[] E_List = GetEmployee(Convert.ToDecimal(this.OrgSel.SelectedValue)).Split(',');
                    this.EmployeeSel.Items.Clear();
                    for (int i = 0; i < E_List.Length; i++)
                    {
                        string[] EC_List = E_List[i].Split('|');
                        this.EmployeeSel.Items.Add(new ListItem(EC_List[1], EC_List[0]));
                    }
                    this.EmployeeSel.SelectedValue = Request.QueryString["EmployeeSel"].ToString();
                }
                if (Request["ids"] != null && Request.QueryString["EmployeeID"] != null)
                {
                    string BillCode = "";
                    if (Request.QueryString["Nshi_id"] != null && Request.QueryString["Nshi_id"] != "")
                    {
                        BillCode = Request.QueryString["Nshi_id"].ToString();
                    }
                    else
                    {
                        BillCode = s_Organise.Find("OrgID", Request.QueryString["OrgID"]).BillCode;
                    }
                    
                    string idlist = Request["ids"].ToString();
                    int result = 0;
                    if (idlist.IndexOf(",") > 0)
                    {
                        string[] spid = idlist.Split(',');
                        foreach (string str in spid)
                        {
                            string NewCode = H_houseinfor.NewHouseCode(BillCode, Employee.Current.ComID.ToString());// H_houseinfor.Meta.Query("exec  dbo.p_h_GetHouseCode " + Request.QueryString["OrgID"] + ",'" + BillCode + "'").Tables[0].Rows[0][0].ToString();
                            result = H_houseinfor.Meta.Execute("update h_houseinfor set OwnerEmployeeID=" + Request.QueryString["EmployeeID"].ToString() + ",OrgID=" + Request.QueryString["OrgID"] + ",shi_id='" + NewCode + "' where HouseID=" + str);
                        }
                    }
                    else
                    {
                        string NewCode = H_houseinfor.NewHouseCode(BillCode, Employee.Current.ComID.ToString());// H_houseinfor.Meta.Query("exec  dbo.p_h_GetHouseCode " + Request.QueryString["OrgID"] + ",'" + BillCode + "'").Tables[0].Rows[0][0].ToString();
                        result = H_houseinfor.Meta.Execute("update h_houseinfor set OwnerEmployeeID=" + Request.QueryString["EmployeeID"].ToString() + ",OrgID=" + Request.QueryString["OrgID"] + ",shi_id='" + NewCode + "' where HouseID=" + idlist);
                    }
                    JSDo_UserCallBack_Success(" formFind();$(\".House_HS:eq(0)\").submit();", "操作成功");
                }
                else if (Request.QueryString["HouseID"] != null && Request.QueryString["EmployeeID"] != null)
                {
                    string HouseID = Request.QueryString["HouseID"].ToString();
                    string BillCode = "";
                    if (Request.QueryString["Nshi_id"] != null && Request.QueryString["Nshi_id"] != "")
                    {
                        BillCode = Request.QueryString["Nshi_id"].ToString();
                    }
                    else
                    {
                        BillCode = s_Organise.Find("OrgID", Request.QueryString["OrgID"]).BillCode;
                    }
                    string NewCode = H_houseinfor.NewHouseCode(BillCode, Employee.Current.ComID.ToString());// H_houseinfor.Meta.Query("exec  dbo.p_h_GetHouseCode " + Request.QueryString["OrgID"] + ",'" + BillCode + "'").Tables[0].Rows[0][0].ToString();
                    int result = H_houseinfor.Meta.Execute("update h_houseinfor set OwnerEmployeeID=" + Request.QueryString["EmployeeID"].ToString() + ",OrgID=" + Request.QueryString["OrgID"] + ",shi_id='" + NewCode + " where HouseID=" + HouseID);
                    if (result > 0)
                    {
                        JSDo_UserCallBack_Success(" formFind();$(\".House_HS:eq(0)\").submit();", "操作成功");
                    }
                }
            }

        }
        protected override string GetWhereClauseFromSearchBar(string typeName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.GetWhereClauseFromSearchBar(typeName));
            if (Request.QueryString["OrgSel"] != null)
            {
                string parms = Request.QueryString["OrgSel"];
                if (parms != null)
                {

                    if (sb.Length > 0)
                    {
                        sb.Append("AND ");
                    }
                    sb.AppendFormat(" OrgID= " + parms);
                }
            }
            if (Request.Form["OrgSel"] != "")
            {
                string parms = Request.Form["OrgSel"];
                if (parms != null)
                {
                    if (sb.Length > 0)
                    {
                        sb.Append("AND ");
                    }
                    sb.AppendFormat(" OrgID= " + parms);
                }
            }
            if (Request.QueryString["EmployeeSel"] != null)
            {
                string parms = Request.QueryString["EmployeeSel"];
                if (parms != null)
                {
                    if (sb.Length > 0)
                    {
                        sb.Append("AND ");
                    }
                    sb.AppendFormat(" OwnerEmployeeID= " + parms);
                }
            }
            if (Request.Form["EmployeeSel"] != "")
            {
                string parms = Request.Form["EmployeeSel"];
                if (parms != null)
                {
                    if (sb.Length > 0)
                    {
                        sb.Append("AND ");
                    }
                    sb.AppendFormat(" OwnerEmployeeID= " + parms);
                }
            }
            if (Request.Form["SHshi_id"] != "")
            {
                string parms = Request.Form["SHshi_id"];
                if (parms != null)
                {
                    if (sb.Length > 0)
                    {
                        sb.Append("AND ");
                    }
                    sb.AppendFormat(" shi_id like '%" + parms + "%'");
                }
            }
            if (sb.Length <= 0)
            {
                sb.Append(" 1=2 ");
            }
            if (sb.Length > 0)
                sb.Append(" AND ");
            sb.AppendFormat(" IsNull(bID,0)<>1");
            return sb.Length == 0 ? null : sb.ToString();
        }
    }
}