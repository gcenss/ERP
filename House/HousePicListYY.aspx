<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HousePicListYY.aspx.cs" Inherits="HouseMIS.Web.House.HousePicListYY" %>

<script type="text/javascript">
    <%--$(function () {
        if(<%=pHasImage %>=="0"){
            $("#cHasImage").attr("checked", true);
            $("#myffrmHasImage").val("on");
        }
    })--%>

    function OpenHouseEdit_Follow_Sale(houseId, houseNo) {
        var url = 'House/HousePicYY.aspx?NavTabId=<%=NavTabId %>&doAjax=true&HouseID=' + houseId + '&EditType=Edit';
        var options = { width: 1060, height: 700, mixable: false }
        $.pdialog.open(url, "housewiew_Follow_Sale_" + houseId, houseNo + "出售房源照片", options);
    }

    function OpenHouseEdit_Follow_Rent(houseId, houseNo) {
        var url = 'House/HouseRentForm.aspx?NavTabId=<%=NavTabId %>&doAjax=true&HouseID=' + houseId + '&EditType=Edit';
        var options = { width: 660, height: 720, mixable: false }
        $.pdialog.open(url, "housewiew_Follow_Rent_" + houseId, houseNo + "-修改出租房源", options);
    }

    function OpenArea(name1, name2) {
        var url = "House/AreaV.aspx?tn1=" + name1 + "&tn2=" + name2 + "&tn3=AreaVillage";
        var options = { width: 580, height: 380, mixable: false }
        $.pdialog.open(url, "AreaVillage", "区域商圈小区选择", options);
    }
</script>
<body>
    <form name="pagerForm" id="pagerForm" runat="server" method="post"
        action="House/HousePicListYY.aspx" 
        onsubmit="return navTabSearch(this);">
        <div class="pageHeader">
            <div class="searchBar">
                <table class="searchContent">
                    <tr>
                        <td class="dateRange">
                            <label>房源编号：</label>
                            <asp:TextBox runat="server" ID="myffrmHouseID" Width="80px"></asp:TextBox>
                        </td>
                        <td class="dateRange">
                            <label>预约人：</label>
                            <asp:TextBox runat="server" ID="myffrmemployee" Width="80px"></asp:TextBox>
                        </td>
                        <td class="dateRange">
                            <label>摄影师：</label>
                            <asp:TextBox runat="server" ID="myffrmphotoer" Width="80px"></asp:TextBox>
                        </td>
                        <td class="dateRange">
                            <label>完成日期：</label>
                            <asp:TextBox runat="server" ID="myffrmFinishtimestart" Width="120px" CssClass="date"></asp:TextBox>—
                            <asp:TextBox runat="server" ID="myffrmFinishtimeend" Width="120px" CssClass="date"></asp:TextBox>
                        </td>
                        <td class="dateRange">
                            <label>栋号：</label>
                            <asp:TextBox ID="myffrmbuild_id" runat="server" Width="30px"></asp:TextBox>
                            房号：
                            <asp:TextBox ID="myffrmbuild_room" runat="server" Width="30px"></asp:TextBox>
                        </td>
                        <td class="dateRange">
                           <label>是否已成交：</label>
                            <asp:DropDownList ID="myffrmcj" runat="server">
                                <asp:ListItem Value="">全部</asp:ListItem>
                                <asp:ListItem Value="0">是</asp:ListItem>
                                <asp:ListItem Value="1">否</asp:ListItem>
                            </asp:DropDownList>
                        </td>

                        
                    </tr>
                    <tr>
                        <td>
                            <label>是否完成：</label>
                            <asp:DropDownList ID="myffrmwc" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <label>是否领取：</label>
                            <asp:DropDownList ID="myffrmlq" runat="server">
                            </asp:DropDownList>
                        </td>
                         <td>
                            <label>是否审核：</label>
                            <asp:DropDownList ID="myffrmsh" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <label>照片：</label>
                            <asp:DropDownList ID="myffrmHasImage" runat="server">
                                <asp:ListItem Value="">全部</asp:ListItem>
                                <asp:ListItem Value="0">有</asp:ListItem>
                                <asp:ListItem Value="1">无</asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;&nbsp;&nbsp;&nbsp;
                           信息部审核：
                            <asp:DropDownList ID="myffrmxinxibush" runat="server">
                                <asp:ListItem Value="">全部</asp:ListItem>
                                <asp:ListItem Value="0">通过</asp:ListItem>
                                <asp:ListItem Value="1">未通过</asp:ListItem>
                            </asp:DropDownList>
                        </td>

                        <td class="dateRange">
                                <label title="区域">区域：</label>
                                <asp:TextBox ID="myffrmtxtArea" runat="server" Width="100px" ReadOnly="true" onclick="OpenArea('myffrmtxtArea','myffrmtxtArea2')"></asp:TextBox>
                                <asp:TextBox ID="myffrmtxtArea2" runat="server" Style="display: none;"></asp:TextBox>
                                <asp:DropDownList ID="mysfrmAreaID" runat="server" Style="width: 73px; display: none;"></asp:DropDownList>
                <%--             <input type="checkbox" id="cHasImage" value="on" onclick="SetCheckValue(this, 'on', 'myffrmHasImage')" />无照片
                                <input type="hidden" id="myffrmHasImage" name="myffrmHasImage" value="" />--%>
                         </td>
                    
                         <td class="dateRange">
                                <label title="委托">委托：</label>
                                <asp:DropDownList ID="myffrmEntrustTypeID" runat="server" CssClass="ffrmEntrustTypeID"></asp:DropDownList>
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
            <div class='panelBar'>
                <ul class='toolBar'>
                    <li class='line'>line</li>
                    <%=z_bottom%>
                    <%=z_del%>
                    <%=ExportToolBar %>
                </ul>
            </div>
        </div>

        <input type="hidden" name="NavTabId" value="<%=NavTabId %>" />
        <input type="hidden" name="status" value="status" />
        <input type="hidden" name="keywords" value="keywords" />
        <input type="hidden" name="numPerPage" value="20" />
        <input type="hidden" name="orderField" value="" />
        <input type="hidden" name="orderDirection" value="" />

        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="ID" DataSourceID="ods"
            AllowPaging="True" CssClass="table" PageSize="20"
            CellPadding="0" GridLines="None" EnableViewState="False">
            <Columns>
                <asp:TemplateField HeaderText="选择">
                    <HeaderTemplate>
                        <input type="checkbox" group="ids" class="checkboxCtrl" title="全部选择/取消选择">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <input name="ids" type="checkbox" value="<%#Eval("ID")%>" />
                    </ItemTemplate>
                    <ItemStyle Width="35" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="房源编号">
                    <ItemTemplate>
                        <a href="<%#"javascript:OpenHouseEdit_Follow_Sale('"+Eval("HouseID")+"','"+Eval("shi_id")+"')"%>"><%#Eval("shi_id")%></a>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="key" Width="120" />
                </asp:TemplateField>
                <asp:BoundField DataField="AreaName" HeaderText="区域">
                    <ItemStyle Width="100" />
                </asp:BoundField>
                <asp:BoundField DataField="SanjakName" HeaderText="商圈">
                    <ItemStyle Width="80" />
                </asp:BoundField>
                <asp:BoundField DataField="EmployeeName" HeaderText="预约人" ItemStyle-Width="60">
                    <ItemStyle Width="80px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="OrgName" HeaderText="预约人部门" ItemStyle-Width="60">
                    <ItemStyle Width="80px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="exe_date" HeaderText="预约时间" SortExpression="exe_date" ItemStyle-Width="80" DataFormatString="{0:yyyy-MM-dd}">
                    <ItemStyle Width="80px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="photoerName" HeaderText="摄影师" ItemStyle-Width="60">
                    <ItemStyle Width="80px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="Finishtime" HeaderText="完成时间" SortExpression="Finishtime" ItemStyle-Width="80" DataFormatString="{0:yyyy-MM-dd}">
                    <ItemStyle Width="80px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="IsBH" HeaderText="是否驳回" ItemStyle-Width="60">
                    <ItemStyle Width="80px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="IsSH" HeaderText="是否审核" ItemStyle-Width="60">
                    <ItemStyle Width="80px"></ItemStyle>
                </asp:BoundField>
                 <asp:BoundField DataField="Remark" HeaderText="备注" ItemStyle-Width="60">
                    <ItemStyle Width="150px"></ItemStyle>
                </asp:BoundField>
            </Columns>
            <EmptyDataTemplate>
                没有符合条件的数据！
            </EmptyDataTemplate>
        </asp:GridView>

        <asp:ObjectDataSource ID="ods" runat="server" EnablePaging="True" SelectCountMethod="FindCount" SelectMethod="FindAll" SortParameterName="orderClause" EnableViewState="false">
            <SelectParameters>
                <asp:Parameter Name="whereClause" Type="String" />
                <asp:Parameter Name="orderClause" Type="String" DefaultValue=" exe_Date desc" />
                <asp:Parameter Name="selects" Type="String" />
                <asp:Parameter Name="startRowIndex" Type="Int32" />
                <asp:Parameter Name="maximumRows" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>

        <TCL:GridViewExtender ID="gvExt" runat="server" LayoutH="140" RowTarget="ID" RowRel="ID">
        </TCL:GridViewExtender>
    </form>
</body>
