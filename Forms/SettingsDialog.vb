'$safeprojectname$ - Simple Photocopier
'Copyright (C) 2007-2012 Matteo Rossi

'This program is free software: you can redistribute it and/or modify
'it under the terms of the GNU General Public License as published by
'the Free Software Foundation, either version 3 of the License, or
'(at your option) any later version.

'This program is distributed in the hope that it will be useful,
'but WITHOUT ANY WARRANTY; without even the implied warranty of
'MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'GNU General Public License for more details.

'You should have received a copy of the GNU General Public License
'along with this program.  If not, see <http://www.gnu.org/licenses/>.

Imports System.Windows.Forms
Imports System.Globalization
Imports WIA
Imports System.Text

Public Class SettingsDialog
    Dim localeRootStr As String

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        SaveSettings()
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Sub SaveSettings()
        If Not (My.Settings.Culture.LCID = cboLanguage.SelectedItem.LCID) Then
            My.Settings.Culture = cboLanguage.SelectedItem
            My.Settings.CustomCulture = True
            MsgBoxWrap(appControl.GetLocalizedString("Msg_Language"), MsgBoxStyle.Information, "ScanExact")
        End If

        My.Settings.RememberSettings = chkRememberScanSettings.Checked
        My.Settings.StoreLocation = chkRememberWindowPos.Checked
        My.Settings.CheckForUpdates = chkUpdates.Checked

        My.Settings.PDFAskWhereToSave = rbAskPDF.Checked
        My.Settings.PDFOpenAfterAcquisition = chkOpenPDF.Checked
        My.Settings.PDFSavePath = txtPathPDF.Text
        My.Settings.FileAskWhereToSave = rbAskFile.Checked
        My.Settings.FileOpenAfterAcquisition = chkOpenFile.Checked

        If txtPathFile.Text.EndsWith("\") Then
            My.Settings.FileSavePath = txtPathFile.Text
        Else
            My.Settings.FileSavePath = txtPathFile.Text + "\"
        End If

        If txtPathPDF.Text.EndsWith("\") Then
            My.Settings.PDFSavePath = txtPathPDF.Text
        Else
            My.Settings.PDFSavePath = txtPathPDF.Text + "\"
        End If

        If cboBitDepth.Text = "Auto" Or cboBitDepth.Text = "" Then
            My.Settings.LastScanSettings.BitDepth = 0
        Else
            My.Settings.LastScanSettings.BitDepth = CInt(cboBitDepth.Text)
        End If

        My.Settings.SQLserver = txtSQLserver.Text.Trim
        My.Settings.ServiceURI = txtServiceURL.Text.Trim
        My.Settings.Administrations = txtAdministrations.Text.Trim

    End Sub

    Sub LoadSettings()
        localeRootStr = Me.Name & "_"
        Me.Text = appControl.GetLocalizedString(Me.Name)
        'Applies localized strings to the controls
        For Each control As System.Windows.Forms.Control In Me.Controls

            Dim text As String = appControl.GetLocalizedString(localeRootStr & control.Name)
            If text <> "" Then control.Text = text
            ToolTip1.SetToolTip(control, appControl.GetLocalizedString(localeRootStr & control.Name & "ToolTip"))
            For Each subcontrol As System.Windows.Forms.Control In control.Controls
                text = appControl.GetLocalizedString(localeRootStr & subcontrol.Name)
                If text <> "" Then subcontrol.Text = text
                ToolTip1.SetToolTip(subcontrol, appControl.GetLocalizedString(localeRootStr & subcontrol.Name & "ToolTip"))
                For Each subsubcontrol As System.Windows.Forms.Control In subcontrol.Controls
                    text = appControl.GetLocalizedString(localeRootStr & subsubcontrol.Name)
                    If text <> "" Then subsubcontrol.Text = text
                    ToolTip1.SetToolTip(subsubcontrol, appControl.GetLocalizedString(localeRootStr & subsubcontrol.Name & "ToolTip"))
                    For Each subsubsubcontrol As System.Windows.Forms.Control In subsubcontrol.Controls
                        text = appControl.GetLocalizedString(localeRootStr & subsubsubcontrol.Name)
                        If text <> "" Then subsubsubcontrol.Text = text
                        ToolTip1.SetToolTip(subsubsubcontrol, appControl.GetLocalizedString(localeRootStr & subsubsubcontrol.Name & "ToolTip"))
                    Next
                Next
            Next
        Next

        'Populates language combobox
        cboLanguage.Items.Clear()

        'It may occur that the thread that gathers the cultures (which might be slow) hasn't done is work yet.
        'We wait for the thread to finish
        Dim tries As Integer = 0
        While tries < 10
            Try
                If appControl.AvailableLanguages.GetUpperBound(0) = 0 Then appControl.GetAvailableLanguages()
                Dim availableCultures As CultureInfo() = appControl.AvailableLanguages
                cboLanguage.Items.AddRange(appControl.AvailableLanguages)
                cboLanguage.DisplayMember = "EnglishName"
                Exit While
            Catch ex As Threading.ThreadStateException
                Threading.Thread.Sleep(1000)
                tries += 1
            End Try
        End While
        'Select the current culture from the combo box
        cboLanguage.SelectedItem = Threading.Thread.CurrentThread.CurrentUICulture

        'This happens if $safeprojectname$ is not translated into the system default culture
        If cboLanguage.SelectedItem Is Nothing Then
            cboLanguage.SelectedItem = CultureInfo.GetCultureInfo("en-us")
        End If

        'Load bit depth settings
        If My.Settings.LastScanSettings.BitDepth = 0 Then
            cboBitDepth.SelectedIndex = 0
        Else
            cboBitDepth.Text = My.Settings.LastScanSettings.BitDepth
        End If

        'Load file saving preferences
        rbAskFile.Checked = My.Settings.FileAskWhereToSave
        rbPathFile.Checked = Not My.Settings.FileAskWhereToSave
        rbAskPDF.Checked = My.Settings.PDFAskWhereToSave
        rbPathPDF.Checked = Not My.Settings.PDFAskWhereToSave

        txtPathFile.Text = My.Settings.FileSavePath
        txtPathPDF.Text = My.Settings.PDFSavePath

        chkOpenFile.Checked = My.Settings.FileOpenAfterAcquisition
        chkOpenPDF.Checked = My.Settings.PDFOpenAfterAcquisition

        'Scan button association
        lblScanner.Text = "Scanner: " + appControl.ScannerDescription
        If Not appControl.Scanner Is Nothing Then
            Dim deviceID As String = appControl.Scanner.DeviceId
            Dim events As WIA.DeviceEvents = appControl.GetScannerEvents()
            Dim evWrappers As New List(Of WIAEventWrapper)
            Dim availActions As New List(Of Action)
            availActions.Add(New Action("Show Interface", ""))
            availActions.Add(New Action("Copy", "/c"))
            availActions.Add(New Action("Scan to File", "/f"))

            ComboBox1.DataSource = availActions
            ComboBox1.ValueMember = "Arguments"
            ComboBox1.DisplayMember = "Description"

            For Each ev As DeviceEvent In events
                If Not ev.Name.Contains("Device") Then
                    Dim evWr As New WIAEventWrapper(ev)
                    evWrappers.Add(evWr)
                    ListBox1.Items.Add(evWr)
                End If
            Next
        Else
            Dim deviceID As String = "No Scanner"
        End If


        chkRememberScanSettings.Checked = My.Settings.RememberSettings
        chkRememberWindowPos.Checked = My.Settings.StoreLocation
        chkUpdates.Checked = My.Settings.CheckForUpdates

        'Exact settings
        Me.txtAdministrations.Text = My.Settings.Administrations
        Me.txtServiceURL.Text = My.Settings.ServiceURI
        Me.txtSQLserver.Text = My.Settings.SQLserver

    End Sub

    Private Sub SettingsDialog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadSettings()
    End Sub

    Private Sub btnRegister_Click(sender As System.Object, e As System.EventArgs) Handles btnRegister.Click

        Dim manager As New DeviceManager()
        Dim ev = DirectCast(ListBox1.SelectedItem, WIAEventWrapper)
        Dim command As String = String.Format("{0} {1} {2}", Application.ExecutablePath, ComboBox1.SelectedValue.ToString(), "/StiDevice:%1")

        'Affects registry keys
        'HKEY_LOCAL_MACHINE\SYSTEM\ControlSet001\Control\StillImage\Events\EVENT_ID
        'HKEY_LOCAL_MACHINE\SYSTEM\ControlSet002\Control\StillImage\Events\EVENT_ID
        'HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\StillImage\Events\EVENT_ID
        manager.RegisterPersistentEvent(command, "$safeprojectname$", "Whatever", "", ev.EventID) ', appControl.Scanner.DeviceId)

    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim manager As New DeviceManager()
        Dim ev = DirectCast(ListBox1.SelectedItem, WIAEventWrapper)
        Dim command As String = String.Format("{0} {1}", Application.ExecutablePath, ComboBox1.SelectedValue.ToString())
        manager.UnregisterPersistentEvent(command, "$safeprojectname$", "Whatever", "", ev.EventID) ', appControl.Scanner.DeviceId)
    End Sub

    Private Sub btnResetScanSettings_Click(sender As System.Object, e As System.EventArgs) Handles btnResetScanSettings.Click
        My.Settings.LastScanSettings = New ScanSettings()
    End Sub

    Private Sub btnBrowseFile_Click(sender As System.Object, e As System.EventArgs) Handles btnBrowseFile.Click
        Dim fldr As New FolderBrowserDialog
        fldr.SelectedPath = txtPathFile.Text
        Dim res = fldr.ShowDialog()
        If res = Windows.Forms.DialogResult.OK Then
            txtPathFile.Text = fldr.SelectedPath
        End If
    End Sub

    Private Sub btnBrowsePDF_Click(sender As System.Object, e As System.EventArgs) Handles btnBrowsePDF.Click
        Dim fldr As New FolderBrowserDialog
        fldr.SelectedPath = txtPathPDF.Text
        Dim res = fldr.ShowDialog()
        If res = Windows.Forms.DialogResult.OK Then
            txtPathPDF.Text = fldr.SelectedPath
        End If
    End Sub

    Private Sub rbAskFile_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbAskFile.CheckedChanged
        txtPathFile.Enabled = Not rbAskFile.Checked
        btnBrowseFile.Enabled = Not rbAskFile.Checked
    End Sub

    Private Sub rbAskPDF_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbAskPDF.CheckedChanged
        txtPathPDF.Enabled = Not rbAskPDF.Checked
        btnBrowsePDF.Enabled = Not rbAskPDF.Checked
    End Sub

    Private Sub cmdTestExactSettings_Click(sender As Object, e As EventArgs) Handles cmdTestExactSettings.Click
        Me.Cursor = Cursors.WaitCursor
        Dim connectionsSuccesfull As Boolean = True
        Dim Admins As List(Of String)
        lstExactTestConnection.Items.Clear()


        If txtAdministrations.Text.Trim = "" Or txtServiceURL.Text.Trim = "" Or txtSQLserver.Text.Trim = "" Then
            lstExactTestConnection.Items.Add("One or more fields are empty!")
            Me.Cursor = Cursors.Default
            Exit Sub
        End If

        Try
            Admins = Administrations_split(Me.txtAdministrations.Text.Trim)
        Catch ex As Exception
            MessageBox.Show("There is something wrong with the administrations textfield" & vbCrLf & _
                             "Please fill in the correct Administrationnumbers like '100'" & vbCrLf & _
                             "or like '100;110;120' or '800;801;802;803'", "ScanExact", MessageBoxButtons.OK)
            connectionsSuccesfull = False
            Me.Cursor = Cursors.Default
            Exit Sub
        End Try

        Dim testClients As New List(Of clsExact_Connection)
        For c = 0 To Admins.Count - 1
            Dim testClient As New clsExact_Connection(Admins(c), txtServiceURL.Text.Trim, txtSQLserver.Text.Trim)
            testClients.Add(testClient)
        Next

        Try
            'Find out if the service is properly running
            If testClients(0).IsConnectingToService = True Then
                lstExactTestConnection.Items.Add("Service URL connection succeeded!")
                Me.txtServiceURL.ForeColor = Color.DarkGreen
            Else
                lstExactTestConnection.Items.Add("Service URL connection failed!")
                lstExactTestConnection.Items.Add("Check if services are running, URL address and firewall")
                Me.txtServiceURL.ForeColor = Color.Red
                connectionsSuccesfull = False
            End If
        Catch ex1 As Exact.Services.Client.Exceptions.EntityFunctionalException
            lstExactTestConnection.Items.Add(ex1.Message)
        Catch ex2 As Exact.Services.Client.Exceptions.EntityException
            lstExactTestConnection.Items.Add(ex2.Message)
        Catch ex As Exception
            lstExactTestConnection.Items.Add("Unable to Connect to Service URL.")
            Me.Cursor = Cursors.Default
        End Try


        Try
            If testClients(0).IsConnectingToService = True Then
                For Each testClient In testClients
                    If testClient.IsConnectingToDatabase = True Then
                        lstExactTestConnection.Items.Add("Connection to administrationnumber: " & testClient.AdministrationNumber & " succeeded!")
                        txtSQLserver.ForeColor = Color.DarkGreen
                        txtAdministrations.ForeColor = Color.DarkGreen
                    Else
                        lstExactTestConnection.Items.Add("Connection to administrationnumber: " & testClient.AdministrationNumber & " failed!")
                        lstExactTestConnection.Items.Add("1. Check the name of the SQL Server")
                        lstExactTestConnection.Items.Add("2. Please check if the administrationnumber (" & testClient.AdministrationNumber & ") is correct,")
                        lstExactTestConnection.Items.Add("   or if the current user has rights in Exact for this administration ")
                        lstExactTestConnection.Items.Add("3. Check Exact documentation on MAC address security!")
                        connectionsSuccesfull = False
                        txtSQLserver.ForeColor = Color.Red
                        txtAdministrations.ForeColor = Color.Red
                    End If
                Next
            End If
        Catch ex As Exception
            MessageBox.Show("Unable to connect to Exact services, verify that Exact services setup is correct and" & vbCrLf & _
                            "check your firewall. Contact your administrator or Exact Consultant!")
            connectionsSuccesfull = False
            Me.Cursor = Cursors.Default
        End Try

        If connectionsSuccesfull = True Then
            My.Settings.SQLserver = txtSQLserver.Text.Trim
            My.Settings.ServiceURI = txtServiceURL.Text.Trim
            My.Settings.Administrations = txtAdministrations.Text.Trim
            My.Settings.IsConnectionToExactSucceeded = True
            lstExactTestConnection.Items.Add("Settings will be saved after closing this window!")
        End If
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub tabExactSettings_GotFocus(sender As Object, e As EventArgs) Handles tabExactSettings.GotFocus
        MsgBox("Got focus")
    End Sub

End Class

Class WIAEventWrapper
    Implements DeviceEvent

    Dim _ev As WIA.DeviceEvent
    Dim _action As Action

    Sub New(ev As WIA.DeviceEvent)
        _ev = ev
    End Sub

    Public Property currentAction() As Action
        Get
            Return _action
        End Get
        Set(ByVal value As Action)
            _action = value
        End Set
    End Property

    Public Overrides Function ToString() As String
        Return _ev.Name
    End Function

    Public ReadOnly Property Description As String Implements WIA.IDeviceEvent.Description
        Get
            Return _ev.Description
        End Get
    End Property

    Public ReadOnly Property EventID As String Implements WIA.IDeviceEvent.EventID
        Get
            Return _ev.EventID
        End Get
    End Property

    Public ReadOnly Property Name As String Implements WIA.IDeviceEvent.Name
        Get
            Return _ev.Name
        End Get
    End Property

    Public ReadOnly Property Type As WIA.WiaEventFlag Implements WIA.IDeviceEvent.Type
        Get
            Return _ev.Type
        End Get
    End Property
End Class

Class Action
    Public Sub New(Description As String, Arguments As String)
        _description = Description
        _arguments = Arguments
    End Sub

    Private _description As String
    Public Property Description() As String
        Get
            Return _description
        End Get
        Set(ByVal value As String)
            _description = value
        End Set
    End Property

    Private _arguments As String
    Public Property Arguments() As String
        Get
            Return _arguments
        End Get
        Set(ByVal value As String)
            _arguments = value
        End Set
    End Property

End Class