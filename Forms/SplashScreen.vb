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
Public NotInheritable Class SplashScreen
    Dim WithEvents timer As New System.Timers.Timer

    Public Sub KillMe(ByVal sender As Object, ByVal e As EventArgs)
        Me.Close()
    End Sub

    Private Sub SplashScreen_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Set up the dialog text at runtime according to the application's assembly information.  
        timer.Interval = 5000
        timer.Enabled = True
        If My.Settings.CustomCulture Then Threading.Thread.CurrentThread.CurrentUICulture = My.Settings.Culture
        'Application title
        ApplicationTitle.Text = String.Format(appControl.GetLocalizedString("splashScreen_Loading"), My.Application.Info.Title)

        Version.Text = My.Application.Info.Version.ToString()

        'Copyright info
        Copyright.Text = My.Application.Info.Copyright
    End Sub

End Class
