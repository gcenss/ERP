using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HouseMIS.Web.UI;
using HouseMIS.EntityUtils;
using System.Text;
namespace HouseMIS.Web.House
{
    public partial class ChangeTelList : EntityListBase<TelChange>
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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
            if (!GetMySearchControlValue("Housecode").IsNullOrWhiteSpace())
            {
                if (sb.Length > 0)
                {
                    sb.Append(" AND ");
                }
                sb.AppendFormat(" HouseID IN (SELECT F.HouseID FROM h_houseinfor F WHERE F.shi_id LIKE '%{0}%')", HouseMIS.EntityUtils.StringHelper.Filter(GetMySearchControlValue("Housecode")));
            }
            if (!GetMySearchControlValue("OldTel").IsNullOrWhiteSpace())
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat(" OldTel like '%{0}%' ", GetMySearchControlValue("OldTel"));
            }
            if (!GetMySearchControlValue("NewTel").IsNullOrWhiteSpace())
            {
                if (sb.Length > 0)
                    sb.Append(" AND ");
                sb.AppendFormat(" NewTel like '%{0}%' ", GetMySearchControlValue("NewTel"));
            }
            if (!GetMySearchControlValue("exe_date1").IsNullOrWhiteSpace())
            {
                if (sb.Length > 0)
                {
                    sb.Append(" AND ");
                }
                sb.AppendFormat(" exe_Date >= '{0}'", HouseMIS.EntityUtils.StringHelper.Filter(GetMySearchControlValue("exe_date1")));
            }
            if (!GetMySearchControlValue("exe_date2").IsNullOrWhiteSpace())
            {
                if (sb.Length > 0)
                {
                    sb.Append(" AND ");
                }
                sb.AppendFormat(" exe_Date <= '{0}'", HouseMIS.EntityUtils.StringHelper.Filter(GetMySearchControlValue("exe_date2")));
            }
            return sb.Length == 0 ? null : sb.ToString();
        }
    }
}