using HouseMIS.EntityUtils;
using HouseMIS.Web.UI;
using System;
using System.Text;

namespace HouseMIS.Web.House
{
    public partial class RecordsList : EntityListBase<i_InternetPhone>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //FullSubGroup();
            }
        }

        protected override string GetWhereClauseFromSearchBar(string typeName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.GetWhereClauseFromSearchBar(typeName));
            //if (!GetMySearchControlValue("SubGroupID").IsNullOrWhiteSpace())
            //{
            //    if (sb.Length > 0)
            //        sb.Append(" AND ");
            //    sb.AppendFormat("HouseID in (select HouseID from h_houseinfor where OrgID in (select OrgID from e_SubGroupDetail where SubGroupID={0}))", HouseMIS.EntityUtils.StringHelper.Filter(GetMySearchControlValue("SubGroupID")));
            //}
            if (!GetMySearchControlValue("Housecode").IsNullOrWhiteSpace())
            {
                if (sb.Length > 0)
                {
                    sb.Append(" AND ");
                }
                sb.AppendFormat(" houseID in (select HouseID from h_houseinfor where shi_id like '%{0}%')", HouseMIS.EntityUtils.StringHelper.Filter(GetMySearchControlValue("name")));
            }
            if (!GetMySearchControlValue("name").IsNullOrWhiteSpace())
            {
                if (sb.Length > 0)
                {
                    sb.Append(" AND ");
                }
                sb.AppendFormat(" employeeID in (select EmployeeID from e_Employee where em_id='{0}')", HouseMIS.EntityUtils.StringHelper.Filter(GetMySearchControlValue("name")));
            }
            return sb.ToString();
        }

        //protected void FullSubGroup()
        //{
        //    EntityList<e_SubGroup> list = e_SubGroup.FindAll();
        //    myffrmSubGroupID.Items.Add(new ListItem("", ""));
        //    foreach (e_SubGroup es in list)
        //    {
        //        myffrmSubGroupID.Items.Add(new ListItem(es.People, es.SubGroupID.ToString()));
        //    }
        //}
    }
}