<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContentEditor.aspx.cs" Inherits="WebFormsAgility.ContentEditor" %>

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
            <asp:TextBox ID="Content" runat="server" TextMode="MultiLine" Rows="10" Columns="10" Text="This is my textarea to be replaced with CKEditor." >
                
            </asp:TextBox>

            <asp:Button ID="SubmitBtn" runat="server" OnClick="SubmitBtn_Click" Text="Button" />
        </div>
    </form>

    <script type="text/javascript">
        CKEDITOR.replace('<%=Content.ClientID %>');
       
            </script>

</body>
</html>
