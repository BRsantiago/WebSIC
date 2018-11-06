<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="WebSIC.Report" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<script lang="javaScript" type="text/javascript" src="aspnet_client\system_web\4_0_30319\crystalreportviewers13\js\crviewer\crv.js"></script>
<link href="/Content/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet">
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="form-horizontal">
            <div class="form-group">
                <div class="col-md-2">
                    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" CssClass="Report"
                        HasCrystalLogo="False" HasSearchButton="False" HasToggleGroupTreeButton="False" HasZoomFactorList="False"
                        meta:resourcekey="CrystalReportViewer1Resource1" EnableDrillDown="False" ReuseParameterValuesOnRefresh="True"
                        EnableToolTips="False" ToolPanelView="None" HasDrilldownTabs="False" HasDrillUpButton="False" HasPageNavigationButtons="False"
                        HasGotoPageButton="False" HasToggleParameterPanelButton="false" HasPrintButton="False" HasExportButton="False" HasRefreshButton="False"
                        DisplayToolbar="False" DisplayStatusbar="false" />
                </div>
                <div class="col-md-2">
                    <CR:CrystalReportViewer ID="CrystalReportViewer2" runat="server" AutoDataBind="true" CssClass="Report"
                        HasCrystalLogo="False" HasSearchButton="False" HasToggleGroupTreeButton="False"
                        HasZoomFactorList="False" meta:resourcekey="CrystalReportViewer1Resource1" EnableDrillDown="False"
                        ReuseParameterValuesOnRefresh="True" EnableToolTips="False" ToolPanelView="None" HasDrilldownTabs="False"
                        HasDrillUpButton="False" HasPageNavigationButtons="False" HasGotoPageButton="False" HasToggleParameterPanelButton="false"
                        HasPrintButton="False" HasExportButton="False" HasRefreshButton="False" DisplayToolbar="False" DisplayStatusbar="false" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>


