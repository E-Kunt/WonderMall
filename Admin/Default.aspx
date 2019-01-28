<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    
    
</head>
<body>
   
   <form id="form1" runat="server">
     <asp:FileUpload ID="FileUpload1" runat="server" />
     <asp:Button ID="Button1" runat="server" Text="上传" OnClick="Button1_Click" />
     <asp:Label ID="Label1" runat="server" Text="" Style="color: Red"></asp:Label><br/>
     <p><img src="../Imges/zhanweitu.gif" id="img1" runat="server" class="pImagePath1" style="width: 205px;500px"/></p>
     </form>
</body>
</html>
