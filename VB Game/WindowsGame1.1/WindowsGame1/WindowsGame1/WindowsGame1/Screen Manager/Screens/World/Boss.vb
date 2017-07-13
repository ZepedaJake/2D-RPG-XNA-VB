Partial Public Class WorldScreen

    Public Shared BossFight = False
    Private BossPowerTurns = 0
    Private BossHealTurns = 0
    Private rnd As Random = New Random
    Private BattleOption As Integer
    Private BossAttack As Integer = -1
    Private ChooseEnemyAttack As Boolean = True
    Public Shared BossInitialize As Integer = -1
    Private Transform As Boolean = False
    Private BossPhase As Integer = 0

    Private SphereSizeX As Integer = 32


    Private BossPowerCool = 4
    Private BossHealCool = 4





    Public Sub StartBoss()
        BossInitialize = 0
        'Try : Game1.SongPlay.Dispose() : Catch : End Try

        Game1.MessageCounter = 1
        ScreenManager.AddScreen(New Message("bossText", 255, 255, 255))

    End Sub

    Public Sub BossBattle()
        If BossInitialize = 0 Then
            If BossFight Then
                BattleMenu.Entries(4).Enabled = False
                BossPhase = 0
                BossHealCool = 4
                BossPowerCool = 4
                SphereSizeX = 32
                BossHealTurns = 0
                BossPowerTurns = 0
                BattleTime = 0
            End If

            If ToonHP < (ToonLvl * 200) Then
                HealTurns = 3
            Else
                HealTurns = 0
            End If

            EnemyHP = 70000
            EnemyAtk = 2000
            EnemyDef = 4000

            XpGain = 5000
            BossInitialize += 1
        End If


        Globals.SpriteBatch.Draw(Textures.SpriteSheet1, New Rectangle(0, 0, Globals.GameSize.X, Globals.GameSize.Y), New Rectangle(16, 170, 1, 1), New Color(0, 0, 40))

        'enemy stats
        Globals.SpriteBatch.DrawString(Fonts.Arial_8, "HP: " + EnemyHP.ToString(), New Vector2(TileSize, TileSize * 0.15), Color.White, 0, New Vector2(0, 0), 2, SpriteEffects.None, 1)
        Globals.SpriteBatch.DrawString(Fonts.Arial_8, "Atk: " + EnemyAtk.ToString(), New Vector2(TileSize, TileSize * 0.5), Color.White, 0, New Vector2(0, 0), 2, SpriteEffects.None, 1)
        Globals.SpriteBatch.DrawString(Fonts.Arial_8, "Def: " + EnemyDef.ToString(), New Vector2(TileSize, TileSize * 0.85), Color.White, 0, New Vector2(0, 0), 2, SpriteEffects.None, 1)
        'make some pics or something later if you get a chance
        If BossPhase = 0 Then
            Globals.SpriteBatch.Draw(Textures.Terry, New Rectangle(TileSize * 5, 0, TileSize * 6, TileSize * 6), New Rectangle(0, 256, 64, 64), Color.White)
        ElseIf BossPhase = 3 Then
            Globals.SpriteBatch.Draw(Textures.Terry, New Rectangle(TileSize * 5, 0, TileSize * 6, TileSize * 6), New Rectangle(192, 256, 64, 64), Color.White)

        End If
        'terry stats
        Globals.SpriteBatch.DrawString(Fonts.Arial_8, "HP: " + ToonHP.ToString(), New Vector2(TileSize * 9.5, TileSize * 9.65), Color.White, 0, New Vector2(0, 0), 2, SpriteEffects.None, 1)
        Globals.SpriteBatch.DrawString(Fonts.Arial_8, "Atk: " + ToonAtk.ToString(), New Vector2(TileSize * 9.5, TileSize * 10), Color.White, 0, New Vector2(0, 0), 2, SpriteEffects.None, 1)
        Globals.SpriteBatch.DrawString(Fonts.Arial_8, "Def: " + ToonDef.ToString(), New Vector2(TileSize * 9.5, TileSize * 10.35), Color.White, 0, New Vector2(0, 0), 2, SpriteEffects.None, 1)
        'make some sort of pic for terry also
        Globals.SpriteBatch.Draw(Textures.Terry, New Rectangle(TileSize * 3, TileSize * 8, TileSize * 3, TileSize * 3), FetchToonSrc(4), Color.White)

        If Debug.DebugMode = True Then
            Globals.SpriteBatch.DrawString(Fonts.Arial_8, BattleTime.ToString, New Vector2(TileSize * 4, TileSize * 4), Color.Yellow, 0, New Vector2(0, 0), 3, SpriteEffects.None, 1)
            Globals.SpriteBatch.DrawString(Fonts.Arial_8, BossAttack.ToString, New Vector2(TileSize * 4, TileSize * 5), Color.Yellow, 0, New Vector2(0, 0), 3, SpriteEffects.None, 1)
            Globals.SpriteBatch.DrawString(Fonts.Arial_8, BattleOption.ToString, New Vector2(TileSize * 4, TileSize * 6), Color.Yellow, 0, New Vector2(0, 0), 3, SpriteEffects.None, 1)
            Globals.SpriteBatch.DrawString(Fonts.Arial_8, ShowEffect.ToString, New Vector2(TileSize * 4, TileSize * 7), Color.Yellow, 0, New Vector2(0, 0), 3, SpriteEffects.None, 1)
        End If

        'INPUTS****************************************************************************************************
        If Input.KeyPressed(Keys.Enter) And BattleTime = 0 Then
            StartTimer = True
            Select Case BattleMenu.MenuSelect
                Case 0
                    ShowEffect = BattleMenu.MenuSelect


                    ToonAtk *= (1 + (SkillAttack * 0.35))

                    If EnemyDef < ToonAtk Then
                        EnemyHP -= ToonAtk - EnemyDef
                    End If


                    PowerTurns += 1
                    HealTurns += 1
                    Music.Hit.Play()
                Case 1
                    ShowEffect = BattleMenu.MenuSelect
                    Defend = True

                    ToonDef *= (2 + (SkillDefend * 0.4))


                    PowerTurns += 1
                    HealTurns += 1
                Case 2
                    ShowEffect = BattleMenu.MenuSelect


                    ToonAtk *= (2 + SkillPower / 2.5)

                    If EnemyDef < ToonAtk Then
                        EnemyHP -= ToonAtk - EnemyDef
                    End If


                    PowerTurns = 0
                    HealTurns += 1
                    BattleMenu.MenuSelect -= 1
                    Music.Hit.Play()
                Case 3
                    ShowEffect = BattleMenu.MenuSelect
                    If ToonHP < ToonLvl * 200 Then
                        ToonHP += ((1 + SkillHeal) / 20) * (ToonLvl * 200)
                    End If

                    HealTurns = 0
                    PowerTurns += 1
                    BattleMenu.MenuSelect -= 2
                    Music.Heal.Play()
            End Select
        End If
        'END INPUTS******************************************************************************************

        If StartTimer Then
            BattleTime += 1
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

        If ChooseEnemyAttack Then

            BattleOption = rnd.Next(0, 100)


            If PowerTurns > 3 Then
                If BattleOption >= 40 And BattleOption <= 75 Then
                    'defend
                    EnemyDef *= 2
                    BossAttack = 1
                ElseIf BattleOption >= 75 And BattleOption <= 83 And BossPowerTurns > BossPowerCool Then
                    'power attack
                    EnemyAtk *= 2
                    BossAttack = 2

                ElseIf BattleOption >= 83 And BattleOption <= 100 And BossHealTurns > BossHealCool And EnemyHP < 45000 Then
                    'heal
                    BossAttack = 3

                Else
                    'attack
                    BossAttack = 0
                End If
            Else
                If BattleOption >= 55 And BattleOption <= 75 Then
                    'defend
                    EnemyDef *= 2
                    BossAttack = 1
                ElseIf BattleOption >= 75 And BattleOption <= 83 And BossPowerTurns > 4
                    'power attack
                    EnemyAtk *= 2
                    BossAttack = 2
                ElseIf BattleOption >= 83 And BattleOption <= 100 And BossHealTurns > 4 And EnemyHP < 45000
                    'heal
                    BossAttack = 3
                Else
                    'attack
                    BossAttack = 0
                End If
            End If

            ChooseEnemyAttack = False
            EnemyTurn = True
        End If

        'Draw Effects*******************************
        If BattleTime > 0 And BattleTime < 40 Then
            If BossAttack = 1 Then
                Globals.SpriteBatch.Draw(Textures.Menu, New Rectangle(TileSize * 6.5, TileSize * 0.5, TileSize * 4, TileSize * 4), New Rectangle(0, 64, 32, 32), Color.White * 0.9F)
            End If

            Select Case ShowEffect
                    Case 0
                        'attack
                        Globals.SpriteBatch.Draw(Textures.Menu, New Rectangle(TileSize * 6.5, TileSize * 0.5, TileSize * 4, TileSize * 4), New Rectangle(0, 96, 32, 32), Color.White)
                        If EnemyDef < ToonAtk Then
                            Globals.SpriteBatch.DrawString(Fonts.Arial_8, "-" + (ToonAtk - EnemyDef).ToString(), New Vector2(TileSize * 5, TileSize * 2), Color.Red, 0, New Vector2(0, 0), 3, SpriteEffects.None, 1)
                        Else
                            Globals.SpriteBatch.DrawString(Fonts.Arial_8, "-0", New Vector2(TileSize * 6, TileSize * 2), Color.Red, 0, New Vector2(0, 0), 3, SpriteEffects.None, 1)
                        End If


                Case 2
                        'PowerAttack
                        Globals.SpriteBatch.Draw(Textures.Menu, New Rectangle(TileSize * 6.5, TileSize * 0.5, TileSize * 4, TileSize * 4), New Rectangle(32, 96, 32, 32), Color.White)
                        If EnemyDef < ToonAtk Then
                            Globals.SpriteBatch.DrawString(Fonts.Arial_8, "-" + (ToonAtk - EnemyDef).ToString(), New Vector2(TileSize * 5, TileSize * 2), Color.Red, 0, New Vector2(0, 0), 4, SpriteEffects.None, 1)
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
        'END EFFECTS*************************************************************

        'EFFECTS FOR BOSS********************************************************
        If BattleTime > 65 And BattleTime < 105 Then

            If BossAttack = 0 Then
                'attack
                Globals.SpriteBatch.Draw(Textures.Menu, New Rectangle(TileSize * 3 - 32, TileSize * 7, TileSize * 4, TileSize * 4), New Rectangle(0, 96, 32, 32), Color.White)
                If EnemyTurn Then
                    Music.Hit.Play()

                    'apply damage
                    If ToonDef < EnemyAtk Then
                        ToonHP -= (EnemyAtk - ToonDef)
                    End If
                    EnemyTurn = False
                End If

                'Draw damage
                If ToonDef < EnemyAtk Then
                    Globals.SpriteBatch.DrawString(Fonts.Arial_8, "-" + (EnemyAtk - ToonDef).ToString, New Vector2(TileSize * 7, TileSize * 8), Color.Red, 0, New Vector2(0, 0), 3, SpriteEffects.None, 1)
                Else
                    Globals.SpriteBatch.DrawString(Fonts.Arial_8, "-0", New Vector2(TileSize * 7, TileSize * 8), Color.Red, 0, New Vector2(0, 0), 3, SpriteEffects.None, 1)
                End If


            ElseIf BossAttack = 1
                'nothing
                'remember to reset def
            ElseIf BossAttack = 2
                'Power attack

                Globals.SpriteBatch.Draw(Textures.Menu, New Rectangle(TileSize * 3 - 32, TileSize * 7, TileSize * 4, TileSize * 4), New Rectangle(32, 96, 32, 32), Color.White)
                If EnemyTurn Then
                    Music.Hit.Play()

                    'apply damage
                    If ToonDef < EnemyAtk Then
                        ToonHP -= (EnemyAtk - ToonDef)
                    End If
                    EnemyTurn = False
                End If

                'drwa damage
                If ToonDef < EnemyAtk Then
                    Globals.SpriteBatch.DrawString(Fonts.Arial_8, "-" + (EnemyAtk - ToonDef).ToString, New Vector2(TileSize * 7, TileSize * 8), Color.Red, 0, New Vector2(0, 0), 3, SpriteEffects.None, 1)
                Else
                    Globals.SpriteBatch.DrawString(Fonts.Arial_8, "-0", New Vector2(TileSize * 7, TileSize * 8), Color.Red, 0, New Vector2(0, 0), 3, SpriteEffects.None, 1)
                End If

            ElseIf BossAttack = 3 Then

                If EnemyTurn Then
                    If EnemyHP > 10000 Then
                        EnemyHP += 5000
                    Else
                        EnemyHP += 12000

                    End If
                    Music.Heal.Play()
                    EnemyTurn = False
                End If
                    Globals.SpriteBatch.Draw(Textures.Menu, New Rectangle(TileSize * 6.5, TileSize * 0.5, TileSize * 3, TileSize * 3), New Rectangle(32, 64, 32, 32), Color.White * 0.9F)
                    If EnemyHP > 10000 Then
                        Globals.SpriteBatch.DrawString(Fonts.Arial_8, "+5000", New Vector2(TileSize * 6, TileSize * 2), Color.Green, 0, New Vector2(0, 0), 3, SpriteEffects.None, 1)
                    Else
                        Globals.SpriteBatch.DrawString(Fonts.Arial_8, "+12000", New Vector2(TileSize * 6, TileSize * 2), Color.Green, 0, New Vector2(0, 0), 3, SpriteEffects.None, 1)
                    End If
                End If
                End If
        'End Boss EFFECTS*****************************************************


        'Change Phase
        If EnemyHP <= 30000 And BossPhase = 0 Then
            Transform = True
        End If

        If BattleTime > 105 Then
            If Transform = False Or BossPhase = 3 Then
                BattleTime = 0
                BattleOption = -1
                'REset Toon Stats
                Select Case ShowEffect
                    Case 0
                        ToonAtk /= (1 + (SkillAttack * 0.35))
                    Case 1
                        ToonDef /= (2 + (SkillDefend * 0.4))
                    Case 2
                        ToonAtk /= (2 + (SkillPower / 2.5))

                End Select

                Select Case BossAttack

                    Case 1
                        EnemyDef /= 2
                    Case 2
                        EnemyAtk /= 2
                        BossPowerTurns = 0
                    Case 3
                        BossHealTurns = 0
                End Select

                BossHealTurns += 1
                BossPowerTurns += 1


                StartTimer = False
                Defend = False
                ChooseEnemyAttack = True
            Else

                If BattleTime > 105 And BattleTime < 165 Then
                    BossPhase = 1
                    Globals.SpriteBatch.Draw(Textures.Terry, New Rectangle(TileSize * 5, 0, TileSize * 6, TileSize * 6), New Rectangle(64, 256, 64, 64), Color.White)
                ElseIf BattleTime > 165 And BattleTime < 205 Then
                    BossPhase = 2

                    Globals.SpriteBatch.Draw(Textures.Terry, New Rectangle(TileSize * 5, 0, TileSize * 6, TileSize * 6), New Rectangle(128, 256, 64, 64), Color.White)
                ElseIf BattleTime > 205 Then
                    BossPhase = 3

                    BossPowerCool -= 2
                    BossHealCool -= 2
                    EnemyAtk += 200
                    EnemyDef += 1000

                    BattleTime = 0
                    BattleOption = -1
                    'REset Toon Stats
                    Select Case ShowEffect
                        Case 0
                            ToonAtk /= (1 + SkillAttack / 5)
                        Case 1
                            ToonDef /= (2 + SkillDefend / 5)
                        Case 2
                            ToonAtk /= (2 + (SkillPower / 2.5))

                    End Select

                    Select Case BossAttack

                        Case 1
                            EnemyDef /= 2
                        Case 2
                            EnemyAtk /= 2
                            BossPowerTurns = 0
                        Case 3
                            BossHealTurns = 0
                    End Select

                    BossHealTurns += 1
                    BossPowerTurns += 1


                    StartTimer = False
                    Defend = False
                    ChooseEnemyAttack = True
                End If
                If BattleTime > 125 And BattleTime < 165 Then
                    SphereSizeX += (BattleTime - 125)


                    Globals.SpriteBatch.Draw(Textures.Terry, New Rectangle(TileSize * 8 - (SphereSizeX / 2), TileSize * 2.5 - (SphereSizeX / 2), SphereSizeX, SphereSizeX), New Rectangle(32, 224, 32, 32), Color.White * 0.9F)
                End If
            End If
        End If

        If EnemyHP <= 0 And BattleTime > 60 Then
            ShowXp = XpGain
            Redraw = False
            Success = True

            EndBattle()
            BossFight = False
            BossInitialize = -1
            CanMove = True

            ScreenManager.UnloadScreen(LoadThisMap)
            Try : Game1.SongPlay.Dispose() : Catch : End Try
            MapSong = 0
            Game1.SongPlaying = False

            ScreenManager.AddScreen(New Message("endText", 255, 255, 255))

        End If

        If ToonHP <= 0 Then
            Death()
            BattleTime = 0
            StartTimer = False
            BossFight = False
            ChooseEnemyAttack = True
            BossInitialize = -1

        End If

    End Sub
End Class
