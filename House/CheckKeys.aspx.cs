using HouseMIS.EntityUtils.DBUtility;
using System;
using System.Data;

namespace HouseMIS.Web.House
{
    public partial class CheckKeys : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string NavTabId = Request.QueryString["NavTabId"];
                string HouseID = Request.QueryString["HouseID"];
                DataTable dt = DbHelperSQL.Query(string.Format(@"SELECT TOP 1 H.IsIn,
                                                                             (S.BillCode+'-'+S.Name) AS Name,H.InOhterCompany,H.Remarks,S.Tel,H.exe_date,H.TEL T,O.OhterCompanyName,H.IsLandran
                                                                    FROM h_HouseKey H
                                                                    LEFT JOIN s_Organise S
                                                                        ON S.OrgID = H.OrgID
                                                                    LEFT JOIN h_OhterCompanyName O
                                                                        ON O.OhterCompanyNameID = H.OhterCompanyNameID
                                                                    WHERE H.HouseID = {0}
                                                                            AND h.isDel=0
                                                                    ORDER BY  H.exe_date DESC",
                                                                    HouseID)).
                                                                    Tables[0];

                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(dt.Rows[0][0]) == true)
                    {
                        // 其他中介
                        if (dt.Rows[0]["InOhterCompany"].ToString() == "True")
                        {
                            if (dt.Rows[0]["OhterCompanyName"].ToString() != "")
                                str = "钥匙在" + dt.Rows[0]["OhterCompanyName"] + " ;</br>电话:" + dt.Rows[0]["T"] + " ;</br>备注:(" + dt.Rows[0]["Remarks"] + ")</br>时间:" + dt.Rows[0]["exe_date"] + "</br><a rel='HouseKey' title='钥匙记录' style='color:red;' href='House/HouseKeyList.aspx?NavTabId=" + NavTabId + "&doAjax=true&HouseID=" + HouseID + "' target=\"navTab\" width=\"680\" height=\"324\">钥匙记录</a> &nbsp;&nbsp;"; /*< a rel =\"GJF_House\" title=\"添加积分跟进\" style='color:red;' href=\"House/FollowUpEditor.aspx?NavTabId=" + NavTabId + "&doAjax=true&GJAtype=20&HouseID=" + HouseID + "\" target=\"dialog\" width=\"380\" height=\"324\">新增钥匙积分跟进</a>*/
                            else
                                str = "钥匙在其他中介 ;</br>备注:(" + dt.Rows[0]["Remarks"] + ")</br>时间:" + dt.Rows[0]["exe_date"] + "</br><a rel='HouseKey' title='钥匙记录' style='color:red;' href='House/HouseKeyList.aspx?NavTabId=" + NavTabId + "&doAjax=true&HouseID=" + HouseID + "' target=\"navTab\" width=\"680\" height=\"324\">钥匙记录</a> &nbsp;&nbsp;";/*<a rel=\"GJF_House\" title=\"添加积分跟进\" style='color:red;' href=\"House/FollowUpEditor.aspx?NavTabId=" + NavTabId + "&doAjax=true&GJAtype=20&HouseID=" + HouseID + "\" target=\"dialog\" width=\"380\" height=\"324\">新增钥匙积分跟进</a>*/
                        }
                        // 选择了房东
                        else if (dt.Rows[0]["IsLandran"].ToString() == "1")
                        {
                            str = "钥匙在房东手上 ;</br>备注:(" + dt.Rows[0]["Remarks"] + ")</br>时间:" + dt.Rows[0]["exe_date"] + "</br><a rel='HouseKey' title='钥匙记录' style='color:red;' href='House/HouseKeyList.aspx?NavTabId=" + NavTabId + "&doAjax=true&HouseID=" + HouseID + "' target=\"navTab\" width=\"680\" height=\"324\">钥匙记录</a> &nbsp;&nbsp;";/*<a rel=\"GJF_House\" title=\"添加积分跟进\" style='color:red;' href=\"House/FollowUpEditor.aspx?NavTabId=" + NavTabId + "&doAjax=true&GJAtype=20&HouseID=" + HouseID + "\" target=\"dialog\" width=\"380\" height=\"324\">新增钥匙积分跟进</a>*/
                        }
                        else
                        {
                            str = "钥匙在:" + dt.Rows[0][1];
                            if (dt.Rows[0][4] != null && !dt.Rows[0][4].ToString().IsNullOrWhiteSpace())
                            {
                                str += "。</br>";
                                str += "分部电话:" + dt.Rows[0][4];
                                str += "</br>时间:" + dt.Rows[0]["exe_date"];
                                str += "</br><a rel='HouseKey' title='钥匙记录' style='color:red;' href='House/HouseKeyList.aspx?NavTabId=" + NavTabId + "&doAjax=true&HouseID=" + HouseID + "' target=\"navTab\" width=\"680\" height=\"324\">钥匙记录</a> &nbsp;&nbsp;";/*<a rel=\"GJF_House\" title=\"添加积分跟进\" style='color:red;' href=\"House/FollowUpEditor.aspx?NavTabId=" + NavTabId + "&doAjax=true&GJAtype=20&HouseID=" + HouseID + "\" target=\"dialog\" width=\"380\" height=\"324\">新增钥匙积分跟进</a>*/
                            }
                        }
                    }
                    else
                    {
                        str = "该房源没有拿钥匙";
                        str += "</br><a rel='HouseKey' title='钥匙记录' style='color:red;' href='House/HouseKeyList.aspx?NavTabId=" + NavTabId + "&doAjax=true&HouseID=" + HouseID + "' target=\"navTab\" width=\"680\" height=\"324\">钥匙记录</a> &nbsp;&nbsp;";/*<a rel=\"GJF_House\" title=\"添加积分跟进\" style='color:red;' href=\"House/FollowUpEditor.aspx?NavTabId=" + NavTabId + "&doAjax=true&GJAtype=20&HouseID=" + HouseID + "\" target=\"dialog\" width=\"380\" height=\"324\">新增钥匙积分跟进</a>*/
                    }
                }
                else
                {
                    str = "该房源没有拿钥匙";
                    str += "</br><a rel='HouseKey' title='钥匙记录' style='color:red;' href='House/HouseKeyList.aspx?NavTabId=" + NavTabId + "&doAjax=true&HouseID=" + HouseID + "' target=\"navTab\" width=\"680\" height=\"324\">钥匙记录</a> &nbsp;&nbsp;";/*<a rel=\"GJF_House\" title=\"添加积分跟进\" style='color:red;' href=\"House/FollowUpEditor.aspx?NavTabId=" + NavTabId + "&doAjax=true&GJAtype=20&HouseID=" + HouseID + "\" target=\"dialog\" width=\"380\" height=\"324\">新增钥匙积分跟进</a>*/
                }
            }
        }

        public string str;
    }
}