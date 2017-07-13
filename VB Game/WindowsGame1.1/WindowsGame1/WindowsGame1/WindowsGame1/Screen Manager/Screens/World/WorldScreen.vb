Public Class WorldScreen
    Inherits BaseScreen

    'MapDimensions
    Public Map As MapBase
    Public Interacters As InteractBase
    Public Enemies As EnemyBase
    Public MapWidth As Integer = 32
    Public MapHeight As Integer = 32
    Public TileSize As Integer = 64

    'Current Coord
    Public MapX As Integer = 0
    Public MapY As Integer = 0


    'SpriteSources
    Private sRect As Rectangle
    Dim MH As New MapHandler

    Dim DebugScreen As New Debug
    Public Coords As Boolean = False
    Public Shared MapSong As Integer
    Public Shared LoadThisMap As String
    Public Shared NextMap As String
    Public Shared CurrentFloor As Integer
    Public Shared ReturnPoint As New Vector2

    Public Sub New(m As Integer)

        MapSong = m
        'test will later need to be changed to floor once maps are ACTUALLY being made********************************************
        LoadThisMap = "floor" + m.ToString()
        CurrentFloor = m
        Name = LoadThisMap

        'toggle on and off for hard codded maps********************************************************
        If IO.File.Exists("saves/" & LoadThisMap & "Enemies.tmp") Then
            Enemies = MH.ReloadEnemies(LoadThisMap)
        Else
            Enemies = MH.LoadEnemies(LoadThisMap)
        End If

        If IO.File.Exists("saves/" & LoadThisMap & "Interacters.tmp") Then
            Interacters = MH.ReloadInteract(LoadThisMap)
        Else
            Interacters = MH.LoadInteract(LoadThisMap)
        End If

        If IO.File.Exists("saves/" & LoadThisMap & "Map.tmp") Then
            Map = MH.ReLoadMap(LoadThisMap)
        Else
            Map = MH.LoadMap(LoadThisMap)

        End If
        NextMap = "floor"

        'end toggle********************************************************************************************

        'switch map mh and new map for load / save
        'available test and test 2
        'test1 and 2 use old sprite sheet delete them asap. use test 3
        'test 3 is only with map base. test 4 uses map and interact base
        'Test 5 has enemybase class with it
        'Interacters = MH.LoadInteract(LoadThisMap)


        'use to load a hard coded map
        'Only map 13 Left

        'Map = New MapBase(15, 13, New Vector2((ToonScreenX - 10) + 3, (ToonScreenY - 10) + 11))
        'Interacters = New InteractBase(MapWidth, MapHeight)
        'Enemies = New EnemyBase(MapWidth, MapHeight)

        MapX = Map.StartLocation.X
        MapY = Map.StartLocation.Y
        MapWidth = Map.MapWidth
        MapHeight = Map.MapHeight

        WillPause = True

        'things might be getting saved in world screen. reset them somewhere...e43
        'current stats are for testing remember to fix this 
        If IO.File.Exists("saves/terry.sav") Then
            MH.LoadTerry()
        Else ' SET UP A STATUS SCREEEN USE PUBLIC SHARED TO MAKE THINGS EASIER
            'fix toon atk also
            'boss ai done
            ToonHP = 100

            ToonAtk = 5
            ToonAllAtk = 0
            StatAtkBoost = 0
            ItemAtkBoost = 0

            ToonDef = 0
            ToonAllDef = 0
            StatDefBoost = 0
            ItemDefBoost = 0

            ToonLvl = 0
            XpTillLvl = (ToonLvl * 2) ^ 2 + 10
            StatPoints = 0
            SkillPoints = 0

            SkillAttack = 0
            SkillDefend = 0
            SkillPower = 0
            SkillHeal = 0

            BronzeKeys = 0
            SilverKeys = 0
            GoldKeys = 0
        End If

        If DeathMenu.ResetStats = True Then
            ResetStatPoints()
            ResetSkillPoints()
            DeathMenu.ResetStats = False
        End If
    End Sub

    Public Shared WillPause As Boolean = True
    Public Shared CanMove As Boolean = True
    Public Right As Boolean = True
    Public Pause As New PauseMenu
    Public Stats As New StatsMenu
    Public Status As New Status
    Public Overrides Sub HandleInput()
        If ToonOffsetX = 0 And ToonOffsetY = 0 And ToonMoving = False And Battle = False AndAlso WillPause = True And CanMove = True Then
            If Input.KeyDown(Keys.S) Then
                Try
                    If Interacters.InteractList(ToonX, ToonY + 1).InteractItem = InteractType.GoldDoor And GoldKeys > 0 Then
                        OpenDoor(ToonX, ToonY + 1)
                    ElseIf Interacters.InteractList(ToonX, ToonY + 1).InteractItem = InteractType.SilverDoor And SilverKeys > 0 Then
                        OpenDoor(ToonX, ToonY + 1)
                    ElseIf Interacters.InteractList(ToonX, ToonY + 1).InteractItem = InteractType.BronzeDoor And BronzeKeys > 0 Then
                        OpenDoor(ToonX, ToonY + 1)

                    End If

                Catch
                End Try
                MoveToon(1, ToonX, ToonY + 1)

                'If ToonMoving = False Then
                '    Try : OpenDoor(ToonFrontX, ToonFrontY) : Catch : End Try
                'End If

                LastDir = 1
                Right = Not Right
            ElseIf Input.KeyDown(Keys.W) Then
                'Do this for other doors and directions!!!!!!!!!!!!!!!!!!
                Try
                    If Interacters.InteractList(ToonX, ToonY - 1).InteractItem = InteractType.GoldDoor And GoldKeys > 0 Then
                        OpenDoor(ToonX, ToonY - 1)
                    ElseIf Interacters.InteractList(ToonX, ToonY - 1).InteractItem = InteractType.SilverDoor And SilverKeys > 0 Then
                        OpenDoor(ToonX, ToonY - 1)
                    ElseIf Interacters.InteractList(ToonX, ToonY - 1).InteractItem = InteractType.BronzeDoor And BronzeKeys > 0 Then
                        OpenDoor(ToonX, ToonY - 1)

                    End If

                Catch
                End Try
                MoveToon(4, ToonX, ToonY - 1)

                'If ToonMoving = False Then
                '    Try : OpenDoor(ToonFrontX, ToonFrontY) : Catch : End Try
                'End If

                LastDir = 4
                Right = Not Right
            ElseIf Input.KeyDown(Keys.A) Then
                Try
                    If Interacters.InteractList(ToonX - 1, ToonY).InteractItem = InteractType.SideGoldDoor And GoldKeys > 0 Then
                        OpenDoor(ToonX - 1, ToonY)
                    ElseIf Interacters.InteractList(ToonX - 1, ToonY).InteractItem = InteractType.SideSilverDoor And SilverKeys > 0 Then
                        OpenDoor(ToonX - 1, ToonY)
                    ElseIf Interacters.InteractList(ToonX - 1, ToonY).InteractItem = InteractType.SideBronzeDoor And BronzeKeys > 0 Then
                        OpenDoor(ToonX - 1, ToonY)

                    End If

                Catch
                End Try
                MoveToon(2, ToonX - 1, ToonY)

                'If ToonMoving = False Then
                '    Try : OpenDoor(ToonFrontX, ToonFrontY) : Catch : End Try
                'End If

                LastDir = 2
                Right = Not Right
            ElseIf Input.KeyDown(Keys.D) Then
                Try
                    If Interacters.InteractList(ToonX + 1, ToonY).InteractItem = InteractType.SideGoldDoor And GoldKeys > 0 Then
                        OpenDoor(ToonX + 1, ToonY)
                    ElseIf Interacters.InteractList(ToonX + 1, ToonY).InteractItem = InteractType.SideSilverDoor And SilverKeys > 0 Then
                        OpenDoor(ToonX + 1, ToonY)
                    ElseIf Interacters.InteractList(ToonX + 1, ToonY).InteractItem = InteractType.SideBronzeDoor And BronzeKeys > 0 Then
                        OpenDoor(ToonX + 1, ToonY)

                    End If

                Catch
                End Try
                MoveToon(3, ToonX + 1, ToonY)

                'If ToonMoving = False Then
                '    Try : OpenDoor(ToonFrontX, ToonFrontY) : Catch : End Try
                'End If

                LastDir = 3
                Right = Not Right
            Else
                MoveDir = 0
            End If
        End If
        Try
            If WillPause = True Then

                If CanMove And Input.KeyPressed(Keys.Enter) Then

                    If Interacters.InteractList(ToonFrontX, ToonFrontY).InteractItem = InteractType.HandLeft Then
                        ScreenManager.AddScreen(New YesNo("Reset Stat Points?", 255, 255, 255))
                        CanMove = False
                    ElseIf Interacters.InteractList(ToonFrontX, ToonFrontY).InteractItem = InteractType.HandRight Then
                        ScreenManager.AddScreen(New YesNo("Reset Skill Points?", 255, 255, 255))
                        CanMove = False
                    ElseIf Interacters.InteractList(ToonFrontX, ToonFrontY).InteractItem = InteractType.Hood And BossFight = False Then
                        StartBoss()
                        CanMove = False
                    End If


                    'ElseIf Input.KeyDown(Keys.Enter) And Interacters.InteractList(ToonFrontX, ToonFrontY).InteractItem = InteractType.Hood And CanMove = True And BossFight = False Then
                    '    CanMove = False
                    '        StartBoss()
                End If
            End If
        Catch
        End Try

        'If Input.KeyDown(Keys.L) Then
        'this is used for saving hard coded maps atm. later make save functions for saving between map changing and continuing a game
        'MH.SaveMap(Map, LoadThisMap)
        'MH.SaveInteract(Interacters, LoadThisMap)
        'MH.SaveEnemies(Enemies, LoadThisMap)
        'MsgBox("Saved" & LoadThisMap)
        'End If



        If Input.KeyPressed(Keys.Escape) And WillPause And Battle = False Then
            Pause = New PauseMenu
            ScreenManager.AddScreen(Pause)
            WillPause = False
        ElseIf Input.KeyPressed(Keys.Escape) And WillPause = False AndAlso ShowingStats = False Then
            WillPause = True
            ScreenManager.UnloadScreen(Pause.Name)

        End If

        PauseStuff()
        'StatStuff()
    End Sub

    Public Shared Sub ResetStatPoints()
        ToonAtk -= StatAtkBoost
        ToonDef -= StatDefBoost
        StatPoints += ToonAllAtk + ToonAllDef
        ToonAllAtk = 0
        ToonAllDef = 0
        StatAtkBoost = 0
        StatDefBoost = 0
    End Sub

    Public Shared Sub ResetSkillPoints()
        SkillPoints += ToonLvl - SkillPoints
        SkillAttack = 0
        SkillDefend = 0
        SkillPower = 0
        SkillHeal = 0
    End Sub

    Public Shared ShowingStats As Boolean = False

    Public Sub PauseStuff()
        If WillPause = False Then

            If Input.KeyPressed(Keys.Enter) Then
                Select Case Pause.MenuSelect
                    Case 0
                        If ShowingStats = False Then
                            ShowingStats = True
                            Status = New Status
                            ScreenManager.AddScreen(New Status)
                        End If

                    Case 1

                        If ShowingStats = False Then

                            MH.SaveOnClose()
                            MH.SaveTerry()
                            MH.ReSaveEnemies(Enemies, LoadThisMap)
                            MH.ReSaveInteract(Interacters, LoadThisMap)

                            ScreenManager.UnloadScreen(LoadThisMap)
                            ScreenManager.UnloadScreen("PauseMenu")
                            Try : Game1.SongPlay.Dispose() : Catch : End Try
                            MapSong = 0
                            Game1.SongPlaying = False
                            ScreenManager.AddScreen(New TitleScreen())
                            'ScreenManager.AddScreen(New MainMenu())
                        End If
                    Case 2
                        If ShowingStats = False Then


                            ScreenManager.UnloadScreen(Pause.Name)
                            WillPause = True
                        End If


                End Select

            End If


        End If
    End Sub

    Public StatLag As Double = 2
    'Public Sub StatStuff()

    '    If ShowingStats = True Then
    '        If StatLag > 0 Then
    '            StatLag -= Globals.GameTime.ElapsedGameTime.TotalSeconds
    '        Else
    '            StatLag = 0
    '        End If

    '        If StatLag = 0 Then
    '            If Input.KeyPressed(Keys.Enter) Then
    '                Select Case Stats.MenuSelect
    '                    Case 0
    '                        If StatPoints > 0 Then
    '                            
    '                            ToonAllAtk += 1
    '                            StatAtkBoost += ToonLvl
    '                            ToonAtk = 5 + StatAtkBoost + ItemAtkBoost
    '                            StatPoints -= 1
    '                        End If
    '                    Case 1
    '                        If StatPoints > 0 Then
    '                           
    '                            ToonAllDef += 1
    '                            StatDefBoost += ToonLvl
    '                            ToonDef = StatDefBoost + ItemDefBoost
    '                            StatPoints -= 1
    '                        End If
    '                    Case 2
    '                        ShowingStats = False
    '                        
    '                        ScreenManager.UnloadScreen("StatsMenu")


    '                End Select
    '            End If
    '        End If
    '    End If
    'End Sub


    Public Overrides Sub Update()
        'Character Movement
        MoveTime += Globals.GameTime.ElapsedGameTime.TotalMilliseconds
        If MoveTime > 25 And ToonMoving Then
            If MoveDir = 0 And (ToonOffsetX <> 0 Or ToonOffsetY <> 0) Then
                'COmplete MOve Cycle before accepting inputs
                Move(LastDir)
            Else
                Move(LastDir)
            End If

            If ToonOffsetX = 0 And ToonOffsetY = 0 Then
                ToonMoving = False
            End If

            MoveTime = 0
        End If

        Select Case LastDir

            Case 1
                ToonFrontX = ToonX
                ToonFrontY = ToonY + 1
            Case 2
                ToonFrontX = ToonX - 1
                ToonFrontY = ToonY
            Case 3
                ToonFrontX = ToonX + 1
                ToonFrontY = ToonY

            Case 4
                ToonFrontX = ToonX
                ToonFrontY = ToonY - 1

            Case Else
                ToonFrontX = 1
                ToonFrontY = 1
        End Select

        'Update toon Coord
        ToonX = MapX + ToonScreenX
        ToonY = MapY + ToonScreenY

        'BattleMaybe(ToonX, ToonY)
        GetKey(ToonX, ToonY)

        'change floors
        ChangeFloor(ToonX, ToonY)

        'powerups
        GetItem(ToonX, ToonY)

        If XpTillLvl = 0 Then
            ToonLvl += 1
            XpGain -= XpTillLvl
            XpTillLvl = (ToonLvl * 2) ^ 2 + 10
            StatPoints += 5
            SkillPoints += 1
            ToonHP += ToonLvl * 50
            ShowLvlUp += 1
        End If
        StatAtkBoost = ToonLvl * ToonAllAtk
        StatDefBoost = ToonLvl * ToonAllDef

        If BossFight = False Then
            ToonDef = StatDefBoost + ItemDefBoost
            ToonAtk = 5 + StatAtkBoost + ItemAtkBoost
        End If
        '************************************************************************************************************************


    End Sub


    Public Overrides Sub Draw()

        'MyBase.Draw()
        Globals.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone)




        'Draw Tile Layer
        For DrawX = -1 To 16
            For DrawY = -1 To 15
                Dim X As Integer = DrawX + MapX
                Dim Y As Integer = DrawY + MapY

                If X >= 0 And X <= MapWidth And Y >= 0 And Y <= MapHeight Then
                    'Globals.SpriteBatch.Draw(Map.TileList(X, Y).TileGFX, New Rectangle(DrawX * TileSize, DrawY * TileSize, TileSize, TileSize), Map.TileList(X, Y).SrcRectangle, Color.White)


                                  'draw map layer
                                  Globals.SpriteBatch.Draw(Textures.SpriteSheet1, New Rectangle(DrawX * TileSize, DrawY * TileSize, TileSize, TileSize), Map.TileList(X, Y).SrcRectangle, Color.White)

                    If Interacters.InteractList(X, Y).Alive = True Then
                        'draw interact layer
                        Globals.SpriteBatch.Draw(Textures.SpriteSheet1, New Rectangle(DrawX * TileSize, DrawY * TileSize, TileSize, TileSize), Interacters.InteractList(X, Y).SrcRectangle, Color.White)
                    End If


                    If Enemies.EnemyList(X, Y).Alive = True Then
                        'draw Enemy layer
                        Globals.SpriteBatch.Draw(Textures.SpriteSheet1, New Rectangle(DrawX * TileSize, DrawY * TileSize, TileSize, TileSize), Enemies.EnemyList(X, Y).SrcRectangle, Color.White)

                    End If

                    'EnemyDefeat(ToonX, ToonY)

                    'View COord
                    If Debug.DebugMode = True Then
                        Globals.SpriteBatch.DrawString(Fonts.Arial_8, "X: " & X & vbCrLf & "Y: " & Y, New Vector2(DrawX * TileSize, DrawY * TileSize), Color.White)
                        Globals.SpriteBatch.DrawString(Fonts.Arial_8, ToonFrontX.ToString() + "," + ToonFrontY.ToString(), New Vector2(TileSize, TileSize * 0.85), Color.White, 0, New Vector2(0, 0), 10, SpriteEffects.None, 1)
                    End If
                End If

            Next
        Next
        'hud
        Globals.SpriteBatch.Draw(Textures.SpriteSheet1, New Rectangle(0, 0, Globals.GameSize.X, TileSize * 1.5), New Rectangle(0, 0, 1, 1), Color.White * 0.5F)
        'items and stuff on hud
        Globals.SpriteBatch.Draw(Textures.SpriteSheet1, New Rectangle(TileSize * 5, (TileSize * 1.5) / 2 - (TileSize / 2), TileSize, TileSize), Interacters.GetTileSource(InteractType.BronzeKey), Color.White)
        Globals.SpriteBatch.DrawString(Fonts.Arial_8, BronzeKeys, New Vector2(TileSize * 6, (TileSize * 1.5) / 2 - (TileSize / 2)), Color.White, 0, New Vector2(0, 0), 3, SpriteEffects.None, 1)
        Globals.SpriteBatch.Draw(Textures.SpriteSheet1, New Rectangle(TileSize * 7, (TileSize * 1.5) / 2 - (TileSize / 2), TileSize, TileSize), Interacters.GetTileSource(InteractType.SilverKey), Color.White)
        Globals.SpriteBatch.DrawString(Fonts.Arial_8, SilverKeys, New Vector2(TileSize * 8, (TileSize * 1.5) / 2 - (TileSize / 2)), Color.White, 0, New Vector2(0, 0), 3, SpriteEffects.None, 1)
        Globals.SpriteBatch.Draw(Textures.SpriteSheet1, New Rectangle(TileSize * 9, (TileSize * 1.5) / 2 - (TileSize / 2), TileSize, TileSize), Interacters.GetTileSource(InteractType.GoldKey), Color.White)
        Globals.SpriteBatch.DrawString(Fonts.Arial_8, GoldKeys, New Vector2(TileSize * 10, (TileSize * 1.5) / 2 - (TileSize / 2)), Color.White, 0, New Vector2(0, 0), 3, SpriteEffects.None, 1)

        'stats
        Globals.SpriteBatch.DrawString(Fonts.Arial_8, "LVL: " + ToonLvl.ToString(), New Vector2(TileSize, TileSize * 0.15), Color.White, 0, New Vector2(0, 0), 2, SpriteEffects.None, 1)
        Globals.SpriteBatch.DrawString(Fonts.Arial_8, "HP: " + ToonHP.ToString(), New Vector2(TileSize, TileSize * 0.5), Color.White, 0, New Vector2(0, 0), 2, SpriteEffects.None, 1)
        Globals.SpriteBatch.DrawString(Fonts.Arial_8, "Atk: " + ToonAtk.ToString(), New Vector2(TileSize * 3, TileSize * 0.15), Color.White, 0, New Vector2(0, 0), 2, SpriteEffects.None, 1)
        Globals.SpriteBatch.DrawString(Fonts.Arial_8, "Def: " + ToonDef.ToString(), New Vector2(TileSize * 3, TileSize * 0.5), Color.White, 0, New Vector2(0, 0), 2, SpriteEffects.None, 1)
        Globals.SpriteBatch.DrawString(Fonts.Arial_8, "Xp Till Lvl: " + XpTillLvl.ToString(), New Vector2(TileSize, TileSize * 0.85), Color.White, 0, New Vector2(0, 0), 2, SpriteEffects.None, 1)
        Globals.SpriteBatch.DrawString(Fonts.Arial_8, "Floor: " + CurrentFloor.ToString(), New Vector2(TileSize * 9.5, TileSize * 10.5), Color.Yellow, 0, New Vector2(0, 0), 2, SpriteEffects.None, 1)


        If StatPoints > 0 Then
            Globals.SpriteBatch.DrawString(Fonts.Arial_8, "You Have " + StatPoints.ToString() + " Stat Points Available", New Vector2(TileSize, TileSize * 2), Color.Yellow, 0, New Vector2(0, 0), 2, SpriteEffects.None, 1)
        End If
        If SkillPoints > 0 Then
            Globals.SpriteBatch.DrawString(Fonts.Arial_8, "You Have " + SkillPoints.ToString() + " Skill Points Available", New Vector2(TileSize, TileSize * 2.5), Color.Yellow, 0, New Vector2(0, 0), 2, SpriteEffects.None, 1)

        End If
        'Terry
        Globals.SpriteBatch.Draw(Textures.Terry, New Rectangle(ToonScreenX * TileSize, ToonScreenY * TileSize, TileSize, TileSize), FetchToonSrc(LastDir), Color.White)

        'test draws
        If BossFight = True Then
            BossBattle()
        End If

        'check for battle
        BattleMaybe(ToonX, ToonY)

        HandleMenuAppearing()
        LvledUp()
        'redraw after escape
        'make a haevy breathing animation or something to account for the still time
        'still time is needed to help get rid of artifacts and secretly drawing enemies
        'fixed now. but heavy breathing is nice to see :P
        If Redraw = True Then
            'Globals.SpriteBatch.DrawString(Fonts.Arial_8, Lag.ToString(), New Vector2(TileSize, TileSize * 0.85), Color.White, 0, New Vector2(0, 0), 10, SpriteEffects.None, 1)
            Globals.SpriteBatch.Draw(Textures.Terry, New Rectangle(TileSize, TileSize * 2, TileSize * 2, TileSize * 2), New Rectangle(0, 128, 32, 32), Color.White)

            Select Case LastDir
                Case 1
                    Enemies.EnemyList(EnemyPosition.X, EnemyPosition.Y).Alive = True
                    Enemies.EnemyList(EnemyPosition.X, EnemyPosition.Y).EnemyName = EnemyName


                Case 2
                    Enemies.EnemyList(EnemyPosition.X, EnemyPosition.Y).Alive = True
                    Enemies.EnemyList(EnemyPosition.X, EnemyPosition.Y).EnemyName = EnemyName

                Case 3
                    Enemies.EnemyList(EnemyPosition.X, EnemyPosition.Y).Alive = True
                    Enemies.EnemyList(EnemyPosition.X, EnemyPosition.Y).EnemyName = EnemyName

                Case 4
                    Enemies.EnemyList(EnemyPosition.X, EnemyPosition.Y).Alive = True
                    Enemies.EnemyList(EnemyPosition.X, EnemyPosition.Y).EnemyName = EnemyName



            End Select

            If Lag > 0 Then
                CanMove = False
                Lag -= Globals.GameTime.ElapsedGameTime.TotalSeconds
            ElseIf Lag <= 0 Then
                Redraw = False
                Battle = False
                CanMove = True
                SetBattleMenuStage = True
            End If
            If Lag < 2.5 And Lag > 2 Then
                Globals.SpriteBatch.Draw(Textures.Terry, New Rectangle(TileSize, TileSize * 2, TileSize * 2, TileSize * 2), New Rectangle(32, 128, 32, 32), Color.White)
            End If
            If Lag < 1.5 And Lag > 1 Then
                Globals.SpriteBatch.Draw(Textures.Terry, New Rectangle(TileSize, TileSize * 2, TileSize * 2, TileSize * 2), New Rectangle(32, 128, 32, 32), Color.White)
            End If
            If Lag < 0.5 And Lag > 0 Then
                Globals.SpriteBatch.Draw(Textures.Terry, New Rectangle(TileSize, TileSize * 2, TileSize * 2, TileSize * 2), New Rectangle(32, 128, 32, 32), Color.White)

            End If
        End If
        Globals.SpriteBatch.End()
    End Sub

    Public Battle As Boolean = False
    Public Lag As Double = 1000
    Public SetBattleMenuStage As Boolean = True
    Public Sub BattleMaybe(x As Integer, y As Integer)
        If Enemies.EnemyList(x, y).EnemyName <> EnemyType.Empty And Enemies.EnemyList(x, y).Alive = True Then
            'stop the damn menu from showing back up           
            If SetBattleMenuStage = True Then
                BattleMenuUp = 0
                SetBattleMenuStage = False


            End If

            Battle = True
            StartBattle(ToonX, ToonY)


        End If
    End Sub

    'stuff for battle
    Public BattleMenuUp As Integer = 0
    Public EnemyHP As Integer = 999
    Public EnemyAtk As Integer = 999
    Public EnemyDef As Integer = 999
    Public Shared BattleMenu As BattleMenu
    Public EnemyTurn As Boolean = False
    Public PowerTurns As Integer = 0
    Public HealTurns As Integer = 3
    Public Defend As Boolean = False
    Public Redraw As Boolean = False
    Public XpGain As Integer
    Public Shared BattleTime As Integer = 0
    Public ShowEffect As Integer = 10
    Public StartTimer As Boolean = False


    'save enemy name for redrawing
    Public EnemyName As String
    Public EnemyPosition As Vector2

    Public Sub Death()



        Debug.DeathCount += 1
        CanMove = True
        ScreenManager.AddScreen(New DeathMenu)
        ScreenManager.UnloadScreen(LoadThisMap)
        ScreenManager.UnloadScreen(BattleMenu.Name)
    End Sub
    Public Sub StartBattle(x As Integer, y As Integer)


        If Battle Then


            If BattleMenuUp = 0 Then
                BattleTime = 0
                StartTimer = False
                BattleMenu = New BattleMenu
                BattleMenuUp = 1
                If ToonHP < (ToonLvl * 200) Then
                    HealTurns = 3
                Else
                    HealTurns = 0
                End If

                MH.SaveOnClose()
                MH.SaveTerry()
                MH.ReSaveEnemies(Enemies, LoadThisMap)
                MH.ReSaveInteract(Interacters, LoadThisMap)

                'use this to edit stats for enemies easily
                Select Case Enemies.EnemyList(x, y).EnemyName
                    Case EnemyType.PinkSlime
                        EnemyHP = 10
                        EnemyAtk = 5
                        EnemyDef = 2
                        EnemyName = Enemies.EnemyList(x, y).EnemyName
                        XpGain = 4
                    'ShowXp = 4
                    Case EnemyType.RedSlime
                        EnemyHP = 250
                        EnemyAtk = 40
                        EnemyDef = 35
                        EnemyName = Enemies.EnemyList(x, y).EnemyName
                        XpGain = 15
                    'ShowXp = 15
                    Case EnemyType.Bat
                        EnemyHP = 60
                        EnemyAtk = 20
                        EnemyDef = 12
                        EnemyName = Enemies.EnemyList(x, y).EnemyName
                        XpGain = 9
                    'ShowXp = 9
                    Case EnemyType.Knight
                        EnemyHP = 300
                        EnemyAtk = 105
                        EnemyDef = 60
                        EnemyName = Enemies.EnemyList(x, y).EnemyName
                        XpGain = 40
                    'ShowXp = 40
                    Case EnemyType.HeavyKnight
                        EnemyHP = 1500
                        EnemyAtk = 300
                        EnemyDef = 200
                        EnemyName = Enemies.EnemyList(x, y).EnemyName
                        XpGain = 100
                    'ShowXp = 100
                    Case EnemyType.Golem
                        EnemyHP = 7000
                        EnemyAtk = 550
                        EnemyDef = 620
                        EnemyName = Enemies.EnemyList(x, y).EnemyName
                        XpGain = 170
                    'ShowXp = 170
                    Case EnemyType.AscendantFemale
                        EnemyHP = 8500
                        EnemyAtk = 800
                        EnemyDef = 1100
                        EnemyName = Enemies.EnemyList(x, y).EnemyName
                        XpGain = 250
                    'ShowXp = 250
                    Case EnemyType.AscendantMale
                        EnemyHP = 6000
                        EnemyAtk = 1000
                        EnemyDef = 800
                        EnemyName = Enemies.EnemyList(x, y).EnemyName
                        XpGain = 250
                    'ShowXp = 250
                    Case EnemyType.CursedAscendantFemale
                        EnemyHP = 15000
                        EnemyAtk = 1000
                        EnemyDef = 2200
                        EnemyName = Enemies.EnemyList(x, y).EnemyName
                        XpGain = 400
                    'ShowXp = 400
                    Case EnemyType.CursedAscendantMale
                        EnemyHP = 12000
                        EnemyAtk = 1600
                        EnemyDef = 1800
                        EnemyName = Enemies.EnemyList(x, y).EnemyName
                        XpGain = 400
                        'ShowXp = 400
                End Select

            ElseIf BattleMenuUp = 1 Then
                ScreenManager.AddScreen(BattleMenu)
                BattleMenuUp = 2
                'EnemyHP = Enemies.EnemyList(x, y).HP
                'EnemyAtk = Enemies.EnemyList(x, y).Atk
                'EnemyDef = Enemies.EnemyList(x, y).Def
                'EnemyName = Enemies.EnemyList(x, y).EnemyName
                'XpGain = Enemies.EnemyList(x, y).XP
                'ShowXp = Enemies.EnemyList(x, y).XP

                EnemyPosition.X = ToonX
                EnemyPosition.Y = ToonY


            ElseIf BattleMenuUp > 1 Then
                Globals.SpriteBatch.Draw(Textures.SpriteSheet1, New Rectangle(0, 0, Globals.GameSize.X, Globals.GameSize.Y), New Rectangle(16, 170, 1, 1), New Color(0, 0, 40))

                'enemy stats
                Globals.SpriteBatch.DrawString(Fonts.Arial_8, "HP: " + EnemyHP.ToString(), New Vector2(TileSize, TileSize * 0.15), Color.White, 0, New Vector2(0, 0), 2, SpriteEffects.None, 1)
                Globals.SpriteBatch.DrawString(Fonts.Arial_8, "Atk: " + EnemyAtk.ToString(), New Vector2(TileSize, TileSize * 0.5), Color.White, 0, New Vector2(0, 0), 2, SpriteEffects.None, 1)
                Globals.SpriteBatch.DrawString(Fonts.Arial_8, "Def: " + EnemyDef.ToString(), New Vector2(TileSize, TileSize * 0.85), Color.White, 0, New Vector2(0, 0), 2, SpriteEffects.None, 1)
                'make some pics or something later if you get a chance
                Globals.SpriteBatch.Draw(Textures.SpriteSheet1, New Rectangle(TileSize * 7, TileSize * 1, TileSize * 3, TileSize * 3), Enemies.EnemyList(x, y).SrcRectangle, Color.White)

                'terry stats
                Globals.SpriteBatch.DrawString(Fonts.Arial_8, "HP: " + ToonHP.ToString(), New Vector2(TileSize * 9.5, TileSize * 9.65), Color.White, 0, New Vector2(0, 0), 2, SpriteEffects.None, 1)
                Globals.SpriteBatch.DrawString(Fonts.Arial_8, "Atk: " + ToonAtk.ToString(), New Vector2(TileSize * 9.5, TileSize * 10), Color.White, 0, New Vector2(0, 0), 2, SpriteEffects.None, 1)
                Globals.SpriteBatch.DrawString(Fonts.Arial_8, "Def: " + ToonDef.ToString(), New Vector2(TileSize * 9.5, TileSize * 10.35), Color.White, 0, New Vector2(0, 0), 2, SpriteEffects.None, 1)
                'make some sort of pic for terry also
                Globals.SpriteBatch.Draw(Textures.Terry, New Rectangle(TileSize * 3, TileSize * 8, TileSize * 3, TileSize * 3), FetchToonSrc(4), Color.White)


                If Input.KeyPressed(Keys.Enter) And BattleMenuUp = 2 And BattleTime = 0 Then
                    StartTimer = True
                    Select Case BattleMenu.MenuSelect
                        Case 0
                            ShowEffect = BattleMenu.MenuSelect
                            If EnemyDef < ToonAtk * (1 + (SkillAttack * 0.35)) Then
                                EnemyHP -= ToonAtk * (1 + (SkillAttack * 0.35)) - EnemyDef
                            End If
                            EnemyTurn = True
                            PowerTurns += 1
                            HealTurns += 1
                            Music.Hit.Play()
                        Case 1
                            ShowEffect = BattleMenu.MenuSelect
                            Defend = True
                            EnemyTurn = True
                            PowerTurns += 1
                            HealTurns += 1
                        Case 2
                            ShowEffect = BattleMenu.MenuSelect
                            If EnemyDef < ToonAtk * (2 + SkillPower / 2.5) Then
                                EnemyHP -= (ToonAtk * (2 + SkillPower / 2.5)) - EnemyDef
                            End If

                            EnemyTurn = True
                            PowerTurns = 0
                            HealTurns += 1
                            BattleMenu.MenuSelect -= 1
                            Music.Hit.Play()
                        Case 3
                            ShowEffect = BattleMenu.MenuSelect
                            If ToonHP < ToonLvl * 200 Then
                                ToonHP += ((1 + SkillHeal) / 20) * (ToonLvl * 200)
                            End If
                            EnemyTurn = True
                            HealTurns = 0
                            PowerTurns += 1
                            BattleMenu.MenuSelect -= 2
                            Music.Heal.Play()
                        Case 4

                            EnemyDefeat(x, y)
                            Battle = False
                            EndBattle()
                            Redraw = True
                            Lag = 3
                            Success = False
                            StartTimer = False
                            BattleTime = 0

                            Select Case LastDir
                                Case 1
                                    MoveToon(4, ToonX, ToonY - 1)
                                    LastDir = 4
                                Case 2
                                    MoveToon(3, ToonX + 1, ToonY)
                                    LastDir = 3
                                Case 3
                                    MoveToon(2, ToonX - 1, ToonY)
                                    LastDir = 2
                                Case 4
                                    MoveToon(1, ToonX, ToonY + 1)
                                    LastDir = 1
                            End Select
                    End Select
                End If
                'used to check timer and select case
                'Globals.SpriteBatch.DrawString(Fonts.Arial_8, BattleTime.ToString(), New Vector2(300, 300), Color.White, 0, New Vector2(0, 0), 2, SpriteEffects.None, 1)
                'Globals.SpriteBatch.DrawString(Fonts.Arial_8, ShowEffect.ToString(), New Vector2(300, 350), Color.White, 0, New Vector2(0, 0), 2, SpriteEffects.None, 1)

                If StartTimer Then
                    BattleTime += 1
                    'Globals.GameTime.ElapsedGameTime.TotalSeconds
                End If
                If BattleTime > 0 And BattleTime < 40 Then

                    Select Case ShowEffect
                        Case 0
                            Globals.SpriteBatch.Draw(Textures.Menu, New Rectangle(TileSize * 6.5, TileSize * 0.5, TileSize * 4, TileSize * 4), New Rectangle(0, 96, 32, 32), Color.White)
                            If EnemyDef < ToonAtk * (1 + (SkillAttack * 0.35)) Then
                                Globals.SpriteBatch.DrawString(Fonts.Arial_8, "-" + (ToonAtk * (1 + (SkillAttack * 0.35)) - EnemyDef).ToString(), New Vector2(TileSize * 5, TileSize * 2), Color.Red, 0, New Vector2(0, 0), 3, SpriteEffects.None, 1)
                            Else
                                Globals.SpriteBatch.DrawString(Fonts.Arial_8, "-0", New Vector2(TileSize * 6, TileSize * 2), Color.Red, 0, New Vector2(0, 0), 3, SpriteEffects.None, 1)
                            End If

                        Case 2
                            Globals.SpriteBatch.Draw(Textures.Menu, New Rectangle(TileSize * 6.5, TileSize * 0.5, TileSize * 4, TileSize * 4), New Rectangle(32, 96, 32, 32), Color.White)
                            If EnemyDef < ToonAtk * (2 + SkillPower / 2.5) Then
                                Globals.SpriteBatch.DrawString(Fonts.Arial_8, "-" + ((ToonAtk * (2 + SkillPower / 2.5)) - EnemyDef).ToString(), New Vector2(TileSize * 5, TileSize * 2), Color.Red, 0, New Vector2(0, 0), 4, SpriteEffects.None, 1)
                            Else
                                Globals.SpriteBatch.DrawString(Fonts.Arial_8, "-0", New Vector2(TileSize * 6, TileSize * 2), Color.Red, 0, New Vector2(0, 0), 3, SpriteEffects.None, 1)
                            End If
                        Case 3
                            Globals.SpriteBatch.Draw(Textures.Menu, New Rectangle(TileSize * 3, TileSize * 7.5, TileSize * 3, TileSize * 3), New Rectangle(32, 64, 32, 32), Color.White * 0.9F)
                            Globals.SpriteBatch.DrawString(Fonts.Arial_8, "+" + (((1 + SkillHeal) / 20) * (ToonLvl * 200)).ToString, New Vector2(TileSize * 7, TileSize * 8), Color.Green, 0, New Vector2(0, 0), 3, SpriteEffects.None, 1)


                    End Select
                ElseIf BattleTime > 0 And ShowEffect = 1 Then
                    Globals.SpriteBatch.Draw(Textures.Menu, New Rectangle(TileSize * 3 - 32, TileSize * 7, TileSize * 4, TileSize * 4), New Rectangle(0, 64, 32, 32), Color.White * 0.9F)

                End If


                If PowerTurns > 2 Then
                        BattleMenu.Entries.Item(2).Enabled = True
                    Else
                        BattleMenu.Entries.Item(2).Enabled = False
                    End If

                    If HealTurns > 2 And ToonHP < ToonLvl * 200 Then
                        BattleMenu.Entries.Item(3).Enabled = True
                    Else
                        BattleMenu.Entries.Item(3).Enabled = False
                    End If


                'enemy attack
                If BattleTime > 65 And BattleTime < 105 Then


                    Globals.SpriteBatch.Draw(Textures.Menu, New Rectangle(TileSize * 3 - 32, TileSize * 7, TileSize * 4, TileSize * 4), New Rectangle(0, 96, 32, 32), Color.White)

                    If Defend Then

                        If (ToonDef * (2 + (SkillDefend * 0.4))) < EnemyAtk Then
                            Globals.SpriteBatch.DrawString(Fonts.Arial_8, "-" + (EnemyAtk - (ToonDef * (2 + (SkillDefend * 0.4)))).ToString, New Vector2(TileSize * 7, TileSize * 8), Color.Red, 0, New Vector2(0, 0), 3, SpriteEffects.None, 1)
                        Else
                            Globals.SpriteBatch.DrawString(Fonts.Arial_8, "-0", New Vector2(TileSize * 7, TileSize * 8), Color.Red, 0, New Vector2(0, 0), 3, SpriteEffects.None, 1)

                        End If

                    ElseIf ToonDef < EnemyAtk
                        Globals.SpriteBatch.DrawString(Fonts.Arial_8, "-" + (EnemyAtk - ToonDef).ToString, New Vector2(TileSize * 7, TileSize * 8), Color.Red, 0, New Vector2(0, 0), 3, SpriteEffects.None, 1)
                    Else
                        Globals.SpriteBatch.DrawString(Fonts.Arial_8, "-0", New Vector2(TileSize * 7, TileSize * 8), Color.Red, 0, New Vector2(0, 0), 3, SpriteEffects.None, 1)

                    End If

                    If EnemyTurn Then
                        Music.Hit.Play()
                        If Defend Then
                            If (ToonDef * (2 + SkillDefend / 5)) < EnemyAtk Then
                                ToonHP -= (EnemyAtk - (ToonDef * (2 + SkillDefend / 5)))



                            End If

                        ElseIf ToonDef < EnemyAtk Then
                            ToonHP -= (EnemyAtk - ToonDef)

                        End If
                        EnemyTurn = False
                    End If

                ElseIf BattleTime > 105
                    BattleTime = 0
                    StartTimer = False
                    Defend = False


                End If



                If EnemyHP <= 0 And BattleTime > 60 Then
                    ShowXp = XpGain
                    Redraw = False
                    Success = True
                    EnemyDefeat(x, y)
                    EndBattle()
                End If

                If ToonHP <= 0 Then
                    Death()
                    BattleTime = 0
                    StartTimer = False
                End If
                End If
            End If
    End Sub



    Public Sub EndBattle()
        Battle = False
        EnemyAtk = 999
        EnemyDef = 999
        EnemyHP = 999
        BattleMenuUp = -1
        ScreenManager.UnloadScreen(BattleMenu.Name)

        PowerTurns = 0
        HealTurns = 3
    End Sub

    Private i As Double = 0
    Public Success As Boolean
    Public Sub EnemyDefeat(x As Integer, y As Integer)

        Enemies.EnemyList(x, y).EnemyName = EnemyType.Empty
        Enemies.EnemyList(x, y).Alive = False

        If Success Then
            'handle leveling up
            Do While XpTillLvl < XpGain
                ToonLvl += 1
                XpGain -= XpTillLvl
                XpTillLvl = (ToonLvl * 2) ^ 2 + 10
                StatPoints += 5
                SkillPoints += 1
                ToonHP += ToonLvl * 50
                ShowLvlUp += 1
            Loop
            XpTillLvl -= XpGain

            Battle = False
            Redraw = False
            'If i <= 10 Then
            '    Globals.SpriteBatch.DrawString(Fonts.Arial_8, "Gained " & XpGain & "Experience", New Vector2(TileSize * 5, TileSize * 5), Color.Red)
            '    i += (Globals.GameTime.ElapsedGameTime.TotalSeconds / 60)

            'ElseIf i > 10
            '    i = 0

            '    Success = False
            'End If

        Else
            Redraw = True
        End If
    End Sub

    'finallY!!!!!
    Public ShowXp As Integer

    Public Sub HandleMenuAppearing()
        If Success Then
            If i <= 3 Then
                'Globals.SpriteBatch.DrawString(Fonts.Arial_8, "Gained " & XpGain & "Experience", New Vector2(TileSize * 5, TileSize * 5), Color.Red)
                Globals.SpriteBatch.DrawString(Fonts.Arial_8, "Gained " & ShowXp & " Experience", New Vector2(TileSize * 4, TileSize * 4), Color.Yellow, 0, New Vector2(0, 0), 2, SpriteEffects.None, 1)

                i += (Globals.GameTime.ElapsedGameTime.TotalSeconds)
                ' Globals.SpriteBatch.DrawString(Fonts.Arial_8, i, New Vector2(TileSize * 6, TileSize * 6), Color.Red)
                'Globals.SpriteBatch.DrawString(Fonts.Arial_8, SetBattleMenuStage, New Vector2(TileSize * 4, TileSize * 4), Color.Red)

                CanMove = True
                If i > 0.5 And SetBattleMenuStage = False Then
                    SetBattleMenuStage = True
                End If
                If i > 1 Then
                    i = 0
                    Success = False
                End If
            End If
        End If
    End Sub

    Private t As Double = 0
    Public ShowLvlUp As Integer = 0
    Public Sub LvledUp()
        If ShowLvlUp > 0 Then
            t += Globals.GameTime.ElapsedGameTime.TotalSeconds

            If t < 1.5 And t > 0 Then
                Globals.SpriteBatch.DrawString(Fonts.Arial_8, "Level Up!!!", New Vector2(TileSize * 4, (TileSize * 3) + (((10 / t)) * 1.5) + 0.1), Color.Yellow, 0, New Vector2(0, 0), 2, SpriteEffects.None, 1)


            ElseIf t > 1.5
                ShowLvlUp -= 1
                t = 0
            End If

        End If
    End Sub

    Public Shared GoldKeys As Integer = 0
    Public Shared SilverKeys As Integer = 0
    Public Shared BronzeKeys As Integer = 0

    Public Sub GetKey(x As Integer, y As Integer)
        If Interacters.InteractList(x, y).InteractItem = InteractType.GoldKey Then
            GoldKeys += 1
            Interacters.InteractList(x, y).Alive = False
            Interacters.InteractList(x, y).InteractItem = InteractType.Empty
            Music.Pickup.Play()
        ElseIf Interacters.InteractList(x, y).InteractItem = InteractType.SilverKey Then
            SilverKeys += 1
            Interacters.InteractList(x, y).Alive = False
            Interacters.InteractList(x, y).InteractItem = InteractType.Empty
            Music.Pickup.Play()
        ElseIf Interacters.InteractList(x, y).InteractItem = InteractType.BronzeKey Then
            BronzeKeys += 1
            Interacters.InteractList(x, y).Alive = False
            Interacters.InteractList(x, y).InteractItem = InteractType.Empty
            Music.Pickup.Play()
        End If
    End Sub

    Public Sub OpenDoor(x As Integer, y As Integer)

        If GoldKeys > 0 And (Interacters.InteractList(x, y).InteractItem = InteractType.GoldDoor Or Interacters.InteractList(x, y).InteractItem = InteractType.SideGoldDoor) Then
            Interacters.InteractList(x, y).Alive = False
            Interacters.InteractList(x, y).IsBlocked = False
            Interacters.InteractList(x, y).InteractItem = InteractType.Empty
            GoldKeys -= 1
        ElseIf SilverKeys > 0 And Interacters.InteractList(x, y).InteractItem = InteractType.SilverDoor Or Interacters.InteractList(x, y).InteractItem = InteractType.SideSilverDoor Then
            Interacters.InteractList(x, y).Alive = False
            Interacters.InteractList(x, y).IsBlocked = False
            Interacters.InteractList(x, y).InteractItem = InteractType.Empty
            SilverKeys -= 1
        ElseIf BronzeKeys > 0 And Interacters.InteractList(x, y).InteractItem = InteractType.BronzeDoor Or Interacters.InteractList(x, y).InteractItem = InteractType.SideBronzeDoor Then
            Interacters.InteractList(x, y).Alive = False
            Interacters.InteractList(x, y).IsBlocked = False
            Interacters.InteractList(x, y).InteractItem = InteractType.Empty
            BronzeKeys -= 1
        End If

    End Sub

    'Change Floors
    Public Sub ChangeFloor(x As Integer, y As Integer)
        If Interacters.InteractList(x, y).InteractItem = InteractType.PortalGreen Then
            Select Case LastDir
                Case 1
                    ReturnPoint = New Vector2((ToonScreenX - 10) + ToonX, (ToonScreenY - 10) + ToonY - 1)
                Case 2
                    ReturnPoint = New Vector2((ToonScreenX - 10) + ToonX + 1, (ToonScreenY - 10) + ToonY)
                Case 3
                    ReturnPoint = New Vector2((ToonScreenX - 10) + ToonX - 1, (ToonScreenY - 10) + ToonY)
                Case 4
                    ReturnPoint = New Vector2((ToonScreenX - 10) + ToonX, (ToonScreenY - 10) + ToonY + 1)

            End Select
            MH.ReSaveEnemies(Enemies, LoadThisMap)
            MH.ReSaveInteract(Interacters, LoadThisMap)
            MH.ReSaveMap(Map, LoadThisMap)
            NextMap += (CurrentFloor + 1).ToString

            MH.SaveOnClose()
            MH.SaveTerry()
            ScreenManager.UnloadScreen(LoadThisMap)
            ScreenManager.AddScreen(New WorldScreen(CurrentFloor + 1))




        End If

        If Interacters.InteractList(x, y).InteractItem = InteractType.PortalRed Then
            Select Case LastDir
                Case 1
                    ReturnPoint = New Vector2((ToonScreenX - 10) + ToonX, (ToonScreenY - 10) + ToonY - 1)
                Case 2
                    ReturnPoint = New Vector2((ToonScreenX - 10) + ToonX + 1, (ToonScreenY - 10) + ToonY)
                Case 3
                    ReturnPoint = New Vector2((ToonScreenX - 10) + ToonX - 1, (ToonScreenY - 10) + ToonY)
                Case 4
                    ReturnPoint = New Vector2((ToonScreenX - 10) + ToonX, (ToonScreenY - 10) + ToonY + 1)

            End Select
            MH.ReSaveEnemies(Enemies, LoadThisMap)
            MH.ReSaveInteract(Interacters, LoadThisMap)
            MH.ReSaveMap(Map, LoadThisMap)
            NextMap += (CurrentFloor - 1).ToString()

            MH.SaveOnClose()
            MH.SaveTerry()
            ScreenManager.UnloadScreen(LoadThisMap)
            ScreenManager.AddScreen(New WorldScreen(CurrentFloor - 1))

        End If
    End Sub

    Public Sub GetItem(x As Integer, y As Integer)


        If Interacters.InteractList(x, y).InteractItem = InteractType.BronzeSword Then
            ItemAtkBoost += 5
            Interacters.InteractList(x, y).Alive = False
            Interacters.InteractList(x, y).IsBlocked = False
            Interacters.InteractList(x, y).InteractItem = InteractType.Empty
            Music.Pickup.Play()
        ElseIf Interacters.InteractList(x, y).InteractItem = InteractType.BronzeShield Then
            ItemDefBoost += 5
            Interacters.InteractList(x, y).Alive = False
            Interacters.InteractList(x, y).IsBlocked = False
            Interacters.InteractList(x, y).InteractItem = InteractType.Empty
            Music.Pickup.Play()
        ElseIf Interacters.InteractList(x, y).InteractItem = InteractType.SilverSword Then
            ItemAtkBoost += 10
            Interacters.InteractList(x, y).Alive = False
            Interacters.InteractList(x, y).IsBlocked = False
            Interacters.InteractList(x, y).InteractItem = InteractType.Empty
            Music.Pickup.Play()
        ElseIf Interacters.InteractList(x, y).InteractItem = InteractType.SilverShield Then
            ItemDefBoost += 10
            Interacters.InteractList(x, y).Alive = False
            Interacters.InteractList(x, y).IsBlocked = False
            Interacters.InteractList(x, y).InteractItem = InteractType.Empty
            Music.Pickup.Play()
        ElseIf Interacters.InteractList(x, y).InteractItem = InteractType.GoldSword Then
            ItemAtkBoost += 15
            Interacters.InteractList(x, y).Alive = False
            Interacters.InteractList(x, y).IsBlocked = False
            Interacters.InteractList(x, y).InteractItem = InteractType.Empty
            Music.Pickup.Play()
        ElseIf Interacters.InteractList(x, y).InteractItem = InteractType.GoldShield Then
            ItemDefBoost += 15
            Interacters.InteractList(x, y).Alive = False
            Interacters.InteractList(x, y).IsBlocked = False
            Interacters.InteractList(x, y).InteractItem = InteractType.Empty
            Music.Pickup.Play()
        ElseIf Interacters.InteractList(x, y).InteractItem = InteractType.CursedSword Then
            ItemAtkBoost += 30
            Interacters.InteractList(x, y).Alive = False
            Interacters.InteractList(x, y).IsBlocked = False
            Interacters.InteractList(x, y).InteractItem = InteractType.Empty
            Music.Pickup.Play()
        ElseIf Interacters.InteractList(x, y).InteractItem = InteractType.CursedShield Then
            ItemDefBoost += 30
            Interacters.InteractList(x, y).Alive = False
            Interacters.InteractList(x, y).IsBlocked = False
            Interacters.InteractList(x, y).InteractItem = InteractType.Empty
            Music.Pickup.Play()
        End If
    End Sub
End Class
