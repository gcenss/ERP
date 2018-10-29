<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HouseFromDK.aspx.cs" Inherits="HouseMIS.Web.House.HouseFromDK" %>

<body>
    <div style="height: 560px; overflow: auto;">
        <table id="DK" class="table">
            <thead>
                <tr>
                    <th>跟进内容
                                </th>
                    <th>带看人
                                </th>
                    <th>陪看人
                                </th>
                    <th>客户编号
                                </th>
                    <th>客户类型
                                </th>
                    <th>房源编号
                                </th>
                    <th>带看证明
                                </th>
                    <th>跟进日期
                                </th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="DK" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%#Eval("Remarks")%>
                            </td>
                            <td>
                                <%#Eval("btName")%>
                            </td>
                            <td>
                                <%#Eval("pkName")%>
                            </td>
                            <td>
                                <%#Eval("Cus_id")%>
                            </td>
                            <td>
                                <%#Eval("isGongKe")%>
                            </td>
                            <td>
                                <%#Eval("shi_id")%>
                            </td>
                            <td>
                                <%#customerBringpingzheng(Eval("HID"), Eval("OperatorID"), Eval("BringCustomerID"))%><%--<a href="House/BringCustomerImg.aspx?BringCustomerID=<%#Eval("BringCustomerID") %>" target="dialog" width="510" height="700" fresh='true' maxable='false' title="跟进证明" >查看</a>--%>
                            </td>
                            <td>
                                <%#Eval("exe_Date")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
        <a style="color: Red;" rel="dlg_page1" href="customer/Customerbring.aspx?NavTabId=<%=NavTabId %>&doAjax=true&HouseID=<%=HouseID %><%=Request.QueryString["isRent"]!=null?"&isRent=1":"" %>"
            width="600" height="390" target="dialog" maxable='false' title="客户带看">客户带看</a>&nbsp;
    <%--<a style="color: Red;" rel="GJF_House" title="添加积分跟进" href="House/FollowUpEditor.aspx?GJAtype=16&HouseID=<%=HouseID %>&NavTabId=<%=NavTabId %>&doAjax=true"
        target="dialog" width="380" height="324">添加积分跟进</a>--%>
    </div>
</body>
