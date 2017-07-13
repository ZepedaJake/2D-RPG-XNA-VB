Public Class BattleMenu
    Inherits BaseScreen

    Public Entries As New List(Of MenuEntry)
    Public MenuSelect As Integer = 0

    Private MenuSize As New Vector2(250, 160)
    Private MenuPos As New Vector2(50, 100)
    Public World As WorldScreen


    Public Sub New()
        Name = "BattleMenu"
        State = ScreenState.Active

        'Add entries
        AddEntry("Attack", True)
        AddEntry("Defend", True)
        AddEntry("Power", False)
        AddEntry("Heal", False)
        AddEntry("Escape", True)

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
        'If Input.KeyPressed(Keys.Enter) Then
        '    Select Case MenuSelect
        '        Case 0
        '            
        '        Case 1
        '            
        '        Case 2
        '           
        '        Case 3
        '            
        '    End Select
        'End If
    End Sub
    Private sRect As Rectangle
    Public Overrides Sub Draw()
        Globals.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone)
        'Draw Menu Graphics


        'Draw Menu Options
        Dim MenuY As Integer = MenuPos.Y + 20
        For x = 0 To Entries.Count - 1
            Select Case x
                Case 0
                    sRect = New Rectangle(0, 32, 32, 32)
                Case 1
                    sRect = New Rectangle(0, 64, 32, 32)
                Case 2
                    sRect = New Rectangle(32, 32, 32, 32)
                Case 3
                    sRect = New Rectangle(32, 64, 32, 32)
                Case 4
                    sRect = New Rectangle(64, 32, 32, 32)
            End Select

            If x = MenuSelect Then
                Globals.SpriteBatch.Draw(Textures.Menu, New Rectangle(MenuPos.X, MenuY, 96, 96), sRect, Color.White)

                'Globals.SpriteBatch.DrawString(Fonts.Arial_8, Entries.Item(x).Text, New Vector2(MenuPos.X, MenuY), Color.Blue, 0, New Vector2(0, 0), 2.5, SpriteEffects.None, 0)
            ElseIf Entries.Item(x).Enabled = True Then
                Globals.SpriteBatch.Draw(Textures.Menu, New Rectangle(MenuPos.X, MenuY, 96, 96), sRect, Color.Gray)

                'Globals.SpriteBatch.DrawString(Fonts.Arial_8, Entries.Item(x).Text, New Vector2(MenuPos.X, MenuY), Color.White, 0, New Vector2(0, 0), 2.5, SpriteEffects.None, 0)
            Else
                Globals.SpriteBatch.Draw(Textures.Menu, New Rectangle(MenuPos.X, MenuY, 96, 96), sRect, New Color(50, 50, 50))

                'Globals.SpriteBatch.DrawString(Fonts.Arial_8, Entries.Item(x).Text, New Vector2(MenuPos.X, MenuY), Color.Gray, 0, New Vector2(0, 0), 2.5, SpriteEffects.None, 0)
            End If

            MenuY += 106
        Next
        Dim MenuX As Integer = MenuPos.X + 20
        Globals.SpriteBatch.End()
    End Sub




End Class
