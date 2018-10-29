<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RecordsList.aspx.cs" Inherits="HouseMIS.Web.House.RecordsList" %>

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

<script type="text/javascript">
    function ChangeOpenHouseEdit(houseId, houseNo, atype) {
        // 所有参数都是可选项。
        var url = 'House/HouseForm.aspx?NavTabId=<%=NavTabId %>&doAjax=true&HouseID=' + houseId + '&EditType=Edit';
        var options = { width: 660, height: 650, mixable: false }
        $.pdialog.open(url, "housewiew2_" + houseId, houseNo + "-修改房源", options);
    }
</script>

<body>
    <form rel="pagerForm" runat="server" name="pagerForm" id="pagerForm" class="RDpagerForm" onsubmit="return navTabSearch(this);" action="House/RecordList.aspx" method="post">
        <div style="position: relative">

            <div class="pageHeader">
                <div class="searchBar">
                    <table class="searchContent">
                        <tr>
                            <%--<td class="dateRange">
				    <label>分组：</label>	<asp:dropdownlist id="myffrmSubGroupID" runat="server"></asp:dropdownlist>
                </td>--%>
                            <td class="dateRange">
                                <label>房源编号：</label>
                                <asp:TextBox runat="server" ID="myffrmHousecode" Width="80px"></asp:TextBox>
                            </td>
                            <td class="dateRange">
                                <label>拨打人工号：</label>
                                <asp:TextBox runat="server" ID="myffrmname" Width="80px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class='panelBar'>
                <ul class='toolBar'>
                    <li><a class="delete" href="House/RecordsList.aspx?NavTabId=<%=NavTabId %>&doAjax=true&doType=delall&phoneID={phoneID}"
                        target="ajaxTodo" title="删除吗?"><span>删除</span></a></li>
                </ul>
            </div>
            <input type="hidden" name="status" value="status" />
            <input type="hidden" name="keywords" value="keywords" />
            <input type="hidden" name="numPerPage" value="20" />
            <input type="hidden" name="orderField" value="" />
            <input type="hidden" name="orderDirection" value="" />
            <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" DataKeyNames="phoneID" DataSourceID="ods" AllowPaging="True" CssClass="table" PageSize="20" CellPadding="0" GridLines="None" EnableModelValidation="True" EnableViewState="false">
                <Columns>
                    <asp:TemplateField HeaderText="选择">
                        <HeaderTemplate>
                            <input type="checkbox" group="ids" class="checkboxCtrl" title="全部选择/取消选择">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <input name="ids" type="checkbox" value="<%#Eval("phoneID")%>" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="房源编号">
                        <ItemTemplate>
                            <a href="javascript:ChangeOpenHouseEdit('<%#Eval("houseID") %>','<%#Eval("Shi_id") %>','1')">

                                <%#Eval("shi_id")%></a>
                        </ItemTemplate>
                        <HeaderStyle Width="80px" />
                        <ItemStyle Width="80px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="EmployeeName" HeaderText="拨打人" SortExpression="employeeID">
                        <ItemStyle Width="80" />
                    </asp:BoundField>
                    <asp:BoundField DataField="dateCreated" HeaderText="拨打时间" SortExpression="dateCreated">
                        <ItemStyle Width="150" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
            <asp:ObjectDataSource ID="ods" TypeName="HouseMIS.EntityUtils.i_InternetPhone" runat="server" EnablePaging="True" SelectCountMethod="FindCount" SelectMethod="FindAll" SortParameterName="orderClause" EnableViewState="false">
                <SelectParameters>
                    <asp:Parameter Name="whereClause" Type="String" />
                    <asp:Parameter Name="orderClause" Type="String" DefaultValue=" dateCreated desc" />
                    <asp:Parameter Name="selects" Type="String" />
                    <asp:Parameter Name="startRowIndex" Type="Int32" />
                    <asp:Parameter Name="maximumRows" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <TCL:GridViewExtender ID="gvExt" runat="server" LayoutH="155" RowTarget="phoneID" RowRel="phoneID">
            </TCL:GridViewExtender>
        </div>
    </form>
</body>
