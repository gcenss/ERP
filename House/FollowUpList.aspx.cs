using HouseMIS.EntityUtils;
using HouseMIS.EntityUtils.DBUtility;
using HouseMIS.Web.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;

namespace HouseMIS.Web.House
{
    public partial class FollowUpList : EntityListBase<h_FollowUp>
    {
        [AjaxPro.AjaxMethod]
        public string GetEmployeeFF(decimal value)
        {
            DataSet ds = DbHelperSQL.Query("select EmployeeID,em_name from e_Employee where StateID=1 and OrgID=" + value);
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckRolePermission("查看"))
                Response.End();

            Dictionary<string, string> dys = ToolBar();
            if (!CheckRolePermission("删除"))
            {
                RemovBtns(dys, "delete");
            }
            tBar = GetBtns(dys).ToString();

            if (!IsPostBack)
            {
                AjaxPro.Utility.RegisterTypeForAjax(typeof(FollowUpList), this.Page);
                GetBindDDL();

                FullCanViewOrgShopDropList(OrgSelFF);
                if (Request.QueryString["OrgSel"] != null)
                {
                    this.OrgSelFF.SelectedValue = Request.QueryString["OrgSel"].ToString();
                    string[] E_List = GetEmployeeFF(Convert.ToDecimal(this.OrgSelFF.SelectedValue)).Split(',');
                    this.EmployeeSelFF.Items.Clear();
                    for (int i = 0; i < E_List.Length; i++)
                    {
                        string[] EC_List = E_List[i].Split('|');
                        this.EmployeeSelFF.Items.Add(new ListItem(EC_List[1], EC_List[0]));
                    }
                    this.EmployeeSelFF.SelectedValue = Request.QueryString["EmployeeSel"].ToString();
                }
            }
        }

        public string tBar = "";

        private Dictionary<string, string> RemovBtns(Dictionary<string, string> dys, string key)
        {
            dys.Remove(key);
            return dys;
        }

        private StringBuilder GetBtns(Dictionary<string, string> newdys)
        {
            StringBuilder sbBtns = new StringBuilder();
            foreach (KeyValuePair<string, string> kvp in newdys)
            {
                sbBtns.Append(kvp.Value);
            }
            return sbBtns;
        }

        private Dictionary<string, string> ToolBar()
        {
            List<string> bts = new List<string>();
            bts.Add("<li><a class='delete' href='House/FollowForm.aspx?NavTabId=" + NavTabId + "&doAjax=true&doType=delall' rel='Followids' target='selectedTodo' title='确定要删除吗?'><span>删除</span></a></li>");
            Dictionary<string, string> dys = new Dictionary<string, string>();
            dys.Add("delete", bts[0]);
            return dys;
        }

        /// <summary>
        /// 绑定下拉框
        /// </summary>
        protected void GetBindDDL()
        {
            ffrmFollowUpTypeID.DataSource = h_FollowUpType.FindAllWithCache();
            ffrmFollowUpTypeID.DataTextField = "Name";
            ffrmFollowUpTypeID.DataValueField = "FollowUpTypeID";
            ffrmFollowUpTypeID.DataBind();
            ffrmFollowUpTypeID.Items.Insert(0, new ListItem(""));
        }

        /// <summary>
        /// 重写查找条件
        ///  </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        protected override string GetWhereClauseFromSearchBar(string typeName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.GetWhereClauseFromSearchBar(typeName));
            if (sb.Length == 0)
            {
                sb.Append("1=1");
            }

            sb.Append(" AND " + GetRolePermissionEmployeeIds("查看", "EmployeeID"));

            string opertype = Request.QueryString["OperType"];
            if (opertype != null)
            {
                sb.Append(" AND EmployeeID = " + Employee.Current.EmployeeID);
            }

            string temp = GetMySearchControlValue("Housecode");
            if (!temp.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" AND HouseID IN (SELECT F.HouseID FROM h_houseinfor F WHERE F.shi_id='{0}')", temp);
            }

            temp = GetMySearchControlValue("OutDate1");
            if (!temp.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" AND exe_Date >= '{0}'", temp);
                myffrmOutDate1.Text = temp;
            }

            temp = GetMySearchControlValue("OutDate2");
            if (!temp.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" AND exe_Date<= '{0} 23:59:59'", temp);
                myffrmOutDate2.Text = temp;
            }

            temp = GetMySearchControlValue("aType");
            if (!temp.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" AND HouseID in (select HouseID from h_Houseinfor where aType={0})", temp);
            }

            string OrgSelStr = "";
            if (Request.QueryString["OrgSel"] != null)
            {
                OrgSelStr = Request.QueryString["OrgSel"];
            }
            else if (Request.Form["OrgSelFF"] != "" && Request.Form["OrgSelFF"] != null)
            {
                OrgSelStr = Request.Form["OrgSelFF"];
            }
            if (!OrgSelStr.IsNullOrWhiteSpace() && OrgSelStr != "0")
            {
                sb.AppendFormat(" AND EmployeeID IN (SELECT E.EmployeeID FROM e_Employee E WHERE E.OrgID = " + OrgSelStr + ") ");
            }

            string EmployeeSelStr = "";
            if (Request.QueryString["EmployeeSel"] != null)
            {
                EmployeeSelStr = Request.QueryString["EmployeeSel"];
            }
            else if (Request.Form["EmployeeSelFF"] != "" && Request.Form["EmployeeSelFF"] != null)
            {
                EmployeeSelStr = Request.Form["EmployeeSelFF"];
            }
            if (!EmployeeSelStr.IsNullOrWhiteSpace() && EmployeeSelStr != "0")
            {
                sb.AppendFormat(" AND EmployeeID= " + EmployeeSelStr);
            }

            if (!Request.Params["EmployeeID"].IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" AND EmployeeID=" + this.Request.Params["EmployeeID"]);
            }

            return sb.Length == 0 ? null : sb.ToString();
        }
    }
}