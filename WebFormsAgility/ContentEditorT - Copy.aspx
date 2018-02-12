<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContentEditorT.aspx.cs" Inherits="WebFormsAgility.ContentEditorT" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <script src="Content/Scripts/jquery-3.2.1.min.js"></script>
    <script src="Content/ckeditor/ckeditor.js"></script>
    <script src="Content/ckeditor/styles.js"></script>
    <script src="Content/ckeditor/config.js"></script>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

                    <asp:TextBox ID="Content" runat="server" TextMode="MultiLine" Rows="10" Columns="10" Text="This is my textarea to be replaced with CKEditor.">
                
                    </asp:TextBox>
                    <%-- class="ckeditor"--%>
                    <asp:Button ID="SubmitBtn" runat="server" OnClick="SubmitBtn_Click" Text="Button" />



                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>

                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="SubmitBtn" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </form>
    <script type="text/javascript">
        CKEDITOR.replace('<%=Content.ClientID %>')
        //editorConfig = function (config) {
       <%-- CKEDITOR.replace('<%=Content.ClientID %>'
            , {
            toolbar =[
                { name: 'basicstyles', items: ['Bold', 'Italic', 'Underline'] },
                { name: 'paragraph', items: ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', '-', 'Blockquote', 'CreateDiv', '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', '-', 'BidiLtr', 'BidiRtl', 'Language'] },
                { name: 'insert', items: ['Image', 'Table', 'HorizontalRule', 'Smiley', 'PageBreak'] },
                { name: 'styles', items: ['Styles', 'Format', 'Font', 'FontSize'] },
                { name: 'colors', items: ['TextColor', 'BGColor'] },
                { name: 'tools', items: ['Maximize'] }
            ],
            disallowedContent = 'script; *[on*]'
        });--%>

     
    </script>

</body>
</html>
