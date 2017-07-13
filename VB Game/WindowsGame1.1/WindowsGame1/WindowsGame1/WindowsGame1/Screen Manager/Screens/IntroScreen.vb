Public Class IntroScreen
    Inherits BaseScreen

    Private y As Integer = 0
    Private DrawPhase As Integer = 0
    Public Sub New()
        Name = "IntroScreen"
        y = 0
    End Sub
    Public Overrides Sub Update()
        If Game1.MessageCounter = 3 Then
            DrawPhase = 1
        End If

        If DrawPhase > 0 And y < (Globals.GameSize.Y) Then
            y += 3
        End If

    End Sub

    Public Overrides Sub Draw()
        Globals.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone)

        Globals.SpriteBatch.Draw(Textures.Terry, New Rectangle(0, 0, Globals.GameSize.X, Globals.GameSize.Y), New Rectangle(16, 16, 1, 1), Color.Black)

        Globals.SpriteBatch.Draw(Textures.Terry, New Rectangle(Globals.GameSize.X / 2 - 64, 400, 32 * 4, 32 * 4), New Rectangle(0, 64, 32, 32), Color.White)

        If DrawPhase > 0 Then
            Globals.SpriteBatch.Draw(Textures.Terry, New Rectangle(0, (y - Globals.GameSize.Y), Globals.GameSize.X, Globals.GameSize.Y), New Rectangle(128, 128, 32 * 5, 32 * 4), Color.White)
            If y >= Globals.GameSize.Y Then
                'Globals.SpriteBatch.Draw(Textures.SpriteSheet1, New Rectangle(85, 375, 250, 50), New Rectangle(0, 0, 1, 1), Color.White)

                'Globals.SpriteBatch.DrawString(Fonts.Arial_8, "F!@# you Terry", New Vector2(80, 380), Color.White, 0, New Vector2(0, 0), 3, SpriteEffects.None, 0)

            End If
        End If


        Globals.SpriteBatch.End()

    End Sub
End Class
