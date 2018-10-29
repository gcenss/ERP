<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HouseZBCheckEdit.aspx.cs" Inherits="HouseMIS.Web.House.HouseZBCheckEdit" %>

<body>
    <div class="pageContent">
        <form id="pageForm" runat="server" class="pageForm required-validate"
            action="house/HouseZBCheckEdit.aspx" onsubmit="return validateCallback(this, dialogAjaxDone);">
            <div class="tabs">
                <div class="tabsContent" style="height: 470px; overflow: auto">
                    <table style="margin: 16px; width: 750px">
                        <tr>
                            <td style="width: 100px">房源编号：
                        </td>
                            <td>
                                <asp:Label ID="lblHouseID" runat="server"></asp:Label></td>
                            <td style="width: 300px" rowspan="10">开盘凭证
                            <div style="width: 300px; height: 350px; border: 1px #89a8c5 solid; float: right;"
                                id="h_houseinfor_ZBCheckPic" runat="server">
                            </div>
                                <div id="h_houseinfor_ZBCheckPics">
                                    <script type="text/javascript" src="/js/flaUpfile.js"></script>
                                    <script type="text/javascript">
                                        writeFla("h_houseinfor_ZBCheckPics", 1, "HouseZBCheckEdit", "", "house_ZBCheckPic", "5000", "", "0", "100", 1, "500", "700");

                                        function HouseZBCheckEdit(nam) {
                                            $("#h_houseinfor_ZBCheckPic").html("<img src='" + GetAllUrl("house_ZBCheckPic", nam) + "?sj=" + Math.random() + "' width='300' height='350' />");
                                            $("#frmpicUrl").val(GetUrl("house_ZBCheckPic", nam));
                                        }
                                    </script>
                                </div>
                                <asp:TextBox ID="frmpicUrl" runat="server" Style="display: none"></asp:TextBox>
                                (上传产证照片或者委托书+房东身份证照片)
                            </td>
                        </tr>
                        <tr>
                            <td>申请人：
                        </td>
                            <td>
                                <asp:Label ID="lblEmpName" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>申请时间</td>
                            <td>
                                <asp:Label ID="lblexe_Date" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>开盘录音：
                        </td>
                            <td>
                                <div style="height: 140px; width: auto; overflow: auto">
                                    <asp:Repeater ID="hf_ly2" runat="server" OnItemDataBound="hf_ly2_ItemDataBound">
                                        <ItemTemplate>
                                            <asp:Panel ID="Panel1" runat="server" Style="float: left;">
                                                <span id="spanRb" runat="server">
                                                    <input type="radio" name="rb1" id="rb1" value='<%#Eval("phoneID") %>' />
                                                </span>
                                                <audio src="<%#Eval("a") %>" preload="none" controls="controls">
                                                </audio>
                                                <br />
                                                <%#Eval("startTime")!=DBNull.Value && Convert.ToInt32(Eval("startTime"))>0?"双方通话：【"+Eval("startTime")+"】秒开始，总时长【"+Eval("realSecond")+"】秒":""%>
                                                <br />
                                                上传员工：<%#Eval("c")%>
                                                <span style="color: red"><%#Convert.ToInt16(Eval("IsCheck"))==1?"(录音已保密)":""%></span><br />
                                                <asp:HyperLink ID="LYClose" runat="server" Style="color: red" NavigateUrl="House/HouseRecordFrom.aspx" mask="true" Target="dialog">录音保密</asp:HyperLink>
                                            </asp:Panel>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>申请状态：
                        </td>
                            <td>
                                <asp:Label ID="lblState" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>审核人：
                        </td>
                            <td>
                                <asp:Label ID="lblauditName" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>审核时间：
                        </td>
                            <td>
                                <asp:Label ID="lblaudit_Date" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>审核意见：
                        </td>
                            <td>
                                <asp:Label ID="lblRemark" runat="server"></asp:Label>
                                <asp:TextBox ID="txtRemark" runat="Server" CssClass="required" title="审核意见必须填写！" TextMode="MultiLine" Height="100" Width="350" Visible="false"></asp:TextBox></td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="formBar">
                <ul>
                    <li>
                        <div class="buttonActive" id="btnSave" runat="server">
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
    </div>
</body>
