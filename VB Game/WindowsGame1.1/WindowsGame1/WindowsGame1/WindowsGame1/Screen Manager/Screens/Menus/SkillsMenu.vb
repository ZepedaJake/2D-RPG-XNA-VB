﻿Public Class SkillsMenu
    Inherits BaseScreen

    Public Entries As New List(Of MenuEntry)
    Public MenuSelect As Integer = 0

    Private MenuSize As New Vector2(Globals.GameSize.X - 100, Globals.GameSize.Y - 100)
    Private MenuPos As New Vector2((Globals.GameSize.X - MenuSize.X) / 2, 0)


    Public Sub New()
        Name = "StatsMenu"
        State = ScreenState.Active


        'Add entries
        AddEntry("Upgrade", True)
        AddEntry("Upgrade", True)
        AddEntry("Upgrade", True)
        AddEntry("Upgrade", True)
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
        If WorldScreen.SkillPoints = 0 Then
            Entries.Item(0).Enabled = False
            Entries.Item(1).Enabled = False
            Entries.Item(2).Enabled = False
            Entries.Item(3).Enabled = False
            MenuSelect = 4
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
                    WorldScreen.SkillAttack += 1
                    WorldScreen.SkillPoints -= 1


                Case 1
                    WorldScreen.SkillDefend += 1
                    WorldScreen.SkillPoints -= 1
                Case 2
                    WorldScreen.SkillPower += 1
                    WorldScreen.SkillPoints -= 1
                Case 3
                    WorldScreen.SkillHeal += 1
                    WorldScreen.SkillPoints -= 1

                Case 4
                    ScreenManager.UnloadScreen("StatsMenu")
                    Status.ShowingMenu = False

            End Select
        End If

    End Sub

    Public Overrides Sub Draw()
        Globals.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone)
        'Draw Menu Graphics
        Globals.SpriteBatch.Draw(Textures.SpriteSheet1, New Rectangle(MenuPos.X, MenuPos.Y, MenuSize.X, MenuSize.Y), New Rectangle(0, 0, 1, 1), Color.White)
        Globals.SpriteBatch.DrawString(Fonts.Arial_8, "Skill Points: " + WorldScreen.SkillPoints.ToString, New Vector2(250, MenuPos.Y), Color.White, 0, New Vector2(0, 0), 2, SpriteEffects.None, 0)

        Globals.SpriteBatch.DrawString(Fonts.Arial_8, "Attack Lvl " + WorldScreen.SkillAttack.ToString, New Vector2(250, MenuPos.Y + 50), Color.White, 0, New Vector2(0, 0), 2, SpriteEffects.None, 0)
        Globals.SpriteBatch.DrawString(Fonts.Arial_8, "Bonus: " + (WorldScreen.SkillAttack * 35).ToString + "%", New Vector2(280, MenuPos.Y + 90), Color.White, 0, New Vector2(0, 0), 1.5, SpriteEffects.None, 0)
        Globals.SpriteBatch.DrawString(Fonts.Arial_8, "Effective Attack: " + (WorldScreen.ToonAtk * (1 + (WorldScreen.SkillAttack * 0.35))).ToString, New Vector2(280, MenuPos.Y + 115), Color.White, 0, New Vector2(0, 0), 1.5, SpriteEffects.None, 0)

        Globals.SpriteBatch.DrawString(Fonts.Arial_8, "Defense Lvl " + WorldScreen.SkillDefend.ToString, New Vector2(250, MenuPos.Y + 150), Color.White, 0, New Vector2(0, 0), 2, SpriteEffects.None, 0)
        Globals.SpriteBatch.DrawString(Fonts.Arial_8, "Bonus: " + (WorldScreen.SkillDefend * 40).ToString + "%", New Vector2(280, MenuPos.Y + 190), Color.White, 0, New Vector2(0, 0), 1.5, SpriteEffects.None, 0)
        Globals.SpriteBatch.DrawString(Fonts.Arial_8, "Effective Defense: " + (WorldScreen.ToonDef * (2 + (WorldScreen.SkillDefend * 0.4))).ToString, New Vector2(280, MenuPos.Y + 215), Color.White, 0, New Vector2(0, 0), 1.5, SpriteEffects.None, 0)

        Globals.SpriteBatch.DrawString(Fonts.Arial_8, "Power Lvl " + WorldScreen.SkillPower.ToString, New Vector2(250, MenuPos.Y + 250), Color.White, 0, New Vector2(0, 0), 2, SpriteEffects.None, 0)
        Globals.SpriteBatch.DrawString(Fonts.Arial_8, "Bonus: " + ((WorldScreen.SkillPower / 5) * 100).ToString + "%", New Vector2(280, MenuPos.Y + 290), Color.White, 0, New Vector2(0, 0), 1.5, SpriteEffects.None, 0)
        Globals.SpriteBatch.DrawString(Fonts.Arial_8, "Effective Attack: " + (WorldScreen.ToonAtk * (2 + (WorldScreen.SkillPower / 2.5))).ToString, New Vector2(280, MenuPos.Y + 315), Color.White, 0, New Vector2(0, 0), 1.5, SpriteEffects.None, 0)

        Globals.SpriteBatch.DrawString(Fonts.Arial_8, "Heal Lvl " + WorldScreen.SkillHeal.ToString, New Vector2(250, MenuPos.Y + 350), Color.White, 0, New Vector2(0, 0), 2, SpriteEffects.None, 0)
        Globals.SpriteBatch.DrawString(Fonts.Arial_8, "Heal %: " + (((1 + WorldScreen.SkillHeal) / 20) * 100).ToString("f2"), New Vector2(280, MenuPos.Y + 390), Color.White, 0, New Vector2(0, 0), 1.5, SpriteEffects.None, 0)
        Globals.SpriteBatch.DrawString(Fonts.Arial_8, "Heal Amount: " + ((WorldScreen.ToonLvl * 200) * ((1 + WorldScreen.SkillHeal) / 20)).ToString("f2"), New Vector2(280, MenuPos.Y + 415), Color.White, 0, New Vector2(0, 0), 1.5, SpriteEffects.None, 0)

        'Draw Menu Options
        Dim MenuY As Integer = MenuPos.Y
        'For x = 0 To Entries.Count - 1
        '    If x = MenuSelect Then
        '        Globals.SpriteBatch.DrawString(Fonts.Arial_8, Entries.Item(x).Text, New Vector2(600, MenuY), New Color(r, g, b), 0, New Vector2(0, 0), 2.75, SpriteEffects.None, 0)
        '    ElseIf Entries.Item(x).Enabled = True Then
        '        Globals.SpriteBatch.DrawString(Fonts.Arial_8, Entries.Item(x).Text, New Vector2(600, MenuY), Color.White, 0, New Vector2(0, 0), 2, SpriteEffects.None, 0)
        '    Else
        '        Globals.SpriteBatch.DrawString(Fonts.Arial_8, Entries.Item(x).Text, New Vector2(600, MenuY), Color.Gray, 0, New Vector2(0, 0), 2, SpriteEffects.None, 0)
        '    End If

        '    MenuY += 120
        'Next

        With Entries.Item(0)
            If MenuSelect = 0 Then
                Globals.SpriteBatch.DrawString(Fonts.Arial_8, Entries.Item(0).Text, New Vector2(400, MenuPos.Y + 45), New Color(r, g, b), 0, New Vector2(0, 0), 2.75, SpriteEffects.None, 0)
            ElseIf Entries.Item(0).Enabled Then
                Globals.SpriteBatch.DrawString(Fonts.Arial_8, Entries.Item(0).Text, New Vector2(400, MenuPos.Y + 50), Color.White, 0, New Vector2(0, 0), 2, SpriteEffects.None, 0)
            Else
                Globals.SpriteBatch.DrawString(Fonts.Arial_8, Entries.Item(0).Text, New Vector2(400, MenuPos.Y + 50), Color.Gray, 0, New Vector2(0, 0), 2, SpriteEffects.None, 0)
            End If
        End With

        With Entries.Item(1)
            If MenuSelect = 1 Then
                Globals.SpriteBatch.DrawString(Fonts.Arial_8, Entries.Item(1).Text, New Vector2(400, MenuPos.Y + 145), New Color(r, g, b), 0, New Vector2(0, 0), 2.75, SpriteEffects.None, 0)
            ElseIf Entries.Item(1).Enabled Then
                Globals.SpriteBatch.DrawString(Fonts.Arial_8, Entries.Item(1).Text, New Vector2(400, MenuPos.Y + 150), Color.White, 0, New Vector2(0, 0), 2, SpriteEffects.None, 0)
            Else
                Globals.SpriteBatch.DrawString(Fonts.Arial_8, Entries.Item(1).Text, New Vector2(400, MenuPos.Y + 150), Color.Gray, 0, New Vector2(0, 0), 2, SpriteEffects.None, 0)
            End If
        End With

        With Entries.Item(2)
            If MenuSelect = 2 Then
                Globals.SpriteBatch.DrawString(Fonts.Arial_8, Entries.Item(2).Text, New Vector2(400, MenuPos.Y + 245), New Color(r, g, b), 0, New Vector2(0, 0), 2.75, SpriteEffects.None, 0)
            ElseIf Entries.Item(2).Enabled Then
                Globals.SpriteBatch.DrawString(Fonts.Arial_8, Entries.Item(2).Text, New Vector2(400, MenuPos.Y + 250), Color.White, 0, New Vector2(0, 0), 2, SpriteEffects.None, 0)
            Else
                Globals.SpriteBatch.DrawString(Fonts.Arial_8, Entries.Item(2).Text, New Vector2(400, MenuPos.Y + 250), Color.Gray, 0, New Vector2(0, 0), 2, SpriteEffects.None, 0)
            End If
        End With

        With Entries.Item(3)
            If MenuSelect = 3 Then
                Globals.SpriteBatch.DrawString(Fonts.Arial_8, Entries.Item(3).Text, New Vector2(400, MenuPos.Y + 345), New Color(r, g, b), 0, New Vector2(0, 0), 2.75, SpriteEffects.None, 0)
            ElseIf Entries.Item(3).Enabled Then
                Globals.SpriteBatch.DrawString(Fonts.Arial_8, Entries.Item(3).Text, New Vector2(400, MenuPos.Y + 350), Color.White, 0, New Vector2(0, 0), 2, SpriteEffects.None, 0)
            Else
                Globals.SpriteBatch.DrawString(Fonts.Arial_8, Entries.Item(3).Text, New Vector2(400, MenuPos.Y + 350), Color.Gray, 0, New Vector2(0, 0), 2, SpriteEffects.None, 0)
            End If
        End With


        With Entries.Item(4)
            If MenuSelect = 4 Then
                Globals.SpriteBatch.DrawString(Fonts.Arial_8, Entries.Item(4).Text, New Vector2(350, MenuPos.Y + MenuSize.Y - 50), Color.White, 0, New Vector2(0, 0), PauseMenu.size, SpriteEffects.None, 0)
            ElseIf Entries.Item(4).Enabled Then
                Globals.SpriteBatch.DrawString(Fonts.Arial_8, Entries.Item(4).Text, New Vector2(350, MenuPos.Y + MenuSize.Y - 50), Color.White, 0, New Vector2(0, 0), 2, SpriteEffects.None, 0)
            Else
                Globals.SpriteBatch.DrawString(Fonts.Arial_8, Entries.Item(4).Text, New Vector2(600, MenuSize.Y - 40), Color.Gray, 0, New Vector2(0, 0), 2, SpriteEffects.None, 0)
            End If
        End With

        Dim MenuX As Integer = MenuPos.X + 20
        Globals.SpriteBatch.End()
    End Sub
End Class