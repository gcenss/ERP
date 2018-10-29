<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HouseProList.aspx.cs" Inherits="HouseMIS.Web.House.HouseProList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript">
        function OpenHouseEdit(houseId, houseNo) {
            var url = 'House/HouseForm.aspx?NavTabId=50_menu2001&doAjax=true&HouseID=' + houseId + '&EditType=Edit';
            var options = { width: 660, height: 680, mixable: false }
            $.pdialog.open(url, "houseview2_" + houseId, houseNo + "-修改房源", options);
        }
    </script>
</head>
<body>
    <form rel="pagerForm" runat="server" name="pagerForm" id="pagerForm" onsubmit="return navTabSearch(this);" action="House/HouseProList.aspx" method="post">
        <div style="position: relative">
            <div class="pageHeader">
                <div class="searchBar">
                    <table class="searchContent">
                        <tr>
                            <td class="dateRange">
                                <label>房源编号</label>
                                <asp:TextBox ID="myffrmshi_id" runat="server" IsLike="true" Width="80px"></asp:TextBox>
                            </td>

                            <td class="dateRange">
                                <label>质疑人</label>
                                <asp:TextBox ID="myffrmemployeeName" runat="server" Width="70px"></asp:TextBox>
                            </td>
                            <td class="dateRange">
                                <label>被质疑人</label>
                                <asp:TextBox ID="myffrmemployee_ProName" runat="server" Width="70px"></asp:TextBox>
                            </td>
                            <td class="dateRange">
                                <label>是否处理</label>
                                <asp:DropDownList ID="myffrmisFinish" runat="server">
                                    <asp:ListItem Text="全部" Selected="True" Value=""></asp:ListItem>
                                    <asp:ListItem Text="是" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="否" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="dateRange">
                                <label>房源核验</label>
                                <asp:DropDownList ID="myffrmh_Check" runat="server">
                                    <asp:ListItem Text="全部" Selected="True" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="质疑" Value="True"></asp:ListItem>
                                    <asp:ListItem Text="正常" Value="False"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="dateRange">
                                <label>户型图</label>
                                <asp:DropDownList ID="myffrmh_hxt" runat="server">
                                    <asp:ListItem Text="全部" Selected="True" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="质疑" Value="True"></asp:ListItem>
                                    <asp:ListItem Text="正常" Value="False"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="dateRange">
                                <label>室内外图</label>
                                <asp:DropDownList ID="myffrmh_pic" runat="server">
                                    <asp:ListItem Text="全部" Selected="True" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="质疑" Value="True"></asp:ListItem>
                                    <asp:ListItem Text="正常" Value="False"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="dateRange">
                                <label>质疑房源</label>
                                <asp:DropDownList ID="myffrmZY" runat="server">
                                    <asp:ListItem Text="全部" Selected="True" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="我的质疑房源" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="我的被质疑房源" Value="2"></asp:ListItem>
                                </asp:DropDownList>
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
                                    </ul>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="panelBar">
            <ul class="toolBar">
                <%=EmpStr.ToString() %>
            </ul>
        </div>
        <input type="hidden" name="status" value="status" />
        <input type="hidden" name="keywords" value="keywords" />
        <input type="hidden" name="numPerPage" value="20" />
        <input type="hidden" name="orderField" value="" />
        <input type="hidden" name="orderDirection" value="" />
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" DataKeyNames="hProID" DataSourceID="ods" AllowPaging="True" CssClass="table" PageSize="20" CellPadding="0" GridLines="None" EnableModelValidation="True" EnableViewState="false">
            <Columns>
                <asp:TemplateField HeaderText="房源编号">
                    <ItemTemplate>
                        <a href="javascript:OpenHouseEdit('<%#Eval("HouseID") %>','<%#Eval("Shi_id") %>')"><span><%#Eval("shi_id")%></span></a>
                    </ItemTemplate>
                    <ItemStyle Width="130" />
                </asp:TemplateField>
                <asp:BoundField DataField="h_CheckName" HeaderText="房源核验" SortExpression="h_Check">
                    <ItemStyle Width="80" />
                </asp:BoundField>
                <asp:BoundField DataField="h_hxtName" HeaderText="户型图" SortExpression="h_hxt">
                    <ItemStyle Width="80" />
                </asp:BoundField>
                <asp:BoundField DataField="h_picName" HeaderText="室内外图" SortExpression="h_pic">
                    <ItemStyle Width="80" />
                </asp:BoundField>
                <asp:BoundField DataField="EmployeeName" HeaderText="质疑人" SortExpression="employeeID">
                    <ItemStyle Width="100" />
                </asp:BoundField>
                <asp:BoundField DataField="EmployeeProName" HeaderText="被质疑人" SortExpression="employeeID_Pro">
                    <ItemStyle Width="100" />
                </asp:BoundField>
                <asp:BoundField DataField="EmployeeFinishName" HeaderText="处理人">
                    <ItemStyle Width="100" />
                </asp:BoundField>
                <asp:BoundField DataField="remark" HeaderText="处理意见">
                    <ItemStyle Width="100" />
                </asp:BoundField>
                <asp:BoundField DataField="updateDate" HeaderText="处理日期" SortExpression="updateDate">
                    <ItemStyle Width="130" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="ods" TypeName="HouseMIS.EntityUtils.h_houseinfor_Problem" runat="server" EnablePaging="True" SelectCountMethod="FindCount" SelectMethod="FindAll" SortParameterName="orderClause" EnableViewState="false">
            <SelectParameters>
                <asp:Parameter Name="whereClause" Type="String" />
                <asp:Parameter Name="orderClause" Type="String" DefaultValue=" updateDate desc" />
                <asp:Parameter Name="selects" Type="String" />
                <asp:Parameter Name="startRowIndex" Type="Int32" />
                <asp:Parameter Name="maximumRows" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <TCL:GridViewExtender ID="gvExt" runat="server" LayoutH="155" RowTarget="hProID" RowRel="hProID">
        </TCL:GridViewExtender>
    </form>
</body>
</html>
