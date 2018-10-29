<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RecordList.aspx.cs" Inherits="HouseMIS.Web.House.RecordList" %>

<script>
    function ChangeOpenHouseEdit(houseId, houseNo, atype) {
        // 所有参数都是可选项。
        var url = 'House/HouseForm.aspx?NavTabId=<%=NavTabId %>&doAjax=true&HouseID=' + houseId + '&EditType=Edit';
        var options = { width: 660, height: 650, mixable: false }
        $.pdialog.open(url, "housewiew2_" + houseId, houseNo + "-修改房源", options);
    }
</script>
<form rel="pagerForm" runat="server" name="pagerForm" id="pagerForm" class="RDpagerForm"
onsubmit="return navTabSearch(this);" action="House/RecordList.aspx" method="post">
<div style="position: relative">
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
                            小区：</label>
                        <asp:dropdownlist runat="server" id="myffrmAreaID" width="115px"></asp:dropdownlist>
                    </td>
                    <td class="dateRange">
                        <label>
                            申请人工号：</label>
                        <asp:textbox runat="server" id="myffrmname" width="80px"></asp:textbox>
                    </td>
                    <td class="dateRange">
                        <label>
                            申请人部门</label>
                        <asp:dropdownlist runat="server" id="myffrmRecordOrgID" width="115px"></asp:dropdownlist>
                        <script type="text/javascript">
                            $("#myffrmRecordOrgID").combobox({ size: 98 });
                        </script>
                    </td>
                    <td class="dateRange">
                        <label>
                            屏蔽类型：</label>
                        <asp:dropdownlist runat="server" id="ffrmaType" width="115px"><asp:ListItem Value="">选择屏蔽类型</asp:ListItem><asp:ListItem Value="1">普通屏蔽</asp:ListItem>
             <asp:ListItem Value="2">永久屏蔽</asp:ListItem></asp:dropdownlist>
                    </td>
                    <td class="dateRange">
                        是否审核：<asp:dropdownlist runat="server" id="ffrmIsCheck"><asp:ListItem Value="">选择</asp:ListItem><asp:ListItem Value="false">否</asp:ListItem>
             <asp:ListItem Value="true">是</asp:ListItem></asp:dropdownlist></asp:Dropdownlist>
                    </td>
                </tr>
            </table>
            <table class="searchContent">
                <tr>
                    <td class="dateRange">
                        <label>
                            录音人工号：</label>
                        <asp:textbox runat="server" id="myffrmEmid" width="80px"></asp:textbox>
                    </td>
                    <td class="dateRange">
                        <label>
                            申请日期：</label>
                        <asp:textbox runat="server" id="myffrmOutDate1" class="date" yearstart="-80" yearend="5"
                            width="80px" readonly="true"></asp:textbox>
                    </td>
                    <td class="dateRange">
                        <label>
                            -
                        </label>
                        <asp:textbox runat="server" id="myffrmOutDate2" class="date" yearstart="-80" yearend="5"
                            width="80px" readonly="true"></asp:textbox>
                    </td>
                    <td class="dateRange">
                        <label>
                            审核日期：</label>
                        <asp:textbox runat="server" id="myffrmCheckDate1" class="date" yearstart="-80" yearend="5"
                            width="80px" readonly="true"></asp:textbox>
                    </td>
                    <td class="dateRange">
                        <label>
                            -
                        </label>
                        <asp:textbox runat="server" id="myffrmCheckDate2" class="date" yearstart="-80" yearend="5"
                            width="80px" readonly="true"></asp:textbox>
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
            <%=EmpStr.ToString() %>
            <%--  <li><a class="delete" href="House/RecordList.aspx?NavTabId=<%=NavTabId %>&doAjax=true&doType=delall&RecordCloseID={RecordCloseID}"
                target="ajaxTodo" title="删除吗?"><span>删除</span></a></li>--%>
            <li><a class="add" href="House/RecordList.aspx?NavTabId=<%=NavTabId %>&doAjax=true&doType=CheckPass&RecordCloseID={RecordCloseID}"
                target="ajaxTodo"><span>审核通过</span></a></li>
            <li><a class="add" href="House/RecordList.aspx?NavTabId=<%=NavTabId %>&doAjax=true&doType=CheckRefuse&RecordCloseID={RecordCloseID}"
                target="ajaxTodo"><span>审核不通过</span></a></li>
        </ul>
    </div>
    <input type="hidden" name="status" value="status" />
    <input type="hidden" name="keywords" value="keywords" />
    <input type="hidden" name="numPerPage" value="20" />
    <input type="hidden" name="orderField" value="" />
    <input type="hidden" name="orderDirection" value="" />
    <asp:gridview id="gv" runat="server" autogeneratecolumns="False" datakeynames="RecordCloseID"
        datasourceid="ods" allowpaging="True" cssclass="table" pagesize="20" cellpadding="0"
        gridlines="None" enablemodelvalidation="True" enableviewstate="false">
            <Columns>
                <asp:TemplateField HeaderText="选择">
                    <HeaderTemplate>
                    <input type="checkbox" group="ids" class="checkboxCtrl" title="全部选择/取消选择">
                    </HeaderTemplate>
                    <ItemTemplate>
                          <%#Convert.ToString(Eval("IsCheckString")) == "未审核" ? "<input name=\"ids\" type=\"checkbox\" value='" + Eval("RecordCloseID").ToString() + "'/>" : "<input name=\"\" type=\"checkbox\" disabled=\"disabled\" aria-readonly=\"true\"/>"%>                 
                       <%-- <input name="ids" type="checkbox" value="<%#Eval("RecordCloseID")%>" />--%>
                    </ItemTemplate>
                </asp:TemplateField>
               
                <asp:TemplateField HeaderText="房源编号">
                    <ItemTemplate>
                        <a href="javascript:ChangeOpenHouseEdit('<%#Eval("HouseID") %>','<%#Eval("Shi_id") %>','1')">
                            
                                <%#Eval("shi_id")%></a>
                    </ItemTemplate>
                    <HeaderStyle Width="80px" />
                    <ItemStyle Width="80px" />
                </asp:TemplateField>
                <asp:BoundField DataField="em_name" HeaderText="申请人" SortExpression="EmployeeID" >
                    <ItemStyle Width="100" />
                </asp:BoundField>
                 <asp:BoundField DataField="AtypeName" HeaderText="类型" SortExpression="aType" >
                    <ItemStyle Width="80" />
                </asp:BoundField>
                <asp:BoundField DataField="IsCheckString" HeaderText="是否通过审核" SortExpression="IsCheck" >
                    <ItemStyle Width="80" />
                </asp:BoundField>
                <asp:BoundField DataField="ecName" HeaderText="审核人" SortExpression="CheckEmployeeID" >
                    <ItemStyle Width="70" />
                </asp:BoundField>
                <asp:BoundField DataField="CheckDate" HeaderText="审核日期" SortExpression="CheckDate" >
                    <ItemStyle Width="150" />
                </asp:BoundField>
                <asp:BoundField DataField="phoneID" HeaderText="录音编号">
                    <ItemStyle Width="80" />
                </asp:BoundField>
                <asp:BoundField DataField="EmployeeName" HeaderText="录音人">
                    <ItemStyle Width="100" />
                </asp:BoundField>
                <asp:BoundField DataField="exe_date" HeaderText="申请日期" SortExpression="exe_date"  >
                    <ItemStyle Width="150" />
                </asp:BoundField>
            </Columns> 
    </asp:gridview>
    <asp:objectdatasource id="ods" typename="HouseMIS.EntityUtils.h_RecordClose" runat="server"
        enablepaging="True" selectcountmethod="FindCount" selectmethod="FindAll" sortparametername="orderClause"
        enableviewstate="false">
        <SelectParameters>      
            <asp:Parameter Name="whereClause" Type="String" />
            <asp:Parameter Name="orderClause" Type="String" DefaultValue=" exe_date desc" />
            <asp:Parameter Name="selects" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:objectdatasource>
    <TCL:GridViewExtender ID="gvExt" runat="server" LayoutH="155" RowTarget="RecordCloseID"
        RowRel="RecordCloseID">
    </TCL:GridViewExtender>
</div>
</form>
