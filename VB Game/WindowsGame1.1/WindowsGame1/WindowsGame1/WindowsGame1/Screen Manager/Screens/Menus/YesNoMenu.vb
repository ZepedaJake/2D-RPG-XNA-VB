Public Class YesNoMenu

    Inherits BaseScreen

    Private Entries As New List(Of MenuEntry)
        Private MenuSelect As Integer = 0

    Private MenuSize As New Vector2(550, 175)
    Private MenuPos As New Vector2((Globals.GameSize.X / 2) - MenuSize.X / 2, 300)

    Private BoxText As String
    Private TileSize As Integer = 64

    Public Sub New(t As String)
        Name = "YesNo"
        State = ScreenState.Active

        'Add entries
        AddEntry("Yes", True)
        AddEntry("No", True)
        BoxText = t
    End Sub

    Private AniTime As Double = 0

    Public Overrides Sub Update()



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
            Select Case MenuSelect
                Case 0
                    'ScreenManager.AddScreen(New Message("Starting Game", New Vector2(0, 0), New Vector2(300, 300), 255, 255, 255))
                    For Each s As String In System.IO.Directory.GetFiles("saves")
                        System.IO.File.Delete(s)
                    Next s
                    ScreenManager.UnloadScreen("TitleScreen")
                    ScreenManager.UnloadScreen("MainMenu")
                    ScreenManager.UnloadScreen("YesNo")
                    TitleScreen.Song.Dispose()
                    ScreenManager.AddScreen(New IntroScreen())
                    ScreenManager.AddScreen(New Message("newGameText", 255, 255, 255))
                    TitleScreen.Startup = True
                    TitleScreen.TitleSongPlaying = False
                    WorldScreen.MapSong = 1

                Case 1

                    ScreenManager.UnloadScreen("YesNo")
                    MainMenu.InDialouge = False


            End Select

        End If
        End Sub

    Public Overrides Sub Draw()
        Globals.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone)
        'Draw Menu Graphics
        'Globals.SpriteBatch.Draw(Textures.SpriteSheet1, New Rectangle(MenuPos.X, MenuPos.Y, MenuSize.X, MenuSize.Y), New Rectangle(45, 54, 1, 1), Color.Red * 0.7F)
        Globals.SpriteBatch.Draw(Textures.Menu, New Rectangle(MenuPos.X, MenuPos.Y, TileSize, TileSize), New Rectangle(0, 0, 16, 16), Color.Red)
        Globals.SpriteBatch.Draw(Textures.Menu, New Rectangle(MenuPos.X + TileSize, MenuPos.Y, MenuSize.X - (TileSize * 2), TileSize), New Rectangle(16, 0, 1, 16), Color.Red)
        Globals.SpriteBatch.Draw(Textures.Menu, New Rectangle(MenuPos.X + MenuSize.X - TileSize, MenuPos.Y, TileSize, TileSize), New Rectangle(16, 0, 16, 16), Color.Red)

        'mid layer
        Globals.SpriteBatch.Draw(Textures.Menu, New Rectangle(MenuPos.X, MenuPos.Y + TileSize, TileSize, MenuSize.Y - (TileSize * 2)), New Rectangle(0, 16, 16, 1), Color.Red)
        Globals.SpriteBatch.Draw(Textures.Menu, New Rectangle(MenuPos.X + TileSize, MenuPos.Y + TileSize, MenuSize.X - (TileSize * 2), MenuSize.Y - (TileSize * 2)), New Rectangle(16, 16, 1, 1), Color.Red)
        Globals.SpriteBatch.Draw(Textures.Menu, New Rectangle(MenuSize.X + MenuPos.X - TileSize, MenuPos.Y + TileSize, TileSize, MenuSize.Y - (TileSize * 2)), New Rectangle(16, 16, 16, 1), Color.Red)

        'Bottom Layer
        Globals.SpriteBatch.Draw(Textures.Menu, New Rectangle(MenuPos.X, MenuSize.Y + MenuPos.Y - TileSize, TileSize, TileSize), New Rectangle(0, 16, 16, 16), Color.Red)
        Globals.SpriteBatch.Draw(Textures.Menu, New Rectangle(MenuPos.X + TileSize, MenuSize.Y + MenuPos.Y - TileSize, MenuSize.X - (TileSize * 2), TileSize), New Rectangle(16, 16, 1, 16), Color.Red)
        Globals.SpriteBatch.Draw(Textures.Menu, New Rectangle(MenuPos.X + MenuSize.X - TileSize, MenuSize.Y + MenuPos.Y - TileSize, TileSize, TileSize), New Rectangle(16, 16, 16, 16), Color.Red)




        Globals.SpriteBatch.DrawString(Fonts.Arial_8, BoxText, New Vector2(MenuPos.X + 20, MenuPos.Y + 20), Color.White, 0, New Vector2(0, 0), 2, SpriteEffects.None, 0)
        Globals.SpriteBatch.DrawString(Fonts.Arial_8, BoxText, New Vector2(MenuPos.X + 21, MenuPos.Y + 21), Color.Black, 0, New Vector2(0, 0), 2, SpriteEffects.None, 0)


        'Draw Menu Options
        Dim MenuY As Integer = MenuPos.Y + 80
        For x = 0 To Entries.Count - 1

            If x = MenuSelect Then

                Globals.SpriteBatch.DrawString(Fonts.Arial_8, Entries.Item(x).Text, New Vector2(MenuPos.X + (MenuSize.X / 2), MenuY), Color.White, 0, New Vector2(0, 0), 3, SpriteEffects.None, 0)
                Globals.SpriteBatch.DrawString(Fonts.Arial_8, Entries.Item(x).Text, New Vector2(MenuPos.X + (MenuSize.X / 2), MenuY + 2), Color.Black, 0, New Vector2(0, 0), 3, SpriteEffects.None, 0)

            ElseIf Entries.Item(x).Enabled = True Then
                Globals.SpriteBatch.DrawString(Fonts.Arial_8, Entries.Item(x).Text, New Vector2(MenuPos.X + (MenuSize.X / 2), MenuY), Color.White, 0, New Vector2(0, 0), 2, SpriteEffects.None, 0)
                Globals.SpriteBatch.DrawString(Fonts.Arial_8, Entries.Item(x).Text, New Vector2(MenuPos.X + (MenuSize.X / 2), MenuY + 2), Color.Black, 0, New Vector2(0, 0), 2, SpriteEffects.None, 0)

            End If

            MenuY += 40
        Next
        Dim MenuX As Integer = MenuPos.X + 20
        Globals.SpriteBatch.End()
    End Sub



End Class


