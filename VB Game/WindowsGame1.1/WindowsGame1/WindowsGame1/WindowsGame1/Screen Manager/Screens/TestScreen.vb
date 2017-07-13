Public Class TestScreen
    Inherits BaseScreen
    Private TestText As String = "NIPPLE SALADS"
    Private TextPos As New Vector2(20, 295)
    Private IsAlive As Boolean = True

    Private LifeSpan As Double = 0

    Public Sub New()
        Name = "TestScreen"
    End Sub

    Public Overrides Sub Update()
        If LifeSpan < 5000 Then
            LifeSpan += Globals.GameTime.ElapsedGameTime.TotalMilliseconds
        Else
            IsAlive = False
        End If

        If IsAlive = False Then
            Me.State = ScreenState.Shutdown
        End If
    End Sub

    Public Overrides Sub Draw()
        Globals.SpriteBatch.Begin()
        Globals.SpriteBatch.Draw(Textures.Sanic, New Rectangle(0, 0, Globals.GameSize.X, Globals.GameSize.Y), New Rectangle(206, 206, 1, 1), Color.White)
        Globals.SpriteBatch.DrawString(Fonts.Arial_20, TestText, TextPos, Color.Red)
        Globals.SpriteBatch.End()

    End Sub
End Class
