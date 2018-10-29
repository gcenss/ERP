<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HousePriceFollowUpList.aspx.cs" Inherits="HouseMIS.Web.House.HousePriceFollowUpList" %>

<script type="text/javascript">
    function OpenHouseEdit_Follow_Sale(houseId, houseNo) {
        var url = 'House/HouseForm.aspx?NavTabId=<%=NavTabId %>&doAjax=true&HouseID=' + houseId + '&EditType=Edit';
        var options = { width: 660, height: 720, mixable: false }
        $.pdialog.open(url, "housewiew_priceFollowUp_Sale_" + houseId, houseNo + "-修改出售房源", options);
    }
</script>
<body>
    <form name="pagerForm" id="pagerForm" runat="server" action="House/HousePriceFollowUpList.aspx" onsubmit="return navTabSearch(this);">
        <div class="pageHeader">
            <div class="searchBar">
                <table class="searchContent">
                    <tr>
                        <td id="priceFollowUp" class="dateRange">
                            <label>门店：</label>
                            <asp:DropDownList ID="myffrmOrgID" CssClass="myffrmOrgID" runat="server" AppendDataBoundItems="true" Style="width: 100px;">
                            </asp:DropDownList>
                            <script type="text/javascript">
                                $("#priceFollowUp .myffrmOrgID").combobox({ size: 100 });
                                </script>
                        </td>
                        <td class="dateRange">
                            <label>房源编号：</label>
                            <asp:TextBox runat="server" ID="myffrmshi_id" Width="150px"></asp:TextBox>
                        </td>
                        <td class="dateRange">
                            <label>压价人：</label>
                            <asp:TextBox runat="server" ID="myffrmPriceEmpID" Width="100px"></asp:TextBox>
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
                        <td class="dateRange" colspan="2">
                            <label>压价日期：</label>
                            <asp:TextBox runat="server" ID="myffrmAddDate_begin" class="date" Width="120px" />
                            -
                            <asp:TextBox runat="server" ID="myffrmAddDate_end" class="date" Width="120px" />
                        </td>
                    </tr>
                </table>
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
                <asp:BoundField DataField="OldPrice" HeaderText="原实价">
                    <ItemStyle Width="80px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="NewPrice" HeaderText="跟进实价">
                    <ItemStyle Width="80px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="OldPrice_Sum" HeaderText="原总价">
                    <ItemStyle Width="80px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="NewPrice_Sum" HeaderText="跟进总价">
                    <ItemStyle Width="80px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="EmployeeName" HeaderText="跟进人">
                    <ItemStyle Width="80px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="Note" HeaderText="压价说明">
                    <ItemStyle Width="150px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="AddDate" HeaderText="跟进时间" SortExpression="AddDate" DataFormatString="{0:yyyy-MM-dd}">
                    <ItemStyle Width="80px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="stateName" HeaderText="价格维护人">
                    <ItemStyle Width="60px"></ItemStyle>
                </asp:BoundField>
                <asp:TemplateField HeaderText="通话录音" ItemStyle-Height="40">
                    <ItemTemplate>
                        <%#Eval("RecFilePath")==null ? 
                            "":
                            "<audio src='"+Eval("RecFilePath")+"' preload='none' controls='controls' style='width: 200px'></audio>" %>
                    </ItemTemplate>
                    <ItemStyle Width="100px"></ItemStyle>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                没有符合条件的数据！
            </EmptyDataTemplate>
        </asp:GridView>

        <asp:ObjectDataSource ID="ods" runat="server" EnablePaging="True" SelectCountMethod="FindCount" SelectMethod="FindAll" SortParameterName="orderClause" EnableViewState="false">
            <SelectParameters>
                <asp:Parameter Name="whereClause" Type="String" />
                <asp:Parameter Name="orderClause" Type="String" DefaultValue="AddDate desc" />
                <asp:Parameter Name="selects" Type="String" />
                <asp:Parameter Name="startRowIndex" Type="Int32" />
                <asp:Parameter Name="maximumRows" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>

        <TCL:GridViewExtender ID="gvExt" runat="server" LayoutH="140" RowTarget="ID" RowRel="ID">
        </TCL:GridViewExtender>
    </form>
</body>
