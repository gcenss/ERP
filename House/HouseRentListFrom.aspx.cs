using System;
using System.Text;
using System.Data;
using System.Web.UI.WebControls;
using HouseMIS.EntityUtils;
using HouseMIS.Web.UI;
using TCode;

namespace HouseMIS.Web.House
{
    public partial class HouseRentListFrom : BasePage
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (!IsPostBack)
            {
                FullCanViewOrgShopDropList(mysfrmOrgID);
            }
        }
        protected string GetFitmentID()
        {
            string sVal = "";
            IEntityOperate op = EntityFactory.CreateOperate(typeof(h_Fitment));
            IEntityList ls = op.Cache.Entities;
            if (ls != null)
            {
                for (int i = 0; i < ls.Count; i++)
                {
                    string textValue = ls[i]["Name"] == null ? "" : ls[i]["Name"].ToString();
                    string valueValue = ls[i]["FitmentID"] == null ? "" : ls[i]["FitmentID"].ToString();
                    sVal += "<input type=\"checkbox\" onclick=\"zxValue(this)\" value=\"" + textValue + "_" + valueValue + "\" />" + textValue;
                }
            }
            return sVal;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(HouseListFrom), Page);
            if (!IsPostBack)
            {
                pagerForm.Action = pagerForm.Action + "?NavTabId=" + Request.QueryString["NavTabId"];
                //FullDropListData(typeof(s_Organise), sfrmOrgID, "Name", "OrgID", null, null);

                PubFunction.FullDropListData(typeof(h_State), sfrmStateID, "Name", "StateID", "");
                sfrmStateID.Items[0].Text = "全部";
                sfrmStateID.Items[0].Value = "0";

                PubFunction.FullDropListData(typeof(H_landlord), sfrmLandlordID, "Name", "LandlordID", "");
                PubFunction.FullDropListData(typeof(h_Property), sfrmPropertyID, "Name", "PropertyID", "");
                PubFunction.FullDropListData(typeof(h_Type), sfrmTypeCode, "Name", "TypeCode", "");
                //PubFunction.FullDropListData(typeof(h_Fitment), mysfrmFitmentID, "Name", "FitmentID", "");
                PubFunction.FullDropListData(typeof(h_Year), sfrmYearID, "Name", "YearID", "");
                PubFunction.FullDropListData(typeof(h_Taxes), sfrmTaxesID, "Name", "TaxesID", "");
                //PubFunction.FullDropListData(typeof(h_AssessState), sfrmAssessStateID, "Name", "AssessStateID", "");
                FullAreaDropListData(mysfrmAreaID, "请选择", "");
                sfrmStateID.SelectedValue = "2";
                yun_Company com = yun_Company.FindByID(TCode.YUN.yun_Company.CurrentID);
                if (com != null)
                {
                    sfrmSanjakID.Items.Add(new ListItem("", ""));
                    EntityList<s_Sanjak> ss = s_Sanjak.FindAll("select * from s_Sanjak where AreaID in (select AreaID from s_Area where pAreaID=" + com.CityID + ")");
                    foreach (s_Sanjak sa in ss)
                    {
                        sfrmSanjakID.Items.Add(new ListItem(sa.Name, sa.SanjakID.ToString()));
                    }
                }

                PubFunction.FullDropListData(typeof(h_Use), sfrmUseID, "Name", "UseID", "");
                PubFunction.FullDropListData(typeof(c_Visit), sfrmVisitID, "Name", "VisitID", "");
                PubFunction.FullDropListData(typeof(h_Faceto), sfrmFacetoID, "Name", "FacetoID", "");

            }
            if (Request.QueryString["type"] != null)
            {
                switch (Request.QueryString["type"].ToString())
                {
                    case "hc": Form.Action = "House/HouseCheck.aspx?NavTabId=" + NavTabId; break;
                    case "hl": Form.Action = "House/HouseRentList.aspx?NavTabId=" + NavTabId; break;
                    case "zf": Form.Action = "House/HouseListZF.aspx?NavTabId=" + NavTabId; break;
                    default: Form.Action = "House/HouseRentList.aspx?NavTabId=" + NavTabId; break;
                }
            }
        }
        [AjaxPro.AjaxMethod]
        public string GetSanjak(string value)
        {
            DataSet ds = new DataSet();
            if (value == "0")
            {
                ds = Employee.Meta.Query("select SanjakID,Name from s_Sanjak");
            }
            else
            {
                ds = Employee.Meta.Query("select SanjakID,Name from s_Sanjak where AreaID=" + value);
            }
            StringBuilder sb = new StringBuilder();
            if (ds.Tables[0].Rows.Count > 0)
            {
                sb.Append(",|请选择");
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    sb.Append("," + dr["SanjakID"].ToString() + "|" + dr["Name"].ToString());
                }
                return sb.ToString().Substring(1);
            }
            else
            {
                sb.Append("|无数据");
                return sb.ToString();
            }
        }
        private new void FullAreaDropListData(DropDownList d, string defaultName, string defaultValue)
        {
            var ls = s_Area.FindAllComDistrict();
            if (!defaultName.IsNullOrWhiteSpace() || !defaultValue.IsNullOrWhiteSpace())
                d.Items.Add(new ListItem(defaultName, defaultValue));
            for (int i = 0; i < ls.Count; i++)
            {
                d.Items.Add(new ListItem(ls[i].Name, ls[i].AreaID.ToString()));
            }
        }
    }
}
