<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShareEmployeeForm.aspx.cs"
    Inherits="HouseMIS.Web.House.ShareEmployeeForm" %>

<div class="pageContent">
    <form id="Form2" method="post" runat="server" action="House/ShareEmployeeForm.aspx" class="pageForm required-validate" onsubmit="return validateCallback(this, dialogAjaxDone)">
    <asp:HiddenField ID="frmEmployeeID" runat="server" />
    <input type="hidden" name="doAjax" value="true" />
    <div class="pageFormContent" layouth="58">
        <div class="unit">
            <label>
                姓名：</label>
            <asp:textbox ID="frmEName" runat="server" width="180px"></asp:textbox>
        </div>
        <div class="unit">
            <label>
                手机：</label>
            <asp:textbox ID="frmEPhone" runat="server" width="180px"></asp:textbox>
        </div>
        <div class="unit">
            <label>
                微信：</label>
            <asp:textbox ID="frmEWeixin" runat="server" width="180px"></asp:textbox>
        </div>
        <div class="unit">
            <label>
                公司：</label>
            <asp:textbox ID="frmCompanyName" runat="server" width="180px"></asp:textbox>
        </div>
    </div>
    <div class="formBar">
        <ul>
            <li>
                <div class="buttonActive">
                    <div class="buttonContent">
                        <button type="submit">
                            提交</button></div>
                </div>
            </li>
            <li>
                <div class="button">
                    <div class="buttonContent">
                        <button type="button" class="close">
                            取消</button></div>
                </div>
            </li>
        </ul>
    </div>
    </form>
</div>
