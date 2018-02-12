<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BrowserChecker.aspx.cs" Inherits="WebFormsAgility.UtilsPage.BrowserChecker" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <%
                var browserCapabilities = WebFormsAgility.UtilsPage.BrowserInfoHelper.GetCapabilities();

                string browserCapabilitiesDet = browserCapabilities.Browser + browserCapabilities.MajorVersion + browserCapabilities.MinorVersion + browserCapabilities.Version;

                string BrowserDet = Request.Browser.Browser + Request.Browser.Version;

                Response.Write(BrowserDet);


                %>


        </div>
    </form>
</body>
</html>
