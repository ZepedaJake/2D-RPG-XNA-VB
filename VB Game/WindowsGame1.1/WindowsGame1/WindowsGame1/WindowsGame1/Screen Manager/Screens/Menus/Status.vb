Public Class Status

    Inherits BaseScreen

    Public Entries As New List(Of MenuEntry)
    Public MenuSelect As Integer = 0

    Private MenuSize As New Vector2(500, 500)
    Private MenuPos As New Vector2((Globals.GameSize.X - MenuSize.X) / 2, 0)


    Public Sub New()
        Name = "Status"
        State = ScreenState.Active


        'Add entries
        AddEntry("Stats Detail", True)
        AddEntry("Skills Detail", True)
        AddEntry("Back", True)

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
            r = rnd.Next(40, 200)
            g = rnd.Next(40, 200)
            b = rnd.Next(40, 200)

            AniTime = 0


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

    Public Shared ShowingMenu As Boolean = False
    Public Overrides Sub HandleInput()
        'Menu up
        If Input.KeyPressed(Keys.Up) Or Input.KeyPressed(Keys.W) And ShowingMenu = False Then
            MenuSelect -= 1
            If MenuSelect < 0 Then MenuSelect = Entries.Count - 1
            'Skip Disabled Entries
            Do Until Entries(MenuSelect).Enabled = True
                MenuSelect -= 1
                If MenuSelect < 0 Then MenuSelect = Entries.Count - 1
            Loop
        End If

        If Input.KeyPressed(Keys.Down) Or Input.KeyPressed(Keys.S) And ShowingMenu = False Then
            MenuSelect += 1
            If MenuSelect > (Entries.Count - 1) Then MenuSelect = 0
            'Skip Disabled Entries
            Do Until Entries(MenuSelect).Enabled = True
                MenuSelect += 1
                If MenuSelect > (Entries.Count - 1) Then MenuSelect = 0
            Loop
        End If

        'Invoke Menu Item
        If Input.KeyPressed(Keys.Enter) And ShowingMenu = False Then
            Select Case MenuSelect
                Case 0 And ShowingMenu = False

                    ShowingMenu = True
                    ScreenManager.AddScreen(New StatsMenu)

                Case 1
                    ShowingMenu = True
                    ScreenManager.AddScreen(New SkillsMenu)

                Case 2

                    ScreenManager.UnloadScreen("Status")
                    WorldScreen.ShowingStats = False

            End Select
        End If

    End Sub

    Public Overrides Sub Draw()
        Globals.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone)
        'Draw Menu Graphics
        Globals.SpriteBatch.Draw(Textures.SpriteSheet1, New Rectangle(MenuPos.X, MenuPos.Y, MenuSize.X, MenuSize.Y), New Rectangle(0, 0, 1, 1), Color.White)
        Globals.SpriteBatch.DrawString(Fonts.Arial_8, "Stat Points: " + WorldScreen.StatPoints.ToString, New Vector2(MenuPos.X + 10, MenuPos.Y + 10), Color.White, 0, New Vector2(0, 0), 2, SpriteEffects.None, 0)
        Globals.SpriteBatch.DrawString(Fonts.Arial_8, "Skill Points: " + WorldScreen.SkillPoints.ToString, New Vector2(MenuPos.X + 250, MenuPos.Y + 10), Color.White, 0, New Vector2(0, 0), 2, SpriteEffects.None, 0)

        Globals.SpriteBatch.DrawString(Fonts.Arial_8, "Health: " + WorldScreen.ToonHP.ToString, New Vector2(MenuPos.X + 150, MenuPos.Y + 50), Color.White, 0, New Vector2(0, 0), 2.5, SpriteEffects.None, 0)
        Globals.SpriteBatch.DrawString(Fonts.Arial_8, "Attack: " + WorldScreen.ToonAtk.ToString, New Vector2(MenuPos.X + 150, MenuPos.Y + 100), Color.White, 0, New Vector2(0, 0), 2.5, SpriteEffects.None, 0)
        Globals.SpriteBatch.DrawString(Fonts.Arial_8, "Defense: " + WorldScreen.ToonDef.ToString, New Vector2(MenuPos.X + 150, MenuPos.Y + 150), Color.White, 0, New Vector2(0, 0), 2.5, SpriteEffects.None, 0)
        Globals.SpriteBatch.DrawString(Fonts.Arial_8, "Experience: " + WorldScreen.XpTillLvl.ToString + " / " + ((WorldScreen.ToonLvl * 2) ^ 2 + 10).ToString, New Vector2(MenuPos.X + 150, MenuPos.Y + 200), Color.White, 0, New Vector2(0, 0), 2.5, SpriteEffects.None, 0)


        'Draw Menu Options
        Dim MenuY As Integer = MenuPos.Y + 300
        For x = 0 To Entries.Count - 1
            If x = MenuSelect Then
                Globals.SpriteBatch.DrawString(Fonts.Arial_8, Entries.Item(x).Text, New Vector2(MenuPos.X + 200, MenuY), Color.White, 0, New Vector2(0, 0), PauseMenu.size, SpriteEffects.None, 0)
            ElseIf Entries.Item(x).Enabled = True Then
                Globals.SpriteBatch.DrawString(Fonts.Arial_8, Entries.Item(x).Text, New Vector2(MenuPos.X + 200, MenuY), Color.White, 0, New Vector2(0, 0), 2, SpriteEffects.None, 0)
            Else
                Globals.SpriteBatch.DrawString(Fonts.Arial_8, Entries.Item(x).Text, New Vector2(MenuPos.X + 200, MenuY), Color.Gray, 0, New Vector2(0, 0), 2, SpriteEffects.None, 0)
            End If

            MenuY += 75
        Next
        Dim MenuX As Integer = MenuPos.X + 20
        Globals.SpriteBatch.End()
    End Sub


End Class
