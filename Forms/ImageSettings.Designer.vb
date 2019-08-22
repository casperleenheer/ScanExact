<System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId:="frm")> <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImageSettings
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
        Me.tbScaling = New System.Windows.Forms.TrackBar()
        Me.lblScaling = New System.Windows.Forms.Label()
        Me.lblResolution = New System.Windows.Forms.Label()
        Me.tbContrast = New System.Windows.Forms.TrackBar()
        Me.lblBrightness = New System.Windows.Forms.Label()
        Me.tbBrightness = New System.Windows.Forms.TrackBar()
        Me.txtContrast = New System.Windows.Forms.TextBox()
        Me.lblContrast = New System.Windows.Forms.Label()
        Me.txtBrightness = New System.Windows.Forms.TextBox()
        Me.txtScaling = New System.Windows.Forms.TextBox()
        Me.lblPerc = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cboResolution = New System.Windows.Forms.ComboBox()
        Me.lblDPI = New System.Windows.Forms.Label()
        Me.lblCompressionLabel = New System.Windows.Forms.Label()
        Me.tbCompression = New System.Windows.Forms.TrackBar()
        Me.lblCompression = New System.Windows.Forms.Label()
        Me.btnDefault = New System.Windows.Forms.Button()
        Me.chkCenter = New System.Windows.Forms.CheckBox()
        CType(Me.tbScaling, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbContrast, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbBrightness, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbCompression, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tbScaling
        '
        Me.tbScaling.LargeChange = 50
        Me.tbScaling.Location = New System.Drawing.Point(86, 74)
        Me.tbScaling.Maximum = 200
        Me.tbScaling.Minimum = 1
        Me.tbScaling.Name = "tbScaling"
        Me.tbScaling.Size = New System.Drawing.Size(101, 45)
        Me.tbScaling.SmallChange = 10
        Me.tbScaling.TabIndex = 39
        Me.tbScaling.TickFrequency = 50
        Me.tbScaling.Value = 100
        '
        'lblScaling
        '
        Me.lblScaling.Location = New System.Drawing.Point(0, 77)
        Me.lblScaling.Name = "lblScaling"
        Me.lblScaling.Size = New System.Drawing.Size(80, 13)
        Me.lblScaling.TabIndex = 38
        Me.lblScaling.Text = "lblScaling"
        Me.lblScaling.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblResolution
        '
        Me.lblResolution.Location = New System.Drawing.Point(4, 114)
        Me.lblResolution.Name = "lblResolution"
        Me.lblResolution.Size = New System.Drawing.Size(80, 13)
        Me.lblResolution.TabIndex = 36
        Me.lblResolution.Text = "lblResolution"
        Me.lblResolution.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'tbContrast
        '
        Me.tbContrast.LargeChange = 10
        Me.tbContrast.Location = New System.Drawing.Point(86, 43)
        Me.tbContrast.Maximum = 100
        Me.tbContrast.Minimum = -100
        Me.tbContrast.Name = "tbContrast"
        Me.tbContrast.Size = New System.Drawing.Size(101, 45)
        Me.tbContrast.TabIndex = 32
        Me.tbContrast.TickFrequency = 20
        '
        'lblBrightness
        '
        Me.lblBrightness.Location = New System.Drawing.Point(1, 15)
        Me.lblBrightness.Name = "lblBrightness"
        Me.lblBrightness.Size = New System.Drawing.Size(83, 13)
        Me.lblBrightness.TabIndex = 34
        Me.lblBrightness.Text = "lblBrightness"
        Me.lblBrightness.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'tbBrightness
        '
        Me.tbBrightness.LargeChange = 10
        Me.tbBrightness.Location = New System.Drawing.Point(86, 12)
        Me.tbBrightness.Maximum = 100
        Me.tbBrightness.Minimum = -100
        Me.tbBrightness.Name = "tbBrightness"
        Me.tbBrightness.Size = New System.Drawing.Size(101, 45)
        Me.tbBrightness.TabIndex = 30
        Me.tbBrightness.TickFrequency = 20
        '
        'txtContrast
        '
        Me.txtContrast.Location = New System.Drawing.Point(193, 43)
        Me.txtContrast.MaxLength = 4
        Me.txtContrast.Name = "txtContrast"
        Me.txtContrast.Size = New System.Drawing.Size(37, 20)
        Me.txtContrast.TabIndex = 33
        '
        'lblContrast
        '
        Me.lblContrast.Location = New System.Drawing.Point(4, 46)
        Me.lblContrast.Name = "lblContrast"
        Me.lblContrast.Size = New System.Drawing.Size(80, 13)
        Me.lblContrast.TabIndex = 35
        Me.lblContrast.Text = "lblContrast"
        Me.lblContrast.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtBrightness
        '
        Me.txtBrightness.Location = New System.Drawing.Point(193, 12)
        Me.txtBrightness.MaxLength = 4
        Me.txtBrightness.Name = "txtBrightness"
        Me.txtBrightness.Size = New System.Drawing.Size(37, 20)
        Me.txtBrightness.TabIndex = 31
        '
        'txtScaling
        '
        Me.txtScaling.Location = New System.Drawing.Point(193, 74)
        Me.txtScaling.MaxLength = 3
        Me.txtScaling.Name = "txtScaling"
        Me.txtScaling.Size = New System.Drawing.Size(28, 20)
        Me.txtScaling.TabIndex = 41
        '
        'lblPerc
        '
        Me.lblPerc.AutoSize = True
        Me.lblPerc.Location = New System.Drawing.Point(221, 77)
        Me.lblPerc.Name = "lblPerc"
        Me.lblPerc.Size = New System.Drawing.Size(15, 13)
        Me.lblPerc.TabIndex = 44
        Me.lblPerc.Text = "%"
        '
        'cboResolution
        '
        Me.cboResolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboResolution.FormattingEnabled = True
        Me.cboResolution.Location = New System.Drawing.Point(90, 111)
        Me.cboResolution.Name = "cboResolution"
        Me.cboResolution.Size = New System.Drawing.Size(88, 21)
        Me.cboResolution.TabIndex = 45
        '
        'lblDPI
        '
        Me.lblDPI.AutoSize = True
        Me.lblDPI.Location = New System.Drawing.Point(193, 114)
        Me.lblDPI.Name = "lblDPI"
        Me.lblDPI.Size = New System.Drawing.Size(35, 13)
        Me.lblDPI.TabIndex = 46
        Me.lblDPI.Text = "lblDPI"
        '
        'lblCompressionLabel
        '
        Me.lblCompressionLabel.Location = New System.Drawing.Point(0, 138)
        Me.lblCompressionLabel.Name = "lblCompressionLabel"
        Me.lblCompressionLabel.Size = New System.Drawing.Size(80, 33)
        Me.lblCompressionLabel.TabIndex = 47
        Me.lblCompressionLabel.Text = "JPEG Compression"
        Me.lblCompressionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tbCompression
        '
        Me.tbCompression.Location = New System.Drawing.Point(86, 148)
        Me.tbCompression.Maximum = 100
        Me.tbCompression.Minimum = 1
        Me.tbCompression.Name = "tbCompression"
        Me.tbCompression.Size = New System.Drawing.Size(101, 45)
        Me.tbCompression.TabIndex = 48
        Me.tbCompression.TickFrequency = 10
        Me.tbCompression.Value = 100
        '
        'lblCompression
        '
        Me.lblCompression.Location = New System.Drawing.Point(193, 148)
        Me.lblCompression.Name = "lblCompression"
        Me.lblCompression.Size = New System.Drawing.Size(28, 23)
        Me.lblCompression.TabIndex = 49
        '
        'btnDefault
        '
        Me.btnDefault.Location = New System.Drawing.Point(155, 179)
        Me.btnDefault.Name = "btnDefault"
        Me.btnDefault.Size = New System.Drawing.Size(75, 23)
        Me.btnDefault.TabIndex = 50
        Me.btnDefault.Text = "Default"
        Me.btnDefault.UseVisualStyleBackColor = True
        '
        'chkCenter
        '
        Me.chkCenter.AutoSize = True
        Me.chkCenter.Checked = Global.ScanExact.My.MySettings.Default.Center
        Me.chkCenter.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkCenter.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Global.ScanExact.My.MySettings.Default, "Center", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.chkCenter.Location = New System.Drawing.Point(12, 183)
        Me.chkCenter.Name = "chkCenter"
        Me.chkCenter.Size = New System.Drawing.Size(75, 17)
        Me.chkCenter.TabIndex = 51
        Me.chkCenter.Text = "chkCenter"
        Me.chkCenter.UseVisualStyleBackColor = True
        '
        'frmImageSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(242, 210)
        Me.Controls.Add(Me.chkCenter)
        Me.Controls.Add(Me.btnDefault)
        Me.Controls.Add(Me.lblCompression)
        Me.Controls.Add(Me.cboResolution)
        Me.Controls.Add(Me.tbScaling)
        Me.Controls.Add(Me.tbCompression)
        Me.Controls.Add(Me.lblCompressionLabel)
        Me.Controls.Add(Me.lblDPI)
        Me.Controls.Add(Me.lblResolution)
        Me.Controls.Add(Me.lblBrightness)
        Me.Controls.Add(Me.txtContrast)
        Me.Controls.Add(Me.lblContrast)
        Me.Controls.Add(Me.txtBrightness)
        Me.Controls.Add(Me.tbContrast)
        Me.Controls.Add(Me.tbBrightness)
        Me.Controls.Add(Me.lblPerc)
        Me.Controls.Add(Me.txtScaling)
        Me.Controls.Add(Me.lblScaling)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.KeyPreview = True
        Me.Name = "frmImageSettings"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Image Settings"
        CType(Me.tbScaling, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbContrast, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbBrightness, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbCompression, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tbScaling As System.Windows.Forms.TrackBar
    Friend WithEvents lblScaling As System.Windows.Forms.Label
    Friend WithEvents lblResolution As System.Windows.Forms.Label
    Friend WithEvents tbContrast As System.Windows.Forms.TrackBar
    Friend WithEvents lblBrightness As System.Windows.Forms.Label
    Friend WithEvents tbBrightness As System.Windows.Forms.TrackBar
    Friend WithEvents txtContrast As System.Windows.Forms.TextBox
    Friend WithEvents lblContrast As System.Windows.Forms.Label
    Friend WithEvents txtBrightness As System.Windows.Forms.TextBox
    Friend WithEvents txtScaling As System.Windows.Forms.TextBox
    Friend WithEvents lblPerc As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents cboResolution As System.Windows.Forms.ComboBox
    Friend WithEvents lblDPI As System.Windows.Forms.Label
    Friend WithEvents lblCompressionLabel As System.Windows.Forms.Label
    Friend WithEvents tbCompression As System.Windows.Forms.TrackBar
    Friend WithEvents lblCompression As System.Windows.Forms.Label
    Friend WithEvents btnDefault As System.Windows.Forms.Button
    Friend WithEvents chkCenter As System.Windows.Forms.CheckBox
End Class
