<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShareInfoIframe.aspx.cs" Inherits="HouseMIS.Web.House.Templete.ShareInfoIframe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<iframe id="frmshare"  src="House/Templete/ShareHouseInfo.aspx?TempleteID=<%= Request.QueryString["TempleteID"] %>&HouseID=<%=Request.QueryString["HouseID"] %>" width="100%"   height="98%"  >
</iframe>
