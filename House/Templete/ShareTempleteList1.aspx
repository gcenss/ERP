<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShareTempleteList1.aspx.cs" Inherits="HouseMIS.Web.House.Templete.ShareTempleteList1" %>

<form name="pagerForm" id="pagerForm" runat="server" rel="pagerForm" action="House/Templete/ShareTempleteList1.aspx"
onsubmit="return navTabSearch(this);">
<div class="pageHeader">
    <div class="searchBar">
        <table class="searchContent">
            <tr>
                <td>
                    模板类型
                </td>
                <td>
                    <asp:dropdownlist id="ffrmShareType" runat="server" height="20px" width="126px" cssclass="required">
                                <asp:ListItem Text="" Value=""></asp:ListItem>
                               <asp:ListItem Text="好文章" Value="好文章"></asp:ListItem>
                        <asp:ListItem Text="视频" Value="视频"></asp:ListItem>
                        <asp:ListItem Text="游戏" Value="游戏"></asp:ListItem>
                            </asp:dropdownlist>
                </td>
                <td>
                    标题
                </td>
                <td>
                    <asp:textbox id="ffrmTitle" runat="server" width="120px"></asp:textbox>
                </td>
                <td>
                    姓名
                </td>
                <td>
                    <asp:textbox id="Name" runat="server" width="120px"></asp:textbox>
                </td>
                <td>
                    添加日期
                </td>
                <td>
                    <asp:textbox id="start" runat="server" width="100px" cssclass="date"></asp:textbox>
                </td>
                <td>
                    -
                </td>
                <td>
                    <asp:textbox id="end" runat="server" width="100px" cssclass="date"></asp:textbox>
                </td>
                <td>
                    <div class="subBar" style="margin-left: 50px">
                        <ul>
                            <li>
                                <div class="buttonActive">
                                    <div class="buttonContent">
                                        <button type="submit" onclick="return formFind()" style="width: 90px;">
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
<div class="panelBar">
    <ul class="toolBar">
        <asp:literal id="ltlToolBar" runat="server"></asp:literal>
        <%--<li> <a class="add" href="OA/NewShow.aspx?NewClassID=223" target="navtab" rel="server_jbsxBox"><span>管理制度</span></a> </li>--%>
    </ul>
</div>
<asp:hiddenfield runat="server" id="Type" />
<input type="hidden" name="NavTabId" value="<%=NavTabId %>" />
<input type="hidden" name="status" value="status" />
<input type="hidden" name="keywords" value="keywords" />
<input type="hidden" name="numPerPage" value="15" />
<input type="hidden" name="orderField" value="" />
<asp:gridview id="gv" runat="server" autogeneratecolumns="False" datakeynames="id"
    datasourceid="ods" allowpaging="True" cssclass="table" pagesize="20" cellpadding="0"
    gridlines="None" enablemodelvalidation="True" enableviewstate="False">
            <Columns>
                <asp:BoundField DataField="ShareType" HeaderText="模板类型">
                    <ItemStyle Width="80" />
                </asp:BoundField>
                <asp:BoundField DataField="TypeName" HeaderText="类别">
                    <ItemStyle Width="60" />
                </asp:BoundField>
                <asp:BoundField DataField="Title" HeaderText="标题">
                    <ItemStyle Width="200" />
                </asp:BoundField>
                <asp:BoundField DataField="GetContent" HeaderText="内容">
                    <ItemStyle Width="300" />
                </asp:BoundField>
                <asp:BoundField DataField="ShareTotal" HeaderText="分享次数"  SortExpression="ShareTotal">
                    <ItemStyle Width="60" />
                </asp:BoundField>
                <asp:BoundField DataField="AddDate" HeaderText="添加时间" >
                    <ItemStyle Width="120" />
                </asp:BoundField>
                <asp:BoundField DataField="AutoName" HeaderText="作者">
                    <ItemStyle Width="80" />
                </asp:BoundField>
            </Columns>

        </asp:gridview>
<asp:objectdatasource id="ods" runat="server" enablepaging="True" selectcountmethod="FindCount"
    selectmethod="FindAll" sortparametername="orderClause" enableviewstate="false">
            <SelectParameters>
                <asp:Parameter Name="whereClause" Type="String" />
                <asp:Parameter Name="orderClause" Type="String" DefaultValue="AddDate desc" />
                <asp:Parameter Name="selects" Type="String"  />
                <asp:Parameter Name="startRowIndex" Type="Int32" />
                <asp:Parameter Name="maximumRows" Type="Int32" />
            </SelectParameters>
        </asp:objectdatasource>
<TCL:GridViewExtender ID="gvExt" runat="server" LayoutH="110" RowTarget="id" RowRel="id">
</TCL:GridViewExtender>
</form>

