<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HouseTelForm_SMS.aspx.cs" Inherits="HouseMIS.Web.House.HouseTelForm_SMS" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" action="House/HouseTelForm_SMS.aspx?doAjax=true" runat="server" class="pageForm required-validate" onsubmit="return validateCallback(this, dialogAjaxDone)">
        <input type="hidden" id="LSH" runat="server" />
        <input type="hidden" name="doAjax" value="true" />
        <div class="pageFormContent" layouth="58">
            <div>
                <ul>
                    <li>
                        <label>短信内容：</label></li>
                    <li>
                        <asp:TextBox ID="txtMsg" runat="server" TextMode="MultiLine" Width="290px" Height="130px"></asp:TextBox></li>
                    <li>
                        <label></label>
                    </li>
                    <li style="color: red">
                        <label>
                            说明：<br />
                            1.不允许在内容中添加任何号码，违者乐捐500元<br />
                            2.短信内容可以编辑，发送到客户手机内容为编辑内容+本人的默认隐号<br />
                            3.发送的短信系统均有记录
                        </label>
                    </li>
                </ul>
            </div>
        </div>
        <div class="formBar">
            <ul>
                <li>
                    <div class="buttonActive">
                        <div class="buttonContent">
                            <button type="submit">发送</button>
                        </div>
                    </div>
                </li>
                <li>
                    <div class="button">
                        <div class="buttonContent">
                            <button type="button" class="close">取消</button>
                        </div>
                    </div>
                </li>
            </ul>
        </div>
    </form>
</body>
</html>
