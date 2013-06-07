<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Seed.aspx.cs" Inherits="CrawlerTestApp.HtmlEncodingTests.TestStyleSheet.CaseF.Seed" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <link rel="stylesheet" type="text/css" href="/HtmlEncodingTests/NonSeed/DynamicAAndB.aspx?a=1&amp;b=1">
        <link rel="stylesheet" type="text/css" href="/HtmlEncodingTests/NonSeed/StyleSheet.css">
    </div>
    </form>
</body>
</html>
