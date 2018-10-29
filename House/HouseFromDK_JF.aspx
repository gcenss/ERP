<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HouseFromDK_JF.aspx.cs" Inherits="HouseMIS.Web.House.HouseFromDK_JF" %>

<body>
    <div style="height: 560px; overflow: auto;">
        <table id="DK" class="table">
            <thead>
                <tr>
                    <th>跟进类别
                                </th>
                    <th>跟进内容
                                </th>
                    <th>积分
                                </th>
                    <th>带看人
                                </th>
                    <th>客户编号
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
                            <td>客户带看
                        </td>
                            <td>
                                <%#Eval("Remarks")%>
                        </td>
                            <td>
                                <%#Eval("Integral") %>
                        </td>
                            <td>
                                <%#Eval("btName")%>
                        </td>
                            <td>
                                <%#Eval("Cus_id")%>
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
    </div>
</body>
