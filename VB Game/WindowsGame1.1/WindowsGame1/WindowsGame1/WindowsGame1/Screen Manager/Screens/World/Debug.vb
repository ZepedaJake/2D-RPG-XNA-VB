Public Class Debug
    Inherits BaseScreen

    Public Screens As String = ""
    Public FocusScreen As String = ""

    Private FPS As Integer
    Private FPSCounter As Integer
    Private FPSTimer As Double
    Private FPSText As String = ""

    Private BGRect As Rectangle

    Private KeyDown As Boolean = False

    Public Shared DebugMode As Boolean = False
    Public Shared DeathCount = 0
    Public Sub New()
        Name = "Debug"
        State = ScreenState.Hidden
        GrabFocus = False
    End Sub

    Public Overrides Sub HandleInput()
        'Temporary until we create an Input Class
        If Input.KeyPressed(Keys.F1) Then
            If State = ScreenState.Active Then
                State = ScreenState.Hidden
                DebugMode = False
            ElseIf State = ScreenState.Hidden Then
                State = ScreenState.Active
                DebugMode = True
            End If
        End If
    End Sub

    Public Overrides Sub Update()
        MyBase.Update()

        If Screens.Length > 0 Then
            Screens = Screens.Substring(0, Screens.Length - 2)
        End If

        Dim txtWidth As Integer = 0
        Dim txtHeight As Integer = 0

        If Fonts.Arial_8.MeasureString(Screens).X > txtWidth Then
            txtWidth = Fonts.Arial_8.MeasureString(Screens).X
        End If
        If Fonts.Arial_8.MeasureString(FocusScreen).X > txtWidth Then
            txtWidth = Fonts.Arial_8.MeasureString(FocusScreen).X
        End If
        txtHeight = Fonts.Arial_8.MeasureString(FPSText).Y * 3

        BGRect = New Rectangle(0, 0, txtWidth + 20, txtHeight + 20)
    End Sub

    Public Overrides Sub Draw()
        MyBase.Draw()

        If Globals.GameTime.TotalGameTime.TotalMilliseconds > FPSTimer Then
            FPS = FPSCounter
            FPSTimer = Globals.GameTime.TotalGameTime.TotalMilliseconds + 1000
            FPSCounter = 1
            FPSText = "FPS: " & FPS
        Else
            FPSCounter += 1
        End If

        Globals.SpriteBatch.Begin()
        Globals.SpriteBatch.Draw(Textures.Drop, BGRect, Color.Black * 0.4F)
        Globals.SpriteBatch.DrawString(Fonts.Arial_8, FPSText, New Vector2(10, 10), Color.White)
        Globals.SpriteBatch.DrawString(Fonts.Arial_8, Screens, New Vector2(10, 22), Color.White)
        Globals.SpriteBatch.DrawString(Fonts.Arial_8, FocusScreen, New Vector2(10, 34), Color.White)
        Globals.SpriteBatch.DrawString(Fonts.Arial_8, Game1.MessageCounter, New Vector2(10, 46), Color.White)



        Globals.SpriteBatch.End()
    End Sub
End Class
