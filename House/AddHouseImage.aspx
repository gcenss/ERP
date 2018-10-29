<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddHouseImage.aspx.cs" Inherits="HouseMIS.Web.House.AddHouseImage" %>

<script type="text/javascript">
    $(document).ready(function () {
        $("#flv").attr("url",HouseMIS.Web.House.AddHouseImage.setValues());
    });
</script>

<object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,28,0"
    width="660" height="500">
    <param name="movie" value="BusCircle/batchUpPic.swf" />
    <param name="quality" value="high" />
    <param name="FlashVars" runat="server" id="flv" /><%--
    <embed name="FlashVars" id="ebflv" flashvars="HouseID=<%=HouseID %>&url=http://localhost:11419/" src="BusCircle/batchUpPic.swf"
        quality="high" pluginspage="http://www.adobe.com/shockwave/download/download.cgi?P1_Prod_Version=ShockwaveFlash"
        type="application/x-shockwave-flash" width="660" height="500"></embed>--%>
</object>