<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangeTelList.aspx.cs"
    Inherits="HouseMIS.Web.House.ChangeTelList" %>

<script>
    function SetTest() {
        $("#myffrmFollowUpText").val("私盘到期转公盘");
        return formFind();

    }
    function ChangeOpenHouseEdit(houseId, houseNo, atype) {
        // 所有参数都是可选项。
        var url = 'House/HouseForm.aspx?NavTabId=<%=NavTabId %>&doAjax=true&HouseID=' + houseId + '&EditType=Edit';
        var options = { width: 660, height: 650, mixable: false }
        $.pdialog.open(url, "housewiew2_" + houseId, houseNo + "-修改房源", options);
    }
</script>
<html>
<head>
    <title></title>
</head>
<body>
    <form rel="pagerForm" runat="server" name="pagerForm" id="pagerForm" onsubmit="return navTabSearch(this);"
    action="House/ChangeTelList.aspx" method="post">
    <div style="position: relative">
        <%--<div class="panelBar">
            <ul>
                <li><span>跟进人</span><asp:textbox runat="server" id="myffrmname" width="80px"></asp:textbox></li>
                <li><span>跟进类型</span>
                    <asp:dropdownlist id="ffrmFollowUpTypeID" runat="server"></asp:dropdownlist>
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
        <style>
            .dateRange label
            {
                width: auto;
            }
            .dateRange input
            {
                width: 40px;
            }
            .dateRange
            {
                padding-right: 5px !important;
            }
            .htab td
            {
                text-align: left;
                font-size: 12px;
                height: 24px;
                padding-left: 4px;
            }
            .htab select
            {
                width: 69px;
                float: left;
            }
            .input_h
            {
                width: 100px;
            }
            .ch_tabls input
            {
                float: left;
            }
            .ch_tabls label
            {
                float: left;
                width: auto;
            }
        </style>
        <div class="pageHeader">
            <div class="searchBar">
                <table class="searchContent">
                    <tr>
                        <td class="dateRange">
                            <label>
                                房源编号：</label>
                            <asp:TextBox runat="server" ID="myffrmHousecode" Width="80px"></asp:TextBox>
                        </td>
                        <td class="dateRange">
                            <label title="房东电话/联系电话">
                                老电话：</label>
                            <asp:TextBox ID="myffrmOldTel" runat="server" title="房东电话/联系电话" Width="75px"></asp:TextBox>
                        </td>
                        <td class="dateRange">
                            <label title="房东电话/联系电话">
                                新电话：</label>
                            <asp:TextBox ID="myffrmNewTel" runat="server" title="房东电话/联系电话" Width="75px"></asp:TextBox>
                        </td>
                        <td class="dateRange">
                            <label>
                                添加日期</label>
                            <asp:TextBox runat="server" ID="myffrmexe_date1" class="date" yearstart="-80" yearend="5"
                                Width="80px" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td class="dateRange">
                            <label>
                                -
                            </label>
                            <asp:TextBox runat="server" ID="myffrmexe_date2" class="date" yearstart="-80" yearend="5"
                                Width="80px" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td>
                            <div class="subBar">
                                <ul>
                                    <li>
                                        <div class="buttonActive">
                                            <div class="buttonContent">
                                                <button type="submit" onclick="return formFind()">
                                                    检索</button></div>
                                        </div>
                                    </li>
                                </ul>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class='panelBar'>
            <ul class='toolBar'>
                <asp:Literal runat="server" ID="ViewSet_gv"></asp:Literal>
            </ul>
        </div>
        <input type="hidden" name="status" value="status" />
        <input type="hidden" name="keywords" value="keywords" />
        <input type="hidden" name="numPerPage" value="20" />
        <input type="hidden" name="orderField" value="" />
        <input type="hidden" name="orderDirection" value="" />
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" DataKeyNames="TelChangeID"
            DataSourceID="ods" AllowPaging="True" CssClass="table" PageSize="20" CellPadding="0"
            GridLines="None" EnableModelValidation="True" EnableViewState="false">
            <Columns>
                <asp:TemplateField HeaderText="选择">
                    <HeaderTemplate>
                        <input type="checkbox" group="ids" class="checkboxCtrl" title="全部选择/取消选择">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <input name="ids" type="checkbox" value="<%#Eval("TelChangeID")%>" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="房源编号">
                    <ItemTemplate>
                        <a href="javascript:ChangeOpenHouseEdit('<%#Eval("HouseID") %>','<%#Eval("Shi_id") %>','1')">
                            <span>
                                <%#Eval("shi_id")%></span></a>
                    </ItemTemplate>
                    <HeaderStyle Width="80px" />
                    <ItemStyle Width="80px" />
                </asp:TemplateField>
                <asp:BoundField DataField="OldTel" HeaderText="老号码" SortExpression="OldTel">
                    <ItemStyle Width="100" />
                </asp:BoundField>
                <asp:BoundField DataField="NewTel" HeaderText="新号码" SortExpression="NewTel">
                    <ItemStyle Width="100" />
                </asp:BoundField>
                <asp:BoundField DataField="EmployeeName" HeaderText="操作人" SortExpression="AddEmployeeID">
                    <ItemStyle Width="150" />
                </asp:BoundField>
                <asp:BoundField DataField="exe_Date" HeaderText="添加日期" SortExpression="exe_Date"
                    DataFormatString="{0:d}">
                    <ItemStyle Width="130" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="ods" TypeName="HouseMIS.EntityUtils.TelChange" runat="server"
            EnablePaging="True" SelectCountMethod="FindCount" SelectMethod="FindAll" SortParameterName="orderClause"
            EnableViewState="false">
            <SelectParameters>
                <asp:Parameter Name="whereClause" Type="String" />
                <asp:Parameter Name="orderClause" Type="String" DefaultValue=" exe_Date desc" />
                <asp:Parameter Name="selects" Type="String" />
                <asp:Parameter Name="startRowIndex" Type="Int32" />
                <asp:Parameter Name="maximumRows" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <TCL:GridViewExtender ID="gvExt" runat="server" LayoutH="155" RowTarget="TelChangeID"
            RowRel="TelChangeID">
        </TCL:GridViewExtender>
    </div>
    </form>
</body>
</html>
