using HouseMIS.EntityUtils;
using HouseMIS.EntityUtils.DBUtility;
using HouseMIS.Web.UI;
using System;
using System.Text;

namespace HouseMIS.Web.House
{
    public partial class HouseKeyList : EntityListBase<h_HouseKey>
    {
        protected string ToolBtn()
        {
            StringBuilder sb = new StringBuilder();
            if (CheckRolePermission("删除钥匙"))
            {
                sb.Append("<li><a class=\"delete\" href=\"House/HouseKeyList.aspx?NavTabId=" + NavTabId + "&doType=del\" rel=\"idsHouseKeyID\" target=\"selectedTodo\" title=\"确定要删除吗 ? \"><span>删除</span></a></li>");
            }

            return sb.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["doType"] != null && Request.QueryString["doType"] == "del")
                {
                    if (!string.IsNullOrEmpty(Request["idsHouseKeyID"]))
                    {
                        foreach (string keyID in Request["idsHouseKeyID"].Split(','))
                        {
                            h_HouseKey hk = h_HouseKey.FindByHouseKeyID(Convert.ToDecimal(keyID));
                            hk.isDel = true;
                            hk.Update();

                            //查找这个房源是否还有钥匙
                            if (Convert.ToInt32(DbHelperSQL.GetSingle(string.Format(@"SELECT count(*)
                                                                                        FROM h_HouseKey
                                                                                        WHERE IsIn=1
                                                                                                AND HouseID={0}
                                                                                                AND isDel=0",
                                                                                        hk.HouseID))) == 0)
                            {
                                H_houseinfor.Meta.Execute("UPDATE h_houseinfor SET HasKey = 0 WHERE HouseID = " + hk.HouseID);
                            }
                        }
                        JSDo_UserCallBack_Success(" formFind();$(\".HKeyList:eq(0)\").submit();", "操作成功");
                    }
                    else
                    {
                        JSDo_UserCallBack_Error(" formFind();$(\".HKeyList:eq(0)\").submit();", "请选择信息");
                    }
                }
            }
        }

        /// <summary>
        /// 重写查找条件
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        protected override string GetWhereClauseFromSearchBar(string typeName)
        {
            StringBuilder sb = new StringBuilder();
            //sb.Append(base.GetWhereClauseFromSearchBar(typeName));
            sb.Append(" isDel=0");
            sb.Append(" and HouseID=" + Request.QueryString["HouseID"]);

            return sb.Length == 0 ? null : sb.ToString();
        }
    }
}