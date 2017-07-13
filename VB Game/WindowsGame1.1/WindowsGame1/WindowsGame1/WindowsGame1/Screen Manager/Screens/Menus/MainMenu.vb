Public Class MainMenu
    Inherits BaseScreen
    Public Shared ExitGame As Boolean = False
    Private Entries As New List(Of MenuEntry)
    Private MenuSelect As Integer = 0

    Private MenuSize As New Vector2(384, 224)
    Private MenuPos As New Vector2(0, 490)
    Public Shared InDialouge = False
    Public TileSize = 64



    Dim MH As New MapHandler

    Public Sub New()
        Name = "MainMenu"
        State = ScreenState.Active

        'Add entries
        AddEntry("New Game", True)
        AddEntry("Continue Game", False)
        'AddEntry("Options", True)
        AddEntry("Credits", True)
        AddEntry("Quit Game", True)
        InDialouge = False
    End Sub

    Private AniTime As Double = 0
    Private r As Integer = 0
    Private g As Integer = 0
    Private b As Integer = 192
    Private rnd As Random = New Random
    Public Overrides Sub Update()
        'flashing
        AniTime += Globals.GameTime.ElapsedGameTime.TotalMilliseconds

        If AniTime > 100 Then
            r = Rnd.Next(40, 200)
            g = Rnd.Next(40, 200)
            b = Rnd.Next(40, 200)

            AniTime = 0
        End If

        'enable continuing
        If IO.File.Exists("saves/map.sav") Then
            Entries(1).Enabled = True
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
        If Input.KeyPressed(Keys.Up) Or Input.KeyPressed(Keys.W) And InDialouge = False Then
            MenuSelect -= 1
            If MenuSelect < 0 Then MenuSelect = Entries.Count - 1
            'Skip Disabled Entries
            Do Until Entries(MenuSelect).Enabled = True
                MenuSelect -= 1
                If MenuSelect < 0 Then MenuSelect = Entries.Count - 1
            Loop
        End If

        If Input.KeyPressed(Keys.Down) Or Input.KeyPressed(Keys.S) And InDialouge = False Then
            MenuSelect += 1
            If MenuSelect > (Entries.Count - 1) Then MenuSelect = 0
            'Skip Disabled Entries
            Do Until Entries(MenuSelect).Enabled = True
                MenuSelect += 1
                If MenuSelect > (Entries.Count - 1) Then MenuSelect = 0
            Loop
        End If

        'Invoke Menu Item
        If Input.KeyPressed(Keys.Enter) And InDialouge = False Then
            Select Case MenuSelect
                Case 0

                    If IO.File.Exists("saves/map.sav") Or IO.File.Exists("saves/terry.sav") Then
                        InDialouge = True
                        ScreenManager.AddScreen(New YesNoMenu("There is already a saved game!" & vbCrLf & "Would you like to erase data and start a new game?"))

                    Else
                        ScreenManager.UnloadScreen("TitleScreen")
                        ScreenManager.UnloadScreen("MainMenu")
                        TitleScreen.Song.Dispose()
                        'ScreenManager.AddScreen(New WorldScreen(1))
                        WorldScreen.MapSong = 1
                        ScreenManager.AddScreen(New IntroScreen())
                        ScreenManager.AddScreen(New Message("newGameText", 255, 255, 255))
                        TitleScreen.Startup = True
                        TitleScreen.TitleSongPlaying = False

                    End If
                'ScreenManager.UnloadScreen("TitleScreen")
                'ScreenManager.UnloadScreen("MainMenu")
                'TitleScreen.Song.Dispose()
                'ScreenManager.AddScreen(New WorldScreen(5))
                Case 1

                    ScreenManager.UnloadScreen("TitleScreen")
                    ScreenManager.UnloadScreen("MainMenu")
                    TitleScreen.Song.Dispose()
                    ScreenManager.AddScreen(New WorldScreen(MH.ReloadSave))
                    TitleScreen.Startup = True
                    TitleScreen.TitleSongPlaying = False
                Case 2
                    'ScreenManager.UnloadScreen("TitleScreen")
                    'ScreenManager.UnloadScreen("MainMenu")
                    'TitleScreen.Song.Dispose()
                    'ScreenManager.AddScreen(New Message("newGameText", 255, 255, 255))
                    'TitleScreen.Startup = 


                    ScreenManager.UnloadScreen("TitleScreen")
                    ScreenManager.UnloadScreen("MainMenu")
                    TitleScreen.Startup = True
                    ScreenManager.AddScreen(New CreditsMenu())
                Case 3
                    ExitGame = True
                Case 4


            End Select
        End If
    End Sub

    Public Overrides Sub Draw()
        Globals.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone)
        'Draw Menu Graphics
        'Globals.SpriteBatch.Draw(Textures.SpriteSheet1, New Rectangle(MenuPos.X, MenuPos.Y, MenuSize.X, MenuSize.Y), New Rectangle(0, 0, 1, 1), Color.White * 0.3F)
        Globals.SpriteBatch.Draw(Textures.Menu, New Rectangle(0, Globals.GameSize.Y - TileSize * 4, TileSize, TileSize), New Rectangle(0, 0, 16, 16), Color.White)
        Globals.SpriteBatch.Draw(Textures.Menu, New Rectangle(0, Globals.GameSize.Y - TileSize, TileSize, TileSize), New Rectangle(0, 16, 16, 16), Color.White)
        Globals.SpriteBatch.Draw(Textures.Menu, New Rectangle(0, Globals.GameSize.Y - TileSize * 3, TileSize, TileSize * 2), New Rectangle(0, 10, 16, 1), Color.White)
        Globals.SpriteBatch.Draw(Textures.Menu, New Rectangle(TileSize, Globals.GameSize.Y - TileSize * 4, TileSize * 4, TileSize), New Rectangle(10, 0, 1, 16), Color.White)
        Globals.SpriteBatch.Draw(Textures.Menu, New Rectangle(TileSize * 5, Globals.GameSize.Y - TileSize * 4, TileSize, TileSize), New Rectangle(16, 0, 16, 16), Color.White)
        Globals.SpriteBatch.Draw(Textures.Menu, New Rectangle(TileSize * 5, Globals.GameSize.Y - TileSize * 3, TileSize, TileSize * 2), New Rectangle(16, 16, 16, 1), Color.White)
        Globals.SpriteBatch.Draw(Textures.Menu, New Rectangle(TileSize * 5, Globals.GameSize.Y - TileSize, TileSize, TileSize), New Rectangle(16, 16, 16, 16), Color.White)
        Globals.SpriteBatch.Draw(Textures.Menu, New Rectangle(TileSize, Globals.GameSize.Y - TileSize, TileSize * 4, TileSize), New Rectangle(16, 16, 1, 16), Color.White)
        Globals.SpriteBatch.Draw(Textures.Menu, New Rectangle(TileSize, Globals.GameSize.Y - TileSize * 3, TileSize * 4, TileSize * 2), New Rectangle(16, 16, 1, 1), Color.White)



        'Draw Menu Options
        Dim MenuY As Integer = MenuPos.Y + 10
        For x = 0 To Entries.Count - 1
            'If x = MenuSelect Then

            '    Globals.SpriteBatch.DrawString(Fonts.Arial_8, Entries.Item(x).Text, New Vector2(MenuPos.X + (MenuSize.X / 2) - Fonts.Arial_8.MeasureString(Entries.Item(x).Text).X / 2, MenuY), Color.White, 0, New Vector2(0, 0), 3, SpriteEffects.None, 0)
            'ElseIf Entries.Item(x).Enabled = True Then
            '    Globals.SpriteBatch.DrawString(Fonts.Arial_8, Entries.Item(x).Text, New Vector2(MenuPos.X + (MenuSize.X / 2) - Fonts.Arial_8.MeasureString(Entries.Item(x).Text).X / 2, MenuY), Color.Black, 0, New Vector2(0, 0), 3, SpriteEffects.None, 0)
            'Else
            '    Globals.SpriteBatch.DrawString(Fonts.Arial_8, Entries.Item(x).Text, New Vector2(MenuPos.X + (MenuSize.X / 2) - Fonts.Arial_8.MeasureString(Entries.Item(x).Text).X / 2, MenuY), Color.Gray, 0, New Vector2(0, 0), 3, SpriteEffects.None, 0)
            'End If
            If x = MenuSelect Then

                Globals.SpriteBatch.DrawString(Fonts.Arial_8, Entries.Item(x).Text, New Vector2(MenuPos.X + (MenuSize.X / 2) - Fonts.Arial_8.MeasureString(Entries.Item(x).Text).X / 2 - 10, MenuY), New Color(r, g, b), 0, New Vector2(0, 0), 3, SpriteEffects.None, 0)
            ElseIf Entries.Item(x).Enabled = True Then
                Globals.SpriteBatch.DrawString(Fonts.Arial_8, Entries.Item(x).Text, New Vector2(MenuPos.X + 25, MenuY), Color.Black, 0, New Vector2(0, 0), 2, SpriteEffects.None, 0)
            Else
                Globals.SpriteBatch.DrawString(Fonts.Arial_8, Entries.Item(x).Text, New Vector2(MenuPos.X + 25, MenuY), Color.Gray, 0, New Vector2(0, 0), 2, SpriteEffects.None, 0)
            End If

            MenuY += 40
        Next
        Dim MenuX As Integer = MenuPos.X + 20
        Globals.SpriteBatch.End()
    End Sub
End Class
