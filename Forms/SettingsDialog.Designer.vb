<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SettingsDialog
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.btnOk = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.lblLanguage = New System.Windows.Forms.Label()
        Me.cboLanguage = New System.Windows.Forms.ComboBox()
        Me.chkRememberWindowPos = New System.Windows.Forms.CheckBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.lblBitDepth = New System.Windows.Forms.Label()
        Me.cboBitDepth = New System.Windows.Forms.ComboBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.tabGeneral = New System.Windows.Forms.TabPage()
        Me.btnResetScanSettings = New System.Windows.Forms.Button()
        Me.lblNote = New System.Windows.Forms.Label()
        Me.chkUpdates = New System.Windows.Forms.CheckBox()
        Me.chkRememberScanSettings = New System.Windows.Forms.CheckBox()
        Me.tabFileSettings = New System.Windows.Forms.TabPage()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblSaveToPDF = New System.Windows.Forms.Label()
        Me.chkOpenPDF = New System.Windows.Forms.CheckBox()
        Me.btnBrowsePDF = New System.Windows.Forms.Button()
        Me.rbAskPDF = New System.Windows.Forms.RadioButton()
        Me.txtPathPDF = New System.Windows.Forms.TextBox()
        Me.rbPathPDF = New System.Windows.Forms.RadioButton()
        Me.chkOpenFile = New System.Windows.Forms.CheckBox()
        Me.btnBrowseFile = New System.Windows.Forms.Button()
        Me.txtPathFile = New System.Windows.Forms.TextBox()
        Me.rbPathFile = New System.Windows.Forms.RadioButton()
        Me.rbAskFile = New System.Windows.Forms.RadioButton()
        Me.lblSaveToFile = New System.Windows.Forms.Label()
        Me.tabScannerButtons = New System.Windows.Forms.TabPage()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.lblScanner = New System.Windows.Forms.Label()
        Me.lblAvailableEvents = New System.Windows.Forms.Label()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.btnRegister = New System.Windows.Forms.Button()
        Me.tabExactSettings = New System.Windows.Forms.TabPage()
        Me.lstExactTestConnection = New System.Windows.Forms.ListBox()
        Me.cmdTestExactSettings = New System.Windows.Forms.Button()
        Me.lblAdministrations = New System.Windows.Forms.Label()
        Me.txtAdministrations = New System.Windows.Forms.TextBox()
        Me.txtSQLserver = New System.Windows.Forms.TextBox()
        Me.lblSQLserver = New System.Windows.Forms.Label()
        Me.txtServiceURL = New System.Windows.Forms.TextBox()
        Me.lblServiceURL = New System.Windows.Forms.Label()
        Me.TabControl1.SuspendLayout()
        Me.tabGeneral.SuspendLayout()
        Me.tabFileSettings.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.tabScannerButtons.SuspendLayout()
        Me.tabExactSettings.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnOk
        '
        Me.btnOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOk.Location = New System.Drawing.Point(286, 261)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(75, 23)
        Me.btnOk.TabIndex = 0
        Me.btnOk.Text = "btnOk"
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(367, 261)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "btnCancel"
        '
        'lblLanguage
        '
        Me.lblLanguage.AutoSize = True
        Me.lblLanguage.Location = New System.Drawing.Point(9, 20)
        Me.lblLanguage.Name = "lblLanguage"
        Me.lblLanguage.Size = New System.Drawing.Size(55, 13)
        Me.lblLanguage.TabIndex = 1
        Me.lblLanguage.Text = "Language"
        '
        'cboLanguage
        '
        Me.cboLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboLanguage.FormattingEnabled = True
        Me.cboLanguage.Location = New System.Drawing.Point(195, 17)
        Me.cboLanguage.Name = "cboLanguage"
        Me.cboLanguage.Size = New System.Drawing.Size(201, 21)
        Me.cboLanguage.TabIndex = 2
        '
        'chkRememberWindowPos
        '
        Me.chkRememberWindowPos.Checked = True
        Me.chkRememberWindowPos.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkRememberWindowPos.Location = New System.Drawing.Point(12, 95)
        Me.chkRememberWindowPos.Name = "chkRememberWindowPos"
        Me.chkRememberWindowPos.Size = New System.Drawing.Size(293, 17)
        Me.chkRememberWindowPos.TabIndex = 3
        Me.chkRememberWindowPos.Text = "Remember window position"
        Me.chkRememberWindowPos.UseVisualStyleBackColor = True
        '
        'lblBitDepth
        '
        Me.lblBitDepth.Location = New System.Drawing.Point(9, 185)
        Me.lblBitDepth.Name = "lblBitDepth"
        Me.lblBitDepth.Size = New System.Drawing.Size(196, 13)
        Me.lblBitDepth.TabIndex = 6
        Me.lblBitDepth.Text = "Force Bit Depth for Color Mode"
        '
        'cboBitDepth
        '
        Me.cboBitDepth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBitDepth.FormattingEnabled = True
        Me.cboBitDepth.Items.AddRange(New Object() {"Auto", "8", "16", "24", "32"})
        Me.cboBitDepth.Location = New System.Drawing.Point(262, 182)
        Me.cboBitDepth.Name = "cboBitDepth"
        Me.cboBitDepth.Size = New System.Drawing.Size(134, 21)
        Me.cboBitDepth.TabIndex = 7
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.tabGeneral)
        Me.TabControl1.Controls.Add(Me.tabFileSettings)
        Me.TabControl1.Controls.Add(Me.tabScannerButtons)
        Me.TabControl1.Controls.Add(Me.tabExactSettings)
        Me.TabControl1.Location = New System.Drawing.Point(12, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(430, 243)
        Me.TabControl1.TabIndex = 8
        '
        'tabGeneral
        '
        Me.tabGeneral.Controls.Add(Me.btnResetScanSettings)
        Me.tabGeneral.Controls.Add(Me.lblNote)
        Me.tabGeneral.Controls.Add(Me.chkUpdates)
        Me.tabGeneral.Controls.Add(Me.chkRememberScanSettings)
        Me.tabGeneral.Controls.Add(Me.cboLanguage)
        Me.tabGeneral.Controls.Add(Me.lblLanguage)
        Me.tabGeneral.Controls.Add(Me.cboBitDepth)
        Me.tabGeneral.Controls.Add(Me.chkRememberWindowPos)
        Me.tabGeneral.Controls.Add(Me.lblBitDepth)
        Me.tabGeneral.Location = New System.Drawing.Point(4, 22)
        Me.tabGeneral.Name = "tabGeneral"
        Me.tabGeneral.Padding = New System.Windows.Forms.Padding(3)
        Me.tabGeneral.Size = New System.Drawing.Size(422, 217)
        Me.tabGeneral.TabIndex = 0
        Me.tabGeneral.Text = "General"
        Me.tabGeneral.UseVisualStyleBackColor = True
        '
        'btnResetScanSettings
        '
        Me.btnResetScanSettings.Location = New System.Drawing.Point(195, 60)
        Me.btnResetScanSettings.Name = "btnResetScanSettings"
        Me.btnResetScanSettings.Size = New System.Drawing.Size(201, 23)
        Me.btnResetScanSettings.TabIndex = 11
        Me.btnResetScanSettings.Text = "Reset to default"
        Me.btnResetScanSettings.UseVisualStyleBackColor = True
        '
        'lblNote
        '
        Me.lblNote.AutoSize = True
        Me.lblNote.Location = New System.Drawing.Point(9, 150)
        Me.lblNote.MaximumSize = New System.Drawing.Size(310, 0)
        Me.lblNote.Name = "lblNote"
        Me.lblNote.Size = New System.Drawing.Size(283, 26)
        Me.lblNote.TabIndex = 10
        Me.lblNote.Text = "NOTE Don't change the following setting unless you have problems with the acquire" & _
    "d images"
        '
        'chkUpdates
        '
        Me.chkUpdates.AutoSize = True
        Me.chkUpdates.Location = New System.Drawing.Point(12, 118)
        Me.chkUpdates.Name = "chkUpdates"
        Me.chkUpdates.Size = New System.Drawing.Size(115, 17)
        Me.chkUpdates.TabIndex = 9
        Me.chkUpdates.Text = "Check for Updates"
        Me.chkUpdates.UseVisualStyleBackColor = True
        Me.chkUpdates.Visible = False
        '
        'chkRememberScanSettings
        '
        Me.chkRememberScanSettings.AutoSize = True
        Me.chkRememberScanSettings.Checked = True
        Me.chkRememberScanSettings.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkRememberScanSettings.Location = New System.Drawing.Point(12, 64)
        Me.chkRememberScanSettings.Name = "chkRememberScanSettings"
        Me.chkRememberScanSettings.Size = New System.Drawing.Size(138, 17)
        Me.chkRememberScanSettings.TabIndex = 8
        Me.chkRememberScanSettings.Text = "Remeber Scan Settings"
        Me.chkRememberScanSettings.UseVisualStyleBackColor = True
        '
        'tabFileSettings
        '
        Me.tabFileSettings.Controls.Add(Me.Panel1)
        Me.tabFileSettings.Controls.Add(Me.chkOpenFile)
        Me.tabFileSettings.Controls.Add(Me.btnBrowseFile)
        Me.tabFileSettings.Controls.Add(Me.txtPathFile)
        Me.tabFileSettings.Controls.Add(Me.rbPathFile)
        Me.tabFileSettings.Controls.Add(Me.rbAskFile)
        Me.tabFileSettings.Controls.Add(Me.lblSaveToFile)
        Me.tabFileSettings.Location = New System.Drawing.Point(4, 22)
        Me.tabFileSettings.Name = "tabFileSettings"
        Me.tabFileSettings.Padding = New System.Windows.Forms.Padding(3)
        Me.tabFileSettings.Size = New System.Drawing.Size(422, 217)
        Me.tabFileSettings.TabIndex = 2
        Me.tabFileSettings.Text = "tabFileSettings"
        Me.tabFileSettings.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.lblSaveToPDF)
        Me.Panel1.Controls.Add(Me.chkOpenPDF)
        Me.Panel1.Controls.Add(Me.btnBrowsePDF)
        Me.Panel1.Controls.Add(Me.rbAskPDF)
        Me.Panel1.Controls.Add(Me.txtPathPDF)
        Me.Panel1.Controls.Add(Me.rbPathPDF)
        Me.Panel1.Location = New System.Drawing.Point(0, 110)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(422, 107)
        Me.Panel1.TabIndex = 14
        '
        'lblSaveToPDF
        '
        Me.lblSaveToPDF.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblSaveToPDF.AutoSize = True
        Me.lblSaveToPDF.Location = New System.Drawing.Point(0, 10)
        Me.lblSaveToPDF.Name = "lblSaveToPDF"
        Me.lblSaveToPDF.Size = New System.Drawing.Size(76, 13)
        Me.lblSaveToPDF.TabIndex = 7
        Me.lblSaveToPDF.Text = "lblSaveToPDF"
        '
        'chkOpenPDF
        '
        Me.chkOpenPDF.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkOpenPDF.AutoSize = True
        Me.chkOpenPDF.Location = New System.Drawing.Point(3, 85)
        Me.chkOpenPDF.Name = "chkOpenPDF"
        Me.chkOpenPDF.Size = New System.Drawing.Size(91, 17)
        Me.chkOpenPDF.TabIndex = 13
        Me.chkOpenPDF.Text = "chkOpenPDF"
        Me.chkOpenPDF.UseVisualStyleBackColor = True
        '
        'btnBrowsePDF
        '
        Me.btnBrowsePDF.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnBrowsePDF.Location = New System.Drawing.Point(231, 57)
        Me.btnBrowsePDF.Name = "btnBrowsePDF"
        Me.btnBrowsePDF.Size = New System.Drawing.Size(174, 23)
        Me.btnBrowsePDF.TabIndex = 12
        Me.btnBrowsePDF.Text = "btnBrowsePDF"
        Me.btnBrowsePDF.UseVisualStyleBackColor = True
        '
        'rbAskPDF
        '
        Me.rbAskPDF.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rbAskPDF.AutoSize = True
        Me.rbAskPDF.Location = New System.Drawing.Point(3, 33)
        Me.rbAskPDF.Name = "rbAskPDF"
        Me.rbAskPDF.Size = New System.Drawing.Size(73, 17)
        Me.rbAskPDF.TabIndex = 9
        Me.rbAskPDF.TabStop = True
        Me.rbAskPDF.Text = "rbAskPDF"
        Me.rbAskPDF.UseVisualStyleBackColor = True
        '
        'txtPathPDF
        '
        Me.txtPathPDF.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtPathPDF.Location = New System.Drawing.Point(3, 59)
        Me.txtPathPDF.Name = "txtPathPDF"
        Me.txtPathPDF.Size = New System.Drawing.Size(222, 20)
        Me.txtPathPDF.TabIndex = 11
        '
        'rbPathPDF
        '
        Me.rbPathPDF.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rbPathPDF.AutoSize = True
        Me.rbPathPDF.Location = New System.Drawing.Point(120, 33)
        Me.rbPathPDF.Name = "rbPathPDF"
        Me.rbPathPDF.Size = New System.Drawing.Size(72, 17)
        Me.rbPathPDF.TabIndex = 10
        Me.rbPathPDF.TabStop = True
        Me.rbPathPDF.Text = "rbPathFile"
        Me.rbPathPDF.UseVisualStyleBackColor = True
        '
        'chkOpenFile
        '
        Me.chkOpenFile.AutoSize = True
        Me.chkOpenFile.Location = New System.Drawing.Point(3, 84)
        Me.chkOpenFile.Name = "chkOpenFile"
        Me.chkOpenFile.Size = New System.Drawing.Size(86, 17)
        Me.chkOpenFile.TabIndex = 6
        Me.chkOpenFile.Text = "chkOpenFile"
        Me.chkOpenFile.UseVisualStyleBackColor = True
        '
        'btnBrowseFile
        '
        Me.btnBrowseFile.Location = New System.Drawing.Point(231, 56)
        Me.btnBrowseFile.Name = "btnBrowseFile"
        Me.btnBrowseFile.Size = New System.Drawing.Size(174, 23)
        Me.btnBrowseFile.TabIndex = 5
        Me.btnBrowseFile.Text = "btnBrowseFile"
        Me.btnBrowseFile.UseVisualStyleBackColor = True
        '
        'txtPathFile
        '
        Me.txtPathFile.Location = New System.Drawing.Point(3, 58)
        Me.txtPathFile.Name = "txtPathFile"
        Me.txtPathFile.Size = New System.Drawing.Size(222, 20)
        Me.txtPathFile.TabIndex = 4
        '
        'rbPathFile
        '
        Me.rbPathFile.AutoSize = True
        Me.rbPathFile.Location = New System.Drawing.Point(120, 35)
        Me.rbPathFile.Name = "rbPathFile"
        Me.rbPathFile.Size = New System.Drawing.Size(72, 17)
        Me.rbPathFile.TabIndex = 3
        Me.rbPathFile.TabStop = True
        Me.rbPathFile.Text = "rbPathFile"
        Me.rbPathFile.UseVisualStyleBackColor = True
        '
        'rbAskFile
        '
        Me.rbAskFile.AutoSize = True
        Me.rbAskFile.Location = New System.Drawing.Point(3, 35)
        Me.rbAskFile.Name = "rbAskFile"
        Me.rbAskFile.Size = New System.Drawing.Size(68, 17)
        Me.rbAskFile.TabIndex = 2
        Me.rbAskFile.TabStop = True
        Me.rbAskFile.Text = "rbAskFile"
        Me.rbAskFile.UseVisualStyleBackColor = True
        '
        'lblSaveToFile
        '
        Me.lblSaveToFile.AutoSize = True
        Me.lblSaveToFile.Location = New System.Drawing.Point(3, 12)
        Me.lblSaveToFile.Name = "lblSaveToFile"
        Me.lblSaveToFile.Size = New System.Drawing.Size(71, 13)
        Me.lblSaveToFile.TabIndex = 0
        Me.lblSaveToFile.Text = "lblSaveToFile"
        '
        'tabScannerButtons
        '
        Me.tabScannerButtons.Controls.Add(Me.Button1)
        Me.tabScannerButtons.Controls.Add(Me.ComboBox1)
        Me.tabScannerButtons.Controls.Add(Me.lblScanner)
        Me.tabScannerButtons.Controls.Add(Me.lblAvailableEvents)
        Me.tabScannerButtons.Controls.Add(Me.ListBox1)
        Me.tabScannerButtons.Controls.Add(Me.btnRegister)
        Me.tabScannerButtons.Location = New System.Drawing.Point(4, 22)
        Me.tabScannerButtons.Name = "tabScannerButtons"
        Me.tabScannerButtons.Padding = New System.Windows.Forms.Padding(3)
        Me.tabScannerButtons.Size = New System.Drawing.Size(422, 217)
        Me.tabScannerButtons.TabIndex = 1
        Me.tabScannerButtons.Text = "Scanner Buttons"
        Me.tabScannerButtons.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Enabled = False
        Me.Button1.Location = New System.Drawing.Point(213, 169)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(66, 23)
        Me.Button1.TabIndex = 8
        Me.Button1.Text = "Unregister"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ComboBox1
        '
        Me.ComboBox1.Enabled = False
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(9, 171)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(126, 21)
        Me.ComboBox1.TabIndex = 7
        '
        'lblScanner
        '
        Me.lblScanner.AutoSize = True
        Me.lblScanner.Enabled = False
        Me.lblScanner.Location = New System.Drawing.Point(6, 16)
        Me.lblScanner.Name = "lblScanner"
        Me.lblScanner.Size = New System.Drawing.Size(53, 13)
        Me.lblScanner.TabIndex = 6
        Me.lblScanner.Text = "Scanner: "
        '
        'lblAvailableEvents
        '
        Me.lblAvailableEvents.AutoSize = True
        Me.lblAvailableEvents.Enabled = False
        Me.lblAvailableEvents.Location = New System.Drawing.Point(6, 38)
        Me.lblAvailableEvents.Name = "lblAvailableEvents"
        Me.lblAvailableEvents.Size = New System.Drawing.Size(86, 13)
        Me.lblAvailableEvents.TabIndex = 5
        Me.lblAvailableEvents.Text = "Available Events"
        '
        'ListBox1
        '
        Me.ListBox1.Enabled = False
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Location = New System.Drawing.Point(9, 60)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(270, 95)
        Me.ListBox1.TabIndex = 1
        '
        'btnRegister
        '
        Me.btnRegister.Enabled = False
        Me.btnRegister.Location = New System.Drawing.Point(141, 169)
        Me.btnRegister.Name = "btnRegister"
        Me.btnRegister.Size = New System.Drawing.Size(66, 23)
        Me.btnRegister.TabIndex = 0
        Me.btnRegister.Text = "Register"
        Me.btnRegister.UseVisualStyleBackColor = True
        '
        'tabExactSettings
        '
        Me.tabExactSettings.Controls.Add(Me.lstExactTestConnection)
        Me.tabExactSettings.Controls.Add(Me.cmdTestExactSettings)
        Me.tabExactSettings.Controls.Add(Me.lblAdministrations)
        Me.tabExactSettings.Controls.Add(Me.txtAdministrations)
        Me.tabExactSettings.Controls.Add(Me.txtSQLserver)
        Me.tabExactSettings.Controls.Add(Me.lblSQLserver)
        Me.tabExactSettings.Controls.Add(Me.txtServiceURL)
        Me.tabExactSettings.Controls.Add(Me.lblServiceURL)
        Me.tabExactSettings.Location = New System.Drawing.Point(4, 22)
        Me.tabExactSettings.Name = "tabExactSettings"
        Me.tabExactSettings.Padding = New System.Windows.Forms.Padding(3)
        Me.tabExactSettings.Size = New System.Drawing.Size(422, 217)
        Me.tabExactSettings.TabIndex = 3
        Me.tabExactSettings.Text = "ExactSettings"
        Me.tabExactSettings.UseVisualStyleBackColor = True
        '
        'lstExactTestConnection
        '
        Me.lstExactTestConnection.BackColor = System.Drawing.SystemColors.WindowText
        Me.lstExactTestConnection.Font = New System.Drawing.Font("Courier New", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstExactTestConnection.ForeColor = System.Drawing.SystemColors.Window
        Me.lstExactTestConnection.FormattingEnabled = True
        Me.lstExactTestConnection.ItemHeight = 12
        Me.lstExactTestConnection.Location = New System.Drawing.Point(12, 117)
        Me.lstExactTestConnection.Name = "lstExactTestConnection"
        Me.lstExactTestConnection.Size = New System.Drawing.Size(390, 52)
        Me.lstExactTestConnection.TabIndex = 7
        '
        'cmdTestExactSettings
        '
        Me.cmdTestExactSettings.Location = New System.Drawing.Point(289, 188)
        Me.cmdTestExactSettings.Name = "cmdTestExactSettings"
        Me.cmdTestExactSettings.Size = New System.Drawing.Size(113, 23)
        Me.cmdTestExactSettings.TabIndex = 6
        Me.cmdTestExactSettings.Text = "Test settings"
        Me.cmdTestExactSettings.UseVisualStyleBackColor = True
        '
        'lblAdministrations
        '
        Me.lblAdministrations.AutoSize = True
        Me.lblAdministrations.Location = New System.Drawing.Point(9, 87)
        Me.lblAdministrations.Name = "lblAdministrations"
        Me.lblAdministrations.Size = New System.Drawing.Size(87, 13)
        Me.lblAdministrations.TabIndex = 5
        Me.lblAdministrations.Text = "lblAdministrations"
        '
        'txtAdministrations
        '
        Me.txtAdministrations.Location = New System.Drawing.Point(131, 84)
        Me.txtAdministrations.Name = "txtAdministrations"
        Me.txtAdministrations.Size = New System.Drawing.Size(271, 20)
        Me.txtAdministrations.TabIndex = 4
        '
        'txtSQLserver
        '
        Me.txtSQLserver.Location = New System.Drawing.Point(131, 51)
        Me.txtSQLserver.Name = "txtSQLserver"
        Me.txtSQLserver.Size = New System.Drawing.Size(271, 20)
        Me.txtSQLserver.TabIndex = 3
        '
        'lblSQLserver
        '
        Me.lblSQLserver.AutoSize = True
        Me.lblSQLserver.Location = New System.Drawing.Point(9, 54)
        Me.lblSQLserver.Name = "lblSQLserver"
        Me.lblSQLserver.Size = New System.Drawing.Size(67, 13)
        Me.lblSQLserver.TabIndex = 2
        Me.lblSQLserver.Text = "lblSQLserver"
        '
        'txtServiceURL
        '
        Me.txtServiceURL.Location = New System.Drawing.Point(131, 17)
        Me.txtServiceURL.Name = "txtServiceURL"
        Me.txtServiceURL.Size = New System.Drawing.Size(271, 20)
        Me.txtServiceURL.TabIndex = 1
        Me.txtServiceURL.Text = "http://localhost:8010/services"
        '
        'lblServiceURL
        '
        Me.lblServiceURL.AutoSize = True
        Me.lblServiceURL.Location = New System.Drawing.Point(9, 20)
        Me.lblServiceURL.Name = "lblServiceURL"
        Me.lblServiceURL.Size = New System.Drawing.Size(75, 13)
        Me.lblServiceURL.TabIndex = 0
        Me.lblServiceURL.Text = "lblServiceURL"
        '
        'SettingsDialog
        '
        Me.AcceptButton = Me.btnOk
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(454, 296)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOk)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SettingsDialog"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Options"
        Me.TabControl1.ResumeLayout(False)
        Me.tabGeneral.ResumeLayout(False)
        Me.tabGeneral.PerformLayout()
        Me.tabFileSettings.ResumeLayout(False)
        Me.tabFileSettings.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.tabScannerButtons.ResumeLayout(False)
        Me.tabScannerButtons.PerformLayout()
        Me.tabExactSettings.ResumeLayout(False)
        Me.tabExactSettings.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents lblLanguage As System.Windows.Forms.Label
    Friend WithEvents cboLanguage As System.Windows.Forms.ComboBox
    Friend WithEvents chkRememberWindowPos As System.Windows.Forms.CheckBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents lblBitDepth As System.Windows.Forms.Label
    Friend WithEvents cboBitDepth As System.Windows.Forms.ComboBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents tabGeneral As System.Windows.Forms.TabPage
    Friend WithEvents chkRememberScanSettings As System.Windows.Forms.CheckBox
    Friend WithEvents tabScannerButtons As System.Windows.Forms.TabPage
    Friend WithEvents btnRegister As System.Windows.Forms.Button
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents lblScanner As System.Windows.Forms.Label
    Friend WithEvents lblAvailableEvents As System.Windows.Forms.Label
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents lblNote As System.Windows.Forms.Label
    Friend WithEvents chkUpdates As System.Windows.Forms.CheckBox
    Friend WithEvents btnResetScanSettings As System.Windows.Forms.Button
    Friend WithEvents tabFileSettings As System.Windows.Forms.TabPage
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblSaveToPDF As System.Windows.Forms.Label
    Friend WithEvents chkOpenPDF As System.Windows.Forms.CheckBox
    Friend WithEvents btnBrowsePDF As System.Windows.Forms.Button
    Friend WithEvents rbAskPDF As System.Windows.Forms.RadioButton
    Friend WithEvents txtPathPDF As System.Windows.Forms.TextBox
    Friend WithEvents rbPathPDF As System.Windows.Forms.RadioButton
    Friend WithEvents chkOpenFile As System.Windows.Forms.CheckBox
    Friend WithEvents btnBrowseFile As System.Windows.Forms.Button
    Friend WithEvents txtPathFile As System.Windows.Forms.TextBox
    Friend WithEvents rbPathFile As System.Windows.Forms.RadioButton
    Friend WithEvents rbAskFile As System.Windows.Forms.RadioButton
    Friend WithEvents lblSaveToFile As System.Windows.Forms.Label
    Friend WithEvents tabExactSettings As System.Windows.Forms.TabPage
    Friend WithEvents lblAdministrations As System.Windows.Forms.Label
    Friend WithEvents txtAdministrations As System.Windows.Forms.TextBox
    Friend WithEvents txtSQLserver As System.Windows.Forms.TextBox
    Friend WithEvents lblSQLserver As System.Windows.Forms.Label
    Friend WithEvents txtServiceURL As System.Windows.Forms.TextBox
    Friend WithEvents lblServiceURL As System.Windows.Forms.Label
    Friend WithEvents cmdTestExactSettings As System.Windows.Forms.Button
    Friend WithEvents lstExactTestConnection As System.Windows.Forms.ListBox

End Class
