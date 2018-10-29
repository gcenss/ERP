<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShareTempleteList.aspx.cs" Inherits="HouseMIS.Web.House.Templete.ShareTempleteList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <div class="pageContent">
        <form name="pagerForm" id="pagerForm" runat="server" action="House/Templete/ShareTempleteList.aspx"
        method="post" onsubmit="return navTabSearch(this);">
        <div class="pageHeader">
         <div class="searchBar">
             <table class="searchContent">
                 <tr>
                     <td>
                         模板类型
                     </td>
                     <td>
                         <asp:DropDownList ID="ffrmShareType" runat="server" Height="20px" Width="126px" CssClass="required">
                             <asp:ListItem Text="" Value=""></asp:ListItem>
                              <asp:ListItem Text="好文章" Value="好文章"></asp:ListItem>
                        <asp:ListItem Text="视频" Value="视频"></asp:ListItem>
                        <asp:ListItem Text="游戏" Value="游戏"></asp:ListItem>
                         </asp:DropDownList>
                     </td>
                     <td>
                         标题
                     </td>
                     <td>
                         <asp:TextBox ID="ffrmTitle" runat="server" Width="120px"></asp:TextBox>
                     </td>
                     <td>
                         姓名
                     </td>
                     <td>
                         <asp:TextBox ID="ffrmName" runat="server" Width="120px"></asp:TextBox>
                     </td>
                     <td>
                         添加日期
                     </td>
                     <td>
                         <asp:TextBox ID="ffrmstart" runat="server" Width="100px" CssClass="date"></asp:TextBox>
                     </td>
                     <td>
                         -
                     </td>
                     <td>
                         <asp:TextBox ID="ffrmend" runat="server" Width="100px" CssClass="date"></asp:TextBox>
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
                <asp:Literal ID="LiteralTool" runat="server"></asp:Literal>
            </ul>
        </div>
       
         <asp:HiddenField  runat="server" ID="Type"   />
        <table class="list" id="ShareTempleteGridView" width="100%" layouth="130">
            <thead>
                <tr>
                    <th>部门</th>
                    <th width="80px">
                        模板类型
                    </th>
                    <th width="50px">
                        类别
                    </th>
                    <th>
                        标题
                    </th>
                    <th>
                        内容
                    </th>
                    <th width="60px">
                        分享次数
                    </th>
                    <th>
                        添加时间
                    </th>
                    <th width="60px">
                        作者
                    </th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rpt1" runat="server">
                    <ItemTemplate>
                        <tr target="MasterKeyValue" rel="<%#Eval("id")%>">
                            <td>
                            <%#Eval("Name")%>
                            </td>
                            <td>
                                <%#Eval("ShareType")%>
                            </td>
                            <td>
                                <%# Eval("Type").ToString()=="0"?"公共":"私有"%>
                            </td>
                            <td>
                                <%# ParseTags(Eval("Title").ToString(),10) %>
                            </td>
                            <td>
                            <%# ParseTags(Eval("Contents") == null ? "" : Eval("Contents").ToString(), 30)%>
                            </td>
                            <td>
                                <%# Eval("ShareTotal")%>
                            </td>
                            <td class="contents">
                                <%# Eval("AddDate") ==DBNull.Value ? " " :Eval("AddDate")%>
                            </td>
                            <td class="contents">
                                <%# Eval("em_name")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
        <div class="panelBar">
            <div class="pages">
                <span>显示</span>
                <select class="combox" name="numPerPage" onchange="navTabPageBreak({numPerPage:this.value})">
                    <option value="<%= NumPerPage %>">选择</option>
                    <option value="20">20</option>
                    <option value="30">30</option>
                    <option value="50">50</option>
                    <option value="100">100</option>
                    <select>
                        <span>条，共<% =TotalCount%>条</span>
            </div>
            <div class="pagination" targettype="navTab" totalcount="<% =TotalCount %>" numperpage="<% =NumPerPage %>"
                pagenumshown="<%=PageNumShown %>" currentpage="<% =PageNum %>">
            </div>
            <input type="hidden" name="NavTabId" value="<%=NavTabId %>" />
            <input type="hidden" name="status" value="status" />
            <input type="hidden" name="keywords" value="keywords" />
            <input type="hidden" name="numPerPage" value="<%=NumPerPage %>" />
            <input type="hidden" name="pageNum" value="1" />
<%--            <input type="hidden" runat="server" id="ods" />--%>
        </div>
        </form>
    </div>



</body>
</html>
