<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HouseFromGJ.aspx.cs" Inherits="HouseMIS.Web.House.HouseFromGJ" %>

<body>
    <div style="height: 560px; overflow: auto;">
        <table id="FU" class="table">
            <thead>
                <tr>
                    <th>跟进类别
                                    </th>
                    <th>跟进内容
                                    </th>
                    <th>跟进人
                                    </th>
                    <th>跟进日期
                                    </th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="FU" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%#Eval("FollowUpType")%>
                                            </td>
                            <td style="width: 280px">
                                <%#Eval("FollowUpText")%>
                                            </td>
                            <td>
                                <%#Eval("EmployeeName")%></td>
                            <td>
                                <%#Eval("exe_Date")%>
                                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
</body>
