using HouseMIS.EntityUtils;
using HouseMIS.Web.UI;
using System;
using System.Data;

namespace HouseMIS.Web.House
{
    public partial class AddHouseKey : EntityFormBase<h_HouseKey>
    {
        public override string MenuCode
        {
            get
            {
                return "2001";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FullDropListData(typeof(h_OhterCompanyName), this.frmOhterCompanyNameID, "OhterCompanyName", "OhterCompanyNameID");
                FullCanViewOrgShopDropList(this.frmOrgID, null);

                rdout.Checked = true;
                string houseid = Request.QueryString["HouseID"];
                id.Value = houseid;

                housekeyid1.Text = EntityUtils.DBUtility.DbHelperSQL.GetSingle(string.Format("select shi_id from h_houseinfor where houseid={0}", houseid)).ToString(); ;
            }
        }

        protected override void OnOtherSaveForm(object sender, EntityFormEventArgs e)
        {
            if (rdout.Checked)      // 拿走
            {
                #region 拿走

                DataTable dthouse = H_houseinfor.Meta.Query("SELECT HasKey FROM h_houseinfor H WHERE H.HouseID =" + Request.Form["id"]).Tables[0];
                if (dthouse.Rows.Count > 0)
                {
                    // 如果没有钥匙
                    if (Convert.ToBoolean(dthouse.Rows[0][0]) != false)
                    {
                        if (Request.Form["frmRemarks"] != "")
                        {
                            if (frmInOhterCompany.Checked == true)                                          // 判断是否其他中介
                            {
                                if (Request.Form["frmOhterCompanyNameID"] != "" && Request.Form["frmTel"] != "")
                                {
                                    if (!CheckRolePermission("拿走钥匙"))
                                    {
                                        Response.Write("<script>alertMsg.error('您无权拿走钥匙')</script>");
                                    }
                                    else
                                    {
                                        Entity.IsIn = false;
                                        H_houseinfor.Meta.Query("UPDATE h_houseinfor SET HasKey = 0 WHERE HouseID = " + Request.Form["id"]);
                                        Entity.Save();
                                    }
                                }
                                else
                                {
                                    Response.Write("<script>alertMsg.info('必须选择中介名和填写电话')</script>");
                                }
                            }
                            else if (isLandrand.Checked == true)                                            // 判断是否选择了房东
                            {
                                if (!CheckRolePermission("拿走钥匙"))
                                {
                                    Response.Write("<script>alertMsg.error('您无权拿走钥匙')</script>");
                                    return;
                                }
                                else
                                {
                                    Entity.IsIn = false;
                                    Entity.IsLandran = 1;
                                    H_houseinfor.Meta.Query("UPDATE h_houseinfor SET HasKey = 0 WHERE HouseID = " + Request.Form["id"]);
                                    Entity.Save();
                                }
                            }
                            else
                            {
                                if (Request.Form["frmOrgID"] != "0")
                                {
                                    if (!CheckRolePermission("拿走钥匙"))
                                    {
                                        Response.Write("<script>alertMsg.error('您无权拿走钥匙')</script>");
                                        return;
                                    }
                                    else
                                    {
                                        Entity.IsIn = false;
                                        H_houseinfor.Meta.Query("UPDATE h_houseinfor SET HasKey = 0 WHERE HouseID = " + Request.Form["id"]);
                                        Entity.Save();
                                    }
                                }
                                else
                                {
                                    Response.Write("<script>alertMsg.info('请选择分部')</script>");
                                    return;
                                }
                            }
                        }
                        else
                        {
                            Response.Write("<script>alertMsg.info('请填写备注')</script>");
                        }
                    }
                    else
                    {
                        Response.Write("<script>alertMsg.info('该房源没有钥匙')</script>");
                    }
                }

                #endregion 拿走
            }
            else if (rdin.Checked)  // 拿入
            {
                #region 拿入

                if (Request.Form["frmRemarks"] != "")
                {
                    if (frmInOhterCompany.Checked == true)                                          // 判断是否其他中介
                    {
                        if (Request.Form["frmOhterCompanyNameID"] != "" && Request.Form["frmTel"] != "")
                        {
                            Entity.IsIn = true;
                            H_houseinfor.Meta.Query("UPDATE h_houseinfor SET HasKey = 1 WHERE HouseID = " + Request.Form["id"]);
                            Entity.Save();
                        }
                        else
                        {
                            Response.Write("<script>alertMsg.info('必须选择中介名和填写电话')</script>");
                        }
                    }
                    else if (isLandrand.Checked == true)                                            // 判断是否选择了房东
                    {
                        Entity.IsIn = true;
                        Entity.IsLandran = 1;
                        H_houseinfor.Meta.Query("UPDATE h_houseinfor SET HasKey = 1 WHERE HouseID = " + Request.Form["id"]);
                        Entity.Save();
                    }
                    else
                    {
                        if (Request.Form["frmOrgID"] != "")
                        {
                            Entity.IsIn = true;
                            H_houseinfor.Meta.Query("UPDATE h_houseinfor SET HasKey = 1 WHERE HouseID = " + Request.Form["id"]);
                            Entity.Save();
                        }
                        else
                        {
                            Response.Write("<script>alertMsg.info('请选择分部')</script>");
                        }
                    }
                }
                else
                    Response.Write("<script>alertMsg.info('请填写备注')</script>");

                #endregion 拿入
            }
            else
                base.OnOtherSaveForm(sender, e);
        }

        protected override void OnSaving(object sender, EntityFormEventArgs e)
        {
            if (rdout.Checked)      // 拿走
            {
                Entity.IsIn = false;
            }
            else if (rdin.Checked)  // 拿入
            {
                Entity.IsIn = true;
            }
            Entity.EmployeeID = Convert.ToInt32(Employee.Current.EmployeeID);
            Entity.HouseID = Convert.ToDecimal(Request.Form["id"]);

            base.OnSaving(sender, e);
        }

        protected override void OnSaveSuccess(object sender, EntityFormEventArgs e)
        {
            //拿入并且不是其他中介，则增加排序积分
            if (rdin.Checked && !Entity.InOhterCompany)
            {
                //增加排序积分
                s_SysParam ss = s_SysParam.FindByParamCode("houseKey");
                //获取分隔符的值，第一个为分值，第二是否有 有效期，第三为有效期值
                string[] ssValue = ss.Value.Split('|');

                e_Integral ei = new e_Integral();
                ei.employeeID = Convert.ToInt32(Current.EmployeeID);
                ei.Type = (int)integral_Type.房源与经纪人;
                ei.tableName = "h_houseinfor";
                ei.coloumnName = "HouseID";
                ei.keyID = Convert.ToInt32(Entity.HouseID);
                ei.integralParam = "houseKey";
                ei.integralValue = ssValue[0].ToInt32();
                ei.integralDay = ssValue[1] == "1" ? ssValue[2].ToInt32() : 0;
                ei.exe_Date = DateTime.Now;
                ei.Insert();
            }
        }
    }
}