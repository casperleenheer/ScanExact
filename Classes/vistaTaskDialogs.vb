'ScanExact - Simple Photocopier
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

Imports Microsoft.WindowsAPICodePack.Dialogs

Class WIARegisterDialog
    Implements IDisposable

    Dim td As TaskDialog
    Dim OKClicked As Boolean

    Public Function Show(ByVal text As String, ByVal instructions As String, ByVal title As String, ByVal cancel As String) As TaskDialogResult

        td = New TaskDialog()
        td.Caption = title
        td.InstructionText = instructions
        td.Icon = TaskDialogStandardIcon.Shield
        td.Cancelable = True
        td.Text = text

        Dim CancelButton As TaskDialogButton = New TaskDialogButton("Cancel", cancel)
        Dim OKButton As TaskDialogButton = New TaskDialogButton("OK", "OK")
        OKButton.ShowElevationIcon = True
        OKButton.Default = True

        td.Controls.Add(OKButton)
        td.Controls.Add(CancelButton)

        AddHandler CancelButton.Click, AddressOf Cancel_Click
        AddHandler OKButton.Click, AddressOf OK_Click
        Try
            td.Show()
        Catch ex As NotSupportedException
            Dim msg As MsgBoxResult = MsgBoxWrap(text, MsgBoxStyle.Exclamation + MsgBoxStyle.OkCancel, title)
            If msg = MsgBoxResult.Ok Then Return TaskDialogResult.Ok Else Return TaskDialogResult.Cancel
        End Try

        If OKClicked Then Return TaskDialogResult.Ok Else Return TaskDialogResult.Cancel
    End Function

    Sub OK_Click()
        td.Close(Microsoft.WindowsAPICodePack.Dialogs.TaskDialogResult.Ok)
        OKClicked = True
    End Sub

    Sub Cancel_Click(ByVal sender As Object, ByVal e As EventArgs)
        td.Close(Microsoft.WindowsAPICodePack.Dialogs.TaskDialogResult.Cancel)
        OKClicked = False
    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                td.Close()
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class

