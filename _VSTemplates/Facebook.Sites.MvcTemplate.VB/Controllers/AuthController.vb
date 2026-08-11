Imports Facebook.Web.Mvc

<HandleError()> _
Public Class AuthController
    Inherits FacebookMvcController

    Public Function Revoke() As ActionResult

        Me.FbContext.Auth.RevokeAuthorization(Me.FbSession.Uid)
        Me.FbSession.Added = False
        Me.FbContext.Session.Expires = DateTime.Now

        Return Me.Redirect("~/")
    End Function


    Public Function Login(ByVal returnUrl As String) As ActionResult

        Me.ViewData("returnUrl") = If(returnUrl, FormsAuthentication.DefaultUrl)
        Return Me.View()

    End Function


    Public Function Redirect(ByVal path As String) As ActionResult

        Return Me.Redirect(path)

    End Function

End Class