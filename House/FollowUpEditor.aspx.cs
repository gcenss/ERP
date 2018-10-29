using System;
using System.Collections.Generic;
using Menu = HouseMIS.EntityUtils.Menu;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HouseMIS.Web.UI;
using HouseMIS.EntityUtils;
using TCode;
using System.Data;

namespace HouseMIS.Web.House
{
    public partial class FollowUpEditor : HouseMIS.Web.UI.EntityFormBase<h_FollowUp>
    {
        /// <summary>
        /// 加载表单前赋值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnSetForm(object sender, EntityFormEventArgs e)
        {
            frmFollowUpTypeID.DataSource = h_FollowUpType.FindAll("select FollowUpTypeID,Name from h_FollowUpType where FollowUpTypeID<9");
            frmFollowUpTypeID.DataTextField = "name";
            frmFollowUpTypeID.DataValueField = "FollowUpTypeID";
            frmFollowUpTypeID.DataBind();
            frmFollowUpTypeID.Items.Insert(0, "");
            //PubFunction.FullDropListData(typeof(h_FollowUpType), frmFollowUpTypeID, "Name", "FollowUpTypeID");
            PubFunction.FullDropListData(typeof(h_FollowUpDic), frmFollowUpDicID, "Name", "LSH");

            DataSet ds = h_FollowUp.Meta.Query(string.Format(@"SELECT d.NAME, 
                                                                        h.shi_id 
                                                                FROM   h_houseinfor h 
                                                                        LEFT JOIN s_housedic d 
                                                                                ON d.housedicid = h.housedicid 
                                                                WHERE  h.houseid = {0} ",
                                                                Entity.HouseID));
            if (ds.Tables[0].Rows.Count > 0)
            {
                frmshi_id.Text = ds.Tables[0].Rows[0]["shi_id"].ToString();
                frmHouseDicName.Text = ds.Tables[0].Rows[0]["Name"].ToString();
            }
            //Session["parms"] = Request.QueryString["HouseID"];
            houseid.Value = Request.QueryString["HouseID"];
            if (Request.QueryString["GJAtype"] != null)
            {
                switch (Request.QueryString["GJAtype"].ToString())
                {
                    case "1": frmFollowUpText.Text = "看房时间："; break;
                    case "2": frmFollowUpText.Text = "房屋情况："; break;
                    case "3": frmFollowUpText.Text = "产证情况："; break;
                    case "4": frmFollowUpText.Text = "光线情况："; break;
                    case "5": frmFollowUpText.Text = "外墙："; break;
                    case "6": frmFollowUpText.Text = "带看人："; break;
                    case "7": frmFollowUpText.Text = "房产计税价："; break;
                    case "8": frmFollowUpText.Text = "婚姻状况："; break;
                    case "9": frmFollowUpText.Text = "年龄段："; break;
                    case "10": frmFollowUpText.Text = "同行名称："; break;
                    case "11": frmFollowUpText.Text = "性格特征："; break;
                    case "12": frmFollowUpText.Text = "特别爱好："; break;
                    case "13": frmFollowUpText.Text = "出售原因："; break;
                    case "14": frmFollowUpText.Text = "房源描述："; break;
                    case "15": frmFollowUpText.Text = "房东照片："; break;
                    case "16": frmFollowUpText.Text = "客户带看："; break;
                    case "17": frmFollowUpText.Text = "全景照片："; break;
                    case "18": frmFollowUpText.Text = "图片上传："; break;
                    case "19": frmFollowUpText.Text = "视频上传："; break;
                    case "20": frmFollowUpText.Text = "钥匙积分留言："; break;
                }
                TR1.Visible = false;
                TR2.Visible = false;
                TR3.Visible = false;
            }
        }

        /// <summary>
        /// 表单提交前执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnSaving(object sender, EntityFormEventArgs e)
        {
            //base.OnSaving(sender, e);
            int count = 0;
            if (frmFollowUpTypeID.SelectedItem.Text != "")
            {
                if (Request.Form["houseid"] != null)
                {
                    Entity.HouseID = Convert.ToDecimal(Request.Form["houseid"]);
                }
                Entity.EmployeeID = Employee.Current.EmployeeID;
                try
                {
                    h_FollowUp.Meta.BeginTrans();
                    Entity.Insert();
                    H_houseinfor.Meta.Execute("update h_houseinfor set FollowUp_Date=getdate(),update_date=getdate() where HouseID=" + Entity.HouseID);
                    if (Request.Form["IsRemind"] != null)
                    {
                        if (Request.Form["frmRemindDate"] != "")
                        {
                            if (Request.Form["frmRemindText"].ToString().Trim() != "")
                            {
                                s_Remind s = new s_Remind();
                                s.Title = "房源跟进提醒-" + frmshi_id.Text;
                                s.Content = Request.Form["frmRemindText"];
                                s.aType = 3;
                                s.RemindDate = Request.Form["frmRemindDate"].ToString().Substring(0, 10);
                                s.RemindTime = Request.Form["frmRemindDate"].ToString().Substring(11, 8);
                                s.IsRead = false;
                                s.IsDelete = true;
                                s.OperatorID = Employee.Current.EmployeeID;
                                s.Insert();
                                //s_Remind.Insert(new string[] { "Title", "Content", "aType", "RemindDate","RemindTime", "IsRead", "IsDelete", "OperatorID" },
                                //    new object[] { "房源跟进提醒-" + frmshi_id.Text, Request.Form["frmRemindText"], 3, Request.Form["frmRemindDate"].ToString().Substring(0, 10), Request.Form["frmRemindDate"].ToString().Substring(11, 8), false, true, Employee.Current.EmployeeID });

                                Response.Write(success(Convert.ToDateTime(Request.Form["frmRemindDate"]), s.RemindID.ToString()));

                                count = 1;
                                //JSDo_UserCallBack_Success(success(Convert.ToDateTime(Request.Form["frmRemindDate"]), s.RemindID.ToString()), "");
                            }
                            else
                            {
                                //JSDo_UserCallBack_Error("", "请填写提醒内容!");
                                Response.Write("<script>alertMsg.info('请填写提醒内容!');</script>");

                                count = 1;
                            }
                        }
                        else
                        {
                            //JSDo_UserCallBack_Error("", "请选择提醒时间!");
                            Response.Write("<script>alertMsg.info('请选择提醒时间!');</script>");

                            count = 1;
                        }
                    }
                    h_FollowUp.Meta.Commit();
                }
                catch
                {
                    h_FollowUp.Meta.Rollback();
                    Response.Write(error());
                    //取消执行
                    e.Cancel = true;
                    return;
                    //JSDo_UserCallBack_Error("", "操作失败!");
                }
                if (count == 1)
                {
                    //取消执行
                    e.Cancel = true;
                    return;
                }
            }
            else
            {
                Response.Write("<script>alertMsg.info('请选择跟进分类!');</script>");
                //取消执行
                e.Cancel = true;
                return;
                //JSDo_UserCallBack_Error("", "请选择跟进分类!");
            }
        }

        public string success(DateTime notRemindSzTime, string notRemindSzId)
        {
            return "{\"statusCode\":\"200\", \"message\":\"操作成功!\", \"notRemindSzTime\":\"" + notRemindSzTime + "\", \"notRemindSzId\":\"" + notRemindSzId + "\"}";
        }

        public string error()
        {
            return "{\"statusCode\":\"300\", \"message\":\"操作失败!\"}";
        }
    }
}