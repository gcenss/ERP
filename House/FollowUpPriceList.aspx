<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FollowUpPriceList.aspx.cs" Inherits="HouseMIS.Web.House.FollowUpPriceList" %>

<body>
    <form rel="pagerForm" runat="server" name="pagerForm" id="pagerForm" onsubmit="return dialogSearch(this);" action="Demo/LogList.aspx" method="post">
        <div class="pageContent" style="padding: 0px 2px 2px 2px">
            <div class="panelBar">
                <ul class="toolBar">
                    <li><a class='add' href='House/FollowUpPriceEditor.aspx?NavTabId=<%=NavTabId %>&doAjax=true&HouseID=<%=Request["HouseID"] %>' target='dialog' width='600' height='560' fresh='true' maxable='false' title="增加压价跟进" rel="zj_PricefollwUp"><span>增加压价跟进</span></a></li>
                </ul>
            </div>
            <input type="hidden" name="NavTabId" value="<%=NavTabId %>" />
            <input type="hidden" name="status" value="status" />
            <input type="hidden" name="keywords" value="keywords" />
            <input type="hidden" name="numPerPage" value="20" />
            <input type="hidden" name="orderField" value="" />
            <input type="hidden" name="orderDirection" value="" />
            <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False"
                DataKeyNames="ID" DataSourceID="ods" AllowPaging="false" CssClass="table"
                PageSize="20" CellPadding="0" GridLines="None" EnableModelValidation="True"
                EnableViewState="false">
                <Columns>
                    <%--            <asp:TemplateField HeaderText="选择">
                <HeaderTemplate>
                <input type="checkbox" group="ids" class="checkboxCtrl" title="全部选择/取消选择">
                </HeaderTemplate>
                <ItemTemplate>
                    <input name="ids" type="checkbox" value="<%#Eval("ID")%>" />
                </ItemTemplate>
                <ItemStyle Width="40px" />
            </asp:TemplateField>--%>
                    <asp:BoundField DataField="shi_id" HeaderText="房源编号">
                        <ItemStyle Width="120px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Note" HeaderText="跟进内容" ItemStyle-Width="120px">
                        <ItemStyle Width="120px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="OldPrice" HeaderText="原始实价" ItemStyle-Width="50px">
                        <ItemStyle HorizontalAlign="Right" Font-Bold="True" />
                    </asp:BoundField>
                    <asp:BoundField DataField="NewPrice" HeaderText="跟进实价" ItemStyle-Width="50px">
                        <ItemStyle HorizontalAlign="Right" Font-Bold="True" />
                    </asp:BoundField>
                    <%--<asp:BoundField DataField="Integral" HeaderText="所得积分" ItemStyle-Width="50px">
                        <ItemStyle Width="50px"></ItemStyle>
                    </asp:BoundField>--%>
                    <asp:BoundField DataField="EmployeeName" HeaderText="操作员"
                        ItemStyle-Width="90px">
                        <ItemStyle Width="90px"></ItemStyle>
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="通话录音" ItemStyle-Width="50px" ItemStyle-Height="40">
                        <ItemTemplate>
                            <%#Eval("RecFilePath")==null ? "":
                                    "<audio src='"+Eval("RecFilePath")+"' preload='none' controls='controls' style='width: 200px'></audio>" %>

                            <%--<video controls="" name="media" width="46" height="40">
                                <source src="<%#Eval("RecFilePath") %>" type="audio/x-wav">
                            </video>--%>
                        </ItemTemplate>
                        <ItemStyle Width="100px"></ItemStyle>
                    </asp:TemplateField>
                    <asp:BoundField DataField="AddDate" HeaderText="操作时间" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                    </asp:BoundField>
                </Columns>
                <RowStyle Height="40px" />
            </asp:GridView>
            <asp:ObjectDataSource ID="ods" runat="server" EnablePaging="True" SelectCountMethod="FindCount" SelectMethod="FindAll" SortParameterName="orderClause" EnableViewState="false">
                <SelectParameters>
                    <asp:Parameter Name="whereClause" Type="String" />
                    <asp:Parameter Name="orderClause" Type="String" DefaultValue="AddDate DESC" />
                    <asp:Parameter Name="selects" Type="String" />
                    <asp:Parameter Name="startRowIndex" Type="Int32" />
                    <asp:Parameter Name="maximumRows" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <TCL:GridViewExtender ID="gvExt" runat="server" LayoutH="60" RowTarget="ID" RowRel="ID" TargetType="dialog">
            </TCL:GridViewExtender>
        </div>
    </form>
</body>