<%@ Page Language="C#" AutoEventWireup="true" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
</head>
<body>
<% Uri absoluteUri = Context.Request.Url;
   string val = absoluteUri.Scheme + "://" + "webcrawlertest2" + (absoluteUri.IsDefaultPort ? "" : (":" + absoluteUri.Port));
   string path = "/ForbiddenTests/RemoteSite/BadLink1.htm";
   string fin = val + path; %>
    <a href="<%=fin %>"><%= fin %></a>
    <a href="/ForbiddenTests/NonSeed/GoodLink1.htm">GoodLink1</a>
    <a href="/ForbiddenTests/NonSeed/GoodLink2.htm">GoodLink2</a>
</body>
</html>
