<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HouseHS.aspx.cs" Inherits="HouseMIS.Web.House.HouseHS" %>

<script>
    function GetEm(tid, id) {
        $("#" + id).empty();
        var OrgID = parseInt($(tid).val());
        var html = HouseMIS.Web.House.HouseHS.GetEmployee(OrgID).value;
        var ArryList = html.split(",");
        for (var i = 0; i < ArryList.length; i++) {
            var Arr = ArryList[i].split("|");
            $("#" + id).append("<option value='" + Arr[0] + "'>" + Arr[1] + "</option>")
        }
    }
    function EmChange() {
        var OrgSel = $("#OrgSel").val();
        var EmployeeSel = $("#EmployeeSel").val();
        $("#fbyg333").attr("href", $("#fbyg333").attr("href") + "&EmployeeSel=" + EmployeeSel + "&OrgSel=" + OrgSel);
        $("#fbyg333").click();
    }
    function FPHouse() {
        var arry = $("input[name='ids']:checked");
        var result = false;
        $(arry).each(function () {
            result = true;
        });

        var EmployeeID = $("#EmployeeSelM").val();
        var OrgID = $("#OrgSelM").val();
        var new_shi_id = $("#new_shi_id").val();
        if (result) {
            $("#fp_1").attr("href", $("#fp_1").attr("href") + "&EmployeeID=" + EmployeeID + "&OrgID=" + OrgID + "&Nshi_id=" + new_shi_id);
            $("#fp_1").click();
        } else {
            $("#fp_2").attr("href", $("#fp_2").attr("href") + "&EmployeeID=" + EmployeeID + "&OrgID=" + OrgID + "&Nshi_id=" + new_shi_id);
            $("#fp_2").click();
        }
    }
</script>

<body>
    <form runat="server" name="pagerForm" id="pagerForm" class="House_HS" action="House/HouseHS.aspx" onsubmit="return navTabSearch(this);" method="post">
        <div class="pageContent" style="padding: 0px 2px 2px 2px">
            <table style="vertical-align:top" width="100%">
                <tr>
                    <td valign="top">
                        <div id="area_jbsxBox" class="unitBox">
                            <link href="themes/css/other.css" rel="stylesheet" type="text/css" />
                            <div class="pageHeader">
                                <div class="searchBar">
                                    <table class="searchContent">
                                        <tr>
                                            <td id="househs_sel" class="dateRange">分部：<asp:DropDownList ID="OrgSel" runat="server" onchange="GetEm(this,'EmployeeSel')"></asp:DropDownList></td>
                                            <script type="text/javascript">
                                                $("#<%= OrgSel.ClientID%>").combobox({ size: 98 });
                                                $("#househs_sel input:eq(0)").blur(function () {
                                                    $("#EmployeeSel").empty();
                                                    var OrgID = parseInt($("#OrgSel").val());
                                                    var html = HouseMIS.Web.House.HouseHS.GetEmployee(OrgID).value;
                                                    var ArryList = html.split(",");
                                                    for (var i = 0; i < ArryList.length; i++) {
                                                        var Arr = ArryList[i].split("|");
                                                        $("#EmployeeSel").append("<option value='" + Arr[0] + "'>" + Arr[1] + "</option>")
                                                    }
                                                });
                                            </script>
                                        </tr>
                                        <tr>
                                            <td>分部员工：<asp:DropDownList ID="EmployeeSel" runat="server" onchange="EmChange()">
                                                <asp:ListItem Value="">请先选择分部</asp:ListItem>
                                            </asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td>过滤房号：<input type="text" name="SHshi_id" id="SHshi_id" />
                                            </td>
                                            <td>
                                                <div class="subBar" style="float: left!important;">
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
                                            <a id="fbyg333" href="House/HouseHS.aspx?NavTabId=<%=NavTabId %>" target="navTab" rel="<%=NavTabId %>" title="房源回收" style="display: none;"></a>
                                        </tr>
                                    </table>
                                </div>
                            </div>

                            <input type="hidden" name="NavTabId" value="<%=NavTabId %>" />
                            <input type="hidden" name="status" value="status" />
                            <input type="hidden" name="keywords" value="keywords" />
                            <input type="hidden" name="numPerPage" value="20" />
                            <input type="hidden" name="orderField" value="" />
                            <input type="hidden" name="orderDirection" value="" />
                            <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" DataKeyNames="HouseID,shi_id" DataSourceID="ods" AllowPaging="True" CssClass="table" PageSize="20" CellPadding="0" GridLines="None" EnableModelValidation="True" EnableViewState="false">
                                <Columns>
                                    <asp:TemplateField HeaderText="选择">
                                        <HeaderTemplate>
                                            <input type="checkbox" group="ids" class="checkboxCtrl" title="全部选择/取消选择">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <input name="ids" type="checkbox" value="<%#Eval("HouseID")%>" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:ImageField DataImageUrlField="Picture">
                                        <ItemStyle Width="30px" />
                                    </asp:ImageField>
                                    <asp:BoundField DataField="exe_date" HeaderText="日期" DataFormatString="{0:yyyy-MM-dd}" SortExpression="exe_date" />
                                    <asp:BoundField DataField="AtypeName" HeaderText="分类" SortExpression="atype" />
                                    <asp:BoundField DataField="shi_id" HeaderText="房源编号" SortExpression="shi_id" />
                                    <asp:BoundField DataField="HouseDicName" HeaderText="楼盘" SortExpression="HouseDicID" />
                                    <asp:BoundField DataField="HouseType" HeaderText="户型" SortExpression="form_foreroom" />
                                    <asp:BoundField DataField="FloorAll" HeaderText="楼层" SortExpression="build_floor" />
                                    <asp:BoundField DataField="SeeHouseType" HeaderText="状态" SortExpression="StateID" />
                                    <asp:BoundField DataField="sum_price" HeaderText="总价(万)" SortExpression="sum_price" />
                                    <asp:BoundField DataField="Prices" HeaderText="单价(元)" SortExpression="sum_price" />
                                </Columns>
                            </asp:GridView>
                            <asp:ObjectDataSource ID="ods" TypeName="HouseMIS.EntityUtils.H_houseinfor" runat="server" EnablePaging="True" SelectCountMethod="FindCount" SelectMethod="FindAll" SortParameterName="orderClause" EnableViewState="false">
                                <SelectParameters>
                                    <asp:Parameter Name="whereClause" Type="String" />
                                    <asp:Parameter Name="orderClause" Type="String" />
                                    <asp:Parameter Name="selects" Type="String" />
                                    <asp:Parameter Name="startRowIndex" Type="Int32" />
                                    <asp:Parameter Name="maximumRows" Type="Int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                            <TCL:GridViewExtender ID="gvExt" runat="server" LayoutH="136" RowTarget="HouseID" RowRel="HouseID">
                            </TCL:GridViewExtender>

                        </div>
                    </td>
                    <td style="vertical-align:top" width="210">
                        <div layouth="2" style="float: left; display: block; overflow: auto; width: 210px; border: solid 1px #CCC; line-height: 21px; background: #fff">
                            <div class='panelBar'>
                                <ul class='toolBar'>

                                    <div style="padding: 6px 0px 0px 10px;">分派设置</div>
                                </ul>
                            </div>
                            <table style="margin-left: 10px;">
                                <tr height="30">
                                    <td>目标门店：</td>
                                    <td id="housefp_sel2">
                                        <asp:DropDownList ID="OrgSelM" runat="server"
                                            onchange="GetEm(this,'EmployeeSelM')">
                                            <asp:ListItem Value="2">总部</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <script type="text/javascript">
                                    $("#<%= OrgSelM.ClientID%>").combobox({ size: 98 });
                                    $("#housefp_sel2 input:eq(0)").blur(function () {
                                        $("#EmployeeSelM").empty();
                                        var OrgID = parseInt($("#OrgSelM").val());
                                        var html = HouseMIS.Web.House.HouseHS.GetEmployee(OrgID).value;
                                        var ArryList = html.split(",");
                                        for (var i = 0; i < ArryList.length; i++) {
                                            var Arr = ArryList[i].split("|");
                                            $("#EmployeeSelM").append("<option value='" + Arr[0] + "'>" + Arr[1] + "</option>")
                                        }
                                    });
                                   </script>
                                <tr height="30">
                                    <td>目标员工：</td>
                                    <td>
                                        <asp:DropDownList ID="EmployeeSelM" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr height="30">
                                    <td colspan="2">新的编号：<input type="text" name="new_shi_id" id="new_shi_id" style="width: 90px;" /></td>
                                </tr>
                                <tr height="30">
                                    <td colspan="2">
                                        <div class="subBar" style="float: right;">
                                            <ul>
                                                <li>
                                                    <div class="buttonActive">
                                                        <div class="buttonContent">
                                                            <button type="submit" onclick="FPHouse()">回收</button>
                                                        </div>
                                                    </div>
                                                </li>
                                            </ul>
                                        </div>
                                    </td>
                                </tr>
                                <a id="fp_1" href="House/HouseHS.aspx?NavTabId=<%=NavTabId %>" posttype='string' rel='ids' target="selectedTodo" style="display: none;"></a>
                                <a id="fp_2" href="House/HouseHS.aspx?NavTabId=<%=NavTabId %>&HouseID={HouseID}" target="ajaxTodo" style="display: none;"></a>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
