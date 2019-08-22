Public Class clsExact_EntitiesConnection

    Private _ServiceURL As String
    Private _SQLserver As String
    Private _Administration As String
    Private _Client As Exact.Services.Client.Entities.EntitiesClientEG
    Private _Connected As Boolean

    <CLSCompliant(False)>
    Public Property Client As Exact.Services.Client.Entities.EntitiesClientEG
        Get
            Return _Client
        End Get
        Set(value As Exact.Services.Client.Entities.EntitiesClientEG)
            _Client = value
        End Set
    End Property

    Public Property Connected As Boolean
        Get
            Return _Connected
        End Get
        Set(value As Boolean)
            _Connected = value
        End Set
    End Property

    Public Property AdministrationNumber As String
        Get
            Return _Administration
        End Get
        Set(value As String)
            _Administration = value
        End Set
    End Property

    Public Sub New(AdministrationNumber As String, ServiceURL As String, SQLserver As String)
        _Administration = AdministrationNumber
        _SQLserver = SQLserver
        _ServiceURL = ServiceURL
        'SetupConnection()
    End Sub

    Public Function SetupConnection() As Boolean
        Try
            _Client = New Exact.Services.Client.Entities.EntitiesClientEG(_ServiceURL, _SQLserver, _Administration)
        Catch ex As Exception
            MessageBox.Show("Controleer de Exact Instellingen!" & vbCrLf & ex.Message)
            _Connected = False
            Return False
        End Try
        _Connected = True
        Return True
    End Function

End Class
