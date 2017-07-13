Imports System.IO

Public Class Message


    Inherits BaseScreen

    Private Entries As New List(Of MenuEntry)
    Private MenuSelect As Integer = 0

    Private MenuSize As New Vector2(550, 175)
    Private MenuPos As New Vector2((Globals.GameSize.X / 2) - MenuSize.X / 2, 300)

    Private BoxText As String = ""
    Private MenuColor As String

    Private TileSize As Integer = 64
    Private RR As Integer = 0
    Private GG As Integer = 0
    Private BB As Integer = 0

    Private currentDialouge As String

    Public Song As SoundEffectInstance
    Public Playing As Boolean = False


    Public Sub New(t As String, r As Integer, g As Integer, b As Integer)

        MainMenu.InDialouge = True
        Name = "Message"
        State = ScreenState.Active
        currentDialouge = t

        If IO.File.Exists(t & "/" & t & Game1.MessageCounter & ".txt") Then
            Try
                FileOpen(Game1.MessageCounter, t & "/" & t & Game1.MessageCounter & ".txt", OpenMode.Input)
                ' Loop until end of file. 
                Do While Not EOF(Game1.MessageCounter)
                    ' Read line into variable.
                    BoxText += LineInput(Game1.MessageCounter) & vbCrLf
                Loop
                FileClose(Game1.MessageCounter)
            Catch
                ScreenManager.UnloadScreen("Message")
            End Try
        Else
            ScreenManager.UnloadScreen("Message")
        End If
        'Add entries
        AddEntry("Ok", True)


        MenuSize = New Vector2(60 + Fonts.Arial_20.MeasureString(BoxText).X, Fonts.Arial_20.MeasureString(BoxText).Y + 100)
        MenuPos = New Vector2((Globals.GameSize.X / 2) - (MenuSize.X / 2), Globals.GameSize.Y - MenuSize.Y)
        RR = r
        GG = g
        BB = b


    End Sub


    Private size As Double
    Private AniTime As Double = 0
    Public Overrides Sub Update()
        'Shrink / grow
        AniTime += Globals.GameTime.ElapsedGameTime.TotalMilliseconds / 400

        If AniTime > 100 Then
            AniTime = 0
        End If

        size = ((Math.Sin(AniTime)) / 4) + 1

        'If Playing = False Then
        '    Song = Music.Dialouge.CreateInstance()
        '    Song.IsLooped = True
        '    Song.Play()
        '    Playing = True
        'End If
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

        'quit message
        If Input.KeyPressed(Keys.Escape) Then

            ScreenManager.UnloadScreen("Message")
            Select Case currentDialouge
                Case "newGameText"
                    ScreenManager.UnloadScreen("IntroScreen")
                    ScreenManager.AddScreen(New WorldScreen(1))
                Case "bossText"
                    WorldScreen.BossFight = True
                    WorldScreen.BattleMenu = New BattleMenu
                    ScreenManager.AddScreen(WorldScreen.BattleMenu)
                Case "endText"

                    Try : Game1.SongPlay.Dispose() : Catch : End Try
                    WorldScreen.MapSong = 0
                    Game1.SongPlaying = False
                    ScreenManager.AddScreen(New CreditsMenu())

            End Select

            Game1.MessageCounter = 1
            MainMenu.InDialouge = False
        End If

        'Invoke Menu Item
        If Input.KeyPressed(Keys.Enter) Then
            Select Case MenuSelect
                Case 0
                    Game1.MessageCounter += 1
                    ScreenManager.UnloadScreen("Message")
                    If IO.File.Exists(currentDialouge & "/" & currentDialouge & Game1.MessageCounter & ".txt") Then
                        ScreenManager.AddScreen(New Message(currentDialouge, 255, 255, 255))
                    Else
                        Select Case currentDialouge
                            Case "newGameText"
                                ScreenManager.UnloadScreen("IntroScreen")
                                ScreenManager.AddScreen(New WorldScreen(1))
                            Case "bossText"
                                WorldScreen.BossFight = True
                                WorldScreen.BattleMenu = New BattleMenu
                                ScreenManager.AddScreen(WorldScreen.BattleMenu)
                            Case "endText"

                                Try : Game1.SongPlay.Dispose() : Catch : End Try
                                WorldScreen.MapSong = 0
                                Game1.SongPlaying = False
                                ScreenManager.AddScreen(New CreditsMenu())

                        End Select

                        Game1.MessageCounter = 1
                        MainMenu.InDialouge = False

                        Try : Game1.SongPlay.Dispose() : Catch : End Try

                    End If
            End Select
        End If
    End Sub

    Public Overrides Sub Draw()
        Globals.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone)
        'Draw Menu Graphics
        'Globals.SpriteBatch.Draw(Textures.SpriteSheet1, New Rectangle(MenuPos.X, MenuPos.Y, MenuSize.X, MenuSize.Y), New Rectangle(45, 54, 1, 1), Color.Red * 0.7F)
        Globals.SpriteBatch.Draw(Textures.Menu, New Rectangle(MenuPos.X, MenuPos.Y, TileSize, TileSize), New Rectangle(0, 0, 16, 16), New Color(RR, GG, BB))
        Globals.SpriteBatch.Draw(Textures.Menu, New Rectangle(MenuPos.X + TileSize, MenuPos.Y, MenuSize.X - (TileSize * 2) + 1, TileSize), New Rectangle(16, 0, 1, 16), New Color(RR, GG, BB))
        Globals.SpriteBatch.Draw(Textures.Menu, New Rectangle(MenuPos.X + MenuSize.X - TileSize, MenuPos.Y, TileSize, TileSize), New Rectangle(16, 0, 16, 16), New Color(RR, GG, BB))

        'mid layer
        Globals.SpriteBatch.Draw(Textures.Menu, New Rectangle(MenuPos.X, MenuPos.Y + TileSize, TileSize, MenuSize.Y - (TileSize * 2)), New Rectangle(0, 16, 16, 1), New Color(RR, GG, BB))
        Globals.SpriteBatch.Draw(Textures.Menu, New Rectangle(MenuPos.X + TileSize, MenuPos.Y + TileSize, MenuSize.X - (TileSize * 2) + 1, MenuSize.Y - (TileSize * 2)), New Rectangle(16, 16, 1, 1), New Color(RR, GG, BB))
        Globals.SpriteBatch.Draw(Textures.Menu, New Rectangle(MenuSize.X + MenuPos.X - TileSize, MenuPos.Y + TileSize, TileSize, MenuSize.Y - (TileSize * 2)), New Rectangle(16, 16, 16, 1), New Color(RR, GG, BB))

        'Bottom Layer
        Globals.SpriteBatch.Draw(Textures.Menu, New Rectangle(MenuPos.X, MenuSize.Y + MenuPos.Y - TileSize, TileSize, TileSize), New Rectangle(0, 16, 16, 16), New Color(RR, GG, BB))
        Globals.SpriteBatch.Draw(Textures.Menu, New Rectangle(MenuPos.X + TileSize, MenuSize.Y + MenuPos.Y - TileSize, MenuSize.X - (TileSize * 2) + 1, TileSize), New Rectangle(16, 16, 1, 16), New Color(RR, GG, BB))
        Globals.SpriteBatch.Draw(Textures.Menu, New Rectangle(MenuPos.X + MenuSize.X - TileSize, MenuSize.Y + MenuPos.Y - TileSize, TileSize, TileSize), New Rectangle(16, 16, 16, 16), New Color(RR, GG, BB))




        Globals.SpriteBatch.DrawString(Fonts.Arial_20, BoxText, New Vector2(MenuPos.X + (MenuSize.X / 2) - Fonts.Arial_20.MeasureString(BoxText).X / 2, MenuPos.Y + 20), Color.White)
        Globals.SpriteBatch.DrawString(Fonts.Arial_20, BoxText, New Vector2(1 + MenuPos.X + (MenuSize.X / 2) - Fonts.Arial_20.MeasureString(BoxText).X / 2, MenuPos.Y + 21), Color.Black)


        'Draw Menu Options
        Dim MenuY As Integer = MenuPos.Y + MenuSize.Y - 60
        For x = 0 To Entries.Count - 1

            If x = MenuSelect Then

                Globals.SpriteBatch.DrawString(Fonts.Arial_20, Entries.Item(x).Text, New Vector2(MenuPos.X + (MenuSize.X / 2) - Fonts.Arial_20.MeasureString(Entries.Item(x).Text).X / 2, MenuY), Color.White, 0, New Vector2(0, 0), size, SpriteEffects.None, 0)
                Globals.SpriteBatch.DrawString(Fonts.Arial_20, Entries.Item(x).Text, New Vector2(2 + MenuPos.X + (MenuSize.X / 2) - Fonts.Arial_20.MeasureString(Entries.Item(x).Text).X / 2, MenuY + 2), Color.Black, 0, New Vector2(0, 0), size, SpriteEffects.None, 0)

            ElseIf Entries.Item(x).Enabled = True Then
                Globals.SpriteBatch.DrawString(Fonts.Arial_8, Entries.Item(x).Text, New Vector2(MenuPos.X + (MenuSize.X / 2), MenuY), Color.White, 0, New Vector2(0, 0), 2, SpriteEffects.None, 0)
                Globals.SpriteBatch.DrawString(Fonts.Arial_8, Entries.Item(x).Text, New Vector2(MenuPos.X + (MenuSize.X / 2), MenuY + 2), Color.Black, 0, New Vector2(0, 0), 2, SpriteEffects.None, 0)

            End If
            ''New Vector2(MenuPos.X + (MenuSize.X / 2) - Fonts.Arial_20.MeasureString(Entries.Item(x).Text).X / 2, MenuY)
            ''New Vector2(MenuPos.X + (MenuSize.X / 2), MenuY)
            MenuY += 40
        Next
        Dim MenuX As Integer = MenuPos.X + 20
        Globals.SpriteBatch.End()
    End Sub






End Class
