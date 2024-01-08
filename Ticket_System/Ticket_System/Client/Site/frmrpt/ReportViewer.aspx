<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ReportViewer.aspx.vb" Inherits="Site_frmrpt_ReportViewer" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <style type="text/css">




table {
	font-size: 1em;
}

    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
            <CR:CrystalReportViewer ID="CrystalRptVwr" runat="server" 
                AutoDataBind="true" />
    
    </div>
    </form>
</body>
</html>
