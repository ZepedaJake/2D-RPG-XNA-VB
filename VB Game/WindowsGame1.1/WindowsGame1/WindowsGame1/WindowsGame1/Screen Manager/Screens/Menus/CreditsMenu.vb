Public Class CreditsMenu

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
        Name = "CreditsMenu"
        State = ScreenState.Active

        WorldScreen.MapSong = 0
        'Add entries
        AddEntry("Enter: Return to Title", True)

    End Sub

        Private AniTime As Single
    Public Overrides Sub Update()
        AniTime += Globals.GameTime.ElapsedGameTime.TotalSeconds

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
                        ScreenManager.UnloadScreen("CreditsMenu")
                        ScreenManager.AddScreen(New TitleScreen())

                End Select
                End If

        End Sub

        Public Overrides Sub Draw()
            Globals.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone)
            'Draw Menu Graphics
            Globals.SpriteBatch.Draw(Textures.SpriteSheet1, New Rectangle(MenuPos.X, MenuPos.Y, MenuSize.X, MenuSize.Y), New Rectangle(0, 0, 1, 1), Color.White)

        Globals.SpriteBatch.DrawString(Fonts.Arial_8, "Credits", New Vector2(10, 10), Color.White, 0, New Vector2(0, 0), 4, SpriteEffects.None, 0)
        Globals.SpriteBatch.DrawString(Fonts.Arial_8, "Sounds and Music created by myself" + vbNewLine + " in FL Studio 10", New Vector2(10, 60), Color.White, 0, New Vector2(0, 0), 2, SpriteEffects.None, 0)
        Globals.SpriteBatch.DrawString(Fonts.Arial_8, "All Artwork created by me in Photoshop 2015", New Vector2(10, 140), Color.White, 0, New Vector2(0, 0), 2, SpriteEffects.None, 0)
        Globals.SpriteBatch.DrawString(Fonts.Arial_8, "Inspired by the game: Magic Tower", New Vector2(10, 190), Color.White, 0, New Vector2(0, 0), 2, SpriteEffects.None, 0)
        Globals.SpriteBatch.DrawString(Fonts.Arial_8, "Thanks to Aardaerimus D'Aritonyss" + vbNewLine + " for his tutorials on YouTube", New Vector2(10, 240), Color.White, 0, New Vector2(0, 0), 2, SpriteEffects.None, 0)
        'Globals.SpriteBatch.DrawString(Fonts.Arial_8, "I dont know what else to put on here, so here", New Vector2(100, 350), Color.White, 0, New Vector2(0, 0), 2, SpriteEffects.None, 0)

        'Globals.SpriteBatch.Draw(Textures.Terry, New Rectangle(100, 400, 64, 64), New Rectangle(0, 256, 64, 64), Color.White, 0, New Vector2(0, 0), SpriteEffects.FlipHorizontally, 0)


        'Draw Menu Options
        Dim MenuY As Integer = MenuSize.Y - 50
        For x = 0 To Entries.Count - 1
                If x = MenuSelect Then
                Globals.SpriteBatch.DrawString(Fonts.Arial_8, Entries.Item(x).Text, New Vector2(MenuPos.X + (MenuSize.X / 2) - Fonts.Arial_8.MeasureString(Entries.Item(x).Text).X * 1.5, MenuY), Color.White, 0, New Vector2(0, 0), 3, SpriteEffects.None, 0)
            ElseIf Entries.Item(x).Enabled = True Then
                Globals.SpriteBatch.DrawString(Fonts.Arial_8, Entries.Item(x).Text, New Vector2(MenuPos.X + (MenuSize.X / 2) - Fonts.Arial_8.MeasureString(Entries.Item(x).Text).X * 1.5, MenuY), Color.DarkRed, 0, New Vector2(0, 0), 3, SpriteEffects.None, 0)
            Else
                Globals.SpriteBatch.DrawString(Fonts.Arial_8, Entries.Item(x).Text, New Vector2(MenuPos.X + (MenuSize.X / 2) - Fonts.Arial_8.MeasureString(Entries.Item(x).Text).X * 1.5, MenuY), Color.Black * 0.0F, 0, New Vector2(0, 0), 3, SpriteEffects.None, 0)
            End If

                MenuY += 75
            Next
            Dim MenuX As Integer = MenuPos.X + 20
            Globals.SpriteBatch.End()
        End Sub

    End Class

