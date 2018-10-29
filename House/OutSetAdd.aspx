<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OutSetAdd.aspx.cs" Inherits="HouseMIS.Web.House.OutSetAdd" %>


<form  name="pagerForm" id="pagerForm" runat="server" onsubmit="return dialogSearch(this);" action="House/OutSetAdd.aspx" method="post">
    <a id="goHouseOutSet" href="House/HouseOutSet.aspx?NavTabId=<%=NavTabId %>&doAjax=true" runat="server" target="dialog" rel="50_menu2001" title="添加到走漏名单"></a>
    <input type="hidden" runat="server" id="HouseOurtHouseID" name="HouseOurtHouseID" />
    <div class="panelBar">
        <ul>
            <li><span>工号</span><asp:textbox runat="server" id="ffrmem_id" width="80px"></asp:textbox></li>
            <li>
                <button type="submit" onclick="return formFind()">检索</button></li>
        </ul>
    </div>
    <div>
        <input type="hidden" name="doAjax" value="true" />
        <input type="hidden" name="NavTabId" value="<%=NavTabId %>" />
        <input type="hidden" name="status" value="status" />
        <input type="hidden" name="keywords" value="keywords" />
        <input type="hidden" name="numPerPage" value="20" />       
        <input type="hidden" name="orderField" value="" />
	    <input type="hidden" name="orderDirection" value="" />
        <asp:gridview id="gv" runat="server" autogeneratecolumns="False" datakeynames="EmployeeID"
                    datasourceid="ods" AllowPaging="True" cssclass="table"  cellpadding="0" PageSize="20"
                    gridlines="None" enablemodelvalidation="True" enableviewstate="false">
                    <Columns>
                        <asp:TemplateField HeaderText="选择">
                            <HeaderTemplate>
                            <input type="checkbox" group="ids" class="checkboxCtrl" title="全部选择/取消选择">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <input name="ids" type="checkbox" value="<%#Eval("EmployeeID")%>" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="em_id" HeaderText="工号">
                            <ItemStyle Width="90px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="em_name" HeaderText="姓名">
                            <ItemStyle Width="100px" />
                        </asp:BoundField>
                    </Columns>
                </asp:gridview>
                <asp:objectdatasource id="ods" typename="HouseMIS.EntityUtils.Employee" runat="server"
                        enablepaging="True" selectcountmethod="FindCount" selectmethod="FindAll" sortparametername="orderClause"
                        enableviewstate="false">
                    <SelectParameters>
                        <asp:Parameter Name="whereClause" Type="String" />
                        <asp:Parameter Name="orderClause" Type="String" />
                        <asp:Parameter Name="selects" Type="String" />
                        <asp:Parameter Name="startRowIndex" Type="Int32" DefaultValue="0" />
                        <asp:Parameter Name="maximumRows" Type="Int32" DefaultValue="0" />
                    </SelectParameters>
                </asp:objectdatasource>
                <tcl:gridviewextender id="gvExt" GridViewID="gv" runat="server" PageNumberNav="false" layouth="110" TargetType="dialog" rowtarget="EmployeeID" rowrel="EmployeeID">
                </tcl:gridviewextender>
    </div>
    <div class="formBar">
            <ul>
			    <li><div class="buttonActive"><div class="buttonContent"><button type="submit">确定</button></div></div></li>
			    <li>
				    <div class="button"><div class="buttonContent"><button type="button" class="close">取消</button></div></div>
			    </li>
		    </ul>
        </div>
</form>