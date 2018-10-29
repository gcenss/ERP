<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HouseKeyList.aspx.cs" Inherits="HouseMIS.Web.House.HouseKeyList" %>

<body>
    <form rel="pagerForm" runat="server" id="pagerForm" class="HKeyList" onsubmit="return navTabSearch(this);"
        action="House/HouseKeyList.aspx" method="post">

        <div class="pageContent" style="padding: 0px 2px 2px 2px;">
            <div class='panelBar'>
                <ul class='toolBar'>
                    <%=ToolBtn()%>
                </ul>
            </div>
            <%--            <input type="hidden" name="doajax" value="true" />--%>
            <input type="hidden" id="HouseNavTabId" name="NavTabId" value="<%=NavTabId %>" />
            <input type="hidden" name="status" value="status" />
            <input type="hidden" name="keywords" value="keywords" />
            <input type="hidden" name="numPerPage" value="20" />
            <input type="hidden" name="orderField" value="" />
            <input type="hidden" name="orderDirection" value="" />
            <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" DataKeyNames="HouseKeyID"
                DataSourceID="ods" AllowPaging="True" CssClass="table" PageSize="20" CellPadding="0"
                GridLines="None" EnableModelValidation="True" EnableViewState="false">
                <Columns>
                    <asp:TemplateField HeaderText="选择">
                        <HeaderTemplate>
                            <input type="checkbox" group="idsHouseKeyID" class="checkboxCtrl" title="全部选择/取消选择">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <input name="idsHouseKeyID" type="checkbox" value="<%#Eval("HouseKeyID")%>" />
                        </ItemTemplate>
                        <ItemStyle Width="30" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="House_No" HeaderText="房源编号">
                        <ItemStyle Width="150" />
                    </asp:BoundField>
                    <asp:BoundField DataField="KeyValues" HeaderText="钥匙记录">
                        <ItemStyle Width="230" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Remarks" ItemStyle-Width="200" HeaderText="备注" />
                    <asp:TemplateField>
                        <HeaderTemplate>
                            钥匙证明
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%#Eval("imgUrl")==null?"无":"<a href=\"House/HouseKeyImg.aspx?HouseKeyID=" + Eval("HouseKeyID") + "\" target=\"dialog\" width=\"700\" height=\"700\" fresh='true' maxable='false' title=\"钥匙证明\" >查看</a>" %>
                        </ItemTemplate>
                        <ItemStyle Width="60" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="Name" HeaderText="操作员" ItemStyle-Width="80" />
                    <asp:BoundField DataField="exe_date" HeaderText="日期" SortExpression="exe_date">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="135px" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
            <asp:ObjectDataSource ID="ods" TypeName="HouseMIS.EntityUtils.h_HouseKey" runat="server"
                EnablePaging="True" SelectCountMethod="FindCount" SelectMethod="FindAll" SortParameterName="orderClause"
                EnableViewState="false">
                <SelectParameters>
                    <asp:Parameter Name="whereClause" Type="String" />
                    <asp:Parameter Name="orderClause" Type="String" DefaultValue=" exe_date desc" />
                    <asp:Parameter Name="selects" Type="String" />
                    <asp:Parameter Name="startRowIndex" Type="Int32" />
                    <asp:Parameter Name="maximumRows" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <TCL:GridViewExtender ID="gvExt" runat="server" LayoutH="80" PageNumberNav="false"
                RowTarget="HouseKeyID" RowRel="HouseKeyID" TargetType="dialog">
            </TCL:GridViewExtender>
        </div>
    </form>
</body>