using HouseMIS.EntityUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using TCode;
using System.Web.Script.Serialization;
namespace HouseMIS.Web.House
{
    public partial class AreaV : System.Web.UI.Page
    {
        protected string pInfo, pSon;
        protected string tName;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["tn1"] != null && Request.QueryString["tn2"] != null && Request.QueryString["tn3"] != null)
                {
                    txtName1.Text = Request.QueryString["tn1"].ToString();
                    txtName2.Text = Request.QueryString["tn2"].ToString();
                    txtName3.Text = Request.QueryString["tn3"].ToString();

                    //s_Sanjak  s_HouseDic
                    EntityList<s_Area> EList = s_Area.FindAllComDistrict();
                    for (int i = 0; i < EList.Count; i++)
                    {
                        pInfo += "<span><input type=\"checkbox\" name=\"cblArea" + EList[i].AreaID + "\" value=\"q_" + EList[i].AreaID + "_" + EList[i].Name + "\" onclick=\"hqValue(this)\" /><a href=\"#\" onclick=\"openSQ(" + EList[i].AreaID + ")\">" + EList[i].Name + "</a></span>";
                        pSon += GetSanjak(EList[i].AreaID);
                    }
                }
                
            }
        }
        private string GetSanjak(decimal iID)
        {
            string sVal = "<div id=\"son_"+ iID + "\" class=\"iio\">";
            EntityList<s_Sanjak> ss = s_Sanjak.FindAll("select * from s_Sanjak where AreaID="+ iID);
            foreach (s_Sanjak sa in ss)
            {
                sVal += "<span><input id=\"cblSanj" + sa.SanjakID.ToString() + "\" type=\"checkbox\" name=\"cblSanj"+ sa.SanjakID.ToString() + "\" value=\"s_" + sa.SanjakID.ToString() + "_" + sa.Name + "\" onclick=\"hqValue(this)\" /><a href=\"#\" onclick=\"openXQ(" + sa.SanjakID.ToString() + ")\">" + sa.Name + "</a></span>";
            }
            return sVal + "</div>";
        }
        ///select HouseDicID,Name from s_HouseDic where SanjakID=22

    }
}