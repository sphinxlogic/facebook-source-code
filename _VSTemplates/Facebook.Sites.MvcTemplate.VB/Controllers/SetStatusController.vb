Imports Facebook.Web.Mvc

<HandleError()> _
<Authorize()> _
Public Class SetStatusController
    Inherits FacebookMvcController

    Public Function Index(ByVal previousResult As String) As ActionResult

        Me.ViewData("PreviousResult") = previousResult
        Return Me.View()

    End Function

    'would really prefer to call this "Set" like I did in the C# version, but since "Set" is a keyword in VB.NET...
    Public Function Post(ByVal status As String) As ActionResult

        Dim result As FacebookResponse(Of Boolean) = Me.FbContext.Status.Set(status)
        Dim resultMessage As String = Nothing

        If (result.IsError) Then
            resultMessage = String.Format("There was an error setting your status: {0}", result.ResponseException.Message)
        Else
            resultMessage = "Your status was set successfully."
        End If

        Return Me.RedirectToAction("Index", New With {.previousResult = resultMessage})

    End Function

End Class
