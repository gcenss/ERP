<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShareHouseList.aspx.cs" Inherits="HouseMIS.Web.House.Templete.ShareHouseList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form name="pagerForm" id="pagerForm" runat="server" rel="pagerForm" action="House/Templete/ShareHouseList.aspx"
    onsubmit="return navTabSearch(this);">
    <div class="pageHeader">
         <div class="searchBar">
             <table class="searchContent">
                 <tr>
                     <td>
                         门 店 &nbsp
                     </td>
                     <td id="Td1" width="150px">
                         <asp:DropDownList ID="ShareHouseList_OrgID" CssClass="ProMangerList_OrgID" runat="server"
                             AppendDataBoundItems="true">
                             <asp:ListItem></asp:ListItem>
                         </asp:DropDownList>
                         <script type="text/javascript">
                             $("#ShareHouseList_OrgID").combobox({ size: 115 });
                         </script>
                     </td>
                     <td>
                         姓名
                     </td>
                     <td>
                         <asp:TextBox ID="fsemployeeid" runat="server" Width="120px"></asp:TextBox>
                     </td>
                     <td>
                         标题
                     </td>
                     <td>
                         <asp:TextBox ID="ffrmtitle" runat="server" Width="120px"></asp:TextBox>
                     </td>
                     <td>
                         分享日期
                     </td>
                     <td>
                         <asp:TextBox ID="start" runat="server" Width="100px" CssClass="date"></asp:TextBox>
                     </td>
                     <td>
                         -
                     </td>
                     <td>
                         <asp:TextBox ID="end" runat="server" Width="100px" CssClass="date"></asp:TextBox>
                     </td>
                     <td>
                         <div class="subBar" style="margin-left: 30px; ">
                             <ul>
                                 <li>
                                     <div class="buttonActive">
                                         <div class="buttonContent">
                                             <button type="submit" onclick="return formFind()" style="width: 90px;  ">
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
            <asp:Literal ID="ltlToolBar" runat="server" ></asp:Literal>
        </ul>
    </div>
    <input type="hidden" name="NavTabId" value="<%=NavTabId %>" />
    <input type="hidden" name="status" value="status" />
    <input type="hidden" name="keywords" value="keywords" />
    <input type="hidden" name="numPerPage" value="15" />
    <input type="hidden" name="orderField" value="" />
    <input type="hidden" name="orderDirection" value="" />
    <asp:GridView ID="gvShareHouseList" runat="server" AutoGenerateColumns="False" DataKeyNames="id"
        DataSourceID="ods" AllowPaging="True" CssClass="table" PageSize="20" CellPadding="0"
        GridLines="None" EnableModelValidation="True" EnableViewState="False" >
        <Columns>
            <asp:BoundField DataField="Name" HeaderText="门店">
                <ItemStyle Width="80" />
            </asp:BoundField>
            <asp:BoundField DataField="em_name" HeaderText="姓名">
                <ItemStyle Width="80" />
            </asp:BoundField>
            <asp:BoundField DataField="Title" HeaderText="标题">
                <ItemStyle Width="250" />
            </asp:BoundField>
            <asp:BoundField DataField="HouseBH" HeaderText="房源编号">
                <ItemStyle Width="80" />
            </asp:BoundField>
            <asp:BoundField DataField="TelTotal" HeaderText="拨打电话" SortExpression="TelTotal">
                <ItemStyle Width="80" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="浏览次数" SortExpression="LookTotal">
            <ItemTemplate>
                <a href="House/Templete/ShareSoucre.aspx?NavTabId=<%= NavTabId %>&doAjax=true&HouseID=<%#Eval("id")%>" target="NavTab" title="访问记录" "><%# Eval("LookTotal")%></a>  
            </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="SearchTotal" HeaderText="搜索点击" SortExpression="SearchTotal">
                <ItemStyle Width="80" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="分享来源">
                <ItemTemplate>
                    <%#Eval("SourceType").ToString()=="0"?"电脑":"手机"%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="AddTime" HeaderText="分享时间">
                <ItemStyle Width="120" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <a class="share" href="House/Templete/WebLookiframe.aspx?ID=<%#Eval("id")%>" width="800"
                        height="700" target="dialog" title="预览" maxable='false' rel='FindHouse'><span>浏览</span></a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="ods" runat="server" EnablePaging="True" SelectCountMethod="FindCount"
        SelectMethod="FindAll" SortParameterName="orderClause" EnableViewState="false">
        <SelectParameters>
            <asp:Parameter Name="whereClause" Type="String" />
            <asp:Parameter Name="orderClause" Type="String" DefaultValue="AddTime desc" />
            <asp:Parameter Name="selects" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <TCL:GridViewExtender ID="gvExt" runat="server" LayoutH="150" RowTarget="id" RowRel="id">
    </TCL:GridViewExtender>
    </form>




  <%--     <script type="text/javascript">
　　           function trim(str) { //删除左右两端的空格
               return str.replace(/(^\s*)|(\s*$)/g, "");
           }
           function ltrim(str) { //删除左边的空格
               return str.replace(/(^\s*)/g, "");
           }
           function rtrim(str) { //删除右边的空格
               return str.replace(/(\s*$)/g, "");
           }
           $(document).ready(function () {
               var zftotal = 0;
               var looktotal = 0;
               var seachtotal = 0;

               $("#gvShareHouseList > tbody >tr").each(function () {
                   var t1 = $(this).find("td").eq(5).text();
                   var t2 = $(this).find("td").eq(6).text();
                   var t3 = $(this).find("td").eq(7).text();
                   if (isNaN(parseInt(t1)) == false) {
                       zftotal += parseInt(trim(t1));
                   }
                   if (isNaN(parseInt(t2)) == false) {
                       looktotal += parseInt(trim(t2));
                   }
                   if (isNaN(parseInt(t3)) == false) {
                       seachtotal += parseInt(trim(t3));
                   }
               })

               var strcontent = '<tr ><td style="width:80px;">统计次数</td><td style="width:60px;"></td> <td style="width:150px;"></td><td style="width:300px;"></td> <td style="width:80px;"></td>';
               strcontent += '<td style="width:60px;  font-weight:bold; color:Red;"><div>' + zftotal + '</div></td>';
               strcontent += '<td style="width:60px; font-weight:bold; color:Red;"><div>' + looktotal + '</div></td>';
               strcontent += ' <td style="width:60px; font-weight:bold; color:Red;"><div>' + seachtotal + '</div></td>';
               strcontent += '<td></td><td style="width:100px;"></td><td></td></tr>';
               $("#gvShareHouseList > tbody").append(strcontent);
           })
    
    </script>--%>
</body>
</html>
