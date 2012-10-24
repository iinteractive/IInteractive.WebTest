<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <% Uri absoluteUri = Context.Request.Url;
   string val = absoluteUri.Scheme + "://" + "webcrawlertest2" + (absoluteUri.IsDefaultPort ? "" : (":" + absoluteUri.Port));
   string path = "/ForbiddenTests/RemoteSite/BrokenLink1.aspx";
   string fin = val + path; %>
    <a href="<%=fin %>"><%= fin %></a>
    </div>
    </form>
</body>
</html>
