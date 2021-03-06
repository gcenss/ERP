﻿using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TFrameWork.Log;
using TFrameWork.Web;
using TCode;

using HouseMIS.EntityUtils;
using HouseMIS.Web.UI;
using GoldMantis.Web.UI.WebControls;

namespace HouseMIS.Web.House
{
    public partial class houselookup : EntityListBase<HouseMIS.EntityUtils.H_houseinfor>
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            GVSeeHouse.RowCreated += new GridViewRowEventHandler(GVSeeHouse_RowCreated);
        }

        void GVSeeHouse_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string bringString = @"frmHouseList:'" + GVSeeHouse.DataKeys[e.Row.RowIndex]["shi_id"].ToString() + 
                                        "',HouseDicName:'" + ((H_houseinfor)e.Row.DataItem).HouseDicName + 
                                        "',build_id:'" + ((H_houseinfor)e.Row.DataItem).Build_id + 
                                        "',build_room:'" + ((H_houseinfor)e.Row.DataItem).Build_room + 
                                        "',frmshi_id:'" + GVSeeHouse.DataKeys[e.Row.RowIndex]["HouseID"].ToString() + 
                                        "',frmbuild_area:'" + ((H_houseinfor)e.Row.DataItem).Build_area + 
                                        "',frmshi_addr:'" + ((H_houseinfor)e.Row.DataItem).HouseDicName + 
                                        "-" + ((H_houseinfor)e.Row.DataItem).Build_id + 
                                        "-" + ((H_houseinfor)e.Row.DataItem).Build_room + 
                                        "',frmsum_price:'" + ((H_houseinfor)e.Row.DataItem).Sum_price +
                                        "',frmHouseDicName_CZ:'" + ((H_houseinfor)e.Row.DataItem).HouseDicName_CZ + "'";
                e.Row.Attributes.Add("ondblclick", "$.bringBack({" + bringString + "});");
            }
        }

        public string GetKeyValue(Type aType, string FildName, string Value, string Name)
        {
            var op = EntityFactory.CreateOperate(aType);
            IEntity ls = op.Find(FildName, Value);
            if (ls != null)
                return ls[Name].ToString();
            else
                return "";
        }

        protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label OwnerEmployeeID = e.Row.FindControl("OwnerEmployeeID") as Label;
                Label HouseDicID = e.Row.FindControl("HouseDicID") as Label;
                HouseMIS.EntityUtils.H_houseinfor drv = ((HouseMIS.EntityUtils.H_houseinfor)e.Row.DataItem);
                if (drv.OwnerEmployeeID > 0)
                    OwnerEmployeeID.Text = GetKeyValue(typeof(Employee), "EmployeeID", drv.OwnerEmployeeID.ToString(), "em_name");
                if (drv.HouseDicID > 0)
                    HouseDicID.Text = GetKeyValue(typeof(s_HouseDic), "HouseDicID", drv.HouseDicID.ToString(), "name");
            }
        }

        protected override string GetWhereClauseFromSearchBar(string typeName)
        {
            string temp = string.Empty;

            StringBuilder sb = new StringBuilder();
            sb.Append(base.GetWhereClauseFromSearchBar(typeName));
            if (sb.ToString().IsNullOrWhiteSpace())
            {
                sb.Append("1=1");
            }
            //过滤售房或租房
            //if (Request.QueryString["BillType"] != null)
            //{
            //    this.Form.Action = "House/houselookup.aspx?BillType=" + Request.QueryString["BillType"];
            //    if (Request.QueryString["BillType"].ToString() == "0")
            //    {
            //        sb.Append(" and aType=0");
            //    }
            //    else if (Request.QueryString["BillType"].ToString() == "1")
            //    {
            //        sb.Append(" and aType=1");
            //    }
            //}
            //过滤回收站房源
            sb.Append(" and deltype = 0");
            //按操作员查找
            temp = GetMySearchControlValue("em_name");
            if (!temp.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" AND OwnerEmployeeID in (SELECT {0} From {1} Where {2} like '%{3}%')", Employee._.EmployeeID, Employee.Meta.TableName, Employee._.Em_name, temp);
            }
            temp = GetMySearchControlValue("Name");
            if (!temp.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" AND HouseDicID in (SELECT {0} From {1} Where {2} like '%{3}%')", s_HouseDic._.HouseDicID, s_HouseDic.Meta.TableName, s_HouseDic._.Name, temp);
            }
            temp = GetMySearchControlValue("DName");
            if (!temp.IsNullOrWhiteSpace())
            {
                if (temp.ToInt32() > 9)
                {
                    sb.AppendFormat(" AND build_id like '%{0}%'", temp);
                }
            }
            temp = GetMySearchControlValue("SName");
            if (!temp.IsNullOrWhiteSpace())
            {
                if (temp.ToInt32() > 9)
                {
                    sb.AppendFormat(" AND build_room like '%{0}%'", temp);
                }
            }
            temp = GetMySearchControlValue("update_date1");
            if (!temp.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" AND update_date>='{0}' ", temp);
            }
            temp = GetMySearchControlValue("update_date2");
            if (!temp.IsNullOrWhiteSpace())
            {
                sb.AppendFormat(" AND update_date<='{0}' ", temp);
            }
            return sb.Length == 0 ? null : sb.ToString();
        }
    }
}