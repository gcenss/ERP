<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FollowAuditList.aspx.cs" Inherits="HouseMIS.Web.House.FollowAuditList" %>

<script type="text/javascript">
    function OpenHouseEdit_Follow_Sale(houseId, houseNo) {
        var url = 'House/HouseForm.aspx?NavTabId=<%=NavTabId %>&doAjax=true&HouseID=' + houseId + '&EditType=Edit';
        var options = { width: 660, height: 720, mixable: false }
        $.pdialog.open(url, "housewiew_Follow_Sale_" + houseId, houseNo + "-修改出售房源", options);
    }

    function OpenHouseEdit_Follow_Rent(houseId, houseNo) {
        var url = 'House/HouseRentForm.aspx?NavTabId=<%=NavTabId %>&doAjax=true&HouseID=' + houseId + '&EditType=Edit';
        var options = { width: 660, height: 720, mixable: false }
        $.pdialog.open(url, "housewiew_Follow_Rent_" + houseId, houseNo + "-修改出租房源", options);
    }
</script>

<body>
    <form name="pagerForm" id="pagerForm" runat="server" action="House/FollowAuditList.aspx" onsubmit="return navTabSearch(this);">
        <div class="pageHeader">
            <div class="searchBar">
                <table class="searchContent">
                    <tr>
                        <td class="dateRange">
                            <label>房源编号：</label>
                            <asp:TextBox runat="server" ID="myffrmHouseID" Width="80px"></asp:TextBox>
                        </td>
                        <td class="dateRange">
                            <label>申请人：</label>
                            <asp:TextBox runat="server" ID="myffrmEmployee" Width="120px"></asp:TextBox>
                        </td>
                        <td class="dateRange">
                            <label>申请日期：</label>
                            <asp:TextBox runat="server" ID="myffrmCreateTime" Width="120px" CssClass="date"></asp:TextBox>
                        </td>
                        <td>
                            <div class="subBar">
                                <ul>
                                    <li>
                                        <div class="buttonActive">
                                            <div class="buttonContent">
                                                <button type="submit" onclick="return formFind()">检索</button>
                                            </div>
                                        </div>
                                    </li>
                                </ul>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="dateRange">
                            <label>审核人：</label>
                            <asp:TextBox runat="server" ID="myffrmAuditEmp" Width="80px"></asp:TextBox>
                        </td>
                        <td class="dateRange">
                            <label>审核日期：</label>
                            <asp:TextBox runat="server" ID="myffrmAuditTime" class="date" Width="120px"></asp:TextBox>
                        </td>
                        <td>
                            <label>是否审核：</label>
                            <asp:DropDownList ID="myffrmddlAudit" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
            <div class='panelBar'>
                <ul class='toolBar'>
                    <li class='line'>line</li>
                    <%=z_bottom%>
                    <%=z_del%>
                </ul>
            </div>
        </div>

        <input type="hidden" name="NavTabId" value="<%=NavTabId %>" />
        <input type="hidden" name="status" value="status" />
        <input type="hidden" name="keywords" value="keywords" />
        <input type="hidden" name="numPerPage" value="20" />
        <input type="hidden" name="orderField" value="" />
        <input type="hidden" name="orderDirection" value="" />

        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" DataKeyNames="FollowAuditID" DataSourceID="ods" AllowSorting="True" AllowPaging="True" CssClass="table" PageSize="20" CellPadding="0" GridLines="None" EnableViewState="False">
            <Columns>
                <asp:TemplateField HeaderText="选择">
                    <HeaderTemplate>
                        <input type="checkbox" group="ids" class="checkboxCtrl" title="全部选择/取消选择">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <input name="ids" type="checkbox" value="<%#Eval("FollowAuditID")%>" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="房源编号">
                    <ItemTemplate>
                        <a href="<%#Eval("HouseType").ToString()=="售房"?"javascript:OpenHouseEdit_Follow_Sale('"+Eval("HouseID")+"','"+Eval("shi_id")+"')":"javascript:OpenHouseEdit_Follow_Rent('"+Eval("HouseID")+"','"+Eval("shi_id")+"')"%>"><%#Eval("shi_id")%></a>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="key" Width="80" />
                </asp:TemplateField>
                <asp:BoundField DataField="HouseType" HeaderText="房源类别" ItemStyle-Width="60">
                    <ItemStyle Width="60px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="FollowText" HeaderText="操作" ItemStyle-Width="200">
                    <ItemStyle Width="200px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="EmployeeName" HeaderText="申请人" ItemStyle-Width="80">
                    <ItemStyle Width="80px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="CreateTime" HeaderText="申请时间" SortExpression="CreateTime" ItemStyle-Width="120">
                    <ItemStyle Width="120px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="AuditEmployeeName" HeaderText="审核人" ItemStyle-Width="80">
                    <ItemStyle Width="80px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="AuditTime" HeaderText="审核时间" SortExpression="AuditTime" ItemStyle-Width="120">
                    <ItemStyle Width="120px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="HouseID" HeaderText="房源ID" Visible="false" ItemStyle-Width="60">
                    <ItemStyle Width="60px"></ItemStyle>
                </asp:BoundField>
            </Columns>
            <EmptyDataTemplate>
                没有符合条件的数据！
            </EmptyDataTemplate>
        </asp:GridView>

        <asp:ObjectDataSource ID="ods" runat="server" EnablePaging="True" SelectCountMethod="FindCount" SelectMethod="FindAll" SortParameterName="orderClause" EnableViewState="false">
            <SelectParameters>
                <asp:Parameter Name="whereClause" Type="String" />
                <asp:Parameter Name="orderClause" Type="String" />
                <asp:Parameter Name="selects" Type="String" />
                <asp:Parameter Name="startRowIndex" Type="Int32" />
                <asp:Parameter Name="maximumRows" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>

        <TCL:GridViewExtender ID="gvExt" runat="server" LayoutH="140" RowTarget="FollowAuditID" RowRel="FollowAuditID">
        </TCL:GridViewExtender>
    </form>
</body>
