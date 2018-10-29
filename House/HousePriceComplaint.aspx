<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HousePriceComplaint.aspx.cs" Inherits="HouseMIS.Web.House.HousePriceComplaint" %>

<script type="text/javascript">
    function OpenHouseEdit_Follow_Sale(houseId, houseNo) {
        var url = 'House/HouseForm.aspx?NavTabId=<%=NavTabId %>&doAjax=true&HouseID=' + houseId + '&EditType=Edit';
        var options = { width: 660, height: 720, mixable: false }
        $.pdialog.open(url, "housewiew_priceFollow_Sale_" + houseId, houseNo + "-修改出售房源", options);
    }
</script>
<body>
    <form name="pagerForm" id="pagerForm" runat="server" action="House/HousePriceComplaint.aspx" onsubmit="return navTabSearch(this);">
        <div class="pageHeader">
            <div class="searchBar">
                <table class="searchContent">
                    <tr>
                        <td class="dateRange">
                            <label>房源编号：</label>
                            <asp:TextBox runat="server" ID="myffrmshi_id" Width="80px"></asp:TextBox>
                        </td>
                        <td class="dateRange">
                            <label>投诉人：</label>
                            <asp:TextBox runat="server" ID="myffrmpriceEmpID" Width="120px"></asp:TextBox>
                        </td>
                        <td class="dateRange">
                            <label>投诉日期：</label>
                            <asp:TextBox runat="server" ID="myffrmComplaintTime" Width="120px" CssClass="date"></asp:TextBox>
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
                            <asp:TextBox runat="server" ID="myffrmHandlingEmpID" Width="80px"></asp:TextBox>
                        </td>
                        <td class="dateRange">
                            <label>审核日期：</label>
                            <asp:TextBox runat="server" ID="myffrmHandlingTime" class="date" Width="120px"></asp:TextBox>
                        </td>
                        <td>
                            <label>是否审核：</label>
                            <asp:DropDownList ID="myffrmHandlingResults" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <label>类型：</label>
                            <asp:DropDownList ID="myffrmstate" runat="server">
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

        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="ods" AllowSorting="True" AllowPaging="True" CssClass="table" PageSize="20" CellPadding="0" GridLines="None" EnableViewState="False">
            <Columns>
                <asp:TemplateField HeaderText="选择">
                    <HeaderTemplate>
                        <input type="checkbox" group="ids" class="checkboxCtrl" title="全部选择/取消选择">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <input name="ids" type="checkbox" value="<%#Eval("ID")%>" />
                    </ItemTemplate>
                    <ItemStyle Width="35" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="房源编号">
                    <ItemTemplate>
                        <a href="javascript:OpenHouseEdit_Follow_Sale('<%#Eval("HouseID").ToString()%>','<%#Eval("shi_id")%>')"><%# Eval("shi_id")%></a>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="key" Width="120" />
                </asp:TemplateField>
                <asp:BoundField DataField="Context" HeaderText="操作" ItemStyle-Width="350"></asp:BoundField>
                <asp:BoundField DataField="EmployeeName" HeaderText="投诉人/确认人" ItemStyle-Width="60">
                    <ItemStyle Width="80px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="ComplaintTxt" HeaderText="投诉内容">
                    <ItemStyle Width="200px"></ItemStyle>
                </asp:BoundField>
                <asp:TemplateField HeaderText="压价录音" ItemStyle-Width="230px" ItemStyle-Height="30px">
                    <ItemTemplate>
                        <%#Eval("recUrl").ToString()==string.Empty?"无":"<audio src="+Eval("recUrl")+" preload=\"none\" controls=\"controls\" style=\"width:250px;height:25px\"></audio>" %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="OkTime" HeaderText="确认时间" SortExpression="OkTime" ItemStyle-Width="80" DataFormatString="{0:yyyy-MM-dd}">
                    <ItemStyle Width="80px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="ComplaintTime" HeaderText="投诉时间" SortExpression="ComplaintTime" ItemStyle-Width="80" DataFormatString="{0:yyyy-MM-dd}">
                    <ItemStyle Width="80px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="AuditEmployeeName" HeaderText="审核人" ItemStyle-Width="60">
                    <ItemStyle Width="80px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="HandlingTime" HeaderText="审核时间" SortExpression="HandlingTime" ItemStyle-Width="80" DataFormatString="{0:yyyy-MM-dd}">
                    <ItemStyle Width="80px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="HandlingResultsName" HeaderText="审核结果" ItemStyle-Width="80">
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
                <asp:Parameter Name="orderClause" Type="String" DefaultValue="createTime desc" />
                <asp:Parameter Name="selects" Type="String" />
                <asp:Parameter Name="startRowIndex" Type="Int32" />
                <asp:Parameter Name="maximumRows" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>

        <TCL:GridViewExtender ID="gvExt" runat="server" LayoutH="140" RowTarget="ID" RowRel="ID">
        </TCL:GridViewExtender>
    </form>
</body>
