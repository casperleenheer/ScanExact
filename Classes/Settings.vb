Imports System.Configuration

Namespace My
    
    ' This class allows you to handle specific events on the settings class:
    ' The SettingChanging event is raised before a setting's value is changed.
    ' The PropertyChanged event is raised after a setting's value is changed.
    ' The SettingsLoaded event is raised after the setting values are loaded.
    ' The SettingsSaving event is raised before the setting values are saved.

    Partial Friend NotInheritable Class MySettings

        Private Sub MySettings_SettingChanging(ByVal sender As Object, ByVal e As System.Configuration.SettingChangingEventArgs) Handles Me.SettingChanging
            If e.SettingName = "DefaultIntent" Then
                If Not (e.NewValue = 1 Or e.NewValue = 2 Or e.NewValue = 4) Then
                    e.Cancel = True
                End If
            End If

        End Sub

    End Class
End Namespace
