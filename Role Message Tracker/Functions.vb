Imports Nini.Config

Module Functions
    Dim dataPath As String = AppDomain.CurrentDomain.BaseDirectory
    Dim settingsFile As New IniConfigSource(dataPath & "settings.ini")
    Public Function GetBotToken() As String
        Dim token As String = settingsFile.Configs("SETTINGS").Get("Token")
        Return token
    End Function

    Public Function GetBotPrefix() As String
        Dim prefix As String = settingsFile.Configs("SETTINGS").Get("Prefix")
        Return prefix
    End Function

    Public Function GetBotName() As String
        Dim name As String = settingsFile.Configs("SETTINGS").Get("Name")
        Return name
    End Function

    Public Function GetBotIcon() As String
        Dim icon As String = settingsFile.Configs("SETTINGS").Get("IconURL")
        Return icon
    End Function

    Public Function GetRolesList() As String
        Dim roles As String = settingsFile.Configs("SETTINGS").Get("Roles")
        Return roles
    End Function

    Public Function GetRolesChannelList() As String
        Dim channels As String = settingsFile.Configs("SETTINGS").Get("Channels")
        Return channels
    End Function
End Module
