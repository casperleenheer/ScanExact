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

Public NotInheritable Class AboutBox

    Private Sub AboutBox_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Disables main form
        appControl.MainForm.Enabled = False
        ' Set the title of the form.
        Dim ApplicationTitle As String
        If My.Application.Info.Title <> "" Then
            ApplicationTitle = My.Application.Info.Title
        Else
            ApplicationTitle = System.IO.Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName)
        End If
        Me.Text = String.Format("About {0}", ApplicationTitle)
        ' Initialize all of the text displayed on the About Box.

        '    properties dialog (under the "Project" menu).
        Me.LabelProductName.Text = My.Application.Info.ProductName
        Me.LabelVersion.Text = String.Format("Version {0}", My.Application.Info.Version.ToString)
        Me.LabelCopyright.Text = My.Application.Info.Copyright
        Me.LabelCompanyName.Text = My.Application.Info.CompanyName
        lblDescription.Text = appControl.GetLocalizedString("AboutBox_lblDescription")
        Label1.Text = appControl.GetLocalizedString("AboutBox_Label1")
        LinkLabel3.Text = appControl.GetLocalizedString("AboutBox_LinkLabel3")
    End Sub

    Private Sub OKButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OKButton.Click
        Me.Hide()
    End Sub

    Private Sub LinkLabel3_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel3.LinkClicked
        Dim licenseFilePath As String = Application.StartupPath + "\License.txt"
        If IO.File.Exists(licenseFilePath) Then
            Process.Start("Notepad.exe", licenseFilePath)
            Me.Hide()
        Else
            MsgBoxWrap(appControl.GetLocalizedString("Msg_LicenseFileNotFound"), MsgBoxStyle.Information, "ScanExact")
        End If
    End Sub

    Private Sub LinkLabel1_LinkClicked_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        System.Diagnostics.Process.Start("http://www.dutchborn.nl")
        Me.Hide()
    End Sub

    Private Sub Donate_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
        System.Diagnostics.Process.Start("http://www.dutchborn.nl")
        Me.Hide()
    End Sub

    Private Sub LinkLabel4_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        System.Diagnostics.Process.Start("http://www.dutchborn.nl")
        Me.Hide()
    End Sub

    Private Sub AboutBox_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.VisibleChanged
        If Not Me.Visible Then
            appControl.MainForm.Enabled = True
            appControl.MainForm.Show()
        End If
    End Sub

End Class
