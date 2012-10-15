<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Seed.aspx.cs" Inherits="CrawlerTestApp.AbsoluteUrlTests.CaseA.Seed" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
<% Uri absoluteUri = Context.Request.Url;
   string val = absoluteUri.Scheme + "://" + "webcrawlertest" + (absoluteUri.IsDefaultPort ? "" : (":" + absoluteUri.Port));
   string path = "/AbsoluteUrlTests/CaseA/Linked.htm";
   string fin = val + path; %>
    <a href="<%=fin %>"><%= fin %></a>
</body>
</html>
