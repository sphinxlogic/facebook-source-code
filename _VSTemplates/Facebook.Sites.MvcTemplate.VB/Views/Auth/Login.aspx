<%@ Page Language="VB" Inherits="Facebook.Web.Mvc.FacebookViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Login</title>
    <link href="/Content/site.css" type="text/css" rel="Stylesheet" />
</head>
<body>
    <script src="http://static.ak.facebook.com/js/api_lib/v0.4/FeatureLoader.js.php" type="text/javascript"></script>
    <script type="text/javascript">
        FB_RequireFeatures(["CanvasUtil", "Api"], function() {
            FB.Facebook.init("<%= Me.FbContext.ApiKey %>", "/xd_receiver.htm");
            FB.Connect.requireSession(function(){
                <%= FbIFrame.RequireSessionScript(CType(Me.ViewData("returnUrl"), String)) %>});
        });
    </script>
</body>
</html>
