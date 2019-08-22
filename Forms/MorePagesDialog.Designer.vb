<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgScanMorePages
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
        Me.btnAddPage = New System.Windows.Forms.Button()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.lblAcquiredPagesNText = New System.Windows.Forms.Label()
        Me.lblAcquiredPagesN = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btnAddPage
        '
        Me.btnAddPage.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnAddPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAddPage.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnAddPage.DialogResult = System.Windows.Forms.DialogResult.Yes
        Me.btnAddPage.Image = Global.ScanExact.My.Resources.Resources.scanner_big
        Me.btnAddPage.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnAddPage.Location = New System.Drawing.Point(12, 85)
        Me.btnAddPage.Name = "btnAddPage"
        Me.btnAddPage.Size = New System.Drawing.Size(125, 135)
        Me.btnAddPage.TabIndex = 0
        Me.btnAddPage.Text = "Scan Another Page"
        Me.btnAddPage.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnAddPage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnAddPage.UseVisualStyleBackColor = True
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.DialogResult = System.Windows.Forms.DialogResult.No
        Me.btnPrint.Image = Global.ScanExact.My.Resources.Resources.printer_big
        Me.btnPrint.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnPrint.Location = New System.Drawing.Point(159, 85)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(125, 135)
        Me.btnPrint.TabIndex = 1
        Me.btnPrint.Text = "Print Pages"
        Me.btnPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(272, 35)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Choose if you want to add more pages or if you want to print the acquired pages."
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblAcquiredPagesNText
        '
        Me.lblAcquiredPagesNText.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblAcquiredPagesNText.Location = New System.Drawing.Point(25, 54)
        Me.lblAcquiredPagesNText.Name = "lblAcquiredPagesNText"
        Me.lblAcquiredPagesNText.Size = New System.Drawing.Size(125, 13)
        Me.lblAcquiredPagesNText.TabIndex = 3
        Me.lblAcquiredPagesNText.Text = "lblAcquiredPagesNText"
        Me.lblAcquiredPagesNText.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblAcquiredPagesN
        '
        Me.lblAcquiredPagesN.AutoSize = True
        Me.lblAcquiredPagesN.Location = New System.Drawing.Point(151, 54)
        Me.lblAcquiredPagesN.Name = "lblAcquiredPagesN"
        Me.lblAcquiredPagesN.Size = New System.Drawing.Size(39, 13)
        Me.lblAcquiredPagesN.TabIndex = 4
        Me.lblAcquiredPagesN.Text = "Label2"
        '
        'dlgScanMorePages
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(296, 232)
        Me.Controls.Add(Me.lblAcquiredPagesN)
        Me.Controls.Add(Me.lblAcquiredPagesNText)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.btnAddPage)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgScanMorePages"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Scan More Pages"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnAddPage As System.Windows.Forms.Button
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents lblAcquiredPagesNText As System.Windows.Forms.Label
    Friend WithEvents lblAcquiredPagesN As System.Windows.Forms.Label

End Class
