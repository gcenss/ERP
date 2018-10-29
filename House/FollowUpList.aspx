<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FollowUpList.aspx.cs" Inherits="HouseMIS.Web.House.FollowUpList" %>

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
    function OpenHouseEdit(houseId, houseNo) {
        var url = 'House/HouseForm.aspx?NavTabId=50_menu2001&doAjax=true&HouseID=' + houseId + '&EditType=Edit';
        var options = { width: 660, height: 668, mixable: false }
        $.pdialog.open(url, "housewiew2_" + houseId, houseNo + "-修改房源", options);
    }
    function GetEmF(tid, id) {
        $("#" + id).empty();
        var OrgID = parseInt($(tid).val());
        var html = HouseMIS.Web.House.FollowUpList.GetEmployeeFF(OrgID).value;
        var ArryList = html.split(",");
        for (var i = 0; i < ArryList.length; i++) {
            var Arr = ArryList[i].split("|");
            $("#" + id).append("<option value='" + Arr[0] + "'>" + Arr[1] + "</option>")
        }
    }
    function EmChangesFF() {
        var OrgSel = $("#OrgSelFF").val();
        var EmployeeSel = $("#EmployeeSelFF").val();
        navTab.reload("House/FollowUpList.aspx?NavTabId=<%=NavTabId %>" + "&EmployeeSel=" + EmployeeSel + "&OrgSel=" + OrgSel);
    }
</script>

<body>
    <form rel="pagerForm" runat="server" name="pagerForm" id="pagerForm" onsubmit="return navTabSearch(this);" action="House/FollowUpList.aspx" method="post">
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

            <div class="pageHeader">
                <div class="searchBar">
                    <table class="searchContent">
                        <tr>
                            <td class="dateRange">
                                <label>房源编号：</label>
                                <asp:TextBox runat="server" ID="myffrmHousecode" Width="80px"></asp:TextBox>
                            </td>
                            <td class="dateRange">
                                <label>类型：</label>
                                <asp:DropDownList ID="myffrmaType" CssClass="frmaType" runat="server"
                                    Width="68px">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem Value="0">出售</asp:ListItem>
                                    <asp:ListItem Value="1">出租</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <%--                         <td class="dateRange">
				             <label>跟进人：</label>
                                <asp:textbox runat="server" id="myffrmname" width="80px"></asp:textbox>
                         </td>--%>
                            <td class="dateRange">
                                <label>跟进类型</label>
                                <asp:DropDownList ID="ffrmFollowUpTypeID" runat="server"></asp:DropDownList>
                            </td>
                            <td class="dateRange">
                                <label>跟进日期</label>
                                <asp:TextBox runat="server" ID="myffrmOutDate1" class="date" yearstart="-80" yearend="5" Width="80px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td class="dateRange">
                                <label>- </label>
                                <asp:TextBox runat="server" ID="myffrmOutDate2" class="date" yearstart="-80" yearend="5" Width="80px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <%--<td class="dateRange" id="HFollowUpListSearch">
				             <label>分部</label>
                                  <asp:DropDownList ID="mysfrmOrgID" CssClass="mysfrmOrgID" runat="server" AppendDataBoundItems="true" Style="width: 105px;">
                            <asp:ListItem></asp:ListItem>
                        </asp:DropDownList>
                        <script type="text/javascript">
                            $("#HFollowUpListSearch .mysfrmOrgID").combobox({ size: 98 });
                        </script>
                         </td>--%>
                            <td id="housefollow" class="dateRange">分部：
                                <asp:DropDownList ID="OrgSelFF" runat="server">
                                </asp:DropDownList>
                                <script type="text/javascript">
                                    $("#housefollow #OrgSelFF").combobox({ size: 150 });
                                    $("#housefollow input:eq(0)").blur(function () {
                                        $("#EmployeeSelFF").empty();
                                        var OrgID = parseInt($("#OrgSelFF").val());
                                        var html = HouseMIS.Web.House.FollowUpList.GetEmployeeFF(OrgID).value;
                                        var ArryList = html.split(",");
                                        for (var i = 0; i < ArryList.length; i++) {
                                            var Arr = ArryList[i].split("|");
                                            $("#EmployeeSelFF").append("<option value='" + Arr[0] + "'>" + Arr[1] + "</option>")
                                        }
                                    });
                            </script>
                            </td>
                            <td>分部员工：<asp:DropDownList ID="EmployeeSelFF" runat="server" onchange="EmChangesFF()" Style="width: 90px;">
                                <asp:ListItem Value="0">请先选择分部</asp:ListItem>
                            </asp:DropDownList>
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
            <div class='panelBar'>
                <ul class='toolBar'>
                    <%=tBar %>
                    <asp:Literal runat="server" ID="ViewSet_gv"></asp:Literal>
                </ul>
            </div>
            <input type="hidden" name="status" value="status" />
            <input type="hidden" name="keywords" value="keywords" />
            <input type="hidden" name="numPerPage" value="20" />
            <input type="hidden" name="orderField" value="" />
            <input type="hidden" name="orderDirection" value="" />
            <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" DataKeyNames="FollowUpID" DataSourceID="ods" AllowPaging="True" CssClass="table" PageSize="20" CellPadding="0" GridLines="None" EnableModelValidation="True" EnableViewState="false">
                <Columns>
                    <asp:TemplateField HeaderText="选择">
                        <HeaderTemplate>
                            <input type="checkbox" group="Followids" class="checkboxCtrl" title="全部选择/取消选择">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <input name="Followids" type="checkbox" value="<%#Eval("FollowUpID")%>" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="FollowUpType" HeaderText="跟进类别" SortExpression="FollowUpTypeID" />
                    <%--                <asp:BoundField DataField="Shi_Id" HeaderText="房源编号" SortExpression="Shi_Id">
                    <ItemStyle Width="70" />
                </asp:BoundField>--%>
                    <asp:TemplateField HeaderText="房源编号">
                        <ItemTemplate>
                            <%--<a href="javascript:OpenHouseEdit('<%#Eval("HouseID") %>','<%#Eval("Shi_id") %>')"><span><%#Eval("shi_id")%></span></a>--%>
                            <%#Eval("shi_id")%>
                        </ItemTemplate>
                        <ItemStyle Width="150" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="FollowUpText" HeaderText="跟进内容" SortExpression="FollowUpText">
                        <ItemStyle Width="300" />
                    </asp:BoundField>
                    <asp:BoundField DataField="EmployeeName" HeaderText="跟进人" SortExpression="EmployeeID">
                        <ItemStyle Width="250" />
                    </asp:BoundField>
                    <asp:BoundField DataField="exe_Date" HeaderText="跟进日期" SortExpression="exe_Date">
                        <ItemStyle Width="130" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
            <asp:ObjectDataSource ID="ods" TypeName="HouseMIS.EntityUtils.h_FollowUp" runat="server" EnablePaging="True" SelectCountMethod="FindCount" SelectMethod="FindAll" SortParameterName="orderClause" EnableViewState="false">
                <SelectParameters>
                    <asp:Parameter Name="whereClause" Type="String" />
                    <asp:Parameter Name="orderClause" Type="String" DefaultValue=" exe_Date desc" />
                    <asp:Parameter Name="selects" Type="String" />
                    <asp:Parameter Name="startRowIndex" Type="Int32" />
                    <asp:Parameter Name="maximumRows" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <TCL:GridViewExtender ID="gvExt" runat="server" LayoutH="155" RowTarget="FollowUpID" RowRel="FollowUpID">
            </TCL:GridViewExtender>
        </div>
    </form>
</body>
