<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContentEditorT.aspx.cs" Inherits="WebFormsAgility.ContentEditorT" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <script src="Content/Scripts/jquery-3.2.1.min.js"></script>
    <script src="Content/ckeditor/ckeditor.js"></script>
    <form id="form1" runat="server">
        <div>


           <%-- <asp:TextBox ID="Content" runat="server" TextMode="MultiLine" Rows="10" Columns="10" Text="This is my textarea to be replaced with CKEditor.">
                
            </asp:TextBox>--%>
            <textarea id="editor1">
                This is my textarea to be replaced with CKEditor.
			</textarea>
            <%--<asp:Button ID="SubmitBtn" runat="server" OnClick="SubmitBtn_Click" Text="Button" />--%>
            <asp:Button ID="Button1" runat="server"  Text="Button" />
            
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            class="ckeditor"
            
        </div>
    </form>
    <script type="text/javascript">
  <%--      CKEDITOR.replace('<%=Content.ClientID %>');--%>
        CKEDITOR.replace('editor1');
    </script>

</body>
</html>
