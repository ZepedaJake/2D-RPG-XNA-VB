Public Enum ScreenState
    Active
    Shutdown
    Hidden
End Enum

Public Class ScreenManager
    Private Shared Screens As New List(Of BaseScreen)
    Private Shared NewScreens As New List(Of BaseScreen)
    Private DebugScreen As New Debug()

    Public Sub New()
        AddScreen(DebugScreen)
    End Sub

    Public Sub Update()
        DebugScreen.Screens = "Screens: "


        'generate List of dead screeens for removal
        Dim RemoveScreens As New List(Of BaseScreen)

        For Each FoundScreen As BaseScreen In Screens
            If FoundScreen.State = ScreenState.Shutdown Then
                RemoveScreens.Add(FoundScreen)
            Else
                DebugScreen.Screens += FoundScreen.Name + ", "
                FoundScreen.Focused = False
            End If
        Next

        'Remove Dead Screens
        For Each FoundScreen As BaseScreen In RemoveScreens
            Screens.Remove(FoundScreen)
        Next

        'Add new Screens to maanger list
        For Each FoundScreen As BaseScreen In NewScreens
            Screens.Add(FoundScreen)
        Next
        NewScreens.Clear()

        'Reset Debug Screen to top of the list
        Screens.Remove(DebugScreen)
        Screens.Add(DebugScreen)

        'check Screen Focus
        If Screens.Count > 0 Then
            For i = Screens.Count - 1 To 0 Step -1
                If Screens(i).GrabFocus Then
                    Screens(i).Focused = True
                    DebugScreen.FocusScreen = "Focused Screen: " + Screens(i).Name
                    Exit For
                End If
            Next
        End If

        'Handle Input for focused screen
        For Each FoundScreen As BaseScreen In Screens
            If Globals.WindowFocused Then
                FoundScreen.HandleInput()
            End If
            FoundScreen.Update()
        Next
    End Sub

    Public Sub Draw()
        For Each FoundScreen As BaseScreen In Screens
            If FoundScreen.State = ScreenState.Active Then
                FoundScreen.Draw()
            End If
        Next
    End Sub

    Public Shared Sub AddScreen(screen As BaseScreen)
        NewScreens.Add(screen)
    End Sub

    Public Shared Sub UnloadScreen(screen As String)
        For Each FoundScreen As BaseScreen In Screens
            If FoundScreen.Name = screen Then
                FoundScreen.Unload()
                Exit For
            End If
        Next
    End Sub
End Class
