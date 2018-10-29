<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HouseLYList.aspx.cs" Inherits="HouseMIS.Web.House.HouseLYList" %>

<script type="text/javascript">
    function Hide() {
        $("#LYListSave").hide();
    }
</script>

<body>
    <form id="pageForm" runat="server" class="pageForm required-validate"
        action="house/HouseLYList.aspx" onsubmit="return validateCallback(this, dialogAjaxDone);">
        <div><span style="color: red; font-size: x-large">请选择你需要申请的录音，保存成功后，请在房源管理-开盘录音认证中查看审核情况</span></div>
        <br />
        <div class="pageContent" style="height: 420px; width: auto; overflow: auto">
            <asp:Repeater ID="hf_ly" runat="server">
                <ItemTemplate>
                    <asp:Panel ID="Panel1" runat="server" Style="float: left;">
                        <input type="radio" name="rb1" value='<%#Eval("phoneID") %>' />
                        <audio src="<%#Eval("a") %>" preload="none" controls="controls">
                        </audio>
                        <br />
                        <%#Eval("startTime")!=DBNull.Value && Convert.ToInt32(Eval("startTime"))>0?"双方通话：【"+Eval("startTime")+"】秒开始，总时长【"+Eval("realSecond")+"】秒":""%>
                        <br />
                        上传员工：<%#Eval("c")%>
                        <span style="color: red"><%#Convert.ToInt16(Eval("IsCheck"))==1?"(录音已保密)":""%></span><br />
                    </asp:Panel>
                </ItemTemplate>
            </asp:Repeater>
            <label id="lbltxt" runat="server" style="color: red; font-size: x-large" visible="false">没有找到录音记录，请先拨打房东电话形成录音记录</label>
        </div>
        <div class="formBar">
            <ul>
                <li id="LYListSave">
                    <div class="buttonActive">
                        <div class="buttonContent">
                            <button type="submit" onclick="Hide()">
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
