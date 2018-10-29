<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FollowUpEditor.aspx.cs" Inherits="HouseMIS.Web.House.FollowUpEditor" %>

<style>
    .test_Tab td {
        padding: 3px 0px;
    }
</style>

<script type="text/javascript">
    if ($("#frmFollowUpDicID") != null) {
        $("#frmFollowUpDicID").change(function () {
            $("#frmFollowUpText").val($("#frmFollowUpText").val() + $("#frmFollowUpDicID").find("option:selected").text());
        })
    }

    if ($("#IsRemind") != null) {
        $("#IsRemind").click(function () {
            $("#frmRemindText").attr("readonly", !$("#IsRemind").attr("checked"));
            $("#frmRemindDate").attr("readonly", !$("#IsRemind").attr("checked"));
        })
    }

    function dialogAjaxDone(json) {
        if (json.notRemindSzTime != null && json.notRemindSzId != null) {
            notRemindSzTime[notRemindSzTime.length] = json.notRemindSzTime;
            notRemindSzId[notRemindSzId.length] = json.notRemindSzId;
        }
        $("#go").click();
        if (json.statusCode == '200') {
            alertMsg.correct(json.message);
        }
        else {
            alertMsg.error(json.message);
        }
        $.pdialog.closeCurrent();
    }
</script>

<body>
    <form id="form1" runat="server" action="House/FollowUpEditor.aspx"
        class="pageForm required-validate" onsubmit="return validateCallback(this, dialogAjaxDone)">
        <a id="go" href="House/FollowUpEditor.aspx" runat="server" target="target" rel="<%=NavTabId %>" title="提醒"></a>
        <%--    <input type="hidden" name="EditType" value="<%=EditType %>" />
    <input type="hidden" name="KeyValue" value="<%=KeyValue %>" />
    <input type="hidden" name="NavTabId" value="<%=NavTabId %>" />--%>
        <input type="hidden" id="houseid" name="houseid" runat="server" />
        <fieldset>
            <legend>跟进信息</legend>
            <table width="355" height="209" border="0" cellpadding="0" cellspacing="0" class="test_Tab">
                <tr>
                    <td width="10">&nbsp;</td>
                    <td width="59">房源编号</td>
                    <td width="109">
                        <asp:TextBox runat="server" ID="frmshi_id" Style="width: 100px" ReadOnly="true" />
                    </td>
                    <td width="59">楼盘</td>
                    <td width="108">
                        <asp:TextBox runat="server" ID="frmHouseDicName" Style="width: 100px" ReadOnly="true" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>跟进分类</td>
                    <td>
                        <asp:DropDownList ID="frmFollowUpTypeID" runat="server" Width="108px">
                        </asp:DropDownList>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td valign="top">&nbsp;</td>
                    <td valign="top">跟进内容</td>
                    <td colspan="3">
                        <asp:TextBox runat="server" Width="96%" Height="80px" ID="frmFollowUpText"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr runat="server" id="TR1">
                    <td>&nbsp;</td>
                    <td>跟进名称</td>
                    <td colspan="3">
                        <asp:DropDownList ID="frmFollowUpDicID" runat="server" Width="280px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr runat="server" id="TR2">
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>
                        <asp:CheckBox runat="server" ID="IsRemind" Text="是否提醒"></asp:CheckBox>
                    </td>
                    <td>提醒日期</td>
                    <td>
                        <asp:TextBox runat="server" Width="108px" ID="frmRemindDate" class="date" format="yyyy-MM-dd HH:mm:ss" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr runat="server" id="TR3">
                    <td>&nbsp;</td>
                    <td>提醒内容</td>
                    <td colspan="3">
                        <asp:TextBox runat="server" Width="96%" Height="80px" ID="frmRemindText" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </fieldset>

        <div class="formBar">
            <ul>
                <li>
                    <div class="buttonActive">
                        <div class="buttonContent">
                            <button type="submit">保存</button>
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
