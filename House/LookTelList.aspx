<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LookTelList.aspx.cs" Inherits="HouseMIS.Web.House.LookTelList" %>

<style>
    .searchContent td {
        padding-right: 10px;
        text-align: left;
    }

    .searchContent label {
        width: auto;
    }
</style>

<script type="text/javascript">
    //打开房源编辑页面
    function OpenHouseEdit_SF(houseId, houseNo, atype) {
        // 所有参数都是可选项。
        var url = 'House/HouseForm.aspx?NavTabId=<%=NavTabId %>&doAjax=true&HouseID=' + houseId + '&EditType=Edit';
        var options = { width: 660, height: 668, mixable: false }
        $.pdialog.open(url, "housewiew2_" + houseId, houseNo + "-修改房源", options);
    }

    $(document).ready(function () {
        $("#ckyc").change(function () {
            if ($("#ckyc").val() == 0) {
                $("#ddlInfotype").val(1)
            }
            else {
                $("#ddlInfotype").val(0)
            }
        })
    })
</script>

<body>
    <form name="pagerForm" id="pagerForm" runat="server" rel="pagerForm" action="House/LookTelList.aspx" onsubmit="return navTabSearch(this);" method="post">
        <div class="pageHeader">
            <div class="searchBar">
                <table class="searchContent">
                    <tr>
                        <td>
                            <label>查看日期开始：</label></td>
                        <td>
                            <asp:TextBox runat="server" Width="70px" ID="myffrmexe_Date" class="date" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td>
                            <label>查看日期结束：</label></td>
                        <td>
                            <asp:TextBox runat="server" Width="70px" ID="myffrmexe_Date_end" class="date" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td>
                            <label>工　　号：</label></td>
                        <td>
                            <asp:TextBox runat="server" Width="50px" ID="myffrmem_id"></asp:TextBox>
                        </td>
                        <td>
                            <label>查看人：</label></td>
                        <td>
                            <asp:TextBox runat="server" Width="85px" ID="myffrmEmployeeID"></asp:TextBox>
                        </td>
                        <td>
                            <div class="buttonActive">
                                <div class="buttonContent">
                                    <button type="submit" onclick="return formFind()">检索</button>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label title="类型">信息类型：</label></td>
                        <td>
                            <asp:DropDownList ID="ddlInfotype" CssClass="ddlInfotype" runat="server">
                                <asp:ListItem Value="0">调电</asp:ListItem>
                                <asp:ListItem Value="1">调门牌号</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <label title="类型">查看门牌是否异常：</label></td>
                        <td>
                            <asp:DropDownList ID="ckyc" CssClass="ckyc" Width="100%" runat="server">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem Value="0">是</asp:ListItem>
                            </asp:DropDownList>
                        </td>

                        <td>
                            <label>状　态：</label></td>
                        <td>
                            <asp:DropDownList ID="ddlStateID" Width="100%" runat="server"></asp:DropDownList>
                        </td>
                        <td>
                            <label>查看人门店：</label></td>
                        <td>
                            <asp:DropDownList ID="mysfrmOrgID_Emp" class="mysfrmOrgID_Emp" runat="server" AppendDataBoundItems="true">
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                            <script type="text/javascript">
                                $("#mysfrmOrgID_Emp").combobox({ size: 100 });
                            </script>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>房源门店：</label></td>
                        <td>
                            <asp:DropDownList ID="mysfrmOrgID" class="mysfrmOrgID" runat="server" AppendDataBoundItems="true">
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                            <script type="text/javascript">
                                $("#mysfrmOrgID").combobox({ size: 100 });
                            </script>
                        </td>
                        <td>
                            <label>房源编号：</label></td>
                        <td>
                            <asp:TextBox runat="server" Width="100px" ID="myffrmshi_id"></asp:TextBox>
                        </td>
                        <td>
                            <label title="类型">房源类型：</label></td>
                        <td>
                            <asp:DropDownList ID="ddlaType" CssClass="ddlaType" Width="100%" runat="server">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem Value="0">出售</asp:ListItem>
                                <asp:ListItem Value="1">出租</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="pageContent" style="padding: 0px 2px 2px 2px">
            <%--<div class="panelBar">
                <ul class="toolBar">
                    <li><a id="Tab_Print<%=NavTabId %>" class="icon" rel="ids" title="确定要打印这些记录吗?"><span>打印表格</span></a></li>
                </ul>
            </div>--%>

            <input type="hidden" name="NavTabId" value="<%=NavTabId %>" />
            <input type="hidden" name="status" value="status" />
            <input type="hidden" name="keywords" value="keywords" />
            <input type="hidden" name="numPerPage" value="20" />
            <input type="hidden" name="orderField" value="" />
            <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" DataKeyNames="Lsh"
                DataSourceID="ods" AllowPaging="True" CssClass="table" PageSize="20" CellPadding="0"
                GridLines="None" EnableViewState="False">
                <Columns>
                    <asp:TemplateField HeaderText="选择">
                        <HeaderTemplate>
                            <input type="checkbox" name="empl_cb" group="ids" class="checkboxCtrl" title="全部选择/取消选择">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <input name="ids" type="checkbox" value="<%#Eval("Lsh")%>" />
                        </ItemTemplate>
                        <ItemStyle Width="40" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="Lsh" HeaderText="序号" SortExpression="Lsh" InsertVisible="False"
                        ReadOnly="True">
                        <ItemStyle Width="70" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="key" />
                    </asp:BoundField>
                    <asp:BoundField DataField="EmployeeName" HeaderText="查看人" SortExpression="EmployeeID">
                        <ItemStyle Width="150" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="房源编号">
                        <ItemTemplate>
                            <a href="javascript:OpenHouseEdit_SF('<%#Eval("HouseID") %>','<%#Eval("Shi_Id") %>','1')"><span><%#Eval("Shi_Id")%></span></a>
                        </ItemTemplate>
                        <HeaderStyle Width="100px" />
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="HouseDicName" HeaderText="小区名称">
                        <ItemStyle Width="160" />
                    </asp:BoundField>
                    <asp:BoundField DataField="HouseDicAddress" HeaderText="小区地址">
                        <ItemStyle Width="160" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="类别">
                        <ItemTemplate>
                            <span><%#Eval("AtypeName")%></span>
                        </ItemTemplate>
                        <HeaderStyle Width="40px" />
                        <ItemStyle Width="40px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="状态">
                        <ItemTemplate>
                            <span><%#Eval("SeeHouseType") %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="exe_Date" HeaderText="查电(门牌)日期" SortExpression="exe_Date">
                        <ItemStyle Width="160" />
                    </asp:BoundField>
                </Columns>
                <EmptyDataTemplate>
                    没有符合条件的数据！
                </EmptyDataTemplate>
            </asp:GridView>
            <asp:ObjectDataSource ID="ods" TypeName="HouseMIS.EntityUtils.h_SeeTelLog" runat="server"
                EnablePaging="True" SelectCountMethod="FindCount" SelectMethod="FindAll" SortParameterName="orderClause"
                EnableViewState="false">
                <SelectParameters>
                    <asp:Parameter Name="whereClause" Type="String" />
                    <asp:Parameter Name="orderClause" Type="String" DefaultValue="exe_date desc" />
                    <asp:Parameter Name="selects" Type="String" />
                    <asp:Parameter Name="startRowIndex" Type="Int32" />
                    <asp:Parameter Name="maximumRows" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <TCL:GridViewExtender ID="gvExt" runat="server" LayoutH="150" RowTarget="Lsh"
                RowRel="Lsh">
            </TCL:GridViewExtender>
        </div>
    </form>
</body>
