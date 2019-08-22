<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class mainFrm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(mainFrm))
        Me.btnCopy = New System.Windows.Forms.Button()
        Me.btnSelScanner = New System.Windows.Forms.Button()
        Me.cboPrintMode = New System.Windows.Forms.ComboBox()
        Me.lblPrinter = New System.Windows.Forms.Label()
        Me.lblScanner = New System.Windows.Forms.Label()
        Me.cboScanMode = New System.Windows.Forms.ComboBox()
        Me.btnPrintSetup = New System.Windows.Forms.Button()
        Me.nudNCopie = New System.Windows.Forms.NumericUpDown()
        Me.lblCopies = New System.Windows.Forms.Label()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ScannerStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.PrinterStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ExactStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.VersionStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.llblSettings = New System.Windows.Forms.LinkLabel()
        Me.btnImageSettings = New System.Windows.Forms.Button()
        Me.ScanMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ScanMultiplePages = New System.Windows.Forms.ToolStripMenuItem()
        Me.ScanToFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.cboPaperSize = New System.Windows.Forms.ComboBox()
        Me.lblPaperSize = New System.Windows.Forms.Label()
        Me.chkADF = New System.Windows.Forms.CheckBox()
        Me.chkDuplex = New System.Windows.Forms.CheckBox()
        Me.chkMultipage = New System.Windows.Forms.CheckBox()
        Me.chkSaveToFile = New System.Windows.Forms.CheckBox()
        Me.chkPDF = New System.Windows.Forms.CheckBox()
        Me.chkPreview = New System.Windows.Forms.CheckBox()
        Me.gpbOrder = New System.Windows.Forms.GroupBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.txtOnderwerp = New System.Windows.Forms.TextBox()
        Me.lblSubject = New System.Windows.Forms.Label()
        Me.lblTypeOfDocument = New System.Windows.Forms.Label()
        Me.cboDocumentType = New System.Windows.Forms.ComboBox()
        Me.grpOrderNumber = New System.Windows.Forms.GroupBox()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.txtDossiernummer = New System.Windows.Forms.TextBox()
        Me.chkExact = New System.Windows.Forms.CheckBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblSaveToFile = New System.Windows.Forms.Label()
        Me.lblSaveAsPdf = New System.Windows.Forms.Label()
        Me.HelpProvider1 = New System.Windows.Forms.HelpProvider()
        CType(Me.nudNCopie, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        Me.ScanMenuStrip.SuspendLayout()
        Me.gpbOrder.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.grpOrderNumber.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnCopy
        '
        Me.btnCopy.Font = New System.Drawing.Font("Consolas", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCopy.Image = Global.ScanExact.My.Resources.Resources.scanner1
        Me.btnCopy.Location = New System.Drawing.Point(434, 135)
        Me.btnCopy.Name = "btnCopy"
        Me.btnCopy.Size = New System.Drawing.Size(88, 88)
        Me.btnCopy.TabIndex = 0
        Me.btnCopy.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnCopy.UseVisualStyleBackColor = True
        '
        'btnSelScanner
        '
        Me.btnSelScanner.Location = New System.Drawing.Point(323, 11)
        Me.btnSelScanner.Name = "btnSelScanner"
        Me.btnSelScanner.Size = New System.Drawing.Size(199, 22)
        Me.btnSelScanner.TabIndex = 7
        Me.btnSelScanner.Text = "btnSelScanner"
        Me.btnSelScanner.UseVisualStyleBackColor = True
        '
        'cboPrintMode
        '
        Me.cboPrintMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPrintMode.FormattingEnabled = True
        Me.cboPrintMode.Location = New System.Drawing.Point(119, 40)
        Me.cboPrintMode.Name = "cboPrintMode"
        Me.cboPrintMode.Size = New System.Drawing.Size(149, 21)
        Me.cboPrintMode.TabIndex = 3
        '
        'lblPrinter
        '
        Me.lblPrinter.Location = New System.Drawing.Point(12, 43)
        Me.lblPrinter.Name = "lblPrinter"
        Me.lblPrinter.Size = New System.Drawing.Size(101, 18)
        Me.lblPrinter.TabIndex = 2
        Me.lblPrinter.Text = "lblPrinter"
        '
        'lblScanner
        '
        Me.lblScanner.Location = New System.Drawing.Point(12, 16)
        Me.lblScanner.Name = "lblScanner"
        Me.lblScanner.Size = New System.Drawing.Size(101, 18)
        Me.lblScanner.TabIndex = 1
        Me.lblScanner.Text = "lblScanner"
        '
        'cboScanMode
        '
        Me.cboScanMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboScanMode.FormattingEnabled = True
        Me.cboScanMode.Location = New System.Drawing.Point(119, 13)
        Me.cboScanMode.Name = "cboScanMode"
        Me.cboScanMode.Size = New System.Drawing.Size(149, 21)
        Me.cboScanMode.TabIndex = 2
        '
        'btnPrintSetup
        '
        Me.btnPrintSetup.Location = New System.Drawing.Point(323, 38)
        Me.btnPrintSetup.Name = "btnPrintSetup"
        Me.btnPrintSetup.Size = New System.Drawing.Size(199, 22)
        Me.btnPrintSetup.TabIndex = 6
        Me.btnPrintSetup.Text = "btnPrintSetup"
        Me.btnPrintSetup.UseVisualStyleBackColor = True
        '
        'nudNCopie
        '
        Me.nudNCopie.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nudNCopie.Location = New System.Drawing.Point(466, 108)
        Me.nudNCopie.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudNCopie.Name = "nudNCopie"
        Me.nudNCopie.Size = New System.Drawing.Size(55, 20)
        Me.nudNCopie.TabIndex = 1
        Me.nudNCopie.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nudNCopie.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'lblCopies
        '
        Me.lblCopies.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCopies.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCopies.Location = New System.Drawing.Point(239, 106)
        Me.lblCopies.Name = "lblCopies"
        Me.lblCopies.Size = New System.Drawing.Size(222, 19)
        Me.lblCopies.TabIndex = 12
        Me.lblCopies.Text = "lblCopies"
        Me.lblCopies.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'StatusStrip1
        '
        Me.StatusStrip1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ScannerStatusLabel, Me.PrinterStatusLabel, Me.ExactStatusLabel, Me.VersionStatusLabel})
        Me.StatusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 375)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(538, 21)
        Me.StatusStrip1.SizingGrip = False
        Me.StatusStrip1.TabIndex = 20
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ScannerStatusLabel
        '
        Me.ScannerStatusLabel.Image = Global.ScanExact.My.Resources.Resources.scanner
        Me.ScannerStatusLabel.Margin = New System.Windows.Forms.Padding(0, 3, 6, 2)
        Me.ScannerStatusLabel.Name = "ScannerStatusLabel"
        Me.ScannerStatusLabel.Size = New System.Drawing.Size(125, 16)
        Me.ScannerStatusLabel.Text = "ScannerStatusLabel"
        '
        'PrinterStatusLabel
        '
        Me.PrinterStatusLabel.Image = Global.ScanExact.My.Resources.Resources.printer
        Me.PrinterStatusLabel.Margin = New System.Windows.Forms.Padding(0, 3, 6, 2)
        Me.PrinterStatusLabel.Name = "PrinterStatusLabel"
        Me.PrinterStatusLabel.Size = New System.Drawing.Size(118, 16)
        Me.PrinterStatusLabel.Text = "PrinterStatusLabel"
        '
        'ExactStatusLabel
        '
        Me.ExactStatusLabel.Image = Global.ScanExact.My.Resources.Resources.imagesT4HFJO6L
        Me.ExactStatusLabel.Name = "ExactStatusLabel"
        Me.ExactStatusLabel.Size = New System.Drawing.Size(110, 16)
        Me.ExactStatusLabel.Text = "ExactStatusLabel"
        '
        'VersionStatusLabel
        '
        Me.VersionStatusLabel.IsLink = True
        Me.VersionStatusLabel.Name = "VersionStatusLabel"
        Me.VersionStatusLabel.Size = New System.Drawing.Size(127, 15)
        Me.VersionStatusLabel.Spring = True
        Me.VersionStatusLabel.Text = "New Version Available!"
        Me.VersionStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.VersionStatusLabel.Visible = False
        '
        'ToolTip1
        '
        Me.ToolTip1.AutomaticDelay = 300
        '
        'llblSettings
        '
        Me.llblSettings.Location = New System.Drawing.Point(12, 205)
        Me.llblSettings.Name = "llblSettings"
        Me.llblSettings.Size = New System.Drawing.Size(144, 13)
        Me.llblSettings.TabIndex = 9
        Me.llblSettings.TabStop = True
        Me.llblSettings.Text = "llblSettings"
        '
        'btnImageSettings
        '
        Me.btnImageSettings.Location = New System.Drawing.Point(323, 65)
        Me.btnImageSettings.Name = "btnImageSettings"
        Me.btnImageSettings.Size = New System.Drawing.Size(199, 22)
        Me.btnImageSettings.TabIndex = 5
        Me.btnImageSettings.Text = "btnImageSettings"
        Me.btnImageSettings.UseVisualStyleBackColor = True
        '
        'ScanMenuStrip
        '
        Me.ScanMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ScanMultiplePages, Me.ScanToFile})
        Me.ScanMenuStrip.Name = "ScanMenuStrip"
        Me.ScanMenuStrip.Size = New System.Drawing.Size(229, 48)
        '
        'ScanMultiplePages
        '
        Me.ScanMultiplePages.Name = "ScanMultiplePages"
        Me.ScanMultiplePages.ShortcutKeyDisplayString = "Ctrl +M"
        Me.ScanMultiplePages.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.M), System.Windows.Forms.Keys)
        Me.ScanMultiplePages.Size = New System.Drawing.Size(228, 22)
        Me.ScanMultiplePages.Text = "Scan Multiple Pages"
        '
        'ScanToFile
        '
        Me.ScanToFile.Name = "ScanToFile"
        Me.ScanToFile.ShortcutKeyDisplayString = "Ctrl+F"
        Me.ScanToFile.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F), System.Windows.Forms.Keys)
        Me.ScanToFile.Size = New System.Drawing.Size(228, 22)
        Me.ScanToFile.Text = "Scan to &File"
        '
        'cboPaperSize
        '
        Me.cboPaperSize.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.cboPaperSize.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboPaperSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPaperSize.DropDownWidth = 150
        Me.cboPaperSize.FormattingEnabled = True
        Me.cboPaperSize.Location = New System.Drawing.Point(119, 67)
        Me.cboPaperSize.Name = "cboPaperSize"
        Me.cboPaperSize.Size = New System.Drawing.Size(149, 21)
        Me.cboPaperSize.TabIndex = 21
        '
        'lblPaperSize
        '
        Me.lblPaperSize.Location = New System.Drawing.Point(12, 70)
        Me.lblPaperSize.Name = "lblPaperSize"
        Me.lblPaperSize.Size = New System.Drawing.Size(101, 23)
        Me.lblPaperSize.TabIndex = 22
        Me.lblPaperSize.Text = "lblPaperSize"
        '
        'chkADF
        '
        Me.chkADF.Location = New System.Drawing.Point(14, 151)
        Me.chkADF.Name = "chkADF"
        Me.chkADF.Size = New System.Drawing.Size(254, 21)
        Me.chkADF.TabIndex = 23
        Me.chkADF.Text = "chkADF"
        Me.chkADF.UseVisualStyleBackColor = True
        '
        'chkDuplex
        '
        Me.chkDuplex.AutoSize = True
        Me.chkDuplex.Location = New System.Drawing.Point(14, 174)
        Me.chkDuplex.Name = "chkDuplex"
        Me.chkDuplex.Size = New System.Drawing.Size(77, 17)
        Me.chkDuplex.TabIndex = 24
        Me.chkDuplex.Text = "chkDuplex"
        Me.chkDuplex.UseVisualStyleBackColor = True
        '
        'chkMultipage
        '
        Me.chkMultipage.Appearance = System.Windows.Forms.Appearance.Button
        Me.chkMultipage.Image = CType(resources.GetObject("chkMultipage.Image"), System.Drawing.Image)
        Me.chkMultipage.Location = New System.Drawing.Point(386, 135)
        Me.chkMultipage.Name = "chkMultipage"
        Me.chkMultipage.Size = New System.Drawing.Size(42, 42)
        Me.chkMultipage.TabIndex = 26
        Me.chkMultipage.UseVisualStyleBackColor = True
        '
        'chkSaveToFile
        '
        Me.chkSaveToFile.Appearance = System.Windows.Forms.Appearance.Button
        Me.chkSaveToFile.Image = CType(resources.GetObject("chkSaveToFile.Image"), System.Drawing.Image)
        Me.chkSaveToFile.Location = New System.Drawing.Point(338, 135)
        Me.chkSaveToFile.Name = "chkSaveToFile"
        Me.chkSaveToFile.Size = New System.Drawing.Size(42, 42)
        Me.chkSaveToFile.TabIndex = 26
        Me.chkSaveToFile.UseVisualStyleBackColor = True
        '
        'chkPDF
        '
        Me.chkPDF.Appearance = System.Windows.Forms.Appearance.Button
        Me.chkPDF.Image = CType(resources.GetObject("chkPDF.Image"), System.Drawing.Image)
        Me.chkPDF.Location = New System.Drawing.Point(290, 135)
        Me.chkPDF.Name = "chkPDF"
        Me.chkPDF.Size = New System.Drawing.Size(42, 42)
        Me.chkPDF.TabIndex = 26
        Me.chkPDF.UseVisualStyleBackColor = True
        '
        'chkPreview
        '
        Me.chkPreview.AutoSize = True
        Me.chkPreview.Location = New System.Drawing.Point(14, 130)
        Me.chkPreview.Name = "chkPreview"
        Me.chkPreview.Size = New System.Drawing.Size(82, 17)
        Me.chkPreview.TabIndex = 4
        Me.chkPreview.Text = "chkPreview"
        Me.chkPreview.UseVisualStyleBackColor = True
        '
        'gpbOrder
        '
        Me.gpbOrder.BackColor = System.Drawing.Color.LightSkyBlue
        Me.gpbOrder.Controls.Add(Me.GroupBox3)
        Me.gpbOrder.Controls.Add(Me.grpOrderNumber)
        Me.gpbOrder.Dock = System.Windows.Forms.DockStyle.Top
        Me.gpbOrder.Location = New System.Drawing.Point(0, 0)
        Me.gpbOrder.Name = "gpbOrder"
        Me.gpbOrder.Size = New System.Drawing.Size(538, 134)
        Me.gpbOrder.TabIndex = 27
        Me.gpbOrder.TabStop = False
        Me.gpbOrder.Text = "gpbOrder"
        Me.gpbOrder.Visible = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.txtOnderwerp)
        Me.GroupBox3.Controls.Add(Me.lblSubject)
        Me.GroupBox3.Controls.Add(Me.lblTypeOfDocument)
        Me.GroupBox3.Controls.Add(Me.cboDocumentType)
        Me.GroupBox3.Location = New System.Drawing.Point(222, 20)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(300, 92)
        Me.GroupBox3.TabIndex = 29
        Me.GroupBox3.TabStop = False
        '
        'txtOnderwerp
        '
        Me.txtOnderwerp.Location = New System.Drawing.Point(113, 41)
        Me.txtOnderwerp.Multiline = True
        Me.txtOnderwerp.Name = "txtOnderwerp"
        Me.txtOnderwerp.Size = New System.Drawing.Size(181, 45)
        Me.txtOnderwerp.TabIndex = 3
        '
        'lblSubject
        '
        Me.lblSubject.AutoSize = True
        Me.lblSubject.Location = New System.Drawing.Point(7, 44)
        Me.lblSubject.Name = "lblSubject"
        Me.lblSubject.Size = New System.Drawing.Size(53, 13)
        Me.lblSubject.TabIndex = 2
        Me.lblSubject.Text = "lblSubject"
        '
        'lblTypeOfDocument
        '
        Me.lblTypeOfDocument.AutoSize = True
        Me.lblTypeOfDocument.Location = New System.Drawing.Point(6, 22)
        Me.lblTypeOfDocument.Name = "lblTypeOfDocument"
        Me.lblTypeOfDocument.Size = New System.Drawing.Size(101, 13)
        Me.lblTypeOfDocument.TabIndex = 1
        Me.lblTypeOfDocument.Text = "lblTypeOfDocument"
        '
        'cboDocumentType
        '
        Me.cboDocumentType.FormattingEnabled = True
        Me.cboDocumentType.Location = New System.Drawing.Point(113, 19)
        Me.cboDocumentType.Name = "cboDocumentType"
        Me.cboDocumentType.Size = New System.Drawing.Size(181, 21)
        Me.cboDocumentType.TabIndex = 0
        '
        'grpOrderNumber
        '
        Me.grpOrderNumber.Controls.Add(Me.ProgressBar1)
        Me.grpOrderNumber.Controls.Add(Me.txtDossiernummer)
        Me.grpOrderNumber.Location = New System.Drawing.Point(12, 20)
        Me.grpOrderNumber.Name = "grpOrderNumber"
        Me.grpOrderNumber.Size = New System.Drawing.Size(201, 92)
        Me.grpOrderNumber.TabIndex = 29
        Me.grpOrderNumber.TabStop = False
        Me.grpOrderNumber.Text = "grpOrderNumber"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.BackColor = System.Drawing.Color.LightSkyBlue
        Me.ProgressBar1.Location = New System.Drawing.Point(6, 54)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(189, 3)
        Me.ProgressBar1.Step = 1
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.ProgressBar1.TabIndex = 29
        '
        'txtDossiernummer
        '
        Me.txtDossiernummer.Font = New System.Drawing.Font("Arial Rounded MT Bold", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDossiernummer.Location = New System.Drawing.Point(6, 19)
        Me.txtDossiernummer.Name = "txtDossiernummer"
        Me.txtDossiernummer.Size = New System.Drawing.Size(189, 35)
        Me.txtDossiernummer.TabIndex = 1
        Me.txtDossiernummer.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'chkExact
        '
        Me.chkExact.Appearance = System.Windows.Forms.Appearance.Button
        Me.chkExact.Image = CType(resources.GetObject("chkExact.Image"), System.Drawing.Image)
        Me.chkExact.Location = New System.Drawing.Point(290, 183)
        Me.chkExact.Name = "chkExact"
        Me.chkExact.Padding = New System.Windows.Forms.Padding(3, 1, 0, 0)
        Me.chkExact.Size = New System.Drawing.Size(138, 40)
        Me.chkExact.TabIndex = 28
        Me.chkExact.Text = "chkExact"
        Me.chkExact.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.chkExact.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lblSaveToFile)
        Me.Panel1.Controls.Add(Me.lblSaveAsPdf)
        Me.Panel1.Controls.Add(Me.lblScanner)
        Me.Panel1.Controls.Add(Me.chkADF)
        Me.Panel1.Controls.Add(Me.chkSaveToFile)
        Me.Panel1.Controls.Add(Me.chkPreview)
        Me.Panel1.Controls.Add(Me.chkExact)
        Me.Panel1.Controls.Add(Me.llblSettings)
        Me.Panel1.Controls.Add(Me.chkDuplex)
        Me.Panel1.Controls.Add(Me.chkMultipage)
        Me.Panel1.Controls.Add(Me.chkPDF)
        Me.Panel1.Controls.Add(Me.lblPaperSize)
        Me.Panel1.Controls.Add(Me.lblPrinter)
        Me.Panel1.Controls.Add(Me.btnCopy)
        Me.Panel1.Controls.Add(Me.cboPrintMode)
        Me.Panel1.Controls.Add(Me.btnImageSettings)
        Me.Panel1.Controls.Add(Me.cboPaperSize)
        Me.Panel1.Controls.Add(Me.btnSelScanner)
        Me.Panel1.Controls.Add(Me.cboScanMode)
        Me.Panel1.Controls.Add(Me.lblCopies)
        Me.Panel1.Controls.Add(Me.nudNCopie)
        Me.Panel1.Controls.Add(Me.btnPrintSetup)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 134)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(538, 237)
        Me.Panel1.TabIndex = 29
        '
        'lblSaveToFile
        '
        Me.lblSaveToFile.AutoSize = True
        Me.lblSaveToFile.Location = New System.Drawing.Point(12, 106)
        Me.lblSaveToFile.Name = "lblSaveToFile"
        Me.lblSaveToFile.Size = New System.Drawing.Size(71, 13)
        Me.lblSaveToFile.TabIndex = 30
        Me.lblSaveToFile.Text = "lblSaveToFile"
        Me.lblSaveToFile.Visible = False
        '
        'lblSaveAsPdf
        '
        Me.lblSaveAsPdf.AutoSize = True
        Me.lblSaveAsPdf.Location = New System.Drawing.Point(12, 106)
        Me.lblSaveAsPdf.Name = "lblSaveAsPdf"
        Me.lblSaveAsPdf.Size = New System.Drawing.Size(87, 13)
        Me.lblSaveAsPdf.TabIndex = 29
        Me.lblSaveAsPdf.Text = "lblExportToExact"
        Me.lblSaveAsPdf.Visible = False
        '
        'mainFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(538, 396)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.gpbOrder)
        Me.Controls.Add(Me.StatusStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "mainFrm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "ScanExact for Exact Software"
        CType(Me.nudNCopie, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ScanMenuStrip.ResumeLayout(False)
        Me.gpbOrder.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.grpOrderNumber.ResumeLayout(False)
        Me.grpOrderNumber.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnCopy As System.Windows.Forms.Button
    Friend WithEvents btnSelScanner As System.Windows.Forms.Button
    Friend WithEvents btnPrintSetup As System.Windows.Forms.Button
    Friend WithEvents nudNCopie As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblCopies As System.Windows.Forms.Label
    Friend WithEvents cboScanMode As System.Windows.Forms.ComboBox
    Friend WithEvents cboPrintMode As System.Windows.Forms.ComboBox
    Friend WithEvents lblPrinter As System.Windows.Forms.Label
    Friend WithEvents lblScanner As System.Windows.Forms.Label
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents llblSettings As System.Windows.Forms.LinkLabel
    Friend WithEvents btnImageSettings As System.Windows.Forms.Button
    Friend WithEvents ScanMenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ScanToFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ScanMultiplePages As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cboPaperSize As System.Windows.Forms.ComboBox
    Friend WithEvents lblPaperSize As System.Windows.Forms.Label
    Friend WithEvents ScannerStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents PrinterStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents VersionStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents chkADF As System.Windows.Forms.CheckBox
    Friend WithEvents chkDuplex As System.Windows.Forms.CheckBox
    Friend WithEvents chkMultipage As System.Windows.Forms.CheckBox
    Friend WithEvents chkSaveToFile As System.Windows.Forms.CheckBox
    Friend WithEvents chkPDF As System.Windows.Forms.CheckBox
    Friend WithEvents chkPreview As System.Windows.Forms.CheckBox
    Friend WithEvents gpbOrder As System.Windows.Forms.GroupBox
    Friend WithEvents txtDossiernummer As System.Windows.Forms.TextBox
    Friend WithEvents chkExact As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents grpOrderNumber As System.Windows.Forms.GroupBox
    Friend WithEvents ExactStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents cboDocumentType As System.Windows.Forms.ComboBox
    Friend WithEvents txtOnderwerp As System.Windows.Forms.TextBox
    Friend WithEvents lblSubject As System.Windows.Forms.Label
    Friend WithEvents lblTypeOfDocument As System.Windows.Forms.Label
    Friend WithEvents HelpProvider1 As System.Windows.Forms.HelpProvider
    Friend WithEvents lblSaveAsPdf As System.Windows.Forms.Label
    Friend WithEvents lblSaveToFile As System.Windows.Forms.Label
End Class
