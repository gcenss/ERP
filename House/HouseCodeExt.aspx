<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HouseCodeExt.aspx.cs" Inherits="HouseMIS.Web.House.HouseCodeExt" %>
<div class="pageContent">
    <form id="Form1" method="post" runat="server" action="House/HouseCodeExt.aspx?type=edit" class="pageForm required-validate" onsubmit="return validateCallback(this, dialogAjaxDone);">
    <div class="pageFormContent" layouth="60">

        <div class="unit">
            <label>
                原房源编号：</label>
               <asp:TextBox runat="server" Width="200px" ID="Shi_id"></asp:TextBox>
          
        </div>
        <div class="unit">
            <label>
                新房源编号：</label>
            <asp:TextBox runat="server" Width="200px" ID="NewShi_id"></asp:TextBox>
        </div>
    </div>
    <div class="formBar">
        <ul>
            <li>
                <div class="buttonActive">
                    <div class="buttonContent">
                        <button type="submit">
                            保存</button></div>
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
