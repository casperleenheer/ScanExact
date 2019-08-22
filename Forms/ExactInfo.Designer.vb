<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ExactInfo
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
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.lblOrderNo = New System.Windows.Forms.Label()
        Me.lblFullname = New System.Windows.Forms.Label()
        Me.lblAddress = New System.Windows.Forms.Label()
        Me.lblZipCity = New System.Windows.Forms.Label()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.cmdOk = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblZipZityData = New System.Windows.Forms.Label()
        Me.lblAddressData = New System.Windows.Forms.Label()
        Me.lblFullnameData = New System.Windows.Forms.Label()
        Me.lblOrderNoData = New System.Windows.Forms.Label()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.Location = New System.Drawing.Point(11, 22)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(61, 13)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "lblHeader"
        '
        'lblOrderNo
        '
        Me.lblOrderNo.AutoSize = True
        Me.lblOrderNo.Location = New System.Drawing.Point(16, 13)
        Me.lblOrderNo.Name = "lblOrderNo"
        Me.lblOrderNo.Size = New System.Drawing.Size(57, 13)
        Me.lblOrderNo.TabIndex = 1
        Me.lblOrderNo.Text = "lblOrderNo"
        '
        'lblFullname
        '
        Me.lblFullname.AutoSize = True
        Me.lblFullname.Location = New System.Drawing.Point(16, 37)
        Me.lblFullname.Name = "lblFullname"
        Me.lblFullname.Size = New System.Drawing.Size(59, 13)
        Me.lblFullname.TabIndex = 2
        Me.lblFullname.Text = "lblFullname"
        '
        'lblAddress
        '
        Me.lblAddress.AutoSize = True
        Me.lblAddress.Location = New System.Drawing.Point(16, 61)
        Me.lblAddress.Name = "lblAddress"
        Me.lblAddress.Size = New System.Drawing.Size(55, 13)
        Me.lblAddress.TabIndex = 3
        Me.lblAddress.Text = "lblAddress"
        '
        'lblZipCity
        '
        Me.lblZipCity.AutoSize = True
        Me.lblZipCity.Location = New System.Drawing.Point(16, 85)
        Me.lblZipCity.Name = "lblZipCity"
        Me.lblZipCity.Size = New System.Drawing.Size(49, 13)
        Me.lblZipCity.TabIndex = 4
        Me.lblZipCity.Text = "lblZipCity"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.Location = New System.Drawing.Point(218, 180)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.cmdCancel)
        Me.SplitContainer1.Panel1MinSize = 95
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.cmdOk)
        Me.SplitContainer1.Panel2MinSize = 95
        Me.SplitContainer1.Size = New System.Drawing.Size(194, 24)
        Me.SplitContainer1.SplitterDistance = 95
        Me.SplitContainer1.TabIndex = 5
        '
        'cmdCancel
        '
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmdCancel.Location = New System.Drawing.Point(0, 0)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(95, 24)
        Me.cmdCancel.TabIndex = 0
        Me.cmdCancel.Text = "cmdCancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'cmdOk
        '
        Me.cmdOk.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.cmdOk.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmdOk.Location = New System.Drawing.Point(0, 0)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.Size = New System.Drawing.Size(95, 24)
        Me.cmdOk.TabIndex = 0
        Me.cmdOk.Text = "cmdOk"
        Me.cmdOk.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.LightSkyBlue
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.lblZipZityData)
        Me.Panel1.Controls.Add(Me.lblAddressData)
        Me.Panel1.Controls.Add(Me.lblFullnameData)
        Me.Panel1.Controls.Add(Me.lblOrderNoData)
        Me.Panel1.Controls.Add(Me.lblOrderNo)
        Me.Panel1.Controls.Add(Me.lblFullname)
        Me.Panel1.Controls.Add(Me.lblZipCity)
        Me.Panel1.Controls.Add(Me.lblAddress)
        Me.Panel1.Location = New System.Drawing.Point(-5, 47)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(430, 110)
        Me.Panel1.TabIndex = 6
        '
        'lblZipZityData
        '
        Me.lblZipZityData.AutoSize = True
        Me.lblZipZityData.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblZipZityData.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblZipZityData.Location = New System.Drawing.Point(127, 81)
        Me.lblZipZityData.Name = "lblZipZityData"
        Me.lblZipZityData.Size = New System.Drawing.Size(109, 17)
        Me.lblZipZityData.TabIndex = 8
        Me.lblZipZityData.Text = "lblZipCityData"
        '
        'lblAddressData
        '
        Me.lblAddressData.AutoSize = True
        Me.lblAddressData.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddressData.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAddressData.Location = New System.Drawing.Point(127, 57)
        Me.lblAddressData.Name = "lblAddressData"
        Me.lblAddressData.Size = New System.Drawing.Size(118, 17)
        Me.lblAddressData.TabIndex = 7
        Me.lblAddressData.Text = "lblAddressData"
        '
        'lblFullnameData
        '
        Me.lblFullnameData.AutoSize = True
        Me.lblFullnameData.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFullnameData.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblFullnameData.Location = New System.Drawing.Point(127, 33)
        Me.lblFullnameData.Name = "lblFullnameData"
        Me.lblFullnameData.Size = New System.Drawing.Size(124, 17)
        Me.lblFullnameData.TabIndex = 6
        Me.lblFullnameData.Text = "lblFullnameData"
        '
        'lblOrderNoData
        '
        Me.lblOrderNoData.AutoSize = True
        Me.lblOrderNoData.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOrderNoData.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblOrderNoData.Location = New System.Drawing.Point(127, 9)
        Me.lblOrderNoData.Name = "lblOrderNoData"
        Me.lblOrderNoData.Size = New System.Drawing.Size(121, 17)
        Me.lblOrderNoData.TabIndex = 5
        Me.lblOrderNoData.Text = "lblOrderNoData"
        '
        'ExactInfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(424, 216)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.lblHeader)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "ExactInfo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ExactInfo"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents lblOrderNo As System.Windows.Forms.Label
    Friend WithEvents lblFullname As System.Windows.Forms.Label
    Friend WithEvents lblAddress As System.Windows.Forms.Label
    Friend WithEvents lblZipCity As System.Windows.Forms.Label
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents cmdOk As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblZipZityData As System.Windows.Forms.Label
    Friend WithEvents lblAddressData As System.Windows.Forms.Label
    Friend WithEvents lblFullnameData As System.Windows.Forms.Label
    Friend WithEvents lblOrderNoData As System.Windows.Forms.Label
End Class
