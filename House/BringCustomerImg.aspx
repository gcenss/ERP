<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BringCustomerImg.aspx.cs" Inherits="HouseMIS.Web.House.BringCustomerImg" %>
<html>
<head><title></title></head>
<body>
<form rel="pagerForm" runat="server"  name="pagerForm" id="pagerForm" onsubmit="return dialogSearch(this);" action="House/BringCustomerImg.aspx" method="post">
<div style="float:left;width:440px; height:700px;">
<input type="hidden" runat="server" id="bid" name="bid" />
    <img runat="server" id="img" onerror="this.src=this.src.replace('http://bj.img.fsfo2o.com/yun','http://img6.efw.cn/newerp')"/>
</div>
<a id="goimg" href="Customer/customerbringlist.aspx" runat="server" target="navTab" rel="190_menu21" title="客户带看管理"></a>
<div style="float:right; vertical-align:top;">
    <%=CusStrimg.ToString()%>
</div>
<script type="text/javascript">
    function showshenhe(opts) {
        var a = HouseMIS.Web.House.BringCustomerImg.showopts(opts, document.getElementById("bid").value).value;
        if (a == 1) {
            $("#imglist").attr("href", "/House/customerbringlist.aspx?NavTabId=" + $("#imglist").val());
            alertMsg.correct("操作成功");
        }
        else if (a == 0) {
            alertMsg.error("操作失败");
        }
        $("#goimg").attr("href", "Customer/customerbringlist.aspx?NavTabId=<%=NavTabId %>&doAjax=true");
        $("#goimg").click();
    }
</script>
</form>
 </body>
</html>