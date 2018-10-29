<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShareSoucre.aspx.cs" Inherits="HouseMIS.Web.House.Templete.ShareSoucre" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form name="pagerForm" id="pagerForm" runat="server" rel="pagerForm" action="House/Templete/ShareSoucre.aspx"
    onsubmit="return navTabSearch(this);">
    <div class="pageHeader">
         <div class="searchBar">
            
         </div>
     </div>
    <div class="panelBar">
        <ul class="toolBar">

        </ul>
    </div>
    <input type="hidden" name="NavTabId" value="<%=NavTabId %>" />
    <input type="hidden" name="status" value="status" />
    <input type="hidden" name="keywords" value="keywords" />
    <input type="hidden" name="numPerPage" value="15" />
    <input type="hidden" name="orderField" value="" />
    <asp:GridView ID="gvShareHouseList" runat="server" AutoGenerateColumns="False" DataKeyNames="id"
        DataSourceID="ods" AllowPaging="True" CssClass="table" PageSize="20" CellPadding="0"
        GridLines="None" EnableModelValidation="True" EnableViewState="False" >
        <Columns>
            <asp:BoundField DataField="IPAddress" HeaderText="IP地址" >
                <ItemStyle Width="120" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="地址">
             <ItemStyle Width="200" />
            <ItemTemplate>
            <%#Eval("AddressName")%>
            </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="VisitEquipmentName" HeaderText="访问设备">
                <ItemStyle Width="300" />
            </asp:BoundField>
            <asp:BoundField DataField="VisitTime" HeaderText="访问时间">
                <ItemStyle Width="120" />
            </asp:BoundField>
          
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="ods" runat="server" EnablePaging="True" SelectCountMethod="FindCount"
        SelectMethod="FindAll" SortParameterName="orderClause" EnableViewState="false">
        <SelectParameters>
            <asp:Parameter Name="whereClause" Type="String" />
            <asp:Parameter Name="orderClause" Type="String" DefaultValue="VisitTime desc" />
            <asp:Parameter Name="selects" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <TCL:GridViewExtender ID="gvExt" runat="server" LayoutH="150" RowTarget="id" RowRel="id">
    </TCL:GridViewExtender>
    </form>
</body>
</html>
