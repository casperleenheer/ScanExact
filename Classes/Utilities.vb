Imports System.IO
Module Utilities
    Function MsgBoxWrap(Message As String, Optional Style As MsgBoxStyle = MsgBoxStyle.DefaultButton1, Optional Title As String = "ScanExact") As MsgBoxResult
        If My.Settings.Silent = "False" Then
            Return MsgBox(Message, Style, Title)
        Else
            Console.WriteLine(Message)
            Return MsgBoxResult.Cancel
        End If
    End Function

    Function GetWritablePath() As String
        Try
            Dim fi As New System.IO.FileInfo(Application.ExecutablePath)
            Dim path As String = fi.DirectoryName

            Dim fs As FileStream = File.Create(path + "\writable")
            fs.Close()
            File.Delete(path + "\writable")
            Return path
        Catch ex As Exception
            Dim path As String = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
            path = IO.Path.Combine(path, Application.ProductName)
            If Not Directory.Exists(path) Then
                Try
                    Directory.CreateDirectory(path)
                    Directory.Delete(path)
                Catch e As Exception
                    Throw
                End Try
            End If
            Return path

        End Try

        Return Application.LocalUserAppDataPath
    End Function
End Module
