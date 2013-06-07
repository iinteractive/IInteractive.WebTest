<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DynamicAAndB.aspx.cs" Inherits="CrawlerTestApp.HtmlEncodingTests.DynamicAandB" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <% 
            var a = this.Context.Request.Params.Get("a");
            var b = this.Context.Request.Params.Get("b");

            if (string.IsNullOrEmpty(a) || string.IsNullOrEmpty(b))
            {
                this.Context.Response.StatusCode = 500;
            }
        %>
    </div>
    </form>
</body>
</html>
