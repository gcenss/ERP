<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="FollowUpPriceEditor.aspx.cs" Inherits="HouseMIS.Web.House.FollowUpPriceEditor" %>

<style>
    .test_Tab td {
        padding: 3px 0px;
    }

    .auto-style1 {
        height: 38px;
    }
</style>
<script type="text/javascript">
    function diafupAjaxDone(json) {
        if (json.notRemindSzTime != null && json.notRemindSzId != null) {
            notRemindSzTime[notRemindSzTime.length] = json.notRemindSzTime;
            notRemindSzId[notRemindSzId.length] = json.notRemindSzId;
        }
        if (json.statusCode == '200') {
            alertMsg.correct(json.message);
        }
        else {
            alertMsg.error(json.message);
        }
        $.pdialog.closeCurrent();
    }
    function CheckLY() {
        var temp = document.getElementsByName("rb1");
        var intHot = "";
        for (var i = 0; i < temp.length; i++) {
            if (temp[i].checked)
                intHot = temp[i].value;
        }
        if (intHot == "") {
            alertMsg.error("请选择录音！");
            return false;
        }
        var np = parseInt($("#frmNewPrice").val());
        var op = parseInt($("#frmOldPrice").val());
        if (np > 0 && op > 0 && op > np) {
            var message = "房源编号:" + $("#frmshi_id").val() + "，原价:" + $("#frmOldPrice").val() + "，现价:" + $("#frmNewPrice").val() +"，发件人:<%=em_name %>，收件人:测试，跟进日期:" + new Date().toLocaleString();
            document["pcchatsoft"].SendUserMsg(3, <%=HouseID %>, "新的压价跟进", message, <%=eid %>, <%=seid %>);
        }
        else {
            alertMsg.error("请检查输入的价格是否有误！");
            return false;
        }
    }
</script>

<body>
    <form id="form1" runat="server" action="House/FollowUpPriceEditor.aspx" class="pageForm required-validate" onsubmit="return validateCallback(this, dialogAjaxDone);">
        <input type="hidden" name="NavTabId" value="<%=NavTabId %>" />

        <table style="width: auto; height: auto" border="0" cellpadding="0" cellspacing="0" class="test_Tab">
            <tr>
                <td width="10" class="auto-style1"></td>
                <td width="59" class="auto-style1">房源编号</td>
                <td width="109" class="auto-style1">
                    <asp:TextBox runat="server" ID="frmshi_id" Style="width: 100px" ReadOnly="true" /></td>
                <td width="59" class="auto-style1">楼盘</td>
                <td width="108" class="auto-style1">
                    <asp:TextBox runat="server" ID="frmHouseDicName" Style="width: 100px" ReadOnly="true" /></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>原始实价</td>
                <td>
                    <asp:TextBox runat="server" ID="frmOldPrice" ReadOnly="true" Style="width: 80px" />
                    万</td>
                <td>跟进实价</td>
                <td>
                    <asp:TextBox runat="server" ID="frmNewPrice" CssClass="number" Style="width: 80px" />
                    万</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>原始总价</td>
                <td>
                    <asp:TextBox runat="server" ID="frmOldPrice_Sum" ReadOnly="true" Style="width: 80px" />
                    万</td>
                <td>跟进总价</td>
                <td>
                    <asp:TextBox runat="server" ID="frmNewPrice_Sum" CssClass="number" Style="width: 80px" />
                    万</td>
            </tr>
            <%--           <tr>
            <td>&nbsp;</td>
            <td>录音凭证</td>
            <td colspan="3">
              <asp:FileUpload runat="server" ID="frmRecFile" />
            </td>
          </tr>--%>

            <tr>
                <td>&nbsp;</td>
                <td>录音</td>
                <td colspan="3">
                    <div style="height: 300px; width: auto; overflow: scroll">
                        <asp:Repeater ID="hf_ly2" runat="server" OnItemDataBound="hf_ly2_ItemDataBound">
                            <ItemTemplate>
                                <asp:Panel ID="Panel1" runat="server" Style="float: left;">
                                    <input type="radio" name="rb1" value="<%#Eval("a") %>">
                                    <audio src="<%#Eval("a") %>" preload="none" controls="controls" style="width: 200px">
                                    </audio>
                                    <br />
                                    编号：<%#Eval("phoneID")%>,上传员工：<%#Eval("c")%>
                                    <%-- <video controls="" name="media" width="40" height="40">
                                    <source src="<%#Eval("a") %>" type="audio/x-wav">
                                </video>--%>
                                </asp:Panel>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </td>
            </tr>
            <tr>
                <td valign="top">&nbsp;</td>
                <td valign="top">压价说明</td>
                <td colspan="3">
                    <asp:TextBox runat="server" Width="96%" Height="80px" ID="frmNote"
                        TextMode="MultiLine"></asp:TextBox></td>
            </tr>
        </table>

        <div class="formBar">
            <ul>
                <li>
                    <div class="buttonActive">
                        <div class="buttonContent">
                            <button onclick="return CheckLY()" type="submit">保存</button>
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
