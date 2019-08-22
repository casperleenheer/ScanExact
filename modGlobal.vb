
Module modGlobal

    Public EntityConnection As New List(Of clsExact_Connection)
    Public EntitiesConnection As New List(Of clsExact_EntitiesConnection)
    Public AdministrationNumbers As New List(Of String)
    Public AvailableInAdministration As New List(Of Boolean)
    Public ScannedFilesForImportToExact As New List(Of String)

    Function Administrations_split(AdministrationString As String) As List(Of String)
        AdministrationNumbers.Clear()
        Dim admins() As String = AdministrationString.Split(";")
        For Each admin In admins
            If admin.Trim <> "" Then AdministrationNumbers.Add(admin)
        Next
        Return AdministrationNumbers
    End Function

    Function IsServiceRunning(ByVal sServiceName As String) As Boolean
        Try
            Dim myServiceController As ServiceProcess.ServiceController
            myServiceController = New ServiceProcess.ServiceController(sServiceName)
            Return myServiceController.Status = ServiceProcess.ServiceControllerStatus.Running
        Catch ex As Exception
            Trace.WriteLine("Print spooler service not running !" & vbCrLf & ex.ToString)
            Return False
        End Try
    End Function

    Function IsDefaultPrinterInstalled() As Boolean
        If Printing.PrinterSettings.InstalledPrinters.Count = -1 Then
            Return False
            Trace.Write("No default printer installed!")
        Else
            Return True
        End If
    End Function

End Module
