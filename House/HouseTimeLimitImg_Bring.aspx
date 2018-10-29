<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HouseTimeLimitImg_Bring.aspx.cs" Inherits="HouseMIS.Web.House.HouseTimeLimitImg_Bring" %>
<html>
<head><title></title></head>
<body>
<form rel="pagerForm" runat="server"  name="pagerForm" id="pagerForm" onsubmit="return dialogSearch(this);" action="House/HouseTimeLimitImg_Bring.aspx" method="post">
<div style="float:left;width:440px; height:700px;">
<input type="hidden" runat="server" id="bid" name="bid" />
    <img runat="server" id="img" onerror="this.src=this.src.replace('http://bj.img.fsfo2o.com/yun','http://img6.efw.cn/newerp')"/>
</div>
</form>
 </body>
</html>