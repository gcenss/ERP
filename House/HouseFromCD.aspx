<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HouseFromCD.aspx.cs" Inherits="HouseMIS.Web.House.HouseFromCD" %>

<body>
    <form id="HouseFromCD" runat="server">
        <div style="height: 560px; overflow: auto;">
            <div class="tabs" currentindex="0" eventtype="click">
                <div class="tabsHeader">
                    <div class="tabsHeaderContent">
                        <ul>
                            <li><a href="javascript:;"><span>调电记录</span></a></li>
                            <li><a href="javascript:;"><span>调门牌记录</span></a></li>
                            <li><a href="javascript:;"><span>拨打记录</span></a></li>
                        </ul>
                    </div>
                </div>
                <div class="tabsContent" layouth="145">
                    <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" DataKeyNames="LSH"
                        CssClass="table" CellPadding="0" GridLines="None" EnableModelValidation="True"
                        EnableViewState="False">
                        <Columns>
                            <asp:BoundField DataField="EmployeeName" HeaderText="查看人" />
                            <asp:BoundField DataField="exe_Date" HeaderText="日期" />
                        </Columns>
                    </asp:GridView>
                    <asp:GridView ID="gv_pai" runat="server" AutoGenerateColumns="False" DataKeyNames="LSH"
                        CssClass="table" CellPadding="0" GridLines="None" EnableModelValidation="True"
                        EnableViewState="False">
                        <Columns>
                            <asp:BoundField DataField="EmployeeName" HeaderText="查看人" />
                            <asp:BoundField DataField="exe_Date" HeaderText="日期" />
                        </Columns>
                    </asp:GridView>
                    <asp:GridView ID="gv1" runat="server" AutoGenerateColumns="False" CssClass="table"
                        CellPadding="0" GridLines="None" EnableModelValidation="True" EnableViewState="False">
                        <Columns>
                            <asp:BoundField DataField="c" HeaderText="查看人" />
                            <asp:BoundField DataField="a" HeaderText="日期" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </form>
</body>
