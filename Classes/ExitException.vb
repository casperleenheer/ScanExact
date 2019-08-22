<SerializableAttribute> _
Public Class ExitException
    Inherits Exception
    Public Overrides ReadOnly Property Message() As String
        Get
            Return "Exit"
        End Get
    End Property
End Class
