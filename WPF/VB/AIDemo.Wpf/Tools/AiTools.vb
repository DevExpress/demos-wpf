Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Linq
Imports System.Windows
Imports DevExpress.AIIntegration
Imports DevExpress.Xpf.Core
Imports Application = System.Windows.Application

Namespace AIDemo.Tools

    Public Module AiTools

        Private ReadOnly availableThemes As HashSet(Of String) = Theme.Themes.[Select](Function(x) x.Name).ToHashSet()

        Private Function GetMainWindow() As ThemedWindow
            Return Application.Current.Windows.OfType(Of ThemedWindow)().FirstOrDefault(Function(w) w.IsActive)
        End Function

        <AIIntegrationTool>
        <Description("Toggle the application between full-screen and windowed modes.")>
        Public Function SwitchFullScreenMode() As String
            Dim win = GetMainWindow()
            win.WindowState = If(win.WindowState = WindowState.Maximized, WindowState.Normal, WindowState.Maximized)
            Dim state = If(win.WindowState = WindowState.Maximized, "full-screen", "windowed")
            Return $"Window state: {state}"
        End Function

        <AIIntegrationTool>
        <Description("Close the application.")>
        Public Sub CloseApplication()
            Dim win = GetMainWindow()
            win.Close()
        End Sub

        <AIIntegrationTool>
        <Description("Gets information about available application themes. Always use this before changing the theme.")>
        Public Function GetThemesInfo() As Object
            Return New With {.CurrentTheme = ApplicationThemeHelper.ApplicationThemeName, availableThemes}
        End Function

        <AIIntegrationTool>
        <Description("Changes the application theme.")>
        Public Function ChangeTheme(<Description("The name of the theme to apply.")> ByVal themeName As String) As Object
            If Not availableThemes.Contains(themeName) Then Return New With {.success = False, .[error] = $"Theme '{themeName}' is not available."}
            ApplicationThemeHelper.ApplicationThemeName = themeName
            Return New With {.success = True, .appliedTheme = themeName}
        End Function
    End Module
End Namespace
