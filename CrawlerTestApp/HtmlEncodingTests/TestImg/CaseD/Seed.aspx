﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Seed.aspx.cs" Inherits="CrawlerTestApp.HtmlEncodingTests.TestImg.CaseD.Seed" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <img src="/HtmlEncodingTests/NonSeed/DynamicAAndB.aspx?b=1&c=1" />
        <img src="/HtmlEncodingTests/NonSeed/DynamicAAndB.aspx?b=1&amp;c=1" />
    </div>
    </form>
</body>
</html>