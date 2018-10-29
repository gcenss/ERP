using System;
using HouseMIS.Web.UI;
using HouseMIS.EntityUtils;
using System.Text;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace HouseMIS.Web.House
{
    public partial class LookTelList : EntityListBase<h_SeeTelLog>
    {
        //s_SysParam hsc;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (!IsPostBack)
            {
                //hsc = s_SysParam.FindByParamCode("HouseStateColor");
                PubFunction.FullDropListData(typeof(h_State), ddlStateID, "Name", "StateID", "");

                List<s_Organise> Organises = s_Organise.FindAllChildsByParent(0).FindAll(x => x.aType != 1 && x.aType != 7);
                mysfrmOrgID.Items.Add(new ListItem("", ""));
                mysfrmOrgID_Emp.Items.Add(new ListItem("", ""));
                foreach (s_Organise Organise in Organises)
                {
                    mysfrmOrgID.Items.Add(new ListItem(Organise.TreeNodeName2 + "(" + Organise.BillCode + ")", Organise.OrgID.ToString()));
                    mysfrmOrgID_Emp.Items.Add(new ListItem(Organise.TreeNodeName2 + "(" + Organise.BillCode + ")", Organise.OrgID.ToString()));
                }
            }
        }

        //protected String GetColStyle(object SeeHouseType)
        //{
        //    if (hsc != null)
        //    {
        //        string[] ss = hsc.Value.Split(',');
        //        foreach (string sv in ss)
        //        {
        //            string[] str = sv.Split('|');
        //            if (str[0] == SeeHouseType.ToString() && str[1] != "")
        //            {
        //                return " style=\"color:#" + str[1] + "\" ";
        //            }
        //        }
        //    }
        //    return null;
        //}

        //protected string GetAtype(object AtypeName)
        //{
        //    if (AtypeName != null && AtypeName.ToString() == "租房")
        //    {
        //        return "blue";
        //    }
        //    if (AtypeName != null && AtypeName.ToString() == "售房")
        //    {
        //        return "red";
        //    }
        //    return "black";
        //}

        protected override string GetWhereClauseFromSearchBar(string typeName)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(base.GetWhereClauseFromSearchBar(typeName));
            if (sb.Length == 0)
            {
                sb.Append("1=1");
            }
            sb.Append(" and " + GetRolePermissionEmployeeIds("查看", "EmployeeID"));

            //房源编号
            string temp = GetMySearchControlValue("shi_id");
            if (!temp.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" AND HouseID = (select HouseID from H_houseinfor where shi_id='{0}')", temp);
            }
            //工号
            temp = GetMySearchControlValue("em_id");
            if (!temp.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" AND EmployeeID = (select EmployeeID from e_Employee where em_id='{0}')", temp);
            }
            //查看日期开始
            temp = GetMySearchControlValue("exe_Date");
            if (!temp.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" AND exe_Date>='{0}'", temp);
                myffrmexe_Date.Text = temp;
            }
            //查看日期结束
            temp = GetMySearchControlValue("exe_Date_end");
            if (!temp.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" AND exe_date<='{0} 23:59:59'", temp);
                myffrmexe_Date_end.Text = temp;
            }
            //查看人
            temp = GetMySearchControlValue("EmployeeID");
            if (!temp.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" AND EmployeeID in(select EmployeeID from e_Employee where em_name like '%{0}%')", temp);
            }

            temp = Request.Form["EmployeeID"];
            if (temp != null)
            {
                sb.AppendFormat(" AND EmployeeID={0}", temp);
            }

            //房源类型
            temp = Request.Form["ddlaType"];
            if (!temp.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" AND HouseID in (select HouseID from H_houseinfor where atype={0} and comid={1})", temp, TCode.YUN.yun_Company.CurrentID);
            }

            //信息类型
            temp = Request.Form["ddlInfotype"];
            if (!temp.IsNullOrWhiteSpace())
            {
                //调门牌
                if (temp == "1")
                    sb.AppendFormat(" and SheBei>1");
                else
                    //调电话
                    sb.AppendFormat(" and SheBei=1");
            }

            //查看门牌是否异常
            temp = Request.Form["ckyc"];
            if (!temp.IsNullOrWhiteSpace())
            {
                if (temp == "0")
                {
                    sb.AppendFormat(" and EmployeeID in (select EmployeeID from e_Alteration where CHARINDEX('查看房源门牌号超出角色限制',Remarks)>0)");
                }
            }

            //状态
            temp = Request.Form["ddlStateID"];
            if (!temp.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" AND HouseID in (select HouseID from H_houseinfor where stateid={0} and comid={1})", temp, TCode.YUN.yun_Company.CurrentID);
            }

            //房源门店
            temp = GetMySearchControlValue("OrgID");
            if (temp.ToInt32() > 0)
            {
                sb.AppendFormat(" AND HouseID in (select HouseID from H_houseinfor where orgid={0})", temp);
            }

            //查看人门店
            temp = GetMySearchControlValue("OrgID_Emp");
            if (temp.ToInt32() > 0)
            {
                sb.AppendFormat(@" AND EmployeeID in (SELECT EmployeeID
                                                        FROM   e_Employee
                                                        WHERE  OrgID IN(SELECT orgid
                                                                        FROM   s_Organise
                                                                        WHERE  IdPath LIKE '%,{0},%'))",
                                temp);
            }

            return sb.ToString();
        }
    }
}