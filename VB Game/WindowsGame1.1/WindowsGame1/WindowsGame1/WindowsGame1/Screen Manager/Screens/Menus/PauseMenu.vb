Public Class PauseMenu

    Inherits BaseScreen

    Public Entries As New List(Of MenuEntry)
    Public MenuSelect As Integer = 0

    Private MenuSize As New Vector2(200, 400)
    Private MenuPos As New Vector2((Globals.GameSize.X - MenuSize.X) / 2, 100)


    Public Sub New()
        Name = "PauseMenu"
        State = ScreenState.Active

        'Add entries
        AddEntry("Status", True)
        AddEntry("Title", True)
        AddEntry("Close", True)


    End Sub


    Private r As Integer = 0
    Private g As Integer = 0
    Private b As Integer = 192

    Private rnd As Random = New Random
    Public Shared size As Double
    Private AniTime As Double = 0
    Public Overrides Sub Update()
        'Shrink / grow
        AniTime += Globals.GameTime.ElapsedGameTime.TotalMilliseconds / 200

        If AniTime > 180 Then
            AniTime = 0
        End If

        size = ((Math.Sin(AniTime)) / 4) + 2.5
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
        If WorldScreen.ShowingStats = False Then
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
            'If Input.KeyPressed(Keys.Enter) Then
            '    Select Case MenuSelect
            '        Case 0
            '            
            '            State = ScreenState.Shutdown
            '            ScreenManager.AddScreen(New StatsMenu)
            '        Case 1
            '            
            '            ScreenManager.UnloadScreen("WorldScreen")
            '            ScreenManager.UnloadScreen("PauseMenu")
            '            ScreenManager.AddScreen(New TitleScreen())
            '            ScreenManager.AddScreen(New MainMenu())
            '        Case 2
            '            
            '            ScreenManager.UnloadScreen("PauseMenu")

            '    End Select
            'End If
        End If
    End Sub

    Public Overrides Sub Draw()
        Globals.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone)
        'Draw Menu Graphics
        Globals.SpriteBatch.Draw(Textures.SpriteSheet1, New Rectangle(MenuPos.X, MenuPos.Y, MenuSize.X, MenuSize.Y), New Rectangle(0, 0, 1, 1), Color.White)
        'Draw Menu Options
        Dim MenuY As Integer = MenuPos.Y + (1 * (MenuSize.Y / (Entries.Count * 2 + 1)))
        For x = 0 To Entries.Count - 1
            If x = MenuSelect Then
                Globals.SpriteBatch.DrawString(Fonts.Arial_8, Entries.Item(x).Text, New Vector2(MenuPos.X + (MenuSize.X / 2) - Fonts.Arial_8.MeasureString(Entries.Item(x).Text).X / 2, MenuY), Color.White, 0, New Vector2(0, 0), size, SpriteEffects.None, 0)
            ElseIf Entries.Item(x).Enabled = True Then
                Globals.SpriteBatch.DrawString(Fonts.Arial_8, Entries.Item(x).Text, New Vector2(MenuPos.X + (MenuSize.X / 2) - Fonts.Arial_8.MeasureString(Entries.Item(x).Text).X / 2, MenuY), Color.White, 0, New Vector2(0, 0), 2, SpriteEffects.None, 0)
            Else
                Globals.SpriteBatch.DrawString(Fonts.Arial_8, Entries.Item(x).Text, New Vector2(MenuPos.X + (MenuSize.X / 2) - Fonts.Arial_8.MeasureString(Entries.Item(x).Text).X / 2, MenuY), Color.Gray, 0, New Vector2(0, 0), 2, SpriteEffects.None, 0)
            End If

            MenuY += 2 * (MenuSize.Y / (Entries.Count * 2 + 1))
        Next
        Dim MenuX As Integer = MenuPos.X + 20
        Globals.SpriteBatch.End()
    End Sub




End Class


