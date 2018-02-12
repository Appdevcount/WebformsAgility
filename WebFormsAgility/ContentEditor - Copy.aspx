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
          <%--  
            <textarea name="editor1" id="editor1" rows="10" cols="80">
                This is my textarea to be replaced with CKEditor.
            </textarea>--%>
            <%--<input type="submit" id="SubmitBtn" value="Submit" onclick="Alertm()" />--%>
            <asp:Button ID="SubmitBtn" runat="server" Text="Button" />
        </div>
    </form>
   <%-- <textarea cols="30" rows="10" id="input"></textarea>
    <p>
        <button id="sanitize">Sanitize</button>
    </p>
    <p id="output"></p>--%>
    <script type="text/javascript">
        CKEDITOR.replace('<%=Content.ClientID %>');
       
            </script>
    <script type="text/javascript">

        CKEDITOR.replace('editor1');
        //function Alertm()
        //{
        //    debugger;
        //    var Content = $('#editor1').val();
        //    alert(Content);
        //}
        (function ($) {
            $.sanitize = function (input) {
                var output = input.replace(/<script[^>]*?>.*?<\/script>/gi, '').
                    replace(/<[\/\!]*?[^<>]*?>/gi, '').
                    replace(/<style[^>]*?>.*?<\/style>/gi, '').
                    replace(/<![\s\S]*?--[ \t\n\r]*>/gi, '');
                return output;
            };
        })(jQuery);

        $(function Alertm() {
            $('#SubmitBtn').click(function () {
                //var $input = $('#editor1').val();
                var $input = CKEDITOR.instances['editor1'].getData();
                alert($.sanitize($input));
                //$('#output').text($.sanitize($input));
            });

        });

        //$(function () {
        //    $('#sanitize').click(function () {
        //        var $input = $('#Cont').val();
        //        //alert($.sanitize($input));
        //        $('#output').text($.sanitize($input));
        //    });

        //});

    </script>
</body>
</html>
