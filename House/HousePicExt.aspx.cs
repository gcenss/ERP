using System;
using HouseMIS.EntityUtils;
using TCode;
using HouseMIS.Common;
using System.Web;

namespace HouseMIS.Web.House
{
    public partial class HousePicExt : HouseMIS.Web.UI.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["Name"] != null)
                {
                    //照片分类ID
                    string PicTypeID = Request["PicTypeID"].ToString();
                    //上传的文件夹
                    string Path = Request["Path"].ToString();
                    //上传文件名
                    string Name = Request["Name"].ToString();
                    //房源ID
                    string HouseID = Request["HouseID"].ToString();
                    //人员ID
                    string EmployeeID = Request["EmployeeID"];

                    if (PicTypeID != "0")
                    {
                        string suburl = ImageHelper.GetDbPath(Path, Name);

                        string allurl = ImageHelper.GetUrl(suburl);

                        h_PicList h_PicList = new h_PicList();
                        h_PicList.PicURL = suburl;
                        h_PicList.PicTypeID = PicTypeID.ToDecimal().Value;

                        if (string.IsNullOrEmpty(EmployeeID) || EmployeeID == "0")
                        {
                            h_PicList.OrgID = Current.OrgID.ToInt32().Value;
                            h_PicList.EmployeeID = Current.EmployeeID.ToInt32().Value;
                        }
                        else
                        {
                            h_PicList.OrgID = int.Parse(Employee.FindByEmployeeID(Convert.ToDecimal(EmployeeID)).OrgID.ToString());
                            h_PicList.EmployeeID = int.Parse(EmployeeID);
                        }
                        h_PicList.HouseID = HouseID.ToDecimal().Value;
                        h_PicList.Insert();

                        Log log1 = new Log();
                        log1.Action = "上传房源照片";
                        log1.Category = "上传房源照片";
                        log1.IP = HttpContext.Current.Request.UserHostAddress;
                        log1.OccurTime = DateTime.Now;
                        log1.UserID = Convert.ToInt32(Employee.Current.EmployeeID);
                        log1.UserName = Employee.Current.Em_name;
                        log1.Remark = string.Format("房源ID={0},房源编号={1}",
                                                    HouseID,
                                                    H_houseinfor.FindByHouseID(HouseID.ToInt32()).Shi_id);
                        log1.Insert();

                        H_houseinfor hh = H_houseinfor.FindByHouseID(HouseID.ToDecimal().Value);
                        if (hh != null)
                        {
                            hh.Update_date = DateTime.Now;
                            hh.HasImage = true;
                            hh.Update();

                            #region 出租积分 上传满5张照片即有积分
                            //租房并且不是毛坯有积分
                            if (hh.aType == 1 && hh.Renovation != "毛坯")
                            {
                                string sql = string.Format(@"SELECT count(1) 
                                                                FROM h_PicList 
                                                                Where PicTypeID>1 
                                                                And EmployeeID={0} 
                                                                And HouseID={1}",
                                                                Employee.Current.EmployeeID,
                                                                HouseID);
                                if (EntityUtils.DBUtility.DbHelperSQL.GetSingle(sql).ToInt32() >= 5)
                                {
                                    UpdateIntegral("上传5张房源照片(租)", DateTime.Now, "H_houseinfor", "HouseID", HouseID);
                                }
                            }
                            #endregion
                        }

                        Response.Write(allurl + "|" + Employee.Current.OrgCode + Employee.Current.OrgName + "-" + Employee.Current.Em_id + Employee.Current.Em_name + " 日期：" + DateTime.Now.ToString());
                    }
                    else
                    {
                        //if (CheckRolePermission("上传全景照片"))
                        //{
                        //h_PicList.Meta.Query("insert into h_AllViewPic(Url,EmployeeID,HouseID) values('/UploadFiles/housePIC/" + Request["Name"] + "'," + Employee.Current.EmployeeID + "," + Request["HouseID"] + ")");
                        //H_houseinfor.Meta.Query("update H_houseinfor set HasImage=1 where HouseID=" + Request["HouseID"]);
                        //h_FollowUp En = new h_FollowUp();
                        //En.HouseID = Convert.ToDecimal(Request["HouseID"]);
                        //En.EmployeeID = Employee.Current.EmployeeID;
                        //En.FollowUpText = "添加全景照片";
                        //En.exe_Date = DateTime.Now;
                        //En.Insert();
                        //UpdateIntegral("添加全景照片", DateTime.Now, "h_AllViewPic", "HouseID", Request["HouseID"]);
                        //Response.Write(Employee.Current.OrgCode + Employee.Current.OrgName + "-" + Employee.Current.Em_id + Employee.Current.Em_name + " 日期：" + DateTime.Now.ToString());
                        //}
                        //else
                        //{
                        Response.Write("2");
                        //}
                    }
                }
            }
        }
    }
}