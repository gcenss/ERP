<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HouseUnitBack.aspx.cs" EnableViewStateMac="false"  Inherits="HouseMIS.Web.House.HouseUnitBack" %>


<form rel="pagerForm" runat="server" name="pagerForm" id="pagerForm" onsubmit="return navTabSearch(this);"
action="House/HouseUnitBack.aspx" method="post">
<div>
    <input type="hidden" name="NavTabId" value="<%=NavTabId %>" />
    <input type="hidden" name="status" value="status" />
    <input type="hidden" name="keywords" value="keywords" />
    <input type="hidden" name="numPerPage" value="20" />
    <input type="hidden" name="orderField" value="" />
    <input type="hidden" name="orderDirection" value="" />
    <asp:gridview id="gv" runat="server" autogeneratecolumns="False" datakeynames="UnitNam"
        datasourceid="ods" allowpaging="True" cssclass="table" pagesize="20" cellpadding="0"
        gridlines="None" enablemodelvalidation="True" enableviewstate="false">
            <Columns>
                <asp:BoundField DataField="UnitNam" HeaderText="单元号" SortExpression="UnitID" />
                <asp:TemplateField HeaderText="选择" >
                <ItemTemplate>
                    <a class="btnSelect" href="javascript:$.bringBack({frmbuild_unit:'<%#Eval("UnitNam")%>',UnitID:'<%#Eval("UnitID")%>'})" title="查找带回">选择</a>
                </ItemTemplate>
             </asp:TemplateField>    
            </Columns> 
    </asp:gridview>
    <asp:objectdatasource id="ods" runat="server"
        enablepaging="True" selectcountmethod="FindCount" selectmethod="FindAll" sortparametername="orderClause"
        enableviewstate="false">
        <SelectParameters>      
            <asp:Parameter Name="whereClause" Type="String" />
            <asp:Parameter Name="orderClause" Type="String" />
            <asp:Parameter Name="selects" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:objectdatasource>
    <tcl:gridviewextender id="gvExt" runat="server" layouth="100" rowtarget="UnitNam"
        rowrel="UnitNam">
    </tcl:gridviewextender>
</div>
</form>
