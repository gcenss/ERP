<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShareFindHouse.aspx.cs" Inherits="HouseMIS.Web.House.Templete.ShareFindHouse" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form rel="pagerForm" runat="server"  name="pagerForm" id="pagerForm" onsubmit="return dialogSearch(this);" action="House/Templete/ShareFindHouse.aspx" method="post">
      <div class="pageHeader">
         <div class="searchBar close">
             <table class="searchContent">
                 <tr>
                     <td>
                         模板类型
                     </td>
                     <td>
                         <asp:DropDownList ID="ffrmShareType" runat="server" Height="20px" Width="126px" CssClass="required">
                             <asp:ListItem Text="" Value=""></asp:ListItem>
                             <asp:ListItem Text="时事新闻" Value="时事新闻"></asp:ListItem>
                             <asp:ListItem Text="生活常识" Value="生活常识"></asp:ListItem>
                             <asp:ListItem Text="开心一刻" Value="开心一刻"></asp:ListItem>
                             <asp:ListItem Text="生活速递" Value="生活速递"></asp:ListItem>
                             <asp:ListItem Text="娱乐时尚" Value="娱乐时尚"></asp:ListItem>
                             <asp:ListItem Text="房产家居" Value="房产家居"></asp:ListItem>
                             <asp:ListItem Text="保健养生" Value="保健养生"></asp:ListItem>
                             <asp:ListItem Text="体育健身" Value="体育健身"></asp:ListItem>
                         </asp:DropDownList>
                     </td>
                     <td>
                         标题
                     </td>
                     <td>
                         <asp:TextBox ID="ffrmTitle" runat="server" Width="120px" IsLike="true"></asp:TextBox>
                     </td>
                     <td>
                     <div class="subBar">
                         <ul>
                             <li>
                                 <div class="buttonActive">
                                     <div class="buttonContent">
                                         <button type="submit" onclick="return formFind()">
                                             检索</button>
                                     </div>
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
    <div class="pageContent" style="padding: 0px 2px 2px 2px">
        <input type="hidden" name="NavTabId" value="<%=NavTabId %>" />
        <input type="hidden" name="status" value="status" />
        <input type="hidden" name="keywords" value="keywords" />
        <input type="hidden" name="numPerPage" value="20" />
        <input type="hidden" name="orderField" value="" />
        <input type="hidden" name="orderDirection" value="" />
        <asp:HiddenField  runat="server" ID="Type"   />
        <asp:HiddenField  runat="server" ID="HouseID"   />


        <table class="list" id="ShareTempleteGridView" width="100%" layouth="100">
            <thead>
                <tr>
                    <th>
                        模板类型
                    </th>
                    <th width="50px">
                        类别
                    </th>
                    <th>
                        标题
                    </th>
                    <th>
                        作者
                    </th>
                    <th>
                        添加时间
                    </th>
                    <th width="60px">
                        分享次数
                    </th>
                    <% =Convert.ToInt32(Request.QueryString["Type"]) == 2 ? "<th> 移除收藏</th>" : "<th> 收藏</th>"%>
                  
                    <th>
                        分享
                    </th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rpt1" runat="server" >
                    <ItemTemplate>
                        <tr target="MasterKeyValue" rel="<%#Eval("id")%>">
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
                                <%#Eval("em_name")%>
                            </td>
                            <td>
                                <%# Eval("AddDate") %>
                            </td>
                            <td>
                                <%# Eval("ShareTotal")%>
                            </td>
                            
                            <td class="hidde"  style="display:none">
                            <%#Eval("id") %>
                            </td>
                            
                            
                            <% =Convert.ToInt32(Request.QueryString["Type"]) == 2 ? "<td class=\"content\"><a href=\"House/Templete/ShareTempleteForm.aspx?Type=yc&&doAjax=true\" target=\"ajaxTodo\">移除</a></td>" : "<td class=\"content\"><a href=\"House/Templete/ShareTempleteForm.aspx?Type=sc&&doAjax=true\" target=\"ajaxTodo\">收藏</a></td>"%>
                           
                            <td class="contents">
                                <a class="btnSelect" href="House/Templete/ShareInfoIframe.aspx?HouseID=<%= Request.QueryString["HouseID"]==null? Request.Form["HouseID"]:Request.QueryString["HouseID"] %>&TempleteID=<%#Eval("id") %>"
                                    rel="lookHouse" width="800" height="700" target="dialog" title="预览" maxable='false'
                                    title="查找带回">选择</a>
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
            <div class="pagination" targettype="dialog" totalcount="<% =TotalCount %>" numperpage="<% =NumPerPage %>"
                pagenumshown="<%=PageNumShown %>" currentpage="<% =PageNum %>">
            </div>
            <input type="hidden" name="NavTabId" value="<%=NavTabId %>" />
            <input type="hidden" name="status" value="status" />
            <input type="hidden" name="keywords" value="keywords" />
            <input type="hidden" name="numPerPage" value="<%=NumPerPage %>" />
            <input type="hidden" name="pageNum" value="1" />
         
        </div>

    </div>
    
    </form>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#ShareTempleteGridView >tbody>tr>td[class='hidde']").each(function () {
                var v = $(this).next(".content");
                if (v.html() != null) {

                    var href = $(v).find("a").attr("href");
                    href += "&TempleteID="+$(this).text();
                    $(v).find("a").attr("href",href);
                   
                }

            })
        })

    </script>
</body>
</html>
