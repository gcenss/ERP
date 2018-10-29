<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HouseZBCheckList.aspx.cs" Inherits="HouseMIS.Web.House.HouseZBCheckList" %>

<script type="text/javascript">
    function OpenHouseEdit_Sale(houseId, houseNo) {
        var url = 'House/HouseForm.aspx?NavTabId=<%=NavTabId %>&doAjax=true&HouseID=' + houseId + '&EditType=Edit';
        var options = { width: 660, height: 720, mixable: false }
        $.pdialog.open(url, "housewiew_Check_Sale_" + houseId, houseNo + "-修改出售房源", options);
    }

    function OpenHouseEdit_Rent(houseId, houseNo) {
        var url = 'House/HouseRentForm.aspx?NavTabId=<%=NavTabId %>&doAjax=true&HouseID=' + houseId + '&EditType=Edit';
        var options = { width: 660, height: 720, mixable: false }
        $.pdialog.open(url, "housewiew_Check_Rent_" + houseId, houseNo + "-修改出租房源", options);
    }

    function OpenZBCheck(ID) {
        var url = 'House/HouseZBCheckEdit.aspx?NavTabId=<%=NavTabId %>&doAjax=true&ID=' + ID + '&EditType=Edit';
        var options = { width: 850, height: 550, mixable: false, mask: false }
        $.pdialog.open(url, "HouseZBCheckEdit" + ID, "开盘录音", options);
    }
</script>

<body>
    <form name="pagerForm" id="pagerForm" runat="server" action="House/HouseZBCheckList.aspx" onsubmit="return navTabSearch(this);">
        <div class="pageHeader">
            <div class="searchBar">
                <table class="searchContent">
                    <tr>
                        <td class="dateRange">房源编号：
                            <asp:TextBox runat="server" ID="myffrmshi_id" Width="80px"></asp:TextBox>
                        </td>
                        <td class="dateRange">申请人门店：
                            <asp:DropDownList ID="myffrmOrg_ZB" CssClass="myffrmOrg_ZB" runat="server" AppendDataBoundItems="true"
                                Style="width: 120px;">
                            </asp:DropDownList>
                            <script type="text/javascript">$("#myffrmOrg_ZB").combobox({ size: 120 }); </script>
                        </td>
                        <td class="dateRange">申请人：
                            <asp:TextBox runat="server" ID="myffrmEmployee" Width="80px"></asp:TextBox>
                        </td>
                        <td class="dateRange">申请日期：
                            <asp:TextBox runat="server" ID="myffrmCreateTime" CssClass="date" Width="80px"></asp:TextBox>
                        </td>
                        <td>房源状态：
                            <asp:DropDownList ID="myffrmHouseState" runat="server">
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
                <table class="searchContent">
                    <tr>
                        <td class="dateRange">审核人：
                            <asp:TextBox runat="server" ID="myffrmAuditEmp" Width="80px"></asp:TextBox>
                        </td>
                        <td class="dateRange">审核日期：
                            <asp:TextBox runat="server" ID="myffrmAuditTime" class="date" Width="80px"></asp:TextBox>
                        </td>
                        <td>是否审核：
                            <asp:DropDownList ID="myffrmddlAudit" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td>开盘录音：
                            <asp:DropDownList ID="myffrmPhoneID" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td>开盘凭证：
                            <asp:DropDownList ID="myffrmpicUrl" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td>委托类型：
                            <asp:DropDownList ID="myffrmEntrustTypeID" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td>是否删除：
                            <asp:DropDownList ID="myffrmIsDel" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
            <div class='panelBar'>
                <ul class='toolBar'>
                    <li id="houseCheckEdit" runat="server">
                        <a class="edit" href="house/HouseZBCheckEdit.aspx?NavTabId=<%=NavTabId %>&ID={ID}&EditType=Edit&doAjax=true"
                            width="850" height="550" target="dialog" mask="false" rel="<%=NavTabId %>"><span>开盘录音</span></a>
                    </li>
                    <li class='line'>line</li>
                    <%=toolStr() %>
                </ul>
            </div>
        </div>
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="ods" AllowSorting="True" AllowPaging="True" CssClass="table" PageSize="20" CellPadding="0" GridLines="None" EnableViewState="False">
            <Columns>
                <asp:TemplateField HeaderText="选择" ItemStyle-Width="20px">
                    <HeaderTemplate>
                        <input type="checkbox" group="ids" class="checkboxCtrl" title="全部选择/取消选择">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <input name="ids" type="checkbox" value="<%#Eval("ID")%>" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="房源编号">
                    <ItemTemplate>
                        <a href="<%#Eval("HouseType").ToString()=="售房"?"javascript:OpenHouseEdit_Sale('"+Eval("HouseID")+"','"+Eval("shi_id")+"')":"javascript:OpenHouseEdit_Rent('"+Eval("HouseID")+"','"+Eval("shi_id")+"')"%>"><%#Eval("shi_id")%></a>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" CssClass="key" Width="120px" />
                </asp:TemplateField>
                <asp:BoundField DataField="HouseType" HeaderText="房源类别">
                    <ItemStyle Width="50px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="SeeHouseType" HeaderText="房源状态">
                    <ItemStyle Width="60px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="EntrustTypeName" HeaderText="委托类型">
                    <ItemStyle Width="60px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="EmployeeName" HeaderText="申请人">
                    <ItemStyle Width="50px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="exe_Date" HeaderText="申请时间" SortExpression="exe_Date">
                    <ItemStyle Width="110px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="state_ZBCheckName" HeaderText="审核情况">
                    <ItemStyle Width="60px"></ItemStyle>
                </asp:BoundField>
                <asp:TemplateField HeaderText="开盘录音" ItemStyle-Width="230px" ItemStyle-Height="30px">
                    <ItemTemplate>
                        <%#Eval("phoneID")==null?"无":"<audio src="+Eval("recUrl")+" preload=\"none\" controls=\"controls\" style=\"width:250px;height:25px\"></audio>" %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="开盘凭证" ItemStyle-Width="60px" ItemStyle-Height="30px">
                    <ItemTemplate>
                        <%#Eval("picUrl")==null?"无":"有" %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="录音时长">
                    <ItemStyle Width="170px" />
                    <ItemTemplate>
                        <%#Eval("startTime")!=DBNull.Value && Convert.ToInt32(Eval("startTime"))>0?"【"+Eval("startTime")+"】秒开始，总长【"+Eval("realSecond")+"】秒":"" %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="rejectNum" HeaderText="驳回次数">
                    <ItemStyle Width="50px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="AuditEmployeeName" HeaderText="审核人">
                    <ItemStyle Width="60px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="audit_Date" HeaderText="审核时间" SortExpression="audit_Date" ItemStyle-Width="120">
                    <ItemStyle Width="110px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="remark" HeaderText="审核内容" ItemStyle-Width="100px"></asp:BoundField>
                <asp:TemplateField HeaderText="是否删除" ItemStyle-Width="60px" ItemStyle-Height="30px">
                    <ItemTemplate>
                        <%#Eval("isDel").ToString()=="False"?"否":"是" %>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                没有符合条件的数据！
            </EmptyDataTemplate>
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

        <TCL:GridViewExtender ID="gvExt" runat="server" LayoutH="140" RowTarget="ID" RowRel="ID">
        </TCL:GridViewExtender>
    </form>
</body>
