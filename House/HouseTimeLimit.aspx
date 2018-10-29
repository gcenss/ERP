<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HouseTimeLimit.aspx.cs" Inherits="HouseMIS.Web.House.HouseTimeLimit" %>

<body>
    <div style="height: 560px; overflow: auto;">
        <table id="TL" class="table">
            <thead>
                <tr>
                    <th>限入人
                                </th>
                    <th>责任人
                                </th>
                    <th>定金金额
                                </th>
                    <th>状态
                                </th>
                    <th>基本信息
                                </th>
                    <th>协议照片
                                </th>
                    <th>日期
                                </th>  
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="DK" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%#Eval("name1")%>
                            </td>
                            <td>
                                <%#Eval("name2")%>
                            </td>
                            <td>
                                <%#Eval("Money")%>
                            </td>
                            <td>
                                <%#Eval("state")%>
                            </td>
                            <td>
                                <%#Eval("Remark")%>
                            </td>
                            <td>
                                <%#customerBringpingzheng(Eval("HouseID"), Eval("EmployeeID"), Eval("ID"))%>
                            </td>
                            <td>
                                <%#Eval("exe_Date")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
        <a style="color: Red;" rel="dlg_page1" href="House/HouseTimeLimit_Edit.aspx?NavTabId=<%=NavTabId %>&doAjax=true&HouseID=<%=HouseID %>"
            width="600" height="360" target="dialog" maxable='false' title="限时协议">限时协议</a>&nbsp;
    </div>
</body>