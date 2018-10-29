using HouseMIS.EntityUtils;
using HouseMIS.EntityUtils.DBUtility;
using HouseMIS.Web.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using TCode;

namespace HouseMIS.Web.House
{
    public partial class FollowUpPriceEditor : EntityFormBase<h_PriceFollowUp>
    {
        public decimal HouseID;
        public string em_name;
        public string eid;
        public string seid;
        List<h_RecordClose> list_h_RecordClose = new List<h_RecordClose>();

        protected override void OnPreInit(EventArgs e)
        {
            HouseID = Request["HouseID"].ToDecimal().Value;

            base.OnPreInit(e);
        }

        protected override void OnSetForm(object sender, EntityFormEventArgs e)
        {
            if (Entity.House.HouseID > 0)
            {
                frmOldPrice.Text = Entity.House.Min_price.ToString();
                frmNewPrice.Text = Entity.House.Min_price.ToString();
                frmOldPrice_Sum.Text = Entity.House.Sum_price.ToString();
                frmNewPrice_Sum.Text = Entity.House.Sum_price.ToString();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            form1.Action += "?HouseID=" + HouseID;
            if (!IsPostBack)
            {
                string seeRecord = GetRolePermissionEmployeeIds("查看录音", "EmployeeID");
                string seeRecord_Mask = GetRolePermissionEmployeeIds("查看屏蔽录音", "EmployeeID");

                string sql = string.Format(@"select b.recordUrl as a,
                                                    d.Name+'('+c.em_id+'-'+c.em_name+')'+'  '+CONVERT(varchar,createTime,120) as c,
                                                    b.phoneID,b.employeeID,source
                                                    from i_InternetPhone b
                                                    inner join e_Employee c on b.employeeID=c.EmployeeID
                                                    inner join s_Organise d on d.OrgID=c.OrgID 
                                                    where b.HouseId={0}
                                                    and b.recordUrlDel=1 
                                                    and b.RecrodType=1 
                                                    and b.talktime>0
                                                    and b.EmployeeID in(select EmployeeID  
                                                                        from e_Employee 
                                                                        where {1})
                                                    and b.phoneID not in (select a.phoneID 
                                                                            from i_InternetPhone a
                                                                            inner join h_RecordClose b on a.phoneID = b.phoneID
                                                                            where a.houseID ={0}
                                                                            and b.IsCheck = 1  
                                                                            and a.EmployeeID not in (select EmployeeID 
                                                                                                        from e_Employee
                                                                                                        where {2}))
                                                    order by b.phoneID desc",
                                                    Request.QueryString["HouseID"],
                                                    seeRecord,
                                                    seeRecord_Mask);

                //string sql = string.Format(@"select A.recordUrl as a,C.Name+'('+B.em_id+'-'+B.em_name+')'+'  '+CONVERT(varchar,createTime,120) as c,A.phoneID,A.employeeID,source
                //                                                from i_InternetPhone A
                //                                                left join e_Employee B on A.employeeID=B.EmployeeID
                //                                                left join s_Organise C on B.OrgID=C.OrgID
                //                                                where houseID={0}
                //                                                and A.recordUrlDel=1
                //                                                order by a.createTime desc",
                //                                                Request.QueryString["HouseID"]);
                DataSet dts = DbHelperSQL.Query(sql);

                if (dts.Tables[0].Rows.Count > 0)
                {
                    sql = string.Format(@"select phoneID,EmployeeID from h_RecordClose where IsCheck=1 and phoneID in({0})",
                            string.Join(",", dts.Tables[0].AsEnumerable().Select(x => x.Field<decimal>("phoneID"))));

                    list_h_RecordClose = h_RecordClose.FindAll(sql);
                }

                hf_ly2.DataSource = dts.Tables[0];
                hf_ly2.DataBind();

                frmOldPrice.Text = H_houseinfor.Find("HouseID", HouseID).Min_price.ToString();
            }
        }

        protected void hf_ly2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView hp = (DataRowView)e.Item.DataItem;
                if (hp != null)
                {
                    Panel pan = (Panel)e.Item.FindControl("Panel1");
                    decimal empID = 0;
                    if (list_h_RecordClose.Count > 0)
                    {
                        var h_RecordClose = list_h_RecordClose.Find(x => x.phoneID == Convert.ToDecimal(hp["phoneID"].ToString()));
                        if (h_RecordClose != null)
                        {
                            empID = h_RecordClose.EmployeeID;
                            if (empID > 0 && empID != Employee.Current.EmployeeID)
                            {
                                if (!CheckRolePermission("查看录音", Convert.ToDecimal(hp["employeeID"])))
                                {
                                    pan.Visible = false;
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }

        protected override void OnOtherSaveForm(object sender, EntityFormEventArgs e)
        {
            //验证数据合法性
            if (Entity.NewPrice == 0 || Entity.NewPrice_Sum == 0)
            {
                ShowMsg(AlertType.error, "跟进实价或总价不能为0!");
                return;
            }
            //经纪人必须上传录音
            if (Current.RoleNames.Contains("经纪人") && (Request.Form["rb1"] == null || Request.Form["rb1"] == ""))
            {
                ShowMsg(AlertType.error, "请选择压价录音!");
                return;
            }

            h_PriceFollowUp.Meta.BeginTrans();
            try
            {
                //改房源实价和平均价格
                decimal NewPrice = Entity.NewPrice;
                decimal NewPrice_Sum = Entity.NewPrice_Sum;
                if (NewPrice > Entity.House.Sum_price)
                {
                    NewPrice_Sum = NewPrice;
                }

                //写一条房源跟进
                h_FollowUp hfo = new h_FollowUp();
                hfo.HouseID = Entity.HouseID;
                hfo.EmployeeID = Employee.Current.EmployeeID;
                hfo.FollowUpText = "压价跟进[实价]：" + Entity.House.Min_price + "→" + NewPrice + "，[总价]：" + Entity.House.Sum_price + "→" + NewPrice_Sum;
                hfo.exe_Date = DateTime.Now;
                hfo.Insert();

                Entity.OldPrice = Entity.House.Min_price;
                Entity.OldPrice_Sum = Entity.House.Sum_price;

                //decimal AddIntegral = Math.Abs(Math.Round(((Entity.NewPrice - Entity.OldPrice) * 10000 / 1000), 0));

                //保存
                Entity.EmployeeID = hfo.EmployeeID;
                Entity.AddDate = hfo.exe_Date;
                //Entity.Integral = AddIntegral.ToInt32().Value;
                Entity.Integral = 0;
                Entity.State = 0;
                if (Current.RoleNames.Contains("经纪人"))
                {
                    //获取此房源历史最低价格,如果新实价比历史最低价降低3%，则更改价格维护人
                    decimal minNewPrice = DbHelperSQL.GetSingle(string.Format(@"select isnull(min(NewPrice),0) from h_PriceFollowUp where HouseID ={0}", Entity.HouseID)).ToDecimal().Value;
                    if ((minNewPrice - NewPrice) >= (minNewPrice * 0.03.ToDecimal()))
                    {
                        Entity.State = 1;

                        //增加排序积分
                        s_SysParam ss = s_SysParam.FindByParamCode("housePrice");
                        //获取分隔符的值，第一个为分值，第二是否有 有效期，第三为有效期值
                        string[] ssValue = ss.Value.Split('|');

                        e_Integral ei = new e_Integral();
                        ei.employeeID = Convert.ToInt32(Current.EmployeeID);
                        ei.Type = (int)integral_Type.房源与经纪人;
                        ei.tableName = "h_houseinfor";
                        ei.coloumnName = "HouseID";
                        ei.keyID = Convert.ToInt32(Entity.HouseID);
                        ei.integralParam = "housePrice";
                        ei.integralValue = ssValue[0].ToInt32();
                        ei.integralDay = ssValue[1] == "1" ? ssValue[2].ToInt32() : 0;
                        ei.exe_Date = DateTime.Now;
                        ei.Insert();
                    }
                }

                if (Request.Form["rb1"] != null && Request.Form["rb1"] != "")
                {
                    Entity.RecFilePath = Request.Form["rb1"].ToString();
                }

                //单价
                int Ohter2ID = Convert.ToInt32(Entity.NewPrice_Sum * 10000 / Entity.House.Build_area);

                string sql = string.Format(@"update h_houseinfor set Min_price={0},Ohter2ID={1},Sum_price={2}
                                            where HouseID={3}",
                                            NewPrice,
                                            Ohter2ID,
                                            NewPrice_Sum,
                                            Entity.HouseID);

                h_PriceFollowUp.Meta.Execute(sql);

                Entity.Save();

                h_PriceFollowUp.Meta.Commit();

                //判断是否修改价格维护人，需要推送给原价格维护人
                if (Entity.State == 1)
                {
                    sql = string.Format(@"SELECT TOP 1 employeeid 
                                            FROM   h_pricefollowup 
                                            WHERE  state = 1 
                                                    AND employeeid != {0} 
                                                    AND houseid = {1} 
                                            ORDER  BY adddate DESC ",
                                            Entity.EmployeeID,
                                            Entity.HouseID);

                    object empID = DbHelperSQL.GetSingle(sql);
                    //判断此房源是否有前一个价格维护人
                    if (empID != null)
                    {
                        string msg = "房源编号：【" + Entity.House.Shi_id + "】的价格维护人修改为【" + Current.Em_name + "】";

                        h_PriceComplaint hpc = new h_PriceComplaint();
                        hpc.h_PriceFollowUpID = Entity.ID;
                        hpc.houseID = Entity.HouseID.ToInt32().Value;
                        hpc.createTime = DateTime.Now;
                        hpc.Context = msg;
                        hpc.priceEditEmpID = Entity.EmployeeID.ToInt32().Value;
                        hpc.priceEmpID = empID.ToInt32().Value;
                        hpc.Insert();

                        Common.MsgPush.PushMsg(msg, new string[] { Entity.EmployeeID.ToString() }, (int)Common.msgType.价格维护);
                    }
                }
            }
            catch
            {
                h_PriceFollowUp.Meta.Rollback();
            }
        }

        /// <summary>
        /// 音频转换
        /// </summary>
        /// <param name="fileName">传入文件名</param>
        private string ChangeFilePhy(string fileName)
        {
            string ffmpeg = System.Web.HttpContext.Current.Server.MapPath("/Chat/ffmpeg/ffmpeg.exe");
            string oldfile = System.Web.HttpContext.Current.Server.MapPath("~/UpAudioFiles/" + fileName + ".3gp");////要转换的文件路径
            string newfile = System.Web.HttpContext.Current.Server.MapPath("~/UpAudioFiles/" + fileName + ".mp3");////转换后的文件路径

            //如果ffmpeg.exe文件或传过来的要转换的文件不存在的话，就返回0
            if ((!System.IO.File.Exists(ffmpeg)) || (!System.IO.File.Exists(oldfile)))
            {
                return "0";
            }

            try
            {
                System.Diagnostics.ProcessStartInfo FilestartInfo = new System.Diagnostics.ProcessStartInfo(ffmpeg);
                FilestartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                FilestartInfo.Arguments = "-i " + oldfile + " -ab 56 -acodec libmp3lame -ac 1 -ar 22050 -r 15 -b 500 -y " + newfile;
                System.Diagnostics.Process.Start(FilestartInfo);
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        //protected override void OnSaveSuccess(object sender, EntityFormEventArgs e)
        //{
            
        //    string url = "House/HouseForm.aspx?NavTabId=" + NavTabId + "&doAjax=true&HouseID=" + HouseID + "&EditType=Edit";
        //    string dialogID = "{dialogId:\"housewiew_" + HouseID + "\"}";
        //    string script = string.Format("<script>$.pdialog.reload(\"{0}\",{1})</script>", url, dialogID);
        //    JSDo_UserCallBack_Success(script, "保存成功");
        //}
    }
}