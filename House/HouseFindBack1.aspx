<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HouseFindBack1.aspx.cs" Inherits="HouseMIS.Web.House.HouseFindBack1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form rel="pagerForm" runat="server"  name="pagerForm" id="pagerForm" onsubmit="return dialogSearch(this);" action="House/HouseFindBack1.aspx" method="post">
      <div class="pageHeader">
         <div class="searchBar close">
         <table class="searchContent">
             <tr>
                 <td class="dateRange">
                     <label>
                         楼盘名：</label>
                     <asp:TextBox ID="ffrmHouseDicName" runat="server" IsLike="true" Width="100px"></asp:TextBox>
                 </td>
                 <td class="dateRange">
                     <label>
                         房源编号：</label>
                     <asp:TextBox ID="ffrmshi_id" runat="server" Width="100px"></asp:TextBox>
                 </td>
                 <td>
                     <div class="subBar">
                         <ul>
                             <li>
                                 <div class="buttonActive">
                                     <div class="buttonContent">
                                         <button type="submit" onclick="return formFind()">
                                             检索</button>
                                     </div>
                                 </div>
                             </li>
                             <div class="button">
                                 <div class="buttonContent">
                                     <button type="button" multlookup="EmpIds" warn="请选择员工">
                                         选择带回</button></div>
                             </div>
                         </ul>
                     </div>
             </tr>
           </table>
         </div>
      </div>
    <div class="pageContent" style="padding: 0px 2px 2px 2px">
        <input type="hidden" name="NavTabId" value="<%=NavTabId %>" />
        <input type="hidden" name="status" value="status" />
        <input type="hidden" name="keywords" value="keywords" />
        <input type="hidden" name="numPerPage" value="20" />
        <input type="hidden" name="orderField" value="" />
        <input type="hidden" name="orderDirection" value="" />
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" DataKeyNames="HouseID"
            DataSourceID="ods" AllowPaging="True" CssClass="table" PageSize="20" CellPadding="0"
            GridLines="None" EnableModelValidation="True" EnableViewState="False" Width="100%">
            <Columns>
                <asp:TemplateField HeaderText="选择">
                    <HeaderTemplate>
                        <input type="checkbox" group="EmpIds" class="checkboxCtrl" title="全部选择/取消选择">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <input name="EmpIds" type="checkbox" value="{HouseID:'<%#Eval("HouseID") %>', Name:'<%#Eval("HouseDicName") %>'}" />
                    </ItemTemplate>
                    <ItemStyle Width="30px" />
                </asp:TemplateField>
                <asp:BoundField DataField="shi_id" HeaderText="房源编号"></asp:BoundField>
                <asp:BoundField DataField="HouseDicName" HeaderText="楼盘名称" InsertVisible="False"
                    ReadOnly="True"></asp:BoundField>
                <asp:BoundField DataField="HouseType" HeaderText="户型"></asp:BoundField>
                <asp:BoundField DataField="FloorAll" HeaderText="楼层"></asp:BoundField>
                <asp:BoundField DataField="build_area" HeaderText="面积"></asp:BoundField>
                <asp:BoundField DataField="sum_price" HeaderText="价格"></asp:BoundField>
                <asp:BoundField DataField="Renovation" HeaderText="装修"></asp:BoundField>
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="ods" runat="server" EnablePaging="True" SelectCountMethod="FindCount"
            SelectMethod="FindAll" SortParameterName="orderClause" EnableViewState="false">
            <SelectParameters>
                <asp:Parameter Name="whereClause" Type="String"  DefaultValue="StateID=2" />
                <asp:Parameter Name="orderClause" Type="String" />
                <asp:Parameter Name="selects" Type="String" />
                <asp:Parameter Name="startRowIndex" Type="Int32" />
                <asp:Parameter Name="maximumRows" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <TCL:GridViewExtender ID="gvExt" runat="server" TargetType="dialog" layouth="80" RowTarget="HouseID" RowRel="HouseID" PageNumberNav="false">
        </TCL:GridViewExtender>
    </div>
    </form>
</body>
</html>
