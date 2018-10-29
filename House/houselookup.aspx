<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="houselookup.aspx.cs" Inherits="HouseMIS.Web.House.houselookup" %>

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
    <form rel="pagerForm" runat="server" name="pagerForm" id="pagerForm" onsubmit="return dialogSearch(this);" action="House/houselookup.aspx" method="post">
        <div class="pageHeader">
            <div class="searchBar">
                <table class="searchContent">
                    <tr>
                        <td class="dateRange">
                            <label>日期</label></td>
                        <td class="dateRange">
                            <asp:TextBox ID="mysfrmupdate_date1" runat="server" CssClass="input_h date"
                                Width="50px"></asp:TextBox>
                        </td>
                        <td class="dateRange">—</td>
                        <td class="dateRange">
                            <asp:TextBox ID="mysfrmupdate_date2" runat="server" CssClass="input_h date"
                                Width="50px"></asp:TextBox>
                        </td>
                        <td class="dateRange">
                            <label>房源编号</label></td>
                        <td class="dateRange">
                            <asp:TextBox ID="sfrmshi_id" runat="server" IsLike="true" Width="60px"></asp:TextBox>
                            &nbsp;</td>
                        <td class="dateRange">
                            <label>楼盘</label></td>
                        <td class="dateRange">
                            <asp:TextBox ID="mysfrmName" runat="server" IsLike="true" Width="60px"></asp:TextBox></td>
                        <td class="dateRange">
                            <label>栋号</label></td>
                        <td class="dateRange">
                            <asp:TextBox ID="mysfrmDName" runat="server" IsLike="true" Width="40px"></asp:TextBox></td>
                        <td class="dateRange">
                            <label>室号</label></td>
                        <td class="dateRange">
                            <asp:TextBox ID="mysfrmSName" runat="server" IsLike="true" Width="40px"></asp:TextBox></td>
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
                DataKeyNames="HouseID,shi_id" AllowPaging="True" CssClass="table" PageSize="20"
                CellPadding="0" GridLines="None" EnableModelValidation="True"
                EnableViewState="False" OnRowDataBound="gv_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="选择">
                        <ItemStyle Width="25px" />
                        <HeaderTemplate>
                            <input type="checkbox" group="HouseIDs" class="checkboxCtrl" title="全部选择/取消选择">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <input name="HouseID" type="checkbox" value="<%#Eval("HouseID")%>" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="update_date" HeaderText="日期" SortExpression="update_date">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="shi_id" HeaderText="编号" SortExpression="shi_id">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="楼盘">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="HouseDicID"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="180px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="build_area" HeaderText="面积" SortExpression="build_area">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="sum_price" HeaderText="价格" SortExpression="sum_price">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="HouseDicAddress" HeaderText="地址" SortExpression="HouseDicAddress">
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
                            <a class="btnSelect" href="javascript:$.bringBack({frmHouseList:'<%#Eval("shi_id")%>',
                                                                                HouseDicName:'<%#Eval("HouseDicName")%>',
                                                                                build_id:'<%#Eval("Build_id")%>',
                                                                                build_room:'<%#Eval("Build_room")%>',
                                                                                frmshi_id:'<%#Eval("HouseID")%>',
                                                                                frmbuild_area:'<%#Eval("Build_area") %>',
                                                                                frmshi_addr:'<%#Eval("HouseDicName")+"-"+Eval("Build_id")+"-"+Eval("Build_room")%>',
                                                                                frmsum_price:'<%#Eval("sum_price") %>',
                                                                                frmHouseDicName_CZ:'<%#Eval("HouseDicName_CZ") %>'});"
                                title="查找带回">选择</a>
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
            <TCL:GridViewExtender ID="gvExt" runat="server" LayoutH="118" RowTarget="HouseID" TargetType="dialog"
                RowRel="HouseID">
            </TCL:GridViewExtender>
        </div>
    </form>
</body>

