Imports WIA
Imports System.ComponentModel
Imports System.Globalization
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Reflection.Assembly
Imports PdfWriter

<Assembly: CLSCompliantAttribute(True)>
Class appControl

    Shared _deviceID As String
    Private Shared LocRM As Resources.ResourceManager
    Private Shared GetCulturesThread As Threading.Thread

    Private Shared _availableCultures As Globalization.CultureInfo()
    Private Shared HasThreadFinished As Boolean
    Private Shared WithEvents _scanner As Scanner
    Private Shared WithEvents _printer As New Printer
    Private Shared CommandLine As Boolean

    Public Shared MainForm As mainFrm

    Shared Sub Main(ByVal sArgs() As String)
        Dim _device As Device
        Dim _wscanner As WIA.Item
        Dim manager As DeviceManager

        My.Settings.Silent = False
        If My.Settings.LastScanSettings Is Nothing Then
            My.Settings.LastScanSettings = New ScanSettings()
            My.Settings.Save()
        End If

        GetCulturesThread = New Threading.Thread(AddressOf GetAvailableLanguages)

        LocRM = New System.Resources.ResourceManager("ScanExact.WinFormStrings", GetType(mainFrm).Assembly)

        'Deletes log file if it is more than 50 KB long
        Dim logPath As String = Path.Combine(GetWritablePath(), "Scan.log")

        Try
            Dim MyFile As New FileInfo(logPath)
            Dim FileSize As Long = MyFile.Length
            If FileSize > 50 * 1024 Then 'Clear the log if it is more than 50 KB

                File.Delete(logPath)

            End If
        Catch ex As Exception

        End Try

        'Sets trace output to console and log file
        Trace.AutoFlush = True

        Dim logListener As New TextWriterTraceListener(logPath, "log")

        Trace.Listeners.Add(logListener)

        Trace.WriteLine("")
        Trace.WriteLine(DateTime.Now)
        Trace.WriteLine(String.Format("ScanExact Version: {0}", Application.ProductVersion))
        Trace.WriteLine(String.Format("Windows Version: {0}", System.Environment.OSVersion.VersionString))
        Trace.WriteLine(String.Format(".NET Version: {0}", System.Environment.Version.ToString()))
        Trace.WriteLine(String.Format("CultureInfo: {0}", GetCulturesThread.CurrentUICulture.Name))
        Trace.WriteLine(String.Format("-----------------"))
        Dim resNames = GetExecutingAssembly.GetManifestResourceNames
        For Each name In resNames
            Trace.WriteLine(String.Format("Resourcename(s) :" & name))
        Next
        Trace.WriteLine(String.Format("-----------------"))
        Trace.Indent()

        Try
            If sArgs.Length = 0 Then 'If there are no arguments, run app normally
                Application.EnableVisualStyles()

                CommandLine = False
                'Avoids that two processes run simultaneously
                If Process.GetProcessesByName("ScanExact").Length > 1 Then
                    MsgBoxWrap(LocRM.GetString("Msg_AlreadyRunning"), MsgBoxStyle.Information, "ScanExact")
                    Throw New ExitException
                End If

                'Searches for languages installed
                Try            'Should avoid ThreadStateException
                    If GetCulturesThread.ThreadState = Threading.ThreadState.Unstarted Then
                        GetCulturesThread.Start()
                    End If
                Catch ex As Threading.ThreadStateException
                    MsgBoxWrap(ex.ToString)
                End Try


                manager = New DeviceManager 'This is the first call to WIA library. If it isn't registered, an error is thrown

                'Initializes new scanning interface
                appControl.CreateScanner(My.Settings.DeviceID)

                Try
                    My.Settings.DeviceID = _scanner.DeviceId
                Catch ex As NullReferenceException
                    Application.Exit()
                End Try

                MainForm = New mainFrm()
                Application.Run(MainForm)
                If Not My.Settings.RememberSettings Then
                    My.Settings.LastScanSettings = New ScanSettings() 'Scan settings are restored to the default
                End If

                My.Settings.Save()
                Application.Exit()

            Else    'Handle Command line arguments
                CommandLine = True 'To inform the program that it is running in command line mode

                'Prints the argument list for debugging purpose
                Dim argstring As String = ""
                For Each arg As String In sArgs
                    argstring += arg + " "
                Next
                Trace.WriteLine(String.Format("Command Line parameters: {0}", argstring))

                For i As Integer = 0 To sArgs.GetUpperBound(0)
                    If sArgs(i) = "/silent" Then My.Settings.Silent = "True"
                Next

                'Utility commands (no WIA library references)
                For i As Integer = 0 To sArgs.GetUpperBound(0)
                    Select Case sArgs(i).ToLower()
                        Case "/?", "/help"
                            MsgBoxWrap(LocRM.GetString("Console_Help"), MsgBoxStyle.Information)
                            Return
                        Case "/wiareg", "/wr"
                            Try
                                manager = New DeviceManager
                            Catch ex As Exception
                                RegisterWiaautdll(True)
                            End Try
                            Return

                    End Select
                Next

                Dim settings As ScanSettings = My.Settings.LastScanSettings

                manager = New DeviceManager 'This is the first call to WIA library. If it isn't registered, an error is thrown

                For i As Integer = 0 To sArgs.GetUpperBound(0)
                    Select Case sArgs(i).ToLower()
                        Case "/register", "/reg"
                            Try
                                manager.RegisterPersistentEvent(Application.ExecutablePath + " /StiDevice:%1 /StiEvent:%2 /copy", "ScanExact", "Directly print using ScanExact", Application.ExecutablePath + ",0", WIA.EventID.wiaEventScanImage)
                                manager.RegisterPersistentEvent(Application.ExecutablePath + " /StiDevice:%1 /StiEvent:%2 /copy", "ScanExact", "Directly print using ScanExact", Application.ExecutablePath + ",0", WIA.EventID.wiaEventScanImage2)
                                manager.RegisterPersistentEvent(Application.ExecutablePath + " /StiDevice:%1 /StiEvent:%2 /file", "ScanExact", "Save to file using ScanExact", Application.ExecutablePath + ",0", WIA.EventID.wiaEventScanImage4)
                                MsgBoxWrap("ScanExact successfully registered to the scanner button. You may need to restart the computer in order for the change to take effect.", vbInformation, "ScanExact")
                            Catch ex As UnauthorizedAccessException
                                MsgBoxWrap("ScanExact must be executed with administrative privileges in order to complete the operation.", vbInformation, "ScanExact")
                            End Try
                            Return
                        Case "/unregister", "/unreg"
                            Try
                                manager.UnregisterPersistentEvent(Application.ExecutablePath + " /StiDevice:%1 /StiEvent:%2 /copy", "ScanExact", "Directly print using ScanExact", Application.ExecutablePath + ",0", WIA.EventID.wiaEventScanImage)
                                manager.UnregisterPersistentEvent(Application.ExecutablePath + " /StiDevice:%1 /StiEvent:%2 /copy", "ScanExact", "Directly print using ScanExact", Application.ExecutablePath + ",0", WIA.EventID.wiaEventScanImage2)
                                manager.UnregisterPersistentEvent(Application.ExecutablePath + " /StiDevice:%1 /StiEvent:%2 /file", "ScanExact", "Save to file using ScanExact", Application.ExecutablePath + ",0", WIA.EventID.wiaEventScanImage4)
                                MsgBoxWrap("ScanExact successfully unregistered from the scanner button. You may need to restart the computer in order for the change to take effect.", vbInformation, "ScanExact")
                            Catch ex As UnauthorizedAccessException
                                MsgBoxWrap("ScanExact must be executed with administrative privileges in order to complete the operation.", vbInformation, "ScanExact")
                            Catch ex As ArgumentException 'Thrown if the event is not found. Either wrong sintax or has already been removed
                                MsgBoxWrap("Couldn't find the correct entry in the registry. Maybe it has already been removed?")
                            End Try
                            Return
                    End Select
                Next

                'Command line arguments parsing
                'STEP 1 Parameters with an argument
                For i = 0 To sArgs.GetUpperBound(0)
                    Try
                        Select Case sArgs(i)
                            Case "/brightness", "/b"
                                settings.Brightness = CType(sArgs(i + 1), Integer)
                                Continue For
                            Case "/contrast", "/cnt"
                                settings.Contrast = CType(sArgs(i + 1), Integer)
                                Continue For
                            Case "/resolution", "/r"
                                settings.Resolution = CType(sArgs(i + 1), Integer)
                                Continue For
                            Case "/copies", "/nc"
                                settings.Copies = CType(sArgs(i + 1), Integer)
                                Continue For
                            Case "/scaling", "/s"
                                settings.Scaling = CType(sArgs(i + 1), Integer)
                                Continue For
                            Case "/printer"
                                Try
                                    _printer.Name = sArgs(i + 1)
                                Catch ex As ArgumentException
                                    MsgBoxWrap("The provided printer name is not valid. Using default printer.")
                                End Try
                                Continue For
                            Case "/path"
                                settings.Path = sArgs(i + 1)
                                Continue For
                        End Select
                    Catch ex As InvalidCastException
                        MsgBoxWrap("Command line parsing failed. See README for correct sintax")
                        Exit Sub
                    End Try

                    'STEP 2 Parameters without an argument
                    Select Case sArgs(i)
                        Case "/log"
                            Trace.Listeners.Add(New ConsoleTraceListener())
                            Continue For
                        Case "/color", "/colour", "/col"
                            settings.Intent = WiaImageIntent.ColorIntent
                            Continue For
                        Case "/grayscale", "/gray"
                            settings.Intent = WiaImageIntent.GrayscaleIntent
                            Continue For
                        Case "/text", "/bw"
                            settings.Intent = WiaImageIntent.TextIntent
                            Continue For
                        Case "/preview", "/p"
                            settings.Preview = True
                            Continue For
                        Case "/adf"
                            settings.UseADF = True
                            Continue For
                        Case "/duplex", "/d"
                            settings.Duplex = True
                            Continue For
                    End Select

                    If sArgs(i).StartsWith("/StiDevice:") Then
                        _deviceID = sArgs(i).Substring(sArgs(i).IndexOf(":") + 1)
                    End If
                    If sArgs(i).StartsWith("/StiEvent:") Then
                        'TODO: Implement
                    End If
                Next

                If _deviceID = "" Then
                    _deviceID = changescanner(My.Settings.DeviceID)
                Else
                    _deviceID = changescanner(_deviceID)
                End If

                _device = manager.DeviceInfos.Item(_deviceID).Connect()
                Trace.WriteLine(String.Format("DeviceID = {0}", _deviceID))
                _wscanner = _device.Items(1)

                'STEP 3 Scanner action parameters
                For i = 0 To sArgs.GetUpperBound(0)
                    Select Case sArgs(i).ToLower()
                        Case "/copy", "/c"
                            settings.ScanOutput = ScanOutput.Printer
                            Copy(settings)
                            Application.Exit()
                            Exit Sub
                        Case "/file", "/tofile", "/Scantofile", "/f"
                            settings.ScanOutput = ScanOutput.File
                            Try
                                Copy(settings)
                                Exit Sub
                            Catch ex As ArgumentException
                                MsgBoxWrap(ex.Message, vbExclamation, "ScanExact")
                            End Try
                        Case "/copymultiplepages"
                            settings.Multipage = True
                            settings.ScanOutput = ScanOutput.Printer
                            Copy(settings)
                            Exit Sub
                        Case "/pdf"
                            settings.ScanOutput = ScanOutput.PDF
                            Try
                                Copy(settings)
                                Exit Sub
                            Catch ex As ArgumentException
                                MsgBoxWrap(ex.Message, vbExclamation, "ScanExact")
                            End Try

                    End Select
                Next

                'If no action parameter is set, copy is the default
                Copy(settings)
            End If
        Catch ex As ExitException

        Catch ex As FileNotFoundException
            Trace.WriteLine("Need wiaaut registration")
            RegisterWiaautdll(False)
        Catch ex As Runtime.InteropServices.COMException
            If ex.ErrorCode = WIA_ERRORS.WIA_ERROR_NOT_REGISTERED Then 'WIA COM error
                Trace.WriteLine("Need wiaaut registration")
                RegisterWiaautdll(False)
            ElseIf ex.ErrorCode = WIA_ERRORS.WIA_ERROR_UNKNOWN_ERROR Or ex.ErrorCode = WIA_ERRORS.WIA_ERROR_NO_SCANNER_CONNECTED Then
                MsgBoxWrap("There is a problem with your scanner connection. Please try to reconnect your scanner and restart ScanExact. If this doesn't solve the problem, please report it on ScanExact website", MsgBoxStyle.Critical, "ScanExact")
            End If
#If DEBUG = False Then
        Catch ex As Exception
            HandleException(ex) 'Overrides .NET message box to include error reporting
#Else
            Throw
#End If
        End Try
        Trace.WriteLine(vbCrLf)
    End Sub

    Private Shared Sub HandleException(ByVal ex As Exception)

        Trace.TraceError("Exception caught.")
        Trace.TraceError(ex.ToString())

        'If the exception is unhandled, prepare error report and send info
        Dim sendReport As MsgBoxResult = MsgBoxWrap(String.Format(appControl.GetLocalizedString("Msg_SendErrorReport2"), ex.GetType().ToString() + " in " + ex.TargetSite.ToString()), MsgBoxStyle.Critical + MsgBoxStyle.OkCancel, "ScanExact")
        If sendReport = MsgBoxResult.Cancel Then
            Exit Sub
        End If

        Trace.Close()

        Dim rd As New StreamReader(Path.Combine(GetWritablePath(), "ScanExact.log"))
        Clipboard.SetText(rd.ReadToEnd())

        'Process.Start("https://sourceforge.net/tracker/?func=add&group_id=201245&atid=976783")
    End Sub

    Shared Function GetLocalizedString(ByVal Label As String) As String
        Dim LocalizedString As String
        LocalizedString = LocRM.GetString(Label)
        'Trace.WriteLine("Localized string: " & LocalizedString)
        Return LocalizedString
    End Function

    Private Shared Sub RegisterWiaautdll(ByVal suppressMessage As Boolean)

        'Check if ScanExact is run as administrator
        Dim isElevated As Boolean
        Dim identity As System.Security.Principal.WindowsIdentity = System.Security.Principal.WindowsIdentity.GetCurrent()
        Dim principal As New System.Security.Principal.WindowsPrincipal(identity)
        isElevated = principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator)

        If isElevated Then
            'Copy wiaaut.dll to system32
            Dim proceed As Boolean = True
            If Not suppressMessage Then
                If MsgBoxWrap(LocRM.GetString("Msg_WIAUnregistered"), MsgBoxStyle.OkCancel + MsgBoxStyle.Information, "ScanExact") = MsgBoxResult.Cancel Then proceed = False
            End If
            If proceed Then
                Try
                    System.IO.File.Copy("wiaaut.dll", "C:\WINDOWS\system32\wiaaut.dll", False)
                Catch authEx As UnauthorizedAccessException 'ScanExact has not administrator privileges
                    MsgBoxWrap(LocRM.GetString("Msg_UnauthorizedAccess"), MsgBoxStyle.Exclamation, "ScanExact")
                    Exit Sub
                Catch fileEx As IO.FileNotFoundException 'File is missing from ScanExact directory
                    MsgBoxWrap(LocRM.GetString("Msg_WiaautMissing"), MsgBoxStyle.Exclamation, "ScanExact")
                    Exit Sub
                Catch ex As IO.IOException
                    'The file is already in system32
                End Try

                'Start regsvr32 to register the dll
                Dim psi As System.Diagnostics.ProcessStartInfo = New ProcessStartInfo()
                psi.FileName = "regsvr32"
                psi.Arguments = "C:\WINDOWS\system32\wiaaut.dll /s"
                Process.Start(psi)

                MsgBoxWrap(LocRM.GetString("Msg_WIARegistrationSuccessful"), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "ScanExact")
            End If
        Else
            If Environment.OSVersion.Version.Major >= 6 Then
                Dim WIADialog As New WIARegisterDialog()
                Dim msg As Microsoft.WindowsAPICodePack.Dialogs.TaskDialogResult = WIADialog.Show(LocRM.GetString("Msg_WIAUnregistered"), LocRM.GetString("Msg_WIAUnregisteredInstruction"), "ScanExact", LocRM.GetString("Msg_WIAUnregisteredCancel"))
                If msg = Microsoft.WindowsAPICodePack.Dialogs.TaskDialogResult.Ok Then
                    Dim psi As System.Diagnostics.ProcessStartInfo = New ProcessStartInfo()
                    psi.FileName = Application.ExecutablePath
                    psi.Arguments = "/wiareg"
                    psi.Verb = "runas"
                    Process.Start(psi)
                    Exit Sub
                ElseIf msg = Microsoft.WindowsAPICodePack.Dialogs.TaskDialogResult.Cancel Then
                    Exit Sub
                End If
            Else
                Dim msg As MsgBoxResult = MsgBoxWrap(LocRM.GetString("Msg_WIAUnregistered"), MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "ScanExact")
                If msg = MsgBoxResult.Yes Then
                    Dim psi As System.Diagnostics.ProcessStartInfo = New ProcessStartInfo()
                    psi.FileName = Application.ExecutablePath
                    psi.Arguments = "/wiareg"
                    psi.Verb = "runas"
                    Process.Start(psi)
                    Exit Sub
                ElseIf msg = MsgBoxResult.No Then
                    Exit Sub
                End If
            End If
        End If
    End Sub

    Shared Sub GetAvailableLanguages()

        HasThreadFinished = False
        ReDim _availableCultures(-1)
        'Need to find a faster way
        Dim resSet As Resources.ResourceSet
        For Each cult As CultureInfo In CultureInfo.GetCultures(CultureTypes.SpecificCultures) 'CHANGE FOR .NET 4.0
            If Not cult.IsNeutralCulture Then 'Excludes neutral languages
                resSet = LocRM.GetResourceSet(cult, True, False) 'Verify if resources for that culture are present
                If Not resSet Is Nothing Then
                    If Not cult.LCID = 127 Then 'Excludes standard language
                        ReDim Preserve _availableCultures(_availableCultures.GetUpperBound(0) + 1)
                        _availableCultures(_availableCultures.GetUpperBound(0)) = cult
                    End If
                End If
            End If
        Next
        Trace.IndentLevel = 0
        Trace.WriteLine("Available languages on this computer.")
        Trace.WriteLine("-------------------------------------")
        For Each lang In _availableCultures
            Trace.WriteLine("Available language: " & lang.Name)
        Next
        Trace.WriteLine("-------------------------------------")
        HasThreadFinished = True
    End Sub

    Shared Function changescanner(Optional ByVal deviceID As String = "") As String
        Try
            If deviceID = "" Then
                'Shows WIA scanner selection dialog
                Dim dialog As New CommonDialog
                _scanner = New Scanner(dialog.ShowSelectDevice(WiaDeviceType.ScannerDeviceType, True, True).DeviceID)
                Return _scanner.DeviceId
            Else
                _scanner = New Scanner(deviceID)
                Return _scanner.DeviceId
            End If

            '**************Exception handling*************
        Catch ex As ArgumentException
            Dim msg As MsgBoxResult = MsgBoxWrap(LocRM.GetString("Msg_NoScannerConnected"), MsgBoxStyle.RetryCancel + MsgBoxStyle.Information, "ScanExact")
            If msg = MsgBoxResult.Retry Then
                Return changescanner(deviceID)
            Else
                Throw New ExitException
            End If

        Catch ex As Runtime.InteropServices.COMException

            Select Case ex.ErrorCode
                Case WIA_ERRORS.WIA_ERROR_NO_SCANNER_SELECTED 'No scanner is selected
                    Return Nothing

                Case WIA_ERRORS.WIA_ERROR_NO_SCANNER_CONNECTED 'No scanner is connected
                    Dim msg As MsgBoxResult = MsgBoxWrap(LocRM.GetString("Msg_NoScannerConnected"), MsgBoxStyle.RetryCancel + MsgBoxStyle.Information, "ScanExact")
                    If msg = MsgBoxResult.Retry Then
                        Return changescanner(deviceID)
                    Else
                        Throw New ExitException
                    End If

                Case WIA_ERRORS.WIA_ERROR_CONNECTION_ERROR  'Can't establish connection with the scanner
                    Dim msg As MsgBoxResult = MsgBoxWrap(LocRM.GetString("Msg_ConnectionError"), MsgBoxStyle.RetryCancel + MsgBoxStyle.Exclamation, "ScanExact")
                    If msg = MsgBoxResult.Retry Then
                        Return changescanner(deviceID)
                    Else
                        Throw New ExitException
                    End If

                Case WIA_ERRORS.WIA_ERROR_OFFLINE
                    Dim msg As MsgBoxResult = MsgBoxWrap(LocRM.GetString("Msg_ScannerOffline"), MsgBoxStyle.RetryCancel + MsgBoxStyle.Information, "ScanExact")
                    If msg = MsgBoxResult.Retry Then
                        Return changescanner(deviceID)
                    Else
                        Throw New ExitException
                    End If

                Case WIA_ERRORS.WIA_ERROR_EXCEPTION_IN_DRIVER
                    MsgBoxWrap(My.Resources.WinFormStrings.Msg_DriverException, MsgBoxStyle.Critical, "ScanExact")
                    Throw New ExitException

                Case WIA_ERRORS.WIA_ERROR_BUSY 'Scanner in use
                    Dim msg As MsgBoxResult = MsgBoxWrap(LocRM.GetString("Msg_ScannerInUse"), MsgBoxStyle.Exclamation + MsgBoxStyle.RetryCancel)
                    If msg = MsgBoxResult.Retry Then
                        Return changescanner(deviceID)
                    Else
                        Throw New ExitException
                    End If
                Case WIA_ERRORS.WIA_ERROR_UNKNOWN_ERROR 'Could happen if the deviceID is invalid (e.g. changed scanner)
                    Return changescanner()

                Case Else
                    Throw
            End Select
        End Try

        Return Nothing
    End Function

    Shared Sub CreateScanner(Optional ByVal deviceID As String = Nothing)
retry:
        If deviceID = Nothing Or deviceID = "" Then
            Dim newscannerID As String = changescanner()
            If newscannerID Is Nothing Then
                Dim msg As MsgBoxResult = MsgBoxWrap(LocRM.GetString("Msg_ChooseAScanner"), MsgBoxStyle.YesNo + MsgBoxStyle.Information, "ScanExact")
                If msg = MsgBoxResult.Yes Then
                    GoTo retry
                Else
                    Throw New ExitException
                End If
            End If

        Else
            If changescanner(deviceID) Is Nothing Then
                Throw New ExitException
            End If
        End If

        Scanner.WritePropertiesLog()
    End Sub

    Shared Function GetAvailableResolutions() As List(Of Integer)
        Return _scanner.AvailableResolutions
    End Function

    Shared Function CanUseADF() As Boolean
        If Not _scanner Is Nothing Then
            Return _scanner.CanUseADF
        Else Return False
        End If
    End Function

    Shared Function CanDoDuplex() As Boolean
        If Not _scanner Is Nothing Then
            Return _scanner.CanDoDuplex
        Else
            Return False
        End If
    End Function

    Private Shared Sub OpenFile(ByVal path As String)
        Try
            Process.Start(path)
        Catch ex As Exception
            MsgBoxWrap(String.Format(LocRM.GetString("Msg_CantOpenfile"), ex.Message), MsgBoxStyle.Exclamation)
        End Try
    End Sub

    Shared Sub Copy(ByVal options As ScanSettings)
        Dim pathWoExt As String = ""
        Dim ext As String = ""
        Dim format As Imaging.ImageFormat = ImageFormat.Jpeg


        If options.ScanOutput = ScanOutput.File Or options.ScanOutput = ScanOutput.PDF Then
            If options.Path = "" Then
                Dim dialog As New SaveFileDialog()

                If options.ScanOutput = ScanOutput.File Then
                    dialog.AddExtension = True
                    dialog.DefaultExt = "jpg"
                    dialog.Filter = "JPEG image|*.jpg|Windows Bitmap|*.bmp|Compuserve GIF|*.gif|Portable Network Graphics (PNG)|*.png"
                Else
                    dialog.AddExtension = True
                    dialog.DefaultExt = "pdf"
                    dialog.Filter = "Adobe PDF file|*.pdf"
                End If

                If Not dialog.ShowDialog() = Windows.Forms.DialogResult.Cancel Then
                    options.Path = dialog.FileName
                Else : Exit Sub
                End If
            Else

                'Check if the provided path is a directory
                If Path.GetFileName(options.Path) = "" Then
                    'In this case, create the file name according to the current date and time
                    Dim filename As String = "ScanExact " + Date.Now.ToString("yyyy-MM-dd hh-mm")
                    If options.ScanOutput = ScanOutput.PDF Then
                        filename += ".pdf"
                    Else
                        filename += ".jpg"
                    End If

                    options.Path = Path.Combine(options.Path, filename)
                End If

                'Check if the path points to an existing file or an unwritable location. In that case throws an error message
                If File.Exists(options.Path) Then
                    Dim msgboxres As MsgBoxResult = MsgBox(String.Format(LocRM.GetString("Msg_FileAlreadyExists"), options.Path), MsgBoxStyle.OkCancel + MsgBoxStyle.Question, "ScanExact")
                    If msgboxres = MsgBoxResult.Ok Then
                        Try
                            File.Delete(options.Path)
                        Catch ex As Exception
                            MsgBoxWrap(String.Format(LocRM.GetString("Msg_FileError"), options.Path), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "ScanExact")
                            Exit Sub
                        End Try
                    Else
                        Exit Sub
                    End If
                End If
            End If

            'Check if the provided path is valid (AUTHORIZATION, SYNTAX, ecc)
            Try
                Dim a As FileStream = IO.File.Create(options.Path)
                a.Close()
            Catch ex As Exception
                MsgBoxWrap(String.Format(LocRM.GetString("Msg_FileError"), options.Path), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "ScanExact")
                Try
                    IO.File.Delete(options.Path)
                Catch e As Exception

                End Try
                Exit Sub
            End Try

            Try 'Delete the temporary file created.
                IO.File.Delete(options.Path)
            Catch e As Exception

            End Try

            If options.ScanOutput = ScanOutput.File Then
                'Determines the extension of the file in order to use the correct format
                ext = Right(options.Path, 3)
                pathWoExt = Left(options.Path, options.Path.Length - 4)
                Select Case ext
                    Case "jpg"
                        format = ImageFormat.Jpeg
                    Case "bmp"
                        format = ImageFormat.Bmp
                    Case "gif"
                        format = ImageFormat.Gif
                    Case "png"
                        format = ImageFormat.Png
                    Case Else
                        Throw New ArgumentException("File extension isn't a valid extension")
                End Select

            End If
        End If

        Dim morePages As DialogResult = DialogResult.No
        'List of acquired images
        Dim images As New List(Of String)()

        Dim dlg As New dlgScanMorePages
        If CommandLine Then
            dlg.Location = New Point(Screen.PrimaryScreen.WorkingArea.Width / 2 - dlg.Width, Screen.PrimaryScreen.WorkingArea.Height / 2 - dlg.Height)
        Else
            dlg.Location = New Point(MainForm.Left + (MainForm.Width - dlg.Width) / 2, MainForm.Top + (MainForm.Height - dlg.Height) / 2)
        End If

        changescanner(_scanner.DeviceId)

        'Calls scan routine
        Do
            Try
                'Add the image to the printer buffer
                images.AddRange(_scanner.ScanADF(options))

                If options.Multipage Then morePages = dlg.ShowDialog(images.Count)

            Catch ex As System.Runtime.InteropServices.COMException
                If ex.ErrorCode = -2145320860 Then       'If acquisition is cancelled
                    Exit Sub
                ElseIf ex.ErrorCode = WIA_ERRORS.WIA_ERROR_WARMING_UP Then
                    MsgBoxWrap(LocRM.GetString("Msg_ScannerWarmingUp"), MsgBoxStyle.Exclamation, "ScanExact")
                    Return
                ElseIf ex.ErrorCode = WIA_ERRORS.WIA_ERROR_EXCEPTION_IN_DRIVER Then
                    MsgBoxWrap(LocRM.GetString("Msg_ExceptionInDriver"), MsgBoxStyle.Exclamation, "ScanExact")
                    Return
                ElseIf ex.ErrorCode = Convert.ToInt32("0x80004005", 16) Then
                    MsgBoxWrap("An error occured while processing the acquired image. Please try again with a lower resolution." & vbCrLf & "If the problem persists please report it.", MsgBoxStyle.Critical, "ScanExact")
                    Exit Sub
                Else
                    Throw
                End If
            End Try

            If morePages = DialogResult.Cancel Then
                'If the process is canceled by closing the dialog
                Dim res As MsgBoxResult = MsgBoxWrap(LocRM.GetString("Msg_CancelAcquisition"), MsgBoxStyle.Question + MsgBoxStyle.YesNo, "ScanExact")
                If res = MsgBoxResult.Yes Then
                    images.Clear()
                    Exit Do

                Else
                    Continue Do
                End If

            End If
        Loop While morePages = DialogResult.Yes

        If images.Count = 0 Then Exit Sub 'The user canceled the acquisition

        ScannedFilesForImportToExact.Clear() 'Clear list of files which will be used for import to Exact

        'Process the images to the desired output
        Select Case options.ScanOutput

            Case ScanOutput.File
                If images.Count = 1 Then
                    Dim img As Image = Image.FromFile(images(0))
                    If format Is ImageFormat.Jpeg Then
                        Dim jgpEncoder As ImageCodecInfo = GetEncoder(ImageFormat.Jpeg)

                        ' Create an Encoder object based on the GUID
                        ' for the Quality parameter category.
                        Dim myEncoder As System.Drawing.Imaging.Encoder = System.Drawing.Imaging.Encoder.Quality

                        ' Create an EncoderParameters object.
                        ' An EncoderParameters object has an array of EncoderParameter
                        ' objects. In this case, there is only one
                        ' EncoderParameter object in the array.
                        Dim myEncoderParameters As New EncoderParameters(1)

                        Dim myEncoderParameter As New EncoderParameter(myEncoder, options.Quality)
                        myEncoderParameters.Param(0) = myEncoderParameter
                        img.Save(options.Path, jgpEncoder, myEncoderParameters)
                    Else
                        img.Save(options.Path, format)
                    End If
                    ScannedFilesForImportToExact.Add(options.Path)
                    img.Dispose()
                    File.Delete(images(0))

                ElseIf images.Count > 1 Then
                    'If more than one page is acquired, we need to rename the files with a counter
                    For i = 0 To images.Count - 1
                        'Save the images with the pattern "filename 000.ext"
                        Dim path As String = pathWoExt + i.ToString(" 000") + "." + ext
                        Dim img As Image = Image.FromFile(images(i))

                        If format Is ImageFormat.Jpeg Then
                            Dim jgpEncoder As ImageCodecInfo = GetEncoder(ImageFormat.Jpeg)

                            ' Create an Encoder object based on the GUID
                            ' for the Quality parameter category.
                            Dim myEncoder As System.Drawing.Imaging.Encoder = System.Drawing.Imaging.Encoder.Quality

                            ' Create an EncoderParameters object.
                            ' An EncoderParameters object has an array of EncoderParameter
                            ' objects. In this case, there is only one
                            ' EncoderParameter object in the array.
                            Dim myEncoderParameters As New EncoderParameters(1)

                            Dim myEncoderParameter As New EncoderParameter(myEncoder, options.Quality)
                            myEncoderParameters.Param(0) = myEncoderParameter
                            img.Save(path, jgpEncoder, myEncoderParameters)
                        Else
                            img.Save(path, format)
                        End If
                        ScannedFilesForImportToExact.Add(path)
                        img.Dispose()
                        File.Delete(images(i))
                    Next
                End If

                If My.Settings.FileOpenAfterAcquisition Then
                    OpenFile(options.Path)
                End If

            Case ScanOutput.PDF
                Dim doc As New PdfWriter.PDFDocument()
                For i = 0 To images.Count - 1
                    Dim img As Image = Image.FromFile(images(i))
                    doc.AddPage(img, options.PaperSize, options.Scaling, options.Center)
                Next

                doc.Save(options.Path)
                ScannedFilesForImportToExact.Add(options.Path)
                doc.Close()

                For i = 0 To images.Count - 1
                    File.Delete(images(i))
                Next

                If My.Settings.PDFOpenAfterAcquisition Then
                    OpenFile(options.Path)
                End If

            Case Else
                _printer.AddImages(images, options.Scaling, options.Center)
                'Prints images
                Try
                    _printer.Print(options.Copies)
                Catch ex As ArgumentException 'If no images were sent to the printer

                End Try
        End Select
    End Sub

    Private Shared Function GetEncoder(ByVal format As ImageFormat) As ImageCodecInfo

        Dim codecs As ImageCodecInfo() = ImageCodecInfo.GetImageDecoders()

        Dim codec As ImageCodecInfo
        For Each codec In codecs
            If codec.FormatID = format.Guid Then
                Return codec
            End If
        Next codec
        Return Nothing

    End Function

    Shared Function GetScannerEvents() As WIA.DeviceEvents
        Return _scanner.Events
    End Function

    Shared ReadOnly Property ScannerDescription() As String
        Get
            If Not _scanner Is Nothing Then
                Return _scanner.Description
            Else
                Return "No Scanner"
            End If

        End Get
    End Property

    Shared ReadOnly Property Scanner() As Scanner
        Get
            Return _scanner
        End Get
    End Property

    Shared ReadOnly Property Printer() As Printer
        Get
            Return _printer
        End Get
    End Property

    Shared ReadOnly Property AvailableLanguages() As Globalization.CultureInfo()
        Get
            If HasThreadFinished = False Then
                Throw New Threading.ThreadStateException("Thread has not yet finished")
            End If
            Return _availableCultures
        End Get
    End Property

End Class