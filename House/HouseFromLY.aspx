<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HouseFromLY.aspx.cs" Inherits="HouseMIS.Web.House.HouseFromLY" %>

<body>
    <div style="height: 560px; overflow: auto;" class="tabs" currentindex="0" eventtype="click">
        <div class="tabsHeader">
            <div class="tabsHeaderContent">
                <ul>
                    <li><a href="javascript:;"><span>拨打录音</span></a></li>
                </ul>
            </div>
        </div>
        <div class="tabsContent" layouth="145">
            <div>
                <asp:Repeater ID="hf_ly2" runat="server" OnItemDataBound="hf_ly2_ItemDataBound">
                    <ItemTemplate>
                        <asp:Panel ID="Panel1" runat="server">
                            <audio src="<%#Eval("a") %>" preload="none" controls="controls">
                                <%--<source src="<%#Eval("a") %>" type="audio/wav">--%>
                            </audio>
                            <br />
                            <%#Eval("startTime")!=DBNull.Value && Convert.ToInt32(Eval("startTime"))>0?"双方通话：【"+Eval("startTime")+"】秒开始":""%>
                            <p />
                            编号：<%#Eval("phoneID")%>,上传员工：<%#Eval("c")%>
                            <asp:Label ID="lblDate" runat="server" Text='<%#Eval("d")%>'></asp:Label>
                            <asp:Label ID="lblClose" runat="server" Style="color: red"></asp:Label>
                            <br />
                            <asp:HyperLink ID="deltelLY" runat="server" Style="color: red" NavigateUrl="House/HouseForm.aspx" Target="ajaxTodo" title="确认删除吗？">删除录音</asp:HyperLink>&nbsp;&nbsp;
                       
                            <asp:HyperLink ID="LYClose" runat="server" Style="color: red" NavigateUrl="House/HouseRecordFrom.aspx" Target="dialog">录音保密</asp:HyperLink>
                            <br />
                            <br />
                        </asp:Panel>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
</body>
