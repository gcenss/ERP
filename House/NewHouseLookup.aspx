<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewHouseLookup.aspx.cs" Inherits="HouseMIS.Web.House.NewHouseLookup" %>

<head id="Head1" runat="server">
    <title></title>
</head>
<style>
    .dateRange label {
        width: auto;
    }

    .dateRange input {
    }

    .dateRange {
        padding-right: 5px !important;
    }
</style>
<body>
    <form rel="pagerForm" runat="server" name="pagerForm" id="pagerForm" onsubmit="return dialogSearch(this);" action="House/NewHouseLookup.aspx" method="post">
        <div class="pageHeader">
            <div class="searchBar">
                <table class="searchContent">
                    <tr>
                        <td class="dateRange">
                            <label>房源编号</label></td>
                        <td class="dateRange">
                            <asp:TextBox ID="sfrmID" runat="server" IsLike="true" Width="60px"></asp:TextBox>
                            &nbsp;</td>
                        <td class="dateRange">
                            <label>楼盘</label></td>
                        <td class="dateRange">
                            <asp:TextBox ID="mysfrmName" runat="server" IsLike="true" Width="60px"></asp:TextBox></td>
                        <td class="dateRange">
                            <label>登记人</label>
                            <asp:TextBox ID="mysfrmem_name" runat="server" Width="60px"></asp:TextBox>
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
                </table>
            </div>
        </div>

        <div class="pageContent" style="padding: 0px 2px 2px 2px; width: 736px;">
            <input type="hidden" name="status" value="status" />
            <input type="hidden" name="keywords" value="keywords" />
            <input type="hidden" name="numPerPage" value="20" />
            <input type="hidden" name="orderField" value="" />
            <input type="hidden" name="orderDirection" value="" />
            <asp:GridView ID="GVSeeHouse" runat="server" AutoGenerateColumns="False" DataSourceID="ods"
                DataKeyNames="ID" AllowPaging="True" CssClass="table" PageSize="20"
                CellPadding="0" GridLines="None" EnableModelValidation="True"
                EnableViewState="False" OnRowDataBound="gv_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="选择">
                        <ItemStyle Width="25px" />
                        <HeaderTemplate>
                            <input type="checkbox" group="IDs" class="checkboxCtrl" title="全部选择/取消选择">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <input name="ID" type="checkbox" value="<%#Eval("ID")%>" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="UpdateTime" HeaderText="日期" SortExpression="UpdateTime">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ID" HeaderText="编号" SortExpression="ID">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Name" HeaderText="名称" SortExpression="Address">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Area" HeaderText="面积" SortExpression="Area">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="AvePrice" HeaderText="价格" SortExpression="AvePrice">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Address" HeaderText="地址" SortExpression="Address">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="登记人">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="OwnerEmployeeID"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="选择">
                        <ItemTemplate>
                            <a class="btnSelect" href="javascript:$.bringBack({frmHouseList:'<%#Eval("ID")%>',frmshi_id:'<%#Eval("ID")%>',frmbuild_area:'<%#Eval("Area") %>',frmshi_addr:'<%#Eval("Address")%>',frmsum_price:'<%#Eval("AvePrice") %>'},);" title="查找带回">选择</a>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
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
            <TCL:GridViewExtender ID="gvExt" runat="server" LayoutH="118" RowTarget="ID" TargetType="dialog"
                RowRel="ID">
            </TCL:GridViewExtender>
        </div>
    </form>
</body>


