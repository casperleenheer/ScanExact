Imports WIA
Imports System.Drawing.Printing
Imports System.Threading
Imports System.ComponentModel
Imports System.Text

Class mainFrm
    Public Shared frmImageSettings As frmImageSettings
    Public Shared AboutBox As AboutBox
    Public Shared frmOptions As SettingsDialog
    Dim splash As SplashScreen
    Dim SplashThread As Threading.Thread

    Dim intent As WiaImageIntent = My.Settings.LastScanSettings.Intent

    Private VersionCheckThread As New Threading.Thread(AddressOf VersionCheck)
    Dim weburl As String
    Dim LocalizedRootStr As String

    Dim bw As BackgroundWorker
    Dim ExactInfo As ExactInfo
    Dim data As New Exact.Services.Client.Data.EntityData
    Dim result As DialogResult

    Sub VersionCheck()
        Dim reader As Xml.XmlTextReader
        Dim curVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version
        Dim newVersion = curVersion
        Try
            reader = New Xml.XmlTextReader(My.Resources.VersionCheckURL)
            reader.MoveToContent()
            If reader.NodeType = Xml.XmlNodeType.Element And reader.Name = Application.ProductName Then
                Dim elementName As String = ""
                While reader.Read()
                    If reader.NodeType = Xml.XmlNodeType.Element Then
                        elementName = reader.Name
                    ElseIf reader.NodeType = Xml.XmlNodeType.Text And reader.HasValue Then
                        Select Case elementName
                            Case "version"
                                newVersion = New Version(reader.Value)
                            Case "url"
                                weburl = reader.Value
                        End Select
                    End If
                End While
            End If
            If curVersion.CompareTo(newVersion) < 0 Then
                VersionStatusLabel.Visible = True
            End If
            My.Settings.LastVersionCheck = Today
        Catch ex As Exception 'File is not available, or internet access missing. Just die without any output
            Exit Sub
        End Try
    End Sub

    Sub LoadSettings()

        AboutBox = New AboutBox()
        frmOptions = New SettingsDialog()
        frmImageSettings = New frmImageSettings()

        If My.Settings.CheckForUpdates Then
            'VersionCheckThread.Start() 'Version check
        End If
        'Loads form location if storelocation is true
        If My.Settings.StoreLocation Then
            Me.Location = My.Settings.Location
        Else
            Me.Location = New Point((Screen.GetBounds(Me).Width - Me.Width) / 2, (Screen.GetBounds(Me).Height - Me.Height) / 2)
        End If

        btnCopy.Image = My.Resources.scanner1
        Me.Icon = My.Resources.scanner_3

        'Set frmImageSettings as child
        Me.AddOwnedForm(frmImageSettings)

        'Applies localized strings to the controls
        For Each control As System.Windows.Forms.Control In Panel1.Controls
            Dim text As String = appControl.GetLocalizedString(LocalizedRootStr & control.Name)
            If text <> "" Then control.Text = text
            ToolTip1.SetToolTip(control, appControl.GetLocalizedString(LocalizedRootStr & control.Name & "ToolTip"))
        Next

        For Each control As System.Windows.Forms.Control In gpbOrder.Controls
            Dim text As String = appControl.GetLocalizedString(LocalizedRootStr & control.Name)
            If text <> "" Then control.Text = text
            ToolTip1.SetToolTip(control, appControl.GetLocalizedString(LocalizedRootStr & control.Name & "ToolTip"))
        Next

        Dim mText As String = appControl.GetLocalizedString(LocalizedRootStr & gpbOrder.Text)
        gpbOrder.Text = mText

        mText = appControl.GetLocalizedString(LocalizedRootStr & ExactStatusLabel.Text)
        ExactStatusLabel.Text = mText


        For Each control As System.Windows.Forms.Control In GroupBox3.Controls
            Dim text As String = appControl.GetLocalizedString(LocalizedRootStr & control.Name)
            If text <> "" Then control.Text = text
            ToolTip1.SetToolTip(control, appControl.GetLocalizedString(LocalizedRootStr & control.Name & "ToolTip"))
        Next

        'Applies localized strings to the menustrip
        For Each strip As ToolStripItem In ScanMenuStrip.Items
            strip.Text = appControl.GetLocalizedString(LocalizedRootStr & strip.Name)
        Next

        'Populates comboboxes
        For i As Integer = 0 To 2
            cboScanMode.Items.Add(appControl.GetLocalizedString(LocalizedRootStr & "cboScanModeItem" & i))
        Next

        For i As Integer = 0 To 1
            cboPrintMode.Items.Add(appControl.GetLocalizedString(LocalizedRootStr & "cboPrintModeItem" & i))
        Next

        'Sets default copies number
        nudNCopie.Controls(1).Text = "1"

        'Loads default printer
        Try
            If IsServiceRunning("Print Spooler") Then
                appControl.Printer.Name = My.Settings.DefaultPrinter
            Else
                MsgBoxWrap("Print Spooler Service has stopped, program will be terminated!")
                End
            End If
        Catch ex As ArgumentException
            If ex.Message = "Printer name is not valid" Then
                Dim sets As New PrinterSettings()
                appControl.Printer.Name = sets.PrinterName
            End If
        End Try
        My.Settings.DefaultPrinter = appControl.Printer.Name
        PrinterStatusLabel.Text = appControl.Printer.Name

        'Statusbar labels
        ScannerStatusLabel.Image = My.Resources.scanner
        ScannerStatusLabel.Text = appControl.ScannerDescription
        PrinterStatusLabel.Image = My.Resources.printer
        If PrinterStatusLabel.Text.Contains("PDF") Then
            PrinterStatusLabel.Image = My.Resources.pdf_icon
        Else
            PrinterStatusLabel.Image = My.Resources.printer
        End If

        'Loads saved intent setting
        If My.Settings.LastScanSettings.Intent = 4 Or My.Settings.LastScanSettings.Intent = 0 Then
            cboScanMode.SelectedIndex = 2
        Else
            cboScanMode.SelectedIndex = My.Settings.LastScanSettings.Intent - 1
        End If

        'Populates paper sizes combo box
        cboPaperSize.DisplayMember = "PaperName" 'Links 
        For Each pSize As PaperSize In appControl.Printer.PrinterSettings.PaperSizes
            If pSize.Kind <> PaperKind.Custom Then
                cboPaperSize.Items.Add(pSize)
            End If
        Next

        cboPaperSize.Text = My.Settings.PrinterSize 'Sets default paper size as stored in settings
        chkADF.Enabled = appControl.CanUseADF()
        chkDuplex.Enabled = chkADF.Checked And appControl.CanDoDuplex()

        chkExact.Checked = (My.Settings.LastScanSettings.ExportToExact)
        chkADF.Checked = My.Settings.LastScanSettings.UseADF
        chkDuplex.Checked = My.Settings.LastScanSettings.Duplex
        chkPDF.Checked = (My.Settings.LastScanSettings.ScanOutput = ScanOutput.PDF)
        chkMultipage.Checked = My.Settings.LastScanSettings.Multipage
        chkSaveToFile.Checked = (My.Settings.LastScanSettings.ScanOutput = ScanOutput.File)

        'Initialize administrations
        If Not My.Settings.Administrations.Trim = "" Then
            AdministrationNumbers = Administrations_split(My.Settings.Administrations)
        End If

        'Setup exact database connections
        Dim setupEntityClient As clsExact_Connection
        Dim setupEntitiesClient As clsExact_EntitiesConnection
        Try
            For Each Admin In AdministrationNumbers
                setupEntityClient = New clsExact_Connection(Admin, My.Settings.ServiceURI.Trim, My.Settings.SQLserver.Trim)
                setupEntitiesClient = New clsExact_EntitiesConnection(Admin, My.Settings.ServiceURI.Trim, My.Settings.SQLserver.Trim)
                EntityConnection.Add(setupEntityClient)
                setupEntityClient.SetupConnection()
                EntitiesConnection.Add(setupEntitiesClient)
                setupEntitiesClient.SetupConnection()
            Next
        Catch ex As Exception
            Trace.WriteLine(ex.Message)
        End Try

        'Load combobox documenttype from Exact
        FillDocumentTypeComboBox()

    End Sub

    Private Sub Hotkeys(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        'Shortcuts
        If e.Control Then 'If CTRL is pressed
            Dim ea As New EventArgs()
            Select Case e.KeyCode
                Case Keys.S 'Copy
                    btnCopy_Click(btnCopy, ea)
                Case Keys.M 'Copy Multiple Pages
                    chkMultipage.Checked = Not chkMultipage.Checked
                Case Keys.F 'Scan to File
                    chkSaveToFile.Checked = Not chkSaveToFile.Checked
                Case Keys.I 'Image settings
                    btnImageSettings_Click(btnImageSettings, ea)
                Case Keys.P 'Scan to PDF
                    chkPDF.Checked = Not chkPDF.Checked
                Case Keys.E 'Convert to ExactGlobe DB
                    chkExact.Checked = Not chkExact.Checked
            End Select
        End If
    End Sub

    Private Sub StartSplash()
        splash = New SplashScreen()
        Application.Run(splash)
    End Sub

    Private Sub mainFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If My.Settings.CustomCulture And My.Settings.Culture IsNot Nothing Then Threading.Thread.CurrentThread.CurrentUICulture = My.Settings.Culture

        Trace.WriteLine("Current Culture : " & Threading.Thread.CurrentThread.CurrentUICulture.Name)
        My.Application.ChangeUICulture(Threading.Thread.CurrentThread.CurrentUICulture.Name)
        Me.BringToFront()
        Me.Focus()

        LocalizedRootStr = Me.Name & "_"
        SplashThread = New Threading.Thread(AddressOf StartSplash)
        SplashThread.Start()

        'Adjust window height for Exact Import options
        Me.Height = Me.Height - 134
        LoadSettings() 'Loads stored settings

        If Me.chkExact.CheckState = CheckState.Checked Then
            btnCopy.Enabled = False
        End If

        If SplashThread.IsAlive Then splash.Invoke(New EventHandler(AddressOf splash.KillMe))
        splash.Dispose()
        splash = Nothing
        SplashThread = Nothing

        'Backgroundworker doing some runs to the Exact database
        bw = New BackgroundWorker
        AddHandler bw.DoWork, AddressOf bw_doWork
        AddHandler bw.ProgressChanged, AddressOf bw_progresschanged
        AddHandler bw.RunWorkerCompleted, AddressOf bw_RunWorkerCompleted
        bw.WorkerReportsProgress = True
        bw.WorkerSupportsCancellation = True

        Me.BringToFront()
        Me.Focus()
    End Sub

    Private Sub mainFrm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        'Stores form location
        If My.Settings.StoreLocation Then My.Settings.Location = Me.Location
        'Gets the last used settings and saves them
        My.Settings.LastScanSettings = getScanSettings()
    End Sub

    Private Sub mainFrm_Move(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Move
        If frmImageSettings IsNot Nothing Then
            If frmImageSettings.Visible Then 'Moves the image settings form with main form
                Dim tempLocation As New Point(Me.Location.X + Me.Size.Width, Me.Location.Y)
                If tempLocation.X + frmImageSettings.Width >= Screen.PrimaryScreen.WorkingArea.Width Then
                    frmImageSettings.Location = New Point(Me.Location.X - frmImageSettings.Width, Me.Location.Y)
                Else
                    frmImageSettings.Location = tempLocation
                End If
            End If
        End If
    End Sub

    Private Sub btnCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy.Click
        Me.Enabled = False
        'Starts copy process
        Dim settings As ScanSettings = getScanSettings()

        If chkExact.CheckState = CheckState.Checked Then
            'If Not bw Is Nothing Then bw = New BackgroundWorker
            If bw.IsBusy <> True Then
                ' Start the asynchronous operation.
                bw.RunWorkerAsync()
            End If
        Else
            appControl.Copy(settings)
        End If

        Me.Enabled = True
    End Sub

    Private Sub SelScanner_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ScannerStatusLabel.Click, btnSelScanner.Click
        Try 'Tries changing the scanner
            Dim newscannerID As String = appControl.changescanner()
            If newscannerID Is Nothing Then Exit Sub

            If My.Settings.DeviceID <> newscannerID Then
                My.Settings.DeviceID = newscannerID 'if a deviceId is returned, store it
            End If

            ScannerStatusLabel.Text = appControl.ScannerDescription
            chkADF.Enabled = appControl.CanUseADF()
            If frmImageSettings.Visible Then btnImageSettings.PerformClick()
            frmImageSettings.Dispose()
            frmImageSettings = New frmImageSettings()
        Catch ex As ExitException
            'Don't change the scanner
        Catch ex As NullReferenceException

        Catch ex As Exception
            Throw
        End Try
    End Sub

    'Show printer settings
    Private Sub PrintSetup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintSetup.Click, PrinterStatusLabel.Click
        'Disable printer settings when chkPDF is selected
        If Not chkPDF.Checked Then
            appControl.Printer.showPreferences()
            cboPaperSize.Items.Clear()
            'Update paper sizes combo box
            cboPaperSize.DisplayMember = "PaperName" 'Links 
            For Each pSize As PaperSize In appControl.Printer.PrinterSettings.PaperSizes
                If pSize.Kind <> PaperKind.Custom Then
                    cboPaperSize.Items.Add(pSize)
                End If
            Next
            cboPaperSize.SelectedIndex = 0

            My.Settings.DefaultPrinter = appControl.Printer.Name
            PrinterStatusLabel.Text = appControl.Printer.Name
            If PrinterStatusLabel.Text.Contains("PDF") Then
                PrinterStatusLabel.Image = My.Resources.pdf_icon
            Else
                PrinterStatusLabel.Image = My.Resources.printer
            End If
        End If
    End Sub

    Private Sub cboScanMode_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboScanMode.SelectedIndexChanged
        'Changes scanning intent and print mode
        Select Case cboScanMode.SelectedIndex
            Case 0
                intent = WiaImageIntent.ColorIntent
                cboPrintMode.SelectedIndex = 0
            Case 1
                intent = WiaImageIntent.GrayscaleIntent
                cboPrintMode.SelectedIndex = 1
            Case 2
                intent = WiaImageIntent.TextIntent
                cboPrintMode.SelectedIndex = 1
        End Select
    End Sub

    Private Sub cboPrintMode_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboPrintMode.SelectedIndexChanged
        'Changes print mode
        appControl.Printer.PageSettings.Color = Not CBool(cboPrintMode.SelectedIndex) 'If index is 0, returns true
        My.Settings.PrintColor = appControl.Printer.PageSettings.Color
    End Sub


    Private Sub btnImageSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImageSettings.Click

        'Shows / hides image settings form in the correct position
        If frmImageSettings.Visible = False Then
            Dim tempLocation As New Point(Me.Location.X + Me.Size.Width, Me.Location.Y)
            If tempLocation.X + frmImageSettings.Width >= Screen.PrimaryScreen.WorkingArea.Width Then
                frmImageSettings.Location = New Point(Me.Location.X - frmImageSettings.Width, Me.Location.Y)
            Else
                frmImageSettings.Location = tempLocation
            End If

            frmImageSettings.Show()
            btnImageSettings.Text = appControl.GetLocalizedString(LocalizedRootStr & "btnImageSettingsHide")
        Else
            frmImageSettings.Hide()
            btnImageSettings.Text = appControl.GetLocalizedString(LocalizedRootStr & "btnImageSettings")
        End If
    End Sub

    Private Sub llblSettings_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llblSettings.LinkClicked
        frmOptions.ShowDialog()
        frmImageSettings.RefreshSettings()
        FillDocumentTypeComboBox()
    End Sub

    Private Sub llblAbout_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
        AboutBox.Show()
    End Sub

    Private Function getScanSettings() As ScanSettings
        Dim opts As New ScanSettings
        Try
            opts.Resolution = Convert.ToInt32(frmImageSettings.cboResolution.Text, Globalization.CultureInfo.InvariantCulture)
        Catch ex As FormatException 'Fixes a bug
            If My.Settings.LastScanSettings.Resolution <> 0 Or (Not Nothing) Then opts.Resolution = My.Settings.LastScanSettings.Resolution
        End Try

        opts.Brightness = frmImageSettings.tbBrightness.Value
        opts.Contrast = frmImageSettings.tbContrast.Value
        opts.Intent = intent
        opts.Preview = chkPreview.Checked
        opts.Quality = frmImageSettings.tbCompression.Value
        opts.Copies = nudNCopie.Value
        opts.Scaling = frmImageSettings.tbScaling.Value
        opts.BitDepth = My.Settings.LastScanSettings.BitDepth
        opts.UseADF = chkADF.Checked
        opts.ExportToExact = chkExact.Checked
        opts.Duplex = chkDuplex.Checked
        opts.Multipage = chkMultipage.Checked
        opts.ExportToExact = chkExact.Checked
        opts.Administrations = My.Settings.Administrations
        opts.SQLServer = My.Settings.SQLserver
        opts.ServiceURI = My.Settings.ServiceURI

        opts.Center = frmImageSettings.chkCenter.Checked

        Try 'Fix for https://sourceforge.net/p/icopy/bugs/239/
            opts.PaperSize = appControl.Printer.PrinterSettings.PaperSizes.Item(cboPaperSize.SelectedIndex)
        Catch ex As IndexOutOfRangeException
            opts.PaperSize = appControl.Printer.PrinterSettings.PaperSizes.Item(0)
        End Try

        If chkSaveToFile.Checked Then
            opts.ScanOutput = ScanOutput.File
            If Not My.Settings.FileAskWhereToSave Then
                opts.Path = My.Settings.FileSavePath
            End If
        ElseIf chkPDF.Checked Then
            opts.ScanOutput = ScanOutput.PDF
            If Not My.Settings.PDFAskWhereToSave Then
                opts.Path = My.Settings.PDFSavePath
            End If
        End If

        My.Settings.LastScanSettings = opts
        Return opts
    End Function

    Private Sub cboPaperSize_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboPaperSize.SelectedIndexChanged
        appControl.Printer.PageSettings.PaperSize = appControl.Printer.PrinterSettings.PaperSizes.Item(cboPaperSize.SelectedIndex)
        My.Settings.PrinterSize = cboPaperSize.Text 'Stores value in settings
    End Sub

    Private Sub VersionStatusLabel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VersionStatusLabel.Click
        Process.Start(weburl)
    End Sub

    Private Sub chkADF_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkADF.CheckedChanged
        chkDuplex.Enabled = chkADF.Checked And appControl.CanDoDuplex()

        If chkADF.Checked Then
            chkPreview.Enabled = False
            chkPreview.Checked = False
        Else
            chkPreview.Enabled = True
            chkDuplex.Checked = False
        End If
    End Sub

    Private Sub Outputchanged(sender As System.Object, e As System.EventArgs) Handles chkSaveToFile.CheckedChanged, chkPDF.CheckedChanged
        If sender Is chkPDF Then
            chkSaveToFile.Checked = chkSaveToFile.Checked And (Not chkPDF.Checked)
        Else
            chkPDF.Checked = chkPDF.Checked And (Not chkSaveToFile.Checked)
        End If

        If chkPDF.Checked Then
            chkSaveToFile.Checked = False
            PrinterStatusLabel.Image = My.Resources.pdf_icon
            PrinterStatusLabel.Text = lblSaveAsPdf.Text
        ElseIf chkSaveToFile.Checked Then
            chkPDF.Checked = False
            PrinterStatusLabel.Image = My.Resources.saveToFile
            PrinterStatusLabel.Text = lblSaveToFile.Text
        Else
            PrinterStatusLabel.Text = appControl.Printer.Name
            If PrinterStatusLabel.Text.Contains("PDF") Then
                PrinterStatusLabel.Image = My.Resources.pdf_icon
            Else
                PrinterStatusLabel.Image = My.Resources.printer
            End If
            cboPaperSize.Enabled = True
            lblPaperSize.Enabled = True
        End If

        'Enable/disable controls that are unused with the pdf and file modes
        'Print mode
        cboPrintMode.Enabled = Not (chkPDF.Checked Or chkSaveToFile.Checked)
        lblPrinter.Enabled = Not (chkPDF.Checked Or chkSaveToFile.Checked)
        'Paper Size (only for save to file)
        cboPaperSize.Enabled = Not chkSaveToFile.Checked
        lblPaperSize.Enabled = Not chkSaveToFile.Checked
        'Printer setup
        btnPrintSetup.Enabled = Not (chkPDF.Checked Or chkSaveToFile.Checked)
        'N* of copies
        nudNCopie.Enabled = Not (chkPDF.Checked Or chkSaveToFile.Checked)
        lblCopies.Enabled = Not (chkPDF.Checked Or chkSaveToFile.Checked)
        'JPEG compression
        frmImageSettings.lblCompressionLabel.Enabled = chkSaveToFile.Checked
        frmImageSettings.tbCompression.Enabled = chkSaveToFile.Checked
        frmImageSettings.lblCompression.Enabled = chkSaveToFile.Checked
        'Image centering and scaling
        frmImageSettings.chkCenter.Enabled = Not chkSaveToFile.Checked
        frmImageSettings.tbScaling.Enabled = Not chkSaveToFile.Checked
        frmImageSettings.txtScaling.Enabled = Not chkSaveToFile.Checked

    End Sub

    Private Sub chkExact_CheckedChanged(sender As Object, e As EventArgs) Handles chkExact.CheckedChanged
        If chkExact.CheckState = CheckState.Checked Then
            gpbOrder.Visible = True
            Me.Height = Me.Height + 134
            Me.CenterToScreen()
            ExactStatusLabel.Visible = True
            Dim text As String = appControl.GetLocalizedString(LocalizedRootStr & ExactStatusLabel.Text)
            If text <> "" Then ExactStatusLabel.Text = text

            If Me.txtDossiernummer.Text.Trim <> "" Then
                btnCopy.Enabled = True
                txtDossiernummer.BackColor = SystemColors.Window
            Else
                txtDossiernummer.BackColor = Color.Orange
                btnCopy.Enabled = False
                txtDossiernummer.Focus()
            End If
        Else
            ExactStatusLabel.Visible = False
            gpbOrder.Visible = False
            Me.Height = Me.Height - 134
            Me.CenterToScreen()
            btnCopy.Enabled = True
            txtDossiernummer.BackColor = SystemColors.Window
        End If
    End Sub

    Private Sub txtDossiernummer_TextChanged(sender As Object, e As EventArgs) Handles txtDossiernummer.TextChanged
        If sender.text.ToString.Trim = "" Then
            btnCopy.Enabled = False
            txtDossiernummer.BackColor = Color.Orange
        Else
            btnCopy.Enabled = True
            txtDossiernummer.BackColor = SystemColors.Window
        End If
    End Sub

    Private Sub bw_doWork(sender As Object, e As DoWorkEventArgs)
        Dim worker As BackgroundWorker = CType(sender, BackgroundWorker)
        worker.ReportProgress(25)
        'Setup connection with the Exact databases
        AvailableInAdministration.Clear()
        Dim perc As Integer = 25

        'Walk through the Administrations to check for availability of the ordernumber
        'If available upload the documentation
        'Else inform user to change the ordernumber
        Dim uploadToAdministration As String = String.Empty
        For lus = 0 To EntityConnection.Count - 1
            Try
                If EntityConnection(lus).Connected = True Then
                    Dim checkOrderAvailability = New clsExact_Entity(EntityConnection (lus).Client , clsExact_Entity.EntityNames.SalesOrderHeader) 'Further implementation required
                    'Add order is available as boolean to the AvailableInAdministration-Stack from modGlobal.vb
                    AvailableInAdministration.Add(checkOrderAvailability.IsAvailable(txtDossiernummer.Text.Trim))
                    worker.ReportProgress(85)
                End If
                'Now itterate through the Availableinadmnistration to find out if we have a hit on the ordernumber given by the user
                For lus1 = 0 To AvailableInAdministration.Count - 1
                    If AvailableInAdministration(lus) = True Then
                        Dim entity As New clsExact_Entity(EntityConnection(lus).Client, clsExact_Entity.EntityNames.SalesOrderHeader)
                        data = entity.EntityData("SalesOrderNumber", txtDossiernummer.Text.Trim)
                        worker.ReportProgress(100)
                    End If
                Next
                If data Is Nothing Then data = New Exact.Services.Client.Data.EntityData
                worker.ReportProgress(100)
            Catch ex As Exception
                Trace.WriteLine("Onverwachte fout in mainfrm_btncopy_click" & vbCrLf & ex.Message, "ScanExact")
            End Try
        Next
        worker.ReportProgress(0)
        'appControl.Copy(settings)

    End Sub

    Private Sub bw_progresschanged(sender As Object, e As ProgressChangedEventArgs)
        ProgressBar1.Value = e.ProgressPercentage
        btnCopy.Text = "Processing.."
    End Sub

    Private Sub bw_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs)
        If e.Cancelled Then
            bw.CancelAsync()
        ElseIf Not e.Error Is Nothing Then
        Else
            bw.CancelAsync()
            btnCopy.Text = String.Empty

            Dim msg As New StringBuilder
            msg.AppendLine("Geen gegevens gevonden in Exact voor dit ordernummer!")
            msg.AppendLine(vbLf)
            msg.AppendLine("Het kan ook zijn dat de order nog open staat in Exact,")
            msg.AppendLine("sluit de order en probeer het opnieuw!")

            If data.Properties.Count = 0 Then
                MessageBox.Show(msg.ToString, "ScanExact")
            Else
                Dim ExactInfo = New ExactInfo
                ExactInfo.Data = data
                result = ExactInfo.ShowDialog(Me)

                If result = Windows.Forms.DialogResult.OK Then
                    Dim settings As ScanSettings = getScanSettings()
                    appControl.Copy(settings)
                    Dim Uploaded As Boolean = UploadToExact(data)

                Else
                    'MessageBox.Show("Clicked_Nee")
                End If
            End If
        End If
    End Sub

    Private Function UploadToExact(data As Exact.Services.Client.Data.EntityData) As Boolean
        btnCopy.Text = "Uploading"
        'Extract Debtor number from data
        Dim Debtor As String = String.Empty
        For Each itm In data.Properties
            Trace.WriteLine("Debtor: " & itm.Name & "; " & itm.Value)
            If itm.Name = "OrderDebtor" Then
                Debtor = itm.Value
            End If
        Next

        '// Bewaren verkooporder informatie voor document upload
        '// Dit omdat 'data' later wordt toegepast voor ophalen Account gegevens
        '// Eigenlijk slordig geprogrammeerd, kan netter.
        Dim EntityDataFromSalesOrderUsedForUploadDocument As Exact.Services.Client.Data.EntityData
        EntityDataFromSalesOrderUsedForUploadDocument = data

        'ophalen alle Accounts met DebiteurNummer om unieke GUID te achterhalen

        btnCopy.Text = "Uploading."

        For lus = 0 To AvailableInAdministration.Count - 1
            If AvailableInAdministration(lus) = True Then
                Dim entitiesData As New Exact.Services.Client.Data.Entities.EntitiesData
                entitiesData.EntityName = "Account"

                '//OM DE GUID OP TE HALEN GAAN WE EEN TOP 1 RETRIEVESET UITVOEREN
                '//DE GUID VAN DE WWN IS NAMELIJK NIET OPGENOMEN IN SALESORDERHEADER
                Dim entitiesFilter As New Exact.Services.Client.Data.Entities.RetrieveCriteria
                entitiesFilter.EntityName = "Account"
                entitiesFilter.BatchSize = 1 'TOP 1
                Dim qp As New Exact.Services.Client.Data.Entities.QueryProperty With {.Operation = "CONTAINS", .PropertyName = "AccountCode", .PropertyValue = Debtor}
                entitiesFilter.FilterQuery.Properties.Add(qp)

                entitiesData = EntitiesConnection(lus).Client.RetrieveSet(entitiesFilter)

                'Sleutel uit retrieveset halen van Entity ACCOUNT, dwz GUID ophalen van CIMPY
                Dim tk As String = "000000-0000-0000-0000-000000000000" 'ID van Account
                For Each record In entitiesData.Entities
                    For Each itm In record.Properties
                        'Console.WriteLine(itm.Name & "; " & itm.Value)
                        If itm.Name = "ID" Then tk = itm.Value
                    Next
                Next

                btnCopy.Text = "Uploading.."

                'Sleutel gebruiken om debiteurdata op te halen
                data = New Exact.Services.Client.Data.EntityData
                data.EntityName = "Account"
                data.Properties.Add(New Exact.Services.Client.Data.PropertyData With {.Name = "ID", .Value = tk})
                data = EntityConnection(lus).Client.Retrieve(data)

                Dim documentUpload As New clsExact_Binary(My.Settings.ServiceURI, My.Settings.SQLserver, EntityConnection(lus).AdministrationNumber, "Account", "ID", tk)

                btnCopy.Text = "Uploading..."

                Dim DocumentType() As String = Me.cboDocumentType.SelectedItem.Split(":")
                Dim returnGuid As Guid
                Dim Counter As Integer = 1
                For Each File In ScannedFilesForImportToExact
                    btnCopy.Text = "Uploading..(" & CStr(Counter) & ")"
                    returnGuid = documentUpload.Upload(File, EntityDataFromSalesOrderUsedForUploadDocument, tk, DocumentType(0).ToString, txtOnderwerp.Text.Trim)

                    If documentUpload.IsUploadCompleted Then
                        Trace.WriteLine("Uploaded to Exact: " & returnGuid.ToString & " - " & File)
                        Counter += 1
                        documentUpload.IsUploadCompleted = False
                    End If
                Next

                Dim infoMessage As String = String.Empty
                infoMessage = String.Format(appControl.GetLocalizedString("Msg_ExactUploadComplete"), Counter - 1, txtDossiernummer.Text)

                If Counter > 1 Then MessageBox.Show(infoMessage, "ScanExact", MessageBoxButtons.OK)
                btnCopy.Text = ""
                txtOnderwerp.Text = String.Empty
                ScannedFilesForImportToExact.Clear()
            End If
        Next

        Return True
    End Function

    Private Sub FillDocumentTypeComboBox()
        cboDocumentType.Items.Clear()

        'Check if the print spooler service is running, else program will freeze
        If IsServiceRunning("Print Spooler") Then
            'check if a default printer is installed
            If IsDefaultPrinterInstalled() Then
                'Populates Exact Document type Combobox
                Try
                    Dim dt As New Exact.Services.Client.Data.Entities.EntitiesData
                    Dim cr As New Exact.Services.Client.Data.Entities.RetrieveCriteria
                    Dim fq As New Exact.Services.Client.Data.Entities.FilterQuery
                    cr.BatchSize = 100
                    cr.EntityName = "DocumentType"
                    Dim qp(0) As Exact.Services.Client.Data.Entities.QueryProperty
                    qp.SetValue(New Exact.Services.Client.Data.Entities.QueryProperty With {.Operation = "=", .PropertyName = "SystemType", .PropertyValue = 0}, 0)
                    cr.FilterQuery.Properties.Add(qp(0))

                    dt = EntitiesConnection(0).Client.RetrieveSet(cr)

                    Dim format As String = "{0,-7} {1,-60}"
                    Dim _str3 As String = String.Empty
                    For Each Entity In dt.Entities
                        For Each Prop In Entity.Properties
                            Dim _str2 As String = String.Empty
                            If Prop.Name = "TypeDescription" And Not Prop.Value Is Nothing Then
                                _str2 += Prop.Value
                                _str3 = _str2
                            End If
                            Dim _str1 As String = String.Empty
                            If Prop.Name = "ID" Then
                                _str1 += Prop.Value.ToString
                                _str1 += ":"
                            End If
                            If Not String.IsNullOrEmpty(_str1) And Not String.IsNullOrEmpty(_str3) Then
                                cboDocumentType.Items.Add(String.Format(format, _str1, _str3))
                            End If
                        Next
                    Next
                    Me.cboDocumentType.SelectedIndex = 0
                Catch ex As Exception
                    If Not SplashThread Is Nothing Then SplashThread.Abort()
                    MsgBoxWrap("Error connecting to or loading data from Exact!")
                End Try
            Else
                MsgBoxWrap("There is no default printer, please select a default printer and restart the program!" & vbCrLf & "Program will be terminated!", MsgBoxStyle.Critical, "ScanExact")
            End If
        Else
            MsgBoxWrap("The printer spooler service is not running, program will be terminated!", MsgBoxStyle.Critical, "ScanExact")
            End
        End If

    End Sub

End Class
