Public Class TitleScreen
    Inherits BaseScreen

    Private AniTime As Double = 0
    Private r As Integer = 0
    Private g As Integer = 0
    Private b As Integer = 192

    Private rnd As Random = New Random

    Private SongTimer As Double = 0
    Public Shared Song As SoundEffectInstance
    Public Shared TitleSongPlaying = False

    Public Shared Startup As Boolean = True
    Public Sub New()
        Name = "TitleScreen"
        State = ScreenState.Active

        If TitleSongPlaying = False Then
            Song = Music.Title.CreateInstance()
            Song.Play()
            TitleSongPlaying = True
        End If


    End Sub

    Public Overrides Sub HandleInput()
        'MyBase.HandleInput()
        If Input.KeyPressed(Keys.Enter) And Startup Then
            ScreenManager.AddScreen(New MainMenu())
            Startup = False
        End If

        'If Input.KeyPressed(Keys.O) Then
        '    ScreenManager.AddScreen(New WorldScreen(12))
        '    ScreenManager.UnloadScreen("TitleScreen")
        'End If
    End Sub

    Public Overrides Sub Update()
        'flashing
        AniTime += Globals.GameTime.ElapsedGameTime.TotalMilliseconds

        If AniTime > 100 Then
            r = rnd.Next(40, 200)
            g = rnd.Next(40, 200)
            b = rnd.Next(40, 200)

            AniTime = 0
        End If
        If SongTimer <= 3.1 Then
            SongTimer += Globals.GameTime.ElapsedGameTime.TotalMinutes
        Else
            Song.Play()
            SongTimer = 0
        End If
    End Sub

    Public Overrides Sub Draw()
        MyBase.Draw()

        Globals.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone)
        Globals.SpriteBatch.Draw(Textures.TitleArt, New Rectangle(0, 0, Globals.GameSize.X, Globals.GameSize.Y), New Rectangle(0, 0, 1024, 1024), Color.White)
        'Globals.SpriteBatch.DrawString(Fonts.Arial_8, SongTimer.ToString(), New Vector2(10, 10), New Color(230, 215, 184), 0, New Vector2(0, 0), 2, SpriteEffects.None, 0)
        Globals.SpriteBatch.DrawString(Fonts.Arial_8, "Terry's Tower", New Vector2(290, 30), Color.White, 0, New Vector2(0, 0), 6, SpriteEffects.None, 0)
        'Globals.SpriteBatch.DrawString(Fonts.Arial_8, "Terry's Tower", New Vector2(102, 102), New Color(r, g, b), 0, New Vector2(0, 0), 8, SpriteEffects.None, 0)
        Globals.SpriteBatch.DrawString(Fonts.Arial_8, "Terry's Tower", New Vector2(286, 26), New Color(1, 1, 1), 0, New Vector2(0, 0), 6, SpriteEffects.None, 0)

        If Startup Then
            Globals.SpriteBatch.DrawString(Fonts.Arial_8, "Press Enter", New Vector2(15, Globals.GameSize.Y - 100), New Color(r, g, b), 0, New Vector2(0, 0), 6, SpriteEffects.None, 0)

        End If

        Globals.SpriteBatch.End()
    End Sub

    Public Overrides Sub Unload()
        MyBase.Unload()
    End Sub
End Class
