<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportPage.aspx.cs" Inherits="WebFormsAgility.Reports.ReportPage" %>

<%@ Register Assembly="Syncfusion.EJ.Web, Version=15.3460.0.29, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.JavaScript.Web" TagPrefix="ej" %>

<%--<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>--%>

<%--<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>--%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Content/Bootstrap/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
<%--            <rsweb:ReportViewer ID="ReportViewer1" runat="server">
            </rsweb:ReportViewer>--%>
            <ej:ReportViewer runat="server"></ej:ReportViewer>
            <ej:Tile ID="Tile1" runat="server"></ej:Tile>
            <ej:Map ID="Map1" runat="server"></ej:Map>
            <ej:CircularGauge ID="CircularGauge1" runat="server"></ej:CircularGauge>
            <ej:Button ID="Button1" runat="server" OnClick="Button1_Click"  Text="Button"></ej:Button>
            <ej:ListBox ID="ListBox1" runat="server" CssClass="form-control">
                <Items>
                    <ej:ListBoxItems Text="ListItem1">
                    </ej:ListBoxItems>
                    <ej:ListBoxItems Text="ListItem2">
                    </ej:ListBoxItems>
                    <ej:ListBoxItems Text="ListItem3">
                    </ej:ListBoxItems>
                    <ej:ListBoxItems Text="ListItem4">
                    </ej:ListBoxItems>
                    <ej:ListBoxItems Text="ListItem5">
                    </ej:ListBoxItems>
                    <ej:ListBoxItems Text="qs" Value="qSD">
                    </ej:ListBoxItems>
                </Items>
            </ej:ListBox>
        </div>
    </form>
</body>
</html>
