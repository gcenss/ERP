<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="api58Login.aspx.cs" Inherits="HouseMIS.Web.House.api58Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <iframe width="100%" height="600px" src="http://openapi.58.com/oauth2/authorize?client_id=50129732061953&redirect_uri=http://erp.efw.cn/ApiService/api58Callback.ashx&response_type=code"></iframe>
    </div>
    </form>
</body>
</html>
