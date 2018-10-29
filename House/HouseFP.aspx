<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HouseFP.aspx.cs" Inherits="HouseMIS.Web.House.HouseFP" %>

<script>
    function GetEms(tid, id) {
        $("#" + id).empty();
        var OrgID = parseInt($(tid).val());
        var html = HouseMIS.Web.House.HouseFP.GetEmployee(OrgID).value;
        var ArryList = html.split(",");
        for (var i = 0; i < ArryList.length; i++) {
            var Arr = ArryList[i].split("|");
            $("#" + id).append("<option value='" + Arr[0] + "'>" + Arr[1] + "</option>")
        }
    }
    function EmChanges() {
        var OrgSel = $("#OrgSelF").val();
        var EmployeeSel = $("#EmployeeSelF").val();
        navTab.reload("House/HouseFP.aspx?NavTabId=<%=NavTabId %>" + "&EmployeeSel=" + EmployeeSel + "&OrgSel=" + OrgSel);
    }
    function FPHouse() {
        var EmployeeID = $("#EmployeeSelMF").val();
        var OrgID = $("#OrgSelMF").val();

        if (EmployeeID == "" || EmployeeID == null || EmployeeID == "0") {
            alertMsg.error('请选择目标员工！');
            return;
        }
        var arry = $("input[name='ids']:checked");
        var result = false;
        if (arry.length > 0) result = true;

        //$(arry).each(function () {
        //        result = true;
        //});
        if (result) {
            $("#fp_1F").attr("href", $("#fp_1F").attr("href") + "&EmployeeID=" + EmployeeID + "&OrgID=" + OrgID);
            $("#fp_1F").click();
        } else {
            $("#fp_2F").attr("href", $("#fp_2F").attr("href") + "&EmployeeID=" + EmployeeID + "&OrgID=" + OrgID);
            $("#fp_2F").click();
        }
    }
    function ShowNavB() {
        navTab.openTab("391_menuAssign", "House/AssignList.aspx?NavTabId=391_menuAssign", { title: "分派记录", fresh: false, data: {} });
    }
</script>

<body>
    <div class="pageContent" id="HouseFPIDs" style="padding: 0px 2px 2px 2px">
        <form runat="server" name="pagerForm" id="pagerForm" class="House_FP" action="House/HouseFP.aspx" onsubmit="return navTabSearch(this);" method="post">
            <input type="hidden" name="NavTabId" value="<%=NavTabId %>" />
            <table width="100%">
                <tr>
                    <td valign="top" width="260">
                        <div layouth="2" style="float: left; display: inherit; overflow: auto; width: 260px; border: solid 1px #CCC; border-top: none; line-height: 21px; background: #fff">
                            <div class='panelBar'>
                                <ul class='toolBar'>
                                    <div style="padding: 6px 0px 0px 10px;">分派设置</div>
                                </ul>
                            </div>
                            <table style="margin-left: 10px;">
                                <tr height="30">
                                    <td>目标门店：</td>
                                    <td id="housefp_sel2">
                                        <asp:DropDownList ID="OrgSelMF" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <script type="text/javascript">
                                    $("#<%= OrgSelMF.ClientID%>").combobox({ size: 150 });
                                    $("#housefp_sel2 input:eq(0)").blur(function () {
                                        $("#EmployeeSelMF").empty();
                                        var OrgID = parseInt($("#OrgSelMF").val());
                                        var html = HouseMIS.Web.House.HouseFP.GetEmployees(OrgID).value;
                                        var ArryList = html.split(",");
                                        for (var i = 0; i < ArryList.length; i++) {
                                            var Arr = ArryList[i].split("|");
                                            $("#EmployeeSelMF").append("<option value='" + Arr[0] + "'>" + Arr[1] + "</option>")
                                        }
                                    });
                                </script>
                                <tr height="30">
                                    <td>目标员工：</td>
                                    <td>
                                        <asp:DropDownList ID="EmployeeSelMF" runat="server" Style="width: 90px;">
                                        </asp:DropDownList></td>
                                </tr>
                                <tr height="30">
                                    <td colspan="2"></td>
                                </tr>
                                <tr height="30">
                                    <td colspan="2">
                                        <div class="subBar" style="float: right;">
                                            <ul>
                                                <li>
                                                    <div class="buttonActive">
                                                        <div class="buttonContent">
                                                            <button type="button" onclick="FPHouse()">分派</button>
                                                        </div>
                                                    </div>
                                                </li>
                                            </ul>
                                        </div>
                                    </td>
                                </tr>
                                <a id="fp_1F" href="House/HouseFP.aspx?NavTabId=<%=NavTabId %>" posttype='string' rel='ids' target="selectedTodo" style="display: none;"></a>
                                <a id="fp_2F" href="House/HouseFP.aspx?NavTabId=<%=NavTabId %>&HouseID={HouseID}" target="ajaxTodo" style="display: none;"></a>
                            </table>
                        </div>
                    </td>
                    <td valign="top">
                        <div id="area_jbsxBox" class="unitBox">
                            <link href="themes/css/other.css" rel="stylesheet" type="text/css" />
                            <div class="pageHeader">

                                <div class="searchBar">
                                    <table class="searchContent">
                                        <tr>
                                            <td id="housefp_sel" class="dateRange">分部：
                                                <asp:DropDownList ID="OrgSelF" runat="server">
                                                </asp:DropDownList>
                                                分部员工：<asp:DropDownList ID="EmployeeSelF" runat="server" onchange="EmChanges()">
                                                    <asp:ListItem Value="0">请先选择分部</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>

                                            <script type="text/javascript">
                                                $("#<%= OrgSelF.ClientID%>").combobox({ size: 150 });
                                                $("#housefp_sel input:eq(0)").blur(function () {
                                                    $("#EmployeeSelF").empty();
                                                    var OrgID = parseInt($("#OrgSelF").val());
                                                    var html = HouseMIS.Web.House.HouseFP.GetEmployee(OrgID).value;
                                                    var ArryList = html.split(",");
                                                    for (var i = 0; i < ArryList.length; i++) {
                                                        var Arr = ArryList[i].split("|");
                                                        $("#EmployeeSelF").append("<option value='" + Arr[0] + "'>" + Arr[1] + "</option>")
                                                    }
                                                });
                                            </script>

                                            <td colspan="2">登记日期:

                                                <asp:TextBox ID="mysfrmexe_date1" runat="server" CssClass="date" Style="width: 66px"></asp:TextBox>

                                                —

                                                <asp:TextBox ID="mysfrmexe_date2" runat="server" CssClass="date" Style="width: 66px"></asp:TextBox>
                                            </td>

                                            <td>房源编号：<input type="text" name="SHshi_id" id="SHshi_id" style="width: 80px" />
                                                状态:

                                                <asp:DropDownList ID="sfrmStateID" runat="server"></asp:DropDownList>
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
                                                        <%--  <li><div class="buttonActive"><div class="buttonContent"><button type="button" onclick="ShowNavB()">分派记录</button></div></div></li>--%>
                                                    </ul>
                                                </div>
                                            </td>

                                            <a id="fbyg333F" href="House/HouseFP.aspx?NavTabId=<%=NavTabId %>" rel="HouseFPIDs" target="navTab" title="房源分派" style="display: none;"></a>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div class="panelBar">
                                <ul class="toolBar">
                                    <li><a class="edit" href="House/AssignList.aspx?NavTabId=391_menuAssign" target="navTab" title="分派记录"><span>查看分派记录</span></a></li>
                                </ul>
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
                                    <asp:BoundField DataField="OwnerEmployeeName" HeaderText="首录人" ItemStyle-Width="60px" />
                                    <asp:BoundField DataField="exe_date" HeaderText="登记日期" ItemStyle-Width="80px" DataFormatString="{0:yyyy-MM-dd}" SortExpression="exe_date" />
                                    <asp:BoundField DataField="AtypeName" HeaderText="分类" SortExpression="atype" />
                                    <asp:BoundField DataField="shi_id" HeaderText="房源编号" ItemStyle-Width="120px" SortExpression="shi_id" />
                                    <asp:BoundField DataField="HouseDicName" HeaderText="楼盘" ItemStyle-Width="150px" SortExpression="HouseDicID" />
                                    <asp:BoundField DataField="HouseType" HeaderText="户型" ItemStyle-Width="100px" SortExpression="form_foreroom" />
                                    <asp:BoundField DataField="FloorAll" HeaderText="楼层" SortExpression="build_floor" />
                                    <asp:BoundField DataField="SeeHouseType" HeaderText="状态" SortExpression="StateID" />
                                    <asp:BoundField DataField="sum_price" HeaderText="总价(万)" SortExpression="sum_price" />
                                    <asp:BoundField DataField="Prices" HeaderText="单价(元)" SortExpression="sum_price" />
                                </Columns>
                            </asp:GridView>
                            <asp:ObjectDataSource ID="ods" TypeName="HouseMIS.EntityUtils.H_houseinfor" runat="server" EnablePaging="True" SelectCountMethod="FindCount" SelectMethod="FindAll" SortParameterName="orderClause" EnableViewState="false">
                                <SelectParameters>
                                    <asp:Parameter Name="whereClause" Type="String" />
                                    <asp:Parameter Name="orderClause" Type="String" DefaultValue="exe_date desc" />
                                    <asp:Parameter Name="selects" Type="String" />
                                    <asp:Parameter Name="startRowIndex" Type="Int32" />
                                    <asp:Parameter Name="maximumRows" Type="Int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                            <TCL:GridViewExtender ID="gvExt" runat="server" LayoutH="115" RowTarget="HouseID" RowRel="HouseID">
                            </TCL:GridViewExtender>
                        </div>
                    </td>
                </tr>
            </table>
        </form>
    </div>
</body>
