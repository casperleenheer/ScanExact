Public Class clsExact_Connection

    Private _ServiceURL As String
    Private _SQLserver As String
    Private _Administration As String
    Private _Client As Exact.Services.Client.Entity.EntityClientEG
    Private _Connected As Boolean

    <CLSCompliant(False)>
    Public Property Client As Exact.Services.Client.Entity.EntityClientEG
        Get
            Return _Client
        End Get
        Set(value As Exact.Services.Client.Entity.EntityClientEG)
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
    End Sub

    Public Function SetupConnection() As Boolean
        Try
            _Client = New Exact.Services.Client.Entity.EntityClientEG(_ServiceURL, _SQLserver, _Administration)
        Catch ex As Exception
            MessageBox.Show("Controleer de Exact Instellingen!" & vbCrLf & ex.Message)
            _Connected = False
            Return False
        End Try
        _Connected = True
        Return True
    End Function

    Public Function IsConnectingToService() As Boolean
        Dim _tstClient = New Exact.Services.Client.Metadata.MetadataEG(_ServiceURL)
        Try
            Dim _data As New Exact.Services.Client.Metadata.MetadataEntity
            _data = _tstClient.Retrieve("Resource")
            _Connected = True
        Catch ex As Exception
            _Connected = False
            Return _Connected
        Finally
            _tstClient.Dispose()
        End Try
        Return _Connected
    End Function

    Public Function IsConnectingToDatabase() As Boolean
        Dim _tstClient = New Exact.Services.Client.Entities.EntitiesClientEG(_ServiceURL, _SQLserver, _Administration)
        Try
            Dim _data As New Exact.Services.Client.Data.Entities.RetrieveCriteria
            _data.BatchSize = 2
            _data.EntityName = "Resource"
            _tstClient.RetrieveSet(_data)
            _Connected = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            _Connected = False
            Return _Connected
        Finally
            _tstClient.Dispose()
        End Try
        Return _Connected
    End Function
End Class
