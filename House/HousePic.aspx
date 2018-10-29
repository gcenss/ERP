<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HousePic.aspx.cs" Inherits="HouseMIS.Web.House.HousePic" %>

<object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000"
    id="upTouXiang" width="1024" height="768"
    codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab">
    <param name="movie" value="Fla/Family.swf" />
    <param name="quality" value="high" />
    <param name="wmode" value="transparent">
    <param name="allowScriptAccess" value="sameDomain" />
    <param name="FlashVars" value="PicTypeID=<%=PicTypeID %>&HouseId=<%=HouseID %>&EmployeeID=<%=HouseMIS.EntityUtils.Employee.Current.EmployeeID%>" />
    <embed src="Fla/Family.swf" quality="high" flashvars="PicTypeID=<%=PicTypeID %>&HouseId=<%=HouseID %>&EmployeeID=<%=EmployeeID%>"
        width="1024" height="768" name="upTouXiang" align="middle" wmode="transparent"
        play="true" loop="false" quality="high" allowscriptaccess="sameDomain"
        type="application/x-shockwave-flash"
        pluginspage="http://www.macromedia.com/go/getflashplayer" />
</object>
