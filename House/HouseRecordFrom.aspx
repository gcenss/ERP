<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HouseRecordFrom.aspx.cs" Inherits="HouseMIS.Web.House.HouseRecordFrom" %>

<%--<script>
    //function GetVile() {
    //    if ($("#HouseRecordF .frmaType:eq(0)").val() == "0") {
    //        alertMsg.error("申请类型不能为空！");
    //        $("#HouseRecordF .frmaType:eq(0)").focus();
    //        return false;
    //    }
    //}
</script>--%>

<body>
    <form id="form1" method="post" runat="server" action="House/HouseRecordFrom.aspx" class="pageForm required-validate"
        onsubmit="return validateCallback(this, dialogAjaxDone)">
        <div class="pageFormContent" layouth="56">
            <table border="0" align="center" cellpadding="0" cellspacing="0" style="margin-bottom: 0px; border: 0px;">
                <tr>
                    <td class="style65">类型：
                </td>
                    <td class="style66" id="HouseRecordF">
                        <asp:HiddenField ID="frmphoneID" runat="server" />
                        <asp:HiddenField ID="frmContorlID" runat="server" />
                        <asp:DropDownList runat="server" CssClass="frmaType" ID="frmaType" Width="115px">
                            <asp:ListItem Value="0">屏蔽</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
        <div class="formBar">
            <ul>
                <li>
                    <div class="buttonActive">
                        <div class="buttonContent">
                            <button type="submit">
                                保存</button>
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
</body>
