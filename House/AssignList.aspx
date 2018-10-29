<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AssignList.aspx.cs" Inherits="HouseMIS.Web.House.AssignList" %>

<style>
    .dateRange label {
        width: auto;
    }

    .dateRange input {
        width: 40px;
    }

    .dateRange {
        padding-right: 5px !important;
    }

    .htab td {
        text-align: left;
        font-size: 12px;
        height: 24px;
        padding-left: 4px;
    }

    .htab select {
        width: 69px;
        float: left;
    }

    .input_h {
        width: 100px;
    }

    .ch_tabls input {
        float: left;
    }

    .ch_tabls label {
        float: left;
        width: auto;
    }
</style>

<body>
    <form rel="pagerForm" runat="server" name="pagerForm" id="pagerForm" onsubmit="return navTabSearch(this);" action="House/AssignList.aspx" method="post">
        <div style="position: relative">
            <%--<div class="panelBar">
            <ul>
                <li><span>跟进人</span><asp:textbox runat="server" id="myffrmname" width="80px"></asp:textbox></li>
                <li><span>跟进类型</span>
                    <asp:dropdownlist id="ffrmAssignTypeID" runat="server"></asp:dropdownlist>
                </li>
                <li><span>跟进日期</span><asp:textbox runat="server" id="myffrmOutDate1" class="date"
                    yearstart="-80" yearend="5" width="80px" readonly="true"></asp:textbox></li>
                <li><span>- </span>
                    <asp:textbox runat="server" id="myffrmOutDate2" class="date" yearstart="-80" yearend="5"
                        width="80px" readonly="true"></asp:textbox>
                </li>
                <li>
                    <button type="submit" onclick="return formFind()">检索</button></li>
            </ul>
        </div>--%>

            <div class="pageHeader">
                <div class="searchBar">
                    <table class="searchContent">
                        <tr>
                            <td class="dateRange">
                                <label>房源编号：</label>
                                <asp:TextBox runat="server" ID="myffrmHousecode" Width="80px"></asp:TextBox>
                            </td>
                            <td class="dateRange">
                                <label>修改日期</label>
                                <asp:TextBox runat="server" ID="myffrmOutDate1" class="date" yearstart="-80" yearend="5" Width="80px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td class="dateRange">
                                <label>- </label>
                                <asp:TextBox runat="server" ID="myffrmOutDate2" class="date" yearstart="-80" yearend="5" Width="80px" ReadOnly="true"></asp:TextBox>
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
            <input type="hidden" name="status" value="status" />
            <input type="hidden" name="keywords" value="keywords" />
            <input type="hidden" name="numPerPage" value="20" />
            <input type="hidden" name="orderField" value="" />
            <input type="hidden" name="orderDirection" value="" />
            <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" DataKeyNames="AssignID" DataSourceID="ods" AllowPaging="True" CssClass="table" PageSize="20" CellPadding="0" GridLines="None" EnableViewState="False">
                <Columns>
                    <asp:TemplateField HeaderText="选择">
                        <HeaderTemplate>
                            <input type="checkbox" group="Assignids" class="checkboxCtrl" title="全部选择/取消选择">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <input name="Assignids" type="checkbox" value="<%#Eval("AssignID")%>" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="OldShi_id" HeaderText="老房源编号" SortExpression="OldShi_id">
                        <ItemStyle Width="120" />
                    </asp:BoundField>
                    <asp:BoundField DataField="NewShi_id" HeaderText="新房源编号" SortExpression="NewShi_id">
                        <ItemStyle Width="120" />
                    </asp:BoundField>
                    <asp:BoundField DataField="OldEmployeeName" HeaderText="原所属人" SortExpression="OldEmployeeID">
                        <ItemStyle Width="200" />
                    </asp:BoundField>
                    <asp:BoundField DataField="NewEmployeeName" HeaderText="新所属人" SortExpression="NewEmployeeID">
                        <ItemStyle Width="200" />
                    </asp:BoundField>
                    <asp:BoundField DataField="exe_Date" HeaderText="操作日期" SortExpression="exe_Date">
                        <ItemStyle Width="130" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
            <asp:ObjectDataSource ID="ods" TypeName="HouseMIS.EntityUtils.h_Assign" runat="server" EnablePaging="True" SelectCountMethod="FindCount" SelectMethod="FindAll" SortParameterName="orderClause" EnableViewState="false">
                <SelectParameters>
                    <asp:Parameter Name="whereClause" Type="String" />
                    <asp:Parameter Name="orderClause" Type="String" DefaultValue=" exe_Date desc" />
                    <asp:Parameter Name="selects" Type="String" />
                    <asp:Parameter Name="startRowIndex" Type="Int32" />
                    <asp:Parameter Name="maximumRows" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <TCL:GridViewExtender ID="gvExt" runat="server" LayoutH="115" RowTarget="AssignID" RowRel="AssignID">
            </TCL:GridViewExtender>
        </div>
    </form>
</body>
</html>