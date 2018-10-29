<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HouseBhReasonAdd.aspx.cs" Inherits="HouseMIS.Web.House.HouseBhReasonAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
   <div class="pageContent">
    <form id="form1" runat="server" action="House/HouseBhReasonAdd.aspx" class="pageForm required-validate" onsubmit="return validateCallback(this, dialogAjaxDone)">
      <input type="hidden" name="Key" id="Key" runat="server" />
        <div class="pageFormContent" layoutH="48">
       <fieldset><legend>驳回</legend>
         <table width="100%">
          <tr>
		   <td width="15"></td>
            <td  width="80">驳回原因</td>
            <td colspan="4">
                <asp:TextBox ID="frmRemark" runat="server" TextMode="MultiLine" Width="97%" Height="100" CssClass="required" ></asp:TextBox>
			 </td>
           </tr>

        </table>
</fieldset>
</div>
        <div class="formBar">
          <ul> 
             <li><div class="buttonActive"><div class="buttonContent"><button type="submit">保存</button></div></div></li>
			    <li>
				    <div class="button"><div class="buttonContent"><button type="button" class="close">取消</button></div></div>
			    </li>
		    </ul>
	   </div>
    </form>
 </div>
</body>
</html>