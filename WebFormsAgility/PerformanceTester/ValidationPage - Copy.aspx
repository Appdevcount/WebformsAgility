<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ValidationPage.aspx.cs" Inherits="WebFormsAgility.PerformanceTester.ValidationPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>

<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="ValidateData" runat="server" Text="Button" OnClick="ValidateData_Click" />
            <asp:label ID="lblName" runat="server" text="Label"></asp:label>
        </div>
    </form>
</body>
</html>
