using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HouseMIS.Web.UI;
using System.Data;
using HouseMIS.EntityUtils.DBUtility;
using HouseMIS.EntityUtils;

namespace HouseMIS.Web.House
{
    public partial class HouseLYList : EntityFormBase<h_houseinfor_ZBCheck>
    {
        private int _houseID = 0;
        private int houseID
        {
            get
            {
                if (Request.QueryString["houseID"] != null)
                {
                    _houseID = Convert.ToInt32(Request.QueryString["houseID"]);
                }

                return _houseID;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pageForm.Action += "?houseID=" + Request.QueryString["houseID"];
                BindData();
            }
        }

        private void BindData()
        {
            string sql = string.Format(@"SELECT A.recordUrl                         AS a,
                                                    C.Name + '(' + B.em_id + '-' + B.em_name + ')' + '  '
                                                    + CONVERT(VARCHAR, createTime, 120) AS c,
                                                    A.phoneID,
                                                    A.employeeID,
                                                    source,
                                                    a.talktime - a.realSecond           AS startTime,
                                                    a.realSecond,
                                                    Isnull(d.IsCheck, 0)                AS IsCheck
                                            FROM   i_InternetPhone A
                                                    LEFT JOIN e_Employee B
                                                            ON A.employeeID = B.EmployeeID
                                                    LEFT JOIN s_Organise C
                                                            ON B.OrgID = C.OrgID
                                                    LEFT JOIN h_RecordClose d
                                                            ON a.phoneID = d.phoneID
                                            WHERE  houseID = {0}
                                                    AND a.employeeID = {1}
                                                    AND a.talktime>0
                                                    AND A.recordUrlDel = 1
                                            ORDER  BY a.createTime DESC",
                                            houseID,
                                            Current.EmployeeID);

            DataSet ds = DbHelperSQL.Query(sql);

            if (ds.Tables[0].Rows.Count > 0)
            {
                hf_ly.DataSource = ds;
                hf_ly.DataBind();
            }
            else
            {
                hf_ly.Visible = false;
                lbltxt.Visible = true;
            }
        }

        protected override void OnSaving(object sender, EntityFormEventArgs e)
        {
            if (Request.Form["rb1"] != null)
            {
                //查找此房源有没有 有录音待审核的记录，如果有，则无法申请开盘
                string sql = string.Format(@"SELECT count(*)
                                                FROM h_houseinfor_ZBCheck
                                                WHERE phoneid is NOT null
                                                        AND audit_Date is null
                                                        AND isDel=0
                                                        AND houseid={0}",
                                            houseID);
                if (Convert.ToInt32(DbHelperSQL.GetSingle(sql)) > 0)
                {
                    ShowMsg(AlertType.error, "保存失败，已有其他经纪人上传开盘录音");
                    return;
                }

                h_houseinfor_ZBCheck entity = new h_houseinfor_ZBCheck();
                entity.houseID = houseID;
                entity.exe_Date = DateTime.Now;
                entity.employeeID = Convert.ToInt32(Current.EmployeeID);
                entity.phoneID = Convert.ToInt32(Request.Form["rb1"]);
                entity.state_ZBCheck = (int)CheckState.待认证;
                entity.comID = Current.ComID;

                entity.Insert();

                //找出此房源所有未审核，未上传开盘录音的记录，将其清空
                List<h_houseinfor_ZBCheck> listZB = h_houseinfor_ZBCheck.FindAll(string.Format(@"select * from h_houseinfor_ZBCheck
		                                                                                                where houseid={0}
		                                                                                                and phoneID is null 
		                                                                                                and employee_auditID is null
		                                                                                                and isDel=0",
                                                                                                    houseID));

                if (listZB.Count > 0)
                {
                    DbHelperSQL.ExecuteSql("update h_houseinfor_ZBCheck set isdel=1 where id in(" + string.Join(",", listZB.Select(x => x.ID).ToArray()) + ")");
                }

                ShowMsg(AlertType.correct, "保存成功");
            }
            else
            {
                ShowMsg(AlertType.error, "保存失败，请先选择录音");
            }
        }
    }
}