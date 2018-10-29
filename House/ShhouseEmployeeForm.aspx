<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShhouseEmployeeForm.aspx.cs" Inherits="HouseMIS.Web.House.ShhouseEmployeeForm" %>

<body>
    <div class="pageContent">
        <form id="Form2" method="post" runat="server" action="House/ShhouseEmployeeForm.aspx?type=add" class="pageForm required-validate" onsubmit="return validateCallback(this, dialogAjaxDone)">
            <asp:HiddenField ID="frmEmployeeID" runat="server" />
            <input type="hidden" name="doAjax" value="true" />
            <div class="pageFormContent" layouth="58">
                <div class="unit">
                    <label>
                        用户名：</label>
                    <asp:TextBox ID="frmshEName" runat="server" Width="180px"></asp:TextBox>
                </div>
                <div class="unit">
                    <label>
                        密码：</label>
                    <asp:TextBox ID="frmshPwd" runat="server" Width="180px"></asp:TextBox>
                </div>
                <%--还没有账号？<a href='/house/efwLogin.aspx'  title="e房网二手房账户绑定">注册账号</a>--%>
            </div>
            <div class="formBar">
                <ul>
                    <li>
                        <div class="buttonActive">
                            <div class="buttonContent">
                                <button type="submit">
                                    提交</button>
                            </div>
                        </div>
                    </li>
                    <li>
                        <div class="button">
                            <div class="buttonContent">
                                <button type="button" class="close">
                                    取消</button>
                            </div>
                        </div>
                    </li>
                </ul>
            </div>
        </form>
    </div>
</body>

