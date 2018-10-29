using HouseMIS.EntityUtils;
using HouseMIS.EntityUtils.DBUtility;
using HouseMIS.Web.UI;
using System;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;

namespace HouseMIS.Web.House
{
    public partial class HouseFP : EntityListBase<H_houseinfor>
    {
        /// <summary>
        /// 所有有房源首录人的房源
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [AjaxPro.AjaxMethod]
        public string GetEmployee(int value)
        {
            //SELECT e.EmployeeID,
            //                                                       e.em_name
            //                                                FROM   h_houseinfor h
            //                                                       LEFT JOIN e_Employee e
            //                                                              ON h.OwnerEmployeeID = e.EmployeeID
            //                                                WHERE h.OrgID = { 0}
            //AND DelType = 0
            //                                                GROUP BY e.EmployeeID,
            //                                                          e.em_name

            //过滤 非离职员工
            DataSet ds = DbHelperSQL.Query(string.Format(@"SELECT e.employeeid, 
                                                                   e.em_name 
                                                            FROM   e_employee e 
                                                            WHERE  stateid = 3
                                                                   AND orgid = {0} 
                                                            GROUP  BY e.employeeid, 
                                                                      e.em_name ",
                                                            value));
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

        /// <summary>
        /// 获取需要分派的员工，过滤当前登陆人
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [AjaxPro.AjaxMethod]
        public string GetEmployees(decimal value)
        {
            DataSet ds = DbHelperSQL.Query(string.Format(@"SELECT EmployeeID,
                                                                   em_name
                                                            FROM   e_Employee
                                                            WHERE  StateID = 1
                                                                   AND OrgID = {0}
                                                                   AND EmployeeID<>{1}",
                                                            value,
                                                            Current.EmployeeID));
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
            if (CheckRolePermission("分派", RangeType.所有))
            {
                FullCanViewOrgShopDropList(OrgSelF);
                FullCanViewOrgShopDropList(OrgSelMF);
            }
            else
            {
                FullCanViewOrgShopDropList(OrgSelF, "分派");
                FullCanViewOrgShopDropList(OrgSelMF, "分派");
            }

            PubFunction.FullDropListData(typeof(h_State), sfrmStateID, "Name", "StateID", "");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(HouseFP), Page);
            if (!IsPostBack)
            {
                if (Request.QueryString["OrgSel"] != null)
                {
                    OrgSelF.SelectedValue = Request.QueryString["OrgSel"].ToString();
                    string[] E_List = GetEmployee(OrgSelF.SelectedValue.ToInt32()).Split(',');
                    EmployeeSelF.Items.Clear();
                    for (int i = 0; i < E_List.Length; i++)
                    {
                        string[] EC_List = E_List[i].Split('|');
                        EmployeeSelF.Items.Add(new ListItem(EC_List[1], EC_List[0]));
                    }
                    EmployeeSelF.SelectedValue = Request.QueryString["EmployeeSel"].ToString();
                }
                if (Request["ids"] != null && Request.QueryString["EmployeeID"] != null)
                {
                    string idlist = Request["ids"].ToString();
                    string sql = string.Empty;
                    int result = 0;
                    if (idlist.IndexOf(",") > 0)
                    {
                        string[] spid = idlist.Split(',');
                        foreach (string str in spid)
                        {
                            result = H_houseinfor.AssignHouse(Convert.ToDecimal(str), Convert.ToDecimal(Request.QueryString["EmployeeID"]), Employee.Current.EmployeeID);
                        }
                    }
                    else
                    {
                        result = H_houseinfor.AssignHouse(Convert.ToDecimal(idlist), Convert.ToDecimal(Request.QueryString["EmployeeID"]), Employee.Current.EmployeeID);
                    }
                    if (result > 0)
                    {
                        JSDo_UserCallBack_Success(" formFind();$(\".House_FP:eq(0)\").submit();", "操作成功");
                    }
                }
                else if (Request.QueryString["HouseID"] != null && Request.QueryString["EmployeeID"] != null)
                {
                    decimal HouseID = Convert.ToDecimal(Request.QueryString["HouseID"]);
                    int result = H_houseinfor.AssignHouse(HouseID, Convert.ToDecimal(Request.QueryString["EmployeeID"]), Employee.Current.EmployeeID);
                    if (result > 0)
                    {
                        JSDo_UserCallBack_Success(" formFind();$(\".House_FP:eq(0)\").submit();", "操作成功");
                    }
                }
            }
        }

        protected override string GetWhereClauseFromSearchBar(string typeName)
        {
            StringBuilder sb = new StringBuilder();
            //sb.Append(base.GetWhereClauseFromSearchBar(typeName));
            sb.AppendFormat(" DelType=0");
            //sb.Append(" AND " + GetRolePermissionOrgIds("分派", "OrgID"));
            //如果不是所有权限，则只查询本人和本店所有非在职员工的房源
            if (GetRolePermission("分派") != RangeType.所有)
            {
                sb.AppendFormat(@" and stateid=2 and OrgID={1} AND (OwnerEmployeeID={0} or OwnerEmployeeID in(select EmployeeID from e_Employee
                                                                                                where stateid <> 1
                                                                                                and OrgID ={1}))",
                                                                                    Current.EmployeeID,
                                                                                    Current.OrgID);
            }

            //选择员工后查询
            string parms = Request.QueryString["OrgSel"];
            if (!parms.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" AND OrgID= " + parms);
            }
            //选择员工后查询
            parms = Request.QueryString["EmployeeSel"];
            if (!parms.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" AND OwnerEmployeeID= " + parms);
            }

            //门店
            parms = Request.Form["OrgSelF"];
            if (!parms.IsNullOrWhiteSpace() && parms != "0")
            {
                sb.AppendFormat(" AND OrgID= " + parms);
            }

            //员工查询
            parms = Request.Form["EmployeeSelF"];
            if (!parms.IsNullOrWhiteSpace() && parms != "0")
            {
                sb.AppendFormat(" AND OwnerEmployeeID= " + parms);
            }

            parms = Request.Form["SHshi_id"];
            if (!parms.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" AND shi_id like '%" + parms + "%'");
            }

            parms = GetMySearchControlValue("exe_date1");
            if (!parms.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" AND exe_date>='{0}' ", parms);
            }

            parms = GetMySearchControlValue("exe_date2");
            if (!parms.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" AND exe_date<='{0} 23:59:59' ", parms);
            }

            parms = GetMySearchControlValue("StateID");
            if (!parms.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" AND StateID={0}", parms);
            }

            return sb.Length == 0 ? null : sb.ToString();
        }
    }
}