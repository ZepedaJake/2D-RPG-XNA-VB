Public Class DeathMenu
    Inherits BaseScreen

    Public Entries As New List(Of MenuEntry)
    Public MenuSelect As Integer = 0

    Private MenuSize As New Vector2(Globals.GameSize.X, Globals.GameSize.Y)
    Private MenuPos As New Vector2(0, 0)
    Public World As WorldScreen

    Public Shared ResetStats As Boolean = False
    Private DeathSong As SoundEffectInstance

    Dim MH As New MapHandler
    Public Sub New()
        Name = "DeathMenu"
        State = ScreenState.Active

        Try : Game1.SongPlay.Dispose() : Catch : End Try
        Game1.SongPlaying = False
        WorldScreen.MapSong = 0
        'Add entries
        AddEntry("Retry", False)
        AddEntry("Title", False)

        AddEntry("Reset Points", False)
        MenuSelect = 5

        DeathSong = Music.Death.CreateInstance()
        DeathSong.Play()
    End Sub

    Private AniTime As Single
    Public Overrides Sub Update()


        If AniTime >= 5 Then
            Entries.Item(0).Enabled = True
            Entries.Item(1).Enabled = True


            If Debug.DeathCount >= 3 Then
                Entries.Item(2).Enabled = True
            End If
        Else

            AniTime += Globals.GameTime.ElapsedGameTime.TotalSeconds

        End If


    End Sub
    Public Sub AddEntry(text As String, enabled As Boolean)
        Dim Entry As MenuEntry
        Entry = New MenuEntry
        With Entry
            .Text = text
            .Enabled = enabled
        End With
        Entries.Add(Entry)
    End Sub

    Public Overrides Sub HandleInput()
        'Menu up
        If AniTime > 5 Then
            If Input.KeyPressed(Keys.Up) Or Input.KeyPressed(Keys.W) Then
                MenuSelect -= 1
                If MenuSelect < 0 Then MenuSelect = Entries.Count - 1
                'Skip Disabled Entries
                Do Until Entries(MenuSelect).Enabled = True
                    MenuSelect -= 1
                    If MenuSelect < 0 Then MenuSelect = Entries.Count - 1
                Loop
            End If

            If Input.KeyPressed(Keys.Down) Or Input.KeyPressed(Keys.S) Then
                MenuSelect += 1
                If MenuSelect > (Entries.Count - 1) Then MenuSelect = 0
                'Skip Disabled Entries
                Do Until Entries(MenuSelect).Enabled = True
                    MenuSelect += 1
                    If MenuSelect > (Entries.Count - 1) Then MenuSelect = 0
                Loop
            End If

            'Invoke Menu Item
            If Input.KeyPressed(Keys.Enter) Then
                DeathSong.Dispose()

                Select Case MenuSelect
                    Case 0
                        ScreenManager.UnloadScreen("DeathMenu")
                        ScreenManager.AddScreen(New WorldScreen(MH.ReloadSave))
                    Case 1
                        ScreenManager.UnloadScreen("DeathMenu")
                        Try : Game1.SongPlay.Dispose() : Catch : End Try
                        WorldScreen.MapSong = 0
                        Game1.SongPlaying = False
                        ScreenManager.AddScreen(New TitleScreen())
                        ScreenManager.AddScreen(New MainMenu())
                    Case 2
                        ResetStats = True

                        ScreenManager.AddScreen(New WorldScreen(MH.ReloadSave))
                        ScreenManager.UnloadScreen("DeathMenu")

                End Select
            End If
        End If
    End Sub

    Public Overrides Sub Draw()
        Globals.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone)
        'Draw Menu Graphics
        Globals.SpriteBatch.Draw(Textures.SpriteSheet1, New Rectangle(MenuPos.X, MenuPos.Y, MenuSize.X, MenuSize.Y), New Rectangle(0, 0, 1, 1), Color.White)

        Globals.SpriteBatch.DrawString(Fonts.Arial_8, "You Are Dead", New Vector2(200, MenuPos.Y + 50), Color.White * (AniTime / 5), 0, New Vector2(0, 0), 5, SpriteEffects.None, 0)

        'Draw Menu Options
        Dim MenuY As Integer = MenuPos.Y + 200
        For x = 0 To Entries.Count - 1
            If x = MenuSelect Then
                Globals.SpriteBatch.DrawString(Fonts.Arial_8, Entries.Item(x).Text, New Vector2(MenuPos.X + (MenuSize.X / 2) - Fonts.Arial_8.MeasureString(Entries.Item(x).Text).X / 3, MenuY), Color.White, 0, New Vector2(0, 0), 3, SpriteEffects.None, 0)
            ElseIf Entries.Item(x).Enabled = True Then
                Globals.SpriteBatch.DrawString(Fonts.Arial_8, Entries.Item(x).Text, New Vector2(MenuPos.X + (MenuSize.X / 2) - Fonts.Arial_8.MeasureString(Entries.Item(x).Text).X / 3, MenuY), Color.DarkRed, 0, New Vector2(0, 0), 3, SpriteEffects.None, 0)
            Else
                Globals.SpriteBatch.DrawString(Fonts.Arial_8, Entries.Item(x).Text, New Vector2(MenuPos.X + (MenuSize.X / 2) - Fonts.Arial_8.MeasureString(Entries.Item(x).Text).X / 3, MenuY), Color.Black * 0.0F, 0, New Vector2(0, 0), 3, SpriteEffects.None, 0)
            End If

            MenuY += 75
        Next
        Dim MenuX As Integer = MenuPos.X + 20
        Globals.SpriteBatch.End()
    End Sub

End Class
