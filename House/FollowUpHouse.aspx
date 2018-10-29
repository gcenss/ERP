<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FollowUpHouse.aspx.cs" Inherits="HouseMIS.Web.House.FollowUpHouse" %>

<style>
    .fuh_tab td
    {
        text-align: left;
        font-size: 12px;
        padding-bottom: 5px;
        padding-left: 4px;
        height: auto;
    }
    .fuh_tab_input
    {
        width: 94px;
        padding:0px;
        margin:0px;
    }

</style>
<form rel="FollowHouseFrom" runat="server" name="FollowHouseFrom" id="FollowHouseFrom" onsubmit="validateCallback(this, dialogAjaxDone);$.pdialog.close('zj_follwUpEx');return false;"
action="House/FollowUpHouse.aspx" method="post">
<div class="pageContent">
    <div class="pageFormContent">
        <table cellpadding="0" cellspacing="0" border="0" width="100%" class="fuh_tab">
            <tr>
                <td width="90">
                    房源编号:
                </td>
                <td width="100">
                     <asp:TextBox runat="server" CssClass="fuh_tab_input" ID="shi_id" ReadOnly="true"></asp:TextBox>
                </td>
                 <td width="80" align="left">
                        (<span style="color: Red;"><%=HouseMIS.EntityUtils.I_IntegralItem.FindByRemarkTemplate("更正看房时间") %></span>)看房:
                </td>
                <td align="left">
                    <asp:DropDownList ID="frmSeeHouseID" runat="server" Width="100px"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td width="90">
                    (<span style="color:Red;"><%=HouseMIS.EntityUtils.I_IntegralItem.FindByRemarkTemplate("更正房源现状") %></span>)现状:
                </td>
                <td>
                     <asp:DropDownList ID="frmNowStateID" runat="server" Width="100px"></asp:DropDownList>
                </td>
                 <td width="80">
                         (<span style="color:Red;"><%=HouseMIS.EntityUtils.I_IntegralItem.FindByRemarkTemplate("更正房源外墙") %></span>)外墙:
                </td>
                <td>
                    <asp:DropDownList ID="frmApplianceID" runat="server" Width="100px"></asp:DropDownList>
                </td>
            </tr>
             <tr>
                <td width="90">
                    (<span style="color:Red;"><%=HouseMIS.EntityUtils.I_IntegralItem.FindByRemarkTemplate("更正房源带看人") %></span>)带看人:
                </td>
                <td>
                     <asp:DropDownList ID="frmPayServantID" runat="server" Width="100px"></asp:DropDownList>
                </td>
                 <td width="80">
                        (<span style="color:Red;"><%=HouseMIS.EntityUtils.I_IntegralItem.FindByRemarkTemplate("更正房源产证情况") %></span>)产证情况:
                </td>
                <td>
                    <asp:DropDownList ID="frmAssortID" runat="server" Width="100px"></asp:DropDownList>
                </td>
            </tr>
             <tr>
                <td width="90">
                    (<span style="color:Red;"><%=HouseMIS.EntityUtils.I_IntegralItem.FindByRemarkTemplate("更正房源光线情况") %></span>)光线情况:
                </td>
                <td>
                     <asp:DropDownList ID="frmSaleMotiveID" runat="server" Width="100px"></asp:DropDownList>
                </td>
               <%if(HouseMIS.EntityUtils.Employee.Current.ComID == 2){ %>
                                                      <td width="80">
                                                委托
                                                <span style="color: Red;"><%=HouseMIS.EntityUtils.I_IntegralItem.FindByRemarkTemplate("独家代理") %></span>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="frmEntrustTypeID" runat="server" Width="100px" CssClass="frmEntrustTypeID">
                                                </asp:DropDownList>
                                            </td>  
            </tr>     <tr>
                <td>出售原因<span class="HPoint" style="color: Red;"><%=HouseMIS.EntityUtils.I_IntegralItem.FindByRemarkTemplate("填写出售原因") %></span></td>
                <td colspan="3">
                    <asp:TextBox runat="server" Width="282px" ID="frmLinkTel2" CssClass="frmLinkTel2"></asp:TextBox>
                </td>
                </tr>     
            <tr>
                <td>描述<span class="HPoint" style="color: Red;"><%=HouseMIS.EntityUtils.I_IntegralItem.FindByRemarkTemplate("填写房源点评") %></span></td>
                <td colspan="3">
                     <asp:TextBox runat="server" Width="282px" ID="frmLinkTel1" CssClass="frmLinkTel1"></asp:TextBox>
                </td>
                </tr>     
            <%}else{ %>
                  <td width="80">
                                              
                                            </td>
                                            <td>
                                         
                                            </td>  

            <%} %>
        <asp:hiddenfield runat="server" id="HouseID"></asp:hiddenfield>
   </table>
  </div>
  <div class="formBar">
        <ul>
            <li>
               <div class="buttonActive"><div class="buttonContent"><button type="submit">保存</button></div></div>
            </li>
            <li>
                <div class="button">
                    <div class="buttonContent">
                        <button type="button" class="close">
                            取消</button></div>
                </div>
            </li>
        </ul>
    </div>
 </div>
</form>